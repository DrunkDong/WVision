﻿namespace WControls
{
    partial class UNumericUpDown
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_line = new System.Windows.Forms.Label();
            this.nud_value = new System.Windows.Forms.NumericUpDown();
            this.btn_sub = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_value)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_line
            // 
            this.lbl_line.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_line.BackColor = System.Drawing.Color.Gray;
            this.lbl_line.Location = new System.Drawing.Point(0, 25);
            this.lbl_line.Name = "lbl_line";
            this.lbl_line.Size = new System.Drawing.Size(180, 1);
            this.lbl_line.TabIndex = 5;
            // 
            // nud_value
            // 
            this.nud_value.BackColor = System.Drawing.Color.White;
            this.nud_value.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nud_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_value.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.nud_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nud_value.Location = new System.Drawing.Point(0, 0);
            this.nud_value.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nud_value.Name = "nud_value";
            this.nud_value.Size = new System.Drawing.Size(180, 25);
            this.nud_value.TabIndex = 8;
            this.nud_value.ValueChanged += new System.EventHandler(this.nud_value_ValueChanged);
            // 
            // btn_sub
            // 
            this.btn_sub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_sub.BackColor = System.Drawing.Color.White;
            this.btn_sub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_sub.FlatAppearance.BorderSize = 0;
            this.btn_sub.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btn_sub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_sub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sub.Image = global::WControls.Properties.Resources.graySub;
            this.btn_sub.Location = new System.Drawing.Point(160, 0);
            this.btn_sub.Margin = new System.Windows.Forms.Padding(14, 20, 14, 20);
            this.btn_sub.Name = "btn_sub";
            this.btn_sub.Size = new System.Drawing.Size(20, 25);
            this.btn_sub.TabIndex = 9;
            this.btn_sub.TabStop = false;
            this.btn_sub.UseVisualStyleBackColor = false;
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.BackColor = System.Drawing.Color.White;
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btn_add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Image = global::WControls.Properties.Resources.grayAdd;
            this.btn_add.Location = new System.Drawing.Point(137, 0);
            this.btn_add.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(20, 25);
            this.btn_add.TabIndex = 10;
            this.btn_add.TabStop = false;
            this.btn_add.UseVisualStyleBackColor = false;
            // 
            // UNumericUpDown
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.btn_sub);
            this.Controls.Add(this.lbl_line);
            this.Controls.Add(this.nud_value);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(300, 26);
            this.MinimumSize = new System.Drawing.Size(50, 26);
            this.Name = "UNumericUpDown";
            this.Size = new System.Drawing.Size(180, 26);
            ((System.ComponentModel.ISupportInitialize)(this.nud_value)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_line;
        private System.Windows.Forms.NumericUpDown nud_value;
        private System.Windows.Forms.Button btn_sub;
        private System.Windows.Forms.Button btn_add;
    }
}
