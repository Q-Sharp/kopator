namespace kopator
{
    partial class CollectControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbIgnore = new System.Windows.Forms.TextBox();
            this.lblCollect = new System.Windows.Forms.Label();
            this.tbCollect = new System.Windows.Forms.TextBox();
            this.btCollectDotDotDot = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Ignorieren:";
            // 
            // tbIgnore
            // 
            this.tbIgnore.Location = new System.Drawing.Point(78, 45);
            this.tbIgnore.Name = "tbIgnore";
            this.tbIgnore.Size = new System.Drawing.Size(354, 20);
            this.tbIgnore.TabIndex = 17;
            // 
            // lblCollect
            // 
            this.lblCollect.AutoSize = true;
            this.lblCollect.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCollect.Location = new System.Drawing.Point(3, 18);
            this.lblCollect.Margin = new System.Windows.Forms.Padding(3);
            this.lblCollect.Name = "lblCollect";
            this.lblCollect.Size = new System.Drawing.Size(32, 21);
            this.lblCollect.TabIndex = 16;
            this.lblCollect.Text = "Pfad:";
            // 
            // tbCollect
            // 
            this.tbCollect.Location = new System.Drawing.Point(78, 18);
            this.tbCollect.Name = "tbCollect";
            this.tbCollect.Size = new System.Drawing.Size(354, 20);
            this.tbCollect.TabIndex = 14;
            // 
            // btCollectDotDotDot
            // 
            this.btCollectDotDotDot.Location = new System.Drawing.Point(438, 18);
            this.btCollectDotDotDot.Name = "btCollectDotDotDot";
            this.btCollectDotDotDot.Size = new System.Drawing.Size(24, 21);
            this.btCollectDotDotDot.TabIndex = 15;
            this.btCollectDotDotDot.Text = "...";
            this.btCollectDotDotDot.UseVisualStyleBackColor = true;
            this.btCollectDotDotDot.Click += new System.EventHandler(this.btCollectDotDotDot_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCollect, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbIgnore, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbCollect, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btCollectDotDotDot, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(465, 68);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // CollectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CollectControl";
            this.Size = new System.Drawing.Size(465, 68);
            this.Load += new System.EventHandler(this.CollectControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIgnore;
        private System.Windows.Forms.Label lblCollect;
        private System.Windows.Forms.TextBox tbCollect;
        private System.Windows.Forms.Button btCollectDotDotDot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
