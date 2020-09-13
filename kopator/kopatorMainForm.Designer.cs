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
            this.gbPaths = new System.Windows.Forms.GroupBox();
            this.oTabControl = new System.Windows.Forms.TabControl();
            this.tabCopy = new System.Windows.Forms.TabPage();
            this.tabCollect = new System.Windows.Forms.TabPage();
            this.tabCatalog = new System.Windows.Forms.TabPage();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.oSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbPaths.SuspendLayout();
            this.oTabControl.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.gbControls.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMove
            // 
            this.cbMove.AutoSize = true;
            this.cbMove.Location = new System.Drawing.Point(301, 18);
            this.cbMove.Name = "cbMove";
            this.cbMove.Size = new System.Drawing.Size(91, 17);
            this.cbMove.TabIndex = 0;
            this.cbMove.Text = "Verschieben?";
            this.cbMove.UseVisualStyleBackColor = true;
            this.cbMove.CheckedChanged += new System.EventHandler(this.cbMove_CheckedChanged);
            // 
            // btCopy
            // 
            this.btCopy.Location = new System.Drawing.Point(398, 14);
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
            this.oProgressBar.Location = new System.Drawing.Point(10, 19);
            this.oProgressBar.Name = "oProgressBar";
            this.oProgressBar.Size = new System.Drawing.Size(478, 23);
            this.oProgressBar.Step = 1;
            this.oProgressBar.TabIndex = 3;
            // 
            // gbPaths
            // 
            this.gbPaths.Controls.Add(this.oTabControl);
            this.gbPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPaths.Location = new System.Drawing.Point(5, 5);
            this.gbPaths.Margin = new System.Windows.Forms.Padding(5);
            this.gbPaths.Name = "gbPaths";
            this.gbPaths.Size = new System.Drawing.Size(499, 155);
            this.gbPaths.TabIndex = 11;
            this.gbPaths.TabStop = false;
            this.gbPaths.Text = "Pfadangaben";
            // 
            // oTabControl
            // 
            this.oTabControl.Controls.Add(this.tabCopy);
            this.oTabControl.Controls.Add(this.tabCollect);
            this.oTabControl.Controls.Add(this.tabCatalog);
            this.oTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oTabControl.Location = new System.Drawing.Point(3, 16);
            this.oTabControl.Name = "oTabControl";
            this.oTabControl.Padding = new System.Drawing.Point(5, 3);
            this.oTabControl.SelectedIndex = 0;
            this.oTabControl.Size = new System.Drawing.Size(493, 136);
            this.oTabControl.TabIndex = 10;
            this.oTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.oTabControl_Selected);
            // 
            // tabCopy
            // 
            this.tabCopy.Location = new System.Drawing.Point(4, 22);
            this.tabCopy.Margin = new System.Windows.Forms.Padding(0);
            this.tabCopy.Name = "tabCopy";
            this.tabCopy.Padding = new System.Windows.Forms.Padding(5);
            this.tabCopy.Size = new System.Drawing.Size(485, 110);
            this.tabCopy.TabIndex = 0;
            this.tabCopy.Text = "Kopieren";
            this.tabCopy.UseVisualStyleBackColor = true;
            // 
            // tabCollect
            // 
            this.tabCollect.Location = new System.Drawing.Point(4, 22);
            this.tabCollect.Margin = new System.Windows.Forms.Padding(0);
            this.tabCollect.Name = "tabCollect";
            this.tabCollect.Padding = new System.Windows.Forms.Padding(5);
            this.tabCollect.Size = new System.Drawing.Size(485, 85);
            this.tabCollect.TabIndex = 1;
            this.tabCollect.Text = "Sammeln";
            this.tabCollect.UseVisualStyleBackColor = true;
            // 
            // tabCatalog
            // 
            this.tabCatalog.Location = new System.Drawing.Point(4, 22);
            this.tabCatalog.Margin = new System.Windows.Forms.Padding(0);
            this.tabCatalog.Name = "tabCatalog";
            this.tabCatalog.Padding = new System.Windows.Forms.Padding(5);
            this.tabCatalog.Size = new System.Drawing.Size(485, 85);
            this.tabCatalog.TabIndex = 2;
            this.tabCatalog.Tag = "";
            this.tabCatalog.Text = "Katalogisieren";
            this.tabCatalog.UseVisualStyleBackColor = true;
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
            this.tlpMain.Size = new System.Drawing.Size(509, 286);
            this.tlpMain.TabIndex = 12;
            // 
            // gbControls
            // 
            this.gbControls.Controls.Add(this.btClose);
            this.gbControls.Controls.Add(this.cbMove);
            this.gbControls.Controls.Add(this.btCopy);
            this.gbControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbControls.Location = new System.Drawing.Point(5, 232);
            this.gbControls.Margin = new System.Windows.Forms.Padding(5);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(499, 49);
            this.gbControls.TabIndex = 13;
            this.gbControls.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.oProgressBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 170);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 52);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fortschritt";
            // 
            // kopatorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 286);
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
            this.tlpMain.ResumeLayout(false);
            this.gbControls.ResumeLayout(false);
            this.gbControls.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.FolderBrowserDialog oFolderBrowserDialog;
        private System.Windows.Forms.GroupBox gbPaths;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbControls;
        private System.Windows.Forms.TabControl oTabControl;
        private System.Windows.Forms.TabPage tabCollect;
        private System.Windows.Forms.TabPage tabCatalog;
        private System.Windows.Forms.TabPage tabCopy;
        public System.Windows.Forms.ProgressBar oProgressBar;
        public System.Windows.Forms.CheckBox cbMove;
        private System.Windows.Forms.SaveFileDialog oSaveFileDialog;
    }
}

