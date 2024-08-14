using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace WCommonTools
{
    public class HaikCamera
    {       //无参构造函数
        public HaikCamera()
        {
            device = new MyCamera();
            deviceInfo = new MyCamera.MV_CC_DEVICE_INFO();
            frameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            mImageBuff = new Queue<HObject>();
            mIsGrabbing = false;
            mIsOpen = false;
            mIsRun = false;
            mIsLinked = false;
            mGrabImageFlag = false;
            mEnReceiveData = true;
            mCameraIP = null;
            mMonoFlag = true;
            mWidth = 0;
            mHeight = 0;
            mPixelDeep = "Bpp10";
            mQueueLock = new object();
            pBufForConvert = IntPtr.Zero;
        }

        #region Fields 字段

        //Camera run Flag 相机运行状态
        private bool mIsOpen;
        private bool mIsRun;
        private bool mIsLinked;
        private int mImageCount;
        private bool mIsGrabbing;

        private bool mGrabImageFlag;
        //Monitor Lost count
        private int mLostCnt;
        private int fmLostFrameCnt;
        //Camera info
        private string mCameraIP;
        private string mSN;
        private string mCameraName;
        private bool mEnReceiveData;
        private bool mMonoFlag;
        private int mWidth;
        private int mHeight;
        private TriggerMode mMode;
        private TriggerSource mTrigSource;
        private double mExposureTime;
        private double mGain;
        private string mPixelDeep;
        private byte[] m_pBufForSaveImage; //ch:用于从驱动获取图像的缓存 
        private Queue<HObject> mImageBuff;
        private double mImageScale;
        private MyCamera device;//创建访问对象实例
        private MyCamera.MV_CC_DEVICE_INFO deviceInfo;
        private MyCamera.cbOutputExdelegate ImageCallback;//相机回调函数
        private MyCamera.cbExceptiondelegate ExceptionCallback;//相机异常回调函数
        private MyCamera.MV_PIXEL_CONVERT_PARAM mConverPixelParam;
        private MyCamera.MV_FRAME_OUT_INFO_EX frameInfo;

        public delegate void CameraExceptiondelegate(string exceptionCode);
        public event CameraExceptiondelegate mCameraExceptiondelegateEvent;

        private object mQueueLock;
        private IntPtr pBufForConvert;
        #endregion

        #region Properties 属性
        public int ImageWidth
        {
            get { return mWidth; }
        }
        public int ImageHeight
        {
            get { return mHeight; }
        }
        public bool IsOpen
        {
            get { return mIsOpen; }
        }

        public bool IsRun
        {
            get { return mIsRun; }
        }

        public bool IsLinked
        {
            get { return mIsLinked; }
        }

        public bool IsLineCamera
        {
            get { return false; }
        }

        public string SerialNum
        {
            get { return mSN; }
            set { mSN = value; }
        }

        public string CameraName
        {
            get { return mCameraName; }
            set { mCameraName = value; }
        }

        public bool EnReceiveData
        {
            get { return mEnReceiveData; }
            set { mEnReceiveData = value; }
        }

        public bool MonoFlag
        {
            get { return mMonoFlag; }
        }

        public string IP
        {
            get { return mCameraIP; }
        }

        public TriggerMode Mode
        {
            get { return mMode; }
        }

        public TriggerSource TrigSource
        {
            get { return mTrigSource; }
        }

        public double ExposureTime
        {
            get { return mExposureTime; }
        }

        public double Gain
        {
            get { return mGain; }
        }

        public double ImageScale
        {
            get => mImageScale;
            set => mImageScale = value;
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

        /// <summary>
        /// 相机是否采集中
        /// </summary>
        public bool IsGrabbing { get => mIsGrabbing; set => mIsGrabbing = value; }
        #endregion

        #region Implements
        public static bool QueryCamera(out List<CameraInfo> CameraInfolist)
        {
            int nRet;
            CameraInfolist = new List<CameraInfo>();
            MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();//设备对象
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                CameraInfo info = new CameraInfo();
                MyCamera.MV_CC_DEVICE_INFO mv_cc_device_info = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (mv_cc_device_info.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO mv_gige_device_info = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(mv_cc_device_info.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    info.CamType = "Gige";
                    if (mv_gige_device_info.chUserDefinedName != "")
                    {
                        info.CamName = mv_gige_device_info.chUserDefinedName;
                        info.CamSerialNumber = mv_gige_device_info.chSerialNumber;
                    }
                    else
                    {
                        info.CamName = mv_gige_device_info.chManufacturerName + " " + mv_gige_device_info.chModelName;
                        info.CamSerialNumber = mv_gige_device_info.chSerialNumber;
                    }
                    CameraInfolist.Add(info);
                }
                else if (mv_cc_device_info.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(mv_cc_device_info.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    info.CamType = "USB3";
                    if (usbInfo.chUserDefinedName != "")
                    {
                        info.CamName = usbInfo.chUserDefinedName;
                        info.CamSerialNumber = usbInfo.chSerialNumber;
                    }
                    else
                    {
                        info.CamName = usbInfo.chManufacturerName + " " + usbInfo.chModelName;
                        info.CamSerialNumber = usbInfo.chSerialNumber;
                    }
                    CameraInfolist.Add(info);
                }
            }
            return true;
        }

        public bool OpenCamera(string SerialNumber)
        {
            mSN = SerialNumber;
            int count = 0;
            int nRet;
            try
            {
                MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();//设备对象
                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO mv_cc_device_info = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (mv_cc_device_info.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        deviceInfo = mv_cc_device_info;
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(mv_cc_device_info.SpecialInfo.stUsb3VInfo, 0), typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        if (usbInfo.chSerialNumber == SerialNumber)
                        {
                            MyCamera.MV_CC_DEVICE_INFO stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                            nRet = device.MV_CC_CreateDevice_NET(ref stDevInfo);
                            nRet = device.MV_CC_OpenDevice_NET();
                            if (MyCamera.MV_OK != nRet)
                            {
                                device.MV_CC_DestroyDevice_NET();
                                return false;
                            }
                            MyCamera.MVCC_INTVALUE pstvalue1 = new MyCamera.MVCC_INTVALUE();
                            nRet = device.MV_CC_GetWidth_NET(ref pstvalue1);//图像宽
                            mWidth = (int)pstvalue1.nCurValue;
                            MyCamera.MVCC_INTVALUE pstvalue2 = new MyCamera.MVCC_INTVALUE();
                            nRet = device.MV_CC_GetHeight_NET(ref pstvalue2);//图像高
                            mHeight = (int)pstvalue2.nCurValue;
                            mImageScale = Convert.ToDouble((double)mHeight / (double)mWidth);//图像比例
                            this.ImageCallback = new MyCamera.cbOutputExdelegate(GrabImageDelegate);//采图回调注册
                            this.ExceptionCallback = new MyCamera.cbExceptiondelegate(ExceptionDelegate);//异常回调注册
                            MyCamera.MVCC_ENUMVALUE pstvalue3 = new MyCamera.MVCC_ENUMVALUE();
                            device.MV_CC_GetPixelFormat_NET(ref pstvalue3);
                            if ((MyCamera.MvGvspPixelType)pstvalue3.nCurValue == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                            {
                                mMonoFlag = true;
                            }
                            else
                            {
                                mMonoFlag = false;
                                if (pBufForConvert == IntPtr.Zero)
                                {
                                    pBufForConvert = Marshal.AllocHGlobal(mWidth * mHeight * 3 + 2048);
                                }
                            }
                            mIsOpen = true;
                            nRet = this.device.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);
                            if (nRet != 0)
                            {
                                LogHelper.WriteExceptionLog("相机 [" + SerialNumber + "] 注册失败");
                                return false;
                            }
                            nRet = this.device.MV_CC_RegisterExceptionCallBack_NET(ExceptionCallback, IntPtr.Zero);
                            if (nRet != 0)
                            {
                                LogHelper.WriteExceptionLog("相机 [" + SerialNumber + "] 注册失败");
                                return false;
                            }
                            count = 1;
                            break;
                        }
                    }
                    else if (mv_cc_device_info.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        MyCamera.MV_GIGE_DEVICE_INFO mv_gige_device_info = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(mv_cc_device_info.SpecialInfo.stGigEInfo, 0), typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        if (mv_gige_device_info.chSerialNumber == SerialNumber)
                        {
                            deviceInfo = mv_cc_device_info;
                            MyCamera.MV_CC_DEVICE_INFO stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                            nRet = device.MV_CC_CreateDevice_NET(ref stDevInfo);
                            nRet = device.MV_CC_OpenDevice_NET();
                            if (MyCamera.MV_OK != nRet)
                            {
                                device.MV_CC_DestroyDevice_NET();
                                return false;
                            }
                            MyCamera.MVCC_INTVALUE pstvalue1 = new MyCamera.MVCC_INTVALUE();
                            nRet = device.MV_CC_GetWidth_NET(ref pstvalue1);//图像宽
                            mWidth = (int)pstvalue1.nCurValue;
                            MyCamera.MVCC_INTVALUE pstvalue2 = new MyCamera.MVCC_INTVALUE();
                            nRet = device.MV_CC_GetHeight_NET(ref pstvalue2);//图像高
                            mHeight = (int)pstvalue2.nCurValue;
                            mImageScale = Convert.ToDouble((double)mHeight / (double)mWidth);//图像比例
                            this.ImageCallback = new MyCamera.cbOutputExdelegate(GrabImageDelegate);//采图回调注册
                            this.ExceptionCallback = new MyCamera.cbExceptiondelegate(ExceptionDelegate);//异常回调注册
                            MyCamera.MVCC_ENUMVALUE pstvalue3 = new MyCamera.MVCC_ENUMVALUE();
                            device.MV_CC_GetPixelFormat_NET(ref pstvalue3);
                            if ((MyCamera.MvGvspPixelType)pstvalue3.nCurValue == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                            {
                                mMonoFlag = true;
                            }
                            else
                            {
                                mMonoFlag = false;
                                if (pBufForConvert == IntPtr.Zero)
                                {
                                    pBufForConvert = Marshal.AllocHGlobal(mWidth * mHeight * 3 + 2048);
                                }
                            }
                            mIsOpen = true;
                            nRet = this.device.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);
                            if (nRet != 0)
                            {
                                LogHelper.WriteExceptionLog("相机 [" + SerialNumber + "] 注册失败");
                                return false;
                            }
                            nRet = this.device.MV_CC_RegisterExceptionCallBack_NET(ExceptionCallback, IntPtr.Zero);
                            if (nRet != 0)
                            {
                                LogHelper.WriteExceptionLog("相机 [" + SerialNumber + "] 注册失败");
                                return false;
                            }
                            if (pBufForConvert == IntPtr.Zero)
                            {
                                pBufForConvert = Marshal.AllocHGlobal(mWidth * mHeight * 3 + 2048);
                            }
                            count = 1;
                            break;
                        }
                    }                
                }             
            }

            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
                return false;
            }
            if (count == 0)
                return false;
            else
                return true;
        }

        public bool CloseCamera()
        {
            device.MV_CC_CloseDevice_NET();
            device.MV_CC_DestroyDevice_NET();
            return true;
        }

        public bool StartGrab()
        {
            frameInfo.nFrameLen = 0;//取流之前先清除帧长度
            frameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            device.MV_CC_StartGrabbing_NET();
            mIsGrabbing = true;
            return true;
        }

        public bool StopGrab()
        {
            device.MV_CC_StopGrabbing_NET();
            mIsGrabbing = false;
            return true;
        }

        public bool SetCameraMode(TriggerMode Mode)
        {
            if (!mIsOpen)
                return false;
            try
            {
                mMode = Mode;
                if (Mode == TriggerMode.Mode_Continue)
                {
                    device.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                }
                else
                {
                    device.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool SetTriggerSource(TriggerSource Source)
        {
            if (!mIsOpen)
                return false;
            try
            {
                mTrigSource = Source;
                if (Source == TriggerSource.Line0)
                {
                    device.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                }
                else if (Source == TriggerSource.Line1)
                {
                    device.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE1);
                }
                else if (Source == TriggerSource.Line2)
                {
                    device.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE2);
                }
                else if (Source == TriggerSource.Line3)
                {
                    device.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE3);
                }
                else
                {
                    device.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetCameraGain(float Gain)
        {
            int nRet;
            device.MV_CC_SetEnumValue_NET("GainAuto", 0);//关闭自动增益
            nRet = device.MV_CC_SetFloatValue_NET("Gain", Convert.ToSingle(Gain));
            if (nRet != MyCamera.MV_OK)
            {
                return false;
            }

            return true;

        }

        public bool SetCameraExposure(float Exposure)
        {
            if (!mIsOpen)
                return false;
            try
            {
                int nRet;
                mExposureTime = Exposure;
                device.MV_CC_SetEnumValue_NET("ExposureAuto", 0);//关闭自动曝光
                nRet = device.MV_CC_SetFloatValue_NET("ExposureTime", Convert.ToSingle(Exposure));
                if (nRet != MyCamera.MV_OK)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SetTriggerDelay(float delay)
        {
            try
            {
                device.MV_CC_SetTriggerDelay_NET((float)delay);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SoftTrigger()
        {
            if (!mIsOpen)
                return false;
            try
            {
                int nRet = device.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool GetCameraExposure(out FloatValue mFloatValue)
        {
            mFloatValue = new FloatValue();
            try
            {
                MyCamera.MVCC_FLOATVALUE value = new MyCamera.MVCC_FLOATVALUE();
                device.MV_CC_GetExposureTime_NET(ref value);
                mFloatValue.FloarMin = value.fMin;
                mFloatValue.FloatMax = value.fMax;
                mFloatValue.FloatCurr = value.fCurValue;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetCameraGain(out FloatValue mFloatValue)
        {
            mFloatValue = new FloatValue();
            try
            {
                MyCamera.MVCC_FLOATVALUE value = new MyCamera.MVCC_FLOATVALUE();
                device.MV_CC_GetGain_NET(ref value);
                mFloatValue.FloarMin = value.fMin;
                mFloatValue.FloatMax = value.fMax;
                mFloatValue.FloatCurr = value.fCurValue;
                mGain= value.fCurValue;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetTriggerDelay(out FloatValue mFloatValue)
        {
            mFloatValue = new FloatValue();
            try
            {
                MyCamera.MVCC_FLOATVALUE value = new MyCamera.MVCC_FLOATVALUE();
                device.MV_CC_GetTriggerDelay_NET(ref value);
                mFloatValue.FloarMin = value.fMin;
                mFloatValue.FloatMax = value.fMax;
                mFloatValue.FloatCurr = value.fCurValue;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                    return true;
                default:
                    return false;
            }
        }

        public string GetLostFrame()
        {
            MyCamera.MV_ALL_MATCH_INFO pstInfo = new MyCamera.MV_ALL_MATCH_INFO();
            if (deviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                MyCamera.MV_MATCH_INFO_NET_DETECT MV_NetInfo = new MyCamera.MV_MATCH_INFO_NET_DETECT();
                pstInfo.nInfoSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MyCamera.MV_MATCH_INFO_NET_DETECT));
                pstInfo.nType = MyCamera.MV_MATCH_TYPE_NET_DETECT;
                int size = Marshal.SizeOf(MV_NetInfo);
                pstInfo.pInfo = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(MV_NetInfo, pstInfo.pInfo, false);

                device.MV_CC_GetAllMatchInfo_NET(ref pstInfo);
                MV_NetInfo = (MyCamera.MV_MATCH_INFO_NET_DETECT)Marshal.PtrToStructure(pstInfo.pInfo, typeof(MyCamera.MV_MATCH_INFO_NET_DETECT));

                string sTemp = MV_NetInfo.nLostFrameCount.ToString();
                Marshal.FreeHGlobal(pstInfo.pInfo);
                return sTemp;
            }
            else if (deviceInfo.nTLayerType == MyCamera.MV_USB_DEVICE)
            {
                MyCamera.MV_MATCH_INFO_USB_DETECT MV_NetInfo = new MyCamera.MV_MATCH_INFO_USB_DETECT();
                pstInfo.nInfoSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MyCamera.MV_MATCH_INFO_USB_DETECT));
                pstInfo.nType = MyCamera.MV_MATCH_TYPE_USB_DETECT;
                int size = Marshal.SizeOf(MV_NetInfo);
                pstInfo.pInfo = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(MV_NetInfo, pstInfo.pInfo, false);

                device.MV_CC_GetAllMatchInfo_NET(ref pstInfo);
                MV_NetInfo = (MyCamera.MV_MATCH_INFO_USB_DETECT)Marshal.PtrToStructure(pstInfo.pInfo, typeof(MyCamera.MV_MATCH_INFO_USB_DETECT));

                string sTemp = MV_NetInfo.nErrorFrameCount.ToString();
                Marshal.FreeHGlobal(pstInfo.pInfo);
                return sTemp;
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region CallBack
        private void GrabImageDelegate(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                frameInfo = pFrameInfo;
                //黑白
                if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    mImageCount++;
                    HOperatorSet.GenImage1(out HObject obj, "byte", mWidth, mHeight, (HTuple)pData);
                    Enqueue(obj);
                }
                else
                {
                    if (IsColorData(pFrameInfo.enPixelType))
                    {
                        mImageCount++;
                        mConverPixelParam.nWidth = pFrameInfo.nWidth;
                        mConverPixelParam.nHeight = pFrameInfo.nHeight;
                        mConverPixelParam.nSrcDataLen = pFrameInfo.nFrameLen;
                        mConverPixelParam.nDstBufferSize = (uint)(mWidth * mHeight * 3 + 2048);
                        mConverPixelParam.enSrcPixelType = pFrameInfo.enPixelType;
                        mConverPixelParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed;
                        mConverPixelParam.pSrcData = pData;
                        mConverPixelParam.pDstBuffer = pBufForConvert;
                        device.MV_CC_ConvertPixelType_NET(ref mConverPixelParam);
                        HOperatorSet.GenImageInterleaved(out HObject obj, (HTuple)pBufForConvert, "bgr", mWidth, mHeight, -1, "byte", mWidth, mHeight, 0, 0, -1, 0);
                        //HOperatorSet.GenImageInterleaved(out HObject obj, (HTuple)pData, "rgb", ImageWidth, ImageHeight, -1, "byte", ImageWidth, ImageHeight, 0, 0, -1, 0);
                        Enqueue(obj);
                    }
                    else
                    {
                        if (mCameraExceptiondelegateEvent != null)
                            mCameraExceptiondelegateEvent("相机 [" + this.SerialNum + "] 图像格式设定错误");
                        else
                            LogHelper.WriteErrorLog("相机 [" + this.SerialNum + "] 图像格式设定错误");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog("相机 [" + this.SerialNum + "] " + ex);
            }
            sw.Stop();
            if (sw.ElapsedMilliseconds > 30)
                LogHelper.WriteErrorLog("相机 [" + this.SerialNum + "] 取流超时，" + sw.ElapsedMilliseconds.ToString() + "ms");
        }

        private void ExceptionDelegate(uint nMsgType, IntPtr pUser)
        {
            if (nMsgType == MyCamera.MV_EXCEPTION_DEV_DISCONNECT)
            {
                if (mCameraExceptiondelegateEvent != null)
                    mCameraExceptiondelegateEvent("[" + this.SerialNum + "] 相机掉线");
            }
        }

        public void SetImageNode(uint NodeNum)
        {
            device.MV_CC_SetImageNodeNum_NET(NodeNum);
        }
        #endregion

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

    public enum TriggerMode
    {
        Mode_Continue,
        Mode_Trigger,
        Mode_SoftTrigger,
    }

    public enum TriggerSource
    {
        Line0,
        Line1,
        Line2,
        Line3,
        Encoder,
        Software
    }

    public struct FloatValue
    {
        public float FloatMax;
        public float FloarMin;
        public float FloatCurr;
    }

    [Serializable]
    public struct CameraInfo
    {
        public string CamType;
        public string CamName;
        public string CamSerialNumber;
    }
}
