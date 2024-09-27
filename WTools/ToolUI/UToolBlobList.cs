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
    public partial class UToolBlobList : UserControl,InterfaceUIBase
    {
        public UToolBlobList()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolBlobListParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        DataTable info = new DataTable();
        //标签索引
        int mLabelIndex = -1;
        public int LabelIndex
        {
            get => mLabelIndex;
            set => mLabelIndex = value;
        }

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolBlobListParam; 
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
            mIsInit = false;
            //更新当前列表顺序
            comboBox_ImageStep.Items.Clear();
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= i + 1)
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
                if (StepIndex <= i + 1)
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
            mIsInit = true;
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

            comboBox_ImageStep.Items.Clear();
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= i + 1)
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
            else 
            {
                //赋默认值
                if (comboBox_ImageStep.Items.Count > 0) 
                {
                    comboBox_ImageStep.SelectedIndex = 0;
                    string[] str = comboBox_ImageStep.SelectedItem.ToString().Split('_');
                    mToolParam.mImageSourceStep = int.Parse(str[0]);
                    mToolParam.mImageSourceMark = StepInfoList[mToolParam.mImageSourceStep - 1].mInnerToolID;
                }
            }

            comboBox_PositioningStep.Items.Clear();
            comboBox_PositioningStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex <= i + 1)
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
        }

        private void UToolBlobList_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();

            info.Columns.Add("序号", typeof(int));
            info.Columns.Add("检测位置", typeof(string));
            dataGridView_LineList.DataSource = info;
            dataGridView_LineList.AllowUserToAddRows = false;//禁止添加行
            dataGridView_LineList.AllowUserToDeleteRows = false;//禁止删除行
            dataGridView_LineList.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Regular);
            dataGridView_LineList.RowsDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Regular);
            dataGridView_LineList.RowTemplate.Height = 14;
            //设置首行不可修改
            dataGridView_LineList.Columns[0].ReadOnly = true;
            //设置所有列不可排序
            dataGridView_LineList.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView_LineList.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            if (mToolParam.mBlobRegionList.Count > 0)
            {
                for (int i = 0; i < mToolParam.mBlobRegionList.Count; i++)
                {
                    DataRow row = info.NewRow();
                    row[0] = i + 1;
                    row[1] = mToolParam.mBlobRegionList[i].mRegionName;
                    info.Rows.Add(row);
                }
            }
            else
            {
                tabPage_Param.Enabled = false;
            }
                    
            mIsInit = true;
        }

        private void GetParam() 
        {
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
            if (LabelIndex > -1)
            {
                mToolParam.mBlobRegionList[LabelIndex].mThresMin = (int)numericUpDown_ThresMin.Value;
                mToolParam.mBlobRegionList[LabelIndex].mThresMax = (int)numericUpDown_ThresMax.Value;
 

                mToolParam.mBlobRegionList[LabelIndex].mIsDynThres = checkBox_IsEnableDyn.Checked;
                mToolParam.mBlobRegionList[LabelIndex].mDynMark = (double)numericUpDown_DynMark.Value;
                mToolParam.mBlobRegionList[LabelIndex].mDynOffset = (double)numericUpDown_DynOffset.Value;
                mToolParam.mBlobRegionList[LabelIndex].mDynMode = (string)comboBox_DynMode.SelectedItem;
                if (mToolParam.mBlobRegionList[LabelIndex].mIsDynThres)
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


                mToolParam.mBlobRegionList[LabelIndex].mIsBlob = checkBox_IsEnableBlob.Checked;
                mToolParam.mBlobRegionList[LabelIndex].mBlobMode = (string)comboBox_BlobMode.SelectedItem;
                mToolParam.mBlobRegionList[LabelIndex].mBlobParam1 = (double)numericUpDown_BlobParam1.Value;
                mToolParam.mBlobRegionList[LabelIndex].mBlobParam2 = (double)numericUpDown_BlobParam2.Value;
                if (radioButton_BlobByCircle.Checked)
                {
                    mToolParam.mBlobRegionList[LabelIndex].mBlobByMode = 0;
                    numericUpDown_BlobParam1.Enabled = true;
                    numericUpDown_BlobParam2.Enabled = false;
                }
                else
                {
                    mToolParam.mBlobRegionList[LabelIndex].mBlobByMode = 1;
                    numericUpDown_BlobParam1.Enabled = true;
                    numericUpDown_BlobParam2.Enabled = true;
                }

                if (mToolParam.mBlobRegionList[LabelIndex].mIsBlob)
                {
                    checkBox_IsEnableBlob.Checked = true;
                    groupBox10.Enabled = true;
                }
                else
                {
                    checkBox_IsEnableBlob.Checked = false;
                    groupBox10.Enabled = false;
                }


                mToolParam.mBlobRegionList[LabelIndex].mIsTop = checkBox_IsEnableTop.Checked;
                mToolParam.mBlobRegionList[LabelIndex].mTopMode = (string)comboBox_TopMode.SelectedItem;
                mToolParam.mBlobRegionList[LabelIndex].mTopParam = (double)numericUpDown_TopParam.Value;
                if (radioButton_TopByCircle.Checked)
                    mToolParam.mBlobRegionList[LabelIndex].mTopByMode = 0;
                else
                    mToolParam.mBlobRegionList[LabelIndex].mTopByMode = 1;

                if (radioButton_TopPart1.Checked)
                    mToolParam.mBlobRegionList[LabelIndex].mTopPartMode = 0;
                else
                    mToolParam.mBlobRegionList[LabelIndex].mTopPartMode = 1;

                if (mToolParam.mBlobRegionList[LabelIndex].mIsTop)
                {
                    checkBox_IsEnableTop.Checked = true;
                    groupBox11.Enabled = true;
                }
                else
                {
                    checkBox_IsEnableTop.Checked = false;
                    groupBox11.Enabled = false;
                }

                mToolParam.mBlobRegionList[LabelIndex].mWidthMin = (int)numericUpDown_WidthMin.Value;
                mToolParam.mBlobRegionList[LabelIndex].mWidthMax = (int)numericUpDown_WidthMax.Value;
                mToolParam.mBlobRegionList[LabelIndex].mHeightMin = (int)numericUpDown_HeightMin.Value;
                mToolParam.mBlobRegionList[LabelIndex].mHeightMax = (int)numericUpDown_HeightMax.Value;
                mToolParam.mBlobRegionList[LabelIndex].mAreaMin = (int)numericUpDown_AreaMin.Value;
                mToolParam.mBlobRegionList[LabelIndex].mAreaMax = (int)numericUpDown_AreaMax.Value;
                mToolParam.mBlobRegionList[LabelIndex].mSelectAreaMin = (int)numericUpDown_SelectAreaMin.Value;
                mToolParam.mBlobRegionList[LabelIndex].mSelectAreaMax = (int)numericUpDown_SelectAreaMax.Value;

                mToolParam.mBlobRegionList[LabelIndex].mLength1Min = (double)numericUpDown_Length1Min.Value;
                mToolParam.mBlobRegionList[LabelIndex].mLength1Max = (double)numericUpDown_Length1Max.Value;
                mToolParam.mBlobRegionList[LabelIndex].mLength2Min = (double)numericUpDown_Length2Min.Value;
                mToolParam.mBlobRegionList[LabelIndex].mLength2Max = (double)numericUpDown_Length2Max.Value;

                if (radioButton_Max.Checked)
                    mToolParam.mBlobRegionList[LabelIndex].mSelectMode = 0;
                else if (radioButton_Min.Checked)
                    mToolParam.mBlobRegionList[LabelIndex].mSelectMode = 1;
                else
                    mToolParam.mBlobRegionList[LabelIndex].mSelectMode = 2;

                if (radioButton_ConnectMode1.Checked)
                    mToolParam.mBlobRegionList[LabelIndex].mConnectMode = 0;
                else
                    mToolParam.mBlobRegionList[LabelIndex].mConnectMode = 1;
                if (radioButton_CheckMode1.Checked)
                    mToolParam.mBlobRegionList[LabelIndex].mCheckMode = 0;
                else
                    mToolParam.mBlobRegionList[LabelIndex].mCheckMode = 1;
            }
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            mToolMachine.ShowRegionList.Clear();
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            mToolMachine.ShowRegionList.Clear();
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
            if (LabelIndex > -1)
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
                    mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, mode, mType, mSize, out obj, LabelIndex);
                    mToolMachine.ShowRegionList.Clear();
                    mToolMachine.ShowRegionList.Add(obj);
                    mToolMachine.mEnableControlDel(true);
                } 
            }
        }

        private void button_DrawRoi2_Click(object sender, EventArgs e)
        {
            if (LabelIndex > -1) 
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
                    mToolParam.mDrawRoi2Del(mToolMachine.ToolCurrImage, mType, out obj, LabelIndex);
                    mToolMachine.ShowRegionList.Clear();
                    mToolMachine.ShowRegionList.Add(obj);
                    mToolMachine.mEnableControlDel(true);
                } 
            }
        }

        private void button_DeleRoi_Click(object sender, EventArgs e)
        {
            if (LabelIndex > -1)
            {
                mToolParam.mDeleRoiDel(LabelIndex);
                mToolMachine.ShowRegionList.Clear(); 
            }
        }

        private void numericUpDown_ThresMin_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_ThresMin.Value >= numericUpDown_ThresMax.Value)
                numericUpDown_ThresMin.Value = numericUpDown_ThresMax.Value - 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1) 
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString; 
            }
        }

        private void numericUpDown_ThresMax_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_ThresMax.Value <= numericUpDown_ThresMin.Value)
                numericUpDown_ThresMax.Value = numericUpDown_ThresMin.Value + 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1) 
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString; 
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }

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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mRegionThresholdDel2(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void numericUpDown_WidthMin_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_HeightMin.Value >= numericUpDown_HeightMax.Value)
                numericUpDown_HeightMin.Value = numericUpDown_HeightMax.Value - 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString; 
            }
        }

        private void numericUpDown_HeightMin_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_WidthMin.Value >= numericUpDown_WidthMax.Value)
                numericUpDown_WidthMin.Value = numericUpDown_WidthMax.Value - 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void numericUpDown_AreaMin_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_AreaMin.Value >= numericUpDown_AreaMax.Value)
                numericUpDown_AreaMin.Value = numericUpDown_AreaMax.Value - 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void numericUpDown_WidthMax_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_HeightMax.Value <= numericUpDown_HeightMin.Value)
                numericUpDown_HeightMax.Value = numericUpDown_HeightMin.Value + 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void numericUpDown_HeightMax_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_WidthMax.Value <= numericUpDown_WidthMin.Value)
                numericUpDown_WidthMax.Value = numericUpDown_WidthMin.Value + 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void numericUpDown_AreaMax_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_AreaMax.Value <= numericUpDown_AreaMin.Value)
                numericUpDown_AreaMax.Value = numericUpDown_AreaMin.Value + 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
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
            if (LabelIndex > -1) 
            {
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
            if (LabelIndex > -1)
            {
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

        private void numericUpDown_SelectAreaMin_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_SelectAreaMin.Value >= numericUpDown_SelectAreaMax.Value)
                numericUpDown_SelectAreaMin.Value = numericUpDown_SelectAreaMax.Value - 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void numericUpDown_SelectAreaMax_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_SelectAreaMax.Value <= numericUpDown_SelectAreaMin.Value)
                numericUpDown_SelectAreaMax.Value = numericUpDown_SelectAreaMin.Value + 1;
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void NumericUpDown_Length1Min_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_Length1Min.Value >= numericUpDown_Length1Max.Value)
                numericUpDown_Length1Min.Value = (decimal)((double)numericUpDown_Length1Max.Value - 0.1);
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void NumericUpDown_Length1Max_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_Length1Max.Value <= numericUpDown_Length1Min.Value)
                numericUpDown_Length1Max.Value = (decimal)((double)numericUpDown_Length1Min.Value + 0.1);
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void NumericUpDown_Length2Min_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_Length2Min.Value >= numericUpDown_Length2Max.Value)
                numericUpDown_Length2Min.Value = (decimal)((double)numericUpDown_Length2Max.Value - 0.1);
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void NumericUpDown_Length2Max_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_Length2Max.Value <= numericUpDown_Length2Min.Value)
                numericUpDown_Length2Max.Value = (decimal)((double)numericUpDown_Length2Min.Value + 0.1);
            GetParam();
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (LabelIndex > -1)
            {
                HTuple s1, s2;
                HObject obj1;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mSelectShapeDel(mToolMachine.ToolCurrImage, StepInfoList, out obj1, LabelIndex);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj1);
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void dataGridView_LineList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_LineList.Rows.Count > 0 && dataGridView_LineList.CurrentRow != null)
            {
                LabelIndex = dataGridView_LineList.CurrentRow.Index;
                if (LabelIndex < 0)
                    return;
                mIsInit = false;
                int a = LabelIndex;

                numericUpDown_ThresMin.Value = mToolParam.mBlobRegionList[a].mThresMin;
                numericUpDown_ThresMax.Value = mToolParam.mBlobRegionList[a].mThresMax;

                checkBox_IsEnableDyn.Checked = mToolParam.mBlobRegionList[a].mIsDynThres;
                if (mToolParam.mBlobRegionList[a].mIsDynThres)
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
                numericUpDown_DynMark.Value = (decimal)mToolParam.mBlobRegionList[a].mDynMark;
                numericUpDown_DynOffset.Value = (decimal)mToolParam.mBlobRegionList[a].mDynOffset;
                comboBox_DynMode.SelectedItem = mToolParam.mBlobRegionList[a].mDynMode;

                if (mToolParam.mBlobRegionList[a].mIsBlob)
                {
                    checkBox_IsEnableBlob.Checked = true;
                    groupBox10.Enabled = true;
                }
                else
                {
                    checkBox_IsEnableBlob.Checked = false;
                    groupBox10.Enabled = false;
                }
                comboBox_BlobMode.SelectedItem = mToolParam.mBlobRegionList[a].mBlobMode;
                numericUpDown_BlobParam1.Value = (decimal)mToolParam.mBlobRegionList[a].mBlobParam1;
                numericUpDown_BlobParam2.Value = (decimal)mToolParam.mBlobRegionList[a].mBlobParam2;
                if (mToolParam.mBlobRegionList[a].mBlobByMode == 0)
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

                if (mToolParam.mBlobRegionList[a].mIsTop)
                {
                    checkBox_IsEnableTop.Checked = true;
                    groupBox11.Enabled = true;
                }
                else
                {
                    checkBox_IsEnableTop.Checked = false;
                    groupBox11.Enabled = false;
                }
                comboBox_TopMode.SelectedItem = mToolParam.mBlobRegionList[a].mTopMode;
                numericUpDown_TopParam.Value = (decimal)mToolParam.mBlobRegionList[a].mTopParam;
                if (mToolParam.mBlobRegionList[a].mTopByMode == 0)
                {
                    radioButton_TopByCircle.Checked = true;
                    radioButton_TopByRec.Checked = false;
                }
                else
                {
                    radioButton_TopByCircle.Checked = false;
                    radioButton_TopByRec.Checked = true;
                }
                if (mToolParam.mBlobRegionList[a].mTopPartMode == 0)
                {
                    radioButton_TopPart1.Checked = true;
                    radioButton_TopPart2.Checked = false;
                }
                else
                {
                    radioButton_TopPart1.Checked = false;
                    radioButton_TopPart2.Checked = true;
                }


                numericUpDown_WidthMin.Value = mToolParam.mBlobRegionList[a].mWidthMin;
                numericUpDown_WidthMax.Value = mToolParam.mBlobRegionList[a].mWidthMax;
                numericUpDown_HeightMin.Value = mToolParam.mBlobRegionList[a].mHeightMin;
                numericUpDown_HeightMax.Value = mToolParam.mBlobRegionList[a].mHeightMax;
                numericUpDown_AreaMin.Value = mToolParam.mBlobRegionList[a].mAreaMin;
                numericUpDown_AreaMax.Value = mToolParam.mBlobRegionList[a].mAreaMax;
                numericUpDown_SelectAreaMin.Value = mToolParam.mBlobRegionList[a].mSelectAreaMin;
                numericUpDown_SelectAreaMax.Value = mToolParam.mBlobRegionList[a].mSelectAreaMax;

                numericUpDown_Length1Min.Value = (decimal)mToolParam.mBlobRegionList[a].mLength1Min;
                numericUpDown_Length1Max.Value = (decimal)mToolParam.mBlobRegionList[a].mLength1Max;
                numericUpDown_Length2Min.Value = (decimal)mToolParam.mBlobRegionList[a].mLength2Min;
                numericUpDown_Length2Max.Value = (decimal)mToolParam.mBlobRegionList[a].mLength2Max;

                if (mToolParam.mBlobRegionList[a].mSelectMode == 0)
                    radioButton_Max.Checked = true;
                else if (mToolParam.mBlobRegionList[a].mSelectMode == 1)
                    radioButton_Min.Checked = true;
                else
                    radioButton_All.Checked = true;
                if (mToolParam.mBlobRegionList[a].mConnectMode == 0)
                    radioButton_ConnectMode1.Checked = true;
                else
                    radioButton_ConnectMode2.Checked = true;
                if (mToolParam.mBlobRegionList[a].mCheckMode == 0)
                    radioButton_CheckMode1.Checked = true;
                else
                    radioButton_CheckMode2.Checked = true;

                mIsInit = true;
                mToolMachine.DrawWind.ClearWindow();

                if (mToolMachine.ToolCurrImage == null)
                {
                    textBox_Mes.Text = "请先加载一张图片";
                    return;
                }
                mToolMachine.DrawWind.SetLineWidth(2);
                mToolMachine.DrawWind.SetDraw("margin");
                mToolMachine.DrawWind.SetColor(ShowColors.Colors[a]);
                mToolMachine.DrawWind.DispObj(mToolParam.mBlobRegionList[a].CheckRegion);
            }
        }

        private void dataGridView_LineList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < info.Rows.Count; i++)
            {
                mToolParam.mBlobRegionList[i].mRegionName = info.Rows[i].ItemArray[1].ToString();
            }
        }

        private void button_AddRecognitionRegion_Click(object sender, EventArgs e)
        {
            BlobParamValue param = new BlobParamValue();
            param.mRegionName = "识别区域" + (mToolParam.mBlobRegionList.Count + 1);
            mToolParam.mBlobRegionList.Add(param);

            DataRow row = info.NewRow();
            row[0] = mToolParam.mBlobRegionList.Count;
            row[1] = param.mRegionName;
            info.Rows.Add(row);

            if (mToolParam.mBlobRegionList.Count > 0)
                tabPage_Param.Enabled = true;
            else 
                tabPage_Param.Enabled = false;         
        }

        private void button_DeleIndex_Click(object sender, EventArgs e)
        {
            mIsInit = false;
            int a = LabelIndex;
            if (a > -1)
            {
                mToolParam.mBlobRegionList.RemoveAt(a);
                info.Rows.Clear();
                if (mToolParam.mBlobRegionList.Count > 0)
                {
                    for (int i = 0; i < mToolParam.mBlobRegionList.Count; i++)
                    {
                        DataRow row = info.NewRow();
                        row[0] = i + 1;
                        row[1] = mToolParam.mBlobRegionList[i].mRegionName;
                        info.Rows.Add(row);
                    }
                }
            }
            if (mToolParam.mBlobRegionList.Count > 0)
                tabPage_Param.Enabled = true;
            else
                tabPage_Param.Enabled = false;
            mIsInit = true;
        }
    }
}
