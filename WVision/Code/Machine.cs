using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCommonTools;
using WControls;
using HalconDotNet;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using WTools;

namespace WVision
{
    public delegate void SoceketReceiveDel(string mes);
    public class Machine : SingletonTemplate<Machine>
    {
        public readonly string SettingInfoSavePath = AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig\\Setting.dat";

        int mOkNumber;
        int mNGNumber;
        int mAllNumber;
        int mDataViewNum;

        SerialPortTool mRs232_Light1;
        SerialPortTool mRs232_Light2;

        List<HWindow> mHWindowList;
        List<HDebugWindow> mHWindowControlList;
        List<DahengCamera> mCamList;
        List<ProjectInfo> mProjectInfoList;
        ProjectInfo mCurrProjectInfo;
        UserAccess mUserAccessOp;
        TCPClient mSocket_Client;
        ModbusTcp mModbus_Tcp;
        ZmotionOperation mZmotion;
        SettingInfo mSettingInfo;
        string mSavePath;
        SoceketReceiveDel mSoceketReceive;
      
        CamParam mCameraParam;
        bool mIsGrabDebug;

        FrmProjectSet mFrmProject;

        FrmWait mWaiting;
        XmlToolTree mXmlTree;
        List<ProjectTaskBase> mTaskList;
        List<DataGridViewCell> mCellList;

        public List<HWindow> HWindowList
        {
            get => mHWindowList;
            set => mHWindowList = value;
        }
        public List<HDebugWindow> HWindowControlList
        {
            get => mHWindowControlList;
            set => mHWindowControlList = value;
        }
        public List<DahengCamera> CamList
        {
            get => mCamList;
            set => mCamList = value;
        }
        public ProjectInfo CurrProjectInfo
        {
            get => mCurrProjectInfo;
            set => mCurrProjectInfo = value;
        }
        public List<ProjectInfo> ProjectInfoList
        {
            get => mProjectInfoList;
            set => mProjectInfoList = value;
        }
        public UserAccess UserAccessOp
        {
            get => mUserAccessOp;
            set => mUserAccessOp = value;
        }
        public TCPClient Socket_Client
        {
            get => mSocket_Client;
            set => mSocket_Client = value;
        }
        public ModbusTcp Modbus_Tcp
        {
            get => mModbus_Tcp;
            set => mModbus_Tcp = value;
        }
        public ZmotionOperation Zmotion
        {
            get => mZmotion;
            set => mZmotion = value;
        }
        public SettingInfo SettingInfo
        {
            get => mSettingInfo;
            set => mSettingInfo = value;
        }
        public string SavePath
        {
            get => mSavePath;
            set => mSavePath = value;
        }
        public SoceketReceiveDel SoceketReceive
        {
            get => mSoceketReceive;
            set => mSoceketReceive = value;
        }
        public int OkNumber
        {
            get => mOkNumber;
            set => mOkNumber = value;
        }
        public int NGNumber
        {
            get => mNGNumber;
            set => mNGNumber = value;
        }
        public int AllNumber
        {
            get => mAllNumber;
            set => mAllNumber = value;
        }
        public CamParam CameraParam
        {
            get => mCameraParam;
            set => mCameraParam = value;
        }
        public bool IsGrabDebug
        {
            get => mIsGrabDebug;
            set => mIsGrabDebug = value;
        }
        public FrmProjectSet FrmProject
        {
            get => mFrmProject;
            set => mFrmProject = value;
        }

        public FrmWait Waiting
        {
            get => mWaiting;
            set => mWaiting = value;
        }

        public XmlToolTree XmlTree
        {
            get => mXmlTree;
            set => mXmlTree = value;
        }

        public List<ProjectTaskBase> TaskList
        {
            get => mTaskList;
            set => mTaskList = value;
        }
        public int DataViewNum
        { get => mDataViewNum;
          set => mDataViewNum = value; 
        }
        public List<DataGridViewCell> CellList 
        { 
            get => mCellList; 
            set => mCellList = value; 
        }
        public SerialPortTool Rs232_Light1
        {
            get => mRs232_Light1;
            set => mRs232_Light1 = value;
        }
        public SerialPortTool Rs232_Light2
        {
            get => mRs232_Light2;
            set => mRs232_Light2 = value;
        }

        public Machine()
        {
            mHWindowList = new List<HWindow>();
            mHWindowControlList = new List<HDebugWindow>();
            mCamList = new List<DahengCamera>();
            mCurrProjectInfo = new ProjectInfo();
            mProjectInfoList = new List<ProjectInfo>();
            mUserAccessOp = new UserAccess();
            Socket_Client = new TCPClient();
            mModbus_Tcp = new ModbusTcp();
            mZmotion = new ZmotionOperation();
            mSettingInfo = new SettingInfo();
            mSavePath = "E:\\Images\\";
            mSoceketReceive = null;
            mCameraParam = new CamParam();
            mIsGrabDebug = true;
            Rs232_Light1 = new SerialPortTool();
            Rs232_Light2 = new SerialPortTool();

            mXmlTree = new XmlToolTree(AppDomain.CurrentDomain.BaseDirectory+ "\\AppConfig\\");
        }

        public bool DeSerializeFuc<T>(string path, out T param) where T : class, new()
        {
            param = new T();
            if (!File.Exists(path))
                return false;

            FileStream fStream = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter soapFormat = new BinaryFormatter();
                param = (T)soapFormat.Deserialize(fStream);
                fStream.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                fStream.Close();
                return false;
            }
        }
        public bool SerializeFuc<T>(string path, T param)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig").Create();
            FileStream fStream = new FileStream(path, FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter Format = new BinaryFormatter();
                Format.Serialize(fStream, param);
                fStream.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                fStream.Close();
                return false;
            }
        }

        public bool DeSoapSerializeFuc<T>(string path, out T param) where T : class, new()
        {
            param = new T();
            if (!File.Exists(path))
                return false;
            FileStream fStream = new FileStream(path, FileMode.Open);
            try
            {
                SoapFormatter soapFormat = new SoapFormatter();
                param = (T)soapFormat.Deserialize(fStream);
                fStream.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                fStream.Close();
                return false;
            }
        }

        public bool SoapSerializeFuc<T>(string path, T param)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig").Create();
            FileStream fStream = new FileStream(path, FileMode.OpenOrCreate);
            try
            {
                SoapFormatter Format = new SoapFormatter();
                Format.Serialize(fStream, param);
                fStream.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                fStream.Close();
                return false;
            }
        }


        public bool GetRemainMemeory(string str_HardDiskName) //磁盘号
        {
            try
            {
                long freeSpace = new long();
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);//转GB
                    }
                }
                if (freeSpace < SettingInfo.LowDiskCapacity)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }


        public ResStatus ReadTask(string filename, out List<ToolBase> toolList)
        {
            ToolOperation op = new ToolOperation();
            if (op.ReadToolList(filename, out toolList) != ResStatus.OK)
            {
                MessageBox.Show("工程读取失败", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return ResStatus.Error;
            }
            return ResStatus.OK;
        }
        public ResStatus SaveTask(string filename, List<ToolBase> toolList)
        {
            ToolOperation op = new ToolOperation();
            if (op.SaveToolList(filename, toolList) != ResStatus.OK)
            {
                MessageBox.Show("工程保存失败", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return ResStatus.Error;
            }
            else
            {
                MessageBox.Show("工程保存成功");
                return ResStatus.OK;
            }

        }

        public void CreateCheckTask(int num)
        {
            TaskList = new List<ProjectTaskBase>();
            //新建task
            ProjectTask1 task1 = new ProjectTask1();
            ProjectTask2 task2 = new ProjectTask2();
            ProjectTask3 task3 = new ProjectTask3();
            ProjectTask4 task4 = new ProjectTask4();
            //ProjectTask5 task5 = new ProjectTask5();
            //ProjectTask6 task6 = new ProjectTask6();

            TaskList.Add(task1);
            TaskList.Add(task2);
            TaskList.Add(task3);
            TaskList.Add(task4);
            //TaskList.Add(task5);
            //TaskList.Add(task6);
        }
        public int ReadCheckTask(string mCurrProjectName)
        {
            try
            {
                for (int i = 0; i < TaskList.Count; i++)
                {
                    Application.DoEvents();
                    List<ToolBase> mToolList = new List<ToolBase>();
                    List<StepInfo> mStepInfoList = new List<StepInfo>();
                    //文件名
                    string path = Application.StartupPath + "\\Project\\" + mCurrProjectName + "\\" + (i + 1) + ".dat";
                    if (!File.Exists(path))
                        continue;
                    //从文件中读取Tool链表
                    ReadTask(path, out mToolList);
                    //预初始化Tool状态
                    if (mToolList.Count > 0)
                    {
                        foreach (ToolBase item in mToolList)
                        {
                            mStepInfoList.Add(item.ToolParam.StepInfo);
                            //初始化AI模型
                            if (item.ToolParam.ToolType == WTools.ToolType.HObjecDetect1 ||
                                item.ToolParam.ToolType == WTools.ToolType.HsemanticAI ||
                                item.ToolParam.ToolType == WTools.ToolType.HClassifiyAI ||
                                item.ToolParam.ToolType == WTools.ToolType.DeepOcr)
                            {
                                //加载模型
                                item.InitAiResources();
                            }
                        }
                    }
                    //赋值对象
                    TaskList[i].ToolList = mToolList;
                    TaskList[i].StepInfoList = mStepInfoList;
                }
                return 0;
            }
            catch (System.Exception)
            {
                return -1;
            }

        }
    }
}
