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
    public class S1ResultParam
    {
        public int CurrClassID;
        public HObject CurrGetObj;
        public S1ResultParam()
        {
            HOperatorSet.GenEmptyObj(out CurrGetObj);
        }
    }

    [Serializable]
    public class S1Param
    {
        public int ClassID;
        public HRegion ClassRegion;
        public string ClassName;
        public int WidthMin;
        public int WidthMax;
        public int HeightMin;
        public int HeightMax;
        public string Logic;
        public int FiltArea;
        public int MaxWidth;
        public int MaxHeight;
        public int MaxNum;
        public int MaxRegionArea;
        public int AllAreaMax;

        public S1Param()
        {
            ClassID = 0;
            ClassRegion = new HRegion();
            ClassRegion.GenEmptyObj();
            ClassName = "";
            WidthMin = 0;
            WidthMax = 10000;
            HeightMin = 0;
            HeightMax = 10000;
            Logic = "and";
            FiltArea = 50;
            MaxNum = 2;
            MaxWidth = 30;
            MaxHeight = 30;
            MaxRegionArea = 200;
            AllAreaMax = 300;
        }
    }

    [Serializable]
    public class ToolHalconS1Param : ToolParamBase
    {
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;

        public string mAiModelPath;
        public int mBatchSize;
        public int mGpuID;

        public List<S1Param> mS1ParamList;

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
        public int ImageSourceStep
        {
            get => mImageSourceStep;
            set => mImageSourceStep = value;
        }
        public override string ResultString
        {
            get => mResultString;
            set => mResultString = value;
        }

        public delegate ResStatus InitAiModelDe();
        public delegate int ParamChangedDe(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj);
        public delegate int CheckAiDel(HObject obj1, List<StepInfo> StepInfoList);
        public delegate int DrawRoiDel(HObject obj1, int mIndex, int type, string mRoiType, int mMarkSize, out HObject obj2);
        public delegate int DrawRoi2Del(HObject obj1, int mIndex, string mRoiType, out HObject obj2);
        public delegate int SureRoiDel();
        public delegate int DeleRoiDel(int mIndex);

        [NonSerialized]
        public InitAiModelDe mInitAiModelDe;
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public CheckAiDel mCheckAiDel;
        [NonSerialized]
        public DrawRoi2Del mDrawRoi2Del;
        [NonSerialized]
        public DrawRoiDel mDrawRoiDel;
        [NonSerialized]
        public SureRoiDel mSureRoiDel;
        [NonSerialized]
        public DeleRoiDel mDeleRoiDel;

        public ToolHalconS1Param()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.HsemanticAI;
            mToolType = ToolType.HsemanticAI;
            mStepInfo.mToolResultType = ToolResultType.None;
            mToolResultType = ToolResultType.None;

            mImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;

            mBatchSize = 1;
            mGpuID = 0;
            mAiModelPath = "";
            mS1ParamList = new List<S1Param>();

            mShowName = "语义分割";
            mToolName = "语义分割";
            mStepInfo.mShowName = "语义分割";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolHalconS1 : ToolBase
    {
        ToolHalconS1Param mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;
        HTuple mHalconDlModel;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolHalconS1Param;
        }

        public ToolHalconS1(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolHalconS1Param;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.mAiModelPath == "")
                return -1;
            if (mToolParam.ForceOK)
                return 0;
            return Check(objj1, StepInfoList);
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            mHalconDlModel?.Dispose();
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

        public override int ToolRun(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mHalconDlModel == null)
            {
                LogHelper.WriteExceptionLog("未初始化AI");
                return mToolParam.NgReturnValue;
            }
            int res;
            List<S1ResultParam> r1Results;
            res = GetCheckResultToolRun(obj1, StepInfoList, out r1Results);
            if (res != 0)
                return res;
            string mes;
            res = CheckDebugToolRun(r1Results, out mes);
            if (res != 0)
            {
                StepJumpInfo.mAiLabel = mes;
                r1Results.Clear();
                Thread.Sleep(1);
                return res;
            }
            Thread.Sleep(1);
            return 0;

        }


        public override ResStatus InitAiResources()
        {
            try
            {
                if (mToolParam.mAiModelPath == "")
                    return ResStatus.Error;
                mHalconDlModel = new HTuple();
                mHalconDlModel.Dispose();
                HOperatorSet.ReadDlModel(mToolParam.mAiModelPath, out mHalconDlModel);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size", mToolParam.mBatchSize);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "gpu", mToolParam.mGpuID);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime_init", "immediately");
                HTuple hv_ClassIDs;
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                long[] ids = hv_ClassIDs.ToLArr();
                if (mToolParam.mS1ParamList.Count < ids.Count())
                {
                    for (int i = mToolParam.mS1ParamList.Count + 1; i < ids.Count(); i++)
                    {
                        S1Param par = new S1Param();
                        par.ClassID = i;
                        par.ClassName = "缺陷" + i;
                        mToolParam.mS1ParamList.Add(par);
                    }
                }
                else
                {
                    mToolParam.mS1ParamList.Clear();
                    for (int i = 1; i < ids.Count(); i++)
                    {
                        S1Param par = new S1Param();
                        par.ClassID = i;
                        par.ClassName = "缺陷" + i;
                        mToolParam.mS1ParamList.Add(par);
                    }
                }
                return ResStatus.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ResStatus.Error;
            }

        }

        public ResStatus InitAI()
        {
            try
            {
                if (mToolParam.mAiModelPath == "")
                    return ResStatus.Error;
                mHalconDlModel = new HTuple();
                mHalconDlModel.Dispose();
                HOperatorSet.ReadDlModel(mToolParam.mAiModelPath, out mHalconDlModel);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size", mToolParam.mBatchSize);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "gpu", 0);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime_init", "immediately");
                HTuple hv_ClassIDs;
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                long[] ids = hv_ClassIDs.ToLArr();
                if (mToolParam.mS1ParamList.Count < ids.Count() && mToolParam.mS1ParamList.Count > 0)
                {
                    for (int i = mToolParam.mS1ParamList.Count + 1; i < ids.Count(); i++)
                    {
                        S1Param par = new S1Param();
                        par.ClassID = i; 
                        par.ClassName = "缺陷"+ i;
                        mToolParam.mS1ParamList.Add(par);
                    }
                }
                else
                {
                    mToolParam.mS1ParamList.Clear();
                    for (int i = 1; i < ids.Count(); i++)
                    {
                        S1Param par = new S1Param();
                        par.ClassID = i;
                        par.ClassName = "缺陷" + i;
                        mToolParam.mS1ParamList.Add(par);
                    }
                }

                return ResStatus.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ResStatus.Error;
            }
        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mInitAiModelDe += InitAI;
                mToolParam.mParamChangedDe += ParamChanged;
                mToolParam.mCheckAiDel += Check;
                mToolParam.mDrawRoi2Del += DrawRoi2;
                mToolParam.mDrawRoiDel += DrawRoi;
                mToolParam.mDeleRoiDel += DeleRoi;
            }
            else
            {
                mToolParam.mInitAiModelDe = null;
                mToolParam.mParamChangedDe = null;
                mToolParam.mCheckAiDel = null;
                mToolParam.mDrawRoi2Del = null;
                mToolParam.mDrawRoiDel = null;
                mToolParam.mDeleRoiDel = null;
            }

            return ResStatus.OK;
        }


        private int Check(HObject ho_Image, List<StepInfo> StepInfoList)
        {
            int res;
            List<S1ResultParam> r1Results;
            res = GetCheckResult(ho_Image, StepInfoList, out r1Results);
            if (res != 0)
                return res;
            res = CheckDebug(r1Results, StepInfoList);
            if (res != 0)
            {
                r1Results.Clear();
                return res;
            }
            return 0;
        }


        public int DrawRoi(HObject obj1, int mIndex, int type, string mRoiType, int mMarkSize, out HObject showRegion)
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
                                HOperatorSet.Union2(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.mS1ParamList[mIndex].ClassRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.mS1ParamList[mIndex].ClassRegion = r1;
                            }
                            else
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Difference(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.mS1ParamList[mIndex].ClassRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.mS1ParamList[mIndex].ClassRegion = r1;
                            }
                        }
                        mDrawWind.SetRgba(255, 0, 0, 90);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.mS1ParamList[mIndex].ClassRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 90);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.mS1ParamList[mIndex].ClassRegion);
                showRegion = mToolParam.mS1ParamList[mIndex].ClassRegion;
                Thread.Sleep(20);
                return 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int DrawRoi2(HObject obj1, int mIndex, string mRoiType, out HObject showRegion)
        {
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("fill");
            mDrawWind.SetRgba(255, 0, 0, 120);
            mDrawWind.DispObj(mToolParam.mS1ParamList[mIndex].ClassRegion);
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                if (mRoiType == "circle")
                {
                    double row, column, radius;
                    HObject obj;
                    mDrawWind.DrawCircle(out row, out column, out radius);
                    HOperatorSet.GenCircle(out obj, row, column, radius);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.mS1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle1")
                {
                    double row, column, row2, column2;
                    HObject obj;
                    mDrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                    HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.mS1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle2")
                {
                    double row, column, phi, length1, length2;
                    HObject obj;
                    mDrawWind.DrawRectangle2(out row, out column, out phi, out length1, out length2);
                    HOperatorSet.GenRectangle2(out obj, row, column, phi, length1, length2);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.mS1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }
                else if (mRoiType == "ellipse")
                {
                    double row, column, phi, radius1, radius2;
                    HObject obj;
                    mDrawWind.DrawEllipse(out row, out column, out phi, out radius1, out radius2);
                    HOperatorSet.GenEllipse(out obj, row, column, phi, radius1, radius2);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.mS1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mS1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.mS1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }

                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.mS1ParamList[mIndex].ClassRegion);
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }


        public int DeleRoi(int mIndex)
        {
            mToolParam.mS1ParamList[mIndex].ClassRegion.Dispose();
            mToolParam.mS1ParamList[mIndex].ClassRegion.GenEmptyObj();
            mDrawWind.ClearWindow();
            return 0;
        }

        private int GetCheckResult(HObject ho_Image, List<StepInfo> StepInfoList, out List<S1ResultParam> r1Results)
        {
            mToolParam.ResultString = "";
            r1Results = new List<S1ResultParam>();
            if (mHalconDlModel == null || mHalconDlModel.Length == 0) 
            {
                mToolParam.ResultString = "模型为空";
                return mToolParam.NgReturnValue;
            }
            //输入图片源，即为推理图片
            HObject imageFinal;
            if (mToolParam.mImageSourceStep > -1)
                imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
            else
                imageFinal = ho_Image;

            //求模型的通道
            HTuple hv_ImageDimensions;
            HOperatorSet.GetDlModelParam(mHalconDlModel, "image_dimensions", out hv_ImageDimensions);
            HTuple channel;
            HOperatorSet.CountChannels(imageFinal, out channel);
            if (hv_ImageDimensions[2].I != channel.I)
            {
                mToolParam.ResultString = "模型图片训练通道数与推理图片通道数不一致！";
                return mToolParam.NgReturnValue;
            }

            //仿射矩阵，用来移动检测区域
            HTuple HomMat2D = new HTuple();
            if (mToolParam.mShapeModelStep > -1)
            {
                HOperatorSet.VectorAngleToRigid(
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                0,
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                out HomMat2D);
            }


            HObject ho_ImagePreprocessed;
            HObject ho_SegImage, ho_Confidence;


            // Local control variables 

            HTuple hv_ClassIDs = new HTuple();
            HTuple hv_DLSample = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DLResult = new HTuple();
            HTuple hv_I = new HTuple(), hv_Area = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple();
            HTuple hv_HomMat2DIdentity = new HTuple();
            HTuple hv_HomMat2DRotate = new HTuple();
            HTuple hv_H1 = new HTuple();
            HTuple hv_W1 = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImagePreprocessed);
            HOperatorSet.GenEmptyObj(out ho_SegImage);
            HOperatorSet.GenEmptyObj(out ho_Confidence);
            try
            {
                hv_ClassIDs.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                hv_DLSample.Dispose();
                HOperatorSet.CreateDict(out hv_DLSample);
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(imageFinal, out hv_Width, out hv_Height);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ImagePreprocessed.Dispose();
                    HOperatorSet.ZoomImageSize(imageFinal, out ho_ImagePreprocessed, hv_ImageDimensions.TupleSelect(
                        0), hv_ImageDimensions.TupleSelect(1), "constant");
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConvertImageType(ho_ImagePreprocessed, out ExpTmpOutVar_0, "real");
                    ho_ImagePreprocessed.Dispose();
                    ho_ImagePreprocessed = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ScaleImage(ho_ImagePreprocessed, out ExpTmpOutVar_0, 1, -127);
                    ho_ImagePreprocessed.Dispose();
                    ho_ImagePreprocessed = ExpTmpOutVar_0;
                }
                HOperatorSet.SetDictObject(ho_ImagePreprocessed, hv_DLSample, "image");
                hv_DLResult.Dispose();
                HOperatorSet.ApplyDlModel(mHalconDlModel, hv_DLSample, new HTuple(), out hv_DLResult);
                ho_SegImage.Dispose();
                HOperatorSet.GetDictObject(out ho_SegImage, hv_DLResult, "segmentation_image");
                ho_Confidence.Dispose();
                HOperatorSet.GetDictObject(out ho_Confidence, hv_DLResult, "segmentation_confidence");


                //生成区域仿射矩阵
                hv_HomMat2DIdentity.Dispose();
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                hv_HomMat2DRotate.Dispose();
                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, 0, 0, 0, out hv_HomMat2DRotate);

                hv_H1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_H1 = (hv_Height.TupleReal()
                        ) / (((hv_ImageDimensions.TupleSelect(1))).TupleReal());
                }
                hv_W1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_W1 = (hv_Width.TupleReal()
                        ) / (((hv_ImageDimensions.TupleSelect(0))).TupleReal());
                }
                hv_HomMat2DScale.Dispose();
                HOperatorSet.HomMat2dScale(hv_HomMat2DRotate, hv_H1, hv_W1, 0, 0, out hv_HomMat2DScale);


                for (hv_I = 1; (int)hv_I <= (int)((new HTuple(hv_ClassIDs.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                {

                    HObject ho_RegionAffineTrans, ho_Region;
                    //得到标签的检测结果
                    HOperatorSet.Threshold(ho_SegImage, out ho_Region, hv_I, hv_I);
                    //将检测到的区域还原原比例大小
                    HOperatorSet.AffineTransRegion(ho_Region, out ho_RegionAffineTrans, hv_HomMat2DScale, "nearest_neighbor");

                    //看输入源是否为裁剪图片,如果是，得到的区域则加上偏移量
                    if (mToolParam.mImageSourceStep > -1 && StepInfoList[mToolParam.mImageSourceStep - 1].mToolType == ToolType.CropImage)
                    {
                        HObject temp;
                        HOperatorSet.MoveRegion(ho_RegionAffineTrans, out temp,
                            StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mParamOutPut[0],
                            StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mParamOutPut[1]);
                        ho_RegionAffineTrans.Dispose();
                        ho_RegionAffineTrans = temp;
                    }

                    //将设定区域进行仿射
                    HObject region;
                    if (mToolParam.mShapeModelStep > -1)
                        HOperatorSet.AffineTransRegion(mToolParam.mS1ParamList[hv_I.I - 1].ClassRegion, out region, HomMat2D, "nearest_neighbor");
                    else
                        region = mToolParam.mS1ParamList[hv_I.I - 1].ClassRegion.CopyObj(1, 1);

                    HObject region1;
                    //求区域的是否有交集
                    HOperatorSet.Intersection(ho_RegionAffineTrans, region, out region1);
                    ho_RegionAffineTrans.Dispose();
                    region.Dispose();
                    HTuple area;
                    HOperatorSet.RegionFeatures(region1, "area", out area);
                    if (area > 0)
                    {
                        S1ResultParam par = new S1ResultParam();
                        par.CurrClassID = hv_I.I - 1;
                        par.CurrGetObj = region1;
                        r1Results.Add(par);
                    }
                }
                ho_ImagePreprocessed.Dispose();
                ho_SegImage.Dispose();
                ho_Confidence.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_I.Dispose();
                hv_Area.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_H1.Dispose();
                hv_W1.Dispose();
                hv_HomMat2DScale.Dispose();
                return 0;
            }

            catch (System.Exception ex)
            {
                ho_ImagePreprocessed.Dispose();
                ho_SegImage.Dispose();
                ho_Confidence.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_I.Dispose();
                hv_Area.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_H1.Dispose();
                hv_W1.Dispose();
                hv_HomMat2DScale.Dispose();
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        private int GetCheckResultToolRun(HObject ho_Image, List<StepInfo> StepInfoList, out List<S1ResultParam> r1Results)
        {
            mToolParam.ResultString = "";
            r1Results = new List<S1ResultParam>();
            if (mHalconDlModel == null)
            {
                LogHelper.WriteExceptionLog("模型为空");
                return mToolParam.NgReturnValue;
            }
            //输入图片源
            HObject imageFinal;
            if (mToolParam.mImageSourceStep > -1)
                imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
            else
                imageFinal = ho_Image;

            //求模型的通道
            HTuple hv_ImageDimensions;
            HOperatorSet.GetDlModelParam(mHalconDlModel, "image_dimensions", out hv_ImageDimensions);
            HTuple channel;
            HOperatorSet.CountChannels(imageFinal, out channel);
            if (hv_ImageDimensions[2].I != channel.I)
            {
                LogHelper.WriteExceptionLog("模型图片训练通道数与推理图片通道数不一致！");
                return mToolParam.NgReturnValue;
            }

            //仿射区域
            HTuple HomMat2D = new HTuple();
            if (mToolParam.mShapeModelStep > -1)
            {
                HOperatorSet.VectorAngleToRigid(
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                0,
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                out HomMat2D);
            }


            HObject ho_ImagePreprocessed;
            HObject ho_SegImage, ho_Confidence;

            // Local control variables 

            HTuple hv_ClassIDs = new HTuple();
            HTuple hv_DLSample = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DLResult = new HTuple();
            HTuple hv_I = new HTuple(), hv_Area = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple();
            HTuple hv_HomMat2DIdentity = new HTuple();
            HTuple hv_HomMat2DRotate = new HTuple();
            HTuple hv_H1 = new HTuple();
            HTuple hv_W1 = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImagePreprocessed);
            HOperatorSet.GenEmptyObj(out ho_SegImage);
            HOperatorSet.GenEmptyObj(out ho_Confidence);
            try
            {
                hv_ClassIDs.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                hv_DLSample.Dispose();
                HOperatorSet.CreateDict(out hv_DLSample);
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(imageFinal, out hv_Width, out hv_Height);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ImagePreprocessed.Dispose();
                    HOperatorSet.ZoomImageSize(imageFinal, out ho_ImagePreprocessed, hv_ImageDimensions.TupleSelect(
                        0), hv_ImageDimensions.TupleSelect(1), "constant");
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ConvertImageType(ho_ImagePreprocessed, out ExpTmpOutVar_0, "real");
                    ho_ImagePreprocessed.Dispose();
                    ho_ImagePreprocessed = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ScaleImage(ho_ImagePreprocessed, out ExpTmpOutVar_0, 1, -127);
                    ho_ImagePreprocessed.Dispose();
                    ho_ImagePreprocessed = ExpTmpOutVar_0;
                }
                HOperatorSet.SetDictObject(ho_ImagePreprocessed, hv_DLSample, "image");
                hv_DLResult.Dispose();
                HOperatorSet.ApplyDlModel(mHalconDlModel, hv_DLSample, new HTuple(), out hv_DLResult);
                ho_SegImage.Dispose();
                HOperatorSet.GetDictObject(out ho_SegImage, hv_DLResult, "segmentation_image");
                ho_Confidence.Dispose();
                HOperatorSet.GetDictObject(out ho_Confidence, hv_DLResult, "segmentation_confidence");


                //生成区域仿射矩阵
                hv_HomMat2DIdentity.Dispose();
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                hv_HomMat2DRotate.Dispose();
                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, 0, 0, 0, out hv_HomMat2DRotate);

                hv_H1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_H1 = (hv_Height.TupleReal()
                        ) / (((hv_ImageDimensions.TupleSelect(1))).TupleReal());
                }
                hv_W1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_W1 = (hv_Width.TupleReal()
                        ) / (((hv_ImageDimensions.TupleSelect(0))).TupleReal());
                }
                hv_HomMat2DScale.Dispose();
                HOperatorSet.HomMat2dScale(hv_HomMat2DRotate, hv_H1, hv_W1, 0, 0, out hv_HomMat2DScale);


                for (hv_I = 1; (int)hv_I <= (int)((new HTuple(hv_ClassIDs.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                {

                    HObject ho_RegionAffineTrans, ho_Region;
                    HOperatorSet.Threshold(ho_SegImage, out ho_Region, hv_I, hv_I);
                    HOperatorSet.AffineTransRegion(ho_Region, out ho_RegionAffineTrans, hv_HomMat2DScale, "nearest_neighbor");

                    //看输入源是否为裁剪图片,如果是，得到的区域则加上偏移量
                    if (mToolParam.mImageSourceStep > -1 && StepInfoList[mToolParam.mImageSourceStep - 1].mToolType == ToolType.CropImage)
                    {
                        HObject temp;
                        HOperatorSet.MoveRegion(ho_RegionAffineTrans, out temp,
                            StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mParamOutPut[0],
                            StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mParamOutPut[1]);
                        ho_RegionAffineTrans.Dispose();
                        ho_RegionAffineTrans = temp;
                    }

                    //区域进行放射
                    HObject region;
                    if (mToolParam.mShapeModelStep > -1)
                        HOperatorSet.AffineTransRegion(mToolParam.mS1ParamList[hv_I.I - 1].ClassRegion, out region, HomMat2D, "nearest_neighbor");
                    else
                        region = mToolParam.mS1ParamList[hv_I.I - 1].ClassRegion.CopyObj(1, 1);

                    HObject region1;
                    //求区域的是否有交集
                    HOperatorSet.Intersection(ho_RegionAffineTrans, region, out region1);
                    ho_RegionAffineTrans.Dispose();
                    region.Dispose();
                    HTuple area;
                    HOperatorSet.RegionFeatures(region1, "area", out area);
                    if (area > 0)
                    {
                        S1ResultParam par = new S1ResultParam();
                        par.CurrClassID = hv_I.I - 1;
                        par.CurrGetObj = region1;
                        r1Results.Add(par);
                    }
                }
                ho_ImagePreprocessed.Dispose();
                ho_SegImage.Dispose();
                ho_Confidence.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_I.Dispose();
                hv_Area.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_H1.Dispose();
                hv_W1.Dispose();
                hv_HomMat2DScale.Dispose();
                return 0;
            }

            catch (System.Exception ex)
            {
                ho_ImagePreprocessed.Dispose();
                ho_SegImage.Dispose();
                ho_Confidence.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_I.Dispose();
                hv_Area.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_H1.Dispose();
                hv_W1.Dispose();
                hv_HomMat2DScale.Dispose();
                LogHelper.WriteExceptionLog(ex);
                return -1;
            }
        }

        public int CheckDebug(List<S1ResultParam> r1Results, List<StepInfo> StepInfoList)
        {
            if (r1Results.Count == 0)
                return 0;
            HTuple HomMat2D = new HTuple();
            if (mToolParam.mShapeModelStep > -1)
            {
                HOperatorSet.VectorAngleToRigid(
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                0,
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                out HomMat2D);
            }
            int res = 0;
            mToolParam.ResultString = "";
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("margin");
            mDrawWind.SetFont("Consolas-24");
            for (int i = 0; i < r1Results.Count; i++)
            {
                //获取结果
                HObject obj = r1Results[i].CurrGetObj;
                HObject obj1, obj2, objSelect1, objSelect3, objUnion1;
                //将区域分割
                HOperatorSet.Connection(obj, out obj1);

                //显示检测的区域
                mDrawWind.SetColor(ShowColors.Colors[r1Results[i].CurrClassID]);
                mDrawWind.SetLineWidth(2);
                if (mToolParam.mShapeModelStep > -1)
                {
                    HObject obj3;
                    //检测区域移动
                    HOperatorSet.AffineTransRegion(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassRegion, out obj3, HomMat2D, "nearest_neighbor");
                    mDrawWind.DispObj(obj3);
                    obj3.Dispose();
                }
                else
                {
                    mDrawWind.DispObj(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassRegion);
                }

                //先按长宽进行筛选
                HTuple features = new HTuple();
                features[0] = "height";
                features[1] = "width";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].HeightMin;
                minValue[1] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].WidthMin;
                HTuple maxValue = new HTuple();
                maxValue[0] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].HeightMax;
                maxValue[1] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].WidthMax;
                HOperatorSet.SelectShape(obj1, out obj2, features, mToolParam.mS1ParamList[r1Results[i].CurrClassID].Logic, minValue, maxValue);

                //获取过滤后的区域
                HOperatorSet.SelectShape(obj2, out objSelect1, "area", "and",
                    mToolParam.mS1ParamList[r1Results[i].CurrClassID].FiltArea, 999999999);
                //计算所有区域面积
                HTuple a1, r1, c1;
                HOperatorSet.AreaCenter(objSelect1, out a1, out r1, out c1);

                //判断个数是否超出设定
                if (objSelect1.CountObj() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxNum)
                {
                    res = 1;
                    mToolParam.ResultString = "NG-->查找个数  当前个数为： " + objSelect1.CountObj() + "\n";
                    for (int j = 0; j < objSelect1.CountObj(); j++)
                    {
                        HObject mSelect;
                        HOperatorSet.SelectObj(objSelect1, out mSelect, j + 1);
                        HTuple area, height, width;
                        HOperatorSet.RegionFeatures(mSelect, "area", out area);
                        HOperatorSet.RegionFeatures(mSelect, "height", out height);
                        HOperatorSet.RegionFeatures(mSelect, "width", out width);
                        mDrawWind.DispObj(mSelect);
                        int r = Convert.ToInt32(r1[j].D);
                        int c = Convert.ToInt32(c1[j].D);
                        mDrawWind.SetFont("Consolas-12");
                        mDrawWind.SetTposition(r, c);
                        mDrawWind.DispText(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString(), "image", r - 50, c, "red", new HTuple(), new HTuple());
                        mToolParam.ResultString += i + "# " + mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString() +
                            "缺陷， area:= " + area + " ,height:=" + height + " ,width:=" + width + "\r\n";
                        mSelect.Dispose();
                    }
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    continue;
                }

                //判断最大宽度是否超设定
                HTuple mWidth;
                HOperatorSet.RegionFeatures(objSelect1, "width", out mWidth);
                if (objSelect1.CountObj() > 0 && mWidth.TupleMax() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxWidth) 
                {
                    res = 1;
                    mToolParam.ResultString = "NG-->最大宽度超设定  最大宽度为： " + mWidth.TupleMax() + "\n";
                    for (int j = 0; j < objSelect1.CountObj(); j++)
                    {
                        HObject mSelect;
                        HOperatorSet.SelectObj(objSelect1, out mSelect, j + 1);
                        HTuple area, height, width;
                        HOperatorSet.RegionFeatures(mSelect, "area", out area);
                        HOperatorSet.RegionFeatures(mSelect, "height", out height);
                        HOperatorSet.RegionFeatures(mSelect, "width", out width);
                        mDrawWind.DispObj(mSelect);
                        int r = Convert.ToInt32(r1[j].D);
                        int c = Convert.ToInt32(c1[j].D);
                        mDrawWind.SetFont("Consolas-12");
                        mDrawWind.SetTposition(r, c);
                        mDrawWind.DispText(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString(), "image", r - 50, c, "red", new HTuple(), new HTuple());
                        mToolParam.ResultString += i + "# " + mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString() +
                            "缺陷， area:= " + area + " ,height:=" + height + " ,width:=" + width + "\r\n";
                        mSelect.Dispose();
                    }
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    continue;
                }

                //判断最大高度是否超设定
                HTuple mHeight;
                HOperatorSet.RegionFeatures(objSelect1, "height", out mHeight);
                if (objSelect1.CountObj() > 0 && mHeight.TupleMax() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxHeight)
                {
                    res = 1;
                    mToolParam.ResultString = "NG-->最大高度超设定  最大高度为： " + mWidth.TupleMax() + "\n";
                    for (int j = 0; j < objSelect1.CountObj(); j++)
                    {
                        HObject mSelect;
                        HOperatorSet.SelectObj(objSelect1, out mSelect, j + 1);
                        HTuple area, height, width;
                        HOperatorSet.RegionFeatures(mSelect, "area", out area);
                        HOperatorSet.RegionFeatures(mSelect, "height", out height);
                        HOperatorSet.RegionFeatures(mSelect, "width", out width);
                        mDrawWind.DispObj(mSelect);
                        int r = Convert.ToInt32(r1[j].D);
                        int c = Convert.ToInt32(c1[j].D);
                        mDrawWind.SetFont("Consolas-12");
                        mDrawWind.SetTposition(r, c);
                        mDrawWind.DispText(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString(), "image", r - 50, c, "red", new HTuple(), new HTuple());
                        mToolParam.ResultString += i + "# " + mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString() +
                            "缺陷， area:= " + area + " ,height:=" + height + " ,width:=" + width + "\r\n";
                        mSelect.Dispose();
                    }
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    continue;
                }


                //当个数大于0，且最大面积超出设定
                if (objSelect1.CountObj() > 0 && a1.TupleMax() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxRegionArea)
                {
                    res = 1;
                    mToolParam.ResultString += "NG-->最大的区域超出   ： " + a1.TupleMax() + "\n";
                    for (int j = 0; j < objSelect1.CountObj(); j++)
                    {
                        HObject mSelect;
                        HOperatorSet.SelectObj(objSelect1, out mSelect, j + 1);
                        HTuple area, height, width;
                        HOperatorSet.RegionFeatures(mSelect, "area", out area);
                        HOperatorSet.RegionFeatures(mSelect, "height", out height);
                        HOperatorSet.RegionFeatures(mSelect, "width", out width);
                        mDrawWind.DispObj(mSelect);
                        int r = Convert.ToInt32(r1[j].D);
                        int c = Convert.ToInt32(c1[j].D);
                        mDrawWind.SetFont("Consolas-12");
                        mDrawWind.SetTposition(r, c);
                        mDrawWind.DispText(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString(), "image", r - 50, c, "red", new HTuple(), new HTuple());
                        mToolParam.ResultString += i + "# " + mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString() +
                            "缺陷， area:= " + area + " ,height:=" + height + " ,width:=" + width + "\r\n";
                        mSelect.Dispose();
                    }
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    continue;
                }

                //联合区域
                HOperatorSet.Union1(objSelect1, out objUnion1);
                HOperatorSet.SelectShape(objUnion1, out objSelect3, "area", "and",
                    mToolParam.mS1ParamList[r1Results[i].CurrClassID].AllAreaMax, 999999999);

                //总面积超出设定
                if (objSelect3.CountObj() > 0)
                {
                    res = 1;
                    HTuple a2, r2, c2;
                    //计算面积
                    HOperatorSet.AreaCenter(objSelect3, out a2, out r2, out c2);
                    mToolParam.ResultString = "NG-->总面积超出设定  面积为： " + a2.D.ToString() + "\n";
                    for (int j = 0; j < objSelect1.CountObj(); j++)
                    {
                        HObject mSelect;
                        HOperatorSet.SelectObj(objSelect1, out mSelect, j + 1);
                        HTuple area, height, width;
                        HOperatorSet.RegionFeatures(mSelect, "area", out area);
                        HOperatorSet.RegionFeatures(mSelect, "height", out height);
                        HOperatorSet.RegionFeatures(mSelect, "width", out width);
                        mDrawWind.DispObj(mSelect);
                        int r = Convert.ToInt32(r1[j].D);
                        int c = Convert.ToInt32(c1[j].D);
                        mDrawWind.SetFont("Consolas-12");
                        mDrawWind.SetTposition(r, c);
                        mDrawWind.DispText(mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString(), "image", r - 50, c, "red", new HTuple(), new HTuple());
                        mToolParam.ResultString += i + "# " + mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName.ToString() +
                            "缺陷， area:= " + area + " ,height:=" + height + " ,width:=" + width + "\r\n";
                        mSelect.Dispose();
                    }
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    objSelect3.Dispose();
                    objUnion1.Dispose();
                    continue;
                }
            }
            return res;
        }

        public int CheckDebugToolRun(List<S1ResultParam> r1Results, out string labelname)
        {
            labelname = "";
            if (r1Results.Count == 0)
                return 0;
            int res = 0;
            for (int i = 0; i < r1Results.Count; i++)
            {
                //获取结果
                HObject obj = r1Results[i].CurrGetObj;
                HObject obj1, obj2, objSelect1, objSelect3, objUnion1;
                //将区域分割
                HOperatorSet.Connection(obj, out obj1);
                //先按长宽进行筛选
                HTuple features = new HTuple();
                features[0] = "height";
                features[1] = "width";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].HeightMin;
                minValue[1] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].WidthMin;
                HTuple maxValue = new HTuple();
                maxValue[0] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].HeightMax;
                maxValue[1] = mToolParam.mS1ParamList[r1Results[i].CurrClassID].WidthMax;
                HOperatorSet.SelectShape(obj1, out obj2, features, mToolParam.mS1ParamList[r1Results[i].CurrClassID].Logic, minValue, maxValue);

                //获取过滤后的区域
                HOperatorSet.SelectShape(obj2, out objSelect1, "area", "and",
                    mToolParam.mS1ParamList[r1Results[i].CurrClassID].FiltArea, 999999999);
                //计算所有区域面积
                HTuple a1, r1, c1;
                HOperatorSet.AreaCenter(objSelect1, out a1, out r1, out c1);

                //判断个数是否超出设定
                if (objSelect1.CountObj() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxNum)
                {
                    labelname = mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName;
                    res = 1;
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    return res;
                }

                //判断最大宽度是否超设定
                HTuple mWidth;
                HOperatorSet.RegionFeatures(objSelect1, "width", out mWidth);
                if (objSelect1.CountObj() > 0 && mWidth.TupleMax() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxWidth)
                {
                    labelname = mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName;
                    res = 1;
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    return res;
                }

                //判断最大高度是否超设定
                HTuple mHeight;
                HOperatorSet.RegionFeatures(objSelect1, "height", out mHeight);
                if (objSelect1.CountObj() > 0 && mHeight.TupleMax() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxHeight)
                {
                    labelname = mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName;
                    res = 1;
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    return res;
                }

                //当个数大于0，且最大面积超出设定
                if (objSelect1.CountObj() > 0 && a1.TupleMax() > mToolParam.mS1ParamList[r1Results[i].CurrClassID].MaxRegionArea)
                {
                    labelname = mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName;
                    res = 1;
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    return res;
                }

                //联合区域
                HOperatorSet.Union1(objSelect1, out objUnion1);
                HOperatorSet.SelectShape(objUnion1, out objSelect3, "area", "and",
                    mToolParam.mS1ParamList[r1Results[i].CurrClassID].AllAreaMax, 999999999);

                //总面积超出设定
                if (objSelect3.CountObj() > 0)
                {
                    labelname = mToolParam.mS1ParamList[r1Results[i].CurrClassID].ClassName;
                    res = 1;
                    obj1.Dispose();
                    obj2.Dispose();
                    objSelect1.Dispose();
                    objSelect3.Dispose();
                    objUnion1.Dispose();
                    return res;
                }
            }
            return res;
        }
    }
}
