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
    public class ToolCropImageParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mRegionStep;//区域源
        public int mRegionMark;

        public double mCenterRow;
        public double mCenterColumn;
        public double mLengthWidth;
        public double mLengthHeight;

        public bool mIsSaveImage;
        public string mSaveFold;

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

        public delegate int ParamChangedDe(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj);
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;

        public ToolCropImageParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.CropImage;
            mToolType = ToolType.CropImage;
            mStepInfo.mToolResultType = ToolResultType.Image;
            mToolResultType = ToolResultType.Image;

            ImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;
            mRegionStep = -1;
            mRegionMark = -1;

            mCenterRow = 0;
            mCenterColumn = 0;
            mLengthWidth = 200;
            mLengthHeight = 100;

            mIsSaveImage = false;
            mSaveFold = "";

            mShowName = "裁剪图片";
            mToolName = "裁剪图片";
            mStepInfo.mShowName= "裁剪图片";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolCropImage : ToolBase
    {
        ToolCropImageParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;



        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolCropImageParam;
        }

        public ToolCropImage(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolCropImageParam;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            try
            {
                //判断图像输入源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = objj1;

                //判断是否有模板输入源，若有输入源，则加上模板的偏移差值
                double offsRow = 0;
                double offsColumn = 0;
                if (mToolParam.mShapeModelStep > -1)
                {
                    //偏移Row
                    offsRow = StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2] -
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0];
                    //偏移Col
                    offsColumn = StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3] -
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1];
                }
                HObject obj1, obj2, obj3;
                HOperatorSet.GenRectangle2(out obj1, mToolParam.mCenterRow + offsRow, mToolParam.mCenterColumn + offsColumn, 0, mToolParam.mLengthWidth, mToolParam.mLengthHeight);
                HOperatorSet.ReduceDomain(imageFinal, obj1, out obj2);
                HOperatorSet.CropDomain(obj2, out obj3);
                mToolParam.StepInfo.mToolRunResul.mImageOutPut = obj3.CopyObj(1, 1);
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mCenterRow + offsRow - mToolParam.mLengthHeight;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mCenterColumn + offsColumn - mToolParam.mLengthWidth;

                if (mToolParam.mIsSaveImage)
                {
                    if (Directory.Exists(mToolParam.mSaveFold) && mToolParam.mSaveFold != null) 
                    {
                        string name = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        HOperatorSet.WriteImage(obj3, "png", 0, mToolParam.mSaveFold + "//"+ name);
                    }
                }

                mDrawWind.SetDraw("margin");
                mDrawWind.SetColor("magenta");
                mDrawWind.SetLineWidth(2);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(objj1);
                mDrawWind.DispObj(obj1);
                obj1.Dispose();
                obj2.Dispose();
                obj3.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return 1;
            }
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

        public override int ToolRun(HObject obj, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            try
            {
                //判断图像输入源
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = obj;

                //判断是否有模板输入源，若有输入源，则加上模板的偏移差值
                double offsRow = 0;
                double offsColumn = 0;
                if (mToolParam.mShapeModelStep > -1)
                {
                    //偏移Row
                    offsRow = StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2] -
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0];
                    //偏移Col
                    offsColumn = StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3] -
                        StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1];
                }
                HObject obj1, obj2, obj3;
                HOperatorSet.GenRectangle2(out obj1, mToolParam.mCenterRow + offsRow, mToolParam.mCenterColumn + offsColumn, 0, mToolParam.mLengthWidth, mToolParam.mLengthHeight);
                HOperatorSet.ReduceDomain(imageFinal, obj1, out obj2);
                HOperatorSet.CropDomain(obj2, out obj3);
                mToolParam.StepInfo.mToolRunResul.mImageOutPut = obj3.CopyObj(1, 1);
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mCenterRow + offsRow - mToolParam.mLengthHeight;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mCenterColumn + offsColumn - mToolParam.mLengthWidth;
                obj1.Dispose();
                obj2.Dispose();
                obj3.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
                return 1;
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
