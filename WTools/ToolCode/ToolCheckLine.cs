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
    public class ToolCheckLineParam : ToolParamBase
    {
        public double[] mRoiValue;
        public int mMeasureWidth;
        public int mMeasureHeight;
        public int mMeasureDistance;
        public int mMeasureThreshold;
        public string mMeasureSelect;
        public string mMeasureTransition;
        public bool mIsShowMeasureObj;
        public int ShapeModelStep;
        public int mShapeModelMark;

        public int mImageSourceStep;//图像源
        public int mImageSourceMark;//图像源
        public int mLineSourceStep;
        public int mLineSourceMark;

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

        public delegate int ParamChangedDe(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj);
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;

        public ToolCheckLineParam()
        {
            mStepInfo = new StepInfo();
            mStepJumpInfo = new JumpInfo();
            mStepInfo.mToolType = ToolType.FindLine;
            mToolType = ToolType.FindLine;
            mStepInfo.mToolResultType = ToolResultType.Line;
            mToolResultType = ToolResultType.Line;

            mShowName = "查找直线";
            mToolName = "查找直线";
            mStepInfo.mShowName = "查找直线";
            mResultString = "";
            mImageSourceStep = -1;
            mImageSourceMark = -1;
            mLineSourceStep = -1;
            mLineSourceMark = -1;
            mRoiValue = new double[8];
            mMeasureWidth = 20;
            mMeasureHeight = 120;
            mMeasureDistance = 10;
            mMeasureThreshold = 30;
            mMeasureSelect = "all";
            mMeasureTransition = "all";
            mIsShowMeasureObj = true;
            ShapeModelStep = -1;
            mShapeModelMark = -1;
            mNgReturnValue = 1;
        }
    }

    public class ToolCheckLine : ToolBase
    {
        ToolCheckLineParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;


        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolCheckLineParam;
        }

        public ToolCheckLine(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolCheckLineParam;
            BindDelegate(true);
        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mParamChangedDe = new ToolCheckLineParam.ParamChangedDe(ParamChanged);
            }
            else
            {
                mToolParam.mParamChangedDe = null;
            }

            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj)
        {
            JumpInfo StepJumpInfo;
            return DebugRun(obj1,StepInfoList, false, out StepJumpInfo);
        }

        public override int DebugRun(HObject objj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            if (mToolParam.ForceOK)
                return 0;
            if (mToolParam.mLineSourceStep < 0)
            {
                if (mToolParam.mRoiValue.All(i => i == 0))
                {
                    mToolParam.ResultString = "内部直线为空，请输入或绘制一条直线";
                    return 1;
                }
            }
            else
            {
                if (StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut.All(i => i == 0))
                {
                    mToolParam.ResultString = "外部输入直线为空";
                    return 1;
                }
            }

            try
            {
                HObject objFinal;
                //图像输入步骤
                if (mToolParam.mImageSourceStep > -1)
                    objFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    objFinal = objj1;

                HTuple row1, column1, row2, column2;
                double rowFinal1, columnFinal1, rowFinal2, columnFinal2;
                //获取直线输入来源
                if (mToolParam.mLineSourceStep < 0)
                {
                    row1 = mToolParam.mRoiValue[0];
                    column1 = mToolParam.mRoiValue[1];
                    row2 = mToolParam.mRoiValue[2];
                    column2 = mToolParam.mRoiValue[3];
                }
                else
                {
                    row1 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0];
                    column1 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1];
                    row2 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2];
                    column2 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3];
                }

                //是否有模板匹配，若有则转换坐标
                if (mToolParam.ShapeModelStep > -1)
                {
                    HTuple HomMat2D;
                    HTuple r1 = new HTuple();
                    HTuple c1 = new HTuple();
                    HTuple r2 = new HTuple();
                    HTuple c2 = new HTuple();
                    HOperatorSet.VectorAngleToRigid(
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                        0,
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                        out HomMat2D);
                    HOperatorSet.AffineTransPoint2d(HomMat2D, row1, column1, out r1, out c1);
                    HOperatorSet.AffineTransPoint2d(HomMat2D, row2, column2, out r2, out c2);
                    HomMat2D.Dispose();
                    rowFinal1 = r1.D;
                    columnFinal1 = c1.D;
                    rowFinal2 = r2.D;
                    columnFinal2 = c2.D;
                }
                else
                {
                    rowFinal1 = row1;
                    columnFinal1 = column1;
                    rowFinal2 = row2;
                    columnFinal2 = column2;
                }


                HObject ho_Contours, ho_Contour;
                HTuple hv_Row = new HTuple();HTuple hv_Column = new HTuple();
                HTuple s1, s2;
                HOperatorSet.CountSeconds(out s1);
                HTuple param = new HTuple();
                param[0] = "num_measures";
                param[1] = "measure_select";
                param[2] = "measure_transition";

                HTuple paramValue = new HTuple();
                paramValue[0] = mToolParam.mMeasureDistance;
                paramValue[1] = mToolParam.mMeasureSelect;
                paramValue[2] = mToolParam.mMeasureTransition;

                HTuple hv_Width = new HTuple(); HTuple hv_Index = new HTuple();
                HTuple hv_Height = new HTuple(), hv_MetrologyHandle = new HTuple();
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(objFinal, out hv_Width, out hv_Height);//获取大小
                hv_MetrologyHandle.Dispose();
                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);//创建搜索模型
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);//设置大小
                HOperatorSet.AddMetrologyObjectLineMeasure(hv_MetrologyHandle, rowFinal1, columnFinal1,rowFinal2, columnFinal2,//参数导入模型
                    mToolParam.mMeasureHeight, mToolParam.mMeasureWidth, 1, mToolParam.mMeasureThreshold, param, paramValue, out hv_Index);
                HOperatorSet.ApplyMetrologyModel(objFinal, hv_MetrologyHandle);//获取模型结果                                                                              
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type", "all_param", out HTuple hv_Parameter); //获取参数结果
                HOperatorSet.CountSeconds(out s2);
                if (mToolParam.mIsShowMeasureObj)
                {
                    hv_Row.Dispose(); hv_Column.Dispose();
                    HOperatorSet.GetMetrologyObjectMeasures(out ho_Contours, hv_MetrologyHandle, "all", "all", out hv_Row, out hv_Column);//获取检测框
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(objFinal);
                    mDrawWind.SetLineWidth(2);
                    mDrawWind.SetColored(12);
                    mDrawWind.DispObj(ho_Contours);
                    ho_Contours.Dispose();
                }
                HOperatorSet.GetMetrologyObjectResultContour(out ho_Contour, hv_MetrologyHandle, 0, "all", 1.5);
                if (ho_Contour.CountObj() < 1) 
                {
                    ho_Contour.Dispose();
                    hv_MetrologyHandle.Dispose();
                    mToolParam.ResultString = "查找直线失败！";
                    return mToolParam.NgReturnValue;
                }
                //传值
                mToolParam.StepInfo.mToolRunResul.mParamOutPut = new double[8];
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = hv_Parameter[0].D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = hv_Parameter[1].D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = hv_Parameter[2].D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = hv_Parameter[3].D;

                mToolParam.ResultString = "耗时：" + ((s2.D - s1.D) * 1000).ToString("f2") + "ms" + "\r\n";
                mToolParam.ResultString += "直线坐标为：\n" +
                    "Row1:=" +hv_Parameter[0].D.ToString("0.00") + "   " + 
                    "Column1:=" + hv_Parameter[1].D.ToString("0.00") + "   " + 
                    "Row2:=" + hv_Parameter[2].D.ToString("0.00") + "   " + 
                    "Column2:=" + hv_Parameter[3].D.ToString("0.00") + "\r\n";

                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColor("magenta");
                mDrawWind.DispObj(ho_Contour);
                ho_Contour.Dispose();
                hv_MetrologyHandle.Dispose();
                return 0;
            }
            catch (Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public override int ToolRun(HObject objj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            if (mToolParam.ForceOK)
                return 0;


            try
            {
                if (mToolParam.mLineSourceStep < 0)
                {
                    if (mToolParam.mRoiValue.All(i => i == 0))
                    {
                        mToolParam.ResultString = "内部直线为空，请输入或绘制一条直线";
                        return 1;
                    }
                }
                else
                {
                    if (StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut.All(i => i == 0))
                    {
                        mToolParam.ResultString = "外部输入直线为空";
                        return 1;
                    }
                }
                HObject objFinal;
                //图像输入步骤
                if (mToolParam.mImageSourceStep > -1)
                    objFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    objFinal = objj1;

                HTuple row1, column1, row2, column2;
                double rowFinal1, columnFinal1, rowFinal2, columnFinal2;
                //获取直线输入来源
                if (mToolParam.mLineSourceStep < 0)
                {
                    row1 = mToolParam.mRoiValue[0];
                    column1 = mToolParam.mRoiValue[1];
                    row2 = mToolParam.mRoiValue[2];
                    column2 = mToolParam.mRoiValue[3];
                }
                else
                {
                    row1 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[0];
                    column1 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[1];
                    row2 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[2];
                    column2 = StepInfoList[mToolParam.mLineSourceStep - 1].mToolRunResul.mParamOutPut[3];
                }

                //是否有模板匹配，若有则转换坐标
                if (mToolParam.ShapeModelStep > -1)
                {
                    HTuple HomMat2D;
                    HTuple r1 = new HTuple();
                    HTuple c1 = new HTuple();
                    HTuple r2 = new HTuple();
                    HTuple c2 = new HTuple();
                    HOperatorSet.VectorAngleToRigid(
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                        0,
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                        StepInfoList[mToolParam.ShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                        out HomMat2D);
                    HOperatorSet.AffineTransPoint2d(HomMat2D, row1, column1, out r1, out c1);
                    HOperatorSet.AffineTransPoint2d(HomMat2D, row2, column2, out r2, out c2);
                    HomMat2D.Dispose();
                    rowFinal1 = r1.D;
                    columnFinal1 = c1.D;
                    rowFinal2 = r2.D;
                    columnFinal2 = c2.D;
                }
                else
                {
                    rowFinal1 = row1;
                    columnFinal1 = column1;
                    rowFinal2 = row2;
                    columnFinal2 = column2;
                }

                HTuple param = new HTuple();
                param[0] = "num_measures";
                param[1] = "measure_select";
                param[2] = "measure_transition";

                HTuple paramValue = new HTuple();
                paramValue[0] = mToolParam.mMeasureDistance;
                paramValue[1] = mToolParam.mMeasureSelect;
                paramValue[2] = mToolParam.mMeasureTransition;

                HTuple hv_Width = new HTuple(); HTuple hv_Index = new HTuple();
                HTuple hv_Height = new HTuple(), hv_MetrologyHandle = new HTuple();
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(objFinal, out hv_Width, out hv_Height);//获取大小
                hv_MetrologyHandle.Dispose();
                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);//创建搜索模型
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);//设置大小
                HOperatorSet.AddMetrologyObjectLineMeasure(hv_MetrologyHandle, rowFinal1, columnFinal1, rowFinal2, columnFinal2,//参数导入模型
                    mToolParam.mMeasureHeight, mToolParam.mMeasureWidth, 1, mToolParam.mMeasureThreshold, param, paramValue, out hv_Index);
                HOperatorSet.ApplyMetrologyModel(objFinal, hv_MetrologyHandle);//获取模型结果                                                                              
                HOperatorSet.GetMetrologyObjectResult(hv_MetrologyHandle, 0, "all", "result_type", "all_param", out HTuple hv_Parameter); //获取参数结果
                //传值
                mToolParam.StepInfo.mToolRunResul.mParamOutPut = new double[8];
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = hv_Parameter[0].D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = hv_Parameter[1].D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = hv_Parameter[2].D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = hv_Parameter[3].D;
                hv_MetrologyHandle.Dispose();
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
    }
}
