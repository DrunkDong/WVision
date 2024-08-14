using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCommonTools;
using HalconDotNet;

namespace WTools
{
    [Serializable]
    public class ToolCompareDistanceParam : ToolParamBase
    {
        public int mLine1StepIndex;
        public int mLine2StepIndex;
        public int mLine1StepMark;
        public int mLine2StepMark;
        public int mCalibrationStepIndex;
        public int mSelectMinValue;
        public int mSelectMaxValue;

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
        public ToolCompareDistanceParam()
        {
            mStepInfo = new StepInfo();
            ToolType = ToolType.CompareDistance;
            StepInfo.mToolType = ToolType.CompareDistance;
            mShowName = "间距比较";
            mToolName = "间距比较";
            mStepInfo.mShowName = "间距比较";

            mLine1StepIndex = -1;
            mLine2StepIndex = -1;
            mLine1StepMark = -1;
            mLine2StepMark = -1;
            mCalibrationStepIndex = -1;
            mSelectMinValue = 0;
            mSelectMaxValue = 99999;
            mNgReturnValue = 1;
        }


    }
    public class ToolCompareDistance : ToolBase
    {
        ToolCompareDistanceParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolCompareDistanceParam;
        }

        public ToolCompareDistance(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolCompareDistanceParam;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            try
            {
                double dis1 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[0];
                double dis2 = StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[0];
                double dis3 = Math.Abs(dis1 - dis2);
                mToolParam.ResultString =
                    "间距1为：" + dis1.ToString("0.00") + "\r\n" +
                    "间距2为：" + dis2.ToString("0.00") + "\r\n" +
                    "两者差值为：" + dis3.ToString("0.00");
                if (dis3 > mToolParam.mSelectMaxValue)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
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

        public override int ToolRun(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            try
            {
                double dis1 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[0];
                double dis2 = StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[0];
                double dis3 = Math.Abs(dis1 - dis2);
                mToolParam.ResultString =
                    "间距1为：" + dis1.ToString("0.00") + "\r\n" +
                    "间距2为：" + dis2.ToString("0.00") + "\r\n" +
                    "两者差值为：" + dis3.ToString("0.00");
                if (dis3 > mToolParam.mSelectMaxValue)
                    return 1;
                return 0;
            }
            catch (Exception ex)
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
            }
            else
            {
                mToolParam.mParamChangedDe = null;
            }

            return ResStatus.OK;
        }
    }
}
