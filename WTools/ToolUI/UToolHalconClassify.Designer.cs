
namespace WTools
{
    partial class UToolHalconClassify
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
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox_ImageStep = new System.Windows.Forms.ComboBox();
            this.label_Positioning = new System.Windows.Forms.Label();
            this.comboBox_PositioningStep = new System.Windows.Forms.ComboBox();
            this.textBox_RefreshName = new System.Windows.Forms.TextBox();
            this.checkBox_ReName = new System.Windows.Forms.CheckBox();
            this.checkBox_ForceOK = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_RetrunValue1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_GpuID = new System.Windows.Forms.NumericUpDown();
            this.button_LoadModel = new System.Windows.Forms.Button();
            this.button_SelectModelPath = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_VidiPath = new System.Windows.Forms.TextBox();
            this.numericUpDown_BatchSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Mes = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_ToolName = new System.Windows.Forms.Label();
            this.tabControl_Check = new System.Windows.Forms.TabControl();
            this.tabPage_Param = new System.Windows.Forms.TabPage();
            this.textBox_ResultName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_LineList = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage_Setting.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_GpuID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BatchSize)).BeginInit();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tabControl_Check.SuspendLayout();
            this.tabPage_Param.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_LineList)).BeginInit();
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
            this.tabPage_Setting.Controls.Add(this.groupBox1);
            this.tabPage_Setting.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Size = new System.Drawing.Size(486, 651);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "设置";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.comboBox_ImageStep);
            this.groupBox5.Controls.Add(this.label_Positioning);
            this.groupBox5.Controls.Add(this.comboBox_PositioningStep);
            this.groupBox5.Location = new System.Drawing.Point(5, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(475, 115);
            this.groupBox5.TabIndex = 72;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "输入输出";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 20);
            this.label21.TabIndex = 84;
            this.label21.Text = "图像输入步骤：";
            // 
            // comboBox_ImageStep
            // 
            this.comboBox_ImageStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ImageStep.FormattingEnabled = true;
            this.comboBox_ImageStep.Location = new System.Drawing.Point(130, 30);
            this.comboBox_ImageStep.Name = "comboBox_ImageStep";
            this.comboBox_ImageStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_ImageStep.TabIndex = 85;
            this.comboBox_ImageStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_ImageStep_SelectedIndexChanged);
            // 
            // label_Positioning
            // 
            this.label_Positioning.AutoSize = true;
            this.label_Positioning.Location = new System.Drawing.Point(9, 73);
            this.label_Positioning.Name = "label_Positioning";
            this.label_Positioning.Size = new System.Drawing.Size(107, 20);
            this.label_Positioning.TabIndex = 70;
            this.label_Positioning.Text = "定位输入步骤：";
            // 
            // comboBox_PositioningStep
            // 
            this.comboBox_PositioningStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PositioningStep.FormattingEnabled = true;
            this.comboBox_PositioningStep.Location = new System.Drawing.Point(130, 70);
            this.comboBox_PositioningStep.Name = "comboBox_PositioningStep";
            this.comboBox_PositioningStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_PositioningStep.TabIndex = 71;
            this.comboBox_PositioningStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_PositioningStep_SelectedIndexChanged);
            // 
            // textBox_RefreshName
            // 
            this.textBox_RefreshName.Enabled = false;
            this.textBox_RefreshName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBox_RefreshName.Location = new System.Drawing.Point(100, 38);
            this.textBox_RefreshName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_RefreshName.Name = "textBox_RefreshName";
            this.textBox_RefreshName.Size = new System.Drawing.Size(280, 25);
            this.textBox_RefreshName.TabIndex = 69;
            // 
            // checkBox_ReName
            // 
            this.checkBox_ReName.AutoSize = true;
            this.checkBox_ReName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ReName.Location = new System.Drawing.Point(10, 39);
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
            this.checkBox_ForceOK.Location = new System.Drawing.Point(10, 9);
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
            this.label8.Location = new System.Drawing.Point(6, 555);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "NG返回值设定";
            // 
            // numericUpDown_RetrunValue1
            // 
            this.numericUpDown_RetrunValue1.Location = new System.Drawing.Point(7, 580);
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
            this.groupBox1.Controls.Add(this.numericUpDown_GpuID);
            this.groupBox1.Controls.Add(this.button_LoadModel);
            this.groupBox1.Controls.Add(this.button_SelectModelPath);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox_VidiPath);
            this.groupBox1.Controls.Add(this.numericUpDown_BatchSize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(5, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 175);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设定";
            // 
            // numericUpDown_GpuID
            // 
            this.numericUpDown_GpuID.Location = new System.Drawing.Point(358, 102);
            this.numericUpDown_GpuID.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDown_GpuID.Name = "numericUpDown_GpuID";
            this.numericUpDown_GpuID.Size = new System.Drawing.Size(100, 25);
            this.numericUpDown_GpuID.TabIndex = 44;
            // 
            // button_LoadModel
            // 
            this.button_LoadModel.Location = new System.Drawing.Point(196, 128);
            this.button_LoadModel.Name = "button_LoadModel";
            this.button_LoadModel.Size = new System.Drawing.Size(100, 36);
            this.button_LoadModel.TabIndex = 34;
            this.button_LoadModel.Text = "加载模型";
            this.button_LoadModel.UseVisualStyleBackColor = true;
            this.button_LoadModel.Click += new System.EventHandler(this.button_LoadModel_Click);
            // 
            // button_SelectModelPath
            // 
            this.button_SelectModelPath.Location = new System.Drawing.Point(49, 128);
            this.button_SelectModelPath.Name = "button_SelectModelPath";
            this.button_SelectModelPath.Size = new System.Drawing.Size(100, 36);
            this.button_SelectModelPath.TabIndex = 33;
            this.button_SelectModelPath.Text = "选择模型";
            this.button_SelectModelPath.UseVisualStyleBackColor = true;
            this.button_SelectModelPath.Click += new System.EventHandler(this.button_SelectModelPath_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(380, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 20);
            this.label11.TabIndex = 43;
            this.label11.Text = "GPU号：";
            // 
            // textBox_VidiPath
            // 
            this.textBox_VidiPath.Location = new System.Drawing.Point(11, 50);
            this.textBox_VidiPath.Multiline = true;
            this.textBox_VidiPath.Name = "textBox_VidiPath";
            this.textBox_VidiPath.Size = new System.Drawing.Size(321, 70);
            this.textBox_VidiPath.TabIndex = 32;
            // 
            // numericUpDown_BatchSize
            // 
            this.numericUpDown_BatchSize.Location = new System.Drawing.Point(358, 51);
            this.numericUpDown_BatchSize.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDown_BatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_BatchSize.Name = "numericUpDown_BatchSize";
            this.numericUpDown_BatchSize.Size = new System.Drawing.Size(100, 25);
            this.numericUpDown_BatchSize.TabIndex = 42;
            this.numericUpDown_BatchSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_BatchSize.ValueChanged += new System.EventHandler(this.numericUpDown_BatchSize_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "模型路径";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "单次处理数：";
            // 
            // textBox_Mes
            // 
            this.textBox_Mes.Location = new System.Drawing.Point(3, 723);
            this.textBox_Mes.Name = "textBox_Mes";
            this.textBox_Mes.Size = new System.Drawing.Size(494, 129);
            this.textBox_Mes.TabIndex = 60;
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
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
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
            this.label_ToolName.Size = new System.Drawing.Size(494, 29);
            this.label_ToolName.TabIndex = 0;
            this.label_ToolName.Text = "ToolName";
            this.label_ToolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl_Check
            // 
            this.tabControl_Check.Controls.Add(this.tabPage_Setting);
            this.tabControl_Check.Controls.Add(this.tabPage_Param);
            this.tabControl_Check.Location = new System.Drawing.Point(3, 33);
            this.tabControl_Check.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl_Check.Multiline = true;
            this.tabControl_Check.Name = "tabControl_Check";
            this.tabControl_Check.SelectedIndex = 0;
            this.tabControl_Check.Size = new System.Drawing.Size(494, 683);
            this.tabControl_Check.TabIndex = 1;
            // 
            // tabPage_Param
            // 
            this.tabPage_Param.BackColor = System.Drawing.Color.Ivory;
            this.tabPage_Param.Controls.Add(this.button1);
            this.tabPage_Param.Controls.Add(this.textBox_ResultName);
            this.tabPage_Param.Controls.Add(this.label2);
            this.tabPage_Param.Controls.Add(this.dataGridView_LineList);
            this.tabPage_Param.Controls.Add(this.label12);
            this.tabPage_Param.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Param.Name = "tabPage_Param";
            this.tabPage_Param.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Param.Size = new System.Drawing.Size(486, 651);
            this.tabPage_Param.TabIndex = 1;
            this.tabPage_Param.Text = "参数";
            // 
            // textBox_ResultName
            // 
            this.textBox_ResultName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.textBox_ResultName.Location = new System.Drawing.Point(341, 92);
            this.textBox_ResultName.Name = "textBox_ResultName";
            this.textBox_ResultName.Size = new System.Drawing.Size(107, 32);
            this.textBox_ResultName.TabIndex = 99;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 98;
            this.label2.Text = "预期结果";
            // 
            // dataGridView_LineList
            // 
            this.dataGridView_LineList.AllowDrop = true;
            this.dataGridView_LineList.AllowUserToAddRows = false;
            this.dataGridView_LineList.AllowUserToDeleteRows = false;
            this.dataGridView_LineList.AllowUserToResizeRows = false;
            this.dataGridView_LineList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_LineList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView_LineList.BackgroundColor = System.Drawing.Color.Ivory;
            this.dataGridView_LineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_LineList.GridColor = System.Drawing.Color.Azure;
            this.dataGridView_LineList.Location = new System.Drawing.Point(6, 38);
            this.dataGridView_LineList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView_LineList.MultiSelect = false;
            this.dataGridView_LineList.Name = "dataGridView_LineList";
            this.dataGridView_LineList.RowHeadersVisible = false;
            this.dataGridView_LineList.RowHeadersWidth = 43;
            this.dataGridView_LineList.RowTemplate.Height = 23;
            this.dataGridView_LineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_LineList.Size = new System.Drawing.Size(311, 132);
            this.dataGridView_LineList.TabIndex = 97;
            this.dataGridView_LineList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_LineList_CellValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(106, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 20);
            this.label12.TabIndex = 95;
            this.label12.Text = "类别列表";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(363, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 29);
            this.button1.TabIndex = 100;
            this.button1.Text = "写入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UToolHalconClassify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UToolHalconClassify";
            this.Size = new System.Drawing.Size(500, 855);
            this.Load += new System.EventHandler(this.UToolHalconClassify_Load);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_GpuID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_BatchSize)).EndInit();
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tabControl_Check.ResumeLayout(false);
            this.tabPage_Param.ResumeLayout(false);
            this.tabPage_Param.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_LineList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage_Setting;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_ToolName;
        private System.Windows.Forms.TabControl tabControl_Check;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_VidiPath;
        private System.Windows.Forms.Button button_LoadModel;
        private System.Windows.Forms.Button button_SelectModelPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown_BatchSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetrunValue1;
        private System.Windows.Forms.CheckBox checkBox_ForceOK;
        private System.Windows.Forms.RichTextBox textBox_Mes;
        private System.Windows.Forms.TextBox textBox_RefreshName;
        private System.Windows.Forms.CheckBox checkBox_ReName;
        private System.Windows.Forms.ComboBox comboBox_PositioningStep;
        private System.Windows.Forms.Label label_Positioning;
        private System.Windows.Forms.TabPage tabPage_Param;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericUpDown_GpuID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboBox_ImageStep;
        private System.Windows.Forms.DataGridView dataGridView_LineList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ResultName;
        private System.Windows.Forms.Button button1;
    }
}
