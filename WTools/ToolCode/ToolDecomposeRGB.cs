using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WTools
{
    [Serializable]
    public class ToolDecomposeRGBParam : ToolParamBase
    {
        public string mMode;
        public int mSelectMode;
        public int mMult;
        public int mAdd;

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


        public delegate int ParamChangedDe(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj);
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;

        public ToolDecomposeRGBParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.DecomposeRGB;

            mMode = "hsv";
            mSelectMode = 0;

            mShowName = "通道分解";
            mToolName = "通道分解";
            mStepInfo.mShowName= "通道分解";
            mToolType = ToolType.DecomposeRGB;
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
            mMult = 1;
            mAdd = 128;
        }
    }

    public class ToolDecomposeRGB : ToolBase
    {
        ToolDecomposeRGBParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolDecomposeRGBParam;
        }

        public ToolDecomposeRGB(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolDecomposeRGBParam;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();         
            return Check(objj1);
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

        public override int ToolRun(HObject obj, Bitmap obj222, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            HTuple mChannel;
            HOperatorSet.CountChannels(obj, out mChannel);
            if (mChannel.I != 3)
            {
                mToolParam.ResultString = "图片不是三通道";
                return -1;
            }

            HObject finalRegion;
            HOperatorSet.GenEmptyObj(out finalRegion);
            try
            {
                if (mToolParam.mSelectMode == 0)
                {
                    HOperatorSet.Rgb1ToGray(obj, out finalRegion);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                }
                else if (mToolParam.mSelectMode == 1)
                {
                    HObject obj1, obj2;
                    HOperatorSet.Decompose3(obj, out finalRegion, out obj1, out obj2);
                    obj1.Dispose();
                    obj2.Dispose();
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                }
                else if (mToolParam.mSelectMode == 2)
                {
                    HObject obj1, obj2;
                    HOperatorSet.Decompose3(obj, out obj1, out finalRegion, out obj2);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                }
                else if (mToolParam.mSelectMode == 3)
                {
                    HObject obj1, obj2;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out finalRegion);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                }
                else if (mToolParam.mSelectMode == 4)
                {
                    HObject obj1, obj2, obj3, obj4, obj5;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out finalRegion, out obj4, out obj5, mToolParam.mMode);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                }
                else if (mToolParam.mSelectMode == 5)
                {
                    HObject obj1, obj2, obj3, obj4, obj5;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out finalRegion, out obj5, mToolParam.mMode);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                }
                else if (mToolParam.mSelectMode == 6)
                {
                    HObject obj1, obj2, obj3, obj4, obj5;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out finalRegion, mToolParam.mMode);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                }
                else if (mToolParam.mSelectMode == 7)
                {
                    HObject obj1, obj2, obj3;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.SubImage(obj1, obj2, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                else if (mToolParam.mSelectMode == 8)
                {
                    HObject obj1, obj2, obj3;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.SubImage(obj1, obj3, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                else if (mToolParam.mSelectMode == 9)
                {
                    HObject obj1, obj2, obj3;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.SubImage(obj2, obj3, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                else if (mToolParam.mSelectMode == 10)
                {
                    HObject obj1, obj2, obj3, obj4, obj5, obj6;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out obj6, mToolParam.mMode);
                    HOperatorSet.SubImage(obj4, obj5, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                    obj6.Dispose();
                }
                else if (mToolParam.mSelectMode == 11)
                {
                    HObject obj1, obj2, obj3, obj4, obj5, obj6;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out obj6, mToolParam.mMode);
                    HOperatorSet.SubImage(obj4, obj6, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                    obj6.Dispose();
                }
                else if (mToolParam.mSelectMode == 12)
                {
                    HObject obj1, obj2, obj3, obj4, obj5, obj6;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out obj6, mToolParam.mMode);
                    HOperatorSet.SubImage(obj5, obj6, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                    obj6.Dispose();
                }
                return 0;
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
                mToolParam.mParamChangedDe +=ParamChanged;
            }
            else
            {
                mToolParam.mParamChangedDe = null;
            }

            return ResStatus.OK;
        }

        private int Check(HObject obj)
        {
            mToolParam.ResultString = "";
            if (mToolParam.ForceOK)
                return 0;
            HTuple mChannel;
            HOperatorSet.CountChannels(obj, out mChannel);
            if (mChannel.I != 3) 
            {
                mToolParam.ResultString = "图片不是三通道";
                return -1;
            }

            HTuple S1, S2;
            HOperatorSet.CountSeconds(out S1);
            HObject finalRegion;
            HOperatorSet.GenEmptyObj(out finalRegion);
            try
            {
                if (mToolParam.mSelectMode == 0) 
                {
                    HOperatorSet.Rgb1ToGray(obj, out finalRegion);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                }
                else if (mToolParam.mSelectMode == 1)
                {
                    HObject obj1, obj2;
                    HOperatorSet.Decompose3(obj, out finalRegion, out obj1, out obj2);
                    obj1.Dispose();
                    obj2.Dispose();
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                }
                else if (mToolParam.mSelectMode == 2)
                {
                    HObject obj1, obj2;
                    HOperatorSet.Decompose3(obj, out obj1, out finalRegion, out obj2);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                }
                else if (mToolParam.mSelectMode == 3)
                {
                    HObject obj1, obj2;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out finalRegion);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                }
                else if (mToolParam.mSelectMode == 4)
                {
                    HObject obj1, obj2, obj3, obj4, obj5;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out finalRegion, out obj4, out obj5, mToolParam.mMode);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                }
                else if (mToolParam.mSelectMode == 5)
                {
                    HObject obj1, obj2, obj3, obj4, obj5;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out finalRegion, out obj5, mToolParam.mMode);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                }
                else if (mToolParam.mSelectMode == 6)
                {
                    HObject obj1, obj2, obj3, obj4, obj5;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out finalRegion, mToolParam.mMode);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                }
                else if (mToolParam.mSelectMode == 7)
                {
                    HObject obj1, obj2, obj3;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.SubImage(obj1, obj2, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                else if (mToolParam.mSelectMode == 8)
                {
                    HObject obj1, obj2, obj3;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.SubImage(obj1, obj3, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                else if (mToolParam.mSelectMode == 9)
                {
                    HObject obj1, obj2, obj3;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.SubImage(obj2, obj3, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                }
                else if (mToolParam.mSelectMode == 10)
                {
                    HObject obj1, obj2, obj3, obj4, obj5, obj6;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out obj6, mToolParam.mMode);
                    HOperatorSet.SubImage(obj4, obj5, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                    obj6.Dispose();
                }
                else if (mToolParam.mSelectMode == 11)
                {
                    HObject obj1, obj2, obj3, obj4, obj5, obj6;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out obj6, mToolParam.mMode);
                    HOperatorSet.SubImage(obj4, obj6, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                    obj6.Dispose();
                }
                else if (mToolParam.mSelectMode == 12)
                {
                    HObject obj1, obj2, obj3, obj4, obj5, obj6;
                    HOperatorSet.Decompose3(obj, out obj1, out obj2, out obj3);
                    HOperatorSet.TransFromRgb(obj1, obj2, obj3, out obj4, out obj5, out obj6, mToolParam.mMode);
                    HOperatorSet.SubImage(obj5, obj6, out finalRegion, mToolParam.mMult, mToolParam.mAdd);
                    mToolParam.StepInfo.mToolRunResul.mImageOutPut = finalRegion;
                    obj1.Dispose();
                    obj2.Dispose();
                    obj3.Dispose();
                    obj4.Dispose();
                    obj5.Dispose();
                    obj6.Dispose();
                }
                HOperatorSet.CountSeconds(out S2);
                mToolParam.ResultString = "耗时：" + ((S2.D - S1.D) * 1000).ToString("f2") + "ms";
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(finalRegion);
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }
    }
}
