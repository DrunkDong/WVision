namespace WVision
{
    partial class FrmSystemSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemSetting));
            this.uiipTextBox_Modbus = new Sunny.UI.UIIPTextBox();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.numericUpDown_ModbusPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.uiButton_SelectPath = new Sunny.UI.UIButton();
            this.uiRichTextBox_SavePath = new Sunny.UI.UIRichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.uiComboBox_SaveNgMode = new Sunny.UI.UIComboBox();
            this.uiComboBox_SaveOKMode = new Sunny.UI.UIComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.uiButton_Save = new Sunny.UI.UIButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDown_LowYield = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_LowDiskCapacity = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_AnomalyCount = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ModbusPort)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LowYield)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LowDiskCapacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AnomalyCount)).BeginInit();
            this.SuspendLayout();
            // 
            // uiipTextBox_Modbus
            // 
            this.uiipTextBox_Modbus.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiipTextBox_Modbus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiipTextBox_Modbus.Location = new System.Drawing.Point(94, 33);
            this.uiipTextBox_Modbus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiipTextBox_Modbus.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiipTextBox_Modbus.Name = "uiipTextBox_Modbus";
            this.uiipTextBox_Modbus.Padding = new System.Windows.Forms.Padding(1);
            this.uiipTextBox_Modbus.RectColor = System.Drawing.Color.Teal;
            this.uiipTextBox_Modbus.ShowText = false;
            this.uiipTextBox_Modbus.Size = new System.Drawing.Size(150, 29);
            this.uiipTextBox_Modbus.TabIndex = 0;
            this.uiipTextBox_Modbus.Text = "172.17.0.1";
            this.uiipTextBox_Modbus.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiipTextBox_Modbus.Value = ((System.Net.IPAddress)(resources.GetObject("uiipTextBox_Modbus.Value")));
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.numericUpDown_ModbusPort);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.uiipTextBox_Modbus);
            this.uiGroupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(4, 40);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.RectColor = System.Drawing.Color.Teal;
            this.uiGroupBox1.Size = new System.Drawing.Size(428, 174);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "IP通讯设定";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_ModbusPort
            // 
            this.numericUpDown_ModbusPort.Font = new System.Drawing.Font("宋体", 13F);
            this.numericUpDown_ModbusPort.Location = new System.Drawing.Point(307, 35);
            this.numericUpDown_ModbusPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_ModbusPort.Name = "numericUpDown_ModbusPort";
            this.numericUpDown_ModbusPort.Size = new System.Drawing.Size(107, 27);
            this.numericUpDown_ModbusPort.TabIndex = 3;
            this.numericUpDown_ModbusPort.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Modbus IP";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.uiButton_SelectPath);
            this.uiGroupBox2.Controls.Add(this.uiRichTextBox_SavePath);
            this.uiGroupBox2.Controls.Add(this.label10);
            this.uiGroupBox2.Controls.Add(this.uiComboBox_SaveNgMode);
            this.uiGroupBox2.Controls.Add(this.uiComboBox_SaveOKMode);
            this.uiGroupBox2.Controls.Add(this.label9);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox2.Location = new System.Drawing.Point(436, 40);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.RectColor = System.Drawing.Color.Teal;
            this.uiGroupBox2.Size = new System.Drawing.Size(345, 174);
            this.uiGroupBox2.TabIndex = 2;
            this.uiGroupBox2.Text = "图像存储设置";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiButton_SelectPath
            // 
            this.uiButton_SelectPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_SelectPath.FillColor = System.Drawing.Color.Teal;
            this.uiButton_SelectPath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_SelectPath.Location = new System.Drawing.Point(244, 120);
            this.uiButton_SelectPath.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_SelectPath.Name = "uiButton_SelectPath";
            this.uiButton_SelectPath.RectColor = System.Drawing.Color.Teal;
            this.uiButton_SelectPath.Size = new System.Drawing.Size(61, 35);
            this.uiButton_SelectPath.TabIndex = 8;
            this.uiButton_SelectPath.Text = "选择";
            this.uiButton_SelectPath.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_SelectPath.Click += new System.EventHandler(this.UiButton_SelectPath_Click);
            // 
            // uiRichTextBox_SavePath
            // 
            this.uiRichTextBox_SavePath.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox_SavePath.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiRichTextBox_SavePath.Location = new System.Drawing.Point(87, 101);
            this.uiRichTextBox_SavePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiRichTextBox_SavePath.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox_SavePath.Name = "uiRichTextBox_SavePath";
            this.uiRichTextBox_SavePath.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox_SavePath.RectColor = System.Drawing.Color.Teal;
            this.uiRichTextBox_SavePath.ScrollBarColor = System.Drawing.Color.Teal;
            this.uiRichTextBox_SavePath.ScrollBarStyleInherited = false;
            this.uiRichTextBox_SavePath.ShowText = false;
            this.uiRichTextBox_SavePath.Size = new System.Drawing.Size(150, 68);
            this.uiRichTextBox_SavePath.TabIndex = 7;
            this.uiRichTextBox_SavePath.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 16);
            this.label10.TabIndex = 6;
            this.label10.Text = "存图路径";
            // 
            // uiComboBox_SaveNgMode
            // 
            this.uiComboBox_SaveNgMode.DataSource = null;
            this.uiComboBox_SaveNgMode.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.uiComboBox_SaveNgMode.FillColor = System.Drawing.Color.White;
            this.uiComboBox_SaveNgMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboBox_SaveNgMode.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiComboBox_SaveNgMode.Items.AddRange(new object[] {
            "none",
            "jpeg",
            "png",
            "bmp"});
            this.uiComboBox_SaveNgMode.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiComboBox_SaveNgMode.Location = new System.Drawing.Point(87, 65);
            this.uiComboBox_SaveNgMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox_SaveNgMode.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox_SaveNgMode.Name = "uiComboBox_SaveNgMode";
            this.uiComboBox_SaveNgMode.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox_SaveNgMode.RectColor = System.Drawing.Color.Teal;
            this.uiComboBox_SaveNgMode.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox_SaveNgMode.TabIndex = 5;
            this.uiComboBox_SaveNgMode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiComboBox_SaveNgMode.Watermark = "";
            // 
            // uiComboBox_SaveOKMode
            // 
            this.uiComboBox_SaveOKMode.DataSource = null;
            this.uiComboBox_SaveOKMode.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.uiComboBox_SaveOKMode.FillColor = System.Drawing.Color.White;
            this.uiComboBox_SaveOKMode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboBox_SaveOKMode.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiComboBox_SaveOKMode.Items.AddRange(new object[] {
            "none",
            "jpeg",
            "png",
            "bmp"});
            this.uiComboBox_SaveOKMode.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiComboBox_SaveOKMode.Location = new System.Drawing.Point(87, 30);
            this.uiComboBox_SaveOKMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox_SaveOKMode.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox_SaveOKMode.Name = "uiComboBox_SaveOKMode";
            this.uiComboBox_SaveOKMode.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox_SaveOKMode.RectColor = System.Drawing.Color.Teal;
            this.uiComboBox_SaveOKMode.Size = new System.Drawing.Size(150, 29);
            this.uiComboBox_SaveOKMode.TabIndex = 4;
            this.uiComboBox_SaveOKMode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiComboBox_SaveOKMode.Watermark = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 16);
            this.label9.TabIndex = 3;
            this.label9.Text = "NG模式";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "OK模式";
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.label16);
            this.uiGroupBox3.Controls.Add(this.label15);
            this.uiGroupBox3.Controls.Add(this.label14);
            this.uiGroupBox3.Controls.Add(this.numericUpDown_AnomalyCount);
            this.uiGroupBox3.Controls.Add(this.numericUpDown_LowDiskCapacity);
            this.uiGroupBox3.Controls.Add(this.numericUpDown_LowYield);
            this.uiGroupBox3.Controls.Add(this.label13);
            this.uiGroupBox3.Controls.Add(this.label12);
            this.uiGroupBox3.Controls.Add(this.label11);
            this.uiGroupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox3.Location = new System.Drawing.Point(4, 215);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.RectColor = System.Drawing.Color.Teal;
            this.uiGroupBox3.Size = new System.Drawing.Size(428, 230);
            this.uiGroupBox3.TabIndex = 3;
            this.uiGroupBox3.Text = "报警设置";
            this.uiGroupBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiButton_Save
            // 
            this.uiButton_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton_Save.FillColor = System.Drawing.Color.Teal;
            this.uiButton_Save.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_Save.Location = new System.Drawing.Point(708, 398);
            this.uiButton_Save.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton_Save.Name = "uiButton_Save";
            this.uiButton_Save.RectColor = System.Drawing.Color.Teal;
            this.uiButton_Save.Size = new System.Drawing.Size(73, 47);
            this.uiButton_Save.TabIndex = 9;
            this.uiButton_Save.Text = "保存";
            this.uiButton_Save.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton_Save.Click += new System.EventHandler(this.UiButton_Save_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "良率过低报警";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "硬盘容量报警";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 16);
            this.label13.TabIndex = 12;
            this.label13.Text = "连续错误报警";
            // 
            // numericUpDown_LowYield
            // 
            this.numericUpDown_LowYield.Font = new System.Drawing.Font("宋体", 13F);
            this.numericUpDown_LowYield.Location = new System.Drawing.Point(120, 34);
            this.numericUpDown_LowYield.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_LowYield.Name = "numericUpDown_LowYield";
            this.numericUpDown_LowYield.Size = new System.Drawing.Size(107, 27);
            this.numericUpDown_LowYield.TabIndex = 13;
            // 
            // numericUpDown_LowDiskCapacity
            // 
            this.numericUpDown_LowDiskCapacity.Font = new System.Drawing.Font("宋体", 13F);
            this.numericUpDown_LowDiskCapacity.Location = new System.Drawing.Point(120, 66);
            this.numericUpDown_LowDiskCapacity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_LowDiskCapacity.Name = "numericUpDown_LowDiskCapacity";
            this.numericUpDown_LowDiskCapacity.Size = new System.Drawing.Size(107, 27);
            this.numericUpDown_LowDiskCapacity.TabIndex = 14;
            // 
            // numericUpDown_AnomalyCount
            // 
            this.numericUpDown_AnomalyCount.Font = new System.Drawing.Font("宋体", 13F);
            this.numericUpDown_AnomalyCount.Location = new System.Drawing.Point(120, 99);
            this.numericUpDown_AnomalyCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_AnomalyCount.Name = "numericUpDown_AnomalyCount";
            this.numericUpDown_AnomalyCount.Size = new System.Drawing.Size(107, 27);
            this.numericUpDown_AnomalyCount.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(233, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 16);
            this.label14.TabIndex = 16;
            this.label14.Text = "%";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(233, 69);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 16);
            this.label15.TabIndex = 17;
            this.label15.Text = "GB";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(233, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 16);
            this.label16.TabIndex = 18;
            this.label16.Text = "PCS";
            // 
            // FrmSystemSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(785, 450);
            this.Controls.Add(this.uiButton_Save);
            this.Controls.Add(this.uiGroupBox3);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSystemSetting";
            this.RectColor = System.Drawing.Color.Teal;
            this.Text = "设备参数设定界面";
            this.TitleColor = System.Drawing.Color.Teal;
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.Load += new System.EventHandler(this.FrmSystemSetting_Load);
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ModbusPort)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LowYield)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LowDiskCapacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AnomalyCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIIPTextBox uiipTextBox_Modbus;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_ModbusPort;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Sunny.UI.UIComboBox uiComboBox_SaveOKMode;
        private Sunny.UI.UIComboBox uiComboBox_SaveNgMode;
        private Sunny.UI.UIRichTextBox uiRichTextBox_SavePath;
        private System.Windows.Forms.Label label10;
        private Sunny.UI.UIButton uiButton_SelectPath;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UIButton uiButton_Save;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDown_AnomalyCount;
        private System.Windows.Forms.NumericUpDown numericUpDown_LowDiskCapacity;
        private System.Windows.Forms.NumericUpDown numericUpDown_LowYield;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}