namespace kopator
{
    partial class CopyControl
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
            this.lSource = new System.Windows.Forms.Label();
            this.tbDestiny = new System.Windows.Forms.TextBox();
            this.tbSource = new System.Windows.Forms.TextBox();
            this.lDestiny = new System.Windows.Forms.Label();
            this.btDestinyDotDotDot = new System.Windows.Forms.Button();
            this.btSourceDotDotDot = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lSource
            // 
            this.lSource.Location = new System.Drawing.Point(4, 28);
            this.lSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lSource.Name = "lSource";
            this.lSource.Size = new System.Drawing.Size(60, 32);
            this.lSource.TabIndex = 20;
            this.lSource.Text = "Quelle:";
            // 
            // tbDestiny
            // 
            this.tbDestiny.Location = new System.Drawing.Point(116, 70);
            this.tbDestiny.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDestiny.Name = "tbDestiny";
            this.tbDestiny.Size = new System.Drawing.Size(529, 26);
            this.tbDestiny.TabIndex = 17;
            // 
            // tbSource
            // 
            this.tbSource.Location = new System.Drawing.Point(116, 28);
            this.tbSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(529, 26);
            this.tbSource.TabIndex = 16;
            // 
            // lDestiny
            // 
            this.lDestiny.Location = new System.Drawing.Point(4, 70);
            this.lDestiny.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lDestiny.Name = "lDestiny";
            this.lDestiny.Size = new System.Drawing.Size(40, 32);
            this.lDestiny.TabIndex = 21;
            this.lDestiny.Text = "Ziel:";
            // 
            // btDestinyDotDotDot
            // 
            this.btDestinyDotDotDot.Location = new System.Drawing.Point(653, 70);
            this.btDestinyDotDotDot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btDestinyDotDotDot.Name = "btDestinyDotDotDot";
            this.btDestinyDotDotDot.Size = new System.Drawing.Size(36, 32);
            this.btDestinyDotDotDot.TabIndex = 19;
            this.btDestinyDotDotDot.Text = "...";
            this.btDestinyDotDotDot.UseVisualStyleBackColor = true;
            this.btDestinyDotDotDot.Click += new System.EventHandler(this.btDestinyDotDotDot_Click);
            // 
            // btSourceDotDotDot
            // 
            this.btSourceDotDotDot.Location = new System.Drawing.Point(653, 28);
            this.btSourceDotDotDot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btSourceDotDotDot.Name = "btSourceDotDotDot";
            this.btSourceDotDotDot.Size = new System.Drawing.Size(36, 32);
            this.btSourceDotDotDot.TabIndex = 18;
            this.btSourceDotDotDot.Text = "...";
            this.btSourceDotDotDot.UseVisualStyleBackColor = true;
            this.btSourceDotDotDot.Click += new System.EventHandler(this.btSourceDotDotDot_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btDestinyDotDotDot, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btSourceDotDotDot, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lDestiny, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lSource, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbDestiny, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbSource, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 107);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // CopyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CopyControl";
            this.Size = new System.Drawing.Size(693, 107);
            this.Load += new System.EventHandler(this.CopyControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lSource;
        private System.Windows.Forms.TextBox tbDestiny;
        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.Label lDestiny;
        private System.Windows.Forms.Button btDestinyDotDotDot;
        private System.Windows.Forms.Button btSourceDotDotDot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
