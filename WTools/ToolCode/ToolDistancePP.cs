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
    public class ToolDistancePPParam : ToolParamBase
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
        public ToolDistancePPParam()
        {
            mStepInfo = new StepInfo();
            ToolType = ToolType.DistancePP;
            StepInfo.mToolType = ToolType.DistancePP;
            mShowName = "点到点";
            mToolName = "点到点";
            mStepInfo.mShowName = "点到点";

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
    public class ToolDistancePP : ToolBase
    {
        ToolDistancePPParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolDistancePPParam;
        }

        public ToolDistancePP(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolDistancePPParam;

            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            try
            {
                mToolParam.ResultString = "";
                HObject cross1;
                HObject cross2;
                //拟合直线
                HTuple Row1, Row2, Col1, Col2;
  
                HTuple Dis = new HTuple();
                Row1 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[0];
                Col1 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[1];
                Row2 = StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[0];
                Col2 = StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[1];
                HOperatorSet.GenCrossContourXld(out cross1, Row1, Col1, 100, 0.78);
                HOperatorSet.GenCrossContourXld(out cross2, Row2, Col2, 100, 0.78);
                HOperatorSet.DistancePp(Row1, Col1, Row2, Col2, out Dis);

                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = Dis;
                mToolParam.ResultString ="点到点距离为" + Dis.D.ToString("0.00") + "\r\n";
                if (Dis > mToolParam.mSelectMaxValue || Dis < mToolParam.mSelectMinValue)
                {
                    mDrawWind.SetColor("magenta");
                    mDrawWind.SetLineWidth(3);
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(cross1);
                    mDrawWind.DispObj(cross2);
                    cross1.Dispose();
                    cross2.Dispose();
                    return 1;
                }              
                mDrawWind.SetColor("magenta");
                mDrawWind.SetLineWidth(3);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(cross1);
                mDrawWind.DispObj(cross2);
                cross1.Dispose();
                cross2.Dispose();
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
                //拟合直线
                HTuple Row1, Row2, Col1, Col2;
                HTuple deltaY, deltaX;
                HTuple Dis = new HTuple();
                Row1 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[0];
                Col1 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[1];
                Row2 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[2];
                Col2 = StepInfoList[mToolParam.mLine1StepIndex - 1].mToolRunResul.mParamOutPut[3];
                deltaY = (Row2 - Row1) / 20; deltaX = (Col2 - Col1) / 20;
                for (int i = 0; i < 20; i++)
                {
                    HTuple r, c, d;
                    r = Row1 + i * deltaY;
                    c = Col1 + i * deltaX;
                    HOperatorSet.DistancePl(r, c, StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLine2StepIndex - 1].mToolRunResul.mParamOutPut[3], out d);
                    Dis = Dis.TupleConcat(d);
                }
                double a = Dis.TupleMax().D;
                double b = Dis.TupleMin().D;
                double cc = Dis.TupleMean().D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = cc;
                if (cc > mToolParam.mSelectMaxValue || cc < mToolParam.mSelectMinValue)
                {
                    return mToolParam.NgReturnValue;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
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
