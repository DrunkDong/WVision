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
    public partial class UToolAngleLL : UserControl, InterfaceUIBase
    {
        public UToolAngleLL()
        {
            InitializeComponent();
        }

        #region Fields
        ToolAngleLLParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        bool mIsInit;
        public ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolAngleLLParam;
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

        private void UToolAngleLL_Load(object sender, EventArgs e)
        {
            mToolMachine = ToolMachine.GetInstance();
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }

        #region Events

        #endregion

        #region Methods
        public ResStatus InitRoiShow()
        {
            //更新当前列表顺序
            comboBox_LineSourceStep.Items.Clear();
            comboBox_LineSourceStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Line)
                {
                    comboBox_LineSourceStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            //对齐步骤
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

            comboBox_Line2SourceStep.Items.Clear();
            comboBox_Line2SourceStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Line)
                {
                    comboBox_Line2SourceStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            //对齐步骤
            if (mToolParam.mLine2SourceMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mLine2SourceMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_Line2SourceStep.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_Line2SourceStep.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mLine2SourceStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_Line2SourceStep.Text = "";
                            mToolParam.mLine2SourceStep = -1;
                            mToolParam.mLine2SourceMark = -1;
                        }
                    }
                }

                if (!mIsfind)
                {
                    comboBox_Line2SourceStep.Text = "";
                    mToolParam.mLine2SourceStep = -1;
                    mToolParam.mLine2SourceMark = -1;
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
            mToolParam.mAngleMin = (double)numericUpDown_MinAngle.Value;
            mToolParam.mAngleMax = (double)numericUpDown_MaxAngle.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;

            if (radioButton_AngleLH.Checked)
                mToolParam.mMode = 0;
            else if (radioButton_AngleLV.Checked)
                mToolParam.mMode = 1;
            else if (radioButton_AngleLL.Checked)
                mToolParam.mMode = 2;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
        }

        private void InitParam() 
        {
            label_ToolName.Text = mToolParam.ToolName;
            textBox_RefreshName.Text= mToolParam.ShowName;

            numericUpDown_MinAngle.Value = (decimal)mToolParam.mAngleMin;
            numericUpDown_MaxAngle.Value = (decimal)mToolParam.mAngleMax;
            numericUpDown_RetrunValue1.Value = mToolParam.NgReturnValue;
            checkBox_ForceOK.Checked = mToolParam.ForceOK;

            if (mToolParam.mMode == 0)
            {
                radioButton_AngleLH.Checked = true;
                comboBox_Line2SourceStep.Enabled = false;
            }
            else if (mToolParam.mMode == 1)
            {
                radioButton_AngleLV.Checked = true;
                comboBox_Line2SourceStep.Enabled = false;
            }
            else if (mToolParam.mMode == 2)
            {
                radioButton_AngleLL.Checked = true;
            }

            comboBox_LineSourceStep.Items.Clear();
            comboBox_LineSourceStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Line)
                {
                    comboBox_LineSourceStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            //对齐步骤
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

            comboBox_Line2SourceStep.Items.Clear();
            comboBox_Line2SourceStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Line)
                {
                    comboBox_Line2SourceStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            //对齐步骤
            if (mToolParam.mLine2SourceMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mLine2SourceMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_Line2SourceStep.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_Line2SourceStep.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mLine2SourceStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_Line2SourceStep.Text = "";
                            mToolParam.mLine2SourceStep = -1;
                            mToolParam.mLine2SourceMark = -1;
                        }
                    }
                }

                if (!mIsfind)
                {
                    comboBox_Line2SourceStep.Text = "";
                    mToolParam.mLine2SourceStep = -1;
                    mToolParam.mLine2SourceMark = -1;
                }
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
                    mToolParam.StepInfo.mShowName= textBox_RefreshName.Text;
                    mToolMachine.mChangeToolName(StepIndex, textBox_RefreshName.Text);
                }
                else
                    textBox_RefreshName.Text = mToolParam.ShowName;
            }
        }

        private void numericUpDown_MinAngle_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage,StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_MaxAngle_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void checkBox_ForceOK_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_RetrunValue1_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_AngleLH_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (radioButton_AngleLH.Checked)
            {
                comboBox_Line2SourceStep.Enabled = false;
            }
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;

        }

        private void radioButton_AngleLV_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (radioButton_AngleLV.Checked)
            {
                comboBox_Line2SourceStep.Enabled = false;
            }
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;

        }

        private void radioButton_AngleLL_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (radioButton_AngleLL.Checked)
            {
                comboBox_Line2SourceStep.Enabled = true;
            }
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
        }

        private void comboBox_Line2SourceStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            string[] str = comboBox_Line2SourceStep.SelectedItem.ToString().Split('_');

            if (int.Parse(str[0]) > 0)
            {
                mToolParam.mLine2SourceStep = int.Parse(str[0]);
                mToolParam.mLine2SourceMark = StepInfoList[mToolParam.mLine2SourceStep - 1].mInnerToolID;
            }
            else
            {
                mToolParam.mLine2SourceStep = -1;
                mToolParam.mLine2SourceMark = -1;
            }
  
        }
        private void comboBox_LineSourceStep_SelectedIndexChanged(object sender, EventArgs e)
        {      
            if (!mIsInit)
                return;
            string[] str = comboBox_LineSourceStep.SelectedItem.ToString().Split('_');
            if (int.Parse(str[0]) > 0)
            {
                mToolParam.mLineSourceStep = int.Parse(str[0]);
                mToolParam.mLineSourceMark = StepInfoList[mToolParam.mLineSourceStep - 1].mInnerToolID;
            }
            else
            {
                mToolParam.mLineSourceStep = -1;
                mToolParam.mLineSourceMark = -1;
            }
        }
    }
}
