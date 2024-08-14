
namespace WTools
{
    partial class UToolDecomposeRGB
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Mes = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_ToolName = new System.Windows.Forms.Label();
            this.tabControl_Check = new System.Windows.Forms.TabControl();
            this.tabPage_Setting = new System.Windows.Forms.TabPage();
            this.textBox_RefreshName = new System.Windows.Forms.TextBox();
            this.checkBox_ReName = new System.Windows.Forms.CheckBox();
            this.checkBox_ForceOK = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_RetrunValue1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_RB = new System.Windows.Forms.RadioButton();
            this.radioButton_SV = new System.Windows.Forms.RadioButton();
            this.radioButton_HV = new System.Windows.Forms.RadioButton();
            this.radioButton_HS = new System.Windows.Forms.RadioButton();
            this.radioButton_V = new System.Windows.Forms.RadioButton();
            this.radioButton_S = new System.Windows.Forms.RadioButton();
            this.radioButton_H = new System.Windows.Forms.RadioButton();
            this.radioButton_Blue = new System.Windows.Forms.RadioButton();
            this.radioButton_Green = new System.Windows.Forms.RadioButton();
            this.radioButton_Red = new System.Windows.Forms.RadioButton();
            this.radioButton_Gray = new System.Windows.Forms.RadioButton();
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.radioButton_RG = new System.Windows.Forms.RadioButton();
            this.radioButton_GB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_Mult = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Add = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tabControl_Check.SuspendLayout();
            this.tabPage_Setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Add)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Mes
            // 
            this.textBox_Mes.Location = new System.Drawing.Point(3, 700);
            this.textBox_Mes.Name = "textBox_Mes";
            this.textBox_Mes.Size = new System.Drawing.Size(494, 152);
            this.textBox_Mes.TabIndex = 61;
            this.textBox_Mes.Text = "";
            // 
            // tableLayoutPanel_Body
            // 
            this.tableLayoutPanel_Body.ColumnCount = 1;
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.Controls.Add(this.label_ToolName, 0, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.tabControl_Check, 0, 1);
            this.tableLayoutPanel_Body.Controls.Add(this.textBox_Mes, 0, 2);
            this.tableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Body.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Body.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel_Body.Name = "tableLayoutPanel_Body";
            this.tableLayoutPanel_Body.RowCount = 3;
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanel_Body.Size = new System.Drawing.Size(500, 855);
            this.tableLayoutPanel_Body.TabIndex = 6;
            // 
            // label_ToolName
            // 
            this.label_ToolName.AutoSize = true;
            this.label_ToolName.BackColor = System.Drawing.Color.NavajoWhite;
            this.label_ToolName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_ToolName.Font = new System.Drawing.Font("宋体", 18F);
            this.label_ToolName.Location = new System.Drawing.Point(3, 0);
            this.label_ToolName.Name = "label_ToolName";
            this.label_ToolName.Size = new System.Drawing.Size(494, 27);
            this.label_ToolName.TabIndex = 0;
            this.label_ToolName.Text = "ToolName";
            this.label_ToolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl_Check
            // 
            this.tabControl_Check.Controls.Add(this.tabPage_Setting);
            this.tabControl_Check.Location = new System.Drawing.Point(3, 31);
            this.tabControl_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl_Check.Multiline = true;
            this.tabControl_Check.Name = "tabControl_Check";
            this.tabControl_Check.SelectedIndex = 0;
            this.tabControl_Check.Size = new System.Drawing.Size(494, 662);
            this.tabControl_Check.TabIndex = 1;
            // 
            // tabPage_Setting
            // 
            this.tabPage_Setting.BackColor = System.Drawing.Color.Ivory;
            this.tabPage_Setting.Controls.Add(this.textBox_RefreshName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ReName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ForceOK);
            this.tabPage_Setting.Controls.Add(this.label8);
            this.tabPage_Setting.Controls.Add(this.numericUpDown_RetrunValue1);
            this.tabPage_Setting.Controls.Add(this.groupBox1);
            this.tabPage_Setting.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Size = new System.Drawing.Size(486, 630);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "设置";
            // 
            // textBox_RefreshName
            // 
            this.textBox_RefreshName.Enabled = false;
            this.textBox_RefreshName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBox_RefreshName.Location = new System.Drawing.Point(104, 40);
            this.textBox_RefreshName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_RefreshName.Name = "textBox_RefreshName";
            this.textBox_RefreshName.Size = new System.Drawing.Size(280, 25);
            this.textBox_RefreshName.TabIndex = 71;
            // 
            // checkBox_ReName
            // 
            this.checkBox_ReName.AutoSize = true;
            this.checkBox_ReName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ReName.Location = new System.Drawing.Point(14, 41);
            this.checkBox_ReName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox_ReName.Name = "checkBox_ReName";
            this.checkBox_ReName.Size = new System.Drawing.Size(70, 24);
            this.checkBox_ReName.TabIndex = 70;
            this.checkBox_ReName.Text = "重命名";
            this.checkBox_ReName.UseVisualStyleBackColor = true;
            this.checkBox_ReName.CheckedChanged += new System.EventHandler(this.checkBox_ReName_CheckedChanged);
            // 
            // checkBox_ForceOK
            // 
            this.checkBox_ForceOK.AutoSize = true;
            this.checkBox_ForceOK.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ForceOK.Location = new System.Drawing.Point(14, 12);
            this.checkBox_ForceOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox_ForceOK.Name = "checkBox_ForceOK";
            this.checkBox_ForceOK.Size = new System.Drawing.Size(98, 24);
            this.checkBox_ForceOK.TabIndex = 59;
            this.checkBox_ForceOK.Text = "屏蔽此功能";
            this.checkBox_ForceOK.UseVisualStyleBackColor = true;
            this.checkBox_ForceOK.CheckedChanged += new System.EventHandler(this.checkBox_ForceOK_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 564);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "NG返回值设定";
            // 
            // numericUpDown_RetrunValue1
            // 
            this.numericUpDown_RetrunValue1.Location = new System.Drawing.Point(7, 589);
            this.numericUpDown_RetrunValue1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDown_RetrunValue1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_RetrunValue1.Name = "numericUpDown_RetrunValue1";
            this.numericUpDown_RetrunValue1.Size = new System.Drawing.Size(94, 25);
            this.numericUpDown_RetrunValue1.TabIndex = 58;
            this.numericUpDown_RetrunValue1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_RetrunValue1.ValueChanged += new System.EventHandler(this.numericUpDown_RetrunValue1_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown_Add);
            this.groupBox1.Controls.Add(this.numericUpDown_Mult);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButton_GB);
            this.groupBox1.Controls.Add(this.radioButton_RG);
            this.groupBox1.Controls.Add(this.radioButton_RB);
            this.groupBox1.Controls.Add(this.radioButton_SV);
            this.groupBox1.Controls.Add(this.radioButton_HV);
            this.groupBox1.Controls.Add(this.radioButton_HS);
            this.groupBox1.Controls.Add(this.radioButton_V);
            this.groupBox1.Controls.Add(this.radioButton_S);
            this.groupBox1.Controls.Add(this.radioButton_H);
            this.groupBox1.Controls.Add(this.radioButton_Blue);
            this.groupBox1.Controls.Add(this.radioButton_Green);
            this.groupBox1.Controls.Add(this.radioButton_Red);
            this.groupBox1.Controls.Add(this.radioButton_Gray);
            this.groupBox1.Controls.Add(this.comboBox_Mode);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(6, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 429);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设定";
            // 
            // radioButton_RB
            // 
            this.radioButton_RB.AutoSize = true;
            this.radioButton_RB.Location = new System.Drawing.Point(396, 274);
            this.radioButton_RB.Name = "radioButton_RB";
            this.radioButton_RB.Size = new System.Drawing.Size(51, 24);
            this.radioButton_RB.TabIndex = 95;
            this.radioButton_RB.Text = "R-B";
            this.radioButton_RB.UseVisualStyleBackColor = true;
            this.radioButton_RB.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged_1);
            // 
            // radioButton_SV
            // 
            this.radioButton_SV.AutoSize = true;
            this.radioButton_SV.Location = new System.Drawing.Point(396, 394);
            this.radioButton_SV.Name = "radioButton_SV";
            this.radioButton_SV.Size = new System.Drawing.Size(50, 24);
            this.radioButton_SV.TabIndex = 94;
            this.radioButton_SV.Text = "S-V";
            this.radioButton_SV.UseVisualStyleBackColor = true;
            this.radioButton_SV.CheckedChanged += new System.EventHandler(this.radioButton_GB_CheckedChanged);
            // 
            // radioButton_HV
            // 
            this.radioButton_HV.AutoSize = true;
            this.radioButton_HV.Location = new System.Drawing.Point(396, 364);
            this.radioButton_HV.Name = "radioButton_HV";
            this.radioButton_HV.Size = new System.Drawing.Size(53, 24);
            this.radioButton_HV.TabIndex = 93;
            this.radioButton_HV.Text = "H-V";
            this.radioButton_HV.UseVisualStyleBackColor = true;
            this.radioButton_HV.CheckedChanged += new System.EventHandler(this.radioButton_RB_CheckedChanged);
            // 
            // radioButton_HS
            // 
            this.radioButton_HS.AutoSize = true;
            this.radioButton_HS.Location = new System.Drawing.Point(396, 334);
            this.radioButton_HS.Name = "radioButton_HS";
            this.radioButton_HS.Size = new System.Drawing.Size(52, 24);
            this.radioButton_HS.TabIndex = 92;
            this.radioButton_HS.Text = "H-S";
            this.radioButton_HS.UseVisualStyleBackColor = true;
            this.radioButton_HS.CheckedChanged += new System.EventHandler(this.radioButton_RG_CheckedChanged);
            // 
            // radioButton_V
            // 
            this.radioButton_V.AutoSize = true;
            this.radioButton_V.Location = new System.Drawing.Point(396, 214);
            this.radioButton_V.Name = "radioButton_V";
            this.radioButton_V.Size = new System.Drawing.Size(65, 24);
            this.radioButton_V.TabIndex = 91;
            this.radioButton_V.Text = "V/etc.";
            this.radioButton_V.UseVisualStyleBackColor = true;
            this.radioButton_V.CheckedChanged += new System.EventHandler(this.radioButton_V_CheckedChanged);
            // 
            // radioButton_S
            // 
            this.radioButton_S.AutoSize = true;
            this.radioButton_S.Location = new System.Drawing.Point(396, 184);
            this.radioButton_S.Name = "radioButton_S";
            this.radioButton_S.Size = new System.Drawing.Size(35, 24);
            this.radioButton_S.TabIndex = 90;
            this.radioButton_S.Text = "S";
            this.radioButton_S.UseVisualStyleBackColor = true;
            this.radioButton_S.CheckedChanged += new System.EventHandler(this.radioButton_S_CheckedChanged);
            // 
            // radioButton_H
            // 
            this.radioButton_H.AutoSize = true;
            this.radioButton_H.Location = new System.Drawing.Point(396, 154);
            this.radioButton_H.Name = "radioButton_H";
            this.radioButton_H.Size = new System.Drawing.Size(38, 24);
            this.radioButton_H.TabIndex = 89;
            this.radioButton_H.Text = "H";
            this.radioButton_H.UseVisualStyleBackColor = true;
            this.radioButton_H.CheckedChanged += new System.EventHandler(this.radioButton_H_CheckedChanged);
            // 
            // radioButton_Blue
            // 
            this.radioButton_Blue.AutoSize = true;
            this.radioButton_Blue.Location = new System.Drawing.Point(396, 124);
            this.radioButton_Blue.Name = "radioButton_Blue";
            this.radioButton_Blue.Size = new System.Drawing.Size(36, 24);
            this.radioButton_Blue.TabIndex = 88;
            this.radioButton_Blue.Text = "B";
            this.radioButton_Blue.UseVisualStyleBackColor = true;
            this.radioButton_Blue.CheckedChanged += new System.EventHandler(this.radioButton_Blue_CheckedChanged);
            // 
            // radioButton_Green
            // 
            this.radioButton_Green.AutoSize = true;
            this.radioButton_Green.Location = new System.Drawing.Point(396, 94);
            this.radioButton_Green.Name = "radioButton_Green";
            this.radioButton_Green.Size = new System.Drawing.Size(37, 24);
            this.radioButton_Green.TabIndex = 87;
            this.radioButton_Green.Text = "G";
            this.radioButton_Green.UseVisualStyleBackColor = true;
            this.radioButton_Green.CheckedChanged += new System.EventHandler(this.radioButton_Green_CheckedChanged);
            // 
            // radioButton_Red
            // 
            this.radioButton_Red.AutoSize = true;
            this.radioButton_Red.Location = new System.Drawing.Point(396, 64);
            this.radioButton_Red.Name = "radioButton_Red";
            this.radioButton_Red.Size = new System.Drawing.Size(36, 24);
            this.radioButton_Red.TabIndex = 86;
            this.radioButton_Red.Text = "R";
            this.radioButton_Red.UseVisualStyleBackColor = true;
            this.radioButton_Red.CheckedChanged += new System.EventHandler(this.radioButton_Red_CheckedChanged);
            // 
            // radioButton_Gray
            // 
            this.radioButton_Gray.AutoSize = true;
            this.radioButton_Gray.Checked = true;
            this.radioButton_Gray.Location = new System.Drawing.Point(396, 34);
            this.radioButton_Gray.Name = "radioButton_Gray";
            this.radioButton_Gray.Size = new System.Drawing.Size(55, 24);
            this.radioButton_Gray.TabIndex = 85;
            this.radioButton_Gray.TabStop = true;
            this.radioButton_Gray.Text = "黑白";
            this.radioButton_Gray.UseVisualStyleBackColor = true;
            this.radioButton_Gray.CheckedChanged += new System.EventHandler(this.radioButton_Gray_CheckedChanged);
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Items.AddRange(new object[] {
            "hsv",
            "hsi",
            "yiq",
            "yuv",
            "argyb",
            "ciexyz",
            "hls",
            "ihs",
            "cielab"});
            this.comboBox_Mode.Location = new System.Drawing.Point(112, 124);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(155, 27);
            this.comboBox_Mode.TabIndex = 84;
            this.comboBox_Mode.SelectedIndexChanged += new System.EventHandler(this.comboBox_Mode_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(37, 127);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 20);
            this.label15.TabIndex = 83;
            this.label15.Text = "分解模式：";
            // 
            // radioButton_RG
            // 
            this.radioButton_RG.AutoSize = true;
            this.radioButton_RG.Location = new System.Drawing.Point(396, 244);
            this.radioButton_RG.Name = "radioButton_RG";
            this.radioButton_RG.Size = new System.Drawing.Size(52, 24);
            this.radioButton_RG.TabIndex = 96;
            this.radioButton_RG.Text = "R-G";
            this.radioButton_RG.UseVisualStyleBackColor = true;
            this.radioButton_RG.CheckedChanged += new System.EventHandler(this.RadioButton_RG_CheckedChanged_1);
            // 
            // radioButton_GB
            // 
            this.radioButton_GB.AutoSize = true;
            this.radioButton_GB.Location = new System.Drawing.Point(396, 304);
            this.radioButton_GB.Name = "radioButton_GB";
            this.radioButton_GB.Size = new System.Drawing.Size(52, 24);
            this.radioButton_GB.TabIndex = 97;
            this.radioButton_GB.Text = "G-B";
            this.radioButton_GB.UseVisualStyleBackColor = true;
            this.radioButton_GB.CheckedChanged += new System.EventHandler(this.RadioButton_GB_CheckedChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "Add：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 99;
            this.label2.Text = "Mult：";
            // 
            // numericUpDown_Mult
            // 
            this.numericUpDown_Mult.Location = new System.Drawing.Point(112, 157);
            this.numericUpDown_Mult.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Mult.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.numericUpDown_Mult.Name = "numericUpDown_Mult";
            this.numericUpDown_Mult.Size = new System.Drawing.Size(98, 25);
            this.numericUpDown_Mult.TabIndex = 100;
            this.numericUpDown_Mult.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Mult.ValueChanged += new System.EventHandler(this.NumericUpDown_Mult_ValueChanged);
            // 
            // numericUpDown_Add
            // 
            this.numericUpDown_Add.Location = new System.Drawing.Point(112, 188);
            this.numericUpDown_Add.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.numericUpDown_Add.Minimum = new decimal(new int[] {
            512,
            0,
            0,
            -2147483648});
            this.numericUpDown_Add.Name = "numericUpDown_Add";
            this.numericUpDown_Add.Size = new System.Drawing.Size(98, 25);
            this.numericUpDown_Add.TabIndex = 101;
            this.numericUpDown_Add.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDown_Add.ValueChanged += new System.EventHandler(this.NumericUpDown_Add_ValueChanged);
            // 
            // UToolDecomposeRGB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UToolDecomposeRGB";
            this.Size = new System.Drawing.Size(500, 855);
            this.Load += new System.EventHandler(this.UToolDecomposeRGB_Load);
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tabControl_Check.ResumeLayout(false);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Mult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Add)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_ToolName;
        private System.Windows.Forms.TabControl tabControl_Check;
        private System.Windows.Forms.RichTextBox textBox_Mes;
        private System.Windows.Forms.TabPage tabPage_Setting;
        private System.Windows.Forms.TextBox textBox_RefreshName;
        private System.Windows.Forms.CheckBox checkBox_ReName;
        private System.Windows.Forms.CheckBox checkBox_ForceOK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetrunValue1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_RB;
        private System.Windows.Forms.RadioButton radioButton_SV;
        private System.Windows.Forms.RadioButton radioButton_HV;
        private System.Windows.Forms.RadioButton radioButton_HS;
        private System.Windows.Forms.RadioButton radioButton_V;
        private System.Windows.Forms.RadioButton radioButton_S;
        private System.Windows.Forms.RadioButton radioButton_H;
        private System.Windows.Forms.RadioButton radioButton_Blue;
        private System.Windows.Forms.RadioButton radioButton_Green;
        private System.Windows.Forms.RadioButton radioButton_Red;
        private System.Windows.Forms.RadioButton radioButton_Gray;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton radioButton_GB;
        private System.Windows.Forms.RadioButton radioButton_RG;
        private System.Windows.Forms.NumericUpDown numericUpDown_Add;
        private System.Windows.Forms.NumericUpDown numericUpDown_Mult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
