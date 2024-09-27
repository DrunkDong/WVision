using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;
using HalconDotNet;
using WCommonTools;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WTools;
using System.Diagnostics;
using System.Data;
using WControls;
using ADOX;

namespace WVision
{
    public class ProjectTask
    {
        ushort mLastPosition;
        int mLatchCount;
        double mCount;
        double mNgCount;
        double mOkCount;
        double mCostTime;
        string mTaskNmae;
        string mProjectName;
        string mSaveFolderName;
        bool mTaskThreadRunFlag;
        bool mIsStart;
        string mSelectModelName;
        private DataGridView mDataview;
        int RowCount;
        int ColCount;
        int mCircleNum;
        bool mResetShow;

        public List<int> res;
        DataTable mTable;
        List<int> resList;
       
        Machine mMachine;
        PictureBox mControl;
        Panel mPanelWorkView;
        HaikCamera mCamera;
        ConcurrentQueue<string> mTaskResultQueue;
        Thread mTaskRunThread;

        HObject CurrentImage;
        Bitmap CurrentBitmap;

        ProjectResultProcess mResultProcess;
        List<ToolBase> mToolList;
        List<StepInfo> mStepInfoList;

        double mPreLatchData;
        ConcurrentQueue<LatchSt> mLatchQueue;
        int mLatchID;

        //声明返回状态
        List<int> Restaus;
        //声明返回实际总数量（不包含在同一坐标下的返回值）
        int mRealTotalNum;

        public bool TaskThreadRunFlag
        {
            get => mTaskThreadRunFlag;
            set => mTaskThreadRunFlag = value;
        }

        public ConcurrentQueue<string> TaskResultQueue
        {
            get => mTaskResultQueue;
            set => mTaskResultQueue = value;
        }

        public double Count
        {
            get => mCount;
            set => mCount = value;
        }

        public double NgCount
        {
            get => mNgCount;
            set => mNgCount = value;
        }

        public double OkCount
        {
            get => mOkCount;
            set => mOkCount = value;
        }
        public string SaveFolderName
        {
            get => mSaveFolderName;
            set => mSaveFolderName = value;
        }
        public string ProjectName
        {
            get => mProjectName;
            set => mProjectName = value;
        }
        public PictureBox Control
        {
            get => mControl;
            set => mControl = value;
        }
        public Panel PanelWorkView
        {
            get => mPanelWorkView;
            set => mPanelWorkView = value;
        }
        public HaikCamera Camera
        {
            get => mCamera;
            set => mCamera = value;
        }
        public string TaskNmae
        {
            get => mTaskNmae;
            set => mTaskNmae = value;
        }

        public double CostTime
        {
            get => mCostTime;
            set => mCostTime = value;
        }
        public List<ToolBase> ToolList
        {
            get => mToolList;
            set => mToolList = value;
        }
        public List<StepInfo> StepInfoList
        {
            get => mStepInfoList;
            set => mStepInfoList = value;
        }
        public bool IsStart
        {
            get => mIsStart;
            set => mIsStart = value;
        }
        public ConcurrentQueue<LatchSt> LatchQueue
        {
            get => mLatchQueue;
            set => mLatchQueue = value;
        }
        public double PreLatchData
        {
            get => mPreLatchData;
            set => mPreLatchData = value;
        }
        public int LatchID
        {
            get => mLatchID;
            set => mLatchID = value;
        }
        public int LatchCount
        { 
            get => mLatchCount; 
            set => mLatchCount = value;
        }
        public string SelectModelName 
        { 
            get => mSelectModelName; 
            set => mSelectModelName = value; 
        }
        public DataTable Table
        {
            get => mTable;
            set => mTable = value;
        }
        public int RealTotalNum 
        
        { 
            get => mRealTotalNum; 
            set => mRealTotalNum = value;
        }
        public int CircleNum
        {
            get => mCircleNum;
            set => mCircleNum = value;
        }
        public DataGridView Dataview 
        { 
            get => mDataview; 
            set => mDataview = value; 
        }
        public ushort LastPosition 
        {
            get => mLastPosition; 
            set => mLastPosition = value; 
        }

        public ProjectTask()
        {
            mMachine = Machine.GetInstance();
            ToolList = new List<ToolBase>();
            StepInfoList = new List<StepInfo>();
            mResultProcess = new ProjectResultProcess();
            mSaveFolderName = "";
            mProjectName = "";
            mSelectModelName = "";
            mTaskResultQueue = new ConcurrentQueue<string>();
            mLatchQueue = new ConcurrentQueue<LatchSt>();
            mCount = 0;
            mNgCount = 0;
            mOkCount = 0;
            mTable = new DataTable();
            mTable.Columns.Add(" ", typeof(string));
            mTable.Columns.Add("CCD1", typeof(object));
            resList = new List<int>();
            Restaus = new List<int>();
            IsStart = false;
            res = new List<int>();
            mTaskThreadRunFlag = true;
            mTaskRunThread = new Thread(new ThreadStart(mThreadRunTask));
            mTaskRunThread.Start();
            mCircleNum = 1;
        }

        private void mThreadRunTask()
        {
            while (mTaskThreadRunFlag)
            {
                try
                {
                    if (mCamera == null)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    if (!mIsStart)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    if (mCamera.ImageBuffLength > 0)
                    {
                        HTuple second1, second2;
                        HOperatorSet.CountSeconds(out second1);
                        CurrentImage = mCamera.Dequeue();
                        //累计
                        mCount++;
                        //返回值代号
                        //int res = 0;
                        //显示字符串
                        List<string> mShowStrList = new List<string>();
                        //显示耗时
                        List<string> mErrorCode = new List<string>();
                        ////错误工具名称
                        string mErrorToolName = "";

                        int res = 0;
                       
                        if (mToolList.Count > 0)
                        {
                            foreach (ToolBase item in mToolList)
                            {
                                JumpInfo info;
                                HTuple ss1, ss2;
                                HOperatorSet.CountSeconds(out ss1);
                                res = item.ToolRun(CurrentImage, mStepInfoList, false, out info);
                                HOperatorSet.CountSeconds(out ss2);
                                mErrorCode.Add(TaskNmae + ":  " + item.ToolParam.StepInfo.mStepIndex.ToString() + " _ " + item.ToolParam.ShowName
                                    + "    耗时：" + ((ss2.D - ss1.D) * 1000).ToString());
                                if (res != 0)
                                {
                                    HTuple sss1, sss2;
                                    HOperatorSet.CountSeconds(out sss1);
                                    string showname;
                                    if (item.ToolParam.ToolType == ToolType.HsemanticAI || item.ToolParam.ToolType == ToolType.HObjecDetect1)
                                    {
                                        mErrorToolName = item.ToolParam.StepInfo.mStepIndex.ToString() + "_" + item.ToolParam.ShowName + "_" + info.mAiLabel;
                                        showname = item.ToolParam.ShowName + info.mAiLabel;
                                    }
                                    else
                                    {
                                        mErrorToolName = item.ToolParam.StepInfo.mStepIndex.ToString() + "_" + item.ToolParam.ShowName;
                                        showname = item.ToolParam.ShowName;
                                    }
                                    mShowStrList.Add(showname);
                                    if (mMachine.SettingInfo.IsSaveNgByClass)
                                    {
                                        mShowStrList.Add(item.ToolParam.ShowName);
                                    }
                                    HOperatorSet.CountSeconds(out sss2);
                                    mErrorCode.Add("list add 耗时：" + ((sss2.D - sss1.D) * 1000).ToString("F3"));
                                    break;
                                }
                            }

                        }
                        else
                        {
                            mShowStrList.Add("OK");
                        }
                        HOperatorSet.CountSeconds(out second2);
                        ushort[] GetXY = mMachine.Modbus_Tcp.ReadHoldingRegisters(0, 6004, 1);//当前拍照是托盘第几个料
                        ushort[] GetPortNum = mMachine.Modbus_Tcp.ReadHoldingRegisters(0, 6006, 1);//当前是第几个托盘
                        ushort[] GetAllNum = mMachine.Modbus_Tcp.ReadHoldingRegisters(0, 6008, 1);//当前是一个托盘的总数
                        ushort[] GetNums = mMachine.Modbus_Tcp.ReadHoldingRegisters(0, 6010, 1);//当前配方有几个托盘
                        //检索是否清零
                        if (mResetShow)
                        {
                            mResetShow = false;
                            mDataview.Invoke(new Action(() =>
                            {
                                foreach (var item in mMachine.CellList)
                                {
                                    item.Style.BackColor = Color.White;
                                }
                            }));
                        }
                        int Index = 1;
                        if (GetPortNum[0] == 2) 
                        {
                            //若有两个托盘，则索引为料索引+托盘总数
                            Index = GetXY[0] + GetAllNum[0];
                        }
                        else
                        {    
                            //若一个托盘，则索引为料索引
                            Index = GetXY[0];
                        }
                        if (res != 0)
                        {
                            mDataview.Invoke(new Action(() => { mMachine.CellList[Index - 1].Style.BackColor = Color.Red; }));
                            mMachine.Modbus_Tcp.WriteSingleRegister(0, 6100, 2);
                            mNgCount++;
                        }
                        else
                        {
                            mDataview.Invoke(new Action(() => { mMachine.CellList[Index - 1].Style.BackColor = Color.Green; }));
                            mMachine.Modbus_Tcp.WriteSingleRegister(0, 6100, 1);
                            mOkCount++;
                        }
                        if (GetNums[0] == 2)
                        {
                            //若料索引等于一个托盘的总数
                            if ((GetXY[0] == GetAllNum[0]) && GetPortNum[0] == 2) 
                            {
                                mResetShow = true;//通知下一次清除标志
                            }
                        }
                        else
                        {
                            //若料索引等于一个托盘的总数
                            if (GetXY[0] == GetAllNum[0])
                            {
                                mResetShow = true;//通知下一次清除标志
                            }
                        }

                        //结果入队
                        mTaskResultQueue.Enqueue(mCount + "_0_" + res);
                        //默认路径+检测时间+相机编号+OK/NG+图片名
                        string path = mMachine.SavePath + "\\" + SaveFolderName + "-" + mMachine.CurrProjectInfo.mProjectName + "\\" + TaskNmae;
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        string name = mCount.ToString() + "-" + mTaskNmae;

                        //整理结果汇总至结果处理线程
                        ResultBuff buf = new ResultBuff();
                        buf.mResBuff = CurrentImage;
                        buf.mSavePath = path;
                        buf.mResState = res;
                        buf.mName = name;
                        buf.mStrShowList = mShowStrList;
                        buf.mErrorToolName = mErrorToolName;
                        mResultProcess.Enqueue(buf);
                        Thread.Sleep(1);
                        HOperatorSet.CountSeconds(out second2);
                        mCostTime = (second2.D - second1.D) * 1000;
                        
                        if ((second2.D - second1.D) * 1000 > 200)
                        {
                            if (mErrorCode.Count > 0)
                            {
                                for (int i = 0; i < mErrorCode.Count; i++)
                                {
                                    LogHelper.WriteExceptionLog(mErrorCode[i]);
                                }
                                LogHelper.WriteExceptionLog("总耗时：" + ((second2.D - second1.D) * 1000).ToString("f3") + "ms\r\n");
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteExceptionLog("TaskThread: " + ex);
                }
            }
        }

        public void Close()
        {
            mTaskThreadRunFlag = false;
            mTaskRunThread.Join();
            mResultProcess.Close();
        }

        public void InitResProcess(HShowWindow mHindow)
        {
            mResultProcess.SetShowWind(mHindow);
        }

        public void InitSaveMode(SaveImageMode mod) 
        {
            mResultProcess.SaveImageMode = mod;
        }
    }

    public struct LatchSt 
    {
        public int LatchNum;
        public double LatchValue;
    }

    
}
