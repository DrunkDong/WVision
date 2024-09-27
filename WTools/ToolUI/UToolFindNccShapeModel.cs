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
    public partial class UToolFindNccShapeModel : UserControl, InterfaceUIBase
    {
        public UToolFindNccShapeModel()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolFindNccShapeModelParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;

        public ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolFindNccShapeModelParam;
        }
        public List<StepInfo> StepInfoList
        {
            get { return mStepInfoList; }
            set { mStepInfoList = value; }
        }
        public int StepIndex
        {
            get => mToolParam.StepInfo.mStepIndex;
            set => mToolParam.StepInfo.mStepIndex = value;
        }

        public ResStatus InitRoiShow()
        {  //更新当前列表顺序
            comboBox_ImageStep.Items.Clear();
            comboBox_ImageStep.Items.Add("0_本地图片");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Image)
                {
                    comboBox_ImageStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            //对齐步骤
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
            return ResStatus.OK;
        }

        public ResStatus SetDebugRunWind(HTuple mShowWind, HWindow mDrawWind)
        {
            mToolMachine = ToolMachine.GetInstance();
            return ResStatus.OK;
        }


        private void button_Check_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        public ResStatus ShowCurrMes()
        {
            textBox_Mes.Text = "";
            textBox_Mes.Text = mToolParam.ResultString;
            return ResStatus.OK;
        }

        private void InitParam()
        {
            label_ToolName.Text = mToolParam.ToolName;
            textBox_RefreshName.Text = mToolParam.ShowName;
            numericUpDown_RetrunValue1.Value = mToolParam.NgReturnValue;
            checkBox_ForceOK.Checked = mToolParam.ForceOK;

            numericUpDown_NumLevel.Value = mToolParam.mNumLel;
            numericUpDown_StartPhi.Value = mToolParam.CreateStartAngle;
            numericUpDown_EndPhi.Value = mToolParam.CreateExtentAngle;
            comboBox_Polarity.Text = mToolParam.CreatePolarity;

            numericUpDown_FindStartPhi.Value = mToolParam.FindStartAngle;
            numericUpDown_FindEndPhi.Value = mToolParam.FindExtentAngle;
            numericUpDown_ModelScore.Value = (decimal)mToolParam.FindScore;
            numericUpDown_Greediness.Value = (decimal)mToolParam.Greediness;
            numericUpDown_OverLap.Value = (decimal)mToolParam.Overloap;
            numericUpDown_TimeOut.Value = mToolParam.TimeOut;
            numericUpDown_ZoomParam.Value = (decimal)mToolParam.ZoomParam;

            comboBox_ImageStep.Items.Clear();
            comboBox_ImageStep.Items.Add("0_本地图片");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.Image)
                {
                    comboBox_ImageStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            if (mToolParam.mImageSourceMark > 0)
            {
                string str = mToolParam.mImageSourceStep + "_" + StepInfoList[mToolParam.mImageSourceStep - 1].mShowName;
                if (comboBox_ImageStep.Items.Contains(str))
                {
                    comboBox_ImageStep.SelectedItem = str;
                }
                else
                {
                    mToolParam.mImageSourceStep = -1;
                    mToolParam.mImageSourceMark = -1;
                }
            }
        }

        private void UToolFindNccShapeModel_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }

        private void GetParam()
        {
            mToolParam.mNumLel = (int)numericUpDown_NumLevel.Value;
            mToolParam.CreateStartAngle = (int)numericUpDown_StartPhi.Value;
            mToolParam.CreateExtentAngle = (int)numericUpDown_EndPhi.Value;
            mToolParam.CreatePolarity = comboBox_Polarity.Text;

            mToolParam.FindStartAngle = (int)numericUpDown_FindStartPhi.Value;
            mToolParam.FindExtentAngle = (int)numericUpDown_FindEndPhi.Value;
            mToolParam.FindScore = (double)numericUpDown_ModelScore.Value;
            mToolParam.Greediness = (double)numericUpDown_Greediness.Value;
            mToolParam.Overloap = (double)numericUpDown_OverLap.Value;
            mToolParam.TimeOut = (int)numericUpDown_TimeOut.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
            mToolParam.ZoomParam = (double)numericUpDown_ZoomParam.Value;
        }

        private void button_CreateShapeModel_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple r, c;
            HTuple a = mToolParam.mModelRegion.AreaCenter(out r, out c);
            if (a < 100)
            {
                textBox_Mes.Text = "请先绘制ROI";
                return;
            }
            GetParam();
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            mToolMachine.mEnableControlDel(false);
            HObject ob1;
            mToolMachine.mEnableControlDel(false);
            mToolParam.mGenNccModelDe(mToolMachine.ToolCurrImage,StepInfoList, out ob1);
            mToolMachine.mEnableControlDel(true);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
        }


        private void trackBar_MarkSize_Scroll(object sender, EventArgs e)
        {
            textBox_MarkSize.Text = trackBar_MarkSize.Value.ToString();
        }

        private void numericUpDown_StartPhi_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
            mToolParam.mGenNccModelDe(mToolMachine.ToolCurrImage, StepInfoList, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
        }

        private void numericUpDown_EndPhi_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
            mToolParam.mGenNccModelDe(mToolMachine.ToolCurrImage, StepInfoList, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
        }

        private void comboBox_Polarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
            mToolParam.mGenNccModelDe(mToolMachine.ToolCurrImage, StepInfoList, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
        }

        private void numericUpDown_FindStartPhi_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_FindEndPhi_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_ModelScore_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_Greediness_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;

        }

        private void numericUpDown_TimeOut_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_CamFindModel_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_UpdataShapeModel_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_DeleROI_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            mToolParam.mDeletModelDe();
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_OverLap_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
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
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
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
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_LoadSaveImage_Click(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            HImage image;
            int res = mToolParam.mLoadImageDel(out image);
            if (res == 0)
            {
                mToolMachine.ToolCurrImage = (HObject)image;
                mToolMachine.ToolCurrHimage = image;
                mToolMachine.DrawWind.ClearWindow();
                mToolMachine.DrawWind.AttachBackgroundToWindow(image);
            }
        }

        private void button_DrawROI_Click_1(object sender, EventArgs e)
        {
            string mType;
            if (radioButton_Circle.Checked)
                mType = "circle";
            else
                mType = "rectangle1";
            int mSize = trackBar_MarkSize.Value;
            mToolMachine.mEnableControlDel(false);
            HObject obj;
            mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, 0, mType, mSize, out obj);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj);
            mToolMachine.mEnableControlDel(true);
        }

        private void button_RidRoi_Click(object sender, EventArgs e)
        {
            string mType;
            if (radioButton_Circle.Checked)
                mType = "circle";
            else
                mType = "rectangle1";
            int mSize = trackBar_MarkSize.Value;
            mToolMachine.mEnableControlDel(false);
            HObject obj;
            mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, 1, mType, mSize, out obj);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj);
            mToolMachine.mEnableControlDel(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mToolParam.mDeletPaintRoiDe();
        }

        private void trackBar_MarkSize_Scroll_1(object sender, EventArgs e)
        {
            textBox_MarkSize.Text = trackBar_MarkSize.Value.ToString();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                string mType = "circle";
                int mode = 0;
                //结构体类型
                if (radioButton_Circle.Checked)
                    mType = "circle";
                else if (radioButton_Rec1.Checked)
                    mType = "rectangle1";
                //操作模式
                if (radioButton_Paint.Checked)
                    mode = 0;
                else if (radioButton_Rid.Checked)
                    mode = 1;
                int mSize = trackBar_MarkSize.Value;
                mToolMachine.mEnableControlDel(false);
                HObject obj;
                mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, mode, mType, mSize, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }
        }

        private void button_DrawRoi2_Click_1(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                string mType = "circle";
                //结构体类型
                if (radioButton_Circle2.Checked)
                    mType = "circle";
                else if (radioButton_Rec2.Checked)
                    mType = "rectangle1";
                else if (radioButton_Rec22.Checked)
                    mType = "rectangle2";
                else if (radioButton_ellipse.Checked)
                    mType = "ellipse";
                else if (radioButton_Any.Checked)
                    mType = "any";

                mToolMachine.mEnableControlDel(false);
                HObject obj;
                mToolParam.mDrawRoi2Del(mToolMachine.ToolCurrImage, mType, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mToolParam.mModelRegion.GenEmptyRegion();
            mToolMachine.DrawWind.ClearWindow();
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

        private void numericUpDown_NumLevel_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
            mToolParam.mGenNccModelDe(mToolMachine.ToolCurrImage, StepInfoList, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
        }

        private void numericUpDown_ZoomParam_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
            mToolParam.mGenNccModelDe(mToolMachine.ToolCurrImage, StepInfoList, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
            mToolParam.mSureModelDe(mToolMachine.ToolCurrImage);
        }

        private void Button_FindRegion_Click(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                string mType = "circle";
                //结构体类型
                if (radioButton_Circle2.Checked)
                    mType = "circle";
                else if (radioButton_Rec2.Checked)
                    mType = "rectangle1";
                else if (radioButton_Rec22.Checked)
                    mType = "rectangle2";
                else if (radioButton_ellipse.Checked)
                    mType = "ellipse";
                else if (radioButton_Any.Checked)
                    mType = "any";

                mToolMachine.mEnableControlDel(false);
                HObject obj;
                mToolParam.mDrawRoiFindRegionDel(mToolMachine.ToolCurrImage, mType, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }
        }
    }
}
