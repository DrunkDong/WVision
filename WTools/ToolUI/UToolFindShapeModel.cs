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
    public partial class UToolFindShapeModel : UserControl, InterfaceUIBase
    {
        public UToolFindShapeModel()
        {
            InitializeComponent();
        }

        bool mIsInit;
        ToolFindShapeModelParam mToolParam;
        List<StepInfo> mStepInfoList;
        ToolMachine mToolMachine;
        int mStepIndex;

        public ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolFindShapeModelParam;
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
                if (StepInfoList[i].mToolType == ToolType.ScaleImage || StepInfoList[i].mToolType == ToolType.DecomposeRGB)
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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

            numericUpDown_FindStartPhi.Value = mToolParam.FindStartAngle;
            numericUpDown_FindEndPhi.Value = mToolParam.FindExtentAngle;
            numericUpDown_ModelScore.Value = (decimal)mToolParam.FindScore;
            numericUpDown_Greediness.Value = (decimal)mToolParam.Greediness;
            numericUpDown_OverLap.Value = (decimal)mToolParam.Overloap;
            numericUpDown_TimeOut.Value = mToolParam.TimeOut;

            numericUpDown_MinThreshold.Value = mToolParam.MinThreshold;
            numericUpDown_MaxThreshold.Value = mToolParam.MaxThreshold;
            numericUpDown_StartPhi.Value = mToolParam.StartPhi;
            numericUpDown_EndPhi.Value = mToolParam.EndPhi;
            numericUpDown_NumLevel.Value = mToolParam.NumLevel;
            numericUpDown_Parts.Value = mToolParam.Parts;
            comboBox_Polarity.SelectedItem = mToolParam.Polarity;
            comboBox_Point.SelectedItem = mToolParam.Point;
            if (mToolParam.ScaleX != 0)
                numericUpDown_ScaleX.Value = (decimal)mToolParam.ScaleX;
            else
                numericUpDown_ScaleX.Value = 1;
            if (mToolParam.ScaleY != 0)
                numericUpDown_ScaleY.Value = (decimal)mToolParam.ScaleY;
            else
                mToolParam.ScaleY = 1;
            comboBox_ImageStep.Items.Clear();
            comboBox_ImageStep.Items.Add("0_本地图片");
            for (int i = 0; i < StepInfoList.Count; i++)
            {
                if (StepIndex < (i + 1))
                    break;

                if (StepInfoList[i].mToolType == ToolType.ScaleImage || StepInfoList[i].mToolType == ToolType.DecomposeRGB) 
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

        private void UToolFindShapeModel_Load(object sender, EventArgs e)
        {
            mIsInit = false;
            InitParam();
            mIsInit = true;
        }

        private void GetParam()
        {
            mToolParam.FindStartAngle = (int)numericUpDown_FindStartPhi.Value;
            mToolParam.FindExtentAngle = (int)numericUpDown_FindEndPhi.Value;
            mToolParam.FindScore = (double)numericUpDown_ModelScore.Value;
            mToolParam.Greediness = (double)numericUpDown_Greediness.Value;
            mToolParam.Overloap = (double)numericUpDown_OverLap.Value;
            mToolParam.TimeOut = (int)numericUpDown_TimeOut.Value;
            mToolParam.NgReturnValue = (int)numericUpDown_RetrunValue1.Value;
            mToolParam.ForceOK = checkBox_ForceOK.Checked;

            mToolParam.MinThreshold = (int)numericUpDown_MinThreshold.Value;
            mToolParam.MaxThreshold = (int)numericUpDown_MaxThreshold.Value;
            mToolParam.StartPhi = (int)numericUpDown_StartPhi.Value;
            mToolParam.EndPhi = (int)numericUpDown_EndPhi.Value;
            mToolParam.NumLevel = (int)numericUpDown_NumLevel.Value;
            mToolParam.Parts = (int)numericUpDown_Parts.Value;
            mToolParam.Polarity = comboBox_Polarity.Text;
            mToolParam.Point = comboBox_Point.Text;

            mToolParam.ScaleX = (double)numericUpDown_ScaleX.Value;
            mToolParam.ScaleY = (double)numericUpDown_ScaleY.Value;
        }

        private void numericUpDown_RoiX_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_RoiY_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
            HOperatorSet.CountSeconds(out s2);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, res);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
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
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            mToolMachine.mEnableControlDel(false);
            //mToolParam.mDrawShapeROIDe();
            mToolMachine.mEnableControlDel(true);
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void trackBar_MarkSize_Scroll(object sender, EventArgs e)
        {
            textBox_MarkSize.Text = trackBar_MarkSize.Value.ToString();
        }

        private void numericUpDown_StartPhi_ValueChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))  
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_EndPhi_ValueChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_Polarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_Point_SelectedIndexChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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
            int res = mToolParam.mParamChangedDe(mToolMachine.ToolCurrImage, mToolMachine.ToolCurrBitmap, StepInfoList, false);
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

        private void numericUpDown_NumLevel_ValueChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_MinThreshold_ValueChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_MaxThreshold_ValueChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void numericUpDown_Parts_ValueChanged(object sender, EventArgs e)
        {
            double[] param1 = new double[2] { Convert.ToDouble(numericUpDown_StartPhi.Value), Convert.ToDouble(numericUpDown_EndPhi.Value) };
            double[] param2 = new double[3] { (double)numericUpDown_MinThreshold.Value, (double)numericUpDown_MaxThreshold.Value, (double)numericUpDown_Parts.Value };
            string[] point = new string[2] { comboBox_Point.Text, "no_pregeneration" };
            string Polarity = comboBox_Polarity.Text;

            if (!mIsInit)
                return;
            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
            if (!(mToolParam.mModelRegion.Area > 0))
            {
                textBox_Mes.Text = "请先设定模板区域";
                return;
            }
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            HObject ob1;
                   mToolParam.mGenShapeModelDe(mToolMachine.ToolCurrImage,StepInfoList, param1, point, Polarity, param2, out ob1);
            HOperatorSet.CountSeconds(out s2);
            mToolMachine.ShowRegionList.Clear();
            mToolMachine.ShowRegionList.Add(ob1);
            textBox_Mes.Text = mToolParam.ResultString + "\r\n" + "当前耗时：" + ((s2.D - s1.D) * 1000).ToString("00.00") + " ms";
            mToolMachine.mChangeState(StepIndex, 0);
            mToolMachine.mChangeToolCostTime(StepIndex, ((s2.D - s1.D) * 1000).ToString("0.00"));
            textBox_Mes.Text = mToolParam.ResultString;
        }

        private void comboBox_ImageStep_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!mIsInit)
                return;
            GetParam();
            HObject obj1;
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
                mToolParam.mchoseImg(mToolParam.StepInfo.mToolRunResul.mImageOutPut, StepInfoList, out obj1);

            }

            if (mToolMachine.ToolCurrImage == null)
            {
                textBox_Mes.Text = "请先加载一张图片";
                return;
            }
        }

        private void trackBar_MarkSize_Scroll_1(object sender, EventArgs e)
        {
            textBox_MarkSize.Text = trackBar_MarkSize.Value.ToString();
        }

        private void button_DeleRoi_Click_1(object sender, EventArgs e)
        {
            mToolParam.mModelRegion.GenEmptyObj();
        }

        private void textBox_Mes_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel_Body_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_ToolName_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label_MinThreshold_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label_EndPhi_Click(object sender, EventArgs e)
        {

        }

        private void label_Part_Click(object sender, EventArgs e)
        {

        }

        private void label_MaxThreshold_Click(object sender, EventArgs e)
        {

        }

        private void label_StartPhi_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox_MarkSize_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_CheckHeight_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton_Circle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_Rec1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton_Paint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_Rid_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton_Circle2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_Rec2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_Rec22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_ellipse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton_Any_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage_Setting_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox_RefreshName_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_Greediness_Click(object sender, EventArgs e)
        {

        }

        private void label_FIndEndPhi_Click(object sender, EventArgs e)
        {

        }

        private void label_FindStartPhi_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown_OverLap_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void tabControl_Check_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown_ScaleX_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_ScaleX.Value == 0) 
            {
                numericUpDown_ScaleX.Value = 1;
            }
            if (numericUpDown_ScaleX.Value > numericUpDown_ScaleY.Value) 
            {
                numericUpDown_ScaleX.Value = (decimal)((double)numericUpDown_ScaleY.Value - 0.01);
            }
            GetParam();
        }

        private void NumericUpDown_ScaleY_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInit)
                return;
            if (numericUpDown_ScaleY.Value == 0)
            {
                numericUpDown_ScaleY.Value = 1;
            }
            if (numericUpDown_ScaleY.Value < numericUpDown_ScaleX.Value)
            {
                numericUpDown_ScaleY.Value = (decimal)((double)numericUpDown_ScaleX.Value + 0.01);
            }
            GetParam();
        }

        private void NumericUpDown_ZoomParam_ValueChanged(object sender, EventArgs e)
        {
            GetParam();
        }

        private void button_LoadLocalModel_Click(object sender, EventArgs e)
        {

        }
    }
}
