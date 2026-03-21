using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GogInstaller
{
    public partial class BatchDialog : Form
    {
        private MainDialog _mainDialog;
        private CancellationTokenSource _batchCts;
        private bool _killProcess = false;
        private TextHelpDialog _helpDialog = null;

        public BatchDialog(MainDialog main)
        {
            InitializeComponent();

            _mainDialog = main;
            lblInfo.Text = _mainDialog.GetLastFolder();
            btnBatchRun.Text = $"Run Batch → {Path.GetPathRoot(Environment.GetEnvironmentVariable("TEMP"))}";
            textboxParams.Text = Properties.Settings.Default.BatchCmd;
            checkboxRecursive.Checked = Properties.Settings.Default.BatchScanSubfolders;

            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvFiles.AutoGenerateColumns = false;
            dgvFiles.AllowUserToAddRows = false;
            dgvFiles.RowHeadersVisible = false;
            dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Column 1: Checkbox (Enable)
            DataGridViewCheckBoxColumn colCheck = new DataGridViewCheckBoxColumn();
            colCheck.HeaderText = "Enable";
            colCheck.Name = "colEnable";
            colCheck.Width = 50;
            dgvFiles.Columns.Add(colCheck);

            // Column 2: File Name
            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.HeaderText = "File Name";
            colName.Name = "colFileName";
            colName.ReadOnly = true;
            dgvFiles.Columns.Add(colName);

            // Column 3: Status
            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.MinimumWidth = 130;
            dgvFiles.Columns.Add(colStatus);
        }

        private void SetSelectedRowsState(bool isChecked)
        {
            if (dgvFiles.SelectedRows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in dgvFiles.SelectedRows)
            {
                // Update the 'Enable' checkbox column
                row.Cells["colEnable"].Value = isChecked;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select a folder to scan for EXE files";
                fbd.ShowNewFolderButton = false;
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                fbd.SelectedPath = lblInfo.Text; // Use last selected folder as starting point

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    // Update selected folder
                    lblInfo.Text = fbd.SelectedPath;
                    lblInfo.Update();

                    // Save last folder to settings
                    Properties.Settings.Default.LastFolder = fbd.SelectedPath;
                    Properties.Settings.Default.Save();

                    // 1. Get all .exe files in the selected directory
                    // Use SearchOption.TopDirectoryOnly or SearchOption.AllDirectories
                    string[] exeFiles = Directory.GetFiles(fbd.SelectedPath, "*.exe", checkboxRecursive.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                    if (exeFiles.Length == 0)
                    {
                        MessageBox.Show("No executable files found in this folder.", "Information");
                        return;
                    }

                    // 2. Prevent the grid from flickering during bulk add
                    dgvFiles.SuspendLayout();
                    dgvFiles.Rows.Clear();

                    foreach (string file in exeFiles)
                    {
                        // Remove the root folder path to get the subfolder + filename
                        // Example: C:\MyApps\FolderA\app.exe -> FolderA\app.exe
                        string relativePath = file.Replace(fbd.SelectedPath, "").TrimStart(Path.DirectorySeparatorChar);

                        // Check if file is already in the grid to avoid duplicates
                        bool alreadyExists = dgvFiles.Rows.Cast<DataGridViewRow>()
                            .Any(r => r.Cells["colFileName"].Value.ToString() == relativePath);

                        if (!alreadyExists)
                        {
                            // Add to grid: [Checked, FileName, Status]
                            // Store the full path in the Tag property for easy launching later
                            int rowIndex = dgvFiles.Rows.Add(true, relativePath, "");
                            dgvFiles.Rows[rowIndex].Tag = file;
                        }
                    }

                    dgvFiles.AutoResizeColumns();
                    dgvFiles.ResumeLayout();
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            SetSelectedRowsState(true);
        }

        private void btnUncheck_Click(object sender, EventArgs e)
        {
            SetSelectedRowsState(false);
        }

        private async void btnBatchRun_Click(object sender, EventArgs e)
        {
            // Check TEMP folder
            string tempPath = Environment.GetEnvironmentVariable("TEMP");
            try
            {
                if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating {tempPath}\n{ex.Message}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _batchCts = new CancellationTokenSource();
            btnCancelBatch.Visible = true;
            btnBatchRun.Enabled = false;
            btnOpen.Enabled = false;

            DataGridViewRow Row = null;
            try
            {
                foreach (DataGridViewRow row in dgvFiles.Rows)
                {
                    Row = row;
                    // 1. Check if row is enabled and stop if cancelled
                    bool isEnabled = Convert.ToBoolean(row.Cells["colEnable"].Value);
                    if (!isEnabled) continue;
                    if (_batchCts.Token.IsCancellationRequested) break;

                    string exePath = row.Tag?.ToString();
                    if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath)) continue;

                    // 2. Update Status and Scroll to the row
                    row.Cells["colStatus"].Style.ForeColor = SystemColors.WindowText;
                    row.Cells["colStatus"].Value = "Running...";
                    dgvFiles.FirstDisplayedScrollingRowIndex = row.Index;

                    // 3. Launch and Wait
                    Tools.UnblockFile(exePath);
                    int exitCode = await RunProcessAsync(exePath, _batchCts.Token);

                    // Report status
                    Color color;
                    string status;
                    switch (exitCode)
                    {
                        case 0:
                            color = Color.Green;
                            status = $"Return: {exitCode} (Ok)";
                            break;
                        case 1:
                            color = Color.Red;
                            status = $"Return: {exitCode} (Failed to initialize)";
                            break;
                        case 2:
                            color = Color.Red;
                            status = $"Return: {exitCode} (User decline)";
                            break;
                        case 3:
                        case 4:
                            color = Color.Red;
                            status = $"Return: {exitCode} (Fatal error)";
                            break;
                        case 5:
                            color = Color.Red;
                            status = $"Return: {exitCode} (User interrupt)";
                            break;
                        case 6:
                            color = Color.Red;
                            status = $"Return: {exitCode} (Terminated)";
                            break;
                        case 7:
                            color = Color.Red;
                            status = $"Return: {exitCode} (Cannot proceed)";
                            break;
                        case 8:
                            color = Color.Red;
                            status = $"Return: {exitCode} (Reboot required)";
                            break;
                        case -66:
                            color = SystemColors.WindowText;
                            status = "Killed";
                            break;
                        case -67:
                            color = SystemColors.WindowText;
                            status = "Running... (Aborted)";
                            break;
                        default:
                            {
                                // Format the return code as hex 0xC000041D
                                string hexError = "0x" + exitCode.ToString("X8");
                                color = Color.Red;
                                status = $"Return: {hexError}";
                            }
                            break;
                    }
                    row.Cells["colStatus"].Style.ForeColor = color;
                    row.Cells["colStatus"].Value = status;
                }

                //lblStatus.Text = _batchCts.Token.IsCancellationRequested ? "Batch Cancelled." : "Batch Complete.";
                SystemSounds.Beep.Play();
            }
            catch (TaskCanceledException)
            {
                if (Row != null)
                {
                    Row.Cells["colStatus"].Style.ForeColor = SystemColors.WindowText;
                    Row.Cells["colStatus"].Value = "Aborted";
                }
                SystemSounds.Hand.Play();
            }
            catch (Exception ex)
            {
                if (Row != null)
                {
                    Row.Cells["colStatus"].Style.ForeColor = SystemColors.WindowText;
                    Row.Cells["colStatus"].Value = "Failed";
                }
                MessageBox.Show($"Batch Error: {ex.Message}", _mainDialog.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnBatchRun.Enabled = true;
                btnOpen.Enabled = true;
                btnCancelBatch.Visible = false;
                _batchCts.Dispose();
            }
        }

        private void btnCancelBatch_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Kill running process now?\n" +
                "(press \"No\" if you want for it to finish)", "Abort batch job",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            switch (dialogResult)
            {
                case DialogResult.Yes: // Kill process
                    _killProcess = true;
                    break;
                case DialogResult.No: // Don't kill
                    _killProcess = false;
                    break;
                default: // Cancel the abort
                    return;
            }

            // Cancel job
            _batchCts?.Cancel();
        }

        private async Task<int> RunProcessAsync(string path, CancellationToken token)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo = new ProcessStartInfo
                {
                    FileName = path,
                    WorkingDirectory = Path.GetDirectoryName(path),
                    UseShellExecute = false, // Inherits your TEMP/TMP variables
                    Arguments = textboxParams.Text // Commandline parameters for silent install
                };

                proc.Start();
                int currentPid = proc.Id; // Capture PID immediately
                //await Task.Delay(2000, token);

                // 4. Wait for exit without blocking the UI thread
                int exitCode = await Task.Run(() =>
                {
                    while (!proc.HasExited)
                    {
                        if (token.IsCancellationRequested)
                        {
                            if (_killProcess)
                            {
                                KillProcessTree(currentPid);
                                return -66; // Killed
                            }
                            else
                            {
                                return -67; // Not killed
                            }
                        }
                        Thread.Sleep(100);
                    }
                    return proc.ExitCode;
                });

                return exitCode;
            }
        }

        private void KillProcessTree(int pid)
        {
            try
            {
                // /F = Force, /T = Tree (Include children), /PID = Process ID
                ProcessStartInfo psi = new ProcessStartInfo("taskkill", $"/F /T /PID {pid}")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi).WaitForExit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to kill tree: {ex.Message}");
            }
        }

        private void BatchDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.BatchCmd = textboxParams.Text.Trim();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.BatchCmd))
            {
                Properties.Settings.Default.BatchCmdDefault = Properties.Settings.Default.BatchCmd;
            }
            Properties.Settings.Default.BatchScanSubfolders = checkboxRecursive.Checked;
            Properties.Settings.Default.Save();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textboxParams.Text = Properties.Settings.Default.BatchCmdDefault;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textboxParams.Text = "";
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedRows.Count == 1)
            {
                MoveRow(-1);
                return;
            }

            // 1. Get selected rows sorted by their current position (Top to Bottom)
            var selectedRows = dgvFiles.SelectedRows.Cast<DataGridViewRow>()
                                .OrderBy(r => r.Index)
                                .ToList();

            if (selectedRows.Count == 0 || selectedRows[0].Index == 0) return;

            foreach (var row in selectedRows)
            {
                int oldIndex = row.Index;
                dgvFiles.Rows.RemoveAt(oldIndex);
                dgvFiles.Rows.Insert(oldIndex - 1, row);
                row.Selected = true; // Keep selection active
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dgvFiles.SelectedRows.Count == 1)
            {
                MoveRow(1);
                return;
            }

            // 2. Get selected rows sorted by their current position (Bottom to Top)
            // We reverse the order for moving down so rows don't "step on each other"
            var selectedRows = dgvFiles.SelectedRows.Cast<DataGridViewRow>()
                                .OrderByDescending(r => r.Index)
                                .ToList();

            if (selectedRows.Count == 0 || selectedRows[0].Index == dgvFiles.Rows.Count - 1) return;

            foreach (var row in selectedRows)
            {
                int oldIndex = row.Index;
                dgvFiles.Rows.RemoveAt(oldIndex);
                dgvFiles.Rows.Insert(oldIndex + 1, row);
                row.Selected = true;
            }
        }

        private void MoveRow(int direction)
        {
            if (dgvFiles.SelectedRows.Count == 0) return;

            DataGridViewRow selectedRow = dgvFiles.SelectedRows[0];
            int currentIndex = selectedRow.Index;
            int newIndex = currentIndex + direction;

            // Check bounds
            if (newIndex < 0 || newIndex >= dgvFiles.Rows.Count) return;

            // 1. Remove the row at current position
            dgvFiles.Rows.RemoveAt(currentIndex);

            // 2. Insert it at the new position
            dgvFiles.Rows.Insert(newIndex, selectedRow);

            // 3. Keep the row selected so the user can "spam" the button to move it far
            dgvFiles.ClearSelection();
            dgvFiles.Rows[newIndex].Selected = true;

            // Ensure the row is visible if it moves off-screen
            dgvFiles.FirstDisplayedScrollingRowIndex = Math.Max(0, newIndex - 2);
        }

        private void linklabelParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_helpDialog == null)
            {
                _helpDialog = new TextHelpDialog(Properties.Resources.ParamsHelp);
            }
            _helpDialog.MyShow();
        }
    }
}
