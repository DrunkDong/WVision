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

namespace WTools
{
    [Serializable]

    public class ToolAngleLLParam : ToolParamBase
    {
        public double mAngleMin;
        public double mAngleMax;
        public int mLineSourceStep;
        public int mLineSourceMark;
        public int mMode;

        public int mLine2SourceStep;
        public int mLine2SourceMark;

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

        public ToolAngleLLParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.AngleLL;
            mToolType = ToolType.AngleLL;
            StepInfo.mShowName = "角度测量";
            mShowName = "角度测量";
            mToolName = "角度测量";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";

            mMode = 0;
            mAngleMin = 0;
            mAngleMax = 1.5;
            mNgReturnValue = 1;
            mLineSourceStep = -2;
            mLineSourceMark = -1;
            mLine2SourceStep = -2;
            mLine2SourceMark = -1;
            NgReturnValue = 1;
        }
    }
    public class ToolAngleLL : ToolBase
    {
        ToolAngleLLParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolAngleLLParam;
        }

        public ToolAngleLL(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolAngleLLParam;

            BindDelegate(true);
        }
        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            //是否屏蔽功能
            if (mToolParam.ForceOK)
                return 0;
            if (mToolParam.mLineSourceStep < 0)
                return -1;

            try
            {
                if (mToolParam.mMode == 0) 
                {
                    HTuple tup1, tup2, tup3;
                    double deg;
                    HOperatorSet.AngleLx(
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3],
                        out tup1);
                    HObject obj23;
                    HOperatorSet.GenRegionLine(out obj23,
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    //取绝对值
                    HOperatorSet.TupleAbs(tup1, out tup2);
                    //转角度
                    double tup5 = tup2;
                    HOperatorSet.TupleDeg(tup2, out tup3);
                    if (tup3.D > 90)
                        deg = 180 - tup3.D;
                    else
                        deg = tup3.D;
                    mDrawWind.SetColor("green");
                    mDrawWind.SetLineWidth(2);
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(obj23);
                    obj23.Dispose();
                    mToolParam.ResultString = "直线与水平夹角为：" + tup5.ToString("0.000") + "度";
                    if (deg > mToolParam.mAngleMax || deg < mToolParam.mAngleMin)
                    {
                        return mToolParam.NgReturnValue;
                    }
                }
                else if (mToolParam.mMode == 1)
                {
                    HTuple tup1, tup2, tup3;
                    double deg;
                    HOperatorSet.AngleLx(
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3],
                        out tup1);
                    HObject obj23;
                    HOperatorSet.GenRegionLine(out obj23,
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    //取绝对值
                    HOperatorSet.TupleAbs(tup1, out tup2);
                    //转角度
                    HOperatorSet.TupleDeg(tup2, out tup3);
                    if (tup3.D > 90)
                        deg = 180 - tup3.D;
                    else
                        deg = tup3.D;
                    deg = 90 - deg;
                    mDrawWind.SetColor("green");
                    mDrawWind.SetLineWidth(2);
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(obj23);
                    obj23.Dispose();
                    mToolParam.ResultString = "直线与垂直线夹角为：" + deg.ToString("0.000") + "度";
                    if (deg > mToolParam.mAngleMax || deg < mToolParam.mAngleMin)
                    {
                        return mToolParam.NgReturnValue;
                    }
                }
                else if (mToolParam.mMode == 2)
                {
                    HTuple tup1, tup2, tup3;
                    double deg;
                    HOperatorSet.AngleLl(
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[3],
                        out tup1);
                    HObject obj23;
                    HOperatorSet.GenRegionLine(out obj23,
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    HObject obj24;
                    HOperatorSet.GenRegionLine(out obj24,
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    //取绝对值
                    HOperatorSet.TupleAbs(tup1, out tup2);
                    //转角度
                    HOperatorSet.TupleDeg(tup2, out tup3);
                    if (tup3.D > 90)
                        deg = 180 - tup3.D;
                    else
                        deg = tup3.D;
                    mDrawWind.SetColor("magenta");
                    mDrawWind.SetLineWidth(2);
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(obj23);
                    mDrawWind.DispObj(obj24);
                    obj23.Dispose();
                    obj24.Dispose();
                    mToolParam.ResultString = "直线1与直线2夹角为：" + deg.ToString("0.000") + "度";
                    if (deg > mToolParam.mAngleMax || deg < mToolParam.mAngleMin)
                    {
                        return mToolParam.NgReturnValue;
                    }
                }

                return 0;
            }
            catch (System.Exception ex)
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
            mToolParam.ResultString = "";
            //是否屏蔽功能
            if (mToolParam.ForceOK)
                return 0;
            if (mToolParam.mLineSourceStep < 0)
                return -1;
            try
            {
                if (mToolParam.mMode == 0)
                {
                    HTuple tup1, tup2, tup3;
                    double deg;
                    HOperatorSet.AngleLx(
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3],
                        out tup1);
                    HObject obj23;
                    HOperatorSet.GenRegionLine(out obj23,
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    //取绝对值
                    HOperatorSet.TupleAbs(tup1, out tup2);
                    //转角度
                    HOperatorSet.TupleDeg(tup2, out tup3);
                    if (tup3.D > 90)
                        deg = 180 - tup3.D;
                    else
                        deg = tup3.D;
                    obj23.Dispose();
                    if (deg > mToolParam.mAngleMax || deg < mToolParam.mAngleMin)
                    {
                        return mToolParam.NgReturnValue;
                    }
                }
                else if (mToolParam.mMode == 1)
                {
                    HTuple tup1, tup2, tup3;
                    double deg;
                    HOperatorSet.AngleLx(
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3],
                        out tup1);
                    HObject obj23;
                    HOperatorSet.GenRegionLine(out obj23,
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    //取绝对值
                    HOperatorSet.TupleAbs(tup1, out tup2);
                    //转角度
                    HOperatorSet.TupleDeg(tup2, out tup3);
                    if (tup3.D > 90)
                        deg = 180 - tup3.D;
                    else
                        deg = tup3.D;
                    deg = 90 - deg;
                    obj23.Dispose();
                    if (deg > mToolParam.mAngleMax || deg < mToolParam.mAngleMin)
                    {
                        return mToolParam.NgReturnValue;
                    }
                }
                else if (mToolParam.mMode == 2)
                {
                    HTuple tup1, tup2, tup3;
                    double deg;
                    HOperatorSet.AngleLl(
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[3],
                        out tup1);
                    HObject obj23;
                    HOperatorSet.GenRegionLine(out obj23,
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    HObject obj24;
                    HOperatorSet.GenRegionLine(out obj24,
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[1],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.mLine2SourceStep - 1].mToolRunResul.mParamOutPut[3]);
                    //取绝对值
                    HOperatorSet.TupleAbs(tup1, out tup2);
                    //转角度
                    HOperatorSet.TupleDeg(tup2, out tup3);
                    if (tup3.D > 90)
                        deg = 180 - tup3.D;
                    else
                        deg = tup3.D;
                    obj23.Dispose();
                    obj24.Dispose();
                    if (deg > mToolParam.mAngleMax || deg < mToolParam.mAngleMin)
                    {
                        return mToolParam.NgReturnValue;
                    }
                }

                return 0;
            }
            catch (System.Exception ex)
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
