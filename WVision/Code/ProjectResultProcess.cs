using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using HalconDotNet;
using WCommonTools;
using WControls;


namespace WVision
{
    public class ProjectResultProcess
    {
        Machine mMachine;
        ConcurrentQueue<ResultBuff> mResBuffQueue;
        ConcurrentQueue<SaveImageBuff> mSaveImageBuffQueue;
        HShowWindow mShowWind;
        Thread mSaveResultThread;
        Thread mSaveImageThread;
        bool mThreadRun;
        SaveImageMode mSaveImageMode;

        public SaveImageMode SaveImageMode 
        { 
            get => mSaveImageMode;
            set => mSaveImageMode = value;
        }

        public ProjectResultProcess()
        {
            mMachine = Machine.GetInstance();
            mResBuffQueue = new ConcurrentQueue<ResultBuff>();
            mSaveImageBuffQueue = new ConcurrentQueue<SaveImageBuff>();
            mThreadRun = true;
            mSaveResultThread = new Thread(new ThreadStart(SaveResultProcess));
            mSaveResultThread.Start();
            mSaveImageThread = new Thread(new ThreadStart(SaveImageProcess));
            mSaveImageThread.Start();
        }

        public void Enqueue(ResultBuff obj)
        {
            mResBuffQueue.Enqueue(obj);
        }

        public void SetShowWind(HShowWindow wind)
        {
            this.mShowWind = wind;
        }
        public void Close()
        {
            mThreadRun = false;
            mSaveResultThread.Join();
            mSaveImageThread.Join();
        }

        private void SaveResultProcess()
        {
            while (mThreadRun)
            {
                if (mResBuffQueue.Count > 0)
                {
                    try
                    {
                        ResultBuff buff;
                        SaveImageBuff buff2 = new SaveImageBuff();
                        mResBuffQueue.TryDequeue(out buff);
                        //显示结果
                        if (buff.mResState != 0)
                        {    
                            //显示图片
                            HOperatorSet.SetSystem("flush_graphic", "false");
                            mShowWind.ShowWindow.SetColor("red");
                            mShowWind.ShowWindow.SetFont("微软雅黑-Bold-40");
                            mShowWind.DispObj(buff.mResBuff);
                            HOperatorSet.SetSystem("flush_graphic", "true");
                            if (buff.mStrShowList.Count > 0) 
                            {
                                mShowWind.Window.HalconWindow.DispText(buff.mStrShowList[0], "image", 10, 10, "red", "box", "false");
                            }
                        }
                        else
                        {   
                            //显示图片
                            HOperatorSet.SetSystem("flush_graphic", "false");
                            mShowWind.ShowWindow.SetColor("green");
                            mShowWind.ShowWindow.SetFont("微软雅黑-Bold-40");
                            mShowWind.DispObj(buff.mResBuff);
                            HOperatorSet.SetSystem("flush_graphic", "true");
                            mShowWind.Window.HalconWindow.DispText("OK", "image", 10, 10, "green", "box", "false");
                        }
                        buff2.mResBuff = buff.mResBuff;
                        buff2.mResState = buff.mResState;
                        buff2.mSavePath = buff.mSavePath;
                        buff2.mErrorToolName = buff.mErrorToolName;
                        buff2.mName = buff.mName;
                        mSaveImageBuffQueue.Enqueue(buff2);
                        Thread.Sleep(1);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteErrorLog(ex);
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }


        private void SaveImageProcess()
        {
            while (mThreadRun)
            {
                if (mSaveImageBuffQueue.Count > 0)
                {

                    SaveImageBuff buff;
                    mSaveImageBuffQueue.TryDequeue(out buff);
                    if (GetRemainMemeory("E") == 0) 
                    {
                        //存图
                        if (buff.mResState != 0)
                        {
                            if (SaveImageMode.SaveByClass)
                            {
                                SaveNGImageByClass(buff.mResBuff, buff.mSavePath, buff.mErrorToolName, buff.mName);
                            }
                            else
                            {
                                SaveNGImage(buff.mResBuff, buff.mSavePath, buff.mName);
                            }
                        }
                        else
                        {
                            SaveOKImage(buff.mResBuff, buff.mSavePath, buff.mName);
                        }
                    }
                    //释放资源
                    buff.mResBuff.Dispose();
                    Thread.Sleep(1);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void SaveOKImage(HObject obj, string path, string name)
        {
            try
            {
                string mFinalFath = path + "\\OK\\";
                if (!Directory.Exists(mFinalFath))
                    Directory.CreateDirectory(mFinalFath);
                if (!Directory.Exists(mFinalFath))
                    Directory.CreateDirectory(mFinalFath);
                if (mMachine.SettingInfo.SaveOKMode == 0)
                    return;
                string fomat = "jpeg";
                if (mMachine.SettingInfo.SaveOKMode == 1)
                    fomat = "jpeg";
                else if (mMachine.SettingInfo.SaveOKMode == 2)
                    fomat = "png";
                else if (mMachine.SettingInfo.SaveOKMode == 3)
                    fomat = "bmp";
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteImage(obj, fomat, 0, mFinalFath + name);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog("Save OK Image Error:" + ex);
            }
        }
        private void SaveNGImage(HObject obj, string path, string name)
        {
            try
            {
                string mFinalFath = path + "\\NG\\";
                if (!Directory.Exists(mFinalFath))
                    Directory.CreateDirectory(mFinalFath);
                if (mMachine.SettingInfo.SaveNGMode == 0)
                    return;
                string fomat = "jpeg";
                if (mMachine.SettingInfo.SaveNGMode == 1)
                    fomat = "jpeg";
                else if (mMachine.SettingInfo.SaveNGMode == 2)
                    fomat = "png";
                else if (mMachine.SettingInfo.SaveNGMode == 3)
                    fomat = "bmp";
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteImage(obj, fomat, 0, mFinalFath + name);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog("Save NG Image Error:" + ex);
            }
        }

        private void SaveNGImageByClass(HObject obj, string path, string folderName, string name)
        {
            try
            {
                int num = SaveImageMode.SaveNgMode;
                if (num == 0)
                    return;
                string mFinalFath = path + "\\NgClass\\" + folderName + "\\";
                if (!Directory.Exists(mFinalFath))
                    Directory.CreateDirectory(mFinalFath);
                if (mMachine.SettingInfo.SaveNGMode == 0)
                    return;
                string fomat = "jpeg";
                if (mMachine.SettingInfo.SaveNGMode == 1)
                    fomat = "jpeg";
                else if (mMachine.SettingInfo.SaveNGMode == 2)
                    fomat = "png";
                else if (mMachine.SettingInfo.SaveNGMode == 3)
                    fomat = "bmp";
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteImage(obj, fomat, 0, mFinalFath + name);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog("Save NG Image Error:" + ex);
            }
        }

        private int GetRemainMemeory(string str_HardDiskName) //磁盘号
        {
            try
            {
                long freeSpace = new long();
                str_HardDiskName = str_HardDiskName + ":\\";
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (drive.Name == str_HardDiskName)
                    {
                        freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);//转GB
                    }
                }
                if (freeSpace < mMachine.SettingInfo.LowDiskCapacity)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return -1;
            }
        }
    }

    public struct ResultBuff
    {
        public HObject mResBuff;
        public int mResState;
        public string mSavePath;
        public string mName;
        public string mErrorToolName;
        public List<string> mStrShowList;
    }
    public struct SaveImageBuff
    {
        public HObject mResBuff;
        public int mResState;
        public string mSavePath;
        public string mName;
        public string mErrorToolName;
    }

    [Serializable]
    public struct SaveImageMode 
    {
        public int SaveOKMode;
        public int SaveNgMode;
        public bool SaveByClass;
    }

    [Serializable]
    public class SaveImageModeConfig 
    {
        public SaveImageMode[] mSaveImageArray;
        public SaveImageModeConfig()
        {
            mSaveImageArray = new SaveImageMode[9];

        }
    }
}
