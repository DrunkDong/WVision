
namespace WTools
{
    partial class UToolFindCircle
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_LineSourceStep = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox_ImageStep = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_PositioningStep = new System.Windows.Forms.ComboBox();
            this.button_DrawRoi = new System.Windows.Forms.Button();
            this.checkBox_ShowRegion = new System.Windows.Forms.CheckBox();
            this.checkBox_DrawLine = new System.Windows.Forms.CheckBox();
            this.textBox_RefreshName = new System.Windows.Forms.TextBox();
            this.checkBox_ReName = new System.Windows.Forms.CheckBox();
            this.checkBox_ForceOK = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_RetrunValue1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox_FindLine = new System.Windows.Forms.GroupBox();
            this.label_CheckWidth = new System.Windows.Forms.Label();
            this.label_CheckHeight = new System.Windows.Forms.Label();
            this.textBox_Threshold = new System.Windows.Forms.TextBox();
            this.textBox_CheckNum = new System.Windows.Forms.TextBox();
            this.textBox_CheckWidth = new System.Windows.Forms.TextBox();
            this.textBox_CheckHeight = new System.Windows.Forms.TextBox();
            this.groupBox_Select = new System.Windows.Forms.GroupBox();
            this.radioButton_Max = new System.Windows.Forms.RadioButton();
            this.radioButton_Last = new System.Windows.Forms.RadioButton();
            this.radioButton_First = new System.Windows.Forms.RadioButton();
            this.groupBox_Transition = new System.Windows.Forms.GroupBox();
            this.radioButton_All = new System.Windows.Forms.RadioButton();
            this.radioButton_Negative = new System.Windows.Forms.RadioButton();
            this.radioButton_Positive = new System.Windows.Forms.RadioButton();
            this.label_Threshold = new System.Windows.Forms.Label();
            this.trackBar_CheckThreshold = new System.Windows.Forms.TrackBar();
            this.label_CheckNum = new System.Windows.Forms.Label();
            this.trackBar_CheckNum = new System.Windows.Forms.TrackBar();
            this.trackBar_CheckHeight = new System.Windows.Forms.TrackBar();
            this.trackBar_CheckWidth = new System.Windows.Forms.TrackBar();
            this.textBox_Mes = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_ToolName = new System.Windows.Forms.Label();
            this.tabControl_Check = new System.Windows.Forms.TabControl();
            this.tabPage_Setting.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).BeginInit();
            this.groupBox_FindLine.SuspendLayout();
            this.groupBox_Select.SuspendLayout();
            this.groupBox_Transition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckWidth)).BeginInit();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tabControl_Check.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage_Setting
            // 
            this.tabPage_Setting.BackColor = System.Drawing.Color.Ivory;
            this.tabPage_Setting.Controls.Add(this.groupBox5);
            this.tabPage_Setting.Controls.Add(this.textBox_RefreshName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ReName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ForceOK);
            this.tabPage_Setting.Controls.Add(this.label8);
            this.tabPage_Setting.Controls.Add(this.numericUpDown_RetrunValue1);
            this.tabPage_Setting.Controls.Add(this.groupBox_FindLine);
            this.tabPage_Setting.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Size = new System.Drawing.Size(486, 631);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "设置";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.comboBox_LineSourceStep);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.comboBox_ImageStep);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.comboBox_PositioningStep);
            this.groupBox5.Controls.Add(this.button_DrawRoi);
            this.groupBox5.Controls.Add(this.checkBox_ShowRegion);
            this.groupBox5.Controls.Add(this.checkBox_DrawLine);
            this.groupBox5.Location = new System.Drawing.Point(3, 68);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(477, 139);
            this.groupBox5.TabIndex = 79;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "输入输出";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 84;
            this.label1.Text = "圆输入步骤：";
            // 
            // comboBox_LineSourceStep
            // 
            this.comboBox_LineSourceStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LineSourceStep.FormattingEnabled = true;
            this.comboBox_LineSourceStep.Location = new System.Drawing.Point(123, 98);
            this.comboBox_LineSourceStep.Name = "comboBox_LineSourceStep";
            this.comboBox_LineSourceStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_LineSourceStep.TabIndex = 85;
            this.comboBox_LineSourceStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_LineSourceStep_SelectedIndexChanged_1);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(4, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 20);
            this.label21.TabIndex = 82;
            this.label21.Text = "图像输入步骤：";
            // 
            // comboBox_ImageStep
            // 
            this.comboBox_ImageStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ImageStep.FormattingEnabled = true;
            this.comboBox_ImageStep.Location = new System.Drawing.Point(123, 24);
            this.comboBox_ImageStep.Name = "comboBox_ImageStep";
            this.comboBox_ImageStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_ImageStep.TabIndex = 83;
            this.comboBox_ImageStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_ImageStep_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 20);
            this.label6.TabIndex = 70;
            this.label6.Text = "定位输入步骤：";
            // 
            // comboBox_PositioningStep
            // 
            this.comboBox_PositioningStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PositioningStep.FormattingEnabled = true;
            this.comboBox_PositioningStep.Location = new System.Drawing.Point(123, 61);
            this.comboBox_PositioningStep.Name = "comboBox_PositioningStep";
            this.comboBox_PositioningStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_PositioningStep.TabIndex = 76;
            this.comboBox_PositioningStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_ShapeModel_SelectedIndexChanged);
            // 
            // button_DrawRoi
            // 
            this.button_DrawRoi.Location = new System.Drawing.Point(372, 86);
            this.button_DrawRoi.Name = "button_DrawRoi";
            this.button_DrawRoi.Size = new System.Drawing.Size(76, 33);
            this.button_DrawRoi.TabIndex = 38;
            this.button_DrawRoi.Text = "绘制";
            this.button_DrawRoi.UseVisualStyleBackColor = true;
            this.button_DrawRoi.Click += new System.EventHandler(this.button_DrawRoi_Click);
            // 
            // checkBox_ShowRegion
            // 
            this.checkBox_ShowRegion.AutoSize = true;
            this.checkBox_ShowRegion.Location = new System.Drawing.Point(372, 29);
            this.checkBox_ShowRegion.Name = "checkBox_ShowRegion";
            this.checkBox_ShowRegion.Size = new System.Drawing.Size(84, 24);
            this.checkBox_ShowRegion.TabIndex = 42;
            this.checkBox_ShowRegion.Text = "显示搜索";
            this.checkBox_ShowRegion.UseVisualStyleBackColor = true;
            this.checkBox_ShowRegion.CheckedChanged += new System.EventHandler(this.checkBox_ShowRegion_CheckedChanged);
            // 
            // checkBox_DrawLine
            // 
            this.checkBox_DrawLine.AutoSize = true;
            this.checkBox_DrawLine.Checked = true;
            this.checkBox_DrawLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_DrawLine.Location = new System.Drawing.Point(372, 58);
            this.checkBox_DrawLine.Name = "checkBox_DrawLine";
            this.checkBox_DrawLine.Size = new System.Drawing.Size(84, 24);
            this.checkBox_DrawLine.TabIndex = 37;
            this.checkBox_DrawLine.Text = "绘制目标";
            this.checkBox_DrawLine.UseVisualStyleBackColor = true;
            this.checkBox_DrawLine.CheckedChanged += new System.EventHandler(this.checkBox_ShowRegion_CheckedChanged);
            // 
            // textBox_RefreshName
            // 
            this.textBox_RefreshName.Enabled = false;
            this.textBox_RefreshName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBox_RefreshName.Location = new System.Drawing.Point(113, 36);
            this.textBox_RefreshName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_RefreshName.Name = "textBox_RefreshName";
            this.textBox_RefreshName.Size = new System.Drawing.Size(280, 25);
            this.textBox_RefreshName.TabIndex = 69;
            // 
            // checkBox_ReName
            // 
            this.checkBox_ReName.AutoSize = true;
            this.checkBox_ReName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ReName.Location = new System.Drawing.Point(9, 37);
            this.checkBox_ReName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox_ReName.Name = "checkBox_ReName";
            this.checkBox_ReName.Size = new System.Drawing.Size(70, 24);
            this.checkBox_ReName.TabIndex = 68;
            this.checkBox_ReName.Text = "重命名";
            this.checkBox_ReName.UseVisualStyleBackColor = true;
            this.checkBox_ReName.CheckedChanged += new System.EventHandler(this.checkBox_ReName_CheckedChanged);
            // 
            // checkBox_ForceOK
            // 
            this.checkBox_ForceOK.AutoSize = true;
            this.checkBox_ForceOK.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ForceOK.Location = new System.Drawing.Point(9, 9);
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
            this.label8.Location = new System.Drawing.Point(5, 577);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "NG返回值设定";
            // 
            // numericUpDown_RetrunValue1
            // 
            this.numericUpDown_RetrunValue1.Location = new System.Drawing.Point(6, 602);
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
            // groupBox_FindLine
            // 
            this.groupBox_FindLine.Controls.Add(this.label_CheckWidth);
            this.groupBox_FindLine.Controls.Add(this.label_CheckHeight);
            this.groupBox_FindLine.Controls.Add(this.textBox_Threshold);
            this.groupBox_FindLine.Controls.Add(this.textBox_CheckNum);
            this.groupBox_FindLine.Controls.Add(this.textBox_CheckWidth);
            this.groupBox_FindLine.Controls.Add(this.textBox_CheckHeight);
            this.groupBox_FindLine.Controls.Add(this.groupBox_Select);
            this.groupBox_FindLine.Controls.Add(this.groupBox_Transition);
            this.groupBox_FindLine.Controls.Add(this.label_Threshold);
            this.groupBox_FindLine.Controls.Add(this.trackBar_CheckThreshold);
            this.groupBox_FindLine.Controls.Add(this.label_CheckNum);
            this.groupBox_FindLine.Controls.Add(this.trackBar_CheckNum);
            this.groupBox_FindLine.Controls.Add(this.trackBar_CheckHeight);
            this.groupBox_FindLine.Controls.Add(this.trackBar_CheckWidth);
            this.groupBox_FindLine.Location = new System.Drawing.Point(3, 213);
            this.groupBox_FindLine.Name = "groupBox_FindLine";
            this.groupBox_FindLine.Size = new System.Drawing.Size(477, 350);
            this.groupBox_FindLine.TabIndex = 34;
            this.groupBox_FindLine.TabStop = false;
            this.groupBox_FindLine.Text = "查找圆";
            // 
            // label_CheckWidth
            // 
            this.label_CheckWidth.AutoSize = true;
            this.label_CheckWidth.Location = new System.Drawing.Point(22, 71);
            this.label_CheckWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_CheckWidth.Name = "label_CheckWidth";
            this.label_CheckWidth.Size = new System.Drawing.Size(51, 20);
            this.label_CheckWidth.TabIndex = 81;
            this.label_CheckWidth.Text = "宽度：";
            // 
            // label_CheckHeight
            // 
            this.label_CheckHeight.AutoSize = true;
            this.label_CheckHeight.Location = new System.Drawing.Point(22, 21);
            this.label_CheckHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_CheckHeight.Name = "label_CheckHeight";
            this.label_CheckHeight.Size = new System.Drawing.Size(51, 20);
            this.label_CheckHeight.TabIndex = 79;
            this.label_CheckHeight.Text = "高度：";
            // 
            // textBox_Threshold
            // 
            this.textBox_Threshold.Location = new System.Drawing.Point(396, 196);
            this.textBox_Threshold.Name = "textBox_Threshold";
            this.textBox_Threshold.ReadOnly = true;
            this.textBox_Threshold.Size = new System.Drawing.Size(60, 25);
            this.textBox_Threshold.TabIndex = 78;
            this.textBox_Threshold.Text = "30";
            // 
            // textBox_CheckNum
            // 
            this.textBox_CheckNum.Location = new System.Drawing.Point(396, 145);
            this.textBox_CheckNum.Name = "textBox_CheckNum";
            this.textBox_CheckNum.ReadOnly = true;
            this.textBox_CheckNum.Size = new System.Drawing.Size(60, 25);
            this.textBox_CheckNum.TabIndex = 77;
            this.textBox_CheckNum.Text = "20";
            // 
            // textBox_CheckWidth
            // 
            this.textBox_CheckWidth.Location = new System.Drawing.Point(396, 93);
            this.textBox_CheckWidth.Name = "textBox_CheckWidth";
            this.textBox_CheckWidth.ReadOnly = true;
            this.textBox_CheckWidth.Size = new System.Drawing.Size(60, 25);
            this.textBox_CheckWidth.TabIndex = 76;
            this.textBox_CheckWidth.Text = "10";
            // 
            // textBox_CheckHeight
            // 
            this.textBox_CheckHeight.Location = new System.Drawing.Point(396, 44);
            this.textBox_CheckHeight.Name = "textBox_CheckHeight";
            this.textBox_CheckHeight.ReadOnly = true;
            this.textBox_CheckHeight.Size = new System.Drawing.Size(60, 25);
            this.textBox_CheckHeight.TabIndex = 75;
            this.textBox_CheckHeight.Text = "120";
            // 
            // groupBox_Select
            // 
            this.groupBox_Select.Controls.Add(this.radioButton_Max);
            this.groupBox_Select.Controls.Add(this.radioButton_Last);
            this.groupBox_Select.Controls.Add(this.radioButton_First);
            this.groupBox_Select.Location = new System.Drawing.Point(6, 295);
            this.groupBox_Select.Name = "groupBox_Select";
            this.groupBox_Select.Size = new System.Drawing.Size(450, 48);
            this.groupBox_Select.TabIndex = 74;
            this.groupBox_Select.TabStop = false;
            this.groupBox_Select.Text = "选择项";
            // 
            // radioButton_Max
            // 
            this.radioButton_Max.AutoSize = true;
            this.radioButton_Max.Checked = true;
            this.radioButton_Max.Location = new System.Drawing.Point(327, 15);
            this.radioButton_Max.Name = "radioButton_Max";
            this.radioButton_Max.Size = new System.Drawing.Size(43, 24);
            this.radioButton_Max.TabIndex = 3;
            this.radioButton_Max.TabStop = true;
            this.radioButton_Max.Text = "all";
            this.radioButton_Max.UseVisualStyleBackColor = true;
            this.radioButton_Max.CheckedChanged += new System.EventHandler(this.radioButton_Max_CheckedChanged);
            // 
            // radioButton_Last
            // 
            this.radioButton_Last.AutoSize = true;
            this.radioButton_Last.Location = new System.Drawing.Point(178, 15);
            this.radioButton_Last.Name = "radioButton_Last";
            this.radioButton_Last.Size = new System.Drawing.Size(50, 24);
            this.radioButton_Last.TabIndex = 2;
            this.radioButton_Last.Text = "last";
            this.radioButton_Last.UseVisualStyleBackColor = true;
            this.radioButton_Last.CheckedChanged += new System.EventHandler(this.radioButton_Last_CheckedChanged);
            // 
            // radioButton_First
            // 
            this.radioButton_First.AutoSize = true;
            this.radioButton_First.Location = new System.Drawing.Point(63, 15);
            this.radioButton_First.Name = "radioButton_First";
            this.radioButton_First.Size = new System.Drawing.Size(52, 24);
            this.radioButton_First.TabIndex = 1;
            this.radioButton_First.Text = "first";
            this.radioButton_First.UseVisualStyleBackColor = true;
            this.radioButton_First.CheckedChanged += new System.EventHandler(this.radioButton_First_CheckedChanged);
            // 
            // groupBox_Transition
            // 
            this.groupBox_Transition.Controls.Add(this.radioButton_All);
            this.groupBox_Transition.Controls.Add(this.radioButton_Negative);
            this.groupBox_Transition.Controls.Add(this.radioButton_Positive);
            this.groupBox_Transition.Location = new System.Drawing.Point(6, 241);
            this.groupBox_Transition.Name = "groupBox_Transition";
            this.groupBox_Transition.Size = new System.Drawing.Size(450, 51);
            this.groupBox_Transition.TabIndex = 73;
            this.groupBox_Transition.TabStop = false;
            this.groupBox_Transition.Text = "极性";
            // 
            // radioButton_All
            // 
            this.radioButton_All.AutoSize = true;
            this.radioButton_All.Location = new System.Drawing.Point(327, 18);
            this.radioButton_All.Name = "radioButton_All";
            this.radioButton_All.Size = new System.Drawing.Size(43, 24);
            this.radioButton_All.TabIndex = 2;
            this.radioButton_All.Text = "all";
            this.radioButton_All.UseVisualStyleBackColor = true;
            this.radioButton_All.CheckedChanged += new System.EventHandler(this.radioButton_All_CheckedChanged);
            // 
            // radioButton_Negative
            // 
            this.radioButton_Negative.AutoSize = true;
            this.radioButton_Negative.Location = new System.Drawing.Point(178, 18);
            this.radioButton_Negative.Name = "radioButton_Negative";
            this.radioButton_Negative.Size = new System.Drawing.Size(85, 24);
            this.radioButton_Negative.TabIndex = 1;
            this.radioButton_Negative.Text = "negative";
            this.radioButton_Negative.UseVisualStyleBackColor = true;
            this.radioButton_Negative.CheckedChanged += new System.EventHandler(this.radioButton_Negative_CheckedChanged);
            // 
            // radioButton_Positive
            // 
            this.radioButton_Positive.AutoSize = true;
            this.radioButton_Positive.Checked = true;
            this.radioButton_Positive.Location = new System.Drawing.Point(65, 18);
            this.radioButton_Positive.Name = "radioButton_Positive";
            this.radioButton_Positive.Size = new System.Drawing.Size(79, 24);
            this.radioButton_Positive.TabIndex = 0;
            this.radioButton_Positive.TabStop = true;
            this.radioButton_Positive.Text = "positive";
            this.radioButton_Positive.UseVisualStyleBackColor = true;
            this.radioButton_Positive.CheckedChanged += new System.EventHandler(this.radioButton_Positive_CheckedChanged);
            // 
            // label_Threshold
            // 
            this.label_Threshold.AutoSize = true;
            this.label_Threshold.Location = new System.Drawing.Point(22, 172);
            this.label_Threshold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Threshold.Name = "label_Threshold";
            this.label_Threshold.Size = new System.Drawing.Size(51, 20);
            this.label_Threshold.TabIndex = 72;
            this.label_Threshold.Text = "阈值：";
            // 
            // trackBar_CheckThreshold
            // 
            this.trackBar_CheckThreshold.Location = new System.Drawing.Point(19, 196);
            this.trackBar_CheckThreshold.Maximum = 200;
            this.trackBar_CheckThreshold.Minimum = 1;
            this.trackBar_CheckThreshold.Name = "trackBar_CheckThreshold";
            this.trackBar_CheckThreshold.Size = new System.Drawing.Size(360, 45);
            this.trackBar_CheckThreshold.TabIndex = 71;
            this.trackBar_CheckThreshold.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_CheckThreshold.Value = 30;
            this.trackBar_CheckThreshold.Scroll += new System.EventHandler(this.trackBar_CheckThreshold_Scroll);
            // 
            // label_CheckNum
            // 
            this.label_CheckNum.AutoSize = true;
            this.label_CheckNum.Location = new System.Drawing.Point(22, 120);
            this.label_CheckNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_CheckNum.Name = "label_CheckNum";
            this.label_CheckNum.Size = new System.Drawing.Size(51, 20);
            this.label_CheckNum.TabIndex = 70;
            this.label_CheckNum.Text = "数量：";
            // 
            // trackBar_CheckNum
            // 
            this.trackBar_CheckNum.Location = new System.Drawing.Point(17, 143);
            this.trackBar_CheckNum.Maximum = 300;
            this.trackBar_CheckNum.Minimum = 1;
            this.trackBar_CheckNum.Name = "trackBar_CheckNum";
            this.trackBar_CheckNum.Size = new System.Drawing.Size(362, 45);
            this.trackBar_CheckNum.TabIndex = 69;
            this.trackBar_CheckNum.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_CheckNum.Value = 20;
            this.trackBar_CheckNum.Scroll += new System.EventHandler(this.trackBar_CheckNum_Scroll);
            // 
            // trackBar_CheckHeight
            // 
            this.trackBar_CheckHeight.Location = new System.Drawing.Point(26, 44);
            this.trackBar_CheckHeight.Maximum = 1500;
            this.trackBar_CheckHeight.Minimum = 1;
            this.trackBar_CheckHeight.Name = "trackBar_CheckHeight";
            this.trackBar_CheckHeight.Size = new System.Drawing.Size(353, 45);
            this.trackBar_CheckHeight.TabIndex = 60;
            this.trackBar_CheckHeight.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_CheckHeight.Value = 120;
            this.trackBar_CheckHeight.Scroll += new System.EventHandler(this.trackBar_CheckHeight_Scroll);
            // 
            // trackBar_CheckWidth
            // 
            this.trackBar_CheckWidth.Location = new System.Drawing.Point(21, 93);
            this.trackBar_CheckWidth.Maximum = 300;
            this.trackBar_CheckWidth.Minimum = 1;
            this.trackBar_CheckWidth.Name = "trackBar_CheckWidth";
            this.trackBar_CheckWidth.Size = new System.Drawing.Size(358, 45);
            this.trackBar_CheckWidth.TabIndex = 68;
            this.trackBar_CheckWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_CheckWidth.Value = 10;
            this.trackBar_CheckWidth.Scroll += new System.EventHandler(this.trackBar_CheckWidth_Scroll);
            // 
            // textBox_Mes
            // 
            this.textBox_Mes.Location = new System.Drawing.Point(3, 702);
            this.textBox_Mes.Name = "textBox_Mes";
            this.textBox_Mes.Size = new System.Drawing.Size(494, 150);
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
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
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
            this.label_ToolName.Size = new System.Drawing.Size(494, 28);
            this.label_ToolName.TabIndex = 0;
            this.label_ToolName.Text = "ToolName";
            this.label_ToolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl_Check
            // 
            this.tabControl_Check.Controls.Add(this.tabPage_Setting);
            this.tabControl_Check.Location = new System.Drawing.Point(3, 32);
            this.tabControl_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl_Check.Multiline = true;
            this.tabControl_Check.Name = "tabControl_Check";
            this.tabControl_Check.SelectedIndex = 0;
            this.tabControl_Check.Size = new System.Drawing.Size(494, 663);
            this.tabControl_Check.TabIndex = 1;
            // 
            // UToolFindCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UToolFindCircle";
            this.Size = new System.Drawing.Size(500, 855);
            this.Load += new System.EventHandler(this.UToolFindCircle_Load);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).EndInit();
            this.groupBox_FindLine.ResumeLayout(false);
            this.groupBox_FindLine.PerformLayout();
            this.groupBox_Select.ResumeLayout(false);
            this.groupBox_Select.PerformLayout();
            this.groupBox_Transition.ResumeLayout(false);
            this.groupBox_Transition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_CheckWidth)).EndInit();
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tabControl_Check.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Setting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_ToolName;
        private System.Windows.Forms.TabControl tabControl_Check;
        private System.Windows.Forms.CheckBox checkBox_ShowRegion;
        private System.Windows.Forms.Button button_DrawRoi;
        private System.Windows.Forms.CheckBox checkBox_DrawLine;
        private System.Windows.Forms.GroupBox groupBox_FindLine;
        private System.Windows.Forms.TextBox textBox_Threshold;
        private System.Windows.Forms.TextBox textBox_CheckNum;
        private System.Windows.Forms.TextBox textBox_CheckWidth;
        private System.Windows.Forms.TextBox textBox_CheckHeight;
        private System.Windows.Forms.GroupBox groupBox_Select;
        private System.Windows.Forms.RadioButton radioButton_Max;
        private System.Windows.Forms.RadioButton radioButton_Last;
        private System.Windows.Forms.RadioButton radioButton_First;
        private System.Windows.Forms.GroupBox groupBox_Transition;
        private System.Windows.Forms.RadioButton radioButton_All;
        private System.Windows.Forms.RadioButton radioButton_Negative;
        private System.Windows.Forms.RadioButton radioButton_Positive;
        private System.Windows.Forms.Label label_Threshold;
        private System.Windows.Forms.TrackBar trackBar_CheckThreshold;
        private System.Windows.Forms.Label label_CheckNum;
        private System.Windows.Forms.TrackBar trackBar_CheckNum;
        private System.Windows.Forms.TrackBar trackBar_CheckWidth;
        private System.Windows.Forms.TrackBar trackBar_CheckHeight;
        private System.Windows.Forms.Label label_CheckWidth;
        private System.Windows.Forms.Label label_CheckHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetrunValue1;
        private System.Windows.Forms.CheckBox checkBox_ForceOK;
        private System.Windows.Forms.RichTextBox textBox_Mes;
        private System.Windows.Forms.TextBox textBox_RefreshName;
        private System.Windows.Forms.CheckBox checkBox_ReName;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboBox_ImageStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_PositioningStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_LineSourceStep;
    }
}
