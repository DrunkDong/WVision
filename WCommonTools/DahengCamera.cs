using GxIAPINET;
using HalconDotNet;
using log4net;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WCommonTools
{
    public class DahengCamera
    {
        public DahengCamera() 
        {
            mWidth = 0;
            mHeight = 0;
            mImageScale = 0;
            mImageCount = 0;
            mSN = "";
            mPixelDeep = "Bpp10";
            mMonoFlag = false;
            mImageBuff = new Queue<HObject>();
            mQueueLock = new object();
        }

        private int mWidth;
        private int mHeight;
        private string mSN;
        private bool mMonoFlag;
        private string mPixelDeep;
        private int mImageCount;
        private double mImageScale;

        private Queue<HObject> mImageBuff;                                          ///<图像队列>
        private static IGXFactory mobjIGXFactory = null;                            ///<Factory对像
        private IGXDevice mobjIGXDevice = null;                                     ///<设备对像
        private IGXStream mobjIGXStream = null;                                     ///<流对像
        private IGXFeatureControl mobjIGXFeatureControl = null;

        private object mQueueLock;

        public Queue<HObject> ImageBuff
        {
            get => mImageBuff;
            set => mImageBuff = value;
        }
        public string SerialNum
        {
            get { return mSN; }
            set { mSN = value; }
        }

        public bool MonoFlag
        {
            get => mMonoFlag;
            set => mMonoFlag = value;
        }

        public int ImageWidth
        {
            get { return mWidth; }
        }

        public int ImageHeight
        {
            get { return mHeight; }
        }

        public int ImageCount
        {
            get => mImageCount;
            set => mImageCount = value;
        }

        public int ImageBuffLength
        {
            get
            {
                return mImageBuff.Count;
            }
        }
        public double ImageScale
        {
            get => mImageScale;
            set => mImageScale = value;
        }

        public static bool QueryCamera(out List<CameraInfo> CameraInfolist) 
        {
            CameraInfolist = new List<CameraInfo>();

            try
            {
                //枚举所有相机
                List<IGXDeviceInfo> listGXDeviceInfo = new List<IGXDeviceInfo>();
                mobjIGXFactory = IGXFactory.GetInstance();
                mobjIGXFactory.Init();

                mobjIGXFactory.UpdateDeviceList(500, listGXDeviceInfo);

                foreach (IGXDeviceInfo item in listGXDeviceInfo)
                {
                    CameraInfo info = new CameraInfo();
                    info.CamName = item.GetUserID();
                    if (item.GetDeviceClass() == GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_U3V)
                        info.CamType = "USB3";
                    else if (item.GetDeviceClass() == GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_GEV)
                        info.CamType = "Gige";
                    else
                        info.CamType = "";
                    info.CamSerialNumber = item.GetSN();
                    CameraInfolist.Add(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog("Query Cameras Errors", ex);
                return false;
            }
        }

        public static bool DeInitLib()
        {
            if (null != mobjIGXFactory)
            {
                mobjIGXFactory.Uninit();
            }
            return true;
        }

        public bool OpenCamera(string SerialNumber)
        {
            if (null != mobjIGXStream)
            {
                mobjIGXStream.Close();
                mobjIGXStream = null;
            }

            if (null != mobjIGXDevice)
            {
                mobjIGXDevice.Close();
                mobjIGXDevice = null;
            }

            mSN = SerialNumber;
            try
            {
                mobjIGXDevice = mobjIGXFactory.OpenDeviceBySN(SerialNumber, GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                mobjIGXFeatureControl = mobjIGXDevice.GetRemoteFeatureControl();

                //打开流
                if (null != mobjIGXDevice)
                {
                    mobjIGXStream = mobjIGXDevice.OpenStream(0);
                }

                //RegisterCaptureCallback第一个参数属于用户自定参数(类型必须为引用
                //类型)，若用户想用这个参数可以在委托函数中进行使用
                mobjIGXStream.RegisterCaptureCallback(this, OnDahengCameraFrameReceived);
                //确定图像格式
                if (mobjIGXFeatureControl.GetEnumFeature("PixelFormat").GetValue() == "Mono8")
                {
                    MonoFlag = true;
                }
                else
                {
                    MonoFlag = false;
                    mPixelDeep = mobjIGXFeatureControl.GetEnumFeature("PixelSize").GetValue();
                }
                //确定图像大小
                mWidth = (int)mobjIGXFeatureControl.GetIntFeature("Width").GetValue();
                mHeight = (int)mobjIGXFeatureControl.GetIntFeature("Height").GetValue();
                mImageScale = Convert.ToDouble((double)mHeight / (double)mWidth);//图像比例
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }

            return true;
        }

        public bool CloseCamera()
        {
            try
            {
                //关闭设备
                if (null != mobjIGXDevice)
                {
                    //注销采集回调函数
                    mobjIGXStream.UnregisterCaptureCallback();
                    mobjIGXDevice.Close();
                    mobjIGXDevice = null;
                    mobjIGXStream = null;
                    mobjIGXFeatureControl = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool StartGrab()
        {
            try
            {
                if (null != mobjIGXStream)
                {
                    mImageBuff.Clear();
                    mobjIGXStream.StartGrab();
                }

                //发送开采命令
                if (null != mobjIGXFeatureControl)
                {
                    mobjIGXFeatureControl.GetCommandFeature("AcquisitionStart").Execute();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool StopGrab()
        {
            try
            {
                //发送停采命令
                if (null != mobjIGXFeatureControl)
                {
                    mobjIGXFeatureControl.GetCommandFeature("AcquisitionStop").Execute();
                }

                //关闭采集流通道
                if (null != mobjIGXStream)
                {
                    mobjIGXStream.StopGrab();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool SetCameraGain(float Gain)
        {
            try
            {
                if (Gain > 0 && Gain < 1023)
                {
                    mobjIGXFeatureControl.GetFloatFeature("Gain").SetValue(Gain);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;

            }
        }

        public bool SetCameraExposure(float Exposure)
        {
            try
            {
                if (Exposure > 1 && Exposure < 1000000)
                {
                    mobjIGXFeatureControl.GetFloatFeature("ExposureTime").SetValue(Exposure);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        public bool SetTriggerDelay(float delay)
        {
            try
            {
                mobjIGXFeatureControl.GetFloatFeature("TriggerDelay").SetValue(delay);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        public bool GetCameraExposure(out FloatValue mFloatValue)
        {
            mFloatValue = new FloatValue();
            try
            {
                mFloatValue.FloatCurr = (float)mobjIGXFeatureControl.GetFloatFeature("ExposureTime").GetValue();
                mFloatValue.FloarMin = (float)mobjIGXFeatureControl.GetFloatFeature("ExposureTime").GetMin();
                mFloatValue.FloatMax = (float)mobjIGXFeatureControl.GetFloatFeature("ExposureTime").GetMax();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        public bool GetCameraGain(out FloatValue mFloatValue)
        {
            mFloatValue = new FloatValue();
            try
            {
                mFloatValue.FloatCurr = (float)mobjIGXFeatureControl.GetFloatFeature("Gain").GetValue();
                mFloatValue.FloarMin = (float)mobjIGXFeatureControl.GetFloatFeature("Gain").GetMin();
                mFloatValue.FloatMax = (float)mobjIGXFeatureControl.GetFloatFeature("Gain").GetMax();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        public bool GetTriggerDelay(out FloatValue mFloatValue)
        {
            mFloatValue = new FloatValue();
            try
            {
                mFloatValue.FloatCurr = (float)mobjIGXFeatureControl.GetFloatFeature("TriggerDelay").GetValue();
                mFloatValue.FloarMin = (float)mobjIGXFeatureControl.GetFloatFeature("TriggerDelay").GetMin();
                mFloatValue.FloatMax = (float)mobjIGXFeatureControl.GetFloatFeature("TriggerDelay").GetMax();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        public bool SetCameraMode(TriggerMode Mode)
        {

            try
            {
                if (Mode == TriggerMode.Mode_Continue)
                {
                    mobjIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("Off");
                }
                else
                {
                    mobjIGXFeatureControl.GetEnumFeature("TriggerMode").SetValue("On");
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }

        }

        public bool SetTriggerSource(TriggerSource Source)
        {
            try
            {
                if (Source == TriggerSource.Line0)
                {
                    mobjIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Line0");
                }
                else if (Source == TriggerSource.Line2)
                {
                    mobjIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Line2");
                }
                else
                {
                    mobjIGXFeatureControl.GetEnumFeature("TriggerSource").SetValue("Software");
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        public bool SoftTrigger()
        {
            try
            {
                mobjIGXFeatureControl.GetCommandFeature("TriggerSoftware").Execute();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return false;
            }
        }

        private void OnDahengCameraFrameReceived(object objUserParam, IFrameData objIFrameData)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                ulong Width = objIFrameData.GetWidth();
                ulong Height = objIFrameData.GetHeight();
                if (mMonoFlag)
                {
                    mImageCount++;
                    HOperatorSet.GenImage1(out HObject obj, "byte", mWidth, mHeight, (HTuple)objIFrameData.GetBuffer());
                    Enqueue(obj);
                }
                else
                {
                    byte[] ImageBuff = new byte[Width * Height * 3];
                    IntPtr BuffPtr;
                    if (mPixelDeep == "Bpp8")
                        BuffPtr = objIFrameData.ConvertToRGB24(GX_VALID_BIT_LIST.GX_BIT_0_7, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, false);
                    else if (mPixelDeep == "Bpp12")
                        BuffPtr = objIFrameData.ConvertToRGB24(GX_VALID_BIT_LIST.GX_BIT_4_11, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, false);
                    else
                        BuffPtr = objIFrameData.ConvertToRGB24(GX_VALID_BIT_LIST.GX_BIT_2_9, GX_BAYER_CONVERT_TYPE_LIST.GX_RAW2RGB_NEIGHBOUR, false);

                    HOperatorSet.GenImageInterleaved(out HObject obj, (HTuple)BuffPtr, "bgr", mWidth, mHeight, -1, "byte", mWidth, mHeight, 0, 0, -1, 0);
                    mImageCount++;
                    Enqueue(obj);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("相机 [" + this.SerialNum + "] " + ex);
            }
            sw.Stop();
            if (sw.ElapsedMilliseconds > 50)
                LogHelper.WriteErrorLog("相机 [" + this.SerialNum + "] 取流超时，" + sw.ElapsedMilliseconds.ToString() + "ms");
        }


        #region QueueOperation
        private void Enqueue(HObject obj)
        {
            lock (mQueueLock)
            {
                mImageBuff.Enqueue(obj);
            }
        }

        public HObject Dequeue()
        {
            lock (mQueueLock)
            {
                return mImageBuff.Dequeue();
            }
        }
        public void ClearQueue()
        {
            mImageBuff.Clear();
        }
        #endregion
    }
}
