
namespace WTools
{
    partial class UToolAngleLL
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
            this.tabPage_Setting = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_MaxAngle = new System.Windows.Forms.NumericUpDown();
            this.radioButton_AngleLV = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_AngleLH = new System.Windows.Forms.RadioButton();
            this.numericUpDown_MinAngle = new System.Windows.Forms.NumericUpDown();
            this.radioButton_AngleLL = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_LineSource = new System.Windows.Forms.Label();
            this.comboBox_LineSourceStep = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_Line2SourceStep = new System.Windows.Forms.ComboBox();
            this.textBox_RefreshName = new System.Windows.Forms.TextBox();
            this.checkBox_ReName = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_RetrunValue1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox_ForceOK = new System.Windows.Forms.CheckBox();
            this.textBox_Mes = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_ToolName = new System.Windows.Forms.Label();
            this.tabControl_Check = new System.Windows.Forms.TabControl();
            this.tabPage_Setting.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinAngle)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).BeginInit();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tabControl_Check.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage_Setting
            // 
            this.tabPage_Setting.BackColor = System.Drawing.Color.Ivory;
            this.tabPage_Setting.Controls.Add(this.groupBox3);
            this.tabPage_Setting.Controls.Add(this.groupBox2);
            this.tabPage_Setting.Controls.Add(this.textBox_RefreshName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ReName);
            this.tabPage_Setting.Controls.Add(this.label8);
            this.tabPage_Setting.Controls.Add(this.numericUpDown_RetrunValue1);
            this.tabPage_Setting.Controls.Add(this.checkBox_ForceOK);
            this.tabPage_Setting.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Size = new System.Drawing.Size(486, 630);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numericUpDown_MaxAngle);
            this.groupBox3.Controls.Add(this.radioButton_AngleLV);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.radioButton_AngleLH);
            this.groupBox3.Controls.Add(this.numericUpDown_MinAngle);
            this.groupBox3.Controls.Add(this.radioButton_AngleLL);
            this.groupBox3.Location = new System.Drawing.Point(7, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(473, 112);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "模式及判定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 20);
            this.label1.TabIndex = 68;
            this.label1.Text = "至";
            // 
            // numericUpDown_MaxAngle
            // 
            this.numericUpDown_MaxAngle.DecimalPlaces = 2;
            this.numericUpDown_MaxAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_MaxAngle.Location = new System.Drawing.Point(197, 52);
            this.numericUpDown_MaxAngle.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDown_MaxAngle.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numericUpDown_MaxAngle.Name = "numericUpDown_MaxAngle";
            this.numericUpDown_MaxAngle.Size = new System.Drawing.Size(89, 25);
            this.numericUpDown_MaxAngle.TabIndex = 39;
            this.numericUpDown_MaxAngle.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numericUpDown_MaxAngle.ValueChanged += new System.EventHandler(this.numericUpDown_MaxAngle_ValueChanged);
            // 
            // radioButton_AngleLV
            // 
            this.radioButton_AngleLV.AutoSize = true;
            this.radioButton_AngleLV.Location = new System.Drawing.Point(371, 48);
            this.radioButton_AngleLV.Name = "radioButton_AngleLV";
            this.radioButton_AngleLV.Size = new System.Drawing.Size(83, 24);
            this.radioButton_AngleLV.TabIndex = 66;
            this.radioButton_AngleLV.Text = "垂直夹角";
            this.radioButton_AngleLV.UseVisualStyleBackColor = true;
            this.radioButton_AngleLV.CheckedChanged += new System.EventHandler(this.radioButton_AngleLV_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 38;
            this.label2.Text = "夹角：";
            // 
            // radioButton_AngleLH
            // 
            this.radioButton_AngleLH.AutoSize = true;
            this.radioButton_AngleLH.Checked = true;
            this.radioButton_AngleLH.Location = new System.Drawing.Point(372, 20);
            this.radioButton_AngleLH.Name = "radioButton_AngleLH";
            this.radioButton_AngleLH.Size = new System.Drawing.Size(83, 24);
            this.radioButton_AngleLH.TabIndex = 40;
            this.radioButton_AngleLH.TabStop = true;
            this.radioButton_AngleLH.Text = "水平夹角";
            this.radioButton_AngleLH.UseVisualStyleBackColor = true;
            this.radioButton_AngleLH.CheckedChanged += new System.EventHandler(this.radioButton_AngleLH_CheckedChanged);
            // 
            // numericUpDown_MinAngle
            // 
            this.numericUpDown_MinAngle.DecimalPlaces = 2;
            this.numericUpDown_MinAngle.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_MinAngle.Location = new System.Drawing.Point(68, 52);
            this.numericUpDown_MinAngle.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDown_MinAngle.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numericUpDown_MinAngle.Name = "numericUpDown_MinAngle";
            this.numericUpDown_MinAngle.Size = new System.Drawing.Size(89, 25);
            this.numericUpDown_MinAngle.TabIndex = 0;
            this.numericUpDown_MinAngle.ValueChanged += new System.EventHandler(this.numericUpDown_MinAngle_ValueChanged);
            // 
            // radioButton_AngleLL
            // 
            this.radioButton_AngleLL.AutoSize = true;
            this.radioButton_AngleLL.Location = new System.Drawing.Point(371, 76);
            this.radioButton_AngleLL.Name = "radioButton_AngleLL";
            this.radioButton_AngleLL.Size = new System.Drawing.Size(97, 24);
            this.radioButton_AngleLL.TabIndex = 67;
            this.radioButton_AngleLL.Text = "直线与直线";
            this.radioButton_AngleLL.UseVisualStyleBackColor = true;
            this.radioButton_AngleLL.CheckedChanged += new System.EventHandler(this.radioButton_AngleLL_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_LineSource);
            this.groupBox2.Controls.Add(this.comboBox_LineSourceStep);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBox_Line2SourceStep);
            this.groupBox2.Location = new System.Drawing.Point(7, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 107);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输入";
            // 
            // label_LineSource
            // 
            this.label_LineSource.AutoSize = true;
            this.label_LineSource.Location = new System.Drawing.Point(15, 33);
            this.label_LineSource.Name = "label_LineSource";
            this.label_LineSource.Size = new System.Drawing.Size(115, 20);
            this.label_LineSource.TabIndex = 35;
            this.label_LineSource.Text = "直线1输入步骤：";
            // 
            // comboBox_LineSourceStep
            // 
            this.comboBox_LineSourceStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LineSourceStep.FormattingEnabled = true;
            this.comboBox_LineSourceStep.Location = new System.Drawing.Point(142, 30);
            this.comboBox_LineSourceStep.Name = "comboBox_LineSourceStep";
            this.comboBox_LineSourceStep.Size = new System.Drawing.Size(237, 27);
            this.comboBox_LineSourceStep.TabIndex = 36;
            this.comboBox_LineSourceStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_LineSourceStep_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 64;
            this.label3.Text = "直线2输入步骤：";
            // 
            // comboBox_Line2SourceStep
            // 
            this.comboBox_Line2SourceStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Line2SourceStep.FormattingEnabled = true;
            this.comboBox_Line2SourceStep.Location = new System.Drawing.Point(142, 64);
            this.comboBox_Line2SourceStep.Name = "comboBox_Line2SourceStep";
            this.comboBox_Line2SourceStep.Size = new System.Drawing.Size(237, 27);
            this.comboBox_Line2SourceStep.TabIndex = 65;
            this.comboBox_Line2SourceStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_Line2SourceStep_SelectedIndexChanged);
            // 
            // textBox_RefreshName
            // 
            this.textBox_RefreshName.Enabled = false;
            this.textBox_RefreshName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBox_RefreshName.Location = new System.Drawing.Point(99, 46);
            this.textBox_RefreshName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_RefreshName.Name = "textBox_RefreshName";
            this.textBox_RefreshName.Size = new System.Drawing.Size(280, 25);
            this.textBox_RefreshName.TabIndex = 63;
            // 
            // checkBox_ReName
            // 
            this.checkBox_ReName.AutoSize = true;
            this.checkBox_ReName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ReName.Location = new System.Drawing.Point(15, 46);
            this.checkBox_ReName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox_ReName.Name = "checkBox_ReName";
            this.checkBox_ReName.Size = new System.Drawing.Size(70, 24);
            this.checkBox_ReName.TabIndex = 62;
            this.checkBox_ReName.Text = "重命名";
            this.checkBox_ReName.UseVisualStyleBackColor = true;
            this.checkBox_ReName.CheckedChanged += new System.EventHandler(this.checkBox_ReName_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 572);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "NG返回值设定";
            // 
            // numericUpDown_RetrunValue1
            // 
            this.numericUpDown_RetrunValue1.Location = new System.Drawing.Point(6, 597);
            this.numericUpDown_RetrunValue1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDown_RetrunValue1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
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
            // checkBox_ForceOK
            // 
            this.checkBox_ForceOK.AutoSize = true;
            this.checkBox_ForceOK.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ForceOK.Location = new System.Drawing.Point(15, 14);
            this.checkBox_ForceOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox_ForceOK.Name = "checkBox_ForceOK";
            this.checkBox_ForceOK.Size = new System.Drawing.Size(98, 24);
            this.checkBox_ForceOK.TabIndex = 3;
            this.checkBox_ForceOK.Text = "屏蔽此功能";
            this.checkBox_ForceOK.UseVisualStyleBackColor = true;
            this.checkBox_ForceOK.CheckedChanged += new System.EventHandler(this.checkBox_ForceOK_CheckedChanged);
            // 
            // textBox_Mes
            // 
            this.textBox_Mes.Location = new System.Drawing.Point(3, 704);
            this.textBox_Mes.Name = "textBox_Mes";
            this.textBox_Mes.Size = new System.Drawing.Size(494, 148);
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
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 154F));
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
            this.label_ToolName.Size = new System.Drawing.Size(494, 31);
            this.label_ToolName.TabIndex = 0;
            this.label_ToolName.Text = "ToolName";
            this.label_ToolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl_Check
            // 
            this.tabControl_Check.Controls.Add(this.tabPage_Setting);
            this.tabControl_Check.Location = new System.Drawing.Point(3, 35);
            this.tabControl_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl_Check.Multiline = true;
            this.tabControl_Check.Name = "tabControl_Check";
            this.tabControl_Check.SelectedIndex = 0;
            this.tabControl_Check.Size = new System.Drawing.Size(494, 662);
            this.tabControl_Check.TabIndex = 1;
            // 
            // UToolAngleLL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UToolAngleLL";
            this.Size = new System.Drawing.Size(500, 855);
            this.Load += new System.EventHandler(this.UToolAngleLL_Load);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MinAngle)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).EndInit();
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tabControl_Check.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Setting;
        private System.Windows.Forms.CheckBox checkBox_ForceOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_ToolName;
        private System.Windows.Forms.TabControl tabControl_Check;
        private System.Windows.Forms.ComboBox comboBox_LineSourceStep;
        private System.Windows.Forms.Label label_LineSource;
        private System.Windows.Forms.RadioButton radioButton_AngleLH;
        private System.Windows.Forms.NumericUpDown numericUpDown_MinAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxAngle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetrunValue1;
        private System.Windows.Forms.RichTextBox textBox_Mes;
        private System.Windows.Forms.TextBox textBox_RefreshName;
        private System.Windows.Forms.CheckBox checkBox_ReName;
        private System.Windows.Forms.ComboBox comboBox_Line2SourceStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_AngleLL;
        private System.Windows.Forms.RadioButton radioButton_AngleLV;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
    }
}
