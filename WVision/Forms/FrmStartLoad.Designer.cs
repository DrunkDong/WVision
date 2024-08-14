namespace WVision
{
    partial class FrmStartLoad
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
            this.uProcressBar1 = new WControls.UProcressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // uProcressBar1
            // 
            this.uProcressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(218)))), ((int)(((byte)(219)))));
            this.uProcressBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uProcressBar1.Location = new System.Drawing.Point(2, 255);
            this.uProcressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.uProcressBar1.Name = "uProcressBar1";
            this.uProcressBar1.pBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(218)))), ((int)(((byte)(219)))));
            this.uProcressBar1.pForegroundColor = System.Drawing.Color.Green;
            this.uProcressBar1.Size = new System.Drawing.Size(402, 36);
            this.uProcressBar1.TabIndex = 5;
            this.uProcressBar1.Value = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WVision.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(404, 219);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("楷体", 12F);
            this.label1.Location = new System.Drawing.Point(0, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(404, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "0%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmStartLoad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(207)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(404, 295);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.uProcressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStartLoad";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmStartLoad";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private WControls.UProcressBar uProcressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}