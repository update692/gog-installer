namespace GogInstaller
{
    partial class BatchDialog
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
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.btnOpen = new System.Windows.Forms.Button();
            this.checkboxRecursive = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnUncheck = new System.Windows.Forms.Button();
            this.btnBatchRun = new System.Windows.Forms.Button();
            this.btnCancelBatch = new System.Windows.Forms.Button();
            this.textboxParams = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.linklabelParams = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFiles
            // 
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiles.Location = new System.Drawing.Point(14, 14);
            this.dgvFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFiles.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFiles.Size = new System.Drawing.Size(905, 380);
            this.dgvFiles.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(14, 470);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(177, 36);
            this.btnOpen.TabIndex = 7;
            this.btnOpen.Text = "Select batch folder...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // checkboxRecursive
            // 
            this.checkboxRecursive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkboxRecursive.AutoSize = true;
            this.checkboxRecursive.Location = new System.Drawing.Point(209, 480);
            this.checkboxRecursive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkboxRecursive.Name = "checkboxRecursive";
            this.checkboxRecursive.Size = new System.Drawing.Size(138, 19);
            this.checkboxRecursive.TabIndex = 8;
            this.checkboxRecursive.Text = "Also scan subfolders";
            this.checkboxRecursive.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(13, 412);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "lblInfo";
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheck.Location = new System.Drawing.Point(384, 477);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(73, 23);
            this.btnCheck.TabIndex = 9;
            this.btnCheck.Text = "Check";
            this.toolTip1.SetToolTip(this.btnCheck, "Operation on selected files");
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnUncheck
            // 
            this.btnUncheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUncheck.Location = new System.Drawing.Point(463, 477);
            this.btnUncheck.Name = "btnUncheck";
            this.btnUncheck.Size = new System.Drawing.Size(73, 23);
            this.btnUncheck.TabIndex = 10;
            this.btnUncheck.Text = "Un-check";
            this.toolTip1.SetToolTip(this.btnUncheck, "Operation on selected files");
            this.btnUncheck.UseVisualStyleBackColor = true;
            this.btnUncheck.Click += new System.EventHandler(this.btnUncheck_Click);
            // 
            // btnBatchRun
            // 
            this.btnBatchRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBatchRun.Location = new System.Drawing.Point(742, 470);
            this.btnBatchRun.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnBatchRun.Name = "btnBatchRun";
            this.btnBatchRun.Size = new System.Drawing.Size(177, 36);
            this.btnBatchRun.TabIndex = 0;
            this.btnBatchRun.Text = "Run Batch";
            this.btnBatchRun.UseVisualStyleBackColor = true;
            this.btnBatchRun.Click += new System.EventHandler(this.btnBatchRun_Click);
            // 
            // btnCancelBatch
            // 
            this.btnCancelBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelBatch.Location = new System.Drawing.Point(842, 436);
            this.btnCancelBatch.Name = "btnCancelBatch";
            this.btnCancelBatch.Size = new System.Drawing.Size(77, 23);
            this.btnCancelBatch.TabIndex = 13;
            this.btnCancelBatch.Text = "Abort";
            this.btnCancelBatch.UseVisualStyleBackColor = true;
            this.btnCancelBatch.Visible = false;
            this.btnCancelBatch.Click += new System.EventHandler(this.btnCancelBatch_Click);
            // 
            // textboxParams
            // 
            this.textboxParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxParams.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxParams.Location = new System.Drawing.Point(150, 437);
            this.textboxParams.Name = "textboxParams";
            this.textboxParams.Size = new System.Drawing.Size(491, 22);
            this.textboxParams.TabIndex = 4;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(717, 436);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(64, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(647, 436);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.Location = new System.Drawing.Point(542, 477);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(73, 23);
            this.btnUp.TabIndex = 11;
            this.btnUp.Text = "Up ↑";
            this.toolTip1.SetToolTip(this.btnUp, "Operation on selected files");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Location = new System.Drawing.Point(621, 477);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(73, 23);
            this.btnDown.TabIndex = 12;
            this.btnDown.Text = "Down ↓";
            this.toolTip1.SetToolTip(this.btnDown, "Operation on selected files");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // linklabelParams
            // 
            this.linklabelParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linklabelParams.AutoSize = true;
            this.linklabelParams.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linklabelParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklabelParams.Location = new System.Drawing.Point(11, 442);
            this.linklabelParams.Name = "linklabelParams";
            this.linklabelParams.Size = new System.Drawing.Size(131, 13);
            this.linklabelParams.TabIndex = 14;
            this.linklabelParams.TabStop = true;
            this.linklabelParams.Text = "Command line parameters:";
            this.linklabelParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabelParams_LinkClicked);
            // 
            // BatchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.linklabelParams);
            this.Controls.Add(this.btnBatchRun);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.textboxParams);
            this.Controls.Add(this.btnCancelBatch);
            this.Controls.Add(this.btnUncheck);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.checkboxRecursive);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.dgvFiles);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "BatchDialog";
            this.Text = "Batch install";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.CheckBox checkboxRecursive;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnUncheck;
        private System.Windows.Forms.Button btnBatchRun;
        private System.Windows.Forms.Button btnCancelBatch;
        private System.Windows.Forms.TextBox textboxParams;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.LinkLabel linklabelParams;
    }
}