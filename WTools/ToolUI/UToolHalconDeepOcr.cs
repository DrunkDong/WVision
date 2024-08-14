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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WTools
{
    [ToolboxItem(false)]
    public partial class UToolHalconDeepOcr : UserControl,InterfaceUIBase
    {
        public UToolHalconDeepOcr()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolHalconDeepOcrParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;
        int mLabelIndex=-1;
        DataTable info = new DataTable();
        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolHalconDeepOcrParam; 
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
            openfiledialog1.Title = "打开文件|*.hdl;*.hdo;";
            openfiledialog1.Filter = "(*.hdl;*.hdo)|*.hdl;*.hdo";
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
                MessageBox.Show("加载模型成功");
            else
                MessageBox.Show("加载模型失败"); 
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
        }

        private void UToolDeepOcr_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            InitCharTreeView();

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
            if (mToolParam.CheckRegionList.Count > 0)
            {
                for (int i = 0; i < mToolParam.CheckRegionList.Count; i++)
                {
                    DataRow row = info.NewRow(); 
                    row[0] = i + 1;
                    row[1] = mToolParam.CheckRegionList[i].RegionName;
                    info.Rows.Add(row);
                }
            }
            mIsInit = true;
        }

        private void GetParam() 
        {
            mToolParam.mBatchSize = (int)numericUpDown_BatchSize.Value;
            mToolParam.mGpuID = (int)numericUpDown_GpuID.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
            if (LabelIndex > -1)
            {
                mIsInit = false;
                mToolParam.CheckRegionList[LabelIndex].CharMinScore = (double)numericUpDown_CharMinScore.Value;
                mToolParam.CheckRegionList[LabelIndex].ResultText = textBox_RecognitionText.Text;
                mIsInit = true;
            }
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

        private void InitCharTreeView() 
        {
            for (int i = 0; i < mToolParam.hv_OnlyNumber.Count(); i++)
            {
                treeView1.Nodes.Add(new TreeNode(" " + mToolParam.hv_OnlyNumber[i]));
            }
            for (int i = 0; i < mToolParam.hv_OnlyLowerCaseLetter.Count(); i++)
            {
                treeView2.Nodes.Add(new TreeNode(" " + mToolParam.hv_OnlyLowerCaseLetter[i]));
            }
            for (int i = 0; i < mToolParam.hv_OnlyUpperCaseLetter.Count(); i++)
            {
                treeView3.Nodes.Add(new TreeNode(" " + mToolParam.hv_OnlyUpperCaseLetter[i]));
            }

            for (int i = 0; i < mToolParam.NumberList.Count(); i++) 
            {
                foreach (TreeNode item in treeView1.Nodes)
                {
                    if (item.Text.Trim() == mToolParam.NumberList[i]) 
                    {
                        item.Checked = true;
                        //选择节点并滚动
                        treeView1.SelectedNode = item;
                        treeView1.Select();
                    }
                }
            }
            for (int i = 0; i < mToolParam.LowerCaseLetterList.Count(); i++)
            {
                foreach (TreeNode item in treeView2.Nodes)
                {
                    if (item.Text.Trim() == mToolParam.LowerCaseLetterList[i])
                    {
                        item.Checked = true;
                        //选择节点并滚动
                        treeView2.SelectedNode = item;
                        treeView2.Select();
                    }
                }
            }
            for (int i = 0; i < mToolParam.UpperCaseLetterList.Count(); i++)
            {
                foreach (TreeNode item in treeView3.Nodes)
                {
                    if (item.Text.Trim() == mToolParam.UpperCaseLetterList[i])
                    {
                        item.Checked = true;
                        //选择节点并滚动
                        treeView3.SelectedNode = item;
                        treeView3.Select();
                    }
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            mToolParam.NumberList.Clear();
            foreach (TreeNode item in treeView1.Nodes)
            {
                if (item.Checked)
                {
                    mToolParam.NumberList.Add(item.Text.Trim());
                }
            }
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            mToolParam.LowerCaseLetterList.Clear();
            foreach (TreeNode item in treeView2.Nodes)
            {
                if (item.Checked)
                {
                    mToolParam.LowerCaseLetterList.Add(item.Text.Trim());
                }
            }
        }

        private void treeView3_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            mToolParam.UpperCaseLetterList.Clear();
            foreach (TreeNode item in treeView3.Nodes)
            {
                if (item.Checked)
                {
                    mToolParam.UpperCaseLetterList.Add(item.Text.Trim());
                }
            }
        }

        private void dataGridView_LineList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_LineList.Rows.Count > 0 && dataGridView_LineList.CurrentRow != null)
            {
                LabelIndex = dataGridView_LineList.CurrentRow.Index;
                if (LabelIndex < 0) return;
                mIsInit = false;
                int a = LabelIndex;
                numericUpDown_CharMinScore.Value = (decimal)mToolParam.CheckRegionList[a].CharMinScore;
                textBox_RecognitionText.Text = mToolParam.CheckRegionList[a].ResultText;
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
                mToolMachine.DrawWind.DispObj(mToolParam.CheckRegionList[a].CheckRegion);
            }
        }

        private void dataGridView_LineList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < info.Rows.Count; i++)
            {
                mToolParam.CheckRegionList[i].RegionName = info.Rows[i].ItemArray[1].ToString();
            }
        }

        private void button_AddRecognitionRegion_Click(object sender, EventArgs e)
        {
            DeepOcrCheckParam param = new DeepOcrCheckParam();
            param.RegionName = "识别区域" + (mToolParam.CheckRegionList.Count + 1);
            mToolParam.CheckRegionList.Add(param);

            DataRow row = info.NewRow();
            row[0] = mToolParam.CheckRegionList.Count;
            row[1] = param.RegionName;
            info.Rows.Add(row);
        }

        private void button_DeleIndex_Click(object sender, EventArgs e)
        {
            int a = LabelIndex;
            if (a > -1)
            {
                mToolParam.CheckRegionList.RemoveAt(a);
                info.Rows.RemoveAt(a);
            }
               
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

        private void button_DrawRoi2_Click(object sender, EventArgs e)
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

        private void button_DeleRoi_Click(object sender, EventArgs e)
        {
            mIsInit = false;
            int a = LabelIndex;
            if (LabelIndex > -1)
                mToolParam.CheckRegionList[a].CheckRegion.GenEmptyObj();
            mIsInit = true;
        }

        private void trackBar_MarkSize_Scroll(object sender, EventArgs e)
        {
            textBox_MarkSize.Text = trackBar_MarkSize.Value.ToString();
        }

        private void numericUpDown_CharMinScore_ValueChanged(object sender, EventArgs e)
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

        private void button_WriteIn_Click(object sender, EventArgs e)
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
