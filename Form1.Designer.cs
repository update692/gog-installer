namespace GogInstaller
{
    partial class MainDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.driveComboBox = new System.Windows.Forms.ComboBox();
            this.lblFreeSpace = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnResresh = new System.Windows.Forms.Button();
            this.tmrFreeSpace = new System.Windows.Forms.Timer(this.components);
            this.btnOpen = new System.Windows.Forms.Button();
            this.tmrLaunch = new System.Windows.Forms.Timer(this.components);
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnTools = new System.Windows.Forms.Button();
            this.popupTools = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exploreTEMPFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTEMPFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.popupTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // driveComboBox
            // 
            this.driveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.driveComboBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driveComboBox.FormattingEnabled = true;
            this.driveComboBox.Location = new System.Drawing.Point(12, 12);
            this.driveComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.driveComboBox.Name = "driveComboBox";
            this.driveComboBox.Size = new System.Drawing.Size(304, 22);
            this.driveComboBox.TabIndex = 2;
            this.driveComboBox.SelectedIndexChanged += new System.EventHandler(this.driveComboBox_SelectedIndexChanged);
            // 
            // lblFreeSpace
            // 
            this.lblFreeSpace.AutoSize = true;
            this.lblFreeSpace.Location = new System.Drawing.Point(405, 16);
            this.lblFreeSpace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFreeSpace.Name = "lblFreeSpace";
            this.lblFreeSpace.Size = new System.Drawing.Size(80, 15);
            this.lblFreeSpace.TabIndex = 4;
            this.lblFreeSpace.Text = "lblFreeSpace";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(11, 50);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(761, 201);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "lblStatus";
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLaunch.Location = new System.Drawing.Point(323, 268);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(152, 31);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "Run installer";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnResresh
            // 
            this.btnResresh.Location = new System.Drawing.Point(323, 12);
            this.btnResresh.Name = "btnResresh";
            this.btnResresh.Size = new System.Drawing.Size(75, 23);
            this.btnResresh.TabIndex = 3;
            this.btnResresh.Text = "Refresh";
            this.btnResresh.UseVisualStyleBackColor = true;
            this.btnResresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tmrFreeSpace
            // 
            this.tmrFreeSpace.Interval = 1500;
            this.tmrFreeSpace.Tick += new System.EventHandler(this.tmrFreeSpace_Tick);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(11, 268);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(152, 31);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Select installer...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tmrLaunch
            // 
            this.tmrLaunch.Interval = 3000;
            this.tmrLaunch.Tick += new System.EventHandler(this.tmrLaunch_Tick);
            // 
            // btnBatch
            // 
            this.btnBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBatch.Location = new System.Drawing.Point(620, 268);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(152, 31);
            this.btnBatch.TabIndex = 6;
            this.btnBatch.Text = "Batch install...";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // btnTools
            // 
            this.btnTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTools.Location = new System.Drawing.Point(697, 12);
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(75, 23);
            this.btnTools.TabIndex = 7;
            this.btnTools.Text = "Tools";
            this.btnTools.UseVisualStyleBackColor = true;
            this.btnTools.Click += new System.EventHandler(this.btnTools_Click);
            // 
            // popupTools
            // 
            this.popupTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exploreTEMPFolderToolStripMenuItem,
            this.clearTEMPFolderToolStripMenuItem});
            this.popupTools.Name = "popupTools";
            this.popupTools.Size = new System.Drawing.Size(181, 48);
            // 
            // exploreTEMPFolderToolStripMenuItem
            // 
            this.exploreTEMPFolderToolStripMenuItem.Name = "exploreTEMPFolderToolStripMenuItem";
            this.exploreTEMPFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exploreTEMPFolderToolStripMenuItem.Text = "Explore TEMP folder";
            this.exploreTEMPFolderToolStripMenuItem.Click += new System.EventHandler(this.exploreTEMPFolderToolStripMenuItem_Click);
            // 
            // clearTEMPFolderToolStripMenuItem
            // 
            this.clearTEMPFolderToolStripMenuItem.Enabled = false;
            this.clearTEMPFolderToolStripMenuItem.Name = "clearTEMPFolderToolStripMenuItem";
            this.clearTEMPFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearTEMPFolderToolStripMenuItem.Text = "Clear TEMP folder";
            this.clearTEMPFolderToolStripMenuItem.Click += new System.EventHandler(this.clearTEMPFolderToolStripMenuItem_Click);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 311);
            this.Controls.Add(this.btnBatch);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnResresh);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblFreeSpace);
            this.Controls.Add(this.driveComboBox);
            this.Controls.Add(this.btnTools);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainDialog";
            this.Text = "GOG Installer";
            this.Load += new System.EventHandler(this.MainDialog_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainDialog_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainDialog_DragEnter);
            this.Resize += new System.EventHandler(this.MainDialog_Resize);
            this.popupTools.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox driveComboBox;
        private System.Windows.Forms.Label lblFreeSpace;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnResresh;
        private System.Windows.Forms.Timer tmrFreeSpace;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Timer tmrLaunch;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnTools;
        private System.Windows.Forms.ContextMenuStrip popupTools;
        private System.Windows.Forms.ToolStripMenuItem exploreTEMPFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTEMPFolderToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

