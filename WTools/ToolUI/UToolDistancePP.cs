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
    public partial class UToolDistancePP : UserControl,InterfaceUIBase
    {
        public UToolDistancePP()
        {
            InitializeComponent();
        }

        ToolDistancePPParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        bool mIsInit;

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolDistancePPParam; 
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

        public ResStatus InitRoiShow()
        {
            //更新当前列表顺序
            comboBox_Line1Source.Items.Clear();
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Point || StepInfoList[i].mToolResultType == ToolResultType.Circle)
                {
                    comboBox_Line1Source.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }

            comboBox_Line2Source.Items.Clear();
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Point|| StepInfoList[i].mToolResultType == ToolResultType.Circle)
                {
                    comboBox_Line2Source.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }

            //对齐步骤
            if (mToolParam.mLine1StepMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mLine1StepMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_Line1Source.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_Line1Source.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mLine1StepIndex = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_Line1Source.Text = "";
                            mToolParam.mLine1StepIndex = -1;
                            mToolParam.mLine1StepMark = -1;
                        }
                    }
                }
                if (!mIsfind)
                {
                    comboBox_Line1Source.Text = "";
                    mToolParam.mLine1StepIndex = -1;
                    mToolParam.mLine1StepMark = -1;
                }
            }

            //对齐步骤
            if (mToolParam.mLine2StepMark > 0)
            {
                bool mIsFind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mLine2StepMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_Line2Source.Items.Contains(str))
                        {
                            mIsFind = true;
                            //更新显示
                            comboBox_Line2Source.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mLine2StepIndex = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_Line2Source.Text = "";
                            mToolParam.mLine2StepIndex = -1;
                            mToolParam.mLine2StepMark = -1;
                        }
                    }
                }

                if (!mIsFind)
                {
                    comboBox_Line2Source.Text = "";
                    mToolParam.mLine2StepIndex = -1;
                    mToolParam.mLine2StepMark = -1;
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

        private void UToolDistancePP_Load(object sender, EventArgs e)
        {
            mToolMachine = ToolMachine.GetInstance();
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }
        private void GetParam() 
        {
            mToolParam.mSelectMinValue = (int)numericUpDown_DistanceMin.Value;
            mToolParam.mSelectMaxValue = (int)numericUpDown_DistanceMax.Value;

            mToolParam.NgReturnValue  = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
        }
        private void InitParam() 
        {
            label_ToolName.Text = mToolParam.ToolName;
            textBox_RefreshName.Text = mToolParam.ShowName;
            numericUpDown_RetrunValue1.Value = mToolParam.NgReturnValue;
            checkBox_ForceOK.Checked = mToolParam.ForceOK;

            numericUpDown_DistanceMin.Value = mToolParam.mSelectMinValue;
            numericUpDown_DistanceMax.Value = mToolParam.mSelectMaxValue;
            comboBox_Line1Source.Items.Clear();
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;
                if (StepInfoList[i].mToolType == ToolType.FindCircle) 
                {
                    comboBox_Line1Source.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            comboBox_Line2Source.Items.Clear();
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;
                if (StepInfoList[i].mToolType == ToolType.FindCircle)
                {
                    comboBox_Line2Source.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }

            if (mToolParam.mLine1StepIndex > 0) 
            {
                string str = (mToolParam.mLine1StepIndex) + "_" + StepInfoList[mToolParam.mLine1StepIndex - 1].mShowName;
                if (comboBox_Line1Source.Items.Contains(str))
                {
                    comboBox_Line1Source.SelectedItem = str;
                }
               
            }
                
            if (mToolParam.mLine2StepIndex > 0) 
            {
                string str = (mToolParam.mLine2StepIndex) + "_" + StepInfoList[mToolParam.mLine2StepIndex - 1].mShowName;
                if (comboBox_Line2Source.Items.Contains(str))
                {
                    comboBox_Line2Source.SelectedItem = str;
                }
            }
        }

        private void comboBox_Line1Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            mToolParam.mLine1StepIndex = int.Parse(comboBox_Line1Source.Text.Split('_')[0]);
            mToolParam.mLine1StepMark = StepInfoList[mToolParam.mLine1StepIndex - 1].mInnerToolID;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00")+"ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_Line2Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            mToolParam.mLine2StepIndex = int.Parse(comboBox_Line2Source.Text.Split('_')[0]);
            mToolParam.mLine2StepMark = StepInfoList[mToolParam.mLine2StepIndex - 1].mInnerToolID;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_DistanceMin_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            numericUpDown_DistanceMin.Maximum = numericUpDown_DistanceMax.Value - 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_DistanceMax_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            numericUpDown_DistanceMax.Minimum = numericUpDown_DistanceMin.Value + 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }

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
            if (mToolMachine.ToolCurrImage == null || !mToolMachine.ToolCurrImage.IsInitialized())
                return;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
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
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00") + "ms");
            textBox_Mes.Text = mToolParam.ResultString;
        }
    }
}
