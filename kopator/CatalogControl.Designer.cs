namespace kopator
{
    partial class CatalogControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btCSVDotDotDot = new System.Windows.Forms.Button();
            this.tbCSV = new System.Windows.Forms.TextBox();
            this.btSourceDotDotDot = new System.Windows.Forms.Button();
            this.tbSource = new System.Windows.Forms.TextBox();
            this.lSource = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 21);
            this.label3.TabIndex = 21;
            this.label3.Text = "csv:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btCSVDotDotDot, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbCSV, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btSourceDotDotDot, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbSource, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lSource, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbType, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(465, 95);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // btCSVDotDotDot
            // 
            this.btCSVDotDotDot.Location = new System.Drawing.Point(438, 45);
            this.btCSVDotDotDot.Name = "btCSVDotDotDot";
            this.btCSVDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btCSVDotDotDot.TabIndex = 19;
            this.btCSVDotDotDot.Text = "...";
            this.btCSVDotDotDot.UseVisualStyleBackColor = true;
            this.btCSVDotDotDot.Click += new System.EventHandler(this.btCSVDotDotDot_Click);
            // 
            // tbCSV
            // 
            this.tbCSV.Location = new System.Drawing.Point(78, 45);
            this.tbCSV.Name = "tbCSV";
            this.tbCSV.Size = new System.Drawing.Size(354, 20);
            this.tbCSV.TabIndex = 16;
            // 
            // btSourceDotDotDot
            // 
            this.btSourceDotDotDot.Location = new System.Drawing.Point(438, 18);
            this.btSourceDotDotDot.Name = "btSourceDotDotDot";
            this.btSourceDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btSourceDotDotDot.TabIndex = 18;
            this.btSourceDotDotDot.Text = "...";
            this.btSourceDotDotDot.UseVisualStyleBackColor = true;
            this.btSourceDotDotDot.Click += new System.EventHandler(this.btSourceDotDotDot_Click);
            // 
            // tbSource
            // 
            this.tbSource.Location = new System.Drawing.Point(78, 18);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(354, 20);
            this.tbSource.TabIndex = 17;
            // 
            // lSource
            // 
            this.lSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.lSource.Location = new System.Drawing.Point(3, 18);
            this.lSource.Margin = new System.Windows.Forms.Padding(3);
            this.lSource.Name = "lSource";
            this.lSource.Size = new System.Drawing.Size(40, 21);
            this.lSource.TabIndex = 20;
            this.lSource.Text = "Quelle:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Dateityp:";
            // 
            // tbType
            // 
            this.tbType.Location = new System.Drawing.Point(78, 72);
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(29, 20);
            this.tbType.TabIndex = 23;
            // 
            // CatalogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.Name = "CatalogControl";
            this.Size = new System.Drawing.Size(465, 95);
            this.Load += new System.EventHandler(this.CatalogControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btCSVDotDotDot;
        private System.Windows.Forms.Button btSourceDotDotDot;
        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.TextBox tbCSV;
        private System.Windows.Forms.Label lSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbType;
    }
}
