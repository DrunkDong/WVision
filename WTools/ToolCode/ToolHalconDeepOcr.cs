using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WTools
{
    [Serializable]
    public class DeepOcrCheckParam
    {
        public string RegionName;
        public HRegion CheckRegion;
        public double CharMinScore;
        public string ResultText;
        public DeepOcrCheckParam()
        {
            RegionName = "";
            CheckRegion = new HRegion();
            CheckRegion.GenEmptyObj();
            CharMinScore = 0.8;
            ResultText = "";
        }
    }
    [Serializable]
    public class ToolHalconDeepOcrParam : ToolParamBase
    {
        public int mShapeModelStep;//定位源
        public int mShapeModelMark;
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;

        public string mAiModelPath;
        public int mBatchSize;
        public int mGpuID;

        public string mOCR_Char;
        public string mOCR_Text;
        private StepInfo mStepInfo;
        private string mShowName;
        private string mToolName;
        private ToolType mToolType;
        private JumpInfo mStepJumpInfo;
        private string mResultString;
        private bool mForceOK;
        private int mNgReturnValue;

        public string[] hv_OnlyNumber;
        public string[] hv_OnlyLowerCaseLetter;
        public string[] hv_OnlyUpperCaseLetter;

        public List<string> NumberList;
        public List<string> LowerCaseLetterList;
        public List<string> UpperCaseLetterList;

        public List<DeepOcrCheckParam> CheckRegionList;
        private ToolResultType mToolResultType;

        public override ToolResultType ToolResultType
        {
            get => mToolResultType;
            set => mToolResultType = value;
        }
        public override bool ForceOK
        {
            get => mForceOK;
            set => mForceOK = value;
        }
        public override int NgReturnValue
        {
            get => mNgReturnValue;
            set => mNgReturnValue = value;
        }

        public override StepInfo StepInfo
        {
            get => mStepInfo;
            set => mStepInfo = value;
        }
        public override string ShowName
        {
            get => mShowName;
            set => mShowName = value;
        }
        public override string ToolName
        {
            get => mToolName;
            set => mToolName = value;
        }
        public override ToolType ToolType
        {
            get => mToolType;
            set => mToolType = value;
        }
        public override JumpInfo StepJumpInfo
        {
            get => mStepJumpInfo;
            set => mStepJumpInfo = value;
        }
        public int ImageSourceStep
        {
            get => mImageSourceStep;
            set => mImageSourceStep = value;
        }
        public override string ResultString
        {
            get => mResultString;
            set => mResultString = value;
        }

        public delegate ResStatus InitAiModelDe();
        public delegate int ParamChangedDe(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj);
        public delegate int CheckAiDel(HObject obj1, List<StepInfo> StepInfoList);
        public delegate int DrawRoiDel(HObject obj1, int mIndex, int type, string mRoiType, int mMarkSize, out HObject obj2);
        public delegate int DrawRoi2Del(HObject obj1, int mIndex, string mRoiType, out HObject obj2);
        public delegate int SureRoiDel();
        public delegate int DeleRoiDel(int mIndex);

        [NonSerialized]
        public InitAiModelDe mInitAiModelDe;
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public CheckAiDel mCheckAiDel;
        [NonSerialized]
        public DrawRoi2Del mDrawRoi2Del;
        [NonSerialized]
        public DrawRoiDel mDrawRoiDel;
        [NonSerialized]
        public SureRoiDel mSureRoiDel;
        [NonSerialized]
        public DeleRoiDel mDeleRoiDel;

        public ToolHalconDeepOcrParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.DeepOcr;
            mToolType = ToolType.DeepOcr;
            mStepInfo.mToolResultType = ToolResultType.OCR;
            mToolResultType = ToolResultType.OCR;

            mImageSourceStep = -1;
            mImageSourceMark = -1;
            mShapeModelStep = -1;
            mShapeModelMark = -1;

            mBatchSize = 1;
            mGpuID = 0;
            mAiModelPath = "";
            mOCR_Char = "";
            mOCR_Text = "";
            mShowName = "深度OCR";
            mToolName = "深度OCR";
            mStepInfo.mShowName = "深度OCR";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;

            NumberList = new List<string>();
            LowerCaseLetterList = new List<string>();
            UpperCaseLetterList = new List<string>();

            CheckRegionList = new List<DeepOcrCheckParam>();

            hv_OnlyNumber = new string[10];
            hv_OnlyNumber[0] = "0";
            hv_OnlyNumber[1] = "1";
            hv_OnlyNumber[2] = "2";
            hv_OnlyNumber[3] = "3";
            hv_OnlyNumber[4] = "4";
            hv_OnlyNumber[5] = "5";
            hv_OnlyNumber[6] = "6";
            hv_OnlyNumber[7] = "7";
            hv_OnlyNumber[8] = "8";
            hv_OnlyNumber[9] = "9";

            hv_OnlyLowerCaseLetter = new string[26];
            hv_OnlyLowerCaseLetter[0] = "a";
            hv_OnlyLowerCaseLetter[1] = "b";
            hv_OnlyLowerCaseLetter[2] = "c";
            hv_OnlyLowerCaseLetter[3] = "d";
            hv_OnlyLowerCaseLetter[4] = "e";
            hv_OnlyLowerCaseLetter[5] = "f";
            hv_OnlyLowerCaseLetter[6] = "g";
            hv_OnlyLowerCaseLetter[7] = "h";
            hv_OnlyLowerCaseLetter[8] = "i";
            hv_OnlyLowerCaseLetter[9] = "j";
            hv_OnlyLowerCaseLetter[10] = "k";
            hv_OnlyLowerCaseLetter[11] = "l";
            hv_OnlyLowerCaseLetter[12] = "m";
            hv_OnlyLowerCaseLetter[13] = "n";
            hv_OnlyLowerCaseLetter[14] = "o";
            hv_OnlyLowerCaseLetter[15] = "p";
            hv_OnlyLowerCaseLetter[16] = "q";
            hv_OnlyLowerCaseLetter[17] = "r";
            hv_OnlyLowerCaseLetter[18] = "s";
            hv_OnlyLowerCaseLetter[19] = "t";
            hv_OnlyLowerCaseLetter[20] = "u";
            hv_OnlyLowerCaseLetter[21] = "v";
            hv_OnlyLowerCaseLetter[22] = "w";
            hv_OnlyLowerCaseLetter[23] = "x";
            hv_OnlyLowerCaseLetter[24] = "y";
            hv_OnlyLowerCaseLetter[25] = "z";

            hv_OnlyUpperCaseLetter = new string[26];
            hv_OnlyUpperCaseLetter[0] = "A";
            hv_OnlyUpperCaseLetter[1] = "B";
            hv_OnlyUpperCaseLetter[2] = "C";
            hv_OnlyUpperCaseLetter[3] = "D";
            hv_OnlyUpperCaseLetter[4] = "E";
            hv_OnlyUpperCaseLetter[5] = "F";
            hv_OnlyUpperCaseLetter[6] = "G";
            hv_OnlyUpperCaseLetter[7] = "H";
            hv_OnlyUpperCaseLetter[8] = "I";
            hv_OnlyUpperCaseLetter[9] = "J";
            hv_OnlyUpperCaseLetter[10] = "K";
            hv_OnlyUpperCaseLetter[11] = "L";
            hv_OnlyUpperCaseLetter[12] = "M";
            hv_OnlyUpperCaseLetter[13] = "N";
            hv_OnlyUpperCaseLetter[14] = "O";
            hv_OnlyUpperCaseLetter[15] = "P";
            hv_OnlyUpperCaseLetter[16] = "Q";
            hv_OnlyUpperCaseLetter[17] = "R";
            hv_OnlyUpperCaseLetter[18] = "S";
            hv_OnlyUpperCaseLetter[19] = "T";
            hv_OnlyUpperCaseLetter[20] = "U";
            hv_OnlyUpperCaseLetter[21] = "V";
            hv_OnlyUpperCaseLetter[22] = "W";
            hv_OnlyUpperCaseLetter[23] = "X";
            hv_OnlyUpperCaseLetter[24] = "Y";
            hv_OnlyUpperCaseLetter[25] = "Z";
        }
    }

    public class ToolHalconDeepOcr : ToolBase
    {
        ToolHalconDeepOcrParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;
        HTuple mHalconDlModel;

        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolHalconDeepOcrParam;
        }

        public ToolHalconDeepOcr(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolHalconDeepOcrParam;
            BindDelegate(true);
        }

        public override int DebugRun(HObject objj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.mAiModelPath == "")
                return -1;
            if (mToolParam.ForceOK)
                return 0;
            return Check(objj1, StepInfoList);
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            mHalconDlModel?.Dispose();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj)
        {
            JumpInfo StepJumpInfo;
            return DebugRun(obj1, StepInfoList, false, out StepJumpInfo);
        }

        public override ResStatus SetDebugWind(HTuple DebugWind, HWindow DrawWind)
        {
            mDebugWind = DebugWind;
            mDrawWind = DrawWind;
            return ResStatus.OK;
        }

        public override ResStatus SetRunWind(HTuple DefectWind)
        {
            mDefectWind = DefectWind;
            return ResStatus.OK;
        }

        public override int ToolRun(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
            {
                return 0;
            }
            if (mHalconDlModel == null)
            {
                LogHelper.WriteExceptionLog("未初始化AI");
                return mToolParam.NgReturnValue;
            }
            try
            {
                //输入图片源，即为推理图片
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = obj1;

                //仿射矩阵，用来移动检测区域
                HTuple HomMat2D = new HTuple();
                if (mToolParam.mShapeModelStep > -1)
                {
                    HOperatorSet.VectorAngleToRigid(
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                    0,
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                    out HomMat2D);
                }

                //设置预检测字符
                HTuple hv_OnlyNumber = new HTuple();
                HTuple hv_OnlyLowerCaseLetter = new HTuple();
                HTuple hv_OnlyUpperCaseLetter = new HTuple();
                for (int i = 0; i < mToolParam.NumberList.Count; i++)
                {
                    hv_OnlyNumber[i] = mToolParam.NumberList[i];
                }
                for (int i = 0; i < mToolParam.LowerCaseLetterList.Count; i++)
                {
                    hv_OnlyLowerCaseLetter[i] = mToolParam.LowerCaseLetterList[i];
                }
                for (int i = 0; i < mToolParam.UpperCaseLetterList.Count; i++)
                {
                    hv_OnlyUpperCaseLetter[i] = mToolParam.UpperCaseLetterList[i];
                }
                HTuple tuple = new HTuple();
                tuple = tuple.TupleConcat(hv_OnlyNumber, hv_OnlyLowerCaseLetter, hv_OnlyUpperCaseLetter);
                if (tuple.Length > 0)
                    HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_alphabet", tuple);

                mToolParam.ResultString = "";
                //循环读取字符
                for (int i = 0; i < mToolParam.CheckRegionList.Count; i++)
                {
                    if (mToolParam.CheckRegionList[i].CheckRegion.Area > 0)
                    {
                        //按照实际需要，将设定区域进行仿射
                        HObject region;
                        if (mToolParam.mShapeModelStep > -1)
                            HOperatorSet.AffineTransRegion(mToolParam.CheckRegionList[i].CheckRegion, out region, HomMat2D, "nearest_neighbor");
                        else
                            region = mToolParam.CheckRegionList[i].CheckRegion.CopyObj(1, 1);

                        //裁剪文本图像
                        HOperatorSet.ReduceDomain(imageFinal, region, out HObject objReduced);
                        HOperatorSet.CropDomain(objReduced, out HObject objCrop);
                        objReduced.Dispose();

                        //执行推理
                        HTuple hv_DeepOcrResult = new HTuple(), hv_Tuple = new HTuple();
                        hv_DeepOcrResult.Dispose();
                        HOperatorSet.ApplyDeepOcr(objCrop, mHalconDlModel, "recognition", out hv_DeepOcrResult);
                        hv_Tuple.Dispose();
                        HOperatorSet.GetDictTuple(hv_DeepOcrResult, "word", out hv_Tuple);
                        HOperatorSet.GetDictTuple(hv_DeepOcrResult, "char_candidates", out HTuple Tuple1);

                        string stringResut = "";
                        for (int j = 0; j < Tuple1.TupleLength(); j++)
                        {
                            HOperatorSet.GetDictTuple(Tuple1[j], "confidence", out HTuple tup1);
                            HOperatorSet.GetDictTuple(Tuple1[j], "candidate", out HTuple tup2);
                            if (tup1[0] > mToolParam.CheckRegionList[i].CharMinScore)
                            {
                                mToolParam.ResultString += tup2[0] + "  分数--->" + tup1[0].D.ToString("f3") + "\n";
                                stringResut += tup2[0];
                            }
                        }
                        if (stringResut.ToCharArray().Length > 0)
                        {
                            string result = stringResut;
                            hv_Tuple.Dispose();
                            hv_DeepOcrResult.Dispose();
                            //判断字符内容
                            if (result.ToCharArray().Length == mToolParam.CheckRegionList[i].ResultText.ToCharArray().Length)
                            {
                                //比较字符串
                                if (!JudgeString(mToolParam.CheckRegionList[i].ResultText, result))
                                {
                                    hv_DeepOcrResult.Dispose();
                                    hv_Tuple.Dispose();
                                    return mToolParam.NgReturnValue;
                                }
                            }
                            else
                            {
                                //字符个数不相等
                                mToolParam.ResultString += mToolParam.CheckRegionList[i].RegionName + "   字符与设定个数不一致！\n";
                                hv_DeepOcrResult.Dispose();
                                hv_Tuple.Dispose();
                                return mToolParam.NgReturnValue;
                            }
                        }
                        else
                        {
                            mToolParam.ResultString += mToolParam.CheckRegionList[i].RegionName + "   未检测出字符！\n";
                            hv_DeepOcrResult.Dispose();
                            hv_Tuple.Dispose();
                            return mToolParam.NgReturnValue;
                        }
                    }
                    else
                        return mToolParam.NgReturnValue;

                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
                return 1;
            }

        }
        public override ResStatus InitAiResources()
        {
            try
            {
                if (mToolParam.mAiModelPath == "")
                    return ResStatus.Error;
                mHalconDlModel = new HTuple();
                mHalconDlModel.Dispose();

                HTuple hv_DLDeviceHandles = new HTuple();
                hv_DLDeviceHandles.Dispose();
                HOperatorSet.QueryAvailableDlDevices("runtime", "gpu", out hv_DLDeviceHandles);
                //HOperatorSet.CreateDeepOcr(new HTuple(), new HTuple(), out mHalconDlModel);
                //HOperatorSet.SetDeepOcrParam(mHalconDlModel, "detection_model", mToolParam.mAiModelPath);
                //HOperatorSet.SetDeepOcrParam(mHalconDlModel, "detection_batch_size", 1);
                //HOperatorSet.SetDeepOcrParam(mHalconDlModel, "detection_optimize_for_inference", "true");

                HOperatorSet.ReadDeepOcr(mToolParam.mAiModelPath, out mHalconDlModel);
                HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_batch_size", 1);
                HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_optimize_for_inference", "true");

                //设置预检测字符
                HTuple hv_OnlyNumber = new HTuple();
                HTuple hv_OnlyLowerCaseLetter = new HTuple();
                HTuple hv_OnlyUpperCaseLetter = new HTuple();
                for (int i = 0; i < mToolParam.NumberList.Count; i++)
                {
                    hv_OnlyNumber[i] = mToolParam.NumberList[i];
                }
                for (int i = 0; i < mToolParam.LowerCaseLetterList.Count; i++)
                {
                    hv_OnlyLowerCaseLetter[i] = mToolParam.LowerCaseLetterList[i];
                }
                for (int i = 0; i < mToolParam.UpperCaseLetterList.Count; i++)
                {
                    hv_OnlyUpperCaseLetter[i] = mToolParam.UpperCaseLetterList[i];
                }
                HTuple tuple = new HTuple();
                tuple = tuple.TupleConcat(hv_OnlyNumber, hv_OnlyLowerCaseLetter, hv_OnlyUpperCaseLetter);
                if (tuple.Length > 0)
                {
                    HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_alphabet", tuple);
                }
                //模型加载进GPU
                HOperatorSet.SetDeepOcrParam(mHalconDlModel, "device", hv_DLDeviceHandles[0]);
                return ResStatus.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return ResStatus.Error;
            }

        }

        public ResStatus InitAI()
        {
            try
            {
                if (mToolParam.mAiModelPath == "")
                    return ResStatus.Error;
                mHalconDlModel = new HTuple();
                mHalconDlModel.Dispose();
                HTuple hv_DLDeviceHandles = new HTuple();
                hv_DLDeviceHandles.Dispose();
                HOperatorSet.QueryAvailableDlDevices("runtime", "gpu", out hv_DLDeviceHandles);
                //HOperatorSet.CreateDeepOcr(new HTuple(), new HTuple(), out mHalconDlModel);
                //HOperatorSet.SetDeepOcrParam(mHalconDlModel, "detection_model", mToolParam.mAiModelPath);

                HOperatorSet.ReadDeepOcr(mToolParam.mAiModelPath, out mHalconDlModel); 
                HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_batch_size", 1);
                HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_optimize_for_inference", "true");

                //设置预检测字符
                HTuple hv_OnlyNumber = new HTuple();
                HTuple hv_OnlyLowerCaseLetter = new HTuple();
                HTuple hv_OnlyUpperCaseLetter = new HTuple();
                for (int i = 0; i < mToolParam.NumberList.Count; i++)
                {
                    hv_OnlyNumber[i] = mToolParam.NumberList[i];
                }
                for (int i = 0; i < mToolParam.LowerCaseLetterList.Count; i++)
                {
                    hv_OnlyLowerCaseLetter[i] = mToolParam.LowerCaseLetterList[i];
                }
                for (int i = 0; i < mToolParam.UpperCaseLetterList.Count; i++)
                {
                    hv_OnlyUpperCaseLetter[i] = mToolParam.UpperCaseLetterList[i];
                }
                HTuple tuple = new HTuple();
                tuple = tuple.TupleConcat(hv_OnlyNumber, hv_OnlyLowerCaseLetter, hv_OnlyUpperCaseLetter);
                if (tuple.Length > 0)
                {
                    HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_alphabet", tuple);
                }
                //模型加载进GPU
                HOperatorSet.SetDeepOcrParam(mHalconDlModel, "device", hv_DLDeviceHandles[0]);
                return ResStatus.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return ResStatus.Error;
            }
        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mInitAiModelDe += InitAI;
                mToolParam.mParamChangedDe += ParamChanged;
                mToolParam.mCheckAiDel += Check;
                mToolParam.mDrawRoi2Del += DrawRoi2;
                mToolParam.mDrawRoiDel += DrawRoi;
                mToolParam.mDeleRoiDel += DeleRoi;
            }
            else
            {
                mToolParam.mInitAiModelDe = null;
                mToolParam.mParamChangedDe = null;
                mToolParam.mCheckAiDel = null;
                mToolParam.mDrawRoi2Del = null;
                mToolParam.mDrawRoiDel = null;
                mToolParam.mDeleRoiDel = null;
            }

            return ResStatus.OK;
        }

        public int DrawRoi(HObject obj1, int mIndex, int type, string mRoiType, int mMarkSize, out HObject showRegion)
        {
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                int row, column, buttom;
                buttom = 0;
                row = -1;
                column = -1;
                while (buttom != 4)
                {
                    Application.DoEvents();
                    try
                    {
                        mDrawWind.GetMposition(out row, out column, out buttom);
                    }
                    catch (System.Exception)
                    {
                        buttom = 0;
                    }
                    mDrawWind.SetColor("red");
                    mDrawWind.SetDraw("fill");
                    if (row > 0 && column > 0)
                    {
                        HObject obj;
                        if (mRoiType == "circle")
                        {
                            HOperatorSet.GenCircle(out obj, row, column, mMarkSize / 2);
                        }
                        else
                        {
                            HOperatorSet.GenRectangle1(out obj, row - mMarkSize / 2, column - mMarkSize / 2, row + mMarkSize / 2, column + mMarkSize / 2);
                        }

                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");
                        HOperatorSet.SetSystem("flush_graphic", "false");
                        if (buttom == 1)
                        {
                            if (type == 0)
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Union2(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.CheckRegionList[mIndex].CheckRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.CheckRegionList[mIndex].CheckRegion = r1;
                            }
                            else
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Difference(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.CheckRegionList[mIndex].CheckRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.CheckRegionList[mIndex].CheckRegion = r1;
                            }
                        }
                        mDrawWind.SetRgba(255, 0, 0, 90);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.CheckRegionList[mIndex].CheckRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 90);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.CheckRegionList[mIndex].CheckRegion);
                showRegion = mToolParam.CheckRegionList[mIndex].CheckRegion;
                Thread.Sleep(20);
                return 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int DrawRoi2(HObject obj1, int mIndex, string mRoiType, out HObject showRegion)
        {
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("fill");
            mDrawWind.SetRgba(255, 0, 0, 120);
            mDrawWind.DispObj(mToolParam.CheckRegionList[mIndex].CheckRegion);
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                if (mRoiType == "circle")
                {
                    double row, column, radius;
                    HObject obj;
                    mDrawWind.DrawCircle(out row, out column, out radius);
                    HOperatorSet.GenCircle(out obj, row, column, radius);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out temp);
                    mToolParam.CheckRegionList[mIndex].CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle1")
                {
                    double row, column, row2, column2;
                    HObject obj;
                    mDrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                    HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out temp);
                    mToolParam.CheckRegionList[mIndex].CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle2")
                {
                    double row, column, phi, length1, length2;
                    HObject obj;
                    mDrawWind.DrawRectangle2(out row, out column, out phi, out length1, out length2);
                    HOperatorSet.GenRectangle2(out obj, row, column, phi, length1, length2);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out temp);
                    mToolParam.CheckRegionList[mIndex].CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "ellipse")
                {
                    double row, column, phi, radius1, radius2;
                    HObject obj;
                    mDrawWind.DrawEllipse(out row, out column, out phi, out radius1, out radius2);
                    HOperatorSet.GenEllipse(out obj, row, column, phi, radius1, radius2);
                    showRegion = obj;
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out temp);
                    mToolParam.CheckRegionList[mIndex].CheckRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.CheckRegionList[mIndex].CheckRegion, obj, out temp);
                    mToolParam.CheckRegionList[mIndex].CheckRegion = new HRegion(temp);
                }

                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.CheckRegionList[mIndex].CheckRegion);
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public int DeleRoi(int mIndex)
        {
            mToolParam.CheckRegionList[mIndex].CheckRegion.Dispose();
            mToolParam.CheckRegionList[mIndex].CheckRegion.GenEmptyObj();
            mDrawWind.ClearWindow();
            return 0;
        }

        private int Check(HObject ho_Image, List<StepInfo> StepInfoList)
        {
            int res;
            List<S1ResultParam> r1Results;
            res = GetCheckResult(ho_Image, StepInfoList, out r1Results);
            if (res != 0)
                return res;
            return 0;
        }

        private int GetCheckResult(HObject ho_Image, List<StepInfo> StepInfoList, out List<S1ResultParam> r1Results)
        {
            mToolParam.ResultString = "";
            r1Results = new List<S1ResultParam>();
            if (mHalconDlModel == null)
            {
                mToolParam.ResultString = "模型为空";
                return mToolParam.NgReturnValue;
            }
            try
            {
                //输入图片源，即为推理图片
                HObject imageFinal;
                if (mToolParam.mImageSourceStep > -1)
                    imageFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    imageFinal = ho_Image;

                //仿射矩阵，用来移动检测区域
                HTuple HomMat2D = new HTuple();
                if (mToolParam.mShapeModelStep > -1)
                {
                    HOperatorSet.VectorAngleToRigid(
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[0],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[1],
                    0,
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[2],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[3],
                    StepInfoList[mToolParam.mShapeModelStep - 1].mToolRunResul.mParamOutPut[4],
                    out HomMat2D);
                }

                //设置预检测字符
                HTuple hv_OnlyNumber = new HTuple();
                HTuple hv_OnlyLowerCaseLetter = new HTuple();
                HTuple hv_OnlyUpperCaseLetter = new HTuple();
                for (int i = 0; i < mToolParam.NumberList.Count; i++)
                {
                    hv_OnlyNumber[i] = mToolParam.NumberList[i];
                }
                for (int i = 0; i < mToolParam.LowerCaseLetterList.Count; i++)
                {
                    hv_OnlyLowerCaseLetter[i] = mToolParam.LowerCaseLetterList[i];
                }
                for (int i = 0; i < mToolParam.UpperCaseLetterList.Count; i++)
                {
                    hv_OnlyUpperCaseLetter[i] = mToolParam.UpperCaseLetterList[i];
                }
                HTuple tuple = new HTuple();
                tuple = tuple.TupleConcat(hv_OnlyNumber, hv_OnlyLowerCaseLetter, hv_OnlyUpperCaseLetter);
                if (tuple.Length > 0)
                    HOperatorSet.SetDeepOcrParam(mHalconDlModel, "recognition_alphabet", tuple);

                mToolParam.ResultString = "";
                //提前显示图像
                mDrawWind.DispObj(imageFinal);
                //结果
                int resValue = 0;
                //循环读取字符
                for (int i = 0; i < mToolParam.CheckRegionList.Count; i++)
                {
                    if (mToolParam.CheckRegionList[i].CheckRegion.Area > 0)
                    {
                        //按照实际需要，将设定区域进行仿射
                        HObject region;
                        if (mToolParam.mShapeModelStep > -1)
                            HOperatorSet.AffineTransRegion(mToolParam.CheckRegionList[i].CheckRegion, out region, HomMat2D, "nearest_neighbor");
                        else
                            region = mToolParam.CheckRegionList[i].CheckRegion.CopyObj(1, 1);

                        //裁剪文本图像
                        HOperatorSet.ReduceDomain(imageFinal, region, out HObject objReduced);
                        HOperatorSet.CropDomain(objReduced, out HObject objCrop);
                        objReduced.Dispose();

                        //执行推理
                        HTuple hv_DeepOcrResult = new HTuple(), hv_Tuple = new HTuple();
                        hv_DeepOcrResult.Dispose();
                        HOperatorSet.ApplyDeepOcr(objCrop, mHalconDlModel, "recognition", out hv_DeepOcrResult);
                        hv_Tuple.Dispose();
                        HOperatorSet.GetDictTuple(hv_DeepOcrResult, "word", out hv_Tuple);
                        HOperatorSet.GetDictTuple(hv_DeepOcrResult, "char_candidates", out HTuple Tuple1);

                        string stringResut = "";
                        for (int j = 0; j < Tuple1.TupleLength(); j++)
                        {
                            HOperatorSet.GetDictTuple(Tuple1[j], "confidence", out HTuple tup1);
                            HOperatorSet.GetDictTuple(Tuple1[j], "candidate", out HTuple tup2);
                            if (tup1[0] > mToolParam.CheckRegionList[i].CharMinScore)
                            {
                                mToolParam.ResultString += tup2[0] +"  分数--->"+ tup1[0].D.ToString("f3") + "\n";
                                stringResut += tup2[0];
                            }
                        }
                        if (stringResut.ToCharArray().Length > 0)
                        {
                            string result = stringResut;
                            mToolParam.ResultString += result + "\n";
                            HOperatorSet.SmallestRectangle2(region, out HTuple row, out HTuple column, out HTuple phi, out HTuple length1, out HTuple length2);
                            //获取文本显示位置
                            HTuple hv_D = length1.TupleHypot(length2);
                            HTuple hv_Alpha = length2.TupleAtan2(length1);
                            HTuple hv_WordRow = row + ((((hv_Alpha + phi)).TupleSin()) * hv_D);
                            HTuple hv_WordCol = column - ((((hv_Alpha + phi)).TupleCos()) * hv_D);
                            //显示结果
                            mDrawWind.SetFont("Consolas-24");
                            mDrawWind.SetLineWidth(3);
                            mDrawWind.SetDraw("margin");
                            mDrawWind.SetColor("red");
                            mDrawWind.DispObj(region);
                            mDrawWind.DispText(result, "image", hv_WordRow.D, hv_WordCol.D, "red", new HTuple("box"), new HTuple("false"));
                            region.Dispose();
                            hv_Tuple.Dispose();
                            hv_DeepOcrResult.Dispose();
                            //判断字符内容
                            if (result.ToCharArray().Length == mToolParam.CheckRegionList[i].ResultText.ToCharArray().Length)
                            {
                                //比较字符串
                                if (!JudgeString(mToolParam.CheckRegionList[i].ResultText, result))
                                {
                                    hv_DeepOcrResult.Dispose();
                                    hv_Tuple.Dispose();
                                    resValue = 1;
                                }
                            }
                            else
                            {
                                //字符个数不相等
                                mToolParam.ResultString += mToolParam.CheckRegionList[i].RegionName + "   字符与设定个数不一致！\n";
                                hv_DeepOcrResult.Dispose();
                                hv_Tuple.Dispose();
                                resValue = 1;
                            }
                        }
                        else
                        {
                            mToolParam.ResultString += mToolParam.CheckRegionList[i].RegionName + "   未检测出字符！\n";
                            hv_DeepOcrResult.Dispose();
                            hv_Tuple.Dispose();
                            resValue = 1;
                        }
                    }
                    else
                    {
                        mToolParam.ResultString += mToolParam.CheckRegionList[i].RegionName + "   检测区域未设定！\n";
                        resValue = 1;
                    }

                }
                return resValue;
            }
            catch (Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return 1;
            }
        }

        private bool JudgeString(string str1,string str2) 
        {
            char[] char1 = str1.ToCharArray();
            char[] char2 = str2.ToCharArray();
            for (int i = 0; i < char1.Length; i++)
            {
                if (char1[i] != '#')
                {
                    if (char1[i] != char2[i]) 
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
