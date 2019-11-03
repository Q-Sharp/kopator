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
            this.oTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tbIgnore = new System.Windows.Forms.TextBox();
            this.lblCollect = new System.Windows.Forms.Label();
            this.tbCollect = new System.Windows.Forms.TextBox();
            this.btCollectDotDotDot = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbPaths.SuspendLayout();
            this.oTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.oProgressBar.Location = new System.Drawing.Point(68, 19);
            this.oProgressBar.Name = "oProgressBar";
            this.oProgressBar.Size = new System.Drawing.Size(354, 23);
            this.oProgressBar.Step = 1;
            this.oProgressBar.TabIndex = 3;
            // 
            // tbSource
            // 
            this.tbSource.Location = new System.Drawing.Point(61, 13);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(354, 20);
            this.tbSource.TabIndex = 4;
            // 
            // tbDestiny
            // 
            this.tbDestiny.Location = new System.Drawing.Point(61, 47);
            this.tbDestiny.Name = "tbDestiny";
            this.tbDestiny.Size = new System.Drawing.Size(354, 20);
            this.tbDestiny.TabIndex = 5;
            // 
            // btSourceDotDotDot
            // 
            this.btSourceDotDotDot.Location = new System.Drawing.Point(419, 13);
            this.btSourceDotDotDot.Name = "btSourceDotDotDot";
            this.btSourceDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btSourceDotDotDot.TabIndex = 6;
            this.btSourceDotDotDot.Text = "...";
            this.btSourceDotDotDot.UseVisualStyleBackColor = true;
            this.btSourceDotDotDot.Click += new System.EventHandler(this.btSourceDotDotDot_Click);
            // 
            // btDestinyDotDotDot
            // 
            this.btDestinyDotDotDot.Location = new System.Drawing.Point(419, 46);
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
            this.lSource.Location = new System.Drawing.Point(15, 17);
            this.lSource.Name = "lSource";
            this.lSource.Size = new System.Drawing.Size(40, 13);
            this.lSource.TabIndex = 8;
            this.lSource.Text = "Quelle:";
            // 
            // lDestiny
            // 
            this.lDestiny.AutoSize = true;
            this.lDestiny.Location = new System.Drawing.Point(28, 50);
            this.lDestiny.Name = "lDestiny";
            this.lDestiny.Size = new System.Drawing.Size(27, 13);
            this.lDestiny.TabIndex = 9;
            this.lDestiny.Text = "Ziel:";
            // 
            // gbPaths
            // 
            this.gbPaths.Controls.Add(this.oTabControl);
            this.gbPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPaths.Location = new System.Drawing.Point(5, 5);
            this.gbPaths.Margin = new System.Windows.Forms.Padding(5);
            this.gbPaths.Name = "gbPaths";
            this.gbPaths.Size = new System.Drawing.Size(479, 131);
            this.gbPaths.TabIndex = 11;
            this.gbPaths.TabStop = false;
            this.gbPaths.Text = "Pfadangaben";
            // 
            // oTabControl
            // 
            this.oTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.oTabControl.Controls.Add(this.tabPage1);
            this.oTabControl.Controls.Add(this.tabPage2);
            this.oTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oTabControl.Location = new System.Drawing.Point(3, 16);
            this.oTabControl.Name = "oTabControl";
            this.oTabControl.SelectedIndex = 0;
            this.oTabControl.Size = new System.Drawing.Size(473, 112);
            this.oTabControl.TabIndex = 10;
            this.oTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.oTabControl_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lSource);
            this.tabPage1.Controls.Add(this.tbDestiny);
            this.tabPage1.Controls.Add(this.tbSource);
            this.tabPage1.Controls.Add(this.lDestiny);
            this.tabPage1.Controls.Add(this.btDestinyDotDotDot);
            this.tabPage1.Controls.Add(this.btSourceDotDotDot);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(465, 83);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kopieren";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.tbIgnore);
            this.tabPage2.Controls.Add(this.lblCollect);
            this.tabPage2.Controls.Add(this.tbCollect);
            this.tabPage2.Controls.Add(this.btCollectDotDotDot);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(465, 83);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sammeln";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Ignorieren:";
            // 
            // tbIgnore
            // 
            this.tbIgnore.Location = new System.Drawing.Point(61, 47);
            this.tbIgnore.Name = "tbIgnore";
            this.tbIgnore.Size = new System.Drawing.Size(354, 20);
            this.tbIgnore.TabIndex = 12;
            // 
            // lblCollect
            // 
            this.lblCollect.AutoSize = true;
            this.lblCollect.Location = new System.Drawing.Point(15, 17);
            this.lblCollect.Name = "lblCollect";
            this.lblCollect.Size = new System.Drawing.Size(32, 13);
            this.lblCollect.TabIndex = 11;
            this.lblCollect.Text = "Pfad:";
            // 
            // tbCollect
            // 
            this.tbCollect.Location = new System.Drawing.Point(61, 13);
            this.tbCollect.Name = "tbCollect";
            this.tbCollect.Size = new System.Drawing.Size(354, 20);
            this.tbCollect.TabIndex = 9;
            // 
            // btCollectDotDotDot
            // 
            this.btCollectDotDotDot.Location = new System.Drawing.Point(419, 13);
            this.btCollectDotDotDot.Name = "btCollectDotDotDot";
            this.btCollectDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btCollectDotDotDot.TabIndex = 10;
            this.btCollectDotDotDot.Text = "...";
            this.btCollectDotDotDot.UseVisualStyleBackColor = true;
            this.btCollectDotDotDot.Click += new System.EventHandler(this.btCollectDotDotDot_Click);
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
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(489, 262);
            this.tlpMain.TabIndex = 12;
            // 
            // gbControls
            // 
            this.gbControls.Controls.Add(this.btClose);
            this.gbControls.Controls.Add(this.cbMove);
            this.gbControls.Controls.Add(this.btCopy);
            this.gbControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbControls.Location = new System.Drawing.Point(5, 208);
            this.gbControls.Margin = new System.Windows.Forms.Padding(5);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(479, 49);
            this.gbControls.TabIndex = 13;
            this.gbControls.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oProgressBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 146);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 52);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fortschritt";
            // 
            // kopatorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 262);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "kopatorMainForm";
            this.Text = "kopator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.kopatorMainForm_FormClosing);
            this.Load += new System.EventHandler(this.kopatorMainForm_Load);
            this.gbPaths.ResumeLayout(false);
            this.oTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private System.Windows.Forms.TabControl oTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblCollect;
        private System.Windows.Forms.TextBox tbCollect;
        private System.Windows.Forms.Button btCollectDotDotDot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIgnore;
    }
}

