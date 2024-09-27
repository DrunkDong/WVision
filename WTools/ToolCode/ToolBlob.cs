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
    public class ToolBlobParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mRegionStep;//区域源
        public int mRegionMark;

        public int mThresMin;
        public int mThresMax;

        public bool mIsDynThres;
        public double mDynMark;
        public double mDynOffset;
        public string mDynMode;

        public bool mIsBlob;
        public string mBlobMode;
        public double mBlobParam1;
        public double mBlobParam2;
        public int mBlobByMode;

        public bool mIsTop;
        public string mTopMode;
        public double mTopParam;
        public int mTopByMode;
        public int mTopPartMode;

        public int mSelectMode;
        public int mConnectMode;
        public int mCheckMode;
        public int mWidthMin;
        public int mWidthMax;
        public int mHeightMin;
        public int mHeightMax;
        public int mAreaMin;
        public int mAreaMax;

        public int mSelectAreaMin;
        public int mSelectAreaMax;

        public double mLength1Min;
        public double mLength2Min;
        public double mLength1Max;
        public double mLength2Max;

        public HRegion CheckRegion;

        private StepInfo mStepInfo;
        private string mShowName;
        private string mToolName;
        private ToolType mToolType;
        private JumpInfo mStepJumpInfo;
        private string mResultString;
        private bool mForceOK;
        private int mNgReturnValue;
        private ToolResultType mToolResultType;


        public override ToolResultType ToolResultType
        {
            get => mToolResultType;
            set => mToolResultType = value;
        }
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


        public delegate int ParamChangedDe(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj);
        public delegate int RegionThresholdDel(HObject obj1, List<StepInfo> StepInfoList, out HObject showRegion);
        public delegate int RegionThresholdDel2(HObject obj1, List<StepInfo> StepInfoList, out HObject showRegion);
        public delegate int SelectShapeDel(HObject obj1, List<StepInfo> StepInfoList, out HObject showRegion);
        public delegate int DrawRoiDel(HObject obj1, int type, string mRoiType, int mMarkSize, out HObject obj2);
        public delegate int DrawRoi2Del(HObject obj1, string mRoiType, out HObject obj2);
        public delegate int SureRoiDel();
        public delegate int DeleRoiDel();

        public delegate void AffineRegionDel(int MoveX, int MoveY, double Scale, double Rotate,out HObject finalRegion);
        public delegate void SureAffineDel();


        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public RegionThresholdDel mRegionThresholdDel;
        [NonSerialized]
        public RegionThresholdDel2 mRegionThresholdDel2;
        [NonSerialized]
        public SelectShapeDel mSelectShapeDel;
        [NonSerialized]
        public DrawRoi2Del mDrawRoi2Del;
        [NonSerialized]
        public DrawRoiDel mDrawRoiDel;
        [NonSerialized]
        public SureRoiDel mSureRoiDel;
        [NonSerialized]
        public DeleRoiDel mDeleRoiDel;
        [NonSerialized]
        public AffineRegionDel mAffineRegionDel;
        [NonSerialized]
        public SureAffineDel mSureAffineDel;

        public ToolBlobParam()
        {
            mStepInfo = new StepInfo();
            mShowName = "区域提取";
            mToolName = "区域提取";
            mStepInfo.mShowName = "区域提取";
            mStepInfo.mToolType = ToolType.RegionBlob;
            mToolType = ToolType.RegionBlob;
            mStepInfo.mToolResultType = ToolResultType.Region;
            mToolResultType = ToolResultType.Region;

            mStepJumpInfo = new JumpInfo();
            mResultString = "";

            mThresMin = 0;
            mThresMax = 128;

            mIsDynThres = false;
            mDynMark = 10;
            mDynOffset = 0.5;
            mDynMode = "dark";

            mIsBlob = false;
            mBlobMode = "腐蚀";
            mBlobParam1 = 3.5;
            mBlobParam2 = 3.5;
            mBlobByMode = 0;

            mIsTop = false;
            mTopMode = "顶帽";
            mTopParam = 0.1;
            mTopByMode = 0;
            mTopPartMode = 0;

            mSelectMode = 2;
            mConnectMode = 0;
            mCheckMode = 0;

            mWidthMin = 0;
            mWidthMax = 10000;
            mHeightMin = 0;
            mHeightMax = 10000;
            mAreaMin = 0;
            mAreaMax = 99999999;
            mSelectAreaMin = 0;
            mSelectAreaMax = 50;
            mLength1Min = 20;
            mLength2Min = 20;
            mLength1Max = 99999999;
            mLength2Max = 99999999;

            CheckRegion = new HRegion();
            CheckRegion.GenEmptyRegion(); 
            mNgReturnValue = 1;

            mImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;
            mRegionStep = -1;
            mRegionMark = -1;


        }
    }

    public class ToolBlob : ToolBase
    {
        ToolBlobParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;
        HObject mTempRegion;

        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolBlobParam;
        }

        public ToolBlob(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolBlobParam;
            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            HObject obj;
            int res = SelectAreaFinal(objj1, StepInfoList, out obj);
            obj.Dispose();
            return res;
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj)
        {
            JumpInfo StepJumpInfo;
            return DebugRun(obj1, StepInfoList, false, out StepJumpInfo);
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

        public override int ToolRun(HObject objj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            try
            {
                //图片源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = objj1;
                HTuple mChannel;
                HOperatorSet.CountChannels(imageFinal, out mChannel);
                if (mChannel != 1)
                {
                    mToolParam.ResultString = "图片非单通道图片，请重新输入";
                    return mToolParam.NgReturnValue;
                }
                //区域源
                HObject regionGet;
                if (mToolParam.mRegionStep > -1)
                    regionGet = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                else
                    regionGet = mToolParam.CheckRegion;
                HTuple area;
                HOperatorSet.RegionFeatures(regionGet, "area", out area);
                if (area <= 0)
                {
                    mToolParam.ResultString = "检测区域为空";
                    return mToolParam.NgReturnValue;
                }
                //区域转换
                HObject regionFinal;
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

                    HOperatorSet.AffineTransRegion(regionGet, out regionFinal, HomMat2D, "nearest_neighbor");
                }
                else
                    //直接赋值
                    regionFinal = regionGet;


                HObject imageReduce;
                HObject RegGet;
                //减少定义域
                HOperatorSet.ReduceDomain(imageFinal, regionFinal, out imageReduce);
                //获取区域
                if (!mToolParam.mIsDynThres)
                {
                    HOperatorSet.Threshold(imageReduce, out RegGet, mToolParam.mThresMin, mToolParam.mThresMax);
                }
                else
                {
                    HObject obj1;
                    HOperatorSet.MeanImage(imageReduce, out obj1, mToolParam.mDynMark, mToolParam.mDynMark);
                    HOperatorSet.DynThreshold(imageReduce, obj1, out RegGet, mToolParam.mDynOffset, mToolParam.mDynMode);
                    obj1.Dispose();
                }
                if (mToolParam.mIsBlob)
                {
                    HObject temp1;
                    if (mToolParam.mBlobMode == "腐蚀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ErosionCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ErosionRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "膨胀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.DilationCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.DilationRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "开运算")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.OpeningCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.OpeningRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ClosingCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ClosingRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    RegGet = temp1;
                }
                if (mToolParam.mIsTop)
                {
                    HObject temp1;
                    HObject temp2;
                    //结构体
                    HObject Elemt;
                    if (mToolParam.mTopByMode == 0)
                    {
                        HTuple a, r, c;
                        HOperatorSet.SmallestCircle(RegGet, out a, out r, out c);
                        HOperatorSet.GenCircle(out Elemt, a, r, c * mToolParam.mTopParam);
                    }
                    else
                    {
                        HTuple r, c, phi, length1, length2;
                        HOperatorSet.SmallestRectangle2(RegGet, out r, out c, out phi, out length1, out length2);
                        HOperatorSet.GenRectangle2(out Elemt, r, c, phi, length1 * mToolParam.mTopParam, length2 * mToolParam.mTopParam);
                    }
                    //运算
                    if (mToolParam.mTopMode == "顶帽")
                        HOperatorSet.TopHat(RegGet, Elemt, out temp1);
                    else
                        HOperatorSet.BottomHat(RegGet, Elemt, out temp1);
                    //交差集
                    if (mToolParam.mTopPartMode == 0)
                    {
                        RegGet = temp1;
                    }
                    else
                    {
                        HOperatorSet.Difference(RegGet, temp1, out temp2);
                        RegGet = temp2;
                        temp1.Dispose();
                    }
                    Elemt.Dispose();
                }


                HTuple features = new HTuple();
                features[0] = "area";
                features[1] = "height";
                features[2] = "width";
                features[3] = "rect2_len1";
                features[4] = "rect2_len2";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.mSelectAreaMax;
                minValue[1] = mToolParam.mHeightMin;
                minValue[2] = mToolParam.mWidthMin;
                minValue[3] = mToolParam.mLength1Min;
                minValue[4] = mToolParam.mLength2Min;
                HTuple maxValue = new HTuple();
                maxValue[0] = 9999999999;
                maxValue[1] = mToolParam.mHeightMax;
                maxValue[2] = mToolParam.mWidthMax;
                maxValue[3] = mToolParam.mLength1Max;
                maxValue[4] = mToolParam.mLength2Max;

                if (mToolParam.mConnectMode == 0)
                {
                    HObject temp1;
                    HOperatorSet.SelectShape(RegGet, out temp1, features, "and", minValue, maxValue);
                    RegGet = temp1;
                }
                else
                {
                    HObject temp1;
                    HObject temp2;
                    HOperatorSet.Connection(RegGet, out temp1);
                    HTuple a, r, c;
                    HOperatorSet.AreaCenter(temp1, out a, out r, out c);
                    if (mToolParam.mSelectMode == 0)
                    {
                        HOperatorSet.SelectShape(temp1, out temp2, "area", "and", a.TupleMax(), mToolParam.mAreaMax);
                        temp1 = temp2;
                    }

                    else if (mToolParam.mSelectMode == 1)
                    {
                        HOperatorSet.SelectShape(temp1, out temp2, "area", "and", 0, a.TupleMin());
                        temp1 = temp2;
                    }
                    HOperatorSet.SelectShape(temp1, out RegGet, features, "and", minValue, maxValue);
                    temp1.Dispose();
                }

                HObject union, select;
                HOperatorSet.Union1(RegGet, out union);
                HOperatorSet.SelectShape(union, out select, "area", "and", mToolParam.mAreaMin, mToolParam.mAreaMax);
                union.Dispose();
                //有区域为NG
                if (mToolParam.mCheckMode == 1)
                {
                    if (select.CountObj() > 0)
                    {
                        mToolParam.StepInfo.mToolRunResul.mRegionOutPut = RegGet;
                        imageReduce.Dispose();
                        select.Dispose();
                        return mToolParam.NgReturnValue;
                    }
                    else
                    {
                        imageReduce.Dispose();
                        select.Dispose();
                        return 0;
                    }
                }
                else
                {
                    if (select.CountObj() > 0)
                    {
                        mToolParam.StepInfo.mToolRunResul.mRegionOutPut = RegGet;
                        imageReduce.Dispose();
                        select.Dispose();
                        return 0;
                    }
                    else
                    {
                        imageReduce.Dispose();
                        select.Dispose();
                        return mToolParam.NgReturnValue;
                    }
                }
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mParamChangedDe += ParamChanged;
                mToolParam.mDrawRoi2Del += DrawRoi2;
                mToolParam.mDrawRoiDel += DrawRoi;
                mToolParam.mDeleRoiDel += DeleRoi;
                mToolParam.mRegionThresholdDel += RegionThreshold;
                mToolParam.mRegionThresholdDel2 += BlobRegion;
                mToolParam.mSelectShapeDel += SelectArea;
                mToolParam.mAffineRegionDel += AffineRegion;
                mToolParam.mSureAffineDel += SureChangeRegion;
            }
            else
            {
                mToolParam.mParamChangedDe = null;
                mToolParam.mDrawRoi2Del = null;
                mToolParam.mDrawRoiDel = null;
                mToolParam.mDeleRoiDel = null;
                mToolParam.mRegionThresholdDel = null;
                mToolParam.mSelectShapeDel = null;
                mToolParam.mRegionThresholdDel2 = null;
                mToolParam.mAffineRegionDel = null;
                mToolParam.mSureAffineDel = null;
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
                                HOperatorSet.Union2(mToolParam.CheckRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.CheckRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.CheckRegion = r1;
                            }
                            else
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Difference(mToolParam.CheckRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.CheckRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.CheckRegion = r1;
                            }
                        }
                        mDrawWind.SetRgba(255, 0, 0, 120);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.CheckRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.CheckRegion);
                showRegion = mToolParam.CheckRegion;
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
            mDrawWind.DispObj(mToolParam.CheckRegion);
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
                    HOperatorSet.Union2(mToolParam.CheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle1")
                {
                    double row, column, row2, column2;
                    HObject obj;
                    mDrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                    HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle2")
                {
                    double row, column, phi, length1, length2;
                    HObject obj;
                    mDrawWind.DrawRectangle2(out row, out column, out phi, out length1, out length2);
                    HOperatorSet.GenRectangle2(out obj, row, column, phi, length1, length2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "ellipse")
                {
                    double row, column, phi, radius1, radius2;
                    HObject obj;
                    mDrawWind.DrawEllipse(out row, out column, out phi, out radius1, out radius2);
                    HOperatorSet.GenEllipse(out obj, row, column, phi, radius1, radius2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.CheckRegion = new HRegion(temp);
                }

                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.CheckRegion);
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
            mToolParam.CheckRegion.Dispose();
            mToolParam.CheckRegion.GenEmptyRegion();
            mDrawWind.ClearWindow();
            return 0;
        }

        public int RegionThreshold(HObject objj1, List<StepInfo> StepInfoList, out HObject showRegion)
        {
            mToolParam.ResultString = "";
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                //图片源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = objj1;
                HTuple mChannel;
                HOperatorSet.CountChannels(imageFinal, out mChannel);
                if (mChannel != 1)
                {
                    mToolParam.ResultString = "图片非单通道图片，请重新输入";
                    return mToolParam.NgReturnValue;
                }
                //区域源
                HObject regionGet;
                if (mToolParam.mRegionStep > -1)
                    regionGet = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                else
                    regionGet = mToolParam.CheckRegion;
                HTuple area;
                HOperatorSet.RegionFeatures(regionGet, "area", out area);
                if (area <= 0)
                {
                    mToolParam.ResultString = "检测区域为空";
                    return mToolParam.NgReturnValue;
                }
                //区域转换
                HObject regionFinal;
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
                    HOperatorSet.AffineTransRegion(regionGet, out regionFinal, HomMat2D, "nearest_neighbor");
                }
                else
                    //直接赋值
                    regionFinal = regionGet;


                HObject imageReduce;
                HObject RegGet;
                //减少定义域
                HOperatorSet.ReduceDomain(imageFinal, regionFinal, out imageReduce);
                //获取区域
                if (!mToolParam.mIsDynThres)
                {
                    HOperatorSet.Threshold(imageReduce, out RegGet, mToolParam.mThresMin, mToolParam.mThresMax);
                }
                else
                {
                    HObject obj1;
                    HOperatorSet.MeanImage(imageReduce, out obj1, mToolParam.mDynMark, mToolParam.mDynMark);
                    HOperatorSet.DynThreshold(imageReduce, obj1, out RegGet, mToolParam.mDynOffset, mToolParam.mDynMode);
                    obj1.Dispose();
                }
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(objj1);
                mDrawWind.SetDraw("fill");
                mDrawWind.SetColor("red");
                mDrawWind.DispObj(RegGet);
                mDrawWind.SetDraw("margin");
                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColor("green");
                mDrawWind.DispObj(regionFinal);
                imageReduce.Dispose();
                RegGet.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public int BlobRegion(HObject objj1, List<StepInfo> StepInfoList, out HObject showRegion)
        {
            mToolParam.ResultString = "";
            mDrawWind.ClearWindow();
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                //图片源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = objj1;
                HTuple mChannel;
                HOperatorSet.CountChannels(imageFinal, out mChannel);
                if (mChannel != 1)
                {
                    mToolParam.ResultString = "图片非单通道图片，请重新输入";
                    return mToolParam.NgReturnValue;
                }
                //区域源
                HObject regionGet;
                if (mToolParam.mRegionStep > -1)
                    regionGet = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                else
                    regionGet = mToolParam.CheckRegion;
                HTuple area;
                HOperatorSet.RegionFeatures(regionGet, "area", out area);
                if (area <= 0)
                {
                    mToolParam.ResultString = "检测区域为空";
                    return mToolParam.NgReturnValue;
                }
                //区域转换
                HObject regionFinal;
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
                    HOperatorSet.AffineTransRegion(regionGet, out regionFinal, HomMat2D, "nearest_neighbor");
                }
                else
                    //直接赋值
                    regionFinal = regionGet;


                HObject imageReduce;
                HObject RegGet;
                //减少定义域
                HOperatorSet.ReduceDomain(imageFinal, regionFinal, out imageReduce);
                //获取区域
                if (!mToolParam.mIsDynThres)
                {
                    HOperatorSet.Threshold(imageReduce, out RegGet, mToolParam.mThresMin, mToolParam.mThresMax);
                }
                else
                {
                    HObject obj1;
                    HOperatorSet.MeanImage(imageReduce, out obj1, mToolParam.mDynMark, mToolParam.mDynMark);
                    HOperatorSet.DynThreshold(imageReduce, obj1, out RegGet, mToolParam.mDynOffset, mToolParam.mDynMode);
                    obj1.Dispose();
                }
                if (mToolParam.mIsBlob)
                {
                    HObject temp1;
                    if (mToolParam.mBlobMode == "腐蚀") 
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ErosionCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ErosionRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "膨胀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.DilationCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.DilationRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "开运算")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.OpeningCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.OpeningRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ClosingCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ClosingRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    RegGet = temp1;
                }
                if (mToolParam.mIsTop) 
                {
                    HObject temp1;
                    HObject temp2;
                    //结构体
                    HObject Elemt;
                    if (mToolParam.mTopByMode == 0)
                    {
                        HTuple a, r, c;
                        HOperatorSet.SmallestCircle(RegGet, out a, out r, out c);
                        HOperatorSet.GenCircle(out Elemt, a, r, c * mToolParam.mTopParam);
                     }
                    else
                    {
                        HTuple r, c, phi, length1, length2;
                        HOperatorSet.SmallestRectangle2(RegGet, out r, out c, out phi, out length1, out length2);
                        HOperatorSet.GenRectangle2(out Elemt, r, c, phi, length1 * mToolParam.mTopParam, length2 * mToolParam.mTopParam);
                    }
                    //运算
                    if (mToolParam.mTopMode == "顶帽") 
                        HOperatorSet.TopHat(RegGet, Elemt, out temp1);                    
                    else
                        HOperatorSet.BottomHat(RegGet, Elemt, out temp1);
                    //交差集
                    if (mToolParam.mTopPartMode == 0)
                    {
                        RegGet = temp1;
                    }
                    else
                    {
                        HOperatorSet.Difference(RegGet, temp1, out temp2);
                        RegGet = temp2;
                        temp1.Dispose();
                    }
                    Elemt.Dispose();
                }

                //显示
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(imageFinal);
                mDrawWind.SetDraw("fill");
                mDrawWind.SetColor("red");
                mDrawWind.DispObj(RegGet);
                mDrawWind.SetDraw("margin");
                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColor("green");
                mDrawWind.DispObj(regionFinal);
                imageReduce.Dispose();
                RegGet.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public int SelectArea(HObject objj1, List<StepInfo> StepInfoList, out HObject showRegion)
        {
            mToolParam.ResultString = "";
            mDrawWind.ClearWindow();
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                //图片源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = objj1;
                HTuple mChannel;
                HOperatorSet.CountChannels(imageFinal, out mChannel);
                if (mChannel != 1)
                {
                    mToolParam.ResultString = "图片非单通道图片，请重新输入";
                    return mToolParam.NgReturnValue;
                }
                //区域源
                HObject regionGet;
                if (mToolParam.mRegionStep > -1)
                    regionGet = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                else
                    regionGet = mToolParam.CheckRegion;
                HTuple area;
                HOperatorSet.RegionFeatures(regionGet, "area", out area);
                if (area <= 0)
                {
                    mToolParam.ResultString = "检测区域为空";
                    return mToolParam.NgReturnValue;
                }
                //区域转换
                HObject regionFinal;
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
                    HOperatorSet.AffineTransRegion(regionGet, out regionFinal, HomMat2D, "nearest_neighbor");
                }
                else
                    //直接赋值
                    regionFinal = regionGet;


                HObject imageReduce;
                HObject RegGet;
                //减少定义域
                HOperatorSet.ReduceDomain(imageFinal, regionFinal, out imageReduce);
                //获取区域
                if (!mToolParam.mIsDynThres)
                {
                    HOperatorSet.Threshold(imageReduce, out RegGet, mToolParam.mThresMin, mToolParam.mThresMax);
                }
                else
                {
                    HObject obj1;
                    HOperatorSet.MeanImage(imageReduce, out obj1, mToolParam.mDynMark, mToolParam.mDynMark);
                    HOperatorSet.DynThreshold(imageReduce, obj1, out RegGet, mToolParam.mDynOffset, mToolParam.mDynMode);
                    obj1.Dispose();
                }
                if (mToolParam.mIsBlob)
                {
                    HObject temp1;
                    if (mToolParam.mBlobMode == "腐蚀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ErosionCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ErosionRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "膨胀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.DilationCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.DilationRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "开运算")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.OpeningCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.OpeningRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ClosingCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ClosingRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    RegGet = temp1;
                }
                if (mToolParam.mIsTop)
                {
                    HObject temp1;
                    HObject temp2;
                    //结构体
                    HObject Elemt;
                    if (mToolParam.mTopByMode == 0)
                    {
                        HTuple a, r, c;
                        HOperatorSet.SmallestCircle(RegGet, out a, out r, out c);
                        HOperatorSet.GenCircle(out Elemt, a, r, c * mToolParam.mTopParam);
                    }
                    else
                    {
                        HTuple r, c, phi, length1, length2;
                        HOperatorSet.SmallestRectangle2(RegGet, out r, out c, out phi, out length1, out length2);
                        HOperatorSet.GenRectangle2(out Elemt, r, c, phi, length1 * mToolParam.mTopParam, length2 * mToolParam.mTopParam);
                    }
                    //运算
                    if (mToolParam.mTopMode == "顶帽")
                        HOperatorSet.TopHat(RegGet, Elemt, out temp1);
                    else
                        HOperatorSet.BottomHat(RegGet, Elemt, out temp1);
                    //交差集
                    if (mToolParam.mTopPartMode == 0)
                    {
                        RegGet = temp1;
                    }
                    else
                    {
                        HOperatorSet.Difference(RegGet, temp1, out temp2);
                        RegGet = temp2;
                        temp1.Dispose();
                    }
                    Elemt.Dispose();
                }


                HTuple features = new HTuple();
                features[0] = "area";
                features[1] = "height";
                features[2] = "width";
                features[3] = "rect2_len1";
                features[4] = "rect2_len2";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.mSelectAreaMax;
                minValue[1] = mToolParam.mHeightMin;
                minValue[2] = mToolParam.mWidthMin;
                minValue[3] = mToolParam.mLength1Min;
                minValue[4] = mToolParam.mLength2Min;
                HTuple maxValue = new HTuple();
                maxValue[0] = 9999999999;
                maxValue[1] = mToolParam.mHeightMax;
                maxValue[2] = mToolParam.mWidthMax;
                maxValue[3] = mToolParam.mLength1Max;
                maxValue[4] = mToolParam.mLength2Max;
                if (mToolParam.mConnectMode == 0) 
                {
                    HObject temp1;
                    HOperatorSet.SelectShape(RegGet, out temp1, features, "and", minValue, maxValue);
                    RegGet = temp1;
                }
                else
                {
                    HObject temp1;
                    HObject temp2;
                    HOperatorSet.Connection(RegGet, out temp1);
                    HTuple a, r, c;
                    HOperatorSet.AreaCenter(temp1, out a, out r, out c);
                    if (mToolParam.mSelectMode == 0)
                    {
                        HOperatorSet.SelectShape(temp1, out temp2, "area", "and", a.TupleMax(), mToolParam.mAreaMax);
                        temp1 = temp2;
                    }

                    else if (mToolParam.mSelectMode == 1) 
                    {
                        HOperatorSet.SelectShape(temp1, out temp2, "area", "and", 0, a.TupleMin());
                        temp1 = temp2;
                    }
                    HOperatorSet.SelectShape(temp1, out RegGet, features, "and", minValue, maxValue);
                    temp1.Dispose();
                }

                HObject rect2;
                HOperatorSet.ShapeTrans(RegGet, out rect2, "rectangle2");

                HTuple area1, height1, width1, rect_len1, rect_len2;
                HOperatorSet.RegionFeatures(RegGet, "area", out area1);
                HOperatorSet.RegionFeatures(RegGet, "height", out height1);
                HOperatorSet.RegionFeatures(RegGet, "width", out width1);
                HOperatorSet.RegionFeatures(RegGet, "rect2_len1", out rect_len1);
                HOperatorSet.RegionFeatures(RegGet, "rect2_len2", out rect_len2);

                for (int i = 0; i < area1.TupleLength(); i++)
                {
                    mToolParam.ResultString += (i + 1) + "#   area:= " + area1[i].D + "  ,height:= " + height1[i].D + "  ,width:= " + width1[i].D
                         + "  ,rect2_len1:= " + rect_len1[i].D.ToString("f2") + "  ,rect2_len2:= " + rect_len2[i].D.ToString("f2") + "\r\n";
                }
                //显示
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(imageFinal);
                mDrawWind.SetDraw("fill");
                mDrawWind.SetColor("red");
                mDrawWind.DispObj(RegGet);
                mDrawWind.SetDraw("margin");
                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColor("green");
                mDrawWind.DispObj(regionFinal);
                mDrawWind.SetColor("magenta");
                mDrawWind.DispObj(rect2);
                imageReduce.Dispose();
                RegGet.Dispose();
                rect2.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public int SelectAreaFinal(HObject objj1, List<StepInfo> StepInfoList, out HObject showRegion)
        {
            mToolParam.ResultString = "";
            mDrawWind.ClearWindow();
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                //图片源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = objj1;
                HTuple mChannel;
                HOperatorSet.CountChannels(imageFinal, out mChannel);
                if (mChannel != 1)
                {
                    mToolParam.ResultString = "图片非单通道图片，请重新输入";
                    return mToolParam.NgReturnValue;
                }
                //区域源
                HObject regionGet;
                if (mToolParam.mRegionStep > -1)
                    regionGet = StepInfoList[mToolParam.mRegionStep - 1].mToolRunResul.mRegionOutPut;
                else
                    regionGet = mToolParam.CheckRegion;
                HTuple area;
                HOperatorSet.RegionFeatures(regionGet, "area", out area);
                if (area <= 0)
                {
                    mToolParam.ResultString = "检测区域为空";
                    return mToolParam.NgReturnValue;
                }
                //区域转换
                HObject regionFinal;
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
                    HOperatorSet.HomMat2dScale(HomMat2D, StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[6],
             StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[6], StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2]
             , StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3], out HomMat2D);
                    HOperatorSet.AffineTransRegion(regionGet, out regionFinal, HomMat2D, "nearest_neighbor");
                }
                else
                    //直接赋值
                    regionFinal = regionGet;


                HObject imageReduce;
                HObject RegGet;
                //减少定义域
                HOperatorSet.ReduceDomain(imageFinal, regionFinal, out imageReduce);
                //获取区域
                if (!mToolParam.mIsDynThres)
                {
                    HOperatorSet.Threshold(imageReduce, out RegGet, mToolParam.mThresMin, mToolParam.mThresMax);
                }
                else
                {
                    HObject obj1;
                    HOperatorSet.MeanImage(imageReduce, out obj1, mToolParam.mDynMark, mToolParam.mDynMark);
                    HOperatorSet.DynThreshold(imageReduce, obj1, out RegGet, mToolParam.mDynOffset, mToolParam.mDynMode);
                    obj1.Dispose();
                }
                if (mToolParam.mIsBlob)
                {
                    HObject temp1;
                    if (mToolParam.mBlobMode == "腐蚀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ErosionCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ErosionRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "膨胀")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.DilationCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.DilationRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else if (mToolParam.mBlobMode == "开运算")
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.OpeningCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.OpeningRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    else
                    {
                        if (mToolParam.mBlobByMode == 0)
                            HOperatorSet.ClosingCircle(RegGet, out temp1, mToolParam.mBlobParam1);
                        else
                            HOperatorSet.ClosingRectangle1(RegGet, out temp1, mToolParam.mBlobParam1, mToolParam.mBlobParam2);
                    }
                    RegGet = temp1;
                }
                if (mToolParam.mIsTop)
                {
                    HObject temp1;
                    HObject temp2;
                    //结构体
                    HObject Elemt;
                    if (mToolParam.mTopByMode == 0)
                    {
                        HTuple a, r, c;
                        HOperatorSet.SmallestCircle(RegGet, out a, out r, out c);
                        HOperatorSet.GenCircle(out Elemt, a, r, c * mToolParam.mTopParam);
                    }
                    else
                    {
                        HTuple r, c, phi, length1, length2;
                        HOperatorSet.SmallestRectangle2(RegGet, out r, out c, out phi, out length1, out length2);
                        HOperatorSet.GenRectangle2(out Elemt, r, c, phi, length1 * mToolParam.mTopParam, length2 * mToolParam.mTopParam);
                    }
                    //运算
                    if (mToolParam.mTopMode == "顶帽")
                        HOperatorSet.TopHat(RegGet, Elemt, out temp1);
                    else
                        HOperatorSet.BottomHat(RegGet, Elemt, out temp1);
                    //交差集
                    if (mToolParam.mTopPartMode == 0)
                    {
                        RegGet = temp1;
                    }
                    else
                    {
                        HOperatorSet.Difference(RegGet, temp1, out temp2);
                        RegGet = temp2;
                        temp1.Dispose();
                    }
                    Elemt.Dispose();
                }


                HTuple features = new HTuple();
                features[0] = "area";
                features[1] = "height";
                features[2] = "width";
                features[3] = "rect2_len1";
                features[4] = "rect2_len2";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.mSelectAreaMax;
                minValue[1] = mToolParam.mHeightMin;
                minValue[2] = mToolParam.mWidthMin;
                minValue[3] = mToolParam.mLength1Min;
                minValue[4] = mToolParam.mLength2Min;
                HTuple maxValue = new HTuple();
                maxValue[0] = 9999999999;
                maxValue[1] = mToolParam.mHeightMax;
                maxValue[2] = mToolParam.mWidthMax;
                maxValue[3] = mToolParam.mLength1Max;
                maxValue[4] = mToolParam.mLength2Max;
                if (mToolParam.mConnectMode == 0)
                {
                    HObject temp1;
                    HOperatorSet.SelectShape(RegGet, out temp1, features, "and", minValue, maxValue);
                    RegGet = temp1;
                }
                else
                {
                    HObject temp1;
                    HObject temp2;
                    HOperatorSet.Connection(RegGet, out temp1);
                    HTuple a, r, c;
                    HOperatorSet.AreaCenter(temp1, out a, out r, out c);
                    if (mToolParam.mSelectMode == 0)
                    {
                        HOperatorSet.SelectShape(temp1, out temp2, "area", "and", a.TupleMax(), mToolParam.mAreaMax);
                        temp1 = temp2;
                    }

                    else if (mToolParam.mSelectMode == 1)
                    {
                        HOperatorSet.SelectShape(temp1, out temp2, "area", "and", 0, a.TupleMin());
                        temp1 = temp2;
                    }
                    HOperatorSet.SelectShape(temp1, out RegGet, features, "and", minValue, maxValue);
                    temp1.Dispose();
                }

                HTuple area1, height1, width1, rect_len1, rect_len2;
                HOperatorSet.RegionFeatures(RegGet, "area", out area1);
                HOperatorSet.RegionFeatures(RegGet, "height", out height1);
                HOperatorSet.RegionFeatures(RegGet, "width", out width1);
                HOperatorSet.RegionFeatures(RegGet, "rect2_len1", out rect_len1);
                HOperatorSet.RegionFeatures(RegGet, "rect2_len2", out rect_len2);

                HObject union,select,rect2;
                HTuple areaAll, r11, c11;
                HOperatorSet.Union1(RegGet, out union);
                HOperatorSet.SelectShape(union, out select, "area", "and", mToolParam.mAreaMin, mToolParam.mAreaMax);
                union.Dispose();
                HOperatorSet.AreaCenter(select, out areaAll, out r11, out c11);
                HOperatorSet.ShapeTrans(select, out rect2, "rectangle2");

                //有区域为NG
                if (mToolParam.mCheckMode == 1) 
                {
                    if (select.CountObj() > 0)
                    {
                        mToolParam.ResultString = "总面积为：" + areaAll + "\n"; ;
                        for (int i = 0; i < area1.TupleLength(); i++)
                        {
                            mToolParam.ResultString += (i + 1) + "#   area:= " + area1[i].D + "  ,height:= " + height1[i].D + "  ,width:= " + width1[i].D
                                 + "  ,rect2_len1:= " + rect_len1[i].D.ToString("f2") + "  ,rect2_len2:= " + rect_len2[i].D.ToString("f2") + "\r\n";
                        }
                        mToolParam.StepInfo.mToolRunResul.mRegionOutPut = RegGet;
                        mDrawWind.ClearWindow();
                        mDrawWind.DispObj(imageFinal);
                        mDrawWind.SetDraw("fill");
                        mDrawWind.SetColor("red");
                        mDrawWind.DispObj(RegGet);
                        mDrawWind.SetDraw("margin");
                        mDrawWind.SetLineWidth(2);
                        mDrawWind.SetColor("green");
                        mDrawWind.DispObj(regionFinal);
                        mDrawWind.SetColor("magenta");
                        mDrawWind.DispObj(rect2);
                        imageReduce.Dispose();
                        select.Dispose();
                        rect2.Dispose();
                        return mToolParam.NgReturnValue;
                    }
                    else
                    {
                        mDrawWind.ClearWindow();
                        mDrawWind.DispObj(imageFinal);
                        mDrawWind.SetDraw("margin");
                        mDrawWind.SetLineWidth(2);
                        mDrawWind.SetColor("green");
                        mDrawWind.DispObj(regionFinal);
                        imageReduce.Dispose();
                        select.Dispose();
                        rect2.Dispose();
                        mToolParam.StepInfo.mToolRunResul.mRegionOutPut = RegGet;
                        return 0;
                    }
                }
                else
                {
                    if (select.CountObj() > 0)
                    {
                        mToolParam.ResultString = "总面积为：" + areaAll + "\n";
                        for (int i = 0; i < area1.TupleLength(); i++)
                        {
                            mToolParam.ResultString += (i + 1) + "#   area:= " + area1[i].D + "  ,height:= " + height1[i].D + "  ,width:= " + width1[i].D
                                 + "  ,rect2_len1:= " + rect_len1[i].D.ToString("f2") + "  ,rect2_len2:= " + rect_len2[i].D.ToString("f2") + "\r\n";
                        }
                        mDrawWind.ClearWindow();
                        mDrawWind.DispObj(imageFinal);
                        mDrawWind.SetDraw("fill");
                        mDrawWind.SetColor("green");
                        mDrawWind.DispObj(RegGet);
                        mDrawWind.SetDraw("margin");
                        mDrawWind.SetLineWidth(2);
                        mDrawWind.SetColor("blue");
                        mDrawWind.DispObj(regionFinal);
                        mDrawWind.SetColor("magenta");
                        mDrawWind.DispObj(rect2);
                        imageReduce.Dispose();
                        select.Dispose();
                        rect2.Dispose();
                        mToolParam.StepInfo.mToolRunResul.mRegionOutPut = RegGet;
                        return 0;
                    }
                    else
                    {
                        mDrawWind.ClearWindow();
                        mDrawWind.DispObj(imageFinal);
                        mDrawWind.SetDraw("margin");
                        mDrawWind.SetLineWidth(2);
                        mDrawWind.SetColor("green");
                        mDrawWind.DispObj(regionFinal);
                        mToolParam.ResultString = "未找到区域NG！";
                        imageReduce.Dispose();
                        select.Dispose();
                        rect2.Dispose();
                        mToolParam.StepInfo.mToolRunResul.mRegionOutPut = RegGet;
                        return mToolParam.NgReturnValue;
                    }
                }
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        private void AffineRegion(int MoveX, int MoveY, double Scale, double Rotate, out HObject finalRegion)
        {
            HOperatorSet.GenEmptyObj(out finalRegion);
            HOperatorSet.GenEmptyObj(out mTempRegion);
            if (mToolParam.CheckRegion.Area < 1) 
            {
                mToolParam.ResultString = "检测区域为空！请先绘制！";
                return;
            }
            try
            {
                HTuple HomMat2D;
                HOperatorSet.HomMat2dIdentity(out HomMat2D);
                //缩放
                HOperatorSet.HomMat2dScale(HomMat2D, Scale, Scale, mToolParam.CheckRegion.Row, mToolParam.CheckRegion.Column, out HomMat2D);
                //旋转
                HOperatorSet.HomMat2dRotate(HomMat2D, new HTuple(Rotate).TupleRad(), mToolParam.CheckRegion.Row, mToolParam.CheckRegion.Column, out HomMat2D);
                //平移
                HOperatorSet.HomMat2dTranslate(HomMat2D, MoveX, MoveY, out HomMat2D);
                //转换
                HOperatorSet.AffineTransRegion(mToolParam.CheckRegion, out mTempRegion, HomMat2D, "nearest_neighbor");
                //复制
                finalRegion = mTempRegion.CopyObj(1, 1);
                HomMat2D.Dispose();
            }
            catch (System.Exception)
            {
            }
        }

        private void SureChangeRegion() 
        {
            mToolParam.CheckRegion = new HRegion(mTempRegion);
        }
    }
}
