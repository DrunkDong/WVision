using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WTools;
using HalconDotNet;
using WCommonTools;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using Sunny.UI;
using System.Windows.Shapes;

namespace WVision
{
    public partial class FrmProjectSet : Form
    {
        public FrmProjectSet()
        {
            InitializeComponent();
        }

        Machine mMachine;
        ToolMachine mToolMachine;
        HTuple mShowWind;
        HWindow mDrawWind;
        HWindowControl mShowControl;
        string mCurrDragItemName;
        List<StepInfo> mStepInfoList;
        List<ToolBase> mToolList;
        List<UserControl> mToolPageList;
        List<string> mPathList;
        int mPicIndex = 0;
        bool loock1;
        double RowDown;//鼠标按下时的行坐标
        double ColDown;//鼠标按下时的列坐标
        int mCurrTaskIndex;
        int mMouseDown;

        ToolBase toolScript;

        DahengCamera mCurrCamera;
        bool mIsRun;
        bool mStartTrigger;
        Thread mDebugThread;
        List<HObject> mImageList;
        int mCurrStepIndex;

        int mRunMode;
        int mCurrTestIndex;
        int mAllTestNum;
        int mTestNG;
        int mTestOK;
        bool mTestRun;
        bool mTestAllRun;
        ManualResetEvent manual;
        Thread mToolRunThread;
        List<string> mToolNameList;

        bool mIsRunning;

        List<string> OkPath;
        List<string> NGPath;

        ToolStripMenuItem item1;
        ToolStripMenuItem item2;
        ProjectInfo mCurrProject;
        public ProjectInfo CurrProject
        {
            get => mCurrProject;
            set => mCurrProject = value;
        }

        public string SelectProjectName;
        private void FrmProjectSet_Load(object sender, EventArgs e)
        {


        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mTreeViewTools_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                TreeNode dragNode = e.Item as TreeNode;
                DoDragDrop(dragNode, DragDropEffects.Move);
            }
        }

        private void dataGridView_ToolList_DragEnter(object sender, DragEventArgs e)
        {
            //只接受树节点的拖放
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void dataGridView_ToolList_DragDrop(object sender, DragEventArgs e)
        {
            ToolOperation op = ToolOperation.GetInstance();
            ToolType tool = op.GetToolNameFromStr(mCurrDragItemName);
            if (tool != ToolType.None)
            {
                GetNewToolFromList(tool);
                UpdataToolView();
            }
        }

        private void mTreeViewTools_DragOver(object sender, DragEventArgs e)
        {
            TreeView tv = sender as TreeView;
            tv.SelectedNode = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
            if (tv.SelectedNode != null)
            {
                mCurrDragItemName = tv.SelectedNode.Text;
            }
        }
        //更新工具链表显示(重新排序)
        private void UpdataToolView()
        {
            //更新工作流程顺序
            dataGridView_ToolList.Rows.Clear();
            object[] obj;

            for (int i = 0; i < mToolList.Count; i++)
            {
                mToolList[i].ToolParam.StepInfo.mStepIndex = i + 1;
                obj = new object[4];
                obj[0] = mToolList[i].ToolParam.StepInfo.mStepIndex.ToString();
                obj[1] = Properties.Resources.OK;
                obj[2] = mToolList[i].ToolParam.StepInfo.mShowName;
                obj[3] = "0.0";
                dataGridView_ToolList.Rows.Add(obj);
            }
            if (mToolList.Count > 0)
            {
                dataGridView_ToolList.Rows[0].Selected = false;
            }
            mToolMachine.mChangeState = null;
            mToolMachine.mChangeToolName = null;
            mToolMachine.mChangeToolCostTime = null;
            mToolMachine.mEnableControlDel = null;

            mToolMachine.mChangeState += ChangeState;
            mToolMachine.mChangeToolName += ChangeToolName;
            mToolMachine.mChangeToolCostTime += ChangeCostTime;
            mToolMachine.mEnableControlDel += EnableControl;

            label_ToolStep.Text = "当前算法工具：";
            mCurrStepIndex = -1;
            if (checkBox_EnableBox.Checked)
            {
                mTestRun = false;
                mCurrTestIndex = 0;
                mTestOK = 0;
                mTestNG = 0;
                richTextBox_Mes.Text = "";
                label_CurrID.Text = "当前执行：  第" + mCurrTestIndex + "个   共" + mPathList.Count + "个";
                label_Info.Text = "当前信息：  检测OK共：" + mTestOK + "个   检测到NG：" + mTestNG + "个";
                label_CurrTool.Text = "当前工具：  ";
                manual.Set();

                button_Stop.Text = "暂停";
                button_Stop.Enabled = false;
                button_ActionAll.Enabled = true;
                button_ActionTool.Enabled = true;
                button_Clear.Enabled = true;
                toolStripButton_UP.Enabled = true;
                toolStripButton_Down.Enabled = true;
                toolStripButton_OpenFile.Enabled = true;
            }
            if (mToolPageList.Count > 0)
            {
                panel_ToolForm.Controls.Clear();
                mToolPageList[mToolPageList.Count - 1].Parent = panel_ToolForm;
                mToolPageList[mToolPageList.Count - 1].Show();
            }
        }

        private void ChangeState(int rows, int state)
        {
            try
            {
                dataGridView_ToolList.Invoke(new Action(() =>
                {
                    if (state == 0)
                        this.dataGridView_ToolList.Rows[rows - 1].Cells[1].Value = Properties.Resources.OK;
                    else
                        this.dataGridView_ToolList.Rows[rows - 1].Cells[1].Value = Properties.Resources.NG;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeToolName(int row, string name)
        {
            try
            {
                dataGridView_ToolList.Invoke(new Action(() =>
                {
                    dataGridView_ToolList.Rows[row - 1].Cells[2].Value = name;
                }));
                UpdataUI();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeCostTime(int row, string cost)
        {
            try
            {
                dataGridView_ToolList.Invoke(new Action(() =>
                {
                    if (cost.Contains("ms"))
                        dataGridView_ToolList.Rows[row - 1].Cells[3].Value = cost;
                    else
                        dataGridView_ToolList.Rows[row - 1].Cells[3].Value = cost + "ms";
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, this.toolStrip1.Width, this.toolStrip1.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }

        private void toolStrip4_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, this.toolStrip4.Width, this.toolStrip4.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }

        private void GetNewToolFromList(ToolType Type)
        {
            //获取ID内部编号
            int ID = GetNewToolID();
            ToolOperation op = ToolOperation.GetInstance();
            ToolBase tool;
            UserControl ui;
            op.AddToolFromList(Type, ID, out tool, out ui);
            if (tool != null && ui != null)
            {
                mStepInfoList.Add(tool.ToolParam.StepInfo);
                tool.ToolParam.StepInfo.mStepIndex = mStepInfoList.Count();
                tool.SetDebugWind(mShowWind, mDrawWind);
                mToolList.Add(tool);
                InterfaceUIBase uIBase = (InterfaceUIBase)ui;
                uIBase.StepInfoList = mStepInfoList;
                uIBase.ToolParam = tool.ToolParam;
                uIBase.SetDebugRunWind(null, null);
                ui.Parent = this.panel_ToolForm;
                mToolPageList.Add(ui);
                //ui.Show();
            }

        }

        private void toolStripButton_OpenFile_Click(object sender, EventArgs e)
        {
            string nPath = "";
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Filter = "所有图像文件|*.bmp;*.pcx;*.png;*.jpg;*.gif;*.jpeg";
            openfiledialog1.Title = "打开图像文件";
            if (openfiledialog1.ShowDialog() == DialogResult.OK)
            {
                nPath = openfiledialog1.FileName;
            }
            if (nPath != "")
            {
                HObject mCurrImage;
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.ReadImage(out mCurrImage, nPath);
                richTextBox_Mes.Text = nPath;
                HTuple width, height;
                HOperatorSet.GetImageSize(mCurrImage, out width, out height);
                double a = width; double b = height;
                ChangeWindowSize(Convert.ToDouble(b / a));
                mDrawWind.SetPart(0, 0, height.I, width.I);
                //mDrawWind.ClearWindow();
                //mDrawWind.DispObj(mCurrImage);
                //HOperatorSet.SetPart(mShowWind, 0, 0, height - 1, width - 1);
                //HOperatorSet.ClearWindow(mShowWind);
                //HOperatorSet.DispObj(mCurrImage, mShowWind);
                mToolMachine.ToolCurrImage = mCurrImage;

                FileStream fs = new FileStream(nPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);
                mToolMachine.ToolCurrBitmap = new Bitmap(ms);
                mToolMachine.ToolCurrHimage = new HImage(nPath);
                mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);

                ToolRun();
            }
        }

        private void ChangeWindowSize(double ImageScale)
        {
            int Width, Height;
            double RealScale, ViewScale;
            int PanelX, PanelY;

            RealScale = ImageScale;
            ViewScale = (double)panel_WorkView.Height / panel_WorkView.Width;
            if (RealScale > ViewScale)//以宽为标准
            {
                Height = panel_WorkView.Height;
                Width = (int)(Height / RealScale);
                PanelX = (panel_WorkView.Width - Width) / 2;
                PanelY = 0;
            }
            else//以长为标准
            {
                Width = panel_WorkView.Width;
                Height = (int)(Width * RealScale);
                PanelX = 0;
                PanelY = (panel_WorkView.Height - Height) / 2;
            }
            mShowControl.Parent = panel_WorkView;
            mShowControl.Location = new Point(PanelX, PanelY);
            mShowControl.Height = Height;
            mShowControl.Width = Width;
            mDrawWind.SetWindowExtents(0, 0, Width, Height);
            //HOperatorSet.SetWindowExtents(mShowWind, 0, 0, Width, Height);
        }

        private void toolStripButton_Open_Click(object sender, EventArgs e)
        {
            try
            {
                mPathList = new List<string>();
                string dir = "";
                if (DirEx.SelectDirEx("打开文件夹", ref dir))
                {
                    if (dir != "")
                    {
                        mPathList = new List<string>();
                        mPicIndex = 0;
                        DirectoryInfo theFolder = new DirectoryInfo(dir);
                        FileInfo[] fileInfo = theFolder.GetFiles();
                        for (int i = 0; i < fileInfo.Length; i++)
                        {
                            var fi = new FileInfo(fileInfo[i].FullName);
                            if (fi.Extension == ".png" || fi.Extension == ".jpeg" || fi.Extension == ".jpg"
                                || fi.Extension == ".bmp" || fi.Extension == ".tiff")
                            {
                                mPathList.Add(fileInfo[i].FullName);
                            }
                        }
                    }
                }
                if (mPathList.Count == 0)
                    return;
                if (checkBox_EnableBox.Checked)
                {
                    mTestRun = false;
                    mCurrTestIndex = 0;
                    mTestOK = 0;
                    mTestNG = 0;
                    richTextBox_Mes.Text = "";
                    label_CurrID.Text = "当前执行：  第" + mCurrTestIndex + "个   共" + mPathList.Count + "个";
                    label_Info.Text = "当前信息：  检测OK共：" + mTestOK + "个   检测到NG：" + mTestNG + "个";
                    manual.Set();

                    button_Stop.Text = "暂停";
                    button_Stop.Enabled = false;
                    button_ActionAll.Enabled = true;
                    button_ActionTool.Enabled = true;
                    button_Clear.Enabled = true;
                }
                HObject mCurrImage;
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.ReadImage(out mCurrImage, mPathList[mPicIndex]);
                richTextBox_Mes.Text = mPathList[mPicIndex];
                HTuple width, height;
                HOperatorSet.GetImageSize(mCurrImage, out width, out height);
                double a = width; double b = height;
                ChangeWindowSize(Convert.ToDouble(b / a));
                mDrawWind.SetPart(0, 0, height.I, width.I);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mCurrImage);
                mToolMachine.ToolCurrImage = mCurrImage;
                FileStream fs = new FileStream(mPathList[mPicIndex], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);
                mToolMachine.ToolCurrBitmap = new Bitmap(ms);
                mToolMachine.ToolCurrHimage = new HImage(mPathList[mPicIndex]);
                mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                ToolRun();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int ToolRun()
        {
            int res = 0;
            int step = 0;
            mDrawWind.ClearWindow();
            for (int i = 0; i < mToolList.Count; i++)
            {
                ChangeState(i + 1, 0);
            }
            for (int i = 0; i < mToolList.Count; i++)
            {
                JumpInfo info;
                HTuple s1, s2;
                HOperatorSet.CountSeconds(out s1);
                res = mToolList[i].DebugRun(mToolMachine.ToolCurrImage, null, mStepInfoList, false, out info);
                HOperatorSet.CountSeconds(out s2);
                ChangeCostTime(i + 1, ((s2.D - s1.D) * 1000).ToString("00.00") + " ms");
                ChangeState(i + 1, res);
                InterfaceUIBase uIBase = (InterfaceUIBase)mToolPageList[i];
                this.Invoke(new Action(() => { uIBase.ShowCurrMes(); }));
                if (res != 0)
                {
                    step = i;
                    break;
                }
            }
            if (res != 0)
            {
                if (step < mCurrStepIndex - 1 && mCurrStepIndex > -1)
                {

                    JumpInfo info;
                    HTuple s1, s2;
                    HOperatorSet.CountSeconds(out s1);
                    res = mToolList[step].DebugRun(mToolMachine.ToolCurrImage, null, mStepInfoList, false, out info);
                    HOperatorSet.CountSeconds(out s2);
                    ChangeCostTime(step + 1, ((s2.D - s1.D) * 1000).ToString("00.00") + " ms");
                    ChangeState(step + 1, res);
                    InterfaceUIBase uIBase = (InterfaceUIBase)mToolPageList[step];
                    this.Invoke(new Action(() => { uIBase.ShowCurrMes(); }));

                }
                mDrawWind.SetFont("Consolas-24");
                mDrawWind.SetColor("red");
                mDrawWind.SetTposition(50, 50);
                mDrawWind.WriteString("NG");
                mDrawWind.SetTposition(50, 150);
                mDrawWind.WriteString(mToolList[step].ToolParam.ShowName);
            }
            else
            {
                if (mCurrStepIndex > -1)
                {

                    if (mToolList[mCurrStepIndex].ToolParam.ToolType != ToolType.CropImage)
                    {
                        JumpInfo info;
                        HTuple s1, s2;
                        HOperatorSet.CountSeconds(out s1);
                        res = mToolList[mCurrStepIndex].DebugRun(mToolMachine.ToolCurrImage, null, mStepInfoList, false, out info);
                        HOperatorSet.CountSeconds(out s2);
                        ChangeCostTime(mCurrStepIndex + 1, ((s2.D - s1.D) * 1000).ToString("00.00") + " ms");
                        ChangeState(mCurrStepIndex + 1, res);
                        InterfaceUIBase uIBase = (InterfaceUIBase)mToolPageList[mCurrStepIndex];
                        this.Invoke(new Action(() => { uIBase.ShowCurrMes(); }));
                        if (res != 0)
                        {
                            mDrawWind.SetFont("Consolas-24");
                            mDrawWind.SetColor("red");
                            mDrawWind.SetTposition(50, 50);
                            mDrawWind.WriteString("NG");
                            mDrawWind.SetTposition(50, 150);
                            mDrawWind.WriteString(mToolList[step].ToolParam.ShowName);
                            return -1;
                        }
                    }
                }
                mDrawWind.SetColor("green");
                mDrawWind.SetTposition(50, 50);
                mDrawWind.WriteString("OK");
            }

            return res;
        }

        public int SingeToolRun()
        {
            int res = 0;
            for (int i = 0; i < mToolList.Count; i++)
            {
                ChangeState(i + 1, 0);
            }
            if (mCurrStepIndex > -1)
            {
                JumpInfo info;
                HTuple s1, s2;
                HOperatorSet.CountSeconds(out s1);
                res = mToolList[mCurrStepIndex].DebugRun(mToolMachine.ToolCurrImage, null, mStepInfoList, false, out info);
                HOperatorSet.CountSeconds(out s2);
                ChangeCostTime(mCurrStepIndex + 1, ((s2.D - s1.D) * 1000).ToString("00.00") + " ms");
                ChangeState(mCurrStepIndex + 1, res);
                InterfaceUIBase uIBase = (InterfaceUIBase)mToolPageList[mCurrStepIndex];
                this.Invoke(new Action(() => { uIBase.ShowCurrMes(); }));
            }
            if (res != 0)
            {
                mDrawWind.SetColor("red");
                mDrawWind.SetTposition(50, 50);
                mDrawWind.WriteString("NG");
            }
            else
            {
                mDrawWind.SetColor("green");
                mDrawWind.SetTposition(50, 50);
                mDrawWind.WriteString("OK");
            }

            return res;
        }

        private void toolStripButton_UP_Click(object sender, EventArgs e)
        {
            try
            {
                if (loock1)
                    return;
                if (mToolMachine.ToolCurrBitmap == null)
                    return;
                if (mPathList.Count == 0)
                    return;
                loock1 = true;
                if (mPicIndex > 0)
                {
                    mPicIndex--;
                }
                else
                {
                    mPicIndex = 0;
                }
                HObject mCurrImage;
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.ReadImage(out mCurrImage, mPathList[mPicIndex]);
                richTextBox_Mes.Text = mPathList[mPicIndex];
                HTuple width, height;
                HOperatorSet.GetImageSize(mCurrImage, out width, out height);
                double a = width; double b = height;
                ChangeWindowSize(Convert.ToDouble(b / a));
                mDrawWind.SetPart(0, 0, height.I, width.I);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mCurrImage);
                mToolMachine.ToolCurrImage = mCurrImage;
                FileStream fs = new FileStream(mPathList[mPicIndex], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);
                mToolMachine.ToolCurrBitmap = new Bitmap(ms);
                mToolMachine.ToolCurrHimage = new HImage(mPathList[mPicIndex]);
                mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                ToolRun();
                loock1 = false;
            }
            catch (Exception ex)
            {
                loock1 = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton_Down_Click(object sender, EventArgs e)
        {
            try
            {
                if (loock1)
                    return;
                if (mToolMachine.ToolCurrBitmap == null)
                    return;
                if (mPathList.Count == 0)
                    return;
                loock1 = true;
                if (mPicIndex < mPathList.Count - 1)
                {
                    mPicIndex++;
                }
                else
                {
                    mPicIndex = mPathList.Count - 1;
                }
                HObject mCurrImage;
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.GenEmptyObj(out mCurrImage);
                HOperatorSet.ReadImage(out mCurrImage, mPathList[mPicIndex]);
                richTextBox_Mes.Text = mPathList[mPicIndex];
                HTuple width, height;
                HOperatorSet.GetImageSize(mCurrImage, out width, out height);
                double a = width; double b = height;
                ChangeWindowSize(Convert.ToDouble(b / a));
                mDrawWind.SetPart(0, 0, height.I, width.I);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mCurrImage);
                mToolMachine.ToolCurrImage = mCurrImage;
                FileStream fs = new FileStream(mPathList[mPicIndex], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                MemoryStream ms = new MemoryStream(bytes);
                mToolMachine.ToolCurrBitmap = new Bitmap(ms);
                mToolMachine.ToolCurrHimage = new HImage(mPathList[mPicIndex]);
                mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                ToolRun();
                loock1 = false;
            }
            catch (Exception ex)
            {
                loock1 = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void hWindowControl1_HMouseWheel(object sender, HMouseEventArgs e)
        {
            try
            {
                if (mToolMachine.ToolCurrImage == null)
                    return;
                if (mDrawWind != null)
                {
                    double Zoom;
                    int Row, Col, Button;
                    HTuple Row0, Column0, Row00, Column00, Ht, Wt, r1, c1, r2, c2;
                    if (e.Delta > 0)
                    {
                        Zoom = 1.2;
                    }
                    else
                    {
                        Zoom = 0.8;
                    }
                    mShowControl.Focus();
                    mDrawWind.GetMposition(out Row, out Col, out Button);
                    mDrawWind.GetPart(out Row0, out Column0, out Row00, out Column00);
                    Ht = Row00 - Row0;
                    Wt = Column00 - Column0;
                    if (Ht * Wt < 32000 * 32000 || Zoom == 1.2)//普通版halcon能处理的图像最大尺寸是32K*32K。如果无限缩小原图像，导致显示的图像超出限制，则会造成程序崩溃
                    {
                        r1 = (Row0 + ((1 - (1.0 / Zoom)) * (Row - Row0)));
                        c1 = (Column0 + ((1 - (1.0 / Zoom)) * (Col - Column0)));
                        r2 = r1 + (Ht / Zoom);
                        c2 = c1 + (Wt / Zoom);
                        mDrawWind.SetPart(r1, c1, r2, c2);
                        HOperatorSet.SetSystem("flush_graphic", "false");
                        mDrawWind.ClearWindow();
                        mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                        for (int i = 0; i < mToolMachine.ShowRegionList.Count; i++)
                        {
                            mDrawWind.SetRgba(255, 0, 0, 90);
                            //mDrawWind.SetColor(ShowColors.Colors[i]);
                            if (mToolMachine.ShowRegionList[i] != null && mToolMachine.ShowRegionList[i].IsInitialized())
                            {
                                mDrawWind.DispObj(mToolMachine.ShowRegionList[i]);
                            }

                        }
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");
                        //HOperatorSet.SetPart(mShowWind, r1, c1, r2, c2);
                        //HOperatorSet.ClearWindow(mShowWind);
                        //HOperatorSet.DispObj(mMachine.CurrImage, mShowWind);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left || (Control.ModifierKeys & Keys.Control) != Keys.Control)
                    return;
                if (mToolMachine.ToolCurrImage == null)
                    return;
                if (mDrawWind != null)
                {
                    try
                    {
                        int Row, Column, Button;
                        HTuple row1, col1, row2, col2;
                        mDrawWind.GetMposition(out Row, out Column, out Button);
                        double RowMove = Row - RowDown;   //鼠标弹起时的行坐标减去按下时的行坐标，得到行坐标的移动值
                        double ColMove = Column - ColDown;//鼠标弹起时的列坐标减去按下时的列坐标，得到列坐标的移动值
                        mDrawWind.GetPart(out row1, out col1, out row2, out col2);//得到当前的窗口坐标
                        mDrawWind.SetPart(row1 - RowMove, col1 - ColMove, row2 - RowMove, col2 - ColMove);//这里可能有些不好理解。以左上角原点为参考点
                        HOperatorSet.SetSystem("flush_graphic", "false");
                        mDrawWind.ClearWindow();
                        mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                        for (int i = 0; i < mToolMachine.ShowRegionList.Count; i++)
                        {
                            mDrawWind.SetRgba(255, 0, 0, 90);
                            mDrawWind.DispObj(mToolMachine.ShowRegionList[i]);
                        }
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void hWindowControl1_HMouseDown(object sender, HMouseEventArgs e)
        {
            try
            {
                if (mToolMachine.ToolCurrImage == null)
                    return;
                try
                {
                    int Row, Column, Button;
                    mDrawWind.GetMposition(out Row, out Column, out Button);
                    RowDown = Row;    //鼠标按下时的行坐标
                    ColDown = Column; //鼠标按下时的列坐标
                }
                catch (Exception)
                {

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }



        private void mTreeViewTools_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToolOperation op = ToolOperation.GetInstance();
            ToolType tool = op.GetToolNameFromStr(mTreeViewTools.SelectedNode.Text);
            if (tool != ToolType.None)
            {
                GetNewToolFromList(tool);
                UpdataToolView();
                if (mToolMachine.ToolCurrImage != null)
                    ToolRun();
            }
        }


        private void ReadCurrTask(string mCurrProject, int num)
        {
            mToolList = new List<ToolBase>();
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Project\\" + mCurrProject + "\\" + num + ".dat";
            if (!File.Exists(path))
                return;
            mMachine.ReadTask(path, out mToolList);
            if (mToolList.Count > 0)
            {
                UpdataToolView();
                foreach (ToolBase item in mToolList)
                {
                    Application.DoEvents();
                    mStepInfoList.Add(item.ToolParam.StepInfo);
                    //初始化窗口
                    item.SetDebugWind(mShowWind, mDrawWind);
                    //初始化AI模型
                    if (item.ToolParam.ToolType == ToolType.HObjecDetect1 ||
                         item.ToolParam.ToolType == ToolType.HsemanticAI ||
                         item.ToolParam.ToolType == ToolType.HClassifiyAI ||
                         item.ToolParam.ToolType == ToolType.DeepOcr)
                    {
                        LoadingHelper.ShowLoadingMessage("模型加载中。。。");
                        //加载模型
                        item.InitAiResources();
                    }
                    //初始化Tool界面
                    UserControl ui;
                    int res = GetPageFromToolBase(item, out ui);
                    if (res != 0)
                    {
                        MessageBox.Show("窗口初始化失败！");
                        this.Close();
                    }
                    if (ui != null)
                        mToolPageList.Add(ui);
                    else
                    {
                        MessageBox.Show(new Form { TopMost = true }, "生成工具Page失败！");
                        mToolList.Clear();
                        mStepInfoList.Clear();
                        mToolPageList.Clear();
                        break;

                    }

                }
            }
        }

        public int GetPageFromToolBase(ToolBase tool, out UserControl page)
        {
            page = null;
            try
            {
                ToolOperation op = ToolOperation.GetInstance();
                op.GetPageFromToolBase(tool, out page);
                InterfaceUIBase uIBase = (InterfaceUIBase)page;
                uIBase.StepInfoList = mStepInfoList;
                uIBase.ToolParam = tool.ToolParam;
                uIBase.SetDebugRunWind(null, null);
                page.Parent = this.panel_ToolForm;
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex.Message);
                return 1;
            }
        }

        private void toolStripButton_SaveParam_Click(object sender, EventArgs e)
        {
            if (mIsRunning)
                return;
            this.Enabled = false;
            mIsRunning = true;
            panel1.Enabled = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Project\\" + CurrProject.mProjectName + "\\" + mCurrTaskIndex + ".dat";
            this.Invoke(new Action(() => { mMachine.SaveTask(path, mToolList); }));
            mIsRunning = false;
            panel1.Enabled = true;
            this.Enabled = true;
        }

        private void button_Task1_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            int a = 1;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 1;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务一";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }

                LoadingHelper.CloseForm();  
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task2_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            int a = 2;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 2;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务二";
                //传递相机句柄
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                LoadingHelper.CloseForm();
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task3_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            int a = 3;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 3;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务三";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                LoadingHelper.CloseForm();
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task4_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            int a = 4;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 4;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务四";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                LoadingHelper.CloseForm();
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task5_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            int a = 5;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 5;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务五";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                LoadingHelper.CloseForm();
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task6_Click(object sender, EventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            int a = 6;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 6;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务六";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                LoadingHelper.CloseForm();
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task7_Click(object sender, EventArgs e)
        {
            int a = 7;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 7;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务七";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }

        private void button_Task8_Click(object sender, EventArgs e)
        {
            int a = 8;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 8;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务八";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }
        private void button_Task9_Click(object sender, EventArgs e)
        {
            int a = 9;
            if (mCurrTaskIndex != a)
            {
                if (mToolList.Count > 0)
                {
                    for (int i = 0; i < mToolList.Count; i++)
                    {
                        mToolList[i].Dispose();
                    }
                }
                this.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                mToolPageList.Clear();
                mStepInfoList.Clear();
                dataGridView_ToolList.Rows.Clear();
                panel_ToolForm.Controls.Clear();
                mDrawWind.ClearWindow();
                mDrawWind.DetachBackgroundFromWindow();
                Application.DoEvents();
                mCurrTaskIndex = 9;
                mCurrStepIndex = -1;
                ReadCurrTask(mMachine.CurrProjectInfo.mProjectName, mCurrTaskIndex);
                label_TaskNum.Text = "当前：任务九";
                //传递相机句柄
                if (mMachine.CamList.Count >= mCurrTaskIndex)
                    mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];
                else
                    mCurrCamera = null;
                if (mCurrCamera != null)
                {
                    ChangeWindowSize(mCurrCamera.ImageScale);
                    mDrawWind.SetPart(0, 0, mCurrCamera.ImageHeight, mCurrCamera.ImageWidth);
                }
                this.Enabled = true;
                dataGridView_ToolList.Enabled = true;
            }
        }
        private void 删除工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMouseDown == -1) return;
            if (mMouseDown >= mToolList.Count) return;
            if (mToolList[mMouseDown].ToolParam.ToolType == ToolType.HObjecDetect1 || mToolList[mMouseDown].ToolParam.ToolType == ToolType.HsemanticAI)
                mToolList[mMouseDown].Dispose();
            mToolList.RemoveAt(mMouseDown);
            mToolPageList.RemoveAt(mMouseDown);
            mStepInfoList.RemoveAt(mMouseDown);
            UpdataToolView();
            panel_ToolForm.Controls.Clear();
            mToolMachine.ShowRegionList.Clear();
            if (mToolPageList.Count > 0)
            {
                mToolPageList[0].Parent = panel_ToolForm;
                mToolPageList[0].Show();
            }
            UpdataUI();

        }

        private void 上移工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMouseDown < 1) return;
            ToolBase tool1 = mToolList[mMouseDown - 1];
            ToolBase tool2 = mToolList[mMouseDown];
            mToolList[mMouseDown] = tool1;
            mToolList[mMouseDown - 1] = tool2;

            UserControl control1 = mToolPageList[mMouseDown - 1];
            UserControl control2 = mToolPageList[mMouseDown];
            mToolPageList[mMouseDown] = control1;
            mToolPageList[mMouseDown - 1] = control2;

            UpdataToolView();

            mStepInfoList = new List<StepInfo>();
            foreach (ToolBase item in mToolList)
            {
                mStepInfoList.Add(item.ToolParam.StepInfo);
            }
            panel_ToolForm.Controls.Clear();
            mToolMachine.ShowRegionList.Clear();

            UpdataUI();
        }

        private void 下移工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mMouseDown >= mToolList.Count - 1) return;
            ToolBase tool1 = mToolList[mMouseDown + 1];
            ToolBase tool2 = mToolList[mMouseDown];
            mToolList[mMouseDown] = tool1;
            mToolList[mMouseDown + 1] = tool2;

            UserControl control1 = mToolPageList[mMouseDown + 1];
            UserControl control2 = mToolPageList[mMouseDown];
            mToolPageList[mMouseDown] = control1;
            mToolPageList[mMouseDown + 1] = control2;
            UpdataToolView();
            mStepInfoList = new List<StepInfo>();
            foreach (ToolBase item in mToolList)
            {
                mStepInfoList.Add(item.ToolParam.StepInfo);

            }
            panel_ToolForm.Controls.Clear();
            mToolMachine.ShowRegionList.Clear();

            UpdataUI();
        }

        public void EnableControl(bool isEnable)
        {
            if (!isEnable)
            {
                panel_ToolButton.Enabled = false;
                dataGridView_ToolList.Enabled = false;
                groupBox_ToolForm.Enabled = false;
                mTreeViewTools.Enabled = false;
            }
            else
            {
                panel_ToolButton.Enabled = true;
                dataGridView_ToolList.Enabled = true;
                groupBox_ToolForm.Enabled = true;
                mTreeViewTools.Enabled = true;
            }
        }

        private void toolStripButton_ScaleImageSize_Click(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage == null)
                return;
            HTuple width, height;
            HOperatorSet.GetImageSize(mToolMachine.ToolCurrImage, out width, out height);
            mDrawWind.SetPart(0, 0, height.I, width.I);
            mDrawWind.ClearWindow();
            mDrawWind.DispObj(mToolMachine.ToolCurrImage);

        }

        private void dataGridView_ToolList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;
            if (a > -1)
            {
                mMouseDown = a;
                mCurrStepIndex = a;
                if (mToolList.Count > 0)
                {
                    label_ToolStep.Text = "当前选定工具：  " + (mMouseDown + 1) + "_" + mToolList[a].ToolParam.ShowName + " ";
                    label_CurrTool.Text = "当前工具：  " + (mMouseDown + 1) + "_" + mToolList[a].ToolParam.ShowName + " ";
                }
            }
            if (a >= 0)
            {
                mMouseDown = a;
                panel_ToolForm.Controls.Clear();
                mToolMachine.ShowRegionList.Clear();
                mToolPageList[a].Parent = panel_ToolForm;
                mToolPageList[a].Show();
                if (mToolMachine.ToolCurrImage == null || mToolMachine.ToolCurrBitmap == null)
                    return;
                if (checkBox_EnableBox.Checked)
                    return;
                ToolRun();
            }
        }
        private void DebugRun()
        {
            while (mIsRun)
            {
                if (mCurrCamera == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                if (mCurrCamera.ImageBuffLength > 0)
                {
                    HObject image = mCurrCamera.Dequeue();
                    mDrawWind.ClearWindow();
                    mToolMachine.ToolCurrImage = image;
                    mToolMachine.ToolCurrHimage = new HImage(image);
                    mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                    Thread.Sleep(5);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void toolStripButton_CamStart_Click(object sender, EventArgs e)
        {
            if (mCurrCamera == null)
            {
                MessageBox.Show("当前相机不可用");
                return;
            }
            mCurrCamera.StartGrab();
            mStartTrigger = true;
            toolStripButton_CamStart.Enabled = false;
            toolStripButton_CamStop.Enabled = true;
            toolStrip4.Enabled = false;
            toolStripButton_ScaleImageSize.Enabled = false;
            toolStripButton_SaveParam.Enabled = false;
            toolStripButton_Close.Enabled = false;
            dataGridView_ToolList.Enabled = false;
            panel_ToolForm.Enabled = false;
            panel1.Enabled = false;
            mTreeViewTools.Enabled = false;
        }

        private void toolStripButton_CamStop_Click(object sender, EventArgs e)
        {
            mCurrCamera.StopGrab();
            mStartTrigger = false;
            toolStripButton_CamStart.Enabled = true;
            toolStripButton_CamStop.Enabled = false;
            toolStrip4.Enabled = true;
            toolStripButton_ScaleImageSize.Enabled = true;
            toolStripButton_SaveParam.Enabled = true;
            toolStripButton_Close.Enabled = true;
            dataGridView_ToolList.Enabled = true;
            panel_ToolForm.Enabled = true;
            panel1.Enabled = true;
            mTreeViewTools.Enabled = true;
        }

        private void FrmProjectSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadingHelper.ShowLoadingScreen();
            //注销线程
            mIsRun = false;
            mTestRun = false;
            mDebugThread.Join();
            manual.Set();
            mToolRunThread.Join();
            //清除缓存
            mImageList.Clear();
            for (int i = 0; i < mToolList.Count; i++)
            {
                LoadingHelper.ShowLoadingMessage("模型释放中。。。");
                mToolList[i].Dispose();
            }
            LoadingHelper.CloseForm();
        }

        private void InitButton(int num)
        {
            switch (num)
            {
                case 0:
                    button_Task1.Visible = false;
                    button_Task2.Visible = false;
                    button_Task3.Visible = false;
                    button_Task4.Visible = false;
                    button_Task5.Visible = false;
                    button_Task6.Visible = false;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 1:
                    button_Task1.Visible = true;
                    button_Task2.Visible = false;
                    button_Task3.Visible = false;
                    button_Task4.Visible = false;
                    button_Task5.Visible = false;
                    button_Task6.Visible = false;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 2:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = false;
                    button_Task4.Visible = false;
                    button_Task5.Visible = false;
                    button_Task6.Visible = false;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 3:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = false;
                    button_Task5.Visible = false;
                    button_Task6.Visible = false;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 4:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = true;
                    button_Task5.Visible = false;
                    button_Task6.Visible = false;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 5:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = true;
                    button_Task5.Visible = true;
                    button_Task6.Visible = false;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 6:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = true;
                    button_Task5.Visible = true;
                    button_Task6.Visible = true;
                    button_Task7.Visible = false;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 7:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = true;
                    button_Task5.Visible = true;
                    button_Task6.Visible = true;
                    button_Task7.Visible = true;
                    button_Task8.Visible = false;
                    button_Task9.Visible = false;
                    break;
                case 8:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = true;
                    button_Task5.Visible = true;
                    button_Task6.Visible = true;
                    button_Task7.Visible = true;
                    button_Task8.Visible = true;
                    button_Task9.Visible = false;
                    break;
                case 9:
                    button_Task1.Visible = true;
                    button_Task2.Visible = true;
                    button_Task3.Visible = true;
                    button_Task4.Visible = true;
                    button_Task5.Visible = true;
                    button_Task6.Visible = true;
                    button_Task7.Visible = true;
                    button_Task8.Visible = true;
                    button_Task9.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void UpdataUI()
        {
            for (int i = 0; i < mToolPageList.Count; i++)
            {
                InterfaceUIBase ibase = (InterfaceUIBase)mToolPageList[i];
                ibase.StepInfoList = mStepInfoList;
                ibase.InitRoiShow();

            }
        }
        private void dataGridView_ToolList_CellMouseDown_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.Button == MouseButtons.Right)
            {
                dataGridView_ToolList.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                mMouseDown = e.RowIndex;
            }
        }
        private int GetNewToolID()
        {
            int ToolID = 0;
            if (mToolList.Count > 0)
            {
                //获取工具链中最大ID号
                foreach (StepInfo info in mStepInfoList)
                {
                    if (ToolID < info.mInnerToolID)
                        ToolID = info.mInnerToolID;
                }
                return ToolID + 1;
            }
            else
                return 1;
        }

        private void dataGridView_ToolList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                dataGridView_ToolList.ContextMenuStrip = null;
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                dataGridView_ToolList.ContextMenuStrip = contextMenuStrip_Tools;
                dataGridView_ToolList.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                if (e.RowIndex == mMouseDown)
                    return;
                else
                    mMouseDown = e.RowIndex;
            }
        }


        private void EnableButton2(bool state)
        {
            panel1.Enabled = state;
            toolStripButton_UP.Enabled = state;
            toolStripButton_Down.Enabled = state;
            toolStripButton_OpenFile.Enabled = state;
            toolStrip1.Enabled = state;
            mTreeViewTools.Enabled = state;
            contextMenuStrip_Tools.Enabled = state;
            panel_ToolForm.Enabled = state;
        }

        private void checkBox_EnableBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_EnableBox.Checked)
            {
                EnableButton2(false);
                panel_ControlBox.Enabled = true;
            }
            else
            {
                EnableButton2(true);
                panel_ControlBox.Enabled = false;
            }
        }

        private void radioButton_NgStop_CheckedChanged(object sender, EventArgs e)
        {
            mRunMode = 1;
        }

        private void radioButton_OKStop_CheckedChanged(object sender, EventArgs e)
        {
            mRunMode = 2;
        }

        private void radioButton_OKorNG_CheckedChanged(object sender, EventArgs e)
        {
            mRunMode = 3;
        }

        private void DebugTestRun()
        {
            while (mIsRun)
            {
                if (!mTestRun)
                {
                    Thread.Sleep(10);
                    continue;
                }
                if (mPathList.Count == 0)
                {
                    Thread.Sleep(10);
                    continue;
                }
                for (int i = 0; i < mPathList.Count; i++)
                {
                    manual.WaitOne();
                    if (!mTestRun)
                        break;
                    if (mPicIndex >= mPathList.Count - 1)
                        mPicIndex = 0;
                    HObject mCurrImage;
                    HOperatorSet.ReadImage(out mCurrImage, mPathList[mPicIndex]);
                    HTuple width, height;
                    HOperatorSet.GetImageSize(mCurrImage, out width, out height);
                    mDrawWind.SetPart(0, 0, height.I, width.I);
                    mToolMachine.ToolCurrImage = mCurrImage;
                    mToolMachine.ToolCurrHimage = new HImage(mCurrImage);
                    mDrawWind.AttachBackgroundToWindow(mToolMachine.ToolCurrHimage);
                    int res;
                    if (mTestAllRun)
                        res = ToolRun();
                    else
                        res = SingeToolRun();
                    if (res != 0)
                    {
                        NGPath.Add(mPathList[mPicIndex]);
                        mTestNG++;
                    }
                    else
                    {
                        OkPath.Add(mPathList[mPicIndex]);
                        mTestOK++;
                    }
                    mPicIndex++;
                    if (mRunMode == 1)
                    {
                        if (res != 0)
                        {
                            manual.Reset();
                            button_Stop.Invoke(new Action(() =>
                            {
                                button_Stop.Text = "继续";
                                button_Clear.Enabled = true;
                            }));
                        }
                    }
                    else if (mRunMode == 2)
                    {
                        if (res == 0)
                        {
                            manual.Reset();
                            button_Stop.Invoke(new Action(() =>
                            {
                                button_Stop.Text = "继续";
                                button_Clear.Enabled = true;
                            }));
                        }
                    }
                    mCurrTestIndex++;
                    this.Invoke(new Action(() =>
                    {
                        richTextBox_Mes.Text = "图片路径为：" + mPathList[i];
                        label_CurrID.Text = "当前执行：  第" + mCurrTestIndex + "个   共" + mPathList.Count + "个";
                        label_Info.Text = "当前信息：  检测OK共：" + mTestOK + "个   检测到NG：" + mTestNG + "个";
                    }));
                }
                mTestRun = false;
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    button_Stop.Text = "暂停";
                    button_Stop.Enabled = false;
                    button_Clear.Enabled = true;
                    toolStripButton_UP.Enabled = true;
                    toolStripButton_Down.Enabled = true;
                    toolStripButton_OpenFile.Enabled = true;
                }));

                Thread.Sleep(1);
            }
        }

        private void button_ActionAll_Click(object sender, EventArgs e)
        {
            if (mPathList.Count == 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "未选定图片文件夹或文件夹内图片为空");
                return;
            }
            mTestAllRun = true;
            manual.Set();
            button_Stop.Text = "暂停";
            button_Stop.Enabled = true;
            button_ActionAll.Enabled = false;
            button_ActionTool.Enabled = false;
            button_Clear.Enabled = false;
            toolStripButton_UP.Enabled = false;
            toolStripButton_Down.Enabled = false;
            toolStripButton_OpenFile.Enabled = false;
            mTestRun = true;
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            if (button_Stop.Text == "继续")
            {
                manual.Set();
                button_Stop.Text = "暂停";
                button_Clear.Enabled = false;
            }
            else
            {
                manual.Reset();
                button_Stop.Text = "继续";
                button_Clear.Enabled = true;
            }
        }

        private void button_ActionTool_Click(object sender, EventArgs e)
        {
            if (mCurrStepIndex < 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "未选定单步调试工具");
                return;
            }
            if (mPathList.Count == 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "未选定图片文件夹或文件夹内图片为空");
                return;
            }
            if (!(mToolList[mCurrStepIndex].ToolParam.ToolType == ToolType.HObjecDetect1 ||
                mToolList[mCurrStepIndex].ToolParam.ToolType == ToolType.VsemanticAI ||
                mToolList[mCurrStepIndex].ToolParam.ToolType == ToolType.ShapeModle))
            {
                MessageBox.Show(new Form { TopMost = true }, "当前选定工具并非独立工具，具有输入源，禁止单工具运行");
                return;
            }
            mTestAllRun = false;
            manual.Set();
            button_Stop.Text = "暂停";
            button_Stop.Enabled = true;
            button_ActionAll.Enabled = false;
            button_ActionTool.Enabled = false;
            button_Clear.Enabled = false;
            toolStripButton_UP.Enabled = false;
            toolStripButton_Down.Enabled = false;
            toolStripButton_OpenFile.Enabled = false;
            mTestRun = true;
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            mTestRun = false;
            mPicIndex = 0;
            OkPath = new List<string>();
            NGPath = new List<string>();
            mCurrTestIndex = 0;
            mTestOK = 0;
            mTestNG = 0;
            richTextBox_Mes.Text = "";
            label_CurrID.Text = "当前执行：  第" + mCurrTestIndex + "个   共" + mPathList.Count + "个";
            label_Info.Text = "当前信息：  检测OK共：" + mTestOK + "个   检测到NG：" + mTestNG + "个";
            manual.Set();

            button_Stop.Text = "暂停";
            button_Stop.Enabled = false;
            button_ActionAll.Enabled = true;
            button_ActionTool.Enabled = true;
            button_Clear.Enabled = true;
            toolStripButton_UP.Enabled = true;
            toolStripButton_Down.Enabled = true;
            toolStripButton_OpenFile.Enabled = true;
        }

        //byte[]转彩色图像
        private Bitmap GetColorBitmap(byte[] mImageByte, int mImageWidth, int mImageHeight)
        {
            try
            {
                byte[] brg = new byte[mImageWidth * mImageHeight * 3];
                //对每一个像素的rgb to bgr的转换
                for (int i = 0; i < mImageByte.Length; i += 3)
                {
                    brg[i] = mImageByte[i + 2];
                    brg[i + 1] = mImageByte[i + 1];
                    brg[i + 2] = mImageByte[i];
                }
                Bitmap curBitmap = new Bitmap(mImageWidth, mImageHeight, PixelFormat.Format24bppRgb);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                BitmapData bmpdata = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                Marshal.Copy(brg, 0, bmpdata.Scan0, brg.Length);
                curBitmap.UnlockBits(bmpdata);
                return curBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //byte[]转黑白图像
        private Bitmap GetMonoBitmap(byte[] mImageByte, int mImageWidth, int mImageHeight)
        {
            try
            {
                byte[] brg = new byte[mImageWidth * mImageHeight * 3];
                //对每一个像素的rgb to bgr的转换
                for (int i = 0; i < mImageByte.Length; i++)
                {
                    brg[i] = mImageByte[i];
                    brg[i + 1] = mImageByte[i];
                    brg[i + 1] = mImageByte[i];
                    brg[i + 2] = mImageByte[i];
                }
                Bitmap curBitmap = new Bitmap(mImageWidth, mImageHeight, PixelFormat.Format24bppRgb);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                BitmapData bmpdata = curBitmap.LockBits(rect, ImageLockMode.ReadWrite, curBitmap.PixelFormat);
                Marshal.Copy(brg, 0, bmpdata.Scan0, brg.Length);
                curBitmap.UnlockBits(bmpdata);
                return curBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void button_CopyOk_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            string path = Application.StartupPath + "\\Images\\OK\\" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            for (int i = 0; i < OkPath.Count; i++)
            {
                try
                {
                    File.Copy(OkPath[i], path + OkPath[i].Split('\\')[OkPath[i].Split('\\').Length - 1]);
                }
                catch (Exception)
                {
                }
            }
            this.Enabled = true;
            OkPath.Clear();
            Process.Start(path);
            MessageBox.Show("导出完成");
        }

        private void button_CopyNG_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            string path = Application.StartupPath + "\\Images\\NG\\" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            for (int i = 0; i < NGPath.Count; i++)
            {
                try
                {
                    File.Copy(NGPath[i], path + NGPath[i].Split('\\')[NGPath[i].Split('\\').Length - 1]);
                }
                catch (Exception)
                {
                }
            }
            this.Enabled = true;
            NGPath.Clear();
            Process.Start(path);
            MessageBox.Show("导出完成");
        }

        private void InitcontextMenuStrip()
        {
            item1 = new ToolStripMenuItem();
            item1.Text = "在上方插入";

            item2 = new ToolStripMenuItem();
            item2.Text = "在下方插入";

            for (int i = 0; i < mToolNameList.Count; i++)
            {
                ToolStripMenuItem menu1 = new ToolStripMenuItem();
                menu1.Text = mToolNameList[i];
                menu1.Click += toolStripMenuItem1_Click;
                item1.DropDownItems.Add(menu1);
            }
            for (int i = 0; i < mToolNameList.Count; i++)
            {
                ToolStripMenuItem menu1 = new ToolStripMenuItem();
                menu1.Text = mToolNameList[i];
                menu1.Click += toolStripMenuItem2_Click;
                item2.DropDownItems.Add(menu1);
            }
            contextMenuStrip_Tools.Items.Add(item1);
            contextMenuStrip_Tools.Items.Add(item2);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mMouseDown < 0)
                return;
            ToolOperation op = ToolOperation.GetInstance();
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            string a = menu.Text.ToString();
            ToolType tye = op.GetToolNameFromStr(a);
            if (tye != ToolType.None)
            {
                //获取ID内部编号
                int ID = GetNewToolID();
                ToolBase tool;
                UserControl ui;
                op.AddToolFromList(tye, ID, out tool, out ui);
                if (tool != null && ui != null)
                {
                    mStepInfoList.Add(tool.ToolParam.StepInfo);
                    tool.ToolParam.StepInfo.mStepIndex = mStepInfoList.Count();
                    tool.SetDebugWind(mShowWind, mDrawWind);
                    InterfaceUIBase uIBase = (InterfaceUIBase)ui;
                    uIBase.StepInfoList = mStepInfoList;
                    uIBase.ToolParam = tool.ToolParam;
                    uIBase.SetDebugRunWind(null, null);
                    ui.Parent = this.panel_ToolForm;
                    mToolPageList.Insert(mMouseDown, ui);
                    mToolList.Insert(mMouseDown, tool);
                }

                UpdataToolView();
                mStepInfoList = new List<StepInfo>();
                foreach (ToolBase item in mToolList)
                {
                    mStepInfoList.Add(item.ToolParam.StepInfo);
                }
                panel_ToolForm.Controls.Clear();
                mToolMachine.ShowRegionList.Clear();

                UpdataUI();
                if (mToolMachine.ToolCurrImage != null)
                    ToolRun();
            }

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (mMouseDown < 0)
                return;
            ToolOperation op = ToolOperation.GetInstance();
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            string a = menu.Text.ToString();
            ToolType tye = op.GetToolNameFromStr(a);
            if (tye != ToolType.None)
            {
                //获取ID内部编号
                int ID = GetNewToolID();
                ToolBase tool;
                UserControl ui;
                op.AddToolFromList(tye, ID, out tool, out ui);
                if (tool != null && ui != null)
                {
                    mStepInfoList.Add(tool.ToolParam.StepInfo);
                    tool.ToolParam.StepInfo.mStepIndex = mStepInfoList.Count();
                    tool.SetDebugWind(mShowWind, mDrawWind);
                    InterfaceUIBase uIBase = (InterfaceUIBase)ui;
                    uIBase.StepInfoList = mStepInfoList;
                    uIBase.ToolParam = tool.ToolParam;
                    uIBase.SetDebugRunWind(null, null);
                    ui.Parent = this.panel_ToolForm;
                    mToolPageList.Insert(mMouseDown + 1, ui);
                    mToolList.Insert(mMouseDown + 1, tool);
                }

                UpdataToolView();
                mStepInfoList = new List<StepInfo>();
                foreach (ToolBase item in mToolList)
                {
                    mStepInfoList.Add(item.ToolParam.StepInfo);
                }
                panel_ToolForm.Controls.Clear();
                mToolMachine.ShowRegionList.Clear();

                UpdataUI();
                if (mToolMachine.ToolCurrImage != null)
                    ToolRun();
            }
        }

        private void FrmProjectSet_Shown(object sender, EventArgs e)
        {
            try
            {
                mMachine = Machine.GetInstance();
                mToolMachine = ToolMachine.GetInstance();
                mImageList = new List<HObject>();
                LoadingHelper.ShowLoadingScreen();
                Application.DoEvents();
                mMouseDown = -1;
                mPathList = new List<string>();
                mStepInfoList = new List<StepInfo>();
                mToolList = new List<ToolBase>();
                mToolPageList = new List<UserControl>();
                //初始化控件
                mShowControl = new HWindowControl();
                mShowControl.Parent = panel_WorkView;
                mShowControl.Height = panel_WorkView.Height;
                mShowControl.Width = panel_WorkView.Width;
                mShowControl.Show();
                //初始化控件
                mToolMachine.Control = panel_WorkView;

                //初始化句柄
                mShowWind = mShowControl.HalconID;
                mDrawWind = mShowControl.HalconWindow;
                mDrawWind.SetFont("Consolas-28");
                mToolMachine.DrawWind = mDrawWind;
                //初始化工具表
                mMachine.XmlTree.GetToolClassNameFromXml();
                mMachine.XmlTree.AddNoteToTreeView(mTreeViewTools, out mToolNameList);
                mTreeViewTools.ExpandAll();
                mTreeViewTools.AllowDrop = true;
                InitcontextMenuStrip();
                //初始化拖拽名
                mCurrDragItemName = "";
                //更新项目名称
                label_CurrProject.Text = "当前项目：" + CurrProject.mProjectName;
                //绑定委托

                mToolMachine.mChangeState += ChangeState;
                mToolMachine.mChangeToolName += ChangeToolName;
                mToolMachine.mChangeToolCostTime += ChangeCostTime;
                mToolMachine.mEnableControlDel += EnableControl;

                //缩放
                mShowControl.HMouseWheel += new HMouseEventHandler(hWindowControl1_HMouseWheel);
                mShowControl.HMouseDown += new HMouseEventHandler(hWindowControl1_HMouseDown);
                mShowControl.HMouseMove += new HMouseEventHandler(hWindowControl1_HMouseMove);

                //打开文件
                mCurrTaskIndex = 1;
                mCurrStepIndex = -1;
                ReadCurrTask(CurrProject.mProjectName, mCurrTaskIndex);
                //传递相机句柄
                //mCurrCamera = mMachine.CamList[mCurrTaskIndex - 1];

                //线程注册
                mIsRun = true;
                mDebugThread = new Thread(new ThreadStart(DebugRun));
                mDebugThread.Start();

                mIsRunning = false;
                mRunMode = 1;
                mCurrTestIndex = 0;
                mAllTestNum = 0;
                mTestNG = 0;
                mTestOK = 0;
                manual = new ManualResetEvent(false);
                mTestRun = true;
                mTestAllRun = false;
                mToolRunThread = new Thread(new ThreadStart(DebugTestRun));
                mToolRunThread.Start();
                OkPath = new List<string>();
                NGPath = new List<string>();
                LoadingHelper.CloseForm();
            }
            catch (Exception ex)
            {
                LoadingHelper.CloseForm();
                MessageBox.Show(ex.Message);
            }
        }
    }
}

