
namespace WTools
{
    partial class UToolGenBitmap
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
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RetrunValue1)).BeginInit();
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
            this.tabPage_Setting.Controls.Add(this.label8);
            this.tabPage_Setting.Controls.Add(this.numericUpDown_RetrunValue1);
            this.tabPage_Setting.Controls.Add(this.checkBox_ForceOK);
            this.tabPage_Setting.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabPage_Setting.Location = new System.Drawing.Point(4, 28);
            this.tabPage_Setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Name = "tabPage_Setting";
            this.tabPage_Setting.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage_Setting.Size = new System.Drawing.Size(486, 605);
            this.tabPage_Setting.TabIndex = 0;
            this.tabPage_Setting.Text = "设置";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.comboBox_ImageStep);
            this.groupBox5.Location = new System.Drawing.Point(8, 83);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(472, 88);
            this.groupBox5.TabIndex = 79;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "输入输出";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(4, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 20);
            this.label21.TabIndex = 82;
            this.label21.Text = "图像输入步骤：";
            // 
            // comboBox_ImageStep
            // 
            this.comboBox_ImageStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ImageStep.FormattingEnabled = true;
            this.comboBox_ImageStep.Location = new System.Drawing.Point(123, 33);
            this.comboBox_ImageStep.Name = "comboBox_ImageStep";
            this.comboBox_ImageStep.Size = new System.Drawing.Size(225, 27);
            this.comboBox_ImageStep.TabIndex = 83;
            this.comboBox_ImageStep.SelectedIndexChanged += new System.EventHandler(this.comboBox_ImageStep_SelectedIndexChanged);
            // 
            // textBox_RefreshName
            // 
            this.textBox_RefreshName.Enabled = false;
            this.textBox_RefreshName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBox_RefreshName.Location = new System.Drawing.Point(93, 40);
            this.textBox_RefreshName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_RefreshName.Name = "textBox_RefreshName";
            this.textBox_RefreshName.Size = new System.Drawing.Size(280, 25);
            this.textBox_RefreshName.TabIndex = 63;
            // 
            // checkBox_ReName
            // 
            this.checkBox_ReName.AutoSize = true;
            this.checkBox_ReName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_ReName.Location = new System.Drawing.Point(10, 40);
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
            this.label8.Location = new System.Drawing.Point(5, 547);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 20);
            this.label8.TabIndex = 57;
            this.label8.Text = "NG返回值设定";
            // 
            // numericUpDown_RetrunValue1
            // 
            this.numericUpDown_RetrunValue1.Location = new System.Drawing.Point(6, 572);
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
            this.checkBox_ForceOK.Location = new System.Drawing.Point(10, 8);
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
            this.textBox_Mes.Location = new System.Drawing.Point(3, 679);
            this.textBox_Mes.Name = "textBox_Mes";
            this.textBox_Mes.Size = new System.Drawing.Size(494, 173);
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
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 179F));
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
            this.tabControl_Check.Size = new System.Drawing.Size(494, 637);
            this.tabControl_Check.TabIndex = 1;
            // 
            // UToolGenBitmap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UToolGenBitmap";
            this.Size = new System.Drawing.Size(500, 855);
            this.Load += new System.EventHandler(this.UToolGenBitmap_Load);
            this.tabPage_Setting.ResumeLayout(false);
            this.tabPage_Setting.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_RetrunValue1;
        private System.Windows.Forms.RichTextBox textBox_Mes;
        private System.Windows.Forms.TextBox textBox_RefreshName;
        private System.Windows.Forms.CheckBox checkBox_ReName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboBox_ImageStep;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}
