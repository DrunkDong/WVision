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
    public class ToolHalconClassifyParam : ToolParamBase
    {
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;

        public string mAiModelPath;
        public int mBatchSize;
        public int mGpuID;

        public List<string> mClassifyNameList;
        public string mResultClassName;

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
        public delegate int ParamChangedDe(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj);
        public delegate int CheckAiDel(HObject obj1, List<StepInfo> StepInfoList);

        [NonSerialized]
        public InitAiModelDe mInitAiModelDe;
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public CheckAiDel mCheckAiDel;

        public ToolHalconClassifyParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.HClassifiyAI;
            mToolType = ToolType.HClassifiyAI;
            mStepInfo.mToolResultType = ToolResultType.None;
            mToolResultType = ToolResultType.None;

            mImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;

            mBatchSize = 1;
            mGpuID = 0;
            mAiModelPath = "";
            mClassifyNameList = new List<string>();

            mShowName = "分类器";
            mToolName = "分类器";
            mStepInfo.mShowName = "分类器";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolHalconClassify : ToolBase
    {
        ToolHalconClassifyParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;
        HTuple mHalconDlModel;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolHalconClassifyParam;
        }

        public ToolHalconClassify(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolHalconClassifyParam;
            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
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
            return DebugRun(obj1,StepInfoList, false, out StepJumpInfo);
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

        public override int ToolRun(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mHalconDlModel == null)
            {
                LogHelper.WriteExceptionLog("未初始化AI");
                return mToolParam.NgReturnValue;
            }
            int res;
            res = GetCheckResultToolRun(obj1, StepInfoList);
            if (res != 0)
                return res;
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

                HTuple hv_ClassIDs, class_names;
                HOperatorSet.ReadDlModel(mToolParam.mAiModelPath, out mHalconDlModel);
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_names", out class_names);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size", mToolParam.mBatchSize);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime", "gpu");
                HOperatorSet.SetDlModelParam(mHalconDlModel, "gpu", mToolParam.mGpuID);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime_init", "immediately");


                long[] ids = hv_ClassIDs.ToLArr();
                if (mToolParam.mClassifyNameList.Count != ids.Count())
                {
                    mToolParam.mClassifyNameList.Clear();
                    for (int i = 0; i < ids.Count(); i++)
                    {
                        //添加标签
                        mToolParam.mClassifyNameList.Add(class_names[i]);
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
                HTuple hv_ClassIDs, class_names;
                HOperatorSet.ReadDlModel(mToolParam.mAiModelPath, out mHalconDlModel);
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_ids", out hv_ClassIDs);
                HOperatorSet.GetDlModelParam(mHalconDlModel, "class_names", out class_names);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "batch_size", mToolParam.mBatchSize);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime", "gpu");
                HOperatorSet.SetDlModelParam(mHalconDlModel, "gpu", mToolParam.mGpuID);
                HOperatorSet.SetDlModelParam(mHalconDlModel, "runtime_init", "immediately");
                long[] ids = hv_ClassIDs.ToLArr();
                mToolParam.mClassifyNameList.Clear();
                for (int i = 0; i < ids.Count(); i++)
                {
                    //添加标签
                    mToolParam.mClassifyNameList.Add(class_names[i]);
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
            }
            else
            {
                mToolParam.mInitAiModelDe = null;
                mToolParam.mParamChangedDe = null;
                mToolParam.mCheckAiDel = null;
            }

            return ResStatus.OK;
        }

        private int Check(HObject ho_Image, List<StepInfo> StepInfoList)
        {
            int res;
            res = GetCheckResult(ho_Image, StepInfoList);
            if (res != 0)
                return res;
            return 0;
        }

        private int GetCheckResult(HObject ho_Image, List<StepInfo> StepInfoList)
        {
            mToolParam.ResultString = "";
            if (mHalconDlModel == null)
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


            HObject ho_ImagePreprocessed;
            HTuple hv_DLSample = new HTuple();
            HTuple hv_DLResult = new HTuple();
            HTuple hv_Confidences = new HTuple();
            HTuple hv_PredictClasses = new HTuple();
            HTuple hv_Classids = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_ImagePreprocessed);
            try
            {
                HOperatorSet.ZoomImageSize(ho_Image, out ho_ImagePreprocessed, hv_ImageDimensions.TupleSelect(0), hv_ImageDimensions.TupleSelect(1), "constant");
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
                hv_DLSample.Dispose();
                HOperatorSet.CreateDict(out hv_DLSample);
                HOperatorSet.SetDictObject(ho_ImagePreprocessed, hv_DLSample, "image");
                hv_DLResult.Dispose();
                HOperatorSet.ApplyDlModel(mHalconDlModel, hv_DLSample, new HTuple(), out hv_DLResult);
                hv_Confidences.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "classification_confidences", out hv_Confidences);
                hv_PredictClasses.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "classification_class_names", out hv_PredictClasses);
                hv_Classids.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "classification_class_ids", out hv_Classids);

                string name = hv_PredictClasses.SArr[0].ToString();
                int indx = hv_Classids.TupleSelect(0).I;
                if (name != mToolParam.mResultClassName) 
                {
                    mDrawWind.DispObj(imageFinal);
                    mDrawWind.SetFont("Consolas-20");
                    mDrawWind.DispText("判定结果为：" + name, "image", 0, 0, "red", new HTuple("box"), new HTuple("false"));
                    ho_ImagePreprocessed.Dispose();
                    hv_DLSample.Dispose();
                    hv_DLResult.Dispose();
                    hv_Confidences.Dispose();
                    hv_PredictClasses.Dispose();
                    hv_Classids.Dispose();
                    return 1;
                }

                mDrawWind.DispObj(imageFinal);
                mDrawWind.SetFont("Consolas-20");
                mDrawWind.DispText("判定结果为：" + name, "image", 0, 0, "green", new HTuple("box"), new HTuple("false"));
                ho_ImagePreprocessed.Dispose();
                hv_DLSample.Dispose();
                hv_DLResult.Dispose();
                hv_Confidences.Dispose();
                hv_PredictClasses.Dispose();
                hv_Classids.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog("分类模型: 推理失败！" + ex);
                ho_ImagePreprocessed.Dispose();
                hv_DLSample.Dispose();
                hv_DLResult.Dispose();
                hv_Confidences.Dispose();
                hv_PredictClasses.Dispose();
                hv_Classids.Dispose();
                return -1;
            }
        }

        private int GetCheckResultToolRun(HObject ho_Image, List<StepInfo> StepInfoList)
        {
            mToolParam.ResultString = "";
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

            HObject ho_ImagePreprocessed;
            HTuple hv_DLSample = new HTuple();
            HTuple hv_DLResult = new HTuple();
            HTuple hv_Confidences = new HTuple();
            HTuple hv_PredictClasses = new HTuple();
            HTuple hv_Classids = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_ImagePreprocessed);
            try
            {
                HOperatorSet.ZoomImageSize(ho_Image, out ho_ImagePreprocessed, hv_ImageDimensions.TupleSelect(0), hv_ImageDimensions.TupleSelect(1), "constant");
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
                hv_DLSample.Dispose();
                HOperatorSet.CreateDict(out hv_DLSample);
                HOperatorSet.SetDictObject(ho_ImagePreprocessed, hv_DLSample, "image");
                hv_DLResult.Dispose();
                HOperatorSet.ApplyDlModel(mHalconDlModel, hv_DLSample, new HTuple(), out hv_DLResult);
                hv_Confidences.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "classification_confidences", out hv_Confidences);
                hv_PredictClasses.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "classification_class_names", out hv_PredictClasses);
                hv_Classids.Dispose();
                HOperatorSet.GetDictTuple(hv_DLResult, "classification_class_ids", out hv_Classids);

                string name = hv_PredictClasses.SArr[0].ToString();
                int indx = hv_Classids.TupleSelect(0).I;
                if (name != mToolParam.mResultClassName)
                {
                    ho_ImagePreprocessed.Dispose();
                    hv_DLSample.Dispose();
                    hv_DLResult.Dispose();
                    hv_Confidences.Dispose();
                    hv_PredictClasses.Dispose();
                    hv_Classids.Dispose();
                    return 1;
                }
                ho_ImagePreprocessed.Dispose();
                hv_DLSample.Dispose();
                hv_DLResult.Dispose();
                hv_Confidences.Dispose();
                hv_PredictClasses.Dispose();
                hv_Classids.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog("分类模型: 推理失败！" + ex);
                ho_ImagePreprocessed.Dispose();
                hv_DLSample.Dispose();
                hv_DLResult.Dispose();
                hv_Confidences.Dispose();
                hv_PredictClasses.Dispose();
                hv_Classids.Dispose();
                return -1;
            }
        }

    }
}
