using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTools
{
    [Serializable]
    public class ToolFindShapeModelParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;

        public int FindStartAngle;//查找起始角度
        public int FindExtentAngle;//查找角度范围
        public double FindScore;//查找分数
        public double Greediness;//贪婪度
        public double Overloap;//重叠度
        public int TimeOut;//超时时间

        public HShapeModel ShapeModel;
        public HRegion mModelRegion;
        public HRegion mMatchRegion;
        public double mModelCenterRow;
        public double mModelCenterCol;
        public HImage mImage;
        public bool mIsSaveShapeModelImage;

        public int MinThreshold;
        public int MaxThreshold;
        public int Parts;
        public int StartPhi;
        public int EndPhi;
        public int NumLevel;
        public string Polarity;
        public string Point;
        public double ScaleX;
        public double ScaleY;

        private StepInfo mStepInfo;
        private string mShowName;
        private string mToolName;
        private ToolType mToolType;
        private JumpInfo mStepJumpInfo;
        private string mResultString;
        private bool mForceOK;
        private int mNgReturnValue;

        public static string strConfigPath;
        public static void ConfigPath(string path)
        {
            strConfigPath = path;
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
        public override string ResultString
        {
            get => mResultString;
            set => mResultString = value;
        }
        public int ImageSourceStep
        {
            get => mImageSourceStep;
            set => mImageSourceStep = value;
        }
        public delegate int ParamChangedDe(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj);
        public delegate void DrawShapeROIDe();
        public delegate void GenShapeModelDe(HObject obj1, List<StepInfo> StepInfoList, double[] param1, string[] point, string Polarity, double[] param2, out HObject objShow);
        public delegate int LoadImageDel(out HImage image);
        public delegate void mChoosePicDe(HObject obj, List<StepInfo> StepInfoList,out HObject objShow);
        public delegate int DrawRoiDel(HObject obj1, int type, string mRoiType, int mMarkSize, out HObject obj2);
        public delegate int DrawRoi2Del(HObject obj1, string mRoiType, out HObject obj2);
        public delegate int SureRoiDel();
        public delegate int DeleRoiDel();
        public delegate void SureModelDe(HObject obj);
        public delegate void DeletModelDe();


        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public DrawShapeROIDe mDrawShapeROIDe;
        [NonSerialized]
        public GenShapeModelDe mGenShapeModelDe;
        [NonSerialized]
        public LoadImageDel mLoadImageDel;
        [NonSerialized]
        public DrawRoi2Del mDrawRoi2Del;
        [NonSerialized]
        public DrawRoiDel mDrawRoiDel;
        [NonSerialized]
        public SureRoiDel mSureRoiDel;
        [NonSerialized]
        public DeleRoiDel mDeleRoiDel;
        [NonSerialized]
        public SureModelDe mSureModelDe;
        [NonSerialized]
        public DeletModelDe mDeletModelDe;
        [NonSerialized]
        public mChoosePicDe mchoseImg;


        public ToolFindShapeModelParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.ShapeModle;
            mImageSourceStep = -1;
            mImageSourceMark = -1;
            ShapeModel = new HShapeModel(AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig\\config\\Config.reg");
            mModelRegion = new HRegion();
            mModelRegion.GenEmptyRegion();
            mModelCenterRow = 0;
            mModelCenterCol = 0;

            ScaleX = 1 ;
            ScaleY = 1;

            FindStartAngle = -45;//查找起始角度
            FindExtentAngle = 90;//查找角度范围
            FindScore = 0.25;//查找分数
            Greediness = 0.5;//贪婪度
            Overloap = 0.5;//重叠度
            TimeOut = 30;//超时时间
            mImage = new HImage();
            mImage.GenEmptyObj();
            mIsSaveShapeModelImage = false;

            MinThreshold = 10;
            MaxThreshold = 40;
            Parts = 50;
            StartPhi = -45;
            EndPhi = 90;
            NumLevel = 0;
            Polarity = "use_polarity";
            Point = "none";

            mShowName = "模板匹配";
            mToolName = "模板匹配";
            mStepInfo.mShowName= "模板匹配";
            mToolType = ToolType.ShapeModle;
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolFindShapeModel : ToolBase
    {
        ToolFindShapeModelParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;
        HObject mTempRegion;
        HObject mTempXlds;
        HTuple model;


        public static string strConfigPath;
        public static void ConfigPath(string path)
        {
            strConfigPath = path;
        }
        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolFindShapeModelParam;
        }

        public ToolFindShapeModel(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolFindShapeModelParam;

            BindDelegate(true);
            HOperatorSet.GenEmptyObj(out mTempRegion);
            HOperatorSet.GenEmptyObj(out mTempXlds);
        }

        public override int DebugRun(HObject objj1, Bitmap objj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            if (mToolParam.mModelRegion.Area.TupleLength() == 0)
            {
                mToolParam.ResultString = "没有模板区域";
                return -1;
            }
            return FindShapeModel(objj1, StepInfoList);
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            if (mToolParam.ShapeModel == null || !mToolParam.ShapeModel.IsInitialized())
                mToolParam.ShapeModel.ClearShapeModel();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj)
        {
            JumpInfo StepJumpInfo;
            return DebugRun(obj1, obj2, StepInfoList, false, out StepJumpInfo);
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

        public override int ToolRun(HObject obj, Bitmap obj2, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            HObject ImageSource;
            if (mToolParam.mImageSourceStep < 0)
                ImageSource = obj;
            else
                ImageSource = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
            try
            {
                HImage image = new HImage(ImageSource);
                HTuple row = new HTuple();
                HTuple column = new HTuple();
                HTuple angle = new HTuple();
                HTuple score = new HTuple();
                HTuple scale = new HTuple();
                HTuple s1, s2;
                mToolParam.ShapeModel.SetShapeModelParam("timeout", mToolParam.TimeOut);
                HOperatorSet.CountSeconds(out s1);
                mToolParam.ShapeModel.FindScaledShapeModel(image, (new HTuple(mToolParam.FindStartAngle)).TupleRad().D, (new HTuple(mToolParam.FindExtentAngle)).TupleRad().D, new HTuple(mToolParam.ScaleX),
                    new HTuple(mToolParam.ScaleX), (HTuple)mToolParam.FindScore, 1, mToolParam.Overloap, new HTuple("least_squares"), (new HTuple(7)).TupleConcat(1), mToolParam.Greediness,
                    out row, out column, out angle, out scale, out score);
                HOperatorSet.CountSeconds(out s2);
                if (row.Length != 1)
                {
                    image.Dispose();
                    return mToolParam.NgReturnValue;
                }
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mModelCenterRow;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mModelCenterCol;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = row.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = column.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[4] = angle.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[5] = score.D;             

                image.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
               return -1;
            };
        }

        public override ResStatus InitAiResources()
        {
            try
            {
                return ResStatus.OK;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return ResStatus.Error;
            }

        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mParamChangedDe += ParamChanged;
                mToolParam.mDrawShapeROIDe += CreateRoi;
                mToolParam.mGenShapeModelDe += GenShapeModel;
                mToolParam.mLoadImageDel += LoadImage;
                mToolParam.mDrawRoi2Del += DrawRoi2;
                mToolParam.mDrawRoiDel += DrawRoi;
                mToolParam.mDeleRoiDel += DeleRoi;
                mToolParam.mSureModelDe += SureModel;
                mToolParam.mDeletModelDe += DeleModel;
                mToolParam.mchoseImg += ChangeImg;
                
            }
            else
            {
                mToolParam.mParamChangedDe = null;
                mToolParam.mDrawShapeROIDe = null;
                mToolParam.mGenShapeModelDe = null;
                mToolParam.mLoadImageDel = null;
                mToolParam.mDrawRoi2Del = null;
                mToolParam.mDrawRoiDel = null;
                mToolParam.mDeleRoiDel = null;
                mToolParam.mSureModelDe = null;
                mToolParam.mDeletModelDe = null;
            }

            return ResStatus.OK;
        }

        public void CreateRoi()
        {
            mToolParam.mModelRegion.GenEmptyRegion();
            mDrawWind.ClearWindow();
            double[] param = new double[4];
            mDrawWind.SetColor("red");
            mDrawWind.SetDraw("margin");
            mDrawWind.DrawRectangle1(out param[0], out param[1], out param[2], out param[3]);
            mToolParam.mModelRegion = new HRegion(param[0], param[1], param[2], param[3]);
            mDrawWind.ClearWindow();
            mDrawWind.DispObj(mToolParam.mModelRegion);
        }

        public void GenShapeModel(HObject obj1, List<StepInfo> StepInfoList, double[] param1, string[] point, string Polarity, double[] param2, out HObject objShow)
        {
            HOperatorSet.GenEmptyObj(out objShow);
            HObject ImageSource;
            if (mToolParam.mImageSourceStep < 0)
                ImageSource = obj1;
            else
                ImageSource = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                
            if (!(mToolParam.mModelRegion.Area > 0)) 
            {
                mToolParam.ResultString = "模板创建区域为空";
                return;
            }
            HTuple chanl;
            HOperatorSet.CountChannels(ImageSource, out chanl);
            if (chanl != 1)
            {
                mToolParam.ResultString = "非单通道图片，请重新输入图片源";
                return;
            }

            try
            {
                model = new HTuple();
                HTuple num;
                if (mToolParam.NumLevel == 0)
                    num = "auto";
                else
                    num = mToolParam.NumLevel;
                HObject obj2, obj3, obj4, obj5;
                HTuple a, b, c, d, HomMat2D,scale;
                HTuple w, h;

                //reduce_domain
                HOperatorSet.ReduceDomain(ImageSource, mToolParam.mModelRegion, out obj2);
                HOperatorSet.GetImageSize(ImageSource, out w, out h);
                //create_shape_model
                HOperatorSet.CreateScaledShapeModel(obj2, num, (new HTuple(param1[0])).TupleRad(), (new HTuple(param1[1])).TupleRad(), "auto", new HTuple(mToolParam.ScaleX), new HTuple(mToolParam.ScaleY), "auto", point, Polarity, param2, "auto", out model);
                //获取模板轮廓
                HOperatorSet.GetShapeModelContours(out obj3, model, 1);
                HOperatorSet.FindScaledShapeModel(ImageSource, model, (new HTuple(0)).TupleRad(), (new HTuple(360)).TupleRad(), new HTuple(mToolParam.ScaleX), new HTuple(mToolParam.ScaleY), 0.5, 1, 0.5, "least_squares", 0, 0.7, out a, out b, out c, out scale, out d);
                if (a.Length < 1)
                {
                    mDrawWind.ClearWindow();
                    mDrawWind.SetColor("red");
                    mDrawWind.SetTposition(50, 50);
                    mDrawWind.WriteString("模型创建失败");
                    return;
                }
                HTuple a1, r1, c1;
                HOperatorSet.AreaCenter(mToolParam.mModelRegion, out a1, out r1, out c1);
                mToolParam.mModelCenterRow = r1;
                mToolParam.mModelCenterCol = c1;

                HTuple HomMat2D1, HomMat2D2, HomMat2D3;
                HOperatorSet.HomMat2dIdentity(out HomMat2D1);
                HOperatorSet.HomMat2dScale(HomMat2D1, scale, scale, 0, 0, out HomMat2D2);
                HOperatorSet.HomMat2dRotate(HomMat2D2, c, 0, 0, out HomMat2D3);
                HOperatorSet.HomMat2dTranslate(HomMat2D3, a, b, out HomMat2D1);
                //转换模板轮廓
                HOperatorSet.AffineTransContourXld(obj3, out obj4, HomMat2D1);


                //加入区域缩放
                HOperatorSet.VectorAngleToRigid(mToolParam.mModelCenterRow, mToolParam.mModelCenterCol, 0, a, b, c, out HomMat2D);
                HTuple HomMat2D4;
                HOperatorSet.HomMat2dIdentity(out HomMat2D4);
                HOperatorSet.HomMat2dScale(HomMat2D, scale, scale, a, b, out HomMat2D4);

                HOperatorSet.AffineTransRegion(mToolParam.mModelRegion, out obj5, HomMat2D4, "nearest_neighbor");

                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mModelCenterRow;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mModelCenterCol;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = a.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = b.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[4] = c.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[5] = d.D;
                mDrawWind.DispObj(ImageSource);
                mDrawWind.SetColor("blue");
                mDrawWind.SetDraw("margin");
                mDrawWind.DispObj(obj5);
                mDrawWind.SetColored(12);
                mDrawWind.SetLineWidth(2);
                mDrawWind.DispObj(obj4);
                objShow = obj4;
                mTempXlds = obj4;
                obj2.Dispose();
                obj3.Dispose();
                HomMat2D1.Dispose();
                HomMat2D2.Dispose();
                HomMat2D3.Dispose();
                HomMat2D4.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ChangeImg(HObject obj, List<StepInfo> StepInfoList, out HObject objShow)
        {
            HOperatorSet.GenEmptyObj(out objShow);
            HTuple mChannel;
            HObject objFinal;
            HTuple s1, s2;
            HOperatorSet.CountSeconds(out s1);
            if (mToolParam.ImageSourceStep < 0)
            {
                objFinal = obj;
                //通道数检测
                HOperatorSet.CountChannels(obj, out mChannel);
                if (mChannel != 1)
                {
                    mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                    return;
                }
            }
            else
            {
                objFinal = StepInfoList[mToolParam.ImageSourceStep - 1].mToolRunResul.mImageOutPut;
                //通道数检测
                HOperatorSet.CountChannels(objFinal, out mChannel);
                if (mChannel.I != 1)
                {
                    mToolParam.ResultString = "输入图片通道错误，非单通道图片";
                    return;
                }
            }
            HObject obj1;
            //scale_image_range(objFinal, out obj1, mToolParam.mScaleMin, mToolParam.mScaleMax);
            HOperatorSet.CountSeconds(out s2);
            mToolParam.ResultString = "耗时：" + ((s2.D - s1.D) * 1000).ToString("f2") + "ms" + "\r\n";
            mToolParam.StepInfo.mToolRunResul.mImageOutPut = objFinal;
            mDrawWind.ClearWindow();
            mDrawWind.DispObj(objFinal);
        }
        public void DeleModel()
        {
            //模板区域置空
            mToolParam.mModelRegion.GenEmptyRegion();
            //模板恢复默认
            mToolParam.ShapeModel = new HShapeModel(strConfigPath + "Config.reg");
            mToolParam.mImage.GenEmptyObj();
            mToolParam.mIsSaveShapeModelImage = false;
        }

        public void SureModel(HObject obj)
        {
            mToolParam.ShapeModel = new HShapeModel(model.H);
            mToolParam.mIsSaveShapeModelImage = true;
            mToolParam.mImage = new HImage(obj);
        }

        public int FindShapeModel(HObject obj, List<StepInfo> StepInfoList)
        {
            HObject ImageSource;
            if (mToolParam.mImageSourceStep < 0)
                ImageSource = obj;
            else
                ImageSource = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
            mToolParam.ResultString = "";
            try
            {
                HImage image = new HImage(ImageSource);
                HTuple row = new HTuple();
                HTuple column = new HTuple();
                HTuple angle = new HTuple();
                HTuple score = new HTuple();
                HTuple scale = new HTuple();
                HTuple s1, s2;
                mToolParam.ShapeModel.SetShapeModelParam("timeout", mToolParam.TimeOut);
                HOperatorSet.CountSeconds(out s1);
                mToolParam.ShapeModel.FindScaledShapeModel(image, (new HTuple(mToolParam.FindStartAngle)).TupleRad().D, (new HTuple(mToolParam.FindExtentAngle)).TupleRad().D, new HTuple(mToolParam.ScaleX),
                    new HTuple(mToolParam.ScaleX), (HTuple)mToolParam.FindScore, 1, mToolParam.Overloap, new HTuple("least_squares"), (new HTuple(7)).TupleConcat(1), mToolParam.Greediness,
                    out row, out column, out angle, out scale, out score);
                HOperatorSet.CountSeconds(out s2);
                if (row.Length != 1)
                {
                    mDrawWind.ClearWindow();
                    mDrawWind.DispObj(image);
                    mDrawWind.SetColor("red");
                    mDrawWind.SetTposition(50, 50);
                    mDrawWind.WriteString("查找失败");
                    image.Dispose();
                    return -1;
                }
                mToolParam.ResultString = "匹配耗时：" + ((s2.D - s1.D) * 1000).ToString("0.00") + "ms ;\r\n" + "匹配结果：分数：" + score.D.ToString("0.000") + "\r\n" + " Row:=" + row.D.ToString("0.00") + " Column:="
                    + column.D.ToString("0.00") + " 角度：=" + angle.D.ToString("0.00")+ " scale：=" + scale.D.ToString("0.00");
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mModelCenterRow;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mModelCenterCol;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = row.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = column.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[4] = angle.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[5] = score.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[6] = scale.D;


                HTuple HomMat2D,HomMat2D1, HomMat2D2, HomMat2D3;
                HObject objXlds;
                HObject affine1;
                HObject affine2;

                HOperatorSet.GetShapeModelContours(out objXlds, mToolParam.ShapeModel, 1);

                HOperatorSet.HomMat2dIdentity(out HomMat2D1);
                HOperatorSet.HomMat2dScale(HomMat2D1, scale, scale, 0, 0, out HomMat2D2);
                HOperatorSet.HomMat2dRotate(HomMat2D2, angle, 0, 0, out HomMat2D3);
                HOperatorSet.HomMat2dTranslate(HomMat2D3, row, column, out HomMat2D1);
                //转换模板轮廓
                HOperatorSet.AffineTransContourXld(objXlds, out affine1, HomMat2D1);


                //加入区域缩放
                HOperatorSet.VectorAngleToRigid(mToolParam.mModelCenterRow, mToolParam.mModelCenterCol, 0, row, column, angle, out HomMat2D);
                HTuple HomMat2D4;
                HOperatorSet.HomMat2dIdentity(out HomMat2D4);
                HOperatorSet.HomMat2dScale(HomMat2D, scale, scale, row, column, out HomMat2D4);
                HOperatorSet.AffineTransRegion(mToolParam.mModelRegion, out affine2, HomMat2D4, "nearest_neighbor");


                mDrawWind.ClearWindow();
                mDrawWind.DispObj(ImageSource);
                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColored(12);
                mDrawWind.DispObj(affine1);
                mDrawWind.SetDraw("margin");
                mDrawWind.SetColor("blue");
                mDrawWind.DispObj(affine2);

                image.Dispose();
                objXlds.Dispose();
                affine1.Dispose();
                affine2.Dispose();
                HomMat2D.Dispose();
                HomMat2D1.Dispose();
                HomMat2D2.Dispose();
                HomMat2D3.Dispose();
                HomMat2D4.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                mDrawWind.ClearWindow();
                return -1;
            }
        }

        private int LoadImage(out HImage image) 
        {
            image = new HImage();
            if (mToolParam.mIsSaveShapeModelImage)
            {
                image = mToolParam.mImage;
                return 0;
            }
            else
            {
                return 1;
            }


        }

        public int DrawRoi(HObject obj1, int type, string mRoiType, int mMarkSize, out HObject showRegion)
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
                                HOperatorSet.Union2(mToolParam.mModelRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.mModelRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.mModelRegion = r1;
                            }
                            else
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Difference(mToolParam.mModelRegion, obj, out ExpTmpOutVar_0);
                                mToolParam.mModelRegion.Dispose();
                                HRegion r1 = new HRegion(ExpTmpOutVar_0);
                                mToolParam.mModelRegion = r1;
                            }
                        }
                        mDrawWind.SetRgba(255, 0, 0, 120);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.mModelRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.mModelRegion);
                showRegion = mToolParam.mModelRegion;
                Thread.Sleep(20);
                return 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int DrawRoi2(HObject obj1, string mRoiType, out HObject showRegion)
        {
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("fill");
            mDrawWind.SetRgba(255, 0, 0, 120);
            mDrawWind.DispObj(mToolParam.mModelRegion);
            HOperatorSet.GenEmptyObj(out showRegion);
            try
            {
                if (mRoiType == "circle")
                {
                    double row, column, radius;
                    HObject obj;
                    mDrawWind.DrawCircle(out row, out column, out radius);
                    HOperatorSet.GenCircle(out obj, row, column, radius);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mModelRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mModelRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle1")
                {
                    double row, column, row2, column2;
                    HObject obj;
                    mDrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                    HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mModelRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mModelRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle2")
                {
                    double row, column, phi, length1, length2;
                    HObject obj;
                    mDrawWind.DrawRectangle2(out row, out column, out phi, out length1, out length2);
                    HOperatorSet.GenRectangle2(out obj, row, column, phi, length1, length2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mModelRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mModelRegion = new HRegion(temp);
                }
                else if (mRoiType == "ellipse")
                {
                    double row, column, phi, radius1, radius2;
                    HObject obj;
                    mDrawWind.DrawEllipse(out row, out column, out phi, out radius1, out radius2);
                    HOperatorSet.GenEllipse(out obj, row, column, phi, radius1, radius2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mModelRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mModelRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mModelRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mModelRegion = new HRegion(temp);
                }

                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.mModelRegion);
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public int DeleRoi()
        {
            mToolParam.mModelRegion.Dispose();
            mToolParam.mModelRegion.GenEmptyRegion();
            mDrawWind.ClearWindow();
            return 0;
        }

    }


}
