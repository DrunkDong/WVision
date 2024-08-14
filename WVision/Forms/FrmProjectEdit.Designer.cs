namespace WVision
{
    partial class FrmProjectEdit
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
            this.uiTextBox_ProjectName = new Sunny.UI.UITextBox();
            this.uiSymbolButton_Cancel = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton_OK = new Sunny.UI.UISymbolButton();
            this.uiIntegerUpDown_CamNums = new Sunny.UI.UIIntegerUpDown();
            this.uiTextBox_CreateTime = new Sunny.UI.UITextBox();
            this.uiRichTextBox_ProjectDescribe = new Sunny.UI.UIRichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.uiIntegerUpDown_Row = new Sunny.UI.UIIntegerUpDown();
            this.uiIntegerUpDown_Column = new Sunny.UI.UIIntegerUpDown();
            this.SuspendLayout();
            // 
            // uiTextBox_ProjectName
            // 
            this.uiTextBox_ProjectName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_ProjectName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.uiTextBox_ProjectName.Location = new System.Drawing.Point(210, 48);
            this.uiTextBox_ProjectName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox_ProjectName.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox_ProjectName.Name = "uiTextBox_ProjectName";
            this.uiTextBox_ProjectName.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_ProjectName.RectColor = System.Drawing.Color.Teal;
            this.uiTextBox_ProjectName.ShowText = false;
            this.uiTextBox_ProjectName.Size = new System.Drawing.Size(266, 40);
            this.uiTextBox_ProjectName.TabIndex = 10;
            this.uiTextBox_ProjectName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox_ProjectName.Watermark = "";
            // 
            // uiSymbolButton_Cancel
            // 
            this.uiSymbolButton_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton_Cancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiSymbolButton_Cancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiSymbolButton_Cancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.uiSymbolButton_Cancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiSymbolButton_Cancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiSymbolButton_Cancel.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.uiSymbolButton_Cancel.Location = new System.Drawing.Point(253, 423);
            this.uiSymbolButton_Cancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton_Cancel.Name = "uiSymbolButton_Cancel";
            this.uiSymbolButton_Cancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.uiSymbolButton_Cancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.uiSymbolButton_Cancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiSymbolButton_Cancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.uiSymbolButton_Cancel.Size = new System.Drawing.Size(124, 44);
            this.uiSymbolButton_Cancel.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton_Cancel.StyleCustomMode = true;
            this.uiSymbolButton_Cancel.Symbol = 61453;
            this.uiSymbolButton_Cancel.TabIndex = 114;
            this.uiSymbolButton_Cancel.Text = "取消";
            this.uiSymbolButton_Cancel.TipsFont = new System.Drawing.Font("微软雅黑", 14F);
            this.uiSymbolButton_Cancel.Click += new System.EventHandler(this.UiSymbolButton_Cancel_Click);
            // 
            // uiSymbolButton_OK
            // 
            this.uiSymbolButton_OK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton_OK.FillColor = System.Drawing.Color.Teal;
            this.uiSymbolButton_OK.FillColor2 = System.Drawing.Color.Teal;
            this.uiSymbolButton_OK.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.uiSymbolButton_OK.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.uiSymbolButton_OK.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.uiSymbolButton_OK.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.uiSymbolButton_OK.Location = new System.Drawing.Point(89, 423);
            this.uiSymbolButton_OK.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton_OK.Name = "uiSymbolButton_OK";
            this.uiSymbolButton_OK.RectColor = System.Drawing.Color.Teal;
            this.uiSymbolButton_OK.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.uiSymbolButton_OK.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.uiSymbolButton_OK.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.uiSymbolButton_OK.Size = new System.Drawing.Size(124, 44);
            this.uiSymbolButton_OK.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton_OK.StyleCustomMode = true;
            this.uiSymbolButton_OK.TabIndex = 113;
            this.uiSymbolButton_OK.Text = "确定";
            this.uiSymbolButton_OK.TipsFont = new System.Drawing.Font("微软雅黑", 14F);
            this.uiSymbolButton_OK.Click += new System.EventHandler(this.UiSymbolButton_OK_Click);
            // 
            // uiIntegerUpDown_CamNums
            // 
            this.uiIntegerUpDown_CamNums.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiIntegerUpDown_CamNums.Location = new System.Drawing.Point(210, 147);
            this.uiIntegerUpDown_CamNums.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiIntegerUpDown_CamNums.Maximum = 1;
            this.uiIntegerUpDown_CamNums.Minimum = 1;
            this.uiIntegerUpDown_CamNums.MinimumSize = new System.Drawing.Size(100, 0);
            this.uiIntegerUpDown_CamNums.Name = "uiIntegerUpDown_CamNums";
            this.uiIntegerUpDown_CamNums.RectColor = System.Drawing.Color.Teal;
            this.uiIntegerUpDown_CamNums.ShowText = false;
            this.uiIntegerUpDown_CamNums.Size = new System.Drawing.Size(266, 40);
            this.uiIntegerUpDown_CamNums.TabIndex = 115;
            this.uiIntegerUpDown_CamNums.Text = "uiIntegerUpDown1";
            this.uiIntegerUpDown_CamNums.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiIntegerUpDown_CamNums.Value = 1;
            // 
            // uiTextBox_CreateTime
            // 
            this.uiTextBox_CreateTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox_CreateTime.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.uiTextBox_CreateTime.Location = new System.Drawing.Point(210, 97);
            this.uiTextBox_CreateTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox_CreateTime.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox_CreateTime.Name = "uiTextBox_CreateTime";
            this.uiTextBox_CreateTime.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox_CreateTime.ReadOnly = true;
            this.uiTextBox_CreateTime.RectColor = System.Drawing.Color.Teal;
            this.uiTextBox_CreateTime.ShowText = false;
            this.uiTextBox_CreateTime.Size = new System.Drawing.Size(266, 40);
            this.uiTextBox_CreateTime.TabIndex = 118;
            this.uiTextBox_CreateTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox_CreateTime.Watermark = "";
            // 
            // uiRichTextBox_ProjectDescribe
            // 
            this.uiRichTextBox_ProjectDescribe.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox_ProjectDescribe.Font = new System.Drawing.Font("宋体", 12F);
            this.uiRichTextBox_ProjectDescribe.Location = new System.Drawing.Point(210, 298);
            this.uiRichTextBox_ProjectDescribe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox_ProjectDescribe.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox_ProjectDescribe.Name = "uiRichTextBox_ProjectDescribe";
            this.uiRichTextBox_ProjectDescribe.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox_ProjectDescribe.RectColor = System.Drawing.Color.Teal;
            this.uiRichTextBox_ProjectDescribe.ScrollBarColor = System.Drawing.Color.Teal;
            this.uiRichTextBox_ProjectDescribe.ScrollBarStyleInherited = false;
            this.uiRichTextBox_ProjectDescribe.ShowText = false;
            this.uiRichTextBox_ProjectDescribe.Size = new System.Drawing.Size(266, 115);
            this.uiRichTextBox_ProjectDescribe.TabIndex = 120;
            this.uiRichTextBox_ProjectDescribe.Text = "No Description";
            this.uiRichTextBox_ProjectDescribe.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label4.Location = new System.Drawing.Point(10, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 31);
            this.label4.TabIndex = 124;
            this.label4.Text = "项目描述：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(10, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 31);
            this.label3.TabIndex = 123;
            this.label3.Text = "相机数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(10, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 122;
            this.label2.Text = "创建时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(10, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 121;
            this.label1.Text = "项目名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label5.Location = new System.Drawing.Point(10, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 31);
            this.label5.TabIndex = 125;
            this.label5.Text = "行数量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label6.Location = new System.Drawing.Point(10, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 31);
            this.label6.TabIndex = 126;
            this.label6.Text = "列数量：";
            // 
            // uiIntegerUpDown_Row
            // 
            this.uiIntegerUpDown_Row.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiIntegerUpDown_Row.Location = new System.Drawing.Point(210, 197);
            this.uiIntegerUpDown_Row.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiIntegerUpDown_Row.Maximum = 100;
            this.uiIntegerUpDown_Row.Minimum = 1;
            this.uiIntegerUpDown_Row.MinimumSize = new System.Drawing.Size(100, 0);
            this.uiIntegerUpDown_Row.Name = "uiIntegerUpDown_Row";
            this.uiIntegerUpDown_Row.RectColor = System.Drawing.Color.Teal;
            this.uiIntegerUpDown_Row.ShowText = false;
            this.uiIntegerUpDown_Row.Size = new System.Drawing.Size(266, 40);
            this.uiIntegerUpDown_Row.TabIndex = 127;
            this.uiIntegerUpDown_Row.Text = "uiIntegerUpDown1";
            this.uiIntegerUpDown_Row.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiIntegerUpDown_Row.Value = 1;
            // 
            // uiIntegerUpDown_Column
            // 
            this.uiIntegerUpDown_Column.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiIntegerUpDown_Column.Location = new System.Drawing.Point(210, 247);
            this.uiIntegerUpDown_Column.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiIntegerUpDown_Column.Maximum = 100;
            this.uiIntegerUpDown_Column.Minimum = 1;
            this.uiIntegerUpDown_Column.MinimumSize = new System.Drawing.Size(100, 0);
            this.uiIntegerUpDown_Column.Name = "uiIntegerUpDown_Column";
            this.uiIntegerUpDown_Column.RectColor = System.Drawing.Color.Teal;
            this.uiIntegerUpDown_Column.ShowText = false;
            this.uiIntegerUpDown_Column.Size = new System.Drawing.Size(266, 40);
            this.uiIntegerUpDown_Column.TabIndex = 128;
            this.uiIntegerUpDown_Column.Text = "uiIntegerUpDown1";
            this.uiIntegerUpDown_Column.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiIntegerUpDown_Column.Value = 1;
            // 
            // FrmProjectEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(483, 486);
            this.ControlBox = false;
            this.Controls.Add(this.uiIntegerUpDown_Column);
            this.Controls.Add(this.uiIntegerUpDown_Row);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uiRichTextBox_ProjectDescribe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiTextBox_CreateTime);
            this.Controls.Add(this.uiIntegerUpDown_CamNums);
            this.Controls.Add(this.uiSymbolButton_Cancel);
            this.Controls.Add(this.uiSymbolButton_OK);
            this.Controls.Add(this.uiTextBox_ProjectName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProjectEdit";
            this.RectColor = System.Drawing.Color.Teal;
            this.ShowInTaskbar = false;
            this.Text = "项目新增";
            this.TextAlignment = System.Drawing.StringAlignment.Center;
            this.TitleColor = System.Drawing.Color.Teal;
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.Load += new System.EventHandler(this.FrmProjectEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Sunny.UI.UITextBox uiTextBox_ProjectName;
        private Sunny.UI.UISymbolButton uiSymbolButton_Cancel;
        private Sunny.UI.UISymbolButton uiSymbolButton_OK;
        private Sunny.UI.UIIntegerUpDown uiIntegerUpDown_CamNums;
        private Sunny.UI.UITextBox uiTextBox_CreateTime;
        private Sunny.UI.UIRichTextBox uiRichTextBox_ProjectDescribe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Sunny.UI.UIIntegerUpDown uiIntegerUpDown_Row;
        private Sunny.UI.UIIntegerUpDown uiIntegerUpDown_Column;
    }
}