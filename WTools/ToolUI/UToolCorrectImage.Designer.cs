
namespace WTools
{
    partial class UToolCorrectImage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox_PositioningStep = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label_Positioning = new System.Windows.Forms.Label();
            this.comboBox_ImageStep = new System.Windows.Forms.ComboBox();
            this.textBox_RefreshName = new System.Windows.Forms.TextBox();
            this.checkBox_ReName = new System.Windows.Forms.CheckBox();
            this.checkBox_ForceOK = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_RetrunValue1 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Mes = new System.Windows.Forms.RichTextBox();
            this.label_ToolName = new System.Windows.Forms.Label();
            this.tabControl_Check = new System.Windows.Forms.TabControl();
            this.tabPage_Setting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).BeginInit();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tabControl_Check.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage_Setting
            // 
            this.tabPage_Setting.BackColor = System.Drawing.Color.Ivory;
            this.tabPage_Setting.Controls.Add(this.groupBox1);
            this.tabPage_Setting.Controls.Add(this.textBox_RefreshName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ReName);
            this.tabPage_Setting.Controls.Add(this.checkBox_ForceOK);
            this.tabPage_Setting.Controls.Add(this.label8);
            this.tabPage_Setting.Controls.Add(this.numericUpDown_RetrunValue1);
            this.tabPage_Setting.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Size = new System.Drawing.Size(486, 631);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "设置";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_PositioningStep);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label_Positioning);
            this.groupBox1.Controls.Add(this.comboBox_ImageStep);
            this.groupBox1.Location = new System.Drawing.Point(5, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 114);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入/输出";
            // 
            // comboBox_PositioningStep
            // 
            this.comboBox_PositioningStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_PositioningStep.FormattingEnabled = true;
            this.comboBox_PositioningStep.Location = new System.Drawing.Point(115, 65);
            this.comboBox_PositioningStep.Name = "comboBox_PositioningStep";
            this.comboBox_PositioningStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_PositioningStep.TabIndex = 79;
            this.comboBox_PositioningStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_PositioningStep_SelectedIndexChanged_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 20);
            this.label10.TabIndex = 80;
            this.label10.Text = "图像输入步骤：";
            // 
            // label_Positioning
            // 
            this.label_Positioning.AutoSize = true;
            this.label_Positioning.Location = new System.Drawing.Point(7, 68);
            this.label_Positioning.Name = "label_Positioning";
            this.label_Positioning.Size = new System.Drawing.Size(107, 20);
            this.label_Positioning.TabIndex = 78;
            this.label_Positioning.Text = "定位输入步骤：";
            // 
            // comboBox_ImageStep
            // 
            this.comboBox_ImageStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ImageStep.FormattingEnabled = true;
            this.comboBox_ImageStep.Location = new System.Drawing.Point(115, 26);
            this.comboBox_ImageStep.Name = "comboBox_ImageStep";
            this.comboBox_ImageStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_ImageStep.TabIndex = 81;
            this.comboBox_ImageStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_ImageStep_SelectedIndexChanged);
            // 
            // textBox_RefreshName
            // 
            this.textBox_RefreshName.Enabled = false;
            this.textBox_RefreshName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBox_RefreshName.Location = new System.Drawing.Point(120, 43);
            this.textBox_RefreshName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_RefreshName.Name = "textBox_RefreshName";
            this.textBox_RefreshName.Size = new System.Drawing.Size(280, 25);
            this.textBox_RefreshName.TabIndex = 71;
            // 
            // checkBox_ReName
            // 
            this.checkBox_ReName.AutoSize = true;
            this.checkBox_ReName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ReName.Location = new System.Drawing.Point(14, 43);
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
            this.checkBox_ForceOK.Location = new System.Drawing.Point(14, 11);
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
            this.label8.Location = new System.Drawing.Point(5, 576);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "NG返回值设定";
            // 
            // numericUpDown_RetrunValue1
            // 
            this.numericUpDown_RetrunValue1.Location = new System.Drawing.Point(6, 599);
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
            // tableLayoutPanel_Body
            // 
            this.tableLayoutPanel_Body.ColumnCount = 1;
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.Controls.Add(this.textBox_Mes, 0, 2);
            this.tableLayoutPanel_Body.Controls.Add(this.label_ToolName, 0, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.tabControl_Check, 0, 1);
            this.tableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Body.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Body.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel_Body.Name = "tableLayoutPanel_Body";
            this.tableLayoutPanel_Body.RowCount = 3;
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tableLayoutPanel_Body.Size = new System.Drawing.Size(500, 855);
            this.tableLayoutPanel_Body.TabIndex = 6;
            // 
            // textBox_Mes
            // 
            this.textBox_Mes.Location = new System.Drawing.Point(3, 701);
            this.textBox_Mes.Name = "textBox_Mes";
            this.textBox_Mes.Size = new System.Drawing.Size(494, 151);
            this.textBox_Mes.TabIndex = 62;
            this.textBox_Mes.Text = "";
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
            this.tabControl_Check.Size = new System.Drawing.Size(494, 663);
            this.tabControl_Check.TabIndex = 1;
            // 
            // UToolCorrectImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UToolCorrectImage";
            this.Size = new System.Drawing.Size(500, 855);
            this.Load += new System.EventHandler(this.UToolCorrectImage_Load);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).EndInit();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetrunValue1;
        private System.Windows.Forms.CheckBox checkBox_ForceOK;
        private System.Windows.Forms.TextBox textBox_RefreshName;
        private System.Windows.Forms.CheckBox checkBox_ReName;
        private System.Windows.Forms.ComboBox comboBox_ImageStep;
        private System.Windows.Forms.Label label_Positioning;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_PositioningStep;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox textBox_Mes;
    }
}
