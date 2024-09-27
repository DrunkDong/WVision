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
    public partial class UToolHalconClassify : UserControl,InterfaceUIBase
    {
        public UToolHalconClassify()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolHalconClassifyParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        int mLabelIndex;
        DataTable info = new DataTable();
        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolHalconClassifyParam; 
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
                if (mToolParam.mClassifyNameList.Count > 0)
                {
                    for (int i = 0; i < mToolParam.mClassifyNameList.Count; i++)
                    {
                        DataRow row = info.NewRow(); ;
                        row[0] = i + 1;
                        row[1] = mToolParam.mClassifyNameList[i];
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
            if (mToolParam.mClassifyNameList.Count > 0) 
            {
                for (int i = 0; i < mToolParam.mClassifyNameList.Count; i++)
                {
                    DataRow row = info.NewRow(); ;
                    row[0] = i + 1;
                    row[1] = mToolParam.mClassifyNameList[i];
                    info.Rows.Add(row);
                }
            }

        }

        private void UToolHalconClassify_Load(object sender, EventArgs e)
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

        private void dataGridView_LineList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < info.Rows.Count; i++)
            {
                mToolParam.mClassifyNameList[i] = info.Rows[i].ItemArray[1].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mToolParam.mResultClassName = textBox_ResultName.Text;
        }
    }
}
