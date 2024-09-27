using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTools
{
    [ToolboxItem(false)]
    public partial class UToolFindCircle : UserControl, InterfaceUIBase
    {
        public UToolFindCircle()
        {
            InitializeComponent();
            mToolMachine = ToolMachine.GetInstance();
        }

        #region Fields
        ToolFindCircleParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        bool mIsInit;
        public ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolFindCircleParam;
        }
        public List<StepInfo> StepInfoList
        {
            get { return mStepInfoList; }
            set { mStepInfoList = value; }
        }
        public int StepIndex
        {
            get => mToolParam.StepInfo.mStepIndex;
            set => mStepIndex = value;
        }
        #endregion

        private void UToolFindCircle_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }

        #region Events

        private void button_DrawRoi_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            mToolMachine.mEnableControlDel(false);
            mToolMachine.Control.Focus();
            mToolMachine.DrawWind.ClearWindow();
            mToolParam.mRoiValue = new double[8];
            mToolMachine.DrawWind.SetColor("red");
            mToolMachine.DrawWind.SetLineWidth(2);
            mToolMachine.DrawWind.DrawCircle(out mToolParam.mRoiValue[0], out mToolParam.mRoiValue[1], out mToolParam.mRoiValue[2]);
            mToolMachine.DrawWind.SetDraw("margin");
            HObject obj;
            HOperatorSet.GenCircle(out obj, mToolParam.mRoiValue[0], mToolParam.mRoiValue[1], mToolParam.mRoiValue[2]);
            mToolMachine.DrawWind.DispObj(obj);
            obj.Dispose();
            mToolMachine.mEnableControlDel(true);
        }

        private void trackBar_CheckHeight_Scroll(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            textBox_CheckHeight.Text = trackBar_CheckHeight.Value.ToString();
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void trackBar_CheckWidth_Scroll(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            textBox_CheckWidth.Text = trackBar_CheckWidth.Value.ToString();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void trackBar_CheckNum_Scroll(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            textBox_CheckNum.Text = trackBar_CheckNum.Value.ToString();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void trackBar_CheckThreshold_Scroll(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            textBox_Threshold.Text = trackBar_CheckThreshold.Value.ToString();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_Positive_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_Negative_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_All_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_First_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_Last_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_Max_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void checkBox_ShowRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_ShapeModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_PositioningStep.SelectedItem.ToString().Split('_');
            if (str[0] == "0")
            {
                mToolParam.ShapeModelStep = -1;
                mToolParam.mShapeModelMark = -1;
            }
            else
            {
                mToolParam.ShapeModelStep = int.Parse(str[0]);
                mToolParam.mShapeModelMark = StepInfoList[mToolParam.ShapeModelStep - 1].mInnerToolID;
            }

            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }

        private void comboBox_LineSourceStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
        }
        #endregion

        #region Methods
        public ResStatus InitRoiShow()
        {
            comboBox_PositioningStep.Items.Clear();
            comboBox_PositioningStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.ImageAlignData)
                {
                    comboBox_PositioningStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            if (mToolParam.mShapeModelMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mShapeModelMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_PositioningStep.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_PositioningStep.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.ShapeModelStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_PositioningStep.Text = "";
                            mToolParam.ShapeModelStep = -1;
                            mToolParam.mShapeModelMark = -1;
                        }
                    }
                }
                if (!mIsfind)
                {
                    comboBox_PositioningStep.Text = "";
                    mToolParam.ShapeModelStep = -1;
                    mToolParam.mShapeModelMark = -1;
                }
            }

            comboBox_ImageStep.Items.Clear();
            comboBox_ImageStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.Image)
                {
                    comboBox_ImageStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            if (mToolParam.mImageSourceMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mImageSourceMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_ImageStep.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_ImageStep.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mImageSourceStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_ImageStep.Text = "";
                            mToolParam.mImageSourceStep = -1;
                            mToolParam.mImageSourceMark = -1;
                        }
                    }
                }
                if (!mIsfind)
                {
                    comboBox_ImageStep.Text = "";
                    mToolParam.mImageSourceStep = -1;
                    mToolParam.mImageSourceMark = -1;
                }
            }

            comboBox_LineSourceStep.Items.Clear();
            comboBox_LineSourceStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.Line)
                {
                    comboBox_LineSourceStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            if (mToolParam.mLineSourceMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mLineSourceMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_LineSourceStep.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_LineSourceStep.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mLineSourceStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_LineSourceStep.Text = "";
                            mToolParam.mLineSourceStep = -1;
                            mToolParam.mLineSourceMark = -1;
                        }
                    }
                }
                if (!mIsfind)
                {
                    comboBox_LineSourceStep.Text = "";
                    mToolParam.mLineSourceStep = -1;
                    mToolParam.mLineSourceMark = -1;
                }
            }

            return ResStatus.OK;
        }

        public ResStatus SetDebugRunWind(HTuple mShowWind, HWindow mDrawWind)
        {
            mToolMachine = ToolMachine.GetInstance();
            return ResStatus.OK;
        }

        public ResStatus ShowCurrMes()
        {
            textBox_Mes.Text = mToolParam.ResultString;
            return ResStatus.OK;
        }

        private void GetParam()
        {
            mToolParam.mMeasureHeight = trackBar_CheckHeight.Value;
            mToolParam.mMeasureWidth = trackBar_CheckWidth.Value;
            mToolParam.mMeasureDistance = trackBar_CheckNum.Value;
            mToolParam.mMeasureThreshold = trackBar_CheckThreshold.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;

            if (radioButton_Positive.Checked)
            {
                mToolParam.mMeasureTransition = "positive";
            }
            else if (radioButton_Negative.Checked)
            {
                mToolParam.mMeasureTransition = "negative";
            }
            else if (radioButton_All.Checked)
            {
                mToolParam.mMeasureTransition = "all";
            }

            if (radioButton_First.Checked)
            {
                mToolParam.mMeasureSelect = "first";
            }
            else if (radioButton_Last.Checked)
            {
                mToolParam.mMeasureSelect = "last";
            }
            else if (radioButton_Max.Checked)
            {
                mToolParam.mMeasureSelect = "all";
            }
            if (checkBox_ShowRegion.Checked)
            {
                mToolParam.mIsShowMeasureObj = true;
            }
            else
            {
                mToolParam.mIsShowMeasureObj = false;
            }
        }

        private void InitParam()
        {
            try
            {
                label_ToolName.Text = mToolParam.ToolName;
                textBox_RefreshName.Text = mToolParam.ShowName;
                numericUpDown_RetrunValue1.Value = mToolParam.NgReturnValue;
                checkBox_ForceOK.Checked = mToolParam.ForceOK;

                trackBar_CheckHeight.Value = mToolParam.mMeasureHeight;
                trackBar_CheckWidth.Value = mToolParam.mMeasureWidth;
                trackBar_CheckNum.Value = mToolParam.mMeasureDistance;
                trackBar_CheckThreshold.Value = mToolParam.mMeasureThreshold;

                textBox_CheckHeight.Text = mToolParam.mMeasureHeight.ToString();
                textBox_CheckWidth.Text = mToolParam.mMeasureWidth.ToString();
                textBox_CheckNum.Text = mToolParam.mMeasureDistance.ToString();
                textBox_Threshold.Text = mToolParam.mMeasureThreshold.ToString();

                if (mToolParam.mMeasureTransition == "positive")
                {
                    radioButton_Positive.Checked = true;
                }
                else if (mToolParam.mMeasureTransition == "negative")
                {
                    radioButton_Negative.Checked = true;
                }
                else if (mToolParam.mMeasureTransition == "all")
                {
                    radioButton_All.Checked = true;
                }

                if (mToolParam.mMeasureSelect == "first")
                {
                    radioButton_First.Checked = true;
                }
                else if (mToolParam.mMeasureSelect == "last")
                {
                    radioButton_Last.Checked = true;
                }
                else if (mToolParam.mMeasureSelect == "all")
                {
                    radioButton_Max.Checked = true;
                }
                if (mToolParam.mIsShowMeasureObj)
                {
                    checkBox_ShowRegion.Checked = true;
                }
                else
                {
                    checkBox_ShowRegion.Checked = false;
                }

                comboBox_PositioningStep.Items.Clear();
                comboBox_PositioningStep.Items.Add("0_无输入");
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepIndex < (i + 1))
                        break;

                    if (StepInfoList[i].mToolResultType == ToolResultType.ImageAlignData)
                    {
                        comboBox_PositioningStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                    }

                }
                if (mToolParam.ShapeModelStep > 0)
                {
                    string str = mToolParam.ShapeModelStep + "_" + StepInfoList[mToolParam.ShapeModelStep - 1].mShowName;
                    if (comboBox_PositioningStep.Items.Contains(str))
                    {
                        comboBox_PositioningStep.SelectedItem = str;
                    }
                }

                comboBox_ImageStep.Items.Clear();
                comboBox_ImageStep.Items.Add("0_无输入");
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepIndex < (i + 1))
                        break;

                    if (StepInfoList[i].mToolResultType == ToolResultType.Image)
                    {
                        comboBox_ImageStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                    }

                }
                if (mToolParam.mImageSourceStep > 0)
                {
                    string str = mToolParam.mImageSourceStep + "_" + StepInfoList[mToolParam.mImageSourceStep - 1].mShowName;
                    if (comboBox_ImageStep.Items.Contains(str))
                    {
                        comboBox_ImageStep.SelectedItem = str;
                    }
                }

                comboBox_LineSourceStep.Items.Clear();
                comboBox_LineSourceStep.Items.Add("0_无输入");
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepIndex < (i + 1))
                        break;

                    if (StepInfoList[i].mToolResultType == ToolResultType.Line)
                    {
                        comboBox_LineSourceStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                    }

                }
                if (mToolParam.mLineSourceStep > 0)
                {
                    string str = mToolParam.mLineSourceStep + "_" + StepInfoList[mToolParam.mLineSourceStep - 1].mShowName;
                    if (comboBox_LineSourceStep.Items.Contains(str))
                    {
                        comboBox_LineSourceStep.SelectedItem = str;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      
        #endregion

        private void checkBox_ReName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ReName.Checked)
            {
                textBox_RefreshName.Enabled = true;
            }
            else
            {
                textBox_RefreshName.Enabled = false;
                if (textBox_RefreshName.Text != "" && textBox_RefreshName.Text != mToolParam.ShowName)
                {
                    mToolParam.ShowName = textBox_RefreshName.Text;
                    mToolParam.StepInfo.mShowName = textBox_RefreshName.Text;
                    mToolMachine.mChangeToolName(StepIndex, textBox_RefreshName.Text);
                }
                else
                    textBox_RefreshName.Text = mToolParam.ShowName;
            }
        }

        private void checkBox_ForceOK_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_RetrunValue1_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (mToolParam.mRoiValue.All(i => i == 0))
            {
                textBox_Mes.Text = "请绘制一条圆";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_ImageStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_ImageStep.SelectedItem.ToString().Split('_');
            if (str[0] == "0")
            {
                mToolParam.mImageSourceStep = -1;
                mToolParam.mImageSourceMark = -1;
            }
            else
            {
                mToolParam.mImageSourceStep = int.Parse(str[0]);
                mToolParam.mImageSourceMark = StepInfoList[mToolParam.mImageSourceStep - 1].mInnerToolID;
            }

            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }

        private void comboBox_LineSourceStep_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_LineSourceStep.SelectedItem.ToString().Split('_');
            if (str[0] == "0")
            {
                mToolParam.mLineSourceMark = -1;
                mToolParam.mLineSourceStep = -1;
            }
            else
            {
                mToolParam.mLineSourceStep = int.Parse(str[0]);
                mToolParam.mLineSourceMark = StepInfoList[mToolParam.mImageSourceStep - 1].mInnerToolID;
            }

            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }
    }
}
