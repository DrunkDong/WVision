namespace WVision
{
    partial class FrmProjectSet
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProjectSet));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_CamStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CamStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_ScaleImageSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SaveParam = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Close = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel_MainBody = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_ToolList = new System.Windows.Forms.DataGridView();
            this.Column_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column_ToolName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column_CostTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_Tools = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上移工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_ToolForm = new System.Windows.Forms.GroupBox();
            this.panel_ToolForm = new System.Windows.Forms.Panel();
            this.panel_body = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox_Mes = new System.Windows.Forms.RichTextBox();
            this.panel_ControlBox = new System.Windows.Forms.Panel();
            this.button_CopyNG = new System.Windows.Forms.Button();
            this.label_CurrTool = new System.Windows.Forms.Label();
            this.button_CopyOk = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.label_CurrID = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_ActionTool = new System.Windows.Forms.Button();
            this.button_ActionAll = new System.Windows.Forms.Button();
            this.radioButton_OKorNG = new System.Windows.Forms.RadioButton();
            this.radioButton_OKStop = new System.Windows.Forms.RadioButton();
            this.radioButton_NgStop = new System.Windows.Forms.RadioButton();
            this.checkBox_EnableBox = new System.Windows.Forms.CheckBox();
            this.panel_ToolTree = new System.Windows.Forms.Panel();
            this.mTreeViewTools = new System.Windows.Forms.TreeView();
            this.panel_WorkView = new System.Windows.Forms.Panel();
            this.panel_ToolButton = new System.Windows.Forms.Panel();
            this.label_ToolStep = new System.Windows.Forms.Label();
            this.label_TaskNum = new System.Windows.Forms.Label();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Open = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_UP = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Down = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Task9 = new System.Windows.Forms.Button();
            this.button_Task8 = new System.Windows.Forms.Button();
            this.button_Task5 = new System.Windows.Forms.Button();
            this.button_Task7 = new System.Windows.Forms.Button();
            this.button_Task6 = new System.Windows.Forms.Button();
            this.button_Task4 = new System.Windows.Forms.Button();
            this.button_Task1 = new System.Windows.Forms.Button();
            this.button_Task3 = new System.Windows.Forms.Button();
            this.button_Task2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_CurrProject = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer_show = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel_MainBody.SuspendLayout();
            this.tableLayoutPanel_Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ToolList)).BeginInit();
            this.contextMenuStrip_Tools.SuspendLayout();
            this.groupBox_ToolForm.SuspendLayout();
            this.panel_body.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_ControlBox.SuspendLayout();
            this.panel_ToolTree.SuspendLayout();
            this.panel_ToolButton.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(17, 17);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_CamStart,
            this.toolStripButton_CamStop,
            this.toolStripButton_ScaleImageSize,
            this.toolStripButton_SaveParam,
            this.toolStripButton_Close});
            this.toolStrip1.Location = new System.Drawing.Point(1627, -1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(271, 59);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStrip1_Paint);
            // 
            // toolStripButton_CamStart
            // 
            this.toolStripButton_CamStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_CamStart.Image = global::WVision.Properties.Resources.Start;
            this.toolStripButton_CamStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_CamStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CamStart.Name = "toolStripButton_CamStart";
            this.toolStripButton_CamStart.Size = new System.Drawing.Size(52, 56);
            this.toolStripButton_CamStart.Text = "相机采集";
            this.toolStripButton_CamStart.Click += new System.EventHandler(this.toolStripButton_CamStart_Click);
            // 
            // toolStripButton_CamStop
            // 
            this.toolStripButton_CamStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_CamStop.Enabled = false;
            this.toolStripButton_CamStop.Image = global::WVision.Properties.Resources.Stop;
            this.toolStripButton_CamStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_CamStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CamStop.Name = "toolStripButton_CamStop";
            this.toolStripButton_CamStop.Size = new System.Drawing.Size(52, 56);
            this.toolStripButton_CamStop.Text = "相机停止";
            this.toolStripButton_CamStop.Click += new System.EventHandler(this.toolStripButton_CamStop_Click);
            // 
            // toolStripButton_ScaleImageSize
            // 
            this.toolStripButton_ScaleImageSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ScaleImageSize.Image = global::WVision.Properties.Resources.fill2;
            this.toolStripButton_ScaleImageSize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_ScaleImageSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ScaleImageSize.Name = "toolStripButton_ScaleImageSize";
            this.toolStripButton_ScaleImageSize.Size = new System.Drawing.Size(56, 56);
            this.toolStripButton_ScaleImageSize.Text = "自适应图片";
            this.toolStripButton_ScaleImageSize.Click += new System.EventHandler(this.toolStripButton_ScaleImageSize_Click);
            // 
            // toolStripButton_SaveParam
            // 
            this.toolStripButton_SaveParam.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_SaveParam.Image = global::WVision.Properties.Resources.save2;
            this.toolStripButton_SaveParam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_SaveParam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SaveParam.Name = "toolStripButton_SaveParam";
            this.toolStripButton_SaveParam.Size = new System.Drawing.Size(56, 56);
            this.toolStripButton_SaveParam.Text = "保存参数";
            this.toolStripButton_SaveParam.Click += new System.EventHandler(this.toolStripButton_SaveParam_Click);
            // 
            // toolStripButton_Close
            // 
            this.toolStripButton_Close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Close.Image = global::WVision.Properties.Resources.exit2;
            this.toolStripButton_Close.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Close.Name = "toolStripButton_Close";
            this.toolStripButton_Close.Size = new System.Drawing.Size(52, 56);
            this.toolStripButton_Close.Text = "退出";
            this.toolStripButton_Close.Click += new System.EventHandler(this.toolStripButton_Close_Click);
            // 
            // tableLayoutPanel_MainBody
            // 
            this.tableLayoutPanel_MainBody.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel_MainBody.ColumnCount = 1;
            this.tableLayoutPanel_MainBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_MainBody.Controls.Add(this.tableLayoutPanel_Body, 0, 2);
            this.tableLayoutPanel_MainBody.Controls.Add(this.panel_ToolButton, 0, 1);
            this.tableLayoutPanel_MainBody.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel_MainBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_MainBody.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_MainBody.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel_MainBody.Name = "tableLayoutPanel_MainBody";
            this.tableLayoutPanel_MainBody.RowCount = 3;
            this.tableLayoutPanel_MainBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.553299F));
            this.tableLayoutPanel_MainBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.106599F));
            this.tableLayoutPanel_MainBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.44163F));
            this.tableLayoutPanel_MainBody.Size = new System.Drawing.Size(1904, 1011);
            this.tableLayoutPanel_MainBody.TabIndex = 3;
            // 
            // tableLayoutPanel_Body
            // 
            this.tableLayoutPanel_Body.ColumnCount = 3;
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.37619F));
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.64805F));
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.92308F));
            this.tableLayoutPanel_Body.Controls.Add(this.dataGridView_ToolList, 0, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.groupBox_ToolForm, 2, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.panel_body, 0, 0);
            this.tableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Body.Location = new System.Drawing.Point(3, 108);
            this.tableLayoutPanel_Body.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel_Body.Name = "tableLayoutPanel_Body";
            this.tableLayoutPanel_Body.RowCount = 1;
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.Size = new System.Drawing.Size(1898, 901);
            this.tableLayoutPanel_Body.TabIndex = 0;
            // 
            // dataGridView_ToolList
            // 
            this.dataGridView_ToolList.AllowDrop = true;
            this.dataGridView_ToolList.AllowUserToAddRows = false;
            this.dataGridView_ToolList.AllowUserToDeleteRows = false;
            this.dataGridView_ToolList.AllowUserToResizeRows = false;
            this.dataGridView_ToolList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView_ToolList.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridView_ToolList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ToolList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Index,
            this.Column_Status,
            this.Column_ToolName,
            this.Column_CostTime});
            this.dataGridView_ToolList.ContextMenuStrip = this.contextMenuStrip_Tools;
            this.dataGridView_ToolList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_ToolList.GridColor = System.Drawing.Color.Azure;
            this.dataGridView_ToolList.Location = new System.Drawing.Point(1092, 2);
            this.dataGridView_ToolList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView_ToolList.Name = "dataGridView_ToolList";
            this.dataGridView_ToolList.RowHeadersVisible = false;
            this.dataGridView_ToolList.RowHeadersWidth = 43;
            this.dataGridView_ToolList.RowTemplate.Height = 23;
            this.dataGridView_ToolList.Size = new System.Drawing.Size(291, 897);
            this.dataGridView_ToolList.TabIndex = 8;
            this.dataGridView_ToolList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ToolList_CellClick);
            this.dataGridView_ToolList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ToolList_CellMouseDown);
            this.dataGridView_ToolList.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView_ToolList_DragDrop);
            this.dataGridView_ToolList.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView_ToolList_DragEnter);
            // 
            // Column_Index
            // 
            this.Column_Index.HeaderText = "";
            this.Column_Index.MinimumWidth = 6;
            this.Column_Index.Name = "Column_Index";
            this.Column_Index.ReadOnly = true;
            this.Column_Index.Width = 30;
            // 
            // Column_Status
            // 
            this.Column_Status.HeaderText = "状态";
            this.Column_Status.MinimumWidth = 6;
            this.Column_Status.Name = "Column_Status";
            this.Column_Status.ReadOnly = true;
            this.Column_Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column_Status.Width = 40;
            // 
            // Column_ToolName
            // 
            this.Column_ToolName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column_ToolName.HeaderText = "工具列表";
            this.Column_ToolName.MinimumWidth = 6;
            this.Column_ToolName.Name = "Column_ToolName";
            this.Column_ToolName.ReadOnly = true;
            this.Column_ToolName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column_CostTime
            // 
            this.Column_CostTime.HeaderText = "耗时";
            this.Column_CostTime.MinimumWidth = 6;
            this.Column_CostTime.Name = "Column_CostTime";
            this.Column_CostTime.Width = 60;
            // 
            // contextMenuStrip_Tools
            // 
            this.contextMenuStrip_Tools.ImageScalingSize = new System.Drawing.Size(17, 17);
            this.contextMenuStrip_Tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除工具ToolStripMenuItem,
            this.上移工具ToolStripMenuItem,
            this.下移工具ToolStripMenuItem});
            this.contextMenuStrip_Tools.Name = "contextMenuStrip_Tools";
            this.contextMenuStrip_Tools.Size = new System.Drawing.Size(139, 76);
            // 
            // 删除工具ToolStripMenuItem
            // 
            this.删除工具ToolStripMenuItem.Name = "删除工具ToolStripMenuItem";
            this.删除工具ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.删除工具ToolStripMenuItem.Text = "删除工具";
            this.删除工具ToolStripMenuItem.Click += new System.EventHandler(this.删除工具ToolStripMenuItem_Click);
            // 
            // 上移工具ToolStripMenuItem
            // 
            this.上移工具ToolStripMenuItem.Name = "上移工具ToolStripMenuItem";
            this.上移工具ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.上移工具ToolStripMenuItem.Text = "上移工具";
            this.上移工具ToolStripMenuItem.Click += new System.EventHandler(this.上移工具ToolStripMenuItem_Click);
            // 
            // 下移工具ToolStripMenuItem
            // 
            this.下移工具ToolStripMenuItem.Name = "下移工具ToolStripMenuItem";
            this.下移工具ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.下移工具ToolStripMenuItem.Text = "下移工具";
            this.下移工具ToolStripMenuItem.Click += new System.EventHandler(this.下移工具ToolStripMenuItem_Click);
            // 
            // groupBox_ToolForm
            // 
            this.groupBox_ToolForm.Controls.Add(this.panel_ToolForm);
            this.groupBox_ToolForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ToolForm.Location = new System.Drawing.Point(1389, 2);
            this.groupBox_ToolForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_ToolForm.Name = "groupBox_ToolForm";
            this.groupBox_ToolForm.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox_ToolForm.Size = new System.Drawing.Size(506, 897);
            this.groupBox_ToolForm.TabIndex = 4;
            this.groupBox_ToolForm.TabStop = false;
            this.groupBox_ToolForm.Text = "工具";
            // 
            // panel_ToolForm
            // 
            this.panel_ToolForm.BackColor = System.Drawing.Color.Azure;
            this.panel_ToolForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ToolForm.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.panel_ToolForm.Location = new System.Drawing.Point(3, 22);
            this.panel_ToolForm.Name = "panel_ToolForm";
            this.panel_ToolForm.Size = new System.Drawing.Size(500, 873);
            this.panel_ToolForm.TabIndex = 1;
            // 
            // panel_body
            // 
            this.panel_body.Controls.Add(this.groupBox1);
            this.panel_body.Controls.Add(this.panel_ToolTree);
            this.panel_body.Controls.Add(this.panel_WorkView);
            this.panel_body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_body.Location = new System.Drawing.Point(3, 2);
            this.panel_body.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_body.Name = "panel_body";
            this.panel_body.Size = new System.Drawing.Size(1083, 897);
            this.panel_body.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBox_Mes);
            this.groupBox1.Controls.Add(this.panel_ControlBox);
            this.groupBox1.Controls.Add(this.checkBox_EnableBox);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox1.Location = new System.Drawing.Point(143, 587);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(937, 285);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "批量操作面板";
            // 
            // richTextBox_Mes
            // 
            this.richTextBox_Mes.Location = new System.Drawing.Point(433, 23);
            this.richTextBox_Mes.Name = "richTextBox_Mes";
            this.richTextBox_Mes.ReadOnly = true;
            this.richTextBox_Mes.Size = new System.Drawing.Size(504, 256);
            this.richTextBox_Mes.TabIndex = 9;
            this.richTextBox_Mes.Text = "";
            // 
            // panel_ControlBox
            // 
            this.panel_ControlBox.Controls.Add(this.button_CopyNG);
            this.panel_ControlBox.Controls.Add(this.label_CurrTool);
            this.panel_ControlBox.Controls.Add(this.button_CopyOk);
            this.panel_ControlBox.Controls.Add(this.label_Info);
            this.panel_ControlBox.Controls.Add(this.label_CurrID);
            this.panel_ControlBox.Controls.Add(this.button_Clear);
            this.panel_ControlBox.Controls.Add(this.button_Stop);
            this.panel_ControlBox.Controls.Add(this.button_ActionTool);
            this.panel_ControlBox.Controls.Add(this.button_ActionAll);
            this.panel_ControlBox.Controls.Add(this.radioButton_OKorNG);
            this.panel_ControlBox.Controls.Add(this.radioButton_OKStop);
            this.panel_ControlBox.Controls.Add(this.radioButton_NgStop);
            this.panel_ControlBox.Enabled = false;
            this.panel_ControlBox.Location = new System.Drawing.Point(6, 46);
            this.panel_ControlBox.Name = "panel_ControlBox";
            this.panel_ControlBox.Size = new System.Drawing.Size(421, 233);
            this.panel_ControlBox.TabIndex = 1;
            // 
            // button_CopyNG
            // 
            this.button_CopyNG.Location = new System.Drawing.Point(239, 197);
            this.button_CopyNG.Name = "button_CopyNG";
            this.button_CopyNG.Size = new System.Drawing.Size(125, 30);
            this.button_CopyNG.TabIndex = 13;
            this.button_CopyNG.Text = "导出NG";
            this.button_CopyNG.UseVisualStyleBackColor = true;
            this.button_CopyNG.Click += new System.EventHandler(this.button_CopyNG_Click);
            // 
            // label_CurrTool
            // 
            this.label_CurrTool.AutoSize = true;
            this.label_CurrTool.Location = new System.Drawing.Point(12, 113);
            this.label_CurrTool.Name = "label_CurrTool";
            this.label_CurrTool.Size = new System.Drawing.Size(112, 27);
            this.label_CurrTool.TabIndex = 11;
            this.label_CurrTool.Text = "执行工具：";
            // 
            // button_CopyOk
            // 
            this.button_CopyOk.Location = new System.Drawing.Point(54, 197);
            this.button_CopyOk.Name = "button_CopyOk";
            this.button_CopyOk.Size = new System.Drawing.Size(125, 30);
            this.button_CopyOk.TabIndex = 12;
            this.button_CopyOk.Text = "导出OK";
            this.button_CopyOk.UseVisualStyleBackColor = true;
            this.button_CopyOk.Click += new System.EventHandler(this.button_CopyOk_Click);
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.Location = new System.Drawing.Point(12, 171);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(112, 27);
            this.label_Info.TabIndex = 10;
            this.label_Info.Text = "当前信息：";
            // 
            // label_CurrID
            // 
            this.label_CurrID.AutoSize = true;
            this.label_CurrID.Location = new System.Drawing.Point(12, 142);
            this.label_CurrID.Name = "label_CurrID";
            this.label_CurrID.Size = new System.Drawing.Size(112, 27);
            this.label_CurrID.TabIndex = 8;
            this.label_CurrID.Text = "当前执行：";
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(305, 62);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(79, 43);
            this.button_Clear.TabIndex = 6;
            this.button_Clear.Text = "重置";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Enabled = false;
            this.button_Stop.Location = new System.Drawing.Point(305, 13);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(79, 43);
            this.button_Stop.TabIndex = 5;
            this.button_Stop.Text = "暂停";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // button_ActionTool
            // 
            this.button_ActionTool.Location = new System.Drawing.Point(151, 62);
            this.button_ActionTool.Name = "button_ActionTool";
            this.button_ActionTool.Size = new System.Drawing.Size(148, 43);
            this.button_ActionTool.TabIndex = 4;
            this.button_ActionTool.Text = "执行（单工具）";
            this.button_ActionTool.UseVisualStyleBackColor = true;
            this.button_ActionTool.Click += new System.EventHandler(this.button_ActionTool_Click);
            // 
            // button_ActionAll
            // 
            this.button_ActionAll.Location = new System.Drawing.Point(151, 13);
            this.button_ActionAll.Name = "button_ActionAll";
            this.button_ActionAll.Size = new System.Drawing.Size(148, 43);
            this.button_ActionAll.TabIndex = 3;
            this.button_ActionAll.Text = "执行（所有工具）";
            this.button_ActionAll.UseVisualStyleBackColor = true;
            this.button_ActionAll.Click += new System.EventHandler(this.button_ActionAll_Click);
            // 
            // radioButton_OKorNG
            // 
            this.radioButton_OKorNG.AutoSize = true;
            this.radioButton_OKorNG.Location = new System.Drawing.Point(8, 75);
            this.radioButton_OKorNG.Name = "radioButton_OKorNG";
            this.radioButton_OKorNG.Size = new System.Drawing.Size(93, 31);
            this.radioButton_OKorNG.TabIndex = 2;
            this.radioButton_OKorNG.Text = "不停止";
            this.radioButton_OKorNG.UseVisualStyleBackColor = true;
            this.radioButton_OKorNG.CheckedChanged += new System.EventHandler(this.radioButton_OKorNG_CheckedChanged);
            // 
            // radioButton_OKStop
            // 
            this.radioButton_OKStop.AutoSize = true;
            this.radioButton_OKStop.Location = new System.Drawing.Point(8, 44);
            this.radioButton_OKStop.Name = "radioButton_OKStop";
            this.radioButton_OKStop.Size = new System.Drawing.Size(142, 31);
            this.radioButton_OKStop.TabIndex = 1;
            this.radioButton_OKStop.Text = "检测OK停止";
            this.radioButton_OKStop.UseVisualStyleBackColor = true;
            this.radioButton_OKStop.CheckedChanged += new System.EventHandler(this.radioButton_OKStop_CheckedChanged);
            // 
            // radioButton_NgStop
            // 
            this.radioButton_NgStop.AutoSize = true;
            this.radioButton_NgStop.Checked = true;
            this.radioButton_NgStop.Location = new System.Drawing.Point(8, 13);
            this.radioButton_NgStop.Name = "radioButton_NgStop";
            this.radioButton_NgStop.Size = new System.Drawing.Size(144, 31);
            this.radioButton_NgStop.TabIndex = 0;
            this.radioButton_NgStop.TabStop = true;
            this.radioButton_NgStop.Text = "检测NG停止";
            this.radioButton_NgStop.UseVisualStyleBackColor = true;
            this.radioButton_NgStop.CheckedChanged += new System.EventHandler(this.radioButton_NgStop_CheckedChanged);
            // 
            // checkBox_EnableBox
            // 
            this.checkBox_EnableBox.AutoSize = true;
            this.checkBox_EnableBox.Location = new System.Drawing.Point(6, 23);
            this.checkBox_EnableBox.Name = "checkBox_EnableBox";
            this.checkBox_EnableBox.Size = new System.Drawing.Size(74, 31);
            this.checkBox_EnableBox.TabIndex = 0;
            this.checkBox_EnableBox.Text = "启动";
            this.checkBox_EnableBox.UseVisualStyleBackColor = true;
            this.checkBox_EnableBox.CheckedChanged += new System.EventHandler(this.checkBox_EnableBox_CheckedChanged);
            // 
            // panel_ToolTree
            // 
            this.panel_ToolTree.Controls.Add(this.mTreeViewTools);
            this.panel_ToolTree.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.panel_ToolTree.Location = new System.Drawing.Point(4, 3);
            this.panel_ToolTree.Name = "panel_ToolTree";
            this.panel_ToolTree.Size = new System.Drawing.Size(133, 869);
            this.panel_ToolTree.TabIndex = 8;
            // 
            // mTreeViewTools
            // 
            this.mTreeViewTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTreeViewTools.Location = new System.Drawing.Point(0, 0);
            this.mTreeViewTools.Name = "mTreeViewTools";
            this.mTreeViewTools.Size = new System.Drawing.Size(133, 869);
            this.mTreeViewTools.TabIndex = 0;
            this.mTreeViewTools.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.mTreeViewTools_ItemDrag);
            this.mTreeViewTools.DragOver += new System.Windows.Forms.DragEventHandler(this.mTreeViewTools_DragOver);
            this.mTreeViewTools.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mTreeViewTools_MouseDoubleClick);
            // 
            // panel_WorkView
            // 
            this.panel_WorkView.Location = new System.Drawing.Point(140, 3);
            this.panel_WorkView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_WorkView.Name = "panel_WorkView";
            this.panel_WorkView.Size = new System.Drawing.Size(940, 577);
            this.panel_WorkView.TabIndex = 7;
            // 
            // panel_ToolButton
            // 
            this.panel_ToolButton.BackColor = System.Drawing.Color.White;
            this.panel_ToolButton.Controls.Add(this.label_ToolStep);
            this.panel_ToolButton.Controls.Add(this.label_TaskNum);
            this.panel_ToolButton.Controls.Add(this.toolStrip1);
            this.panel_ToolButton.Controls.Add(this.toolStrip4);
            this.panel_ToolButton.Controls.Add(this.panel1);
            this.panel_ToolButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ToolButton.Location = new System.Drawing.Point(3, 37);
            this.panel_ToolButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_ToolButton.Name = "panel_ToolButton";
            this.panel_ToolButton.Size = new System.Drawing.Size(1898, 67);
            this.panel_ToolButton.TabIndex = 1;
            // 
            // label_ToolStep
            // 
            this.label_ToolStep.AutoSize = true;
            this.label_ToolStep.Location = new System.Drawing.Point(515, 37);
            this.label_ToolStep.Name = "label_ToolStep";
            this.label_ToolStep.Size = new System.Drawing.Size(114, 20);
            this.label_ToolStep.TabIndex = 28;
            this.label_ToolStep.Text = "当前选定工具：";
            // 
            // label_TaskNum
            // 
            this.label_TaskNum.AutoSize = true;
            this.label_TaskNum.Location = new System.Drawing.Point(515, 11);
            this.label_TaskNum.Name = "label_TaskNum";
            this.label_TaskNum.Size = new System.Drawing.Size(84, 20);
            this.label_TaskNum.TabIndex = 27;
            this.label_TaskNum.Text = "当前任务：";
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.Color.White;
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.ImageScalingSize = new System.Drawing.Size(17, 17);
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_OpenFile,
            this.toolStripButton_Open,
            this.toolStripButton_UP,
            this.toolStripButton_Down});
            this.toolStrip4.Location = new System.Drawing.Point(1084, 4);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip4.Size = new System.Drawing.Size(227, 59);
            this.toolStrip4.TabIndex = 25;
            this.toolStrip4.Text = "toolStrip4";
            this.toolStrip4.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStrip4_Paint);
            // 
            // toolStripButton_OpenFile
            // 
            this.toolStripButton_OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_OpenFile.Image = global::WVision.Properties.Resources.open2;
            this.toolStripButton_OpenFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OpenFile.Name = "toolStripButton_OpenFile";
            this.toolStripButton_OpenFile.Size = new System.Drawing.Size(56, 56);
            this.toolStripButton_OpenFile.Text = "打开图片";
            this.toolStripButton_OpenFile.Click += new System.EventHandler(this.toolStripButton_OpenFile_Click);
            // 
            // toolStripButton_Open
            // 
            this.toolStripButton_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Open.Image = global::WVision.Properties.Resources.addfile;
            this.toolStripButton_Open.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Open.Name = "toolStripButton_Open";
            this.toolStripButton_Open.Size = new System.Drawing.Size(56, 56);
            this.toolStripButton_Open.Text = "选择文件夹";
            this.toolStripButton_Open.Click += new System.EventHandler(this.toolStripButton_Open_Click);
            // 
            // toolStripButton_UP
            // 
            this.toolStripButton_UP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_UP.Image = global::WVision.Properties.Resources.up2;
            this.toolStripButton_UP.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_UP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_UP.Name = "toolStripButton_UP";
            this.toolStripButton_UP.Size = new System.Drawing.Size(56, 56);
            this.toolStripButton_UP.Text = "上一张";
            this.toolStripButton_UP.Click += new System.EventHandler(this.toolStripButton_UP_Click);
            // 
            // toolStripButton_Down
            // 
            this.toolStripButton_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Down.Image = global::WVision.Properties.Resources.down2;
            this.toolStripButton_Down.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_Down.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Down.Name = "toolStripButton_Down";
            this.toolStripButton_Down.Size = new System.Drawing.Size(56, 56);
            this.toolStripButton_Down.Text = "下一张";
            this.toolStripButton_Down.Click += new System.EventHandler(this.toolStripButton_Down_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Task9);
            this.panel1.Controls.Add(this.button_Task8);
            this.panel1.Controls.Add(this.button_Task5);
            this.panel1.Controls.Add(this.button_Task7);
            this.panel1.Controls.Add(this.button_Task6);
            this.panel1.Controls.Add(this.button_Task4);
            this.panel1.Controls.Add(this.button_Task1);
            this.panel1.Controls.Add(this.button_Task3);
            this.panel1.Controls.Add(this.button_Task2);
            this.panel1.Location = new System.Drawing.Point(12, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 42);
            this.panel1.TabIndex = 21;
            // 
            // button_Task9
            // 
            this.button_Task9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task9.Location = new System.Drawing.Point(301, 7);
            this.button_Task9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task9.Name = "button_Task9";
            this.button_Task9.Size = new System.Drawing.Size(33, 33);
            this.button_Task9.TabIndex = 25;
            this.button_Task9.Text = "9";
            this.button_Task9.UseVisualStyleBackColor = true;
            this.button_Task9.Visible = false;
            this.button_Task9.Click += new System.EventHandler(this.button_Task9_Click);
            // 
            // button_Task8
            // 
            this.button_Task8.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task8.Location = new System.Drawing.Point(264, 7);
            this.button_Task8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task8.Name = "button_Task8";
            this.button_Task8.Size = new System.Drawing.Size(33, 33);
            this.button_Task8.TabIndex = 24;
            this.button_Task8.Text = "8";
            this.button_Task8.UseVisualStyleBackColor = true;
            this.button_Task8.Visible = false;
            this.button_Task8.Click += new System.EventHandler(this.button_Task8_Click);
            // 
            // button_Task5
            // 
            this.button_Task5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task5.Location = new System.Drawing.Point(153, 7);
            this.button_Task5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task5.Name = "button_Task5";
            this.button_Task5.Size = new System.Drawing.Size(33, 33);
            this.button_Task5.TabIndex = 21;
            this.button_Task5.Text = "5";
            this.button_Task5.UseVisualStyleBackColor = true;
            this.button_Task5.Visible = false;
            this.button_Task5.Click += new System.EventHandler(this.button_Task5_Click);
            // 
            // button_Task7
            // 
            this.button_Task7.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task7.Location = new System.Drawing.Point(227, 7);
            this.button_Task7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task7.Name = "button_Task7";
            this.button_Task7.Size = new System.Drawing.Size(33, 33);
            this.button_Task7.TabIndex = 23;
            this.button_Task7.Text = "7";
            this.button_Task7.UseVisualStyleBackColor = true;
            this.button_Task7.Visible = false;
            this.button_Task7.Click += new System.EventHandler(this.button_Task7_Click);
            // 
            // button_Task6
            // 
            this.button_Task6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task6.Location = new System.Drawing.Point(190, 7);
            this.button_Task6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task6.Name = "button_Task6";
            this.button_Task6.Size = new System.Drawing.Size(33, 33);
            this.button_Task6.TabIndex = 22;
            this.button_Task6.Text = "6";
            this.button_Task6.UseVisualStyleBackColor = true;
            this.button_Task6.Visible = false;
            this.button_Task6.Click += new System.EventHandler(this.button_Task6_Click);
            // 
            // button_Task4
            // 
            this.button_Task4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task4.Location = new System.Drawing.Point(116, 7);
            this.button_Task4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task4.Name = "button_Task4";
            this.button_Task4.Size = new System.Drawing.Size(33, 33);
            this.button_Task4.TabIndex = 20;
            this.button_Task4.Text = "4";
            this.button_Task4.UseVisualStyleBackColor = true;
            this.button_Task4.Click += new System.EventHandler(this.button_Task4_Click);
            // 
            // button_Task1
            // 
            this.button_Task1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task1.Location = new System.Drawing.Point(5, 7);
            this.button_Task1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task1.Name = "button_Task1";
            this.button_Task1.Size = new System.Drawing.Size(33, 33);
            this.button_Task1.TabIndex = 17;
            this.button_Task1.Text = "1";
            this.button_Task1.UseVisualStyleBackColor = true;
            this.button_Task1.Click += new System.EventHandler(this.button_Task1_Click);
            // 
            // button_Task3
            // 
            this.button_Task3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task3.Location = new System.Drawing.Point(79, 7);
            this.button_Task3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task3.Name = "button_Task3";
            this.button_Task3.Size = new System.Drawing.Size(33, 33);
            this.button_Task3.TabIndex = 19;
            this.button_Task3.Text = "3";
            this.button_Task3.UseVisualStyleBackColor = true;
            this.button_Task3.Click += new System.EventHandler(this.button_Task3_Click);
            // 
            // button_Task2
            // 
            this.button_Task2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Task2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_Task2.Location = new System.Drawing.Point(42, 7);
            this.button_Task2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Task2.Name = "button_Task2";
            this.button_Task2.Size = new System.Drawing.Size(33, 34);
            this.button_Task2.TabIndex = 18;
            this.button_Task2.Text = "2";
            this.button_Task2.UseVisualStyleBackColor = true;
            this.button_Task2.Click += new System.EventHandler(this.button_Task2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_CurrProject);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1898, 29);
            this.panel2.TabIndex = 2;
            // 
            // label_CurrProject
            // 
            this.label_CurrProject.AutoSize = true;
            this.label_CurrProject.BackColor = System.Drawing.Color.OldLace;
            this.label_CurrProject.Font = new System.Drawing.Font("楷体", 16F);
            this.label_CurrProject.Location = new System.Drawing.Point(906, 4);
            this.label_CurrProject.Name = "label_CurrProject";
            this.label_CurrProject.Size = new System.Drawing.Size(152, 27);
            this.label_CurrProject.TabIndex = 29;
            this.label_CurrProject.Text = "当前项目：";
            this.label_CurrProject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.OldLace;
            this.label1.Font = new System.Drawing.Font("楷体", 18F);
            this.label1.Location = new System.Drawing.Point(467, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "算法调参工具";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_show
            // 
            this.timer_show.Enabled = true;
            // 
            // FrmProjectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1904, 1011);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel_MainBody);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(700, 0);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmProjectSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "算法调整";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProjectSet_FormClosing);
            this.Load += new System.EventHandler(this.FrmProjectSet_Load);
            this.Shown += new System.EventHandler(this.FrmProjectSet_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel_MainBody.ResumeLayout(false);
            this.tableLayoutPanel_Body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ToolList)).EndInit();
            this.contextMenuStrip_Tools.ResumeLayout(false);
            this.groupBox_ToolForm.ResumeLayout(false);
            this.panel_body.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_ControlBox.ResumeLayout(false);
            this.panel_ControlBox.PerformLayout();
            this.panel_ToolTree.ResumeLayout(false);
            this.panel_ToolButton.ResumeLayout(false);
            this.panel_ToolButton.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripButton toolStripButton_UP;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_ScaleImageSize;
        private System.Windows.Forms.ToolStripButton toolStripButton_SaveParam;
        private System.Windows.Forms.ToolStripButton toolStripButton_Down;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_MainBody;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.GroupBox groupBox_ToolForm;
        private System.Windows.Forms.Panel panel_ToolForm;
        private System.Windows.Forms.Panel panel_body;
        private System.Windows.Forms.Panel panel_WorkView;
        private System.Windows.Forms.Panel panel_ToolButton;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton toolStripButton_Open;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Task5;
        private System.Windows.Forms.Button button_Task6;
        private System.Windows.Forms.Button button_Task4;
        private System.Windows.Forms.Button button_Task1;
        private System.Windows.Forms.Button button_Task3;
        private System.Windows.Forms.Button button_Task2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer_show;
        private System.Windows.Forms.Panel panel_ToolTree;
        private System.Windows.Forms.TreeView mTreeViewTools;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Tools;
        private System.Windows.Forms.ToolStripMenuItem 删除工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上移工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移工具ToolStripMenuItem;
        private System.Windows.Forms.Label label_TaskNum;
        private System.Windows.Forms.ToolStripButton toolStripButton_OpenFile;
        private System.Windows.Forms.ToolStripButton toolStripButton_Close;
        private System.Windows.Forms.Label label_CurrProject;
        private System.Windows.Forms.DataGridView dataGridView_ToolList;
        private System.Windows.Forms.ToolStripButton toolStripButton_CamStart;
        private System.Windows.Forms.ToolStripButton toolStripButton_CamStop;
        private System.Windows.Forms.Label label_ToolStep;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_EnableBox;
        private System.Windows.Forms.Panel panel_ControlBox;
        private System.Windows.Forms.RadioButton radioButton_OKorNG;
        private System.Windows.Forms.RadioButton radioButton_OKStop;
        private System.Windows.Forms.RadioButton radioButton_NgStop;
        private System.Windows.Forms.Button button_ActionAll;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Label label_CurrID;
        private System.Windows.Forms.RichTextBox richTextBox_Mes;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Label label_CurrTool;
        private System.Windows.Forms.Button button_ActionTool;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Index;
        private System.Windows.Forms.DataGridViewImageColumn Column_Status;
        private System.Windows.Forms.DataGridViewButtonColumn Column_ToolName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_CostTime;
        private System.Windows.Forms.Button button_CopyNG;
        private System.Windows.Forms.Button button_CopyOk;
        private System.Windows.Forms.Button button_Task9;
        private System.Windows.Forms.Button button_Task8;
        private System.Windows.Forms.Button button_Task7;
    }
}