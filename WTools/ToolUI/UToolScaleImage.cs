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
    public partial class UToolScaleImage : UserControl,InterfaceUIBase
    {
        public UToolScaleImage()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolScaleImageParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolScaleImageParam; 
            
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

        private void UToolScaleImage_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }


        #region Methods
        private void InitParam()
        {
            label_ToolName.Text = mToolParam.ToolName;
            textBox_RefreshName.Text = mToolParam.ShowName;
            numericUpDown_RetrunValue1.Value = mToolParam.NgReturnValue;
            checkBox_ForceOK.Checked = mToolParam.ForceOK;

            panel1.Enabled = mToolParam.mIsOpenCheck;
            checkBox_mIsOpenCheck.Checked= mToolParam.mIsOpenCheck;
            numericUpDown_Scale1.Value = mToolParam.mScaleMin;
            numericUpDown_Scale2.Value = mToolParam.mScaleMax;
            
            numericUpDown_ThresMin.Value = mToolParam.mThresholdMin;
            numericUpDown_ThresMax.Value = mToolParam.mThresholdMax;
            numericUpDown_SelectMin.Value = mToolParam.mSelectMin;
            numericUpDown_SelectMax.Value = mToolParam.mSelectMax;
            numericUpDown_SelectAreaMin.Value = mToolParam.mAreaMin;
            numericUpDown_SelectAreaMax.Value = mToolParam.mAreaMax;

            numericUpDown_BlobParam.Value = (decimal)mToolParam.mBlobParam;
            comboBox_BlobMode.SelectedItem = mToolParam.mBlobMode;
            if (mToolParam.mSelectMode == 0)
                radioButton_Max.Checked = true;
            else if (mToolParam.mSelectMode == 1)
                radioButton_Min.Checked = true;
            else
                radioButton_All.Checked = true;
            if (mToolParam.mConnectMode == 0) 
                radioButton_ConnectMode1.Checked = true;
            else
                radioButton_ConnectMode2.Checked = true;
            if (mToolParam.mCheckMode == 0)
                radioButton_CheckMode1.Checked = true;
            else
                radioButton_CheckMode2.Checked = true;


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

            comboBox_PositioningStep.Items.Clear();
            comboBox_PositioningStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
               if (StepIndex <= (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.ImageAlignData)
                {
                    comboBox_PositioningStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            if (mToolParam.mShapeModelMark > 0)
            {
                string str = mToolParam.mShapeModelStep + "_" + StepInfoList[mToolParam.mShapeModelStep - 1].mShowName;
                if (comboBox_PositioningStep.Items.Contains(str))
                {
                    comboBox_PositioningStep.SelectedItem = str;
                }
                else
                {
                    mToolParam.mShapeModelStep = -1;
                    mToolParam.mShapeModelMark = -1;
                }
            }

            comboBox_RegionStep.Items.Clear();
            comboBox_RegionStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
               if (StepIndex <= (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.Region)
                {
                    comboBox_RegionStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            if (mToolParam.mRegionMark > 0)
            {
                string str = mToolParam.mRegionStep + "_" + StepInfoList[mToolParam.mRegionStep - 1].mShowName;
                if (comboBox_RegionStep.Items.Contains(str))
                {
                    comboBox_RegionStep.SelectedItem = str;
                }
                else
                {
                    mToolParam.mRegionStep = -1;
                    mToolParam.mRegionMark = -1;
                }
            }
        }

        private void GetParam()
        {
            mToolParam.mIsOpenCheck= checkBox_mIsOpenCheck.Checked;
            mToolParam.mScaleMin = (int)numericUpDown_Scale1.Value;
            mToolParam.mScaleMax = (int)numericUpDown_Scale2.Value;
            mToolParam.mThresholdMin = (int)numericUpDown_ThresMin.Value;
            mToolParam.mThresholdMax = (int)numericUpDown_ThresMax.Value;
            mToolParam.mSelectMin = (int)numericUpDown_SelectMin.Value;
            mToolParam.mSelectMax = (int)numericUpDown_SelectMax.Value;
            mToolParam.mAreaMin = (int)numericUpDown_SelectAreaMin.Value;
            mToolParam.mAreaMax = (int)numericUpDown_SelectAreaMax.Value;

            mToolParam.mBlobParam = (double)numericUpDown_BlobParam.Value;
            mToolParam.mBlobMode = (string)comboBox_BlobMode.SelectedItem;

            if (radioButton_Max.Checked)
                mToolParam.mSelectMode = 0;
            else if (radioButton_Min.Checked)
                mToolParam.mSelectMode = 1;
            else
                mToolParam.mSelectMode = 2;

            if (radioButton_ConnectMode1.Checked)
                mToolParam.mConnectMode = 0 ;
            else
                mToolParam.mConnectMode = 1;
            if (radioButton_CheckMode1.Checked)
                 mToolParam.mCheckMode = 0;
            else
                mToolParam.mCheckMode = 1;

            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
        }

        public ResStatus ShowCurrMes()
        {
            textBox_Mes.Text = "";
            textBox_Mes.Text = mToolParam.ResultString;
            return ResStatus.OK;
        }

        public ResStatus InitRoiShow()
        {
            //更新当前列表顺序
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


            comboBox_PositioningStep.Items.Clear();
            comboBox_PositioningStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
               if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.ImageAlignData)
                {
                    comboBox_PositioningStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            //对齐步骤
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
                            mToolParam.mShapeModelStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_PositioningStep.Text = "";
                            mToolParam.mShapeModelStep = -1;
                            mToolParam.mShapeModelMark = -1;
                        }
                    }
                }

                if (!mIsfind)
                {
                    comboBox_PositioningStep.Text = "";
                    mToolParam.mShapeModelStep = -1;
                    mToolParam.mShapeModelMark = -1;
                }
            }



            comboBox_RegionStep.Items.Clear();
            comboBox_RegionStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
               if (StepIndex <= (i + 1))
                    break;
                if (StepInfoList[i].mToolResultType == ToolResultType.Region)
                {
                    comboBox_RegionStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }
            }
            //对齐步骤
            if (mToolParam.mRegionMark > 0)
            {
                bool mIsfind = false;
                for (int i = 0; i < StepInfoList.Count; i++)
                {
                    if (StepInfoList[i].mInnerToolID == mToolParam.mRegionMark)
                    {
                        string str = (i + 1) + "_" + StepInfoList[i].mShowName;
                        //成功搜索
                        if (comboBox_RegionStep.Items.Contains(str))
                        {
                            mIsfind = true;
                            //更新显示
                            comboBox_RegionStep.SelectedItem = str;
                            string[] str1 = str.Split('_');
                            //更新
                            mToolParam.mRegionStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_RegionStep.Text = "";
                            mToolParam.mRegionMark = -1;
                            mToolParam.mRegionStep = -1;
                        }
                    }
                }

                if (!mIsfind)
                {
                    comboBox_RegionStep.Text = "";
                    mToolParam.mRegionMark = -1;
                    mToolParam.mRegionStep = -1;
                }
            }
            return ResStatus.OK;
        }

        public ResStatus SetDebugRunWind(HTuple mShowWind, HWindow mDrawWind)
        {
            mToolMachine = ToolMachine.GetInstance();
            return ResStatus.OK;
        }
        #endregion

        #region Events

        private void numericUpDown_Scale1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_Scale1.Value >= numericUpDown_Scale2.Value) 
            {
                numericUpDown_Scale1.Value = numericUpDown_Scale2.Value - 1;
            }
            if (!mIsInit)
                return;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            HTuple s1, s2;
            HObject obj;
            HOperatorSet.CountSeconds(out s1);
            mToolParam.mScaleImageDel(mToolMachine.ToolCurrImage,StepInfoList, out obj);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_Scale2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_Scale2.Value <= numericUpDown_Scale1.Value)
            {
                numericUpDown_Scale2.Value = numericUpDown_Scale1.Value + 1;
            }
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
            HObject obj;
            mToolParam.mScaleImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
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
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }
        #endregion

        private void button_DrawROI_Click(object sender, EventArgs e)
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
        private void button_DeleRoi_Click(object sender, EventArgs e)
        {
            mToolParam.mDeleRoiDel();
        }

        private void numericUpDown_ThresMin_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_ThresMax_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_SelectMin_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_SelectMax_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_SelectAreaMin_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_SelectAreaMax_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_PositioningStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            string[] str = comboBox_PositioningStep.SelectedItem.ToString().Split('_');
            mToolParam.mShapeModelStep = int.Parse(str[0]);
            mToolParam.mShapeModelMark = StepInfoList[mToolParam.mShapeModelStep - 1].mInnerToolID;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }

        private void trackBar_MarkSize_Scroll(object sender, EventArgs e)
        {

        }

        private void checkBox_mIsOpenCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            panel1.Enabled = checkBox_mIsOpenCheck.Checked;
            GetParam();
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

        private void button_DrawRoi2_Click(object sender, EventArgs e)
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

        private void button_DeleRoi_Click_1(object sender, EventArgs e)
        {
            mToolParam.mDeleRoiDel();
            mToolMachine.ShowRegionList.Clear();
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

        private void comboBox_PositioningStep_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_PositioningStep.SelectedItem.ToString().Split('_');
            if (str[0] == "0")
            {
                mToolParam.mShapeModelStep = -1;
                mToolParam.mShapeModelMark = -1;
            }
            else
            {
                mToolParam.mShapeModelStep = int.Parse(str[0]);
                mToolParam.mShapeModelMark = StepInfoList[mToolParam.mShapeModelStep - 1].mInnerToolID;
            }

            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }

        private void comboBox_RegionStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_RegionStep.SelectedItem.ToString().Split('_');
            if (str[0] == "0")
            {
                mToolParam.mRegionStep = -1;
                mToolParam.mRegionMark = -1;
            }
            else
            {
                mToolParam.mRegionStep = int.Parse(str[0]);
                mToolParam.mRegionMark = StepInfoList[mToolParam.mRegionStep - 1].mInnerToolID;
            }

            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }

        private void numericUpDown_BlobParam_ValueChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_BlobMode_SelectedIndexChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
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
            HTuple s1, s2;
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_Min_CheckedChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
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
            HTuple s1, s2;
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_ConnectMode1_CheckedChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_ConnectMode2_CheckedChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_CheckMode1_CheckedChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_CheckMode2_CheckedChanged(object sender, EventArgs e)
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mThresImageDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_Region_Click(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                mToolMachine.mEnableControlDel(false);
                HObject obj;
                double row, column, row2, column2;
                mToolMachine.DrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                mToolParam.mReduceRegion = new HRegion(obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }

        }
    }
}
