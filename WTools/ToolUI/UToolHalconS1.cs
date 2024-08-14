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
    public partial class UToolHalconS1 : UserControl,InterfaceUIBase
    {
        public UToolHalconS1()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolHalconS1Param mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        int mLabelIndex;
        DataTable info = new DataTable();

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolHalconS1Param; 
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
        public int LabelIndex 
        { 
            get => mLabelIndex;
            set => mLabelIndex = value; 
        }

        public ResStatus InitRoiShow()
        {
            comboBox_PositioningStep.Items.Clear();
            comboBox_PositioningStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
               if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolType == ToolType.ShapeModle || StepInfoList[i].mToolType == ToolType.NccShapeModel)
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
            comboBox_ImageStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
               if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolType == ToolType.ScaleImage
                    || StepInfoList[i].mToolType == ToolType.DecomposeRGB 
                    || StepInfoList[i].mToolType == ToolType.CropImage
                    || StepInfoList[i].mToolType == ToolType.CorrectImage)
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
                            mToolParam.ImageSourceStep = int.Parse(str1[0]);
                        }
                        else
                        {
                            comboBox_ImageStep.Text = "";
                            mToolParam.ImageSourceStep = -1;
                            mToolParam.mImageSourceMark = -1;
                        }
                    }
                }
                if (!mIsfind)
                {
                    comboBox_ImageStep.Text = "";
                    mToolParam.ImageSourceStep = -1;
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
                if (mToolParam.mS1ParamList.Count > 0)
                {
                    for (int i = 0; i < mToolParam.mS1ParamList.Count; i++)
                    {
                        DataRow row = info.NewRow(); ;
                        row[0] = i + 1;
                        row[1] = mToolParam.mS1ParamList[i].ClassName;
                        info.Rows.Add(row);
                    }
                    if (info.Rows.Count > 0)
                    {
                        dataGridView_LineList.CurrentCell = dataGridView_LineList.Rows[0].Cells[0];
                    }
                }
                MessageBox.Show("加载模型成功");
            }
            else
            {
                info.Rows.Clear();
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
            numericUpDown_BatchSize.Value = mToolParam.mBatchSize;

            groupBox_SelectAI.Visible = false;
            comboBox_PositioningStep.Items.Clear();
            comboBox_PositioningStep.Items.Add("0_无输入");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolType == ToolType.ShapeModle || StepInfoList[i].mToolType == ToolType.NccShapeModel)
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

                if (StepInfoList[i].mToolType == ToolType.ScaleImage
                    || StepInfoList[i].mToolType == ToolType.DecomposeRGB
                    || StepInfoList[i].mToolType == ToolType.CropImage
                    || StepInfoList[i].mToolType == ToolType.CorrectImage)
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

            info.Columns.Add("序号", typeof(int));
            info.Columns.Add("标签名称", typeof(string));
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
            if (mToolParam.mS1ParamList.Count > 0) 
            {
                for (int i = 0; i < mToolParam.mS1ParamList.Count; i++)
                {
                    DataRow row = info.NewRow(); ;
                    row[0] = i + 1;
                    row[1] = mToolParam.mS1ParamList[i].ClassName;
                    info.Rows.Add(row);
                }
            }

        }

        private void UToolHalconS1_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }

        private void GetParam() 
        {
            mToolParam.mBatchSize = (int)numericUpDown_BatchSize.Value;
            mToolParam.mGpuID = (int)numericUpDown_GpuID.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
            int a = LabelIndex;
            if (a > -1)
            {
                mIsInit = false;
                groupBox_SelectAI.Visible = true;
                mToolParam.mS1ParamList[a].AllAreaMax = (int)numericUpDown_AllAreaMax.Value;
                mToolParam.mS1ParamList[a].HeightMin = (int)numericUpDown_HeightMin.Value;
                mToolParam.mS1ParamList[a].HeightMax = (int)numericUpDown_HeightMax.Value;
                mToolParam.mS1ParamList[a].WidthMin = (int)numericUpDown_WidthMin.Value;
                mToolParam.mS1ParamList[a].WidthMax = (int)numericUpDown_WidthMax.Value;
                mToolParam.mS1ParamList[a].Logic = comboBox_Logic.Text;
                mToolParam.mS1ParamList[a].FiltArea = (int)numericUpDown_FiltArea.Value;
                mToolParam.mS1ParamList[a].MaxRegionArea = (int)numericUpDown_MaxRegionArea.Value;
                mToolParam.mS1ParamList[a].MaxNum = (int)numericUpDown_MaxNum.Value;
                mToolParam.mS1ParamList[a].AllAreaMax = (int)numericUpDown_AllAreaMax.Value;
                mToolParam.mS1ParamList[a].MaxWidth = (int)numericUpDown_MaxWidth.Value;
                mToolParam.mS1ParamList[a].MaxHeight = (int)numericUpDown_MaxHeight.Value;
                mIsInit = true;
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
            if (LabelIndex > -1) 
            {
                mToolMachine.mEnableControlDel(false);
                HObject obj;
                mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, LabelIndex, 0, mType, mSize, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
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
            textBox_MarkSize.Text = trackBar_MarkSize.Value.ToString();
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

        private void button_DeleRoi_Click_1(object sender, EventArgs e)
        {
            mIsInit = false;
            int a = LabelIndex;
            mToolParam.mS1ParamList[a].ClassRegion.GenEmptyObj();
            mIsInit = true;
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (mToolMachine.ToolCurrImage != null)
            {
                if (LabelIndex > -1)
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
                    mToolParam.mDrawRoiDel(mToolMachine.ToolCurrImage, LabelIndex, mode, mType, mSize, out obj);
                    mToolMachine.ShowRegionList.Clear();
                    mToolMachine.ShowRegionList.Add(obj);
                    mToolMachine.mEnableControlDel(true);
                }
                else
                    MessageBox.Show("请选择一个标签");
            }
        }

        private void button_DrawRoi2_Click_1(object sender, EventArgs e)
        {
            if (LabelIndex > -1) 
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
                mToolParam.mDrawRoi2Del(mToolMachine.ToolCurrImage, LabelIndex, mType, out obj);
                mToolMachine.ShowRegionList.Clear();
                mToolMachine.ShowRegionList.Add(obj);
                mToolMachine.mEnableControlDel(true);
            }
            else
                MessageBox.Show("请选择一个标签");

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

        private void numericUpDown_MaxRegionArea_ValueChanged(object sender, EventArgs e)
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

        private void numericUpDown_MaxNum_ValueChanged(object sender, EventArgs e)
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

        private void comboBox_Logic_SelectedIndexChanged(object sender, EventArgs e)
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

        private void dataGridView_LineList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_LineList.Rows.Count > 0 && dataGridView_LineList.CurrentRow != null)
            {
                LabelIndex = dataGridView_LineList.CurrentRow.Index;
                if (LabelIndex < 0) return;
                groupBox_SelectAI.Visible = true;
                mIsInit = false;
                int a = LabelIndex;

                numericUpDown_AllAreaMax.Value = mToolParam.mS1ParamList[a].AllAreaMax;
                numericUpDown_MaxNum.Value = mToolParam.mS1ParamList[a].MaxNum;
                numericUpDown_MaxRegionArea.Value = mToolParam.mS1ParamList[a].MaxRegionArea;
                numericUpDown_FiltArea.Value = mToolParam.mS1ParamList[a].FiltArea;
                comboBox_Logic.Text = mToolParam.mS1ParamList[a].Logic;
                numericUpDown_WidthMax.Value = mToolParam.mS1ParamList[a].WidthMax;
                numericUpDown_WidthMin.Value = mToolParam.mS1ParamList[a].WidthMin;
                numericUpDown_HeightMax.Value = mToolParam.mS1ParamList[a].HeightMax;
                numericUpDown_HeightMin.Value = mToolParam.mS1ParamList[a].HeightMin;
                numericUpDown_AllAreaMax.Value = mToolParam.mS1ParamList[a].AllAreaMax;
                numericUpDown_MaxHeight.Value= mToolParam.mS1ParamList[a].MaxHeight;
                numericUpDown_MaxWidth.Value = mToolParam.mS1ParamList[a].MaxWidth;

                mIsInit = true;
                mToolMachine.DrawWind.ClearWindow();

                if (mToolMachine.ToolCurrImage == null)
                {
                    textBox_Mes.Text = "请先加载一张图片";
                    return;
                }
                mToolParam.mCheckAiDel(mToolMachine.ToolCurrImage, StepInfoList);
                mToolMachine.DrawWind.SetLineWidth(2);
                mToolMachine.DrawWind.SetDraw("margin");
                mToolMachine.DrawWind.SetColor(ShowColors.Colors[a]);
                mToolMachine.DrawWind.DispObj(mToolParam.mS1ParamList[a].ClassRegion);
            }
        }

        private void dataGridView_LineList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < info.Rows.Count; i++)
            {
                mToolParam.mS1ParamList[i].ClassName = info.Rows[i].ItemArray[1].ToString();
            }
        }

        private void numericUpDown_MaxWidth_ValueChanged(object sender, EventArgs e)
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

        private void numericUpDown_MaxHeight_ValueChanged(object sender, EventArgs e)
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
    }
}
