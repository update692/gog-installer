using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;  // Add a reference to System.Configuration.dll

namespace GogInstaller
{
    public partial class MainDialog : Form
    {
        // Win32
        private const int WM_DEVICECHANGE = 0x0219;
        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

        // Class fields
        private string _exePath = "";
        private double _oldFreeGB = 0;

        public MainDialog()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += MainDialog_DragEnter;
            this.DragDrop += MainDialog_DragDrop;

            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            string simpleVersion = $"{v.Major}.{v.Minor}";
            this.Text += $" {simpleVersion}";

            LoadDrives();
            MainDialog_Resize(null, null);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_DEVICECHANGE)
            {
                int eventType = m.WParam.ToInt32();
                if (eventType == DBT_DEVICEARRIVAL || eventType == DBT_DEVICEREMOVECOMPLETE)
                {
                    btnRefresh_Click(null, null);
                }
            }
        }

        public string GetLastFolder()
        {
            // Get last folder from settings
            string lastFolder = Properties.Settings.Default.LastFolder;
            if (!Directory.Exists(lastFolder))
            {
                try { lastFolder = Path.GetPathRoot(lastFolder); } catch { }
                if (!Directory.Exists(lastFolder))
                {
                    lastFolder = Properties.Settings.Default.LastDrive;
                }
            }
            return lastFolder;
        }

        private void LoadDrives()
        {
            var driveList = DriveInfo.GetDrives()
                .Where(d => d.IsReady && (d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Removable || d.DriveType == DriveType.Ram))
                .Select(d => new DriveItem // Use the named class here
                {
                    Drive = d,
                    DisplayText = $"{d.Name} {ToFixedLength(d.VolumeLabel, 20)} {(d.AvailableFreeSpace / 1073741824.0):F1} GB free"
                })
                .ToList();

            driveComboBox.DataSource = driveList;
            driveComboBox.DisplayMember = "DisplayText";
            driveComboBox.ValueMember = "Drive"; // This points to the 'Drive' property of DriveItem
        }

        private void GuiUpdateStatus()
        {
            lblStatus.Text = $"File:\n{_exePath}\nDir:\n{(string.IsNullOrEmpty(_exePath) ? "" : Path.GetDirectoryName(_exePath))}\n" +
                $"TEMP={Environment.GetEnvironmentVariable("TEMP")}\n" +
                $"TMP={Environment.GetEnvironmentVariable("TMP")}";
            btnLaunch.Text = $"Run installer → {Path.GetPathRoot(Environment.GetEnvironmentVariable("TEMP"))}";
        }

        private void UpdateDriveSettings(DriveInfo selectedDrive)
        {
            // Display Free Space
            double freeGB = selectedDrive.AvailableFreeSpace / 1024.0 / 1024.0 / 1024.0;
            lblFreeSpace.Text = $"Free space: {freeGB:F2} GB";

            // Set TEMP/TMP Environment Variables
            string tempPath = Path.Combine(selectedDrive.Name, "TEMP");
            try
            {
                // Set for current process
                Environment.SetEnvironmentVariable("TEMP", tempPath, EnvironmentVariableTarget.Process);
                Environment.SetEnvironmentVariable("TMP", tempPath, EnvironmentVariableTarget.Process);
            }
            catch (Exception ex) { MessageBox.Show("Error setting TEMP: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            // Save to Application Settings
            Properties.Settings.Default.LastDrive = selectedDrive.Name;
            Properties.Settings.Default.Save();

            GuiUpdateStatus();
        }

        private void RestorelastDrive()
        {
            string savedDrive = Properties.Settings.Default.LastDrive;
            if (!string.IsNullOrEmpty(savedDrive))
            {
                // Search the DataSource for the item matching the saved drive name
                var items = driveComboBox.DataSource as List<DriveItem>;
                var match = items?.FirstOrDefault(i => i.Drive.Name == savedDrive);

                if (match != null)
                {
                    driveComboBox.SelectedItem = match;
                }
                // Explicitly call logic if SelectedIndexChanged doesn't fire automatically
                driveComboBox_SelectedIndexChanged(null, null);
            }
        }

        private void DropFile(string filename)
        {
            _exePath = filename;
            GuiUpdateStatus();
        }

        private void MainDialog_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && Path.GetExtension(files[0]).ToLower() == ".exe")
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainDialog_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            DropFile(files[0]);
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            RestorelastDrive();
            tmrFreeSpace.Start();
        }

        private void MainDialog_Resize(object sender, EventArgs e)
        {
            btnLaunch.Left = (this.ClientSize.Width - btnLaunch.Width) / 2;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            driveComboBox.Tag = 1; // Don't save last drive on combobox selected item change
            LoadDrives();
            driveComboBox.Tag = null;
            RestorelastDrive();
        }

        private void tmrFreeSpace_Tick(object sender, EventArgs e)
        {
            tmrFreeSpace.Stop();

            if (!driveComboBox.DroppedDown && driveComboBox.Tag == null && driveComboBox.SelectedValue is DriveInfo selectedDrive)
            {
                try
                {
                    double freeGB = selectedDrive.AvailableFreeSpace / 1024.0 / 1024.0 / 1024.0;
                    if (Math.Abs(_oldFreeGB - freeGB) > 0.1)
                    {
                        _oldFreeGB = freeGB;
                        btnRefresh_Click(null, null);
                    }
                }
                catch
                {
                    btnRefresh_Click(null, null);
                }
            }

            tmrFreeSpace.Start();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            // Check EXE file
            if (string.IsNullOrEmpty(_exePath) || !File.Exists(_exePath))
            {
                MessageBox.Show("No .EXE file to run.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

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

            try
            {
                // Launch the process using the previously set Environment Variables (TEMP/TMP)
                Tools.UnblockFile(_exePath);
                Process.Start(new ProcessStartInfo
                {
                    FileName = _exePath,
                    WorkingDirectory = Path.GetDirectoryName(_exePath),
                    UseShellExecute = false // Ensures it inherits the process environment variables
                });

                btnLaunch.Enabled = false;
                tmrLaunch.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch: {ex.Message}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmrLaunch_Tick(object sender, EventArgs e)
        {
            tmrLaunch.Stop();
            btnLaunch.Enabled = true;
        }

        private void driveComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (driveComboBox.Tag == null && driveComboBox.SelectedValue is DriveInfo selectedDrive)
            {
                UpdateDriveSettings(selectedDrive);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                // Get last folder from settings
                string lastFolder = GetLastFolder();

                openFileDialog.InitialDirectory = lastFolder;
                openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Select Executable File";
                openFileDialog.Multiselect = false;
                openFileDialog.CheckFileExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save last folder to settings
                    Properties.Settings.Default.LastFolder = Path.GetDirectoryName(openFileDialog.FileName);
                    Properties.Settings.Default.Save();

                    DropFile(openFileDialog.FileName);
                }
            }
        }

        public static string ToFixedLength(string text, int length)
        {
            if (string.IsNullOrEmpty(text)) return new string(' ', length);
            return (text.Length <= length ? text : text.Substring(0, length)).PadRight(length);
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            using (var batchDialog = new BatchDialog(this))
            {
                batchDialog.ShowDialog();
            }
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            var tempFolder = Environment.GetEnvironmentVariable("TEMP");
            exploreTEMPFolderToolStripMenuItem.ToolTipText = tempFolder;
            editConfigFileToolStripMenuItem.ToolTipText = "Close the program before editing config file";
            popupTools.Show(btnTools, new Point(0, btnTools.Height));
        }

        private void exploreTEMPFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tempFolder = Environment.GetEnvironmentVariable("TEMP");

            // Check if TEMP folder exists
            if (!Directory.Exists(tempFolder))
            {
                MessageBox.Show($"{tempFolder}\nFolder doesn't exist yet; it will be created upon launching the installer.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Open TEMP folder
            var psi = new ProcessStartInfo
            {
                FileName = tempFolder,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        private void editConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }

    public class DriveItem
    {
        public DriveInfo Drive { get; set; }
        public string DisplayText { get; set; }
    }
}
