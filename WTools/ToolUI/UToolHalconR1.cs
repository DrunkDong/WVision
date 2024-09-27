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
    public partial class UToolHalconR1 : UserControl,InterfaceUIBase
    {
        public UToolHalconR1()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolHalconR1Param mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolHalconR1Param; 
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

        private void button_SelectModelPath_Click(object sender, EventArgs e)
        {
            string nPath = "";
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Title = "打开文件";
            openfiledialog1.Filter= "(*.hdl)|*.hdl";
            openfiledialog1.InitialDirectory = "C:\\Program Files\\Common Files\\CloudKeeper\\Model";
            if (openfiledialog1.ShowDialog() == DialogResult.OK)
            {
                nPath = openfiledialog1.FileName;
            }
            if (nPath != "") 
            {
                textBox_VidiPath.Text = nPath;
            }

        }

        private void button_LoadModel_Click(object sender, EventArgs e)
        {
            if (mToolParam.mAiModelPath == textBox_VidiPath.Text)
            {
                MessageBox.Show("模型已加载");
                return;
            }
            mToolParam.mAiModelPath = textBox_VidiPath.Text;
            ResStatus res= mToolParam.mInitAiModelDe();
            if (res == ResStatus.OK)
            {
                UpdataListView();
                MessageBox.Show("加载模型成功");
            }
            else
            {
                MessageBox.Show("加载模型失败"); 
            }
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString+"\r\n"+"当前耗时："+((s2.D-s1.D)*1000).ToString("00.00")+" ms";
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

            textBox_VidiPath.Text = mToolParam.mAiModelPath;
            numericUpDown_SingleOverlap.Value = (decimal)mToolParam.mMaxOverlap;
            numericUpDown_MaxNums.Value = mToolParam.mMaxDetectNum;
            numericUpDown_MulitOverlap.Value = (decimal)mToolParam.mMaxOverlapClass;
            numericUpDown_BatchSize.Value = mToolParam.mBatchSize;
            numericUpDown_GpuID.Value = mToolParam.mGpuID;

            UpdataListView();
            groupBox_SelectAI.Visible = false;

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
                else
                {
                    mToolParam.mShapeModelStep = -1;
                    mToolParam.mShapeModelMark = -1;
                }
            }

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
            if (mToolParam.mImageSourceStep > 0)
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

        private void UToolHalconR1_Load(object sender, EventArgs e)
        {
            mIsInit = false; 
            listView_AiView.View = View.Details; // 设置详细视图      
            listView_AiView.Columns.Add("标签号", 60, HorizontalAlignment.Left);  // 列的添加
            listView_AiView.Columns.Add("标签名称", 100, HorizontalAlignment.Left);
            InitParam();
            mIsInit = true;
        }

        private void GetParam() 
        {
            mToolParam.mMaxOverlap = (double)numericUpDown_SingleOverlap.Value;
            mToolParam.mMaxDetectNum = (int)numericUpDown_MaxNums.Value;
            mToolParam.mMaxOverlapClass = (double)numericUpDown_MulitOverlap.Value;
            mToolParam.mBatchSize = (int)numericUpDown_BatchSize.Value;
            mToolParam.mGpuID = (int)numericUpDown_GpuID.Value;

            mToolParam.NgReturnValue  = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;

            if (listView_AiView.SelectedIndices.Count > 0)
            {
                int a = listView_AiView.SelectedIndices[0];
                mToolParam.R1ParamList[a].AreaMin = (int)numericUpDown_AreaMin.Value;
                mToolParam.R1ParamList[a].AreaMax = (int)numericUpDown_AreaMax.Value;
                mToolParam.R1ParamList[a].HeightMin = (int)numericUpDown_HeightMin.Value;
                mToolParam.R1ParamList[a].HeightMax = (int)numericUpDown_HeightMax.Value;
                mToolParam.R1ParamList[a].WidthMin = (int)numericUpDown_WidthMin.Value;
                mToolParam.R1ParamList[a].WidthMax = (int)numericUpDown_WidthMax.Value;
                mToolParam.R1ParamList[a].AreaSingleMin = (int)numericUpDown_SingAreaMin.Value;
                mToolParam.R1ParamList[a].AreaSingleMax = (int)numericUpDown_SingAreaMax.Value;
                mToolParam.R1ParamList[a].Score = (double)numericUpDown_Score.Value;
            }
        }

        private void numericUpDown_SingleOverlap_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_MaxNums_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }


        private void numericUpDown_MulitOverlap_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_BatchSize_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_DrawROI_Click(object sender, EventArgs e)
        {
            string mType;
            if (radioButton_Circle.Checked)
                mType = "circle";
            else
                mType = "rectangle1";
            int mSize = trackBar_MarkSize.Value;
            if (listView_AiView.SelectedIndices[0] >= 0) 
            {
                mToolMachine.mEnableControlDel(false);
                HObject obj;
                mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, listView_AiView.SelectedIndices[0], 0, mType, mSize, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }

        }

        private void button_DrawRoi2_Click(object sender, EventArgs e)
        {

        }

        private void button_RidRoi_Click(object sender, EventArgs e)
        {
            string mType;
            if (radioButton_Circle.Checked)
                mType = "circle";
            else
                mType = "rectangle1";
            int mSize = trackBar_MarkSize.Value;
            if (listView_AiView.SelectedIndices[0] >= 0) 
            {
                mToolMachine.mEnableControlDel(false);
                HObject obj;
                mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, listView_AiView.SelectedIndices[0], 1, mType, mSize, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }
        }

        private void button_DeleRoi_Click(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0)
            {
                if (listView_AiView.SelectedIndices[0] >= 0)
                {
                    mToolParam.mDeleRoiDel(listView_AiView.SelectedIndices[0]);
                }
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

        private void trackBar_MarkSize_Scroll(object sender, EventArgs e)
        {

        }


        private void UpdataListView()
        {
            listView_AiView.Items.Clear();
            listView_AiView.BeginUpdate();
            for (int i = 0; i < mToolParam.R1ParamList.Count; i++)
            {
                ListViewItem li = new ListViewItem();
                li.Text = i.ToString(); //序号
                li.SubItems.Add("'" + mToolParam.R1ParamList[i].ClassName + "'"); //名称
                listView_AiView.Items.Add(li);
            }
            listView_AiView.EndUpdate();
        }

        private void textBox_VidiPath_TextChanged(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0)
            {
                mIsInit = false;
                int a = listView_AiView.SelectedIndices[0];
                mToolParam.R1ParamList[a].AreaMin = (int)numericUpDown_AreaMin.Value;
                mToolParam.R1ParamList[a].AreaMax = (int)numericUpDown_AreaMax.Value;
                mToolParam.R1ParamList[a].HeightMin = (int)numericUpDown_HeightMin.Value;
                mToolParam.R1ParamList[a].HeightMax = (int)numericUpDown_HeightMax.Value;
                mToolParam.R1ParamList[a].WidthMin = (int)numericUpDown_WidthMin.Value;
                mToolParam.R1ParamList[a].WidthMax = (int)numericUpDown_WidthMax.Value;
                mToolParam.R1ParamList[a].AreaSingleMin = (int)numericUpDown_SingAreaMin.Value;
                mToolParam.R1ParamList[a].AreaSingleMax = (int)numericUpDown_SingAreaMax.Value;
                mIsInit = true;
            }
        }

        private void comboBox_ImageStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            GetParam();
            string[] str = comboBox_ImageStep.SelectedItem.ToString().Split('_');
            if (int.Parse(str[0]) > 0)
            {
                mToolParam.mImageSourceStep = int.Parse(str[0]);
                mToolParam.mImageSourceMark = StepInfoList[mToolParam.mImageSourceStep - 1].mInnerToolID;
            }
            else
            {
                mToolParam.mImageSourceStep = -1;
                mToolParam.mImageSourceMark = -1;
            }
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_Score_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_SingAreaMin_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_SingAreaMax_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
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
            HOperatorSet.CountSeconds(out s1);
            int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void listView_AiView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0)
            {
                groupBox_SelectAI.Visible = true;
                mIsInit = false;
                int a = listView_AiView.SelectedIndices[0];
                numericUpDown_AreaMin.Value = mToolParam.R1ParamList[a].AreaMin;
                numericUpDown_AreaMax.Value = mToolParam.R1ParamList[a].AreaMax;
                numericUpDown_HeightMin.Value = mToolParam.R1ParamList[a].HeightMin;
                numericUpDown_HeightMax.Value = mToolParam.R1ParamList[a].HeightMax;
                numericUpDown_WidthMin.Value = mToolParam.R1ParamList[a].WidthMin;
                numericUpDown_WidthMax.Value = mToolParam.R1ParamList[a].WidthMax;
                numericUpDown_SingAreaMin.Value = mToolParam.R1ParamList[a].AreaSingleMin;
                numericUpDown_SingAreaMax.Value = mToolParam.R1ParamList[a].AreaSingleMax;
                numericUpDown_Score.Value = (decimal)mToolParam.R1ParamList[a].Score;
                mIsInit = true;

                mToolMachine.DrawWind.ClearWindow();
                if (mToolMachine.ToolCurrImage == null)
                {
                    textBox_Mes.Text = "请先加载一张图片";
                    return;
                }

                HTuple s1, s2;
                HOperatorSet.CountSeconds(out s1);
                int res = mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
                if (res == 0)
                {
                    mToolMachine.DrawWind.SetDraw("margin");
                    mToolMachine.DrawWind.SetColor(ShowColors.Colors[a]);
                    mToolMachine.DrawWind.DispObj(mToolParam.R1ParamList[a].ClassRegion);
                }
                HOperatorSet.CountSeconds(out s2);
                textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
                mToolMachine.mChangeState(StepIndex, res);
                mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
                textBox_Mes.Text = mToolParam.ResultString;
            }
        }

        private void button_ChangeName_Click(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0) 
            {
                int a = listView_AiView.SelectedIndices[0];
                mToolParam.R1ParamList[a].ClassName = textBox_ClassName.Text;
                textBox_ClassName.Text = null;
                UpdataListView();
            }            
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0)
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
                    mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, listView_AiView.SelectedIndices[0], mode, mType, mSize, out obj);
                    mToolMachine.ShowRegionList.Clear();
                    mToolMachine.ShowRegionList.Add(obj);
                    mToolMachine.mEnableControlDel(true);
                }
            }
        }

        private void button_DrawRoi2_Click_1(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0) 
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
                mToolParam.mDrawRoi2Del(mToolMachine.ToolCurrImage, listView_AiView.SelectedIndices[0], mType, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }
        }

        private void button_DeleRoi_Click_1(object sender, EventArgs e)
        {
            if (listView_AiView.SelectedIndices.Count > 0)
            {
                mIsInit = false;
                int a = listView_AiView.SelectedIndices[0];
                mToolParam.R1ParamList[a].ClassRegion.GenEmptyObj();
            }
        }
    }
}
