namespace kopator
{
    partial class kopatorMainForm
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
            this.cbMove = new System.Windows.Forms.CheckBox();
            this.btCopy = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.oFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.oProgressBar = new System.Windows.Forms.ProgressBar();
            this.tbSource = new System.Windows.Forms.TextBox();
            this.tbDestiny = new System.Windows.Forms.TextBox();
            this.btSourceDotDotDot = new System.Windows.Forms.Button();
            this.btDestinyDotDotDot = new System.Windows.Forms.Button();
            this.lSource = new System.Windows.Forms.Label();
            this.lDestiny = new System.Windows.Forms.Label();
            this.gbPaths = new System.Windows.Forms.GroupBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbPaths.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.gbControls.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMove
            // 
            this.cbMove.AutoSize = true;
            this.cbMove.Location = new System.Drawing.Point(245, 18);
            this.cbMove.Name = "cbMove";
            this.cbMove.Size = new System.Drawing.Size(91, 17);
            this.cbMove.TabIndex = 0;
            this.cbMove.Text = "Verschieben?";
            this.cbMove.UseVisualStyleBackColor = true;
            this.cbMove.CheckedChanged += new System.EventHandler(this.cbMove_CheckedChanged);
            // 
            // btCopy
            // 
            this.btCopy.Location = new System.Drawing.Point(345, 14);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(90, 23);
            this.btCopy.TabIndex = 1;
            this.btCopy.Text = "Kopieren";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btClose
            // 
            this.btClose.Location = new System.Drawing.Point(10, 14);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(90, 23);
            this.btClose.TabIndex = 2;
            this.btClose.Text = "Schließen";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // oProgressBar
            // 
            this.oProgressBar.Enabled = false;
            this.oProgressBar.Location = new System.Drawing.Point(53, 19);
            this.oProgressBar.Name = "oProgressBar";
            this.oProgressBar.Size = new System.Drawing.Size(354, 23);
            this.oProgressBar.Step = 1;
            this.oProgressBar.TabIndex = 3;
            // 
            // tbSource
            // 
            this.tbSource.Location = new System.Drawing.Point(53, 21);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(354, 20);
            this.tbSource.TabIndex = 4;
            // 
            // tbDestiny
            // 
            this.tbDestiny.Location = new System.Drawing.Point(53, 55);
            this.tbDestiny.Name = "tbDestiny";
            this.tbDestiny.Size = new System.Drawing.Size(354, 20);
            this.tbDestiny.TabIndex = 5;
            // 
            // btSourceDotDotDot
            // 
            this.btSourceDotDotDot.Location = new System.Drawing.Point(411, 21);
            this.btSourceDotDotDot.Name = "btSourceDotDotDot";
            this.btSourceDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btSourceDotDotDot.TabIndex = 6;
            this.btSourceDotDotDot.Text = "...";
            this.btSourceDotDotDot.UseVisualStyleBackColor = true;
            this.btSourceDotDotDot.Click += new System.EventHandler(this.btSourceDotDotDot_Click);
            // 
            // btDestinyDotDotDot
            // 
            this.btDestinyDotDotDot.Location = new System.Drawing.Point(411, 54);
            this.btDestinyDotDotDot.Name = "btDestinyDotDotDot";
            this.btDestinyDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btDestinyDotDotDot.TabIndex = 7;
            this.btDestinyDotDotDot.Text = "...";
            this.btDestinyDotDotDot.UseVisualStyleBackColor = true;
            this.btDestinyDotDotDot.Click += new System.EventHandler(this.btDestinyDotDotDot_Click);
            // 
            // lSource
            // 
            this.lSource.AutoSize = true;
            this.lSource.Location = new System.Drawing.Point(7, 25);
            this.lSource.Name = "lSource";
            this.lSource.Size = new System.Drawing.Size(40, 13);
            this.lSource.TabIndex = 8;
            this.lSource.Text = "Quelle:";
            // 
            // lDestiny
            // 
            this.lDestiny.AutoSize = true;
            this.lDestiny.Location = new System.Drawing.Point(20, 58);
            this.lDestiny.Name = "lDestiny";
            this.lDestiny.Size = new System.Drawing.Size(27, 13);
            this.lDestiny.TabIndex = 9;
            this.lDestiny.Text = "Ziel:";
            // 
            // gbPaths
            // 
            this.gbPaths.Controls.Add(this.lSource);
            this.gbPaths.Controls.Add(this.tbSource);
            this.gbPaths.Controls.Add(this.btDestinyDotDotDot);
            this.gbPaths.Controls.Add(this.btSourceDotDotDot);
            this.gbPaths.Controls.Add(this.lDestiny);
            this.gbPaths.Controls.Add(this.tbDestiny);
            this.gbPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPaths.Location = new System.Drawing.Point(5, 5);
            this.gbPaths.Margin = new System.Windows.Forms.Padding(5);
            this.gbPaths.Name = "gbPaths";
            this.gbPaths.Size = new System.Drawing.Size(443, 83);
            this.gbPaths.TabIndex = 11;
            this.gbPaths.TabStop = false;
            this.gbPaths.Text = "Pfadangaben";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.gbControls, 0, 2);
            this.tlpMain.Controls.Add(this.groupBox1, 0, 1);
            this.tlpMain.Controls.Add(this.gbPaths, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(453, 214);
            this.tlpMain.TabIndex = 12;
            // 
            // gbControls
            // 
            this.gbControls.Controls.Add(this.btClose);
            this.gbControls.Controls.Add(this.cbMove);
            this.gbControls.Controls.Add(this.btCopy);
            this.gbControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbControls.Location = new System.Drawing.Point(5, 160);
            this.gbControls.Margin = new System.Windows.Forms.Padding(5);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(443, 49);
            this.gbControls.TabIndex = 13;
            this.gbControls.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oProgressBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 98);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 52);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fortschritt";
            // 
            // kopatorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 214);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "kopatorMainForm";
            this.Text = "kopator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.kopatorMainForm_FormClosing);
            this.Load += new System.EventHandler(this.kopatorMainForm_Load);
            this.gbPaths.ResumeLayout(false);
            this.gbPaths.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.gbControls.ResumeLayout(false);
            this.gbControls.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbMove;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.FolderBrowserDialog oFolderBrowserDialog;
        private System.Windows.Forms.ProgressBar oProgressBar;
        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.TextBox tbDestiny;
        private System.Windows.Forms.Button btSourceDotDotDot;
        private System.Windows.Forms.Button btDestinyDotDotDot;
        private System.Windows.Forms.Label lSource;
        private System.Windows.Forms.Label lDestiny;
        private System.Windows.Forms.GroupBox gbPaths;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbControls;
    }
}

