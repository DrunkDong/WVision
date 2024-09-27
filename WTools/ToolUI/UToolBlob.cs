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
    public partial class UToolBlob : UserControl,InterfaceUIBase
    {
        public UToolBlob()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolBlobParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolBlobParam; 
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
            comboBox_ImageStep.Items.Clear();
            comboBox_ImageStep.Items.Add("0_本地图片");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
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
                if (StepIndex < (i + 1))
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
                if (StepIndex < (i + 1))
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

            numericUpDown_ThresMin.Value = mToolParam.mThresMin;
            numericUpDown_ThresMax.Value = mToolParam.mThresMax;

            checkBox_IsEnableDyn.Checked = mToolParam.mIsDynThres;
            if (mToolParam.mIsDynThres)
            {
                checkBox_IsEnableDyn.Checked = true;
                groupBox4.Enabled = true;
                numericUpDown_ThresMin.Enabled = false;
                numericUpDown_ThresMax.Enabled = false;
            }
            else
            {
                checkBox_IsEnableDyn.Checked = false;
                groupBox4.Enabled = false;
                numericUpDown_ThresMin.Enabled = true;
                numericUpDown_ThresMax.Enabled = true;
            }
            numericUpDown_DynMark.Value = (decimal)mToolParam.mDynMark;
            numericUpDown_DynOffset.Value = (decimal)mToolParam.mDynOffset;
            comboBox_DynMode.SelectedItem = mToolParam.mDynMode;

            if (mToolParam.mIsBlob)
            {
                checkBox_IsEnableBlob.Checked = true;
                groupBox10.Enabled = true;
            }
            else
            {
                checkBox_IsEnableBlob.Checked = false;
                groupBox10.Enabled = false;
            }
            comboBox_BlobMode.SelectedItem = mToolParam.mBlobMode;
            numericUpDown_BlobParam1.Value = (decimal)mToolParam.mBlobParam1;
            numericUpDown_BlobParam2.Value = (decimal)mToolParam.mBlobParam2;
            if (mToolParam.mBlobByMode == 0)
            {
                radioButton_BlobByCircle.Checked = true;
                radioButton_BlobByRec.Checked = false;
                numericUpDown_BlobParam1.Enabled = true;
                numericUpDown_BlobParam2.Enabled = false;
            }
            else
            {
                radioButton_BlobByCircle.Checked = false;
                radioButton_BlobByRec.Checked = true;
                numericUpDown_BlobParam1.Enabled = true;
                numericUpDown_BlobParam2.Enabled = true;
            }

            if (mToolParam.mIsTop) 
            {
                checkBox_IsEnableTop.Checked = true;
                groupBox11.Enabled = true;
            }
            else
            {
                checkBox_IsEnableTop.Checked = false;
                groupBox11.Enabled = false;
            }
            comboBox_TopMode.SelectedItem = mToolParam.mTopMode;
            numericUpDown_TopParam.Value = (decimal)mToolParam.mTopParam;
            if (mToolParam.mTopByMode == 0) 
            {
                radioButton_TopByCircle.Checked = true;
                radioButton_TopByRec.Checked = false;
            }
            else
            {
                radioButton_TopByCircle.Checked = false;
                radioButton_TopByRec.Checked = true;
            }
            if (mToolParam.mTopPartMode == 0) 
            {
                radioButton_TopPart1.Checked = true;
                radioButton_TopPart2.Checked = false;
            }
            else
            {
                radioButton_TopPart1.Checked = false;
                radioButton_TopPart2.Checked = true;
            }


            numericUpDown_WidthMin.Value = mToolParam.mWidthMin;
            numericUpDown_WidthMax.Value = mToolParam.mWidthMax;
            numericUpDown_HeightMin.Value = mToolParam.mHeightMin;
            numericUpDown_HeightMax.Value = mToolParam.mHeightMax;
            numericUpDown_AreaMin.Value = mToolParam.mAreaMin;
            numericUpDown_AreaMax.Value = mToolParam.mAreaMax;
            numericUpDown_SelectAreaMin.Value = mToolParam.mSelectAreaMin;
            numericUpDown_SelectAreaMax.Value = mToolParam.mSelectAreaMax;

            numericUpDown_Length1Min.Value = (decimal)mToolParam.mLength1Min;
            numericUpDown_Length1Max.Value = (decimal)mToolParam.mLength1Max;
            numericUpDown_Length2Min.Value = (decimal)mToolParam.mLength2Min;
            numericUpDown_Length2Max.Value = (decimal)mToolParam.mLength2Max;

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
                if (StepIndex < (i + 1))
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
                if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolResultType == ToolResultType.ImageAlignData)
                {
                    comboBox_PositioningStep.Items.Add((i + 1) + "_" + StepInfoList[i].mShowName);
                }

            }
            if (mToolParam.mShapeModelStep > 0) 
            {
                string str = mToolParam.mShapeModelStep + "_" + StepInfoList[mToolParam.mShapeModelStep - 1].mShowName;
                if (comboBox_PositioningStep.Items.Contains(str))
                {
                    comboBox_PositioningStep.SelectedItem = str;
                }
            }
            comboBox_RegionStep.Items.Clear();
            comboBox_RegionStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
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

        private void UToolBlob_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }

        private void GetParam() 
        {
            mToolParam.mThresMin = (int)numericUpDown_ThresMin.Value;
            mToolParam.mThresMax = (int)numericUpDown_ThresMax.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;

            mToolParam.mIsDynThres = checkBox_IsEnableDyn.Checked;
            mToolParam.mDynMark = (double)numericUpDown_DynMark.Value;
            mToolParam.mDynOffset = (double)numericUpDown_DynOffset.Value;
            mToolParam.mDynMode = (string)comboBox_DynMode.SelectedItem;
            if (mToolParam.mIsDynThres)
            {
                checkBox_IsEnableDyn.Checked = true;
                groupBox4.Enabled = true;
                numericUpDown_ThresMin.Enabled = false;
                numericUpDown_ThresMax.Enabled = false;
            }
            else
            {
                checkBox_IsEnableDyn.Checked = false;
                groupBox4.Enabled = false;
                numericUpDown_ThresMin.Enabled = true;
                numericUpDown_ThresMax.Enabled = true;
            }


            mToolParam.mIsBlob = checkBox_IsEnableBlob.Checked;
            mToolParam.mBlobMode = (string)comboBox_BlobMode.SelectedItem;
            mToolParam.mBlobParam1 = (double)numericUpDown_BlobParam1.Value;
            mToolParam.mBlobParam2 = (double)numericUpDown_BlobParam2.Value;
            if (radioButton_BlobByCircle.Checked)       
            {
                mToolParam.mBlobByMode = 0;
                numericUpDown_BlobParam1.Enabled = true;
                numericUpDown_BlobParam2.Enabled = false;
            }
            else 
            {
                mToolParam.mBlobByMode = 1;
                numericUpDown_BlobParam1.Enabled = true;
                numericUpDown_BlobParam2.Enabled = true;
            }
                
            if (mToolParam.mIsBlob)
            {
                checkBox_IsEnableBlob.Checked = true;
                groupBox10.Enabled = true;
            }
            else
            {
                checkBox_IsEnableBlob.Checked = false;
                groupBox10.Enabled = false;
            }


            mToolParam.mIsTop = checkBox_IsEnableTop.Checked;
            mToolParam.mTopMode = (string)comboBox_TopMode.SelectedItem;
            mToolParam.mTopParam = (double)numericUpDown_TopParam.Value;
            if (radioButton_TopByCircle.Checked)
                mToolParam.mTopByMode = 0;
            else 
                mToolParam.mTopByMode = 1;
                
            if (radioButton_TopPart1.Checked)
                mToolParam.mTopPartMode = 0;
            else
                mToolParam.mTopPartMode = 1;

            if (mToolParam.mIsTop)
            {
                checkBox_IsEnableTop.Checked = true;
                groupBox11.Enabled = true;
            }
            else
            {
                checkBox_IsEnableTop.Checked = false;
                groupBox11.Enabled = false;
            }

            mToolParam.mWidthMin = (int)numericUpDown_WidthMin.Value;
            mToolParam.mWidthMax = (int)numericUpDown_WidthMax.Value;
            mToolParam.mHeightMin = (int)numericUpDown_HeightMin.Value;
            mToolParam.mHeightMax = (int)numericUpDown_HeightMax.Value;
            mToolParam.mAreaMin = (int)numericUpDown_AreaMin.Value;
            mToolParam.mAreaMax = (int)numericUpDown_AreaMax.Value;
            mToolParam.mSelectAreaMin = (int)numericUpDown_SelectAreaMin.Value;
            mToolParam.mSelectAreaMax = (int)numericUpDown_SelectAreaMax.Value;

            mToolParam.mLength1Min = (double)numericUpDown_Length1Min.Value;
            mToolParam.mLength1Max = (double)numericUpDown_Length1Max.Value;
            mToolParam.mLength2Min = (double)numericUpDown_Length2Min.Value;
            mToolParam.mLength2Max = (double)numericUpDown_Length2Max.Value;

            if (radioButton_Max.Checked)
                mToolParam.mSelectMode = 0;
            else if (radioButton_Min.Checked)
                mToolParam.mSelectMode = 1;
            else
                mToolParam.mSelectMode = 2;

            if (radioButton_ConnectMode1.Checked)
                mToolParam.mConnectMode = 0;
            else
                mToolParam.mConnectMode = 1;
            if (radioButton_CheckMode1.Checked)
                mToolParam.mCheckMode = 0;
            else
                mToolParam.mCheckMode = 1;

        }

        private void comboBox_PositioningStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_PositioningStep.SelectedItem.ToString().Split('_');
            if (int.Parse(str[0]) > 0)
            {
                mToolParam.mShapeModelStep = int.Parse(str[0]);
                mToolParam.mShapeModelMark = StepInfoList[mToolParam.mShapeModelStep - 1].mInnerToolID;
            }
            else
            {
                mToolParam.mShapeModelStep = -1;
                mToolParam.mShapeModelMark = -1;
            }

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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
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
            HObject obj1;
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }
        private void trackBar_MarkSize_Scroll(object sender, EventArgs e)
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

        private void button_DeleRoi_Click(object sender, EventArgs e)
        {
            mToolParam.mDeleRoiDel();
            mToolMachine.ShowRegionList.Clear();
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
            int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void checkBox_IsEnableDyn_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_DynMark_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_DynOffset_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_DynMode_SelectedIndexChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void checkBox_IsEnableBlob_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_BlobParam1_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_BlobParam2_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_BlobByCircle_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_BlobByRec_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void checkBox_IsEnableTop_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;

        }

        private void comboBox_TopMode_SelectedIndexChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_TopParam_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_TopByCircle_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;

        }

        private void radioButton_TopByRec_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_TopPart1_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void radioButton_TopPart2_CheckedChanged(object sender, EventArgs e)
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
            int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_WidthMin_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_HeightMin_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_AreaMin_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_WidthMax_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_HeightMax_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_AreaMax_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            mToolMachine.ShowRegionList.Clear();
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            mToolMachine.ShowRegionList.Clear();
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
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

        }

        private void NumericUpDown_RegionMoveX_ValueChanged(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                int _x = (int)numericUpDown_RegionMoveX.Value;
                int _y = (int)numericUpDown_RegionMoveY.Value;
                double _s = (double)numericUpDown_ScaleValue.Value;
                double _r = (double)numericUpDown_RotateValue.Value;              
                HObject obj;
                mToolParam.mAffineRegionDel(_x, _y, _s, _r, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);

                mToolMachine.DrawWind.ClearWindow();
                mToolMachine.DrawWind.SetColor("magenta");
                mToolMachine.DrawWind.SetDraw("margin");
                mToolMachine.DrawWind.DispObj(obj);
            }
        }

        private void NumericUpDown_RegionMoveY_ValueChanged(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                int _x = (int)numericUpDown_RegionMoveX.Value;
                int _y = (int)numericUpDown_RegionMoveY.Value;
                double _s = (double)numericUpDown_ScaleValue.Value;
                double _r = (double)numericUpDown_RotateValue.Value;
                HObject obj;
                mToolParam.mAffineRegionDel(_x, _y, _s, _r, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);

                mToolMachine.DrawWind.ClearWindow();
                mToolMachine.DrawWind.SetColor("magenta");
                mToolMachine.DrawWind.SetDraw("margin");
                mToolMachine.DrawWind.DispObj(obj);
            }
        }

        private void NumericUpDown_ScaleValue_ValueChanged(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                int _x = (int)numericUpDown_RegionMoveX.Value;
                int _y = (int)numericUpDown_RegionMoveY.Value;
                double _s = (double)numericUpDown_ScaleValue.Value;
                double _r = (double)numericUpDown_RotateValue.Value;
                HObject obj;
                mToolParam.mAffineRegionDel(_x, _y, _s, _r, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);

                mToolMachine.DrawWind.ClearWindow();
                mToolMachine.DrawWind.SetColor("magenta");
                mToolMachine.DrawWind.SetDraw("margin");
                mToolMachine.DrawWind.DispObj(obj);
            }
        }

        private void NumericUpDown_RotateValue_ValueChanged(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                int _x = (int)numericUpDown_RegionMoveX.Value;
                int _y = (int)numericUpDown_RegionMoveY.Value;
                double _s = (double)numericUpDown_ScaleValue.Value;
                double _r = (double)numericUpDown_RotateValue.Value;
                HObject obj;
                mToolParam.mAffineRegionDel(_x, _y, _s, _r, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);

                mToolMachine.DrawWind.ClearWindow();
                mToolMachine.DrawWind.SetColor("magenta");
                mToolMachine.DrawWind.SetDraw("margin");
                mToolMachine.DrawWind.DispObj(obj);
            }
        }

        private void button_SureChange_Click(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                mIsInit = false;
                mToolParam.mSureAffineDel();
                numericUpDown_RegionMoveX.Value = 0;
                numericUpDown_RegionMoveY.Value = 0;
                numericUpDown_ScaleValue.Value = 1;
                numericUpDown_RotateValue.Value = 0;
                mIsInit = true;
            }
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void NumericUpDown_Length1Min_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void NumericUpDown_Length1Max_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void NumericUpDown_Length2Min_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void NumericUpDown_Length2Max_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(obj1);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }
    }
}
