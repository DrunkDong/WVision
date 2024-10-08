﻿using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WTools;

namespace WVision
{
    public class ProjectTask1 : ProjectTaskBase
    {
        Machine mMachine;

        public ProjectTask1()
        {
            mMachine = Machine.GetInstance();
            SaveFolderName = "";
            TaskNmae = "C1";
            Count = 0;
            NgCount = 0;
            OkCount = 0;
            IsStart = false;
            TaskResultQueue = new ConcurrentQueue<string>();
            TaskThreadRunFlag = true;
            TaskRunThread = new Thread(new ThreadStart(ThreadRunTask));
            TaskRunThread.IsBackground = true;
            TaskRunThread.Start();
        }

        public override void ReleaseTool()
        {

        }

        public override void Dispose()
        {
            TaskThreadRunFlag = false;
            TaskRunThread.Join();
        }

        public override void ThreadRunTask()
        {
            while (TaskThreadRunFlag)
            {
                if (Camera == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                if (!IsStart)
                {
                    Thread.Sleep(5);
                    continue;
                }
                if (Camera.ImageBuffLength > 0)
                {
                    try
                    {
                        //结果默认OK
                        int res = 0;
                        //错误工具名称
                        string mErrorToolName = "";
                        //需要显示的区域
                        HOperatorSet.GenEmptyObj(out HObject mToolRegion);
                        //OK的OCR读取字符结果
                        string mOCRChar = "";
                        //取图
                        HObject currImage = Camera.Dequeue();
                        //计时
                        HOperatorSet.CountSeconds(out HTuple S1);
                        //遍历工具
                        foreach (ToolBase item in ToolList)
                        {
                            JumpInfo info;
                            HTuple ss1, ss2;
                            HOperatorSet.CountSeconds(out ss1);
                            res = item.ToolRun(currImage, StepInfoList, false, out info);
                            HOperatorSet.CountSeconds(out ss2);
                            if (CostTime > 300)
                                LogHelper.WriteExceptionLog(TaskNmae + " " + item.ToolParam.ShowName + " 耗时：" + (ss2.D - ss1.D).ToString("f2") + "ms\r\n");
                            //错误工具显示名称
                            if (res != 0)
                            {
                                //获取错误工具显示名称
                                if (item.ToolParam.ToolType == ToolType.HsemanticAI || item.ToolParam.ToolType == ToolType.HObjecDetect1)
                                    mErrorToolName = item.ToolParam.ShowName + info.mAiLabel;
                                else
                                    mErrorToolName = item.ToolParam.ShowName;
                                break;
                            }
                        }
                        //计时
                        HOperatorSet.CountSeconds(out HTuple S2);

                        //总计数增加
                        Count++;
                        if (res != 0)
                        {
                            mMachine.Modbus_Tcp.WriteSingleRegister(0, 6100, 2);
                            NgCount++;
                        }
                        else
                        {
                            mMachine.Modbus_Tcp.WriteSingleRegister(0, 6100, 1);
                            OkCount++;
                        }
                        //外部队列
                        TaskResultQueue.Enqueue(Count + "_1_" + res);
                        //图片名称
                        string name = Count.ToString() + "-" + TaskNmae;


                        //默认路径+检测时间+相机编号+OK/NG+图片名
                        string path = mMachine.SavePath + "\\" + SaveFolderName + "\\" + TaskNmae;


                        SaveImageClass save = new SaveImageClass();
                        save.mImage = currImage.CopyObj(1, 1);
                        save.mSavePath = path;
                        save.mSaveModel = res;
                        save.mSaveName = name;

                        ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImageFuc), save);

                        //显示
                        if (res == 0)
                        {
                            //OK显示图片
                            ToolWind.DispImage(currImage);
                            ToolWind.ShowWindow.SetDraw("margin");
                            ToolWind.ShowWindow.SetColor("green");
                            ToolWind.ShowWindow.SetFont("微软雅黑-Bold-30");
                            if (mToolRegion != null)
                            {
                                //显示区域
                                ToolWind.ShowWindow.DispObj(mToolRegion);
                                mToolRegion.Dispose();
                            }
                            //显示字符
                            ToolWind.ShowWindow.DispText("OK\n" + mOCRChar, "image", 10, 10, "green", "box", "false");
                        }
                        else
                        {
                            //NG显示图片
                            ToolWind.DispImage(currImage);
                            ToolWind.ShowWindow.SetDraw("margin");
                            ToolWind.ShowWindow.SetColor("red");
                            ToolWind.ShowWindow.SetFont("微软雅黑-Bold-30");
                            if (mToolRegion != null)
                            {
                                //显示区域
                                ToolWind.ShowWindow.DispObj(mToolRegion);
                                mToolRegion.Dispose();
                            }
                            //显示字符
                            ToolWind.ShowWindow.DispText("NG:" + mErrorToolName, "image", 10, 10, "red", "box", "false");
                        }
                        //释放图片
                        currImage.Dispose();
                        //计算耗时
                        CostTime = Math.Round((S2.D - S1.D) * 1000, 2);
                        if (CostTime > 300)
                            LogHelper.WriteExceptionLog(TaskNmae + " 总耗时：" + CostTime.ToString("f2") + "ms\r\n");
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteExceptionLog("TaskThread: " + TaskNmae + " " + ex);
                    }
                }
                else
                    Thread.Sleep(5);
            }
        }

        public override void CheckStart()
        {
            IsStart = true;
        }

        public override void CheckStop()
        {
            IsStart = false;
        }

        //private void SaveOKImage(HObject obj, string path, string name)
        //{
        //    try
        //    {
        //        string mFinalFath = path + "\\OK\\";
        //        if (!Directory.Exists(mFinalFath))
        //            Directory.CreateDirectory(mFinalFath);
        //        if (!Directory.Exists(mFinalFath))
        //            Directory.CreateDirectory(mFinalFath);
        //        if (mMachine.SettingInfo.SaveOKMode == 0)
        //            return;
        //        string fomat = "jpeg";
        //        if (mMachine.SettingInfo.SaveOKMode == 1)
        //            fomat = "jpeg";
        //        else if (mMachine.SettingInfo.SaveOKMode == 2)
        //            fomat = "png";
        //        else if (mMachine.SettingInfo.SaveOKMode == 3)
        //            fomat = "bmp";
        //        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //        {
        //            HOperatorSet.WriteImage(obj, fomat, 0, mFinalFath + name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteExceptionLog("Save OK Image Error:" + ex);
        //    }
        //}

        //private void SaveNGImage(HObject obj, string path, string name)
        //{
        //    try
        //    {
        //        string mFinalFath = path + "\\NG\\";
        //        if (!Directory.Exists(mFinalFath))
        //            Directory.CreateDirectory(mFinalFath);
        //        if (mMachine.SettingInfo.SaveNGMode == 0)
        //            return;
        //        string fomat = "jpeg";
        //        if (mMachine.SettingInfo.SaveNGMode == 1)
        //            fomat = "jpeg";
        //        else if (mMachine.SettingInfo.SaveNGMode == 2)
        //            fomat = "png";
        //        else if (mMachine.SettingInfo.SaveNGMode == 3)
        //            fomat = "bmp";
        //        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //        {
        //            HOperatorSet.WriteImage(obj, fomat, 0, mFinalFath + name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteExceptionLog("Save NG Image Error:" + ex);
        //    }
        //}

        
    }
}
