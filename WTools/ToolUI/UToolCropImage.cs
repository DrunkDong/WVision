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
    public partial class UToolCropImage : UserControl,InterfaceUIBase
    {
        public UToolCropImage()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolCropImageParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;

        public ToolParamBase ToolParam 
        { 
            get => mToolParam;
            set => mToolParam = value as ToolCropImageParam; 
            
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

        private void UToolCropImage_Load(object sender, EventArgs e)
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
            numericUpDown_mCenterRow.Value = (decimal)mToolParam.mCenterRow;
            numericUpDown_mCenterColumn.Value = (decimal)mToolParam.mCenterColumn;
            numericUpDown_Width.Value = (int)(mToolParam.mLengthWidth * 2);
            numericUpDown_Height.Value = (int)(mToolParam.mLengthHeight * 2);

            mToolParam.mIsSaveImage = false;
            mToolParam.mSaveFold = "";

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

        private void GetParam()
        {
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;
            mToolParam.mCenterRow  = (double)numericUpDown_mCenterRow.Value;
            mToolParam.mCenterColumn  = (double)numericUpDown_mCenterColumn.Value;
            mToolParam.mLengthWidth = (int)(numericUpDown_Width.Value / 2);
            mToolParam.mLengthHeight = (int)(numericUpDown_Height.Value / 2);
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
        #endregion

        #region Events

        private void numericUpDown_mCenterColumn_ValueChanged(object sender, EventArgs e)
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
            mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_mCenterRow_ValueChanged(object sender, EventArgs e)
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
            HObject obj;
            mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList,false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
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

        private void numericUpDown_Width_ValueChanged(object sender, EventArgs e)
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
            mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_Height_ValueChanged(object sender, EventArgs e)
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
            mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void button_SelectFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;              
                if (foldPath != null)
                {
                    textBox_SaveFile.Text = foldPath;
                    mToolParam.mSaveFold = foldPath;
                }
            }

        }

        private void checkBox_IsSaveImage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_IsSaveImage.Checked)
            {
                groupBox_SaveImage.Enabled = true;
                mToolParam.mIsSaveImage = true;
            }
            else
            {
                groupBox_SaveImage.Enabled = false;
                mToolParam.mIsSaveImage = false;
            }

        }
    }
}
