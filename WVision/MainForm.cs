using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Sunny.UI;
using WControls;
using HalconDotNet;
using WCommonTools;
using WTools;
using System.IO;

namespace WVision
{
    public partial class MainForm : UIForm
    {
        public MainForm()
        {
            InitializeComponent();
            mRunMessageWindow = new UMessageListView();
            mRunMessageWindow.Size = panel_MessageBox.Size;
            mRunMessageWindow.Parent = panel_MessageBox;
        }

        Machine mMachine;
        UMessageListView mRunMessageWindow;
        DataTable info;

        bool mThreadRun;
        bool mIsStart;

        private void MainForm_Load(object sender, EventArgs e)
        {
            FrmStartLoad load = new FrmStartLoad();
            load.StartPosition = FormStartPosition.CenterScreen;
            load.Show();
            load.Refresh();

            HOperatorSet.SetSystem("clip_region", "false");
            mMachine = Machine.GetInstance();
            mMachine.Waiting = new FrmWait();
            mMachine.Waiting.Show();
            mMachine.Waiting.Visible = false;
            load.SetValue(5, "配置文件读取中...");
            //初始化配置文件
            SettingInfo info;
            if (mMachine.DeSerializeFuc(mMachine.SettingInfoSavePath, out info))
            {
                mMachine.SettingInfo = info;
                mMachine.ProjectInfoList = info.ProjectInfoList;
                mMachine.CurrProjectInfo = info.SelectProject;
            }
            else
            {
                MessageBox.Show("配置文件读取失败!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                info = new SettingInfo();
                mMachine.SettingInfo = info;
            }
            int vae = 10;
            DahengCamera.QueryCamera(out List<CameraInfo> lit);
            //初始化相机
            for (int i = 0; i < mMachine.SettingInfo.CameraInfoList.Count; i++)
            {
                vae += 5;
                DahengCamera cam = new DahengCamera();
                load.SetValue(vae, "相机 " + mMachine.SettingInfo.CameraInfoList[i] + " 打开中");;
                cam.OpenCamera(mMachine.SettingInfo.CameraInfoList[i]);
                if (cam.OpenCamera(mMachine.SettingInfo.CameraInfoList[i]) && mMachine.SettingInfo.CameraInfoList[i] != "")
                {
                    mMachine.CamList.Add(cam);
                }

                else
                {
                    if (mMachine.SettingInfo.CameraInfoList[i] != "")
                        MessageBox.Show("相机序列号为[" + mMachine.SettingInfo.CameraInfoList[i] + "]的相机打开失败!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mMachine.CamList.Add(null);
                }
            }
            //初始化ModbusTCP
            load.SetValue(75, "Modbus TCP链接中...");
            mMachine.Modbus_Tcp.Address = info.ModbusIP;
            mMachine.Modbus_Tcp.Port = info.ModbusPort;
            if (!mMachine.Modbus_Tcp.IsModbusTcpOpen())
                MessageBox.Show("ModbusTcp连接失败，IP：" + info.ModbusIP, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //初始化任务
            load.SetValue(85, "任务初始化中...");
            load.Refresh();
            InitTask(mMachine.CamList.Count);
            //初始化任务窗口
            load.SetValue(90, "窗口初始化中...");
            //InitImageShowWindow(mMachine.CamList.Count);
            InitCameraWindow(mMachine.CamList.Count);
            InitDataGridView(mMachine.CamList.Count);
            InitTaskWindow();
            timer_Refresh.Start();
            //
            this.Refresh();
            load.SetValue(100, "加载完成");
            load.Close();
        }

        private void InitImageShowWindow(int num)
        {
            mMachine.HWindowControlList = new List<HDebugWindow>();
            for (int i = 0; i < num; i++)
            {
                HDebugWindow window = new HDebugWindow();
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);
            }
        }
        private void InitDataGridView(int num)
        {
            info = new DataTable();
            info.Columns.Add("相机序号", typeof(string));
            info.Columns.Add("拍照次数", typeof(string));
            info.Columns.Add("产品个数", typeof(string));
            info.Columns.Add("良品总数", typeof(string));
            info.Columns.Add("不良总数", typeof(string));
            info.Columns.Add("良品比例", typeof(string));
            info.Columns.Add("检测耗时", typeof(string));
            dataGridView_LineList.DataSource = info;
            dataGridView_LineList.AllowUserToAddRows = false;//禁止添加行
            dataGridView_LineList.AllowUserToResizeRows = false;//禁止调整行高
            dataGridView_LineList.AllowUserToDeleteRows = false;//禁止删除行
            dataGridView_LineList.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 11, FontStyle.Regular);
            dataGridView_LineList.RowsDefaultCellStyle.Font = new Font("微软雅黑", 11, FontStyle.Regular);
            dataGridView_LineList.RowTemplate.Height = 26;
            dataGridView_LineList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //剔除第一列
            dataGridView_LineList.RowHeadersVisible = false;
            dataGridView_LineList.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_LineList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView_LineList.Columns[0].Width = 80;

            for (int i = 0; i < dataGridView_LineList.Columns.Count; i++)
            {
                dataGridView_LineList.Columns[i].ReadOnly = true;
                dataGridView_LineList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView_LineList.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_LineList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView_LineList.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView_LineList.DefaultCellStyle.SelectionForeColor = Color.FromArgb(48, 48, 48);
            dataGridView_LineList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView_LineList.Rows.Add(mMachine.TaskList.Count);
            //dataGridView_LineList.Rows[0].Selected = false;

            for (int i = 0; i < num; i++)
            {
                object[] obj = new object[7];
                obj[0] = "Cam" + (i + 1);
                obj[1] = "0";
                obj[2] = "0";
                obj[3] = "0";
                obj[4] = "0";
                obj[5] = "NA";
                obj[6] = "00.00ms";
                info.Rows.Add(obj);
            }
            if (dataGridView_LineList.Rows.Count > 0)
                dataGridView_LineList.Rows[0].Selected = false;
        }

        private void UiHeaderButton_CameraSet_Click(object sender, EventArgs e)
        {
            FrmSelectProject se = new FrmSelectProject();
            if (se.ShowDialog() == DialogResult.OK)
            {
                FrmCamListView frm = new FrmCamListView();
                frm.CurrProject = se.CurrProject;
                frm.ShowDialog();
            }

            uiHeaderButton_CameraSet.Selected = false;
        }

        private void UiHeaderButton_Project_Click(object sender, EventArgs e)
        {
            FrmProjectManager fem = new FrmProjectManager(this);
            fem.ShowDialog();
            uiHeaderButton_Project.Selected = false;
        }

        private void UiHeaderButton_UserManage_Click(object sender, EventArgs e)
        {
            FrmUsers frm = new FrmUsers();
            frm.ShowDialog();
            uiHeaderButton_UserManage.Selected = false;
        }

        private void UiHeaderButton_Data_Click(object sender, EventArgs e)
        {

        }

        private void UiHeaderButton_CommunicateSet_Click(object sender, EventArgs e)
        {
            FrmCommunicate frm = new FrmCommunicate();
            frm.ShowDialog();
            uiHeaderButton_CommunicateSet.Selected = false;
        }

        private void UiHeaderButton_ProjectSet_Click(object sender, EventArgs e)
        {
            FrmSelectProject se = new FrmSelectProject();
            if (se.ShowDialog() == DialogResult.OK)
            {
                Machine.GetInstance().FrmProject = new FrmProjectSet();
                Machine.GetInstance().FrmProject.CurrProject = se.CurrProject;
                Machine.GetInstance().FrmProject.ShowDialog();
            }
            uiHeaderButton_ProjectSet.Selected = false;
        }

        private void InitTask(int num)
        {
            mMachine.CreateCheckTask(num);
        }

        private void InitTaskWindow()
        {
            for (int i = 0; i < mMachine.CamList.Count; i++)
            {
                mMachine.TaskList[i].Camera = mMachine.CamList[i];
                mMachine.TaskList[i].ToolWind = mMachine.HWindowControlList[i];

            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UIFormServiceHelper.ShowProcessForm(this);
            //释放线程资源
            foreach (ProjectTaskBase item in mMachine.TaskList)
            {
                foreach (ToolBase item1 in item.ToolList)
                {
                    item1.Dispose();
                }
            }
            timer_Refresh.Stop();
            foreach (var item in mMachine.CamList)
            {
                if (item != null) 
                {
                    item.StopGrab();
                    item.CloseCamera();
                }
            }
            UIFormServiceHelper.HideProcessForm(this);
            mIsStart = false;
            mThreadRun = false;
        }

        private void UiHeaderButton_SystemSet_Click(object sender, EventArgs e)
        {
            FrmSystemSetting frm = new FrmSystemSetting();
            frm.ShowDialog();
            uiHeaderButton_SystemSet.Selected = false;
        }

        //开始检测页面
        private void UiHeaderButton_Run_Click(object sender, EventArgs e)
        {
            try
            {
                UIFormServiceHelper.ShowProcessForm(this);
                string path = Application.StartupPath + "\\Project\\" + mMachine.SettingInfo.SelectProject.mProjectName + "\\CamPar.cam";

                if (!mMachine.GetRemainMemeory(Path.GetPathRoot(mMachine.SettingInfo.SaveImagePath))) 
                {
                    UIFormServiceHelper.HideProcessForm(this);
                    MessageBox.Show("硬盘剩余容量过低!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //设置相机
                //设置曝光参数
                CamParam infoCam;
                if (Machine.GetInstance().DeSoapSerializeFuc(path, out infoCam))
                {
                    mMachine.CameraParam = infoCam;
                    for (int i = 0; i < mMachine.CamList.Count; i++)
                    {
                        if (mMachine.CamList[i] != null)
                        {
                            CamParamInfo info;
                            Machine.GetInstance().CameraParam.GetParamValue(i, out info);
                            mMachine.CamList[i].SetCameraExposure(info.CameraExposure);
                            mMachine.CamList[i].SetCameraGain((float)info.CameraGain);
                            mMachine.CamList[i].SetTriggerDelay(info.TriggerDelay);
                        }
                    }
                }
                timer_Plc.Start();
                //清除所有数据/清除相机图像队列
                ClearTaskCount();

                //读取项目文件
                if (mMachine.ReadCheckTask(mMachine.CurrProjectInfo.mProjectName) != 0)
                {
                    UIFormServiceHelper.HideProcessForm(this);
                    MessageBox.Show(new Form { TopMost = true }, "项目文件读取失败，请调试");
                    this.Enabled = true;
                    return;
                }

                //模型进行预热
                for (int i = 0; i < mMachine.TaskList.Count; i++)
                {
                    //若工具列表有工具
                    if (mMachine.TaskList[i].ToolList.Any(m => 
                    m.ToolParam.ToolType == ToolType.HsemanticAI || 
                    m.ToolParam.ToolType == ToolType.DeepOcr||
                    m.ToolParam.ToolType == ToolType.HClassifiyAI||
                    m.ToolParam.ToolType == ToolType.HObjecDetect1))
                    {
                        HOperatorSet.ReadImage(out HObject image, Application.StartupPath + "\\AppConfig\\预热\\1.png");
                        foreach (var item in mMachine.TaskList[i].ToolList)
                        {
                            item.ToolRun(image, mMachine.TaskList[i].StepInfoList, false, out JumpInfo info);
                        }
                    }
                }


                //设置所有相机工作方式
                for (int i = 0; i < mMachine.CamList.Count; i++)
                {
                    mMachine.CamList[i].SetCameraMode(TriggerMode.Mode_Trigger);
                    mMachine.CamList[i].SetTriggerSource(TriggerSource.Line0);
                }


                //Task文件夹赋值
                string mFoldNameMain = System.DateTime.Now.ToString("yyyy-MM-dd");
                string mFoldName = System.DateTime.Now.ToString("HH-mm-sss");
                //启动所有线程
                foreach (var item in mMachine.TaskList)
                {
                    item.SaveFolderName = mFoldNameMain + "\\" + mFoldName;
                    item.Camera.StartGrab();
                    item.IsStart = true;
                }
                //使能控件
                EnableControl(false);
                uiHeaderButton_Run.Enabled = false;
                uiHeaderButton_Stop.Enabled = true;
                uiHeaderButton_Run.Selected = false;
                UIFormServiceHelper.HideProcessForm(this);
            }
            catch (Exception ex)
            {
                uiHeaderButton_Run.Selected = false;
                //启动所有线程
                foreach (var item in mMachine.TaskList)
                {
                    item.Camera.StopGrab();
                    item.IsStart = false;
                }            
                //使能控件
                timer_Plc.Stop();
                EnableControl(true);
                uiHeaderButton_Run.Enabled = true;
                uiHeaderButton_Stop.Enabled = false;
                uiHeaderButton_Stop.Selected = false;
                UIFormServiceHelper.HideProcessForm(this);
                MessageBox.Show(ex.Message);
            }
        }

        private void EnableControl(bool misEnable)
        {
            panel_BtnList.Enabled = misEnable;
            //uiTitlePanel1.Enabled = misEnable;
            //uiTitlePanel2.Enabled = misEnable;
            uiTitlePanel3.Enabled = misEnable;
            uiTitlePanel1.Enabled = misEnable;
            this.ControlBox = misEnable;
        }

        private void UiHeaderButton_Stop_Click(object sender, EventArgs e)
        {
            UIFormServiceHelper.ShowProcessForm(this);
            foreach (var item in mMachine.TaskList)
            {
                item.Camera.StopGrab();
                item.IsStart = false;
            }
            foreach (var item in mMachine.TaskList)
            {
                foreach (var item1 in item.ToolList)
                {
                    item1.Dispose();
                }
            }
            mIsStart = false;
            //使能控件
            timer_Plc.Stop();
            EnableControl(true);
            uiHeaderButton_Run.Enabled = true;
            uiHeaderButton_Stop.Enabled = false;
            uiHeaderButton_Stop.Selected = false;
            UIFormServiceHelper.HideProcessForm(this);
        }

        private void ClearTaskCount()
        {
            for (int i = 0; i < mMachine.TaskList.Count; i++)
            {
                mMachine.TaskList[i].Count = 0;
                mMachine.TaskList[i].CostTime = 0;
                mMachine.TaskList[i].OkCount = 0;
                mMachine.TaskList[i].NgCount = 0;
                mMachine.TaskList[i].TaskResultQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
                if (mMachine.TaskList[i].Camera != null)
                {
                    mMachine.TaskList[i].Camera.ClearQueue();
                    mMachine.TaskList[i].Camera.ImageCount = 0;
                }

            }
        }

        private void GetFinalResult()
        {
            while (mThreadRun)
            {
                if (mMachine.TaskList.Count != 6)
                {
                    Thread.Sleep(50);
                    continue;
                }
                if (!mIsStart)
                {
                    Thread.Sleep(50);
                    continue;
                }

                if (mMachine.TaskList[1].TaskResultQueue.Count() > 0 &&
                    mMachine.TaskList[2].TaskResultQueue.Count() > 0 &&
                    mMachine.TaskList[3].TaskResultQueue.Count() > 0 &&
                    mMachine.TaskList[4].TaskResultQueue.Count() > 0)
                {
                    List<int> mCountList = new List<int>();
                    List<int> mResultList = new List<int>();

                    mMachine.TaskList[1].TaskResultQueue.TryDequeue(out string res1);
                    mMachine.TaskList[2].TaskResultQueue.TryDequeue(out string res2);
                    mMachine.TaskList[3].TaskResultQueue.TryDequeue(out string res3);
                    mMachine.TaskList[4].TaskResultQueue.TryDequeue(out string res4);

                    mCountList.Add(int.Parse(res1.Split('_')[0]));
                    mCountList.Add(int.Parse(res2.Split('_')[0]));
                    mCountList.Add(int.Parse(res3.Split('_')[0]));
                    mCountList.Add(int.Parse(res4.Split('_')[0]));

                    mResultList.Add(int.Parse(res1.Split('_')[2]));
                    mResultList.Add(int.Parse(res2.Split('_')[2]));
                    mResultList.Add(int.Parse(res3.Split('_')[2]));
                    mResultList.Add(int.Parse(res4.Split('_')[2]));

                    int res = 1;
                    //OK
                    if (mResultList.All(i => i == 0))
                    {
                        res = 0;
                        break;
                    }
                    else if (mResultList.Contains(-1))
                    {
                        res = -1;
                        break;
                    }
                    //-199 反料或无料
                    else if (mResultList.Contains(-199))
                    {
                        res = -199;
                        break;
                    }
                    else
                    {
                        res = 1;
                    }

                    if (res == 0)
                    {
                        mMachine.OkNumber++;
                        mMachine.AllNumber++;
                    }
                    else
                    {
                        mMachine.NGNumber++;
                        mMachine.AllNumber++;
                    }
                }
            }
        }

        private void Timer_Refresh_Tick(object sender, EventArgs e)
        {
            //float pos = Machine.GetInstance().Zmotion.GetFbkPosition(0);

            label_Project.Text = "Project：" + mMachine.SettingInfo.SelectProject.mProjectName;
            label_All.Text = "总数：" + mMachine.TaskList[0].Count;
            if (mMachine.AllNumber > 0)
                label_Percent.Text = "良品率：" + (mMachine.TaskList[0].OkCount / mMachine.TaskList[0].Count).ToString("P");
            else
                label_Percent.Text = "良品率：" + (0).ToString("P");
            label5_OK.Text = "良品数：" + mMachine.TaskList[0].OkCount;
            label_NG.Text = "不良数：" + mMachine.TaskList[0].NgCount;





            this.BeginInvoke(new Action(() =>
            {
                for (int i = 0; i < mMachine.TaskList.Count; i++)
                {
                    if (mMachine.TaskList[i].Camera != null)
                    {
                        dataGridView_LineList.Rows[i].Cells[0].Value = "Cam" + (i + 1);
                        dataGridView_LineList.Rows[i].Cells[1].Value = mMachine.TaskList[i].Camera.ImageCount;
                        dataGridView_LineList.Rows[i].Cells[2].Value = mMachine.TaskList[i].Count;
                        dataGridView_LineList.Rows[i].Cells[3].Value = mMachine.TaskList[i].OkCount;
                        dataGridView_LineList.Rows[i].Cells[4].Value = mMachine.TaskList[i].NgCount;
                        if (mMachine.TaskList[i].Count > 0)
                            dataGridView_LineList.Rows[i].Cells[5].Value = (mMachine.TaskList[i].OkCount / mMachine.TaskList[i].Count).ToString("f2");
                        else
                            dataGridView_LineList.Rows[i].Cells[5].Value = "NA";
                        dataGridView_LineList.Rows[i].Cells[6].Value = mMachine.TaskList[i].CostTime.ToString("f2") + "ms";
                    }
                }

            }));
        }

        private void timer_Plc_Tick(object sender, EventArgs e)
        {
            mMachine.Modbus_Tcp.WriteSingleRegister(0, 6150, 1);
        }


        public void InitCameraWindow(int windowNums) 
        {
            int panelWidth = Panel_Camera.Width;
            int panelHeight = Panel_Camera.Height;

            if (windowNums == 1)
            {
                HDebugWindow window = new HDebugWindow();
                window.WindowName = "Camera 1";
                window.Width = panelWidth - 6;
                window.Height = panelHeight - 6;
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);
            }
            else if (windowNums == 2) 
            {
                int childWidth = (panelWidth - 9) / 2;
                int childHeight = (panelWidth - 9) / 2;

                HDebugWindow window = new HDebugWindow();
                window.WindowName = "Camera 1";
                window.Width = childWidth;
                window.Height = childHeight;
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);

                HDebugWindow window1 = new HDebugWindow();
                window1.WindowName = "Camera 2";
                window1.Width = childWidth;
                window1.Height = childHeight;
                this.Panel_Camera.Controls.Add(window1);
                mMachine.HWindowControlList.Add(window1);
            }
            else if (windowNums == 3)
            {
                int childWidth = (panelWidth - 18) / 2;
                int childHeight = (panelHeight - 18) / 2;

                HDebugWindow window = new HDebugWindow();
                window.WindowName = "Camera 1";
                window.Width = childWidth;
                window.Height = childHeight;
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);

                HDebugWindow window1 = new HDebugWindow();
                window1.WindowName = "Camera 2";
                window1.Width = childWidth;
                window1.Height = childHeight;
                this.Panel_Camera.Controls.Add(window1);
                mMachine.HWindowControlList.Add(window1);

                HDebugWindow window2 = new HDebugWindow();
                window2.WindowName = "Camera 3";
                window2.Width = childWidth;
                window2.Height = childHeight;
                this.Panel_Camera.Controls.Add(window2);
                mMachine.HWindowControlList.Add(window2);
            }
            else if (windowNums == 4)
            {
                int childWidth = (panelWidth - 18) / 2;
                int childHeight = (panelHeight - 18) / 2;

                HDebugWindow window = new HDebugWindow();
                window.WindowName = "Camera 1";
                window.Width = childWidth;
                window.Height = childHeight;
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);

                HDebugWindow window1 = new HDebugWindow();
                window1.WindowName = "Camera 2";
                window1.Width = childWidth;
                window1.Height = childHeight;
                this.Panel_Camera.Controls.Add(window1);
                mMachine.HWindowControlList.Add(window1);

                HDebugWindow window2 = new HDebugWindow();
                window2.WindowName = "Camera 3";
                window2.Width = childWidth;
                window2.Height = childHeight;
                this.Panel_Camera.Controls.Add(window2);
                mMachine.HWindowControlList.Add(window2);

                HDebugWindow window3 = new HDebugWindow();
                window3.WindowName = "Camera 4";
                window3.Width = childWidth;
                window3.Height = childHeight;
                this.Panel_Camera.Controls.Add(window3);
                mMachine.HWindowControlList.Add(window3);
            }

            else if (windowNums == 5)
            {
                int childWidth = (panelWidth - 18) / 3;
                int childHeight = (panelHeight - 12) / 2;

                HDebugWindow window = new HDebugWindow();
                window.WindowName = "Camera 1";
                window.Width = childWidth;
                window.Height = childHeight;
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);

                HDebugWindow window1 = new HDebugWindow();
                window1.WindowName = "Camera 2";
                window1.Width = childWidth;
                window1.Height = childHeight;
                this.Panel_Camera.Controls.Add(window1);
                mMachine.HWindowControlList.Add(window1);

                HDebugWindow window2 = new HDebugWindow();
                window2.WindowName = "Camera 3";
                window2.Width = childWidth;
                window2.Height = childHeight;
                this.Panel_Camera.Controls.Add(window2);
                mMachine.HWindowControlList.Add(window2);

                HDebugWindow window3 = new HDebugWindow();
                window3.WindowName = "Camera 4";
                window3.Width = childWidth;
                window3.Height = childHeight;
                this.Panel_Camera.Controls.Add(window3);
                mMachine.HWindowControlList.Add(window3);

                HDebugWindow window4 = new HDebugWindow();
                window4.WindowName = "Camera 5";
                window4.Width = childWidth;
                window4.Height = childHeight;
                this.Panel_Camera.Controls.Add(window4);
                mMachine.HWindowControlList.Add(window4);
            }
            else if (windowNums == 6)
            {
                int childWidth = (panelWidth - 18) / 3;
                int childHeight = (panelHeight - 12) / 2;

                HDebugWindow window = new HDebugWindow();
                window.WindowName = "Camera 1";
                window.Width = childWidth;
                window.Height = childHeight;
                this.Panel_Camera.Controls.Add(window);
                mMachine.HWindowControlList.Add(window);

                HDebugWindow window1 = new HDebugWindow();
                window1.WindowName = "Camera 2";
                window1.Width = childWidth;
                window1.Height = childHeight;
                this.Panel_Camera.Controls.Add(window1);
                mMachine.HWindowControlList.Add(window1);

                HDebugWindow window2 = new HDebugWindow();
                window2.WindowName = "Camera 3";
                window2.Width = childWidth;
                window2.Height = childHeight;
                this.Panel_Camera.Controls.Add(window2);
                mMachine.HWindowControlList.Add(window2);

                HDebugWindow window3 = new HDebugWindow();
                window3.WindowName = "Camera 4";
                window3.Width = childWidth;
                window3.Height = childHeight;
                this.Panel_Camera.Controls.Add(window3);
                mMachine.HWindowControlList.Add(window3);

                HDebugWindow window4 = new HDebugWindow();
                window4.WindowName = "Camera 5";
                window4.Width = childWidth;
                window4.Height = childHeight;
                this.Panel_Camera.Controls.Add(window4);
                mMachine.HWindowControlList.Add(window4);

                HDebugWindow window5 = new HDebugWindow();
                window5.WindowName = "Camera 6";
                window5.Width = childWidth;
                window5.Height = childHeight;
                this.Panel_Camera.Controls.Add(window5);
                mMachine.HWindowControlList.Add(window5);
            }
        }
    }
}
