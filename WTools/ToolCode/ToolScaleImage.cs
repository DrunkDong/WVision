using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTools
{
    [Serializable]
    public class ToolScaleImageParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mRegionStep;//区域源
        public int mRegionMark;
        public HRegion mCheckRegion;
        public HRegion mReduceRegion;

        public int mScaleMin;
        public int mScaleMax;

        public bool mIsOpenCheck;
        public int mSelectMode;
        public int mConnectMode;
        public int mThresholdMin;
        public int mThresholdMax;
        public double mBlobParam;
        public string mBlobMode;
        public int mSelectMin;
        public int mSelectMax;
        public int mAreaMin;
        public int mAreaMax;
        public int mCheckMode;

        private StepInfo mStepInfo;
        private string mShowName;
        private string mToolName;
        private ToolType mToolType;
        private JumpInfo mStepJumpInfo;
        private string mResultString;
        private bool mForceOK;
        private int mNgReturnValue;

        public override bool ForceOK
        {
            get => mForceOK;
            set => mForceOK = value;
        }
        public override int NgReturnValue
        {
            get => mNgReturnValue;
            set => mNgReturnValue = value;
        }
        public override StepInfo StepInfo
        {
            get => mStepInfo;
            set => mStepInfo = value;
        }
        public override string ShowName
        {
            get => mShowName;
            set => mShowName = value;
        }
        public override string ToolName
        {
            get => mToolName;
            set => mToolName = value;
        }
        public override ToolType ToolType
        {
            get => mToolType;
            set => mToolType = value;
        }
        public override JumpInfo StepJumpInfo
        {
            get => mStepJumpInfo;
            set => mStepJumpInfo = value;
        }

        public override string ResultString
        {
            get => mResultString;
            set => mResultString = value;
        }

        public int ImageSourceStep
        {
            get => mImageSourceStep;
            set => mImageSourceStep = value;
        }

        public delegate int ParamChangedDe(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj);
        public delegate int DrawRoiDel(HObject obj1, int mType, string mRoiType, int mMarkSize, out HObject obj2);
        public delegate int DrawRoi2Del(HObject obj1, string mRoiType, out HObject obj2);
        public delegate int SureRoiDel();
        public delegate int DeleRoiDel();
        public delegate void ScaleImageDel(HObject obj, List<StepInfo> StepInfoList, out HObject objShow);
        public delegate int ThresImageDel(HObject obj, List<StepInfo> StepInfoList, out HObject objShow);



        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public DrawRoiDel mDrawRoiDel;
        [NonSerialized]
        public SureRoiDel mSureRoiDel;
        [NonSerialized]
        public DeleRoiDel mDeleRoiDel;
        [NonSerialized]
        public DrawRoi2Del mDrawRoi2Del;
        [NonSerialized]
        public ScaleImageDel mScaleImageDel;
        [NonSerialized]
        public ThresImageDel mThresImageDel;

        public ToolScaleImageParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.ScaleImage;
            ImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;
            mRegionStep = -1;
            mRegionMark = -1;
            mCheckRegion = new HRegion();
            mCheckRegion.GenEmptyRegion();
            mReduceRegion = new HRegion();
            mReduceRegion.GenEmptyRegion();

            mScaleMin = 100;
            mScaleMax = 200;

            mIsOpenCheck = false;
            mThresholdMin = 0;
            mThresholdMax = 120;
            mBlobParam = 0;
            mBlobMode = "腐蚀";
            mSelectMin = 20;
            mSelectMax = 99999999;
            mSelectMode = 2;
            mConnectMode = 0;

            mAreaMin = 500;
            mAreaMax = 99999999;
            mCheckMode = 1;

            mShowName = "灰度缩放";
            mToolName = "灰度缩放";
            mStepInfo.mShowName= "灰度缩放";
            mToolType = ToolType.ScaleImage;
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolScaleImage : ToolBase
    {
        ToolScaleImageParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;



        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolScaleImageParam;
        }

        public ToolScaleImage(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolScaleImageParam;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            HObject ob;
            return ThesholdImage(objj1, StepInfoList, out ob);
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj)
        {
            JumpInfo StepJumpInfo;
            return DebugRun(obj1, obj2, StepInfoList, false, out StepJumpInfo);
        }

        public override ResStatus SetDebugWind(HTuple DebugWind, HWindow DrawWind)
        {
            mDebugWind = DebugWind;
            mDrawWind = DrawWind;
            return ResStatus.OK;
        }

        public override ResStatus SetRunWind(HTuple DefectWind)
        {
            mDefectWind = DefectWind;
            return ResStatus.OK;
        }

        public override int ToolRun(HObject obj, Bitmap objjj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            mToolParam.ResultString = "";
            try
            {
                HTuple mChannel;
                HObject objFinal;
                if (mToolParam.ImageSourceStep < 0)
                {
                    objFinal = obj;
                    //通道数检测
                    HOperatorSet.CountChannels(obj, out mChannel);
                    if (mChannel.I != 1)
                    {
                        mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                        return mToolParam.NgReturnValue;
                    }
                }
                else
                {
                    objFinal = StepInfoList[mToolParam.ImageSourceStep - 1].mToolRunResul.mImageOutPut;
                    //通道数检测
                    HOperatorSet.CountChannels(objFinal, out mChannel);
                    if (mChannel.I != 1)
                    {
                        mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                        return mToolParam.NgReturnValue;
                    }
                }

                //图片灰度进行缩放
                HObject obj1;
                scale_image_range(objFinal, out obj1, mToolParam.mScaleMin, mToolParam.mScaleMax);
                mToolParam.StepInfo.mToolRunResul.mImageOutPut = obj1;

                if (mToolParam.mIsOpenCheck)
                {
                    //是否运行判定
                    HObject obj2, obj3, obj4, obj5, obj6, obj7, obj8, obj9;
                    //确定区域是否为外部输入
                    if (mToolParam.mRegionStep > -1)
                    {
                        if (StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut == null || !StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut.IsInitialized())
                        {
                            mToolParam.ResultString = "区域输入步骤为空，请先生成区域";
                            return mToolParam.NgReturnValue;
                        }
                        else
                            obj2 = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                    }
                    //为内部区域
                    else
                    {
                        if (mToolParam.mCheckRegion.Area <= 0)
                        {
                            mToolParam.ResultString = "本地区域为空，请先绘制区域";
                            return mToolParam.NgReturnValue;
                        }
                        else
                            obj2 = mToolParam.mCheckRegion.CopyObj(1, 1);
                    }

                    //确定是否有定位输入步骤
                    if (mToolParam.mShapeModelStep > -1)
                    {
                        //仿射区域
                        HTuple HomMat2D;
                        HOperatorSet.VectorAngleToRigid(
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                        0,
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                        out HomMat2D);
                        HOperatorSet.HomMat2dScale(HomMat2D,
                         StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[6],
                         StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[6],
                         StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                         StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3], out HomMat2D);
                        HOperatorSet.AffineTransRegion(obj2, out obj3, HomMat2D, "nearest_neighbor");
                    }
                    else
                        //直接赋值
                        obj3 = obj2;

                    HOperatorSet.ReduceDomain(obj1, obj3, out obj4);
                    HOperatorSet.Threshold(obj4, out obj5, mToolParam.mThresholdMin, mToolParam.mThresholdMax);
                    if (mToolParam.mConnectMode == 0)
                    {
                        if (mToolParam.mBlobParam > 0)
                        {
                            if (mToolParam.mBlobMode == "腐蚀")
                                HOperatorSet.ErosionCircle(obj5, out obj6, mToolParam.mBlobParam);
                            else
                                HOperatorSet.DilationCircle(obj5, out obj6, mToolParam.mBlobParam);
                        }
                        else
                            obj6 = obj5;
                        HTuple area, row, column;
                        HOperatorSet.SelectShape(obj6, out obj7, "area", "and", mToolParam.mSelectMin, mToolParam.mSelectMax);
                        HOperatorSet.SelectShape(obj7, out obj8, "area", "and", mToolParam.mAreaMin, mToolParam.mAreaMax);
                        HOperatorSet.AreaCenter(obj8, out area, out row, out column);
                        if (area > 0)
                        {
                            if (mToolParam.mCheckMode == 1)
                            {
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            return 0;
                        }
                        else
                        {
                            if (mToolParam.mCheckMode == 0)
                            {
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            return 0;
                        }
                    }
                    else
                    {
                        if (mToolParam.mBlobParam > 0)
                        {
                            if (mToolParam.mBlobMode == "腐蚀")
                                HOperatorSet.ErosionCircle(obj5, out obj6, mToolParam.mBlobParam);
                            else
                                HOperatorSet.DilationCircle(obj5, out obj6, mToolParam.mBlobParam);
                        }
                        else
                            obj6 = obj5;
                        HOperatorSet.Connection(obj6, out obj7);
                        HOperatorSet.SelectShape(obj7, out obj8, "area", "and", mToolParam.mSelectMin, mToolParam.mSelectMax);
                        HTuple area, row, column;
                        HOperatorSet.AreaCenter(obj8, out area, out row, out column);
                        obj8.Dispose();
                        if (mToolParam.mSelectMode == 0)
                            HOperatorSet.SelectShape(obj7, out obj8, "area", "and", area.TupleMax(), mToolParam.mSelectMax);
                        else if (mToolParam.mSelectMode == 1)
                            HOperatorSet.SelectShape(obj7, out obj8, "area", "and", 0, area.TupleMin());
                        else
                            HOperatorSet.SelectShape(obj7, out obj8, "area", "and", mToolParam.mSelectMin, mToolParam.mSelectMax);
                        HOperatorSet.SelectShape(obj8, out obj9, "area", "and", mToolParam.mAreaMin, mToolParam.mAreaMax);
                        HTuple area1, row1, column1;
                        HOperatorSet.AreaCenter(obj9, out area1, out row1, out column1);
                        //mDrawWind.ClearWindow();
                        if (area1 > 0)
                        {
                            if (mToolParam.mCheckMode == 1)
                            {
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                obj9.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            obj9.Dispose();
                            return 0;
                        }
                        else
                        {
                            if (mToolParam.mCheckMode == 0)
                            {
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                obj9.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            obj9.Dispose();
                            return 0;
                        }
                    }
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
                return mToolParam.NgReturnValue;
            }
        }

        public void ScaleImage(HObject obj, List<StepInfo> StepInfoList, out HObject objShow)
        {          
            HOperatorSet.GenEmptyObj(out objShow);
            HTuple mChannel;
            HObject objFinal;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            if (mToolParam.ImageSourceStep < 0)
            {
                objFinal = obj;
                //通道数检测
                HOperatorSet.CountChannels(obj, out mChannel);
                if (mChannel.I != 1)
                {
                    mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                    return;
                }
            }
            else
            {
                objFinal = StepInfoList[mToolParam.ImageSourceStep - 1].mToolRunResul.mImageOutPut;
                //通道数检测
                HOperatorSet.CountChannels(objFinal, out mChannel);
                if (mChannel.I != 1)
                {
                    mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                    return;
                }
            }
            HObject obj1;
            scale_image_range(objFinal, out obj1, mToolParam.mScaleMin, mToolParam.mScaleMax);
            HOperatorSet.CountSeconds(out s2);
            mToolParam.ResultString = "耗时：" + ((s2.D - s1.D) * 1000).ToString("f2") + "ms" + "\r\n";
            mToolParam.StepInfo.mToolRunResul.mImageOutPut = obj1;
            mDrawWind.ClearWindow();
            mDrawWind.DispObj(obj1);
        }

        public int ThesholdImage(HObject obj, List<StepInfo> StepInfoList, out HObject objShow)
        {
            mToolParam.ResultString = "";
            HOperatorSet.GenEmptyObj(out objShow);
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            try
            {
                HTuple mChannel;
                HObject objFinal;
                if (mToolParam.ImageSourceStep < 0) 
                {
                    objFinal = obj;
                    //通道数检测
                    HOperatorSet.CountChannels(obj, out mChannel);
                    if (mChannel.I != 1)
                    {
                        mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                        return mToolParam.NgReturnValue;
                    }
                }
                else
                {
                    objFinal = StepInfoList[mToolParam.ImageSourceStep - 1].mToolRunResul.mImageOutPut;
                    //通道数检测
                    HOperatorSet.CountChannels(objFinal, out mChannel);
                    if (mChannel.I != 1)
                    {
                        mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                        return mToolParam.NgReturnValue;
                    }
                }


                //图片灰度进行缩放
                HObject obj1;
                scale_image_range(objFinal, out obj1, mToolParam.mScaleMin, mToolParam.mScaleMax);
                mToolParam.StepInfo.mToolRunResul.mImageOutPut = obj1;

                if (mToolParam.mIsOpenCheck)
                {
                    mDrawWind.ClearWindow();
                    //是否运行判定
                    HObject obj2, obj3, obj4, obj5, obj6, obj7, obj8, obj9, obj10, obj11, obj12;
                    //确定区域是否为外部输入
                    if (mToolParam.mRegionStep > -1) 
                    {
                        if (StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut == null || !StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut.IsInitialized())
                        {
                            mToolParam.ResultString = "区域输入步骤为空，请先生成区域";
                            return mToolParam.NgReturnValue;
                        }
                        else
                            obj2 = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                    }
                    //为内部区域
                    else
                    {
                        if (mToolParam.mCheckRegion.Area <= 0)
                        {
                            mToolParam.ResultString = "本地区域为空，请先绘制区域";
                            return mToolParam.NgReturnValue;
                        }
                        else
                            obj2 = mToolParam.mCheckRegion.CopyObj(1, 1);
                    }

                    //确定是否有定位输入步骤
                    if (mToolParam.mShapeModelStep > -1)
                    {
                        //仿射区域
                        HTuple HomMat2D;
                        HOperatorSet.VectorAngleToRigid(
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                        0,
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                        out HomMat2D);
                        HOperatorSet.AffineTransRegion(obj2, out obj3, HomMat2D, "nearest_neighbor");
                    }
                    else
                        //直接赋值
                        obj3 = obj2;

                    HOperatorSet.ReduceDomain(obj1, obj3, out obj4);
                    HOperatorSet.Threshold(obj4, out obj5, mToolParam.mThresholdMin, mToolParam.mThresholdMax);
                    if (mToolParam.mConnectMode == 0) 
                    {
                        if (mToolParam.mBlobParam > 0)
                        {
                            if (mToolParam.mBlobMode == "腐蚀")
                                HOperatorSet.ErosionCircle(obj5, out obj6, mToolParam.mBlobParam);
                            else
                                HOperatorSet.DilationCircle(obj5, out obj6, mToolParam.mBlobParam);
                        }
                        else
                            obj6 = obj5;
                        HTuple area, row, column;
                        HOperatorSet.SelectShape(obj6, out obj7, "area", "and", mToolParam.mSelectMin, mToolParam.mSelectMax);
                        HOperatorSet.SelectShape(obj7, out obj8, "area", "and", mToolParam.mAreaMin, mToolParam.mAreaMax);
                        HOperatorSet.AreaCenter(obj8, out area, out row, out column);
                        if (area > 0)
                        {
                            mToolParam.ResultString = "面积为：" + area.D.ToString();
                            if (mToolParam.mCheckMode == 1)
                            {
                                mDrawWind.ClearWindow();       
                                mDrawWind.DispObj(obj1);
                                mDrawWind.SetDraw("fill");
                                mDrawWind.SetColor("red");
                                mDrawWind.DispObj(obj8);
                                mDrawWind.SetDraw("margin");
                                mDrawWind.SetLineWidth(2);
                                mDrawWind.SetColor("green");
                                mDrawWind.DispObj(obj3);
;
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            mDrawWind.ClearWindow();
                            mDrawWind.DispObj(obj1);
                            mDrawWind.SetDraw("margin");
                            mDrawWind.SetLineWidth(2);
                            mDrawWind.SetColor("green");
                            mDrawWind.DispObj(obj3);
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            return 0;
                        }
                        else
                        {
                            if (mToolParam.mCheckMode == 0)
                            {
                                mToolParam.ResultString = "未找到区域!";
                                mDrawWind.ClearWindow();
                                mDrawWind.DispObj(obj1);
                                mDrawWind.SetDraw("margin");
                                mDrawWind.SetLineWidth(2);
                                mDrawWind.SetColor("green");
                                mDrawWind.DispObj(obj3);
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            mDrawWind.ClearWindow();
                            mDrawWind.DispObj(obj1);
                            mDrawWind.SetDraw("margin");
                            mDrawWind.SetLineWidth(2);
                            mDrawWind.SetColor("green");
                            mDrawWind.DispObj(obj3);
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            return 0;
                        }
                        
                    }
                    else
                    {
                        if (mToolParam.mBlobParam > 0)
                        {
                            if (mToolParam.mBlobMode == "腐蚀")
                                HOperatorSet.ErosionCircle(obj5, out obj6, mToolParam.mBlobParam);
                            else
                                HOperatorSet.DilationCircle(obj5, out obj6, mToolParam.mBlobParam);
                        }
                        else
                            obj6 = obj5;
                        HOperatorSet.Connection(obj6, out obj7);
                        HOperatorSet.SelectShape(obj7, out obj8, "area", "and", mToolParam.mSelectMin, mToolParam.mSelectMax);
                        HTuple area, row, column;
                        HOperatorSet.AreaCenter(obj8, out area, out row, out column);
                        obj8.Dispose();
                        if (mToolParam.mSelectMode == 0)
                            HOperatorSet.SelectShape(obj7, out obj8, "area", "and", area.TupleMax(), mToolParam.mSelectMax);
                        else if (mToolParam.mSelectMode == 1)
                            HOperatorSet.SelectShape(obj7, out obj8, "area", "and", 0, area.TupleMin());
                        else
                            HOperatorSet.SelectShape(obj7, out obj8, "area", "and", mToolParam.mSelectMin, mToolParam.mSelectMax);
                        HOperatorSet.SelectShape(obj8, out obj9, "area", "and", mToolParam.mAreaMin, mToolParam.mAreaMax);
                        HTuple area1, row1, column1;
                        HOperatorSet.AreaCenter(obj9, out area1, out row1, out column1);
                        if (area1 > 0)
                        {
                            mToolParam.ResultString = "面积为：" + area1.D.ToString();
                            if (mToolParam.mCheckMode == 1)
                            {
                                mDrawWind.ClearWindow();
                                mDrawWind.DispObj(obj1);
                                mDrawWind.SetDraw("fill");
                                mDrawWind.SetColor("red");
                                mDrawWind.DispObj(obj9);
                                mDrawWind.SetDraw("margin");
                                mDrawWind.SetLineWidth(2);
                                mDrawWind.SetColor("green");
                                mDrawWind.DispObj(obj3);

                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                obj9.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            mDrawWind.ClearWindow();
                            mDrawWind.DispObj(obj1);
                            mDrawWind.SetDraw("margin");
                            mDrawWind.SetLineWidth(2);
                            mDrawWind.SetColor("green");
                            mDrawWind.DispObj(obj3);
                            mDrawWind.SetDraw("fill");
                            mDrawWind.SetColor("green");
                            mDrawWind.DispObj(obj9);
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            obj9.Dispose();
                            return 0;
                        }
                        else
                        {
                            if (mToolParam.mCheckMode == 0)
                            {
                                mToolParam.ResultString = "未找到区域!";
                                mDrawWind.ClearWindow();
                                mDrawWind.DispObj(obj1);
                                mDrawWind.SetDraw("margin");
                                mDrawWind.SetLineWidth(2);
                                mDrawWind.SetColor("green");
                                mDrawWind.DispObj(obj3);
                                obj2.Dispose();
                                obj3.Dispose();
                                obj4.Dispose();
                                obj5.Dispose();
                                obj6.Dispose();
                                obj7.Dispose();
                                obj8.Dispose();
                                obj9.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                            mDrawWind.ClearWindow();
                            mDrawWind.DispObj(obj1);
                            mDrawWind.SetDraw("margin");
                            mDrawWind.SetLineWidth(2);
                            mDrawWind.SetColor("green");
                            mDrawWind.DispObj(obj3);
                            obj2.Dispose();
                            obj3.Dispose();
                            obj4.Dispose();
                            obj5.Dispose();
                            obj6.Dispose();
                            obj7.Dispose();
                            obj8.Dispose();
                            obj9.Dispose();
                            return 0;
                        }
                    }


                }

                HOperatorSet.CountSeconds(out s2);
                mToolParam.ResultString = "耗时：" + ((s2.D - s1.D) * 1000).ToString("f2") + "ms" + "\r\n";
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(obj1);
                return 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return mToolParam.NgReturnValue;
            }
        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mParamChangedDe += ParamChanged;
                mToolParam.mDrawRoiDel += DrawRoi;
                mToolParam.mDeleRoiDel += DeleRoi;
                mToolParam.mDrawRoi2Del += DrawRoi2;
                mToolParam.mScaleImageDel += ScaleImage;
                mToolParam.mThresImageDel += ThesholdImage;
            }
            else
            {
                mToolParam.mParamChangedDe = null;
                mToolParam.mDrawRoiDel = null;
                mToolParam.mDeleRoiDel = null;
                mToolParam.mDrawRoi2Del = null;
                mToolParam.mScaleImageDel = null;
                mToolParam.mThresImageDel = null;
            }

            return ResStatus.OK;
        }

        public int DrawRoi(HObject obj1, int type, string mRoiType, int mMarkSize, out HObject showRegion)
        {
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                int row, column, buttom;
                buttom = 0;
                row = -1;
                column = -1;
                while (buttom != 4)
                {
                    Application.DoEvents();
                    try
                    {
                        mDrawWind.GetMposition(out row, out column, out buttom);
                    }
                    catch (System.Exception)
                    {
                        buttom = 0;
                    }
                    mDrawWind.SetColor("red");
                    mDrawWind.SetDraw("fill");
                    if (row > 0 && column > 0)
                    {
                        HObject obj;
                        if (mRoiType == "circle")
                        {
                            HOperatorSet.GenCircle(out obj, row, column, mMarkSize / 2);
                        }
                        else
                        {
                            HOperatorSet.GenRectangle1(out obj, row - mMarkSize / 2, column - mMarkSize / 2, row + mMarkSize / 2, column + mMarkSize / 2);
                        }

                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");
                        HOperatorSet.SetSystem("flush_graphic", "false");
                        if (buttom == 1)
                        {
                            if (type == 0)
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Union2(mToolParam.mCheckRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.mCheckRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.mCheckRegion = r1;
                            }
                            else
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Difference(mToolParam.mCheckRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.mCheckRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.mCheckRegion = r1;
                            }
                        }
                        mDrawWind.SetRgba(255, 0, 0, 120);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.mCheckRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.mCheckRegion);
                showRegion = mToolParam.mCheckRegion;
                Thread.Sleep(20);
                return 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int DrawRoi2(HObject obj1, string mRoiType, out HObject showRegion)
        {
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("fill");
            mDrawWind.SetRgba(255, 0, 0, 120);
            mDrawWind.DispObj(mToolParam.mCheckRegion);
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                if (mRoiType == "circle")
                {
                    double row, column, radius;
                    HObject obj;
                    mDrawWind.DrawCircle(out row, out column, out radius);
                    HOperatorSet.GenCircle(out obj, row, column, radius);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mCheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mCheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle1")
                {
                    double row, column, row2, column2;
                    HObject obj;
                    mDrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                    HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mCheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mCheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle2")
                {
                    double row, column, phi, length1, length2;
                    HObject obj;
                    mDrawWind.DrawRectangle2(out row, out column, out phi, out length1, out length2);
                    HOperatorSet.GenRectangle2(out obj, row, column, phi, length1, length2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mCheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mCheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "ellipse")
                {
                    double row, column, phi, radius1, radius2;
                    HObject obj;
                    mDrawWind.DrawEllipse(out row, out column, out phi, out radius1, out radius2);
                    HOperatorSet.GenEllipse(out obj, row, column, phi, radius1, radius2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mCheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mCheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mCheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mCheckRegion = new HRegion(temp);
                }

                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.mCheckRegion);
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public int DeleRoi()
        {
            mToolParam.mCheckRegion.Dispose();
            mToolParam.mCheckRegion.GenEmptyRegion();
            mDrawWind.ClearWindow();
            return 0;
        }

        private void scale_image_range(HObject ho_Image, out HObject ho_ImageScaled, HTuple hv_Min,HTuple hv_Max)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ImageSelected = null, ho_SelectedChannel = null;
            HObject ho_LowerRegion = null, ho_UpperRegion = null, ho_ImageSelectedScaled = null;

            // Local copy input parameter variables 
            HObject ho_Image_COPY_INP_TMP;
            ho_Image_COPY_INP_TMP = new HObject(ho_Image);



            // Local control variables 

            HTuple hv_LowerLimit = new HTuple(), hv_UpperLimit = new HTuple();
            HTuple hv_Mult = new HTuple(), hv_Add = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_ImageIndex = new HTuple(), hv_Channels = new HTuple();
            HTuple hv_ChannelIndex = new HTuple(), hv_MinGray = new HTuple();
            HTuple hv_MaxGray = new HTuple(), hv_Range = new HTuple();
            HTuple hv_Max_COPY_INP_TMP = new HTuple(hv_Max);
            HTuple hv_Min_COPY_INP_TMP = new HTuple(hv_Min);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_ImageSelected);
            HOperatorSet.GenEmptyObj(out ho_SelectedChannel);
            HOperatorSet.GenEmptyObj(out ho_LowerRegion);
            HOperatorSet.GenEmptyObj(out ho_UpperRegion);
            HOperatorSet.GenEmptyObj(out ho_ImageSelectedScaled);
            //Convenience procedure to scale the gray values of the
            //input image Image from the interval [Min,Max]
            //to the interval [0,255] (default).
            //Gray values < 0 or > 255 (after scaling) are clipped.
            //
            //If the image shall be scaled to an interval different from [0,255],
            //this can be achieved by passing tuples with 2 values [From, To]
            //as Min and Max.
            //Example:
            //scale_image_range(Image:ImageScaled:[100,50],[200,250])
            //maps the gray values of Image from the interval [100,200] to [50,250].
            //All other gray values will be clipped.
            //
            //input parameters:
            //Image: the input image
            //Min: the minimum gray value which will be mapped to 0
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //Max: The maximum gray value which will be mapped to 255
            //     If a tuple with two values is given, the first value will
            //     be mapped to the second value.
            //
            //Output parameter:
            //ImageScale: the resulting scaled image.
            //
            if ((int)(new HTuple((new HTuple(hv_Min_COPY_INP_TMP.TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_LowerLimit.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_LowerLimit = hv_Min_COPY_INP_TMP.TupleSelect(
                        1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Min = hv_Min_COPY_INP_TMP.TupleSelect(
                            0);
                        hv_Min_COPY_INP_TMP.Dispose();
                        hv_Min_COPY_INP_TMP = ExpTmpLocalVar_Min;
                    }
                }
            }
            else
            {
                hv_LowerLimit.Dispose();
                hv_LowerLimit = 0.0;
            }
            if ((int)(new HTuple((new HTuple(hv_Max_COPY_INP_TMP.TupleLength())).TupleEqual(
                2))) != 0)
            {
                hv_UpperLimit.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_UpperLimit = hv_Max_COPY_INP_TMP.TupleSelect(
                        1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Max = hv_Max_COPY_INP_TMP.TupleSelect(
                            0);
                        hv_Max_COPY_INP_TMP.Dispose();
                        hv_Max_COPY_INP_TMP = ExpTmpLocalVar_Max;
                    }
                }
            }
            else
            {
                hv_UpperLimit.Dispose();
                hv_UpperLimit = 255.0;
            }
            //
            //Calculate scaling parameters.
            hv_Mult.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Mult = (((hv_UpperLimit - hv_LowerLimit)).TupleReal()
                    ) / (hv_Max_COPY_INP_TMP - hv_Min_COPY_INP_TMP);
            }
            hv_Add.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Add = ((-hv_Mult) * hv_Min_COPY_INP_TMP) + hv_LowerLimit;
            }
            //
            //Scale image.
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ScaleImage(ho_Image_COPY_INP_TMP, out ExpTmpOutVar_0, hv_Mult, hv_Add);
                ho_Image_COPY_INP_TMP.Dispose();
                ho_Image_COPY_INP_TMP = ExpTmpOutVar_0;
            }
            //
            //Clip gray values if necessary.
            //This must be done for each image and channel separately.
            ho_ImageScaled.Dispose();
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            hv_NumImages.Dispose();
            HOperatorSet.CountObj(ho_Image_COPY_INP_TMP, out hv_NumImages);
            HTuple end_val49 = hv_NumImages;
            HTuple step_val49 = 1;
            for (hv_ImageIndex = 1; hv_ImageIndex.Continue(end_val49, step_val49); hv_ImageIndex = hv_ImageIndex.TupleAdd(step_val49))
            {
                ho_ImageSelected.Dispose();
                HOperatorSet.SelectObj(ho_Image_COPY_INP_TMP, out ho_ImageSelected, hv_ImageIndex);
                hv_Channels.Dispose();
                HOperatorSet.CountChannels(ho_ImageSelected, out hv_Channels);
                HTuple end_val52 = hv_Channels;
                HTuple step_val52 = 1;
                for (hv_ChannelIndex = 1; hv_ChannelIndex.Continue(end_val52, step_val52); hv_ChannelIndex = hv_ChannelIndex.TupleAdd(step_val52))
                {
                    ho_SelectedChannel.Dispose();
                    HOperatorSet.AccessChannel(ho_ImageSelected, out ho_SelectedChannel, hv_ChannelIndex);
                    hv_MinGray.Dispose(); hv_MaxGray.Dispose(); hv_Range.Dispose();
                    HOperatorSet.MinMaxGray(ho_SelectedChannel, ho_SelectedChannel, 0, out hv_MinGray,
                        out hv_MaxGray, out hv_Range);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_LowerRegion.Dispose();
                        HOperatorSet.Threshold(ho_SelectedChannel, out ho_LowerRegion, ((hv_MinGray.TupleConcat(
                            hv_LowerLimit))).TupleMin(), hv_LowerLimit);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_UpperRegion.Dispose();
                        HOperatorSet.Threshold(ho_SelectedChannel, out ho_UpperRegion, hv_UpperLimit,
                            ((hv_UpperLimit.TupleConcat(hv_MaxGray))).TupleMax());
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.PaintRegion(ho_LowerRegion, ho_SelectedChannel, out ExpTmpOutVar_0,
                            hv_LowerLimit, "fill");
                        ho_SelectedChannel.Dispose();
                        ho_SelectedChannel = ExpTmpOutVar_0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.PaintRegion(ho_UpperRegion, ho_SelectedChannel, out ExpTmpOutVar_0,
                            hv_UpperLimit, "fill");
                        ho_SelectedChannel.Dispose();
                        ho_SelectedChannel = ExpTmpOutVar_0;
                    }
                    if ((int)(new HTuple(hv_ChannelIndex.TupleEqual(1))) != 0)
                    {
                        ho_ImageSelectedScaled.Dispose();
                        HOperatorSet.CopyObj(ho_SelectedChannel, out ho_ImageSelectedScaled, 1,
                            1);
                    }
                    else
                    {
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.AppendChannel(ho_ImageSelectedScaled, ho_SelectedChannel,
                                out ExpTmpOutVar_0);
                            ho_ImageSelectedScaled.Dispose();
                            ho_ImageSelectedScaled = ExpTmpOutVar_0;
                        }
                    }
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConcatObj(ho_ImageScaled, ho_ImageSelectedScaled, out ExpTmpOutVar_0
                        );
                    ho_ImageScaled.Dispose();
                    ho_ImageScaled = ExpTmpOutVar_0;
                }
            }
            ho_Image_COPY_INP_TMP.Dispose();
            ho_ImageSelected.Dispose();
            ho_SelectedChannel.Dispose();
            ho_LowerRegion.Dispose();
            ho_UpperRegion.Dispose();
            ho_ImageSelectedScaled.Dispose();

            hv_Max_COPY_INP_TMP.Dispose();
            hv_Min_COPY_INP_TMP.Dispose();
            hv_LowerLimit.Dispose();
            hv_UpperLimit.Dispose();
            hv_Mult.Dispose();
            hv_Add.Dispose();
            hv_NumImages.Dispose();
            hv_ImageIndex.Dispose();
            hv_Channels.Dispose();
            hv_ChannelIndex.Dispose();
            hv_MinGray.Dispose();
            hv_MaxGray.Dispose();
            hv_Range.Dispose();

            return;
        }
    }
}
