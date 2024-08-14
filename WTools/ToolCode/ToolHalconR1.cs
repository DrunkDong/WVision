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
    public class R1ResultParam
    {
        public double CurrScore;
        public int CurrClassID;
        public HObject CurrGetObj;
        public R1ResultParam()
        {
            HOperatorSet.GenEmptyObj(out CurrGetObj);
        }
    }
    [Serializable]
    public class R1Param
    {
        public int ClassID;
        public double Score;
        public HRegion ClassRegion;
        public string ClassName;
        public int WidthMin;
        public int WidthMax;
        public int HeightMin;
        public int HeightMax;
        public int AreaSingleMin;
        public int AreaSingleMax;
        public int AreaMin;
        public int AreaMax;

        public R1Param()
        {
            ClassID = 0;
            Score = 0.5;
            ClassRegion = new HRegion();
            ClassRegion.GenEmptyObj();
            ClassName = "";
            WidthMin = 0;
            WidthMax = 10000;
            HeightMin = 0;
            HeightMax = 10000;
            AreaSingleMin = 0;
            AreaSingleMax = 99999999;
            AreaMin = 0;
            AreaMax = 99999999;
        }
    }
    [Serializable]
    public class ToolHalconR1Param : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;//图像源
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;

        public string mAiModelPath;
        public double mMaxOverlap;
        public double mMaxOverlapClass;
        public int mMaxDetectNum;
        public int mBatchSize;
        public int mGpuID;

        public List<R1Param> R1ParamList;

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

        public delegate ResStatus InitAiModelDe();
        public delegate int ParamChangedDe(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj);
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

        public ToolHalconR1Param()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.HObjecDetect1;
            mShowName = "目标检测1";
            mToolName = "目标检测1";
            mStepInfo.mShowName = "目标检测1";
            mToolType = ToolType.HObjecDetect1;

            mShapeModelStep = -1;
            mShapeModelMark = -1;
            mImageSourceStep = -1;
            mImageSourceMark = -1;
            mAiModelPath = "";
            R1ParamList = new List<R1Param>();

            mMaxOverlap = 0.2;
            mMaxOverlapClass = 0.2;
            mMaxDetectNum = 100;
            mBatchSize = 1;
            mGpuID = 0;

            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }


    public class ToolHalconR1 : ToolBase
    {
        ToolHalconR1Param mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;
        HTuple mHalconDlModel;

        List<R1ResultParam> ResultList;

        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolHalconR1Param;
        }

        public ToolHalconR1(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolHalconR1Param;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
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

        public override int ToolRun(HObject obj11, Bitmap obj22, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mHalconDlModel == null)
            {
                LogHelper.WriteExceptionLog("未初始化AI");
                return -1;
            }
            List<R1ResultParam> r1Results;
            int res;
            res = GetCheckResultToolRun(obj11, StepInfoList, out r1Results);
            if (res != 0)
                return res;
            string ss;
            res = CheckDebugToolRun(r1Results, out ss);
            if (res != 0)
            {
                StepJumpInfo.mAiLabel = ss;
                r1Results.Clear();
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
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size_multiplier", 1);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime", "gpu"); 
                HOperatorSet.SetDlModelParam(mHalconDlModel, "gpu", mToolParam.mGpuID);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime_init", "immediately");
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
                HTuple hv_ClassIDs;
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                //设置置信度阈值
                HOperatorSet.SetDlModelParam(mHalconDlModel, "min_confidence", 0.1);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size", mToolParam.mBatchSize);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size_multiplier", 1);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime", "gpu");
                HOperatorSet.SetDlModelParam(mHalconDlModel, "gpu", mToolParam.mGpuID);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime_init", "immediately");
                long[] ids = hv_ClassIDs.ToLArr();
                if (mToolParam.R1ParamList.Count != hv_ClassIDs.TupleLength()) 
                {
                    mToolParam.R1ParamList.Clear();
                    for (int i = 0; i < ids.Count(); i++)
                    {
                        R1Param par = new R1Param();
                        par.ClassID = i;
                        mToolParam.R1ParamList.Add(par);
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

        public int Check(HObject ho_Image, List<StepInfo> StepInfoList) 
        {
            int res;
            List<R1ResultParam> r1Results;
            res = GetCheckResult(ho_Image, StepInfoList, out r1Results);
            if (res != 0)
                return res;
            res = CheckDebug(r1Results);
            if (res != 0)
            {
                r1Results.Clear();
                return res;
            }
            return 0;
        }

        private int GetCheckResult(HObject ho_Image, List<StepInfo> StepInfoList, out List<R1ResultParam> r1Results)
        {
            r1Results = new List<R1ResultParam>();
            if (mHalconDlModel == null)
            {
                mToolParam.ResultString = "模型为空";
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
                mToolParam.ResultString = "模型图片训练通道数与推理图片通道数不一致！";
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

            mToolParam.ResultString = "";
            HObject ho_ImageZoom, ho_ImagePreprocessed;
            HObject ho_Rectangle, ho_RegionAffineTrans;
            HObject ho_Contours;

            // Local control variables 
            HTuple hv_GpuId = new HTuple();
            HTuple hv_ClassIDs = new HTuple();
            HTuple hv_InstanceType = new HTuple(), hv_DLSample = new HTuple();
            HTuple hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DLResult = new HTuple();
            HTuple hv_Bbox_ids = new HTuple(), hv_Bbox_scores = new HTuple();
            HTuple hv_Bbox_row1s = new HTuple(), hv_Bbox_col1s = new HTuple();
            HTuple hv_Bbox_row2s = new HTuple(), hv_Bbox_col2s = new HTuple();
            HTuple hv_HomMat2DIdentity = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple(), hv_Max = new HTuple();
            HTuple hv_IndexID = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_IndexObj = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageZoom);
            HOperatorSet.GenEmptyObj(out ho_ImagePreprocessed);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            try
            {
                //设置置信度阈值
                HOperatorSet.SetDlModelParam(mHalconDlModel, "min_confidence", 0.1);
                //设置单幅图中物体最大数量
                HOperatorSet.SetDlModelParam(mHalconDlModel, "max_num_detections", mToolParam.mMaxDetectNum);
                //设置重叠程度
                HOperatorSet.SetDlModelParam(mHalconDlModel, "max_overlap", mToolParam.mMaxOverlap);
                //设置不同类物体间的重叠程度
                HOperatorSet.SetDlModelParam(mHalconDlModel, "max_overlap_class_agnostic",mToolParam.mMaxOverlapClass);
                hv_ImageDimensions.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "image_dimensions", out hv_ImageDimensions);
                hv_ClassIDs.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                hv_InstanceType.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "instance_type", out hv_InstanceType);

                hv_DLSample.Dispose();
                HOperatorSet.CreateDict(out hv_DLSample);

                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ImageZoom.Dispose();
                    HOperatorSet.ZoomImageSize(ho_Image, out ho_ImageZoom, hv_ImageDimensions.TupleSelect(
                        0), hv_ImageDimensions.TupleSelect(1), "constant");
                }
                ho_ImagePreprocessed.Dispose();
                HOperatorSet.ConvertImageType(ho_ImageZoom, out ho_ImagePreprocessed, "real");
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ScaleImage(ho_ImagePreprocessed, out ExpTmpOutVar_0, 1, -127);
                    ho_ImagePreprocessed.Dispose();
                    ho_ImagePreprocessed = ExpTmpOutVar_0;
                }
                HOperatorSet.SetDictObject(ho_ImagePreprocessed, hv_DLSample, "image");
                hv_DLResult.Dispose();
                HOperatorSet.ApplyDlModel(mHalconDlModel, hv_DLSample, new HTuple(), out hv_DLResult);
                hv_Bbox_ids.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "bbox_class_id", out hv_Bbox_ids);
                hv_Bbox_scores.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "bbox_confidence", out hv_Bbox_scores);

                if ((int)(new HTuple((new HTuple(hv_Bbox_ids.TupleLength())).TupleGreater(0))) != 0)
                {

                    hv_Bbox_row1s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_row1", out hv_Bbox_row1s);
                    hv_Bbox_col1s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_col1", out hv_Bbox_col1s);
                    hv_Bbox_row2s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_row2", out hv_Bbox_row2s);
                    hv_Bbox_col2s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_col2", out hv_Bbox_col2s);
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Bbox_row1s, hv_Bbox_col1s,
                        hv_Bbox_row2s, hv_Bbox_col2s);
                    //求一个单位矩阵
                    hv_HomMat2DIdentity.Dispose();
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                    //求一个旋转矩阵，绕点0，0旋转0度（逆时针）
                    hv_HomMat2DRotate.Dispose();
                    HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, 0, 0, 0, out hv_HomMat2DRotate);
                    //求一个缩放矩阵，以点0，0为基点进行缩放
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_HomMat2DScale.Dispose();
                        HOperatorSet.HomMat2dScale(hv_HomMat2DRotate, hv_Height.D / (hv_ImageDimensions.TupleSelect(1)).D,
                            hv_Width.D / (hv_ImageDimensions.TupleSelect(0)).D, 0, 0, out hv_HomMat2DScale);
                    }

                    ho_RegionAffineTrans.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Rectangle, out ho_RegionAffineTrans, hv_HomMat2DScale,
                        "nearest_neighbor");

                    //搜索编号最高的标签
                    hv_Max.Dispose();
                    HOperatorSet.TupleMax(hv_Bbox_ids, out hv_Max);
                    HTuple end_val55 = hv_Max;
                    HTuple step_val55 = 1;
                    for (hv_IndexID = 0; hv_IndexID.Continue(end_val55, step_val55); hv_IndexID = hv_IndexID.TupleAdd(step_val55))
                    {
                        hv_Indices.Dispose();
                        HOperatorSet.TupleFind(hv_Bbox_ids, hv_IndexID, out hv_Indices);
                        if ((int)(new HTuple(hv_Indices.TupleGreaterEqual(0))) != 0)
                        {
                            for (hv_IndexObj = 0; (int)hv_IndexObj <= (int)((new HTuple(hv_Indices.TupleLength())) - 1); hv_IndexObj = (int)hv_IndexObj + 1)
                            {
                                //区域进行放射
                                HObject region;
                                if (mToolParam.mShapeModelStep > -1)
                                    HOperatorSet.AffineTransRegion(mToolParam.R1ParamList[hv_IndexID.I].ClassRegion, out region, HomMat2D, "nearest_neighbor");
                                else
                                    region = mToolParam.R1ParamList[hv_IndexID.I].ClassRegion.CopyObj(1, 1);
                                HObject ho_ObjectSelected = null;
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.SelectObj(ho_RegionAffineTrans, out ho_ObjectSelected,(hv_Indices.TupleSelect(hv_IndexObj)) + 1);
                                }
                                HObject region1;
                                //求区域的是否有交集
                                HOperatorSet.Intersection(ho_ObjectSelected, region, out region1);
                                ho_ObjectSelected.Dispose();
                                region.Dispose();
                                HTuple area;
                                HOperatorSet.RegionFeatures(region1, "area", out area);
                                if (area > 0) 
                                {
                                    R1ResultParam par = new R1ResultParam();
                                    par.CurrScore = hv_Bbox_scores.TupleSelect(hv_Indices.TupleSelect(hv_IndexObj)).D;
                                    par.CurrClassID = hv_IndexID.I;
                                    par.CurrGetObj = region1;
                                    r1Results.Add(par);
                                }
                            }
                        }
                    }
                }

                HomMat2D.Dispose();
                ho_ImageZoom.Dispose();
                ho_ImagePreprocessed.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Contours.Dispose();

                hv_GpuId.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_InstanceType.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_Bbox_ids.Dispose();
                hv_Bbox_scores.Dispose();
                hv_Bbox_row1s.Dispose();
                hv_Bbox_col1s.Dispose();
                hv_Bbox_row2s.Dispose();
                hv_Bbox_col2s.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_Max.Dispose();
                hv_IndexID.Dispose();
                hv_Indices.Dispose();
                hv_IndexObj.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                HomMat2D.Dispose();
                ho_ImageZoom.Dispose();
                ho_ImagePreprocessed.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Contours.Dispose();

                hv_GpuId.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_InstanceType.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_Bbox_ids.Dispose();
                hv_Bbox_scores.Dispose();
                hv_Bbox_row1s.Dispose();
                hv_Bbox_col1s.Dispose();
                hv_Bbox_row2s.Dispose();
                hv_Bbox_col2s.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_Max.Dispose();
                hv_IndexID.Dispose();
                hv_Indices.Dispose();
                hv_IndexObj.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();

                MessageBox.Show(ex.Message);
                return -1;
            }

        }

        private int GetCheckResultToolRun(HObject ho_Image, List<StepInfo> StepInfoList, out List<R1ResultParam> r1Results)
        {
            r1Results = new List<R1ResultParam>();
            if (mHalconDlModel == null)
            {
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

            HObject ho_ImageZoom, ho_ImagePreprocessed;
            HObject ho_Rectangle, ho_RegionAffineTrans;
            HObject ho_Contours;

            // Local control variables 
            HTuple hv_GpuId = new HTuple();
            HTuple hv_ClassIDs = new HTuple();
            HTuple hv_InstanceType = new HTuple(), hv_DLSample = new HTuple();
            HTuple hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_DLResult = new HTuple();
            HTuple hv_Bbox_ids = new HTuple(), hv_Bbox_scores = new HTuple();
            HTuple hv_Bbox_row1s = new HTuple(), hv_Bbox_col1s = new HTuple();
            HTuple hv_Bbox_row2s = new HTuple(), hv_Bbox_col2s = new HTuple();
            HTuple hv_HomMat2DIdentity = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_HomMat2DScale = new HTuple(), hv_Max = new HTuple();
            HTuple hv_IndexID = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_IndexObj = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageZoom);
            HOperatorSet.GenEmptyObj(out ho_ImagePreprocessed);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            try
            {
                //设置置信度阈值
                HOperatorSet.SetDlModelParam(mHalconDlModel, "min_confidence", 0.1);
                //设置单幅图中物体最大数量
                HOperatorSet.SetDlModelParam(mHalconDlModel, "max_num_detections", mToolParam.mMaxDetectNum);
                //设置重叠程度
                HOperatorSet.SetDlModelParam(mHalconDlModel, "max_overlap", mToolParam.mMaxOverlap);
                //设置不同类物体间的重叠程度
                HOperatorSet.SetDlModelParam(mHalconDlModel, "max_overlap_class_agnostic", mToolParam.mMaxOverlapClass);
                hv_ImageDimensions.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "image_dimensions", out hv_ImageDimensions);
                hv_ClassIDs.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                hv_InstanceType.Dispose();
                HOperatorSet.GetDlModelParam(mHalconDlModel, "instance_type", out hv_InstanceType);

                hv_DLSample.Dispose();
                HOperatorSet.CreateDict(out hv_DLSample);

                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ImageZoom.Dispose();
                    HOperatorSet.ZoomImageSize(ho_Image, out ho_ImageZoom, hv_ImageDimensions.TupleSelect(
                        0), hv_ImageDimensions.TupleSelect(1), "constant");
                }
                ho_ImagePreprocessed.Dispose();
                HOperatorSet.ConvertImageType(ho_ImageZoom, out ho_ImagePreprocessed, "real");
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.ScaleImage(ho_ImagePreprocessed, out ExpTmpOutVar_0, 1, -127);
                    ho_ImagePreprocessed.Dispose();
                    ho_ImagePreprocessed = ExpTmpOutVar_0;
                }
                HOperatorSet.SetDictObject(ho_ImagePreprocessed, hv_DLSample, "image");
                hv_DLResult.Dispose();
                HOperatorSet.ApplyDlModel(mHalconDlModel, hv_DLSample, new HTuple(), out hv_DLResult);
                hv_Bbox_ids.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "bbox_class_id", out hv_Bbox_ids);
                hv_Bbox_scores.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "bbox_confidence", out hv_Bbox_scores);

                if ((int)(new HTuple((new HTuple(hv_Bbox_ids.TupleLength())).TupleGreater(0))) != 0)
                {

                    hv_Bbox_row1s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_row1", out hv_Bbox_row1s);
                    hv_Bbox_col1s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_col1", out hv_Bbox_col1s);
                    hv_Bbox_row2s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_row2", out hv_Bbox_row2s);
                    hv_Bbox_col2s.Dispose();
                    HOperatorSet.GetDictTuple(hv_DLResult, "bbox_col2", out hv_Bbox_col2s);
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Bbox_row1s, hv_Bbox_col1s,
                        hv_Bbox_row2s, hv_Bbox_col2s);
                    //求一个单位矩阵
                    hv_HomMat2DIdentity.Dispose();
                    HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                    //求一个旋转矩阵，绕点0，0旋转0度（逆时针）
                    hv_HomMat2DRotate.Dispose();
                    HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, 0, 0, 0, out hv_HomMat2DRotate);
                    //求一个缩放矩阵，以点0，0为基点进行缩放
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_HomMat2DScale.Dispose();
                        HOperatorSet.HomMat2dScale(hv_HomMat2DRotate, hv_Height.D / (hv_ImageDimensions.TupleSelect(1)).D,
                            hv_Width.D / (hv_ImageDimensions.TupleSelect(0)).D, 0, 0, out hv_HomMat2DScale);
                    }

                    ho_RegionAffineTrans.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Rectangle, out ho_RegionAffineTrans, hv_HomMat2DScale,
                        "nearest_neighbor");

                    //搜索编号最高的标签
                    hv_Max.Dispose();
                    HOperatorSet.TupleMax(hv_Bbox_ids, out hv_Max);
                    HTuple end_val55 = hv_Max;
                    HTuple step_val55 = 1;
                    for (hv_IndexID = 0; hv_IndexID.Continue(end_val55, step_val55); hv_IndexID = hv_IndexID.TupleAdd(step_val55))
                    {
                        hv_Indices.Dispose();
                        HOperatorSet.TupleFind(hv_Bbox_ids, hv_IndexID, out hv_Indices);
                        if ((int)(new HTuple(hv_Indices.TupleGreaterEqual(0))) != 0)
                        {
                            for (hv_IndexObj = 0; (int)hv_IndexObj <= (int)((new HTuple(hv_Indices.TupleLength())) - 1); hv_IndexObj = (int)hv_IndexObj + 1)
                            {
                                //区域进行放射
                                HObject region;
                                if (mToolParam.mShapeModelStep > -1)
                                    HOperatorSet.AffineTransRegion(mToolParam.R1ParamList[hv_IndexID.I].ClassRegion, out region, HomMat2D, "nearest_neighbor");
                                else
                                    region = mToolParam.R1ParamList[hv_IndexID.I].ClassRegion.CopyObj(1, 1);
                                HObject ho_ObjectSelected = null;
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.SelectObj(ho_RegionAffineTrans, out ho_ObjectSelected, (hv_Indices.TupleSelect(hv_IndexObj)) + 1);
                                }
                                HObject region1;
                                //求区域的是否有交集
                                HOperatorSet.Intersection(ho_ObjectSelected, region, out region1);
                                ho_ObjectSelected.Dispose();
                                region.Dispose();
                                HTuple area;
                                HOperatorSet.RegionFeatures(region1, "area", out area);
                                if (area > 0)
                                {
                                    R1ResultParam par = new R1ResultParam();
                                    par.CurrScore = hv_Bbox_scores.TupleSelect(hv_Indices.TupleSelect(hv_IndexObj)).D;
                                    par.CurrClassID = hv_IndexID.I;
                                    par.CurrGetObj = region1;
                                    r1Results.Add(par);
                                }
                            }
                        }
                    }
                }

                HomMat2D.Dispose();
                ho_ImageZoom.Dispose();
                ho_ImagePreprocessed.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Contours.Dispose();

                hv_GpuId.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_InstanceType.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_Bbox_ids.Dispose();
                hv_Bbox_scores.Dispose();
                hv_Bbox_row1s.Dispose();
                hv_Bbox_col1s.Dispose();
                hv_Bbox_row2s.Dispose();
                hv_Bbox_col2s.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_Max.Dispose();
                hv_IndexID.Dispose();
                hv_Indices.Dispose();
                hv_IndexObj.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                HomMat2D.Dispose();
                ho_ImageZoom.Dispose();
                ho_ImagePreprocessed.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Contours.Dispose();

                hv_GpuId.Dispose();
                hv_ImageDimensions.Dispose();
                hv_ClassIDs.Dispose();
                hv_InstanceType.Dispose();
                hv_DLSample.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_DLResult.Dispose();
                hv_Bbox_ids.Dispose();
                hv_Bbox_scores.Dispose();
                hv_Bbox_row1s.Dispose();
                hv_Bbox_col1s.Dispose();
                hv_Bbox_row2s.Dispose();
                hv_Bbox_col2s.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_HomMat2DScale.Dispose();
                hv_Max.Dispose();
                hv_IndexID.Dispose();
                hv_Indices.Dispose();
                hv_IndexObj.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();
                LogHelper.WriteExceptionLog(ex);
                return -1;
            }

        }

        public int CheckDebug(List<R1ResultParam> r1Results) 
        {
            if (r1Results.Count == 0)
                return 0;
            int res = 0;
            mToolParam.ResultString = "";
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("margin");
            mDrawWind.SetFont("Consolas-24");

            for (int i = 0; i < r1Results.Count; i++)
            {
                HTuple features = new HTuple();
                features[0] = "area";
                features[1] = "height";
                features[2] = "width";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.R1ParamList[r1Results[i].CurrClassID].AreaSingleMin;
                minValue[1] = mToolParam.R1ParamList[r1Results[i].CurrClassID].HeightMin;
                minValue[2] = mToolParam.R1ParamList[r1Results[i].CurrClassID].WidthMin;
                HTuple maxValue = new HTuple();
                maxValue[0] = mToolParam.R1ParamList[r1Results[i].CurrClassID].AreaSingleMax;
                maxValue[1] = mToolParam.R1ParamList[r1Results[i].CurrClassID].HeightMax;
                maxValue[2] = mToolParam.R1ParamList[r1Results[i].CurrClassID].WidthMax;

                //先筛选分数
                if (r1Results[i].CurrScore > mToolParam.R1ParamList[r1Results[i].CurrClassID].Score) 
                {
                    HObject obj = r1Results[i].CurrGetObj;
                    HObject obj1;
                    HOperatorSet.Connection(obj, out obj1);
                    HObject obj2;
                    HOperatorSet.SelectShape(obj1, out obj2, features, "and", minValue, maxValue);
                    HTuple a1, r1, c1;
                    HOperatorSet.AreaCenter(obj2, out a1, out r1, out c1);
                    if (a1.Length > 0) 
                    {
                        res = 1;
                        HTuple area, height, width;
                        HOperatorSet.RegionFeatures(obj2,"area",out area);
                        HOperatorSet.RegionFeatures(obj2, "height", out height);
                        HOperatorSet.RegionFeatures(obj2, "width", out width);
                        mDrawWind.SetColor(ShowColors.Colors[r1Results[i].CurrClassID]);
                        mDrawWind.SetLineWidth(2);
                        mDrawWind.DispObj(obj2);
                        mDrawWind.DispObj(mToolParam.R1ParamList[r1Results[i].CurrClassID].ClassRegion);
                        int r = Convert.ToInt32(r1.D);
                        int c = Convert.ToInt32(c1.D);
                        mDrawWind.SetFont("Consolas-12");
                        mDrawWind.SetTposition(r, c);
                        //mDrawWind.WriteString( r1Results[i].CurrScore.ToString("f3"));
                        mDrawWind.DispText(r1Results[i].CurrScore.ToString("f3"), "image", r - 50, c, "red", new HTuple(), new HTuple());
                        mToolParam.ResultString += i + "# " + mToolParam.R1ParamList[r1Results[i].CurrClassID].ClassName +
                            "缺陷， 分数为:=" + r1Results[i].CurrScore.ToString("f3") + "  ,area:= " + area + " ,height:=" + height + " ,width:=" + width + "\r\n";
                    }
                }
            }

            return res;
        }
        public int CheckDebugToolRun(List<R1ResultParam> r1Results,out string ss)
        {
            ss = "";
            if (r1Results.Count == 0)
                return 0;
            int res = 0;
            for (int i = 0; i < r1Results.Count; i++)
            {
                HTuple features = new HTuple();
                features[0] = "area";
                features[1] = "height";
                features[2] = "width";
                HTuple minValue = new HTuple();
                minValue[0] = mToolParam.R1ParamList[r1Results[i].CurrClassID].AreaSingleMin;
                minValue[1] = mToolParam.R1ParamList[r1Results[i].CurrClassID].HeightMin;
                minValue[2] = mToolParam.R1ParamList[r1Results[i].CurrClassID].WidthMin;
                HTuple maxValue = new HTuple();
                maxValue[0] = mToolParam.R1ParamList[r1Results[i].CurrClassID].AreaSingleMax;
                maxValue[1] = mToolParam.R1ParamList[r1Results[i].CurrClassID].HeightMax;
                maxValue[2] = mToolParam.R1ParamList[r1Results[i].CurrClassID].WidthMax;

                //先筛选分数
                if (r1Results[i].CurrScore > mToolParam.R1ParamList[r1Results[i].CurrClassID].Score)
                {
                    HObject obj = r1Results[i].CurrGetObj;
                    HObject obj1;
                    HOperatorSet.Connection(obj, out obj1);
                    HObject obj2;
                    HOperatorSet.SelectShape(obj1, out obj2, features, "and", minValue, maxValue);
                    HTuple a1, r1, c1;
                    HOperatorSet.AreaCenter(obj2, out a1, out r1, out c1);
                    if (a1.Length > 0)
                    {
                        ss = mToolParam.R1ParamList[r1Results[i].CurrClassID].ClassName;
                        return mToolParam.NgReturnValue;
                    }

                }
            }
            return res;
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
                                HOperatorSet.Union2(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.R1ParamList[mIndex].ClassRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.R1ParamList[mIndex].ClassRegion = r1;
                            }
                            else
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Difference(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.R1ParamList[mIndex].ClassRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.R1ParamList[mIndex].ClassRegion = r1;
                            }
                        }
                        mDrawWind.SetRgba(255, 0, 0, 90);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.R1ParamList[mIndex].ClassRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 90);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.R1ParamList[mIndex].ClassRegion);
                showRegion = mToolParam.R1ParamList[mIndex].ClassRegion;
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
            mDrawWind.DispObj(mToolParam.R1ParamList[mIndex].ClassRegion);
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
                    HOperatorSet.Union2(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.R1ParamList[mIndex].ClassRegion = new HRegion(temp);
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
                    HOperatorSet.Union2(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.R1ParamList[mIndex].ClassRegion = new HRegion(temp);
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
                    HOperatorSet.Union2(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.R1ParamList[mIndex].ClassRegion = new HRegion(temp);
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
                    HOperatorSet.Union2(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.R1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.R1ParamList[mIndex].ClassRegion, obj, out temp);
                    mToolParam.R1ParamList[mIndex].ClassRegion = new HRegion(temp);
                }
                
                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.R1ParamList[mIndex].ClassRegion);
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
            mToolParam.R1ParamList[mIndex].ClassRegion.Dispose();
            mToolParam.R1ParamList[mIndex].ClassRegion.GenEmptyObj();
            mDrawWind.ClearWindow();
            return 0;
        }


    }
}
