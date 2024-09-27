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
    public class ToolCorrectImageParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mRegionStep;//区域源
        public int mRegionMark;

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

        public int ImageSourceStep
        {
            get => mImageSourceStep;
            set => mImageSourceStep = value;
        }

        public delegate int ParamChangedDe(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj);
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;

        public ToolCorrectImageParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.CorrectImage;
            mToolType = ToolType.CorrectImage;
            mStepInfo.mToolResultType = ToolResultType.Image;
            mToolResultType = ToolResultType.Image;


            ImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;
            mRegionStep = -1;
            mRegionMark = -1;

            mShowName = "图像矫正";
            mToolName = "图像矫正";
            mStepInfo.mShowName= "图像矫正";      
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolCorrectImage : ToolBase
    {
        ToolCorrectImageParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;



        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolCorrectImageParam;
        }

        public ToolCorrectImage(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolCorrectImageParam;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            HObject ob;
            return CorrectImage(objj1, StepInfoList, out ob);
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj)
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

        public override int ToolRun(HObject obj,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            mToolParam.ResultString = "";
            try
            {
                HObject objFinal;
                if (mToolParam.ImageSourceStep < 0)
                {
                    objFinal = obj;
                }
                else
                {
                    objFinal = StepInfoList[mToolParam.ImageSourceStep - 1].mToolRunResul.mImageOutPut;
                }

                if (mToolParam.mShapeModelStep > -1)
                {
                    //仿射区域
                    HTuple HomMat2D = new HTuple();
                    HOperatorSet.VectorAngleToRigid(
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                    0,
                    out HomMat2D);

                    HOperatorSet.AffineTransImage(objFinal, out HObject imageAffine, HomMat2D, "constant", "false");
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = imageAffine;
                    //mDrawWind.DispObj(imageAffine);
                    HomMat2D.Dispose();
                    return 0;
                }
                else
                {
                    LogHelper.WriteExceptionLog("图像矫正未输入定位步骤！");
                    return mToolParam.NgReturnValue;
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
                return mToolParam.NgReturnValue;
            }
        }

        public int CorrectImage(HObject obj, List<StepInfo> StepInfoList, out HObject objShow)
        {
            mToolParam.ResultString = "";
            HOperatorSet.GenEmptyObj(out objShow);
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            try
            {
                HObject objFinal;
                if (mToolParam.ImageSourceStep < 0)
                {
                    objFinal = obj;
                }
                else
                {
                    objFinal = StepInfoList[mToolParam.ImageSourceStep - 1].mToolRunResul.mImageOutPut;
                }

                if (mToolParam.mShapeModelStep > -1)
                {                
                    //仿射区域
                    HTuple HomMat2D = new HTuple();
                    HOperatorSet.VectorAngleToRigid(
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                    0,
                    out HomMat2D);

                    HOperatorSet.AffineTransImage(objFinal, out HObject imageAffine, HomMat2D, "constant", "false");
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = imageAffine;


                    HOperatorSet.CountSeconds(out s2);
                    mToolParam.ResultString = "耗时：" + ((s2.D - s1.D) * 1000).ToString("f2") + "ms" + "\r\n";
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(imageAffine);
                    return 0;
                }
                else
                {
                    mToolParam.ResultString = "未输入定位步骤！";
                    mDrawWind.ClearWindow();
                    return 1;
                }
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
            }
            else
            {
                mToolParam.mParamChangedDe = null;
            }

            return ResStatus.OK;
        }

    }
}
