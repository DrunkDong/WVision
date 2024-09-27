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
    public class ToolFindNccShapeModelParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;//图像源

        public int CreateStartAngle;//创建起始角度
        public int CreateExtentAngle;//创建角度范围
        public string CreatePolarity;//创建极性
        public int FindStartAngle;//查找起始角度
        public int FindExtentAngle;//查找角度范围
        public double FindScore;//查找分数
        public double Greediness;//贪婪度
        public double Overloap;//重叠度
        public int TimeOut;//超时时间
        public double ZoomParam;

        public int mNumLel;
        public HNCCModel NccModel;
        public HRegion mModelRegion;
        public HRegion mFindRegion;
        public double mModelCenterRow;
        public double mModelCenterCol;
        public HImage mImage;
        public bool mIsSaveShapeModelImage;

        private StepInfo mStepInfo;
        private string mShowName;
        private string mToolName;
        private ToolType mToolType;
        private JumpInfo mStepJumpInfo;
        private string mResultString;
        private bool mForceOK;
        private int mNgReturnValue;
        public static string strConfigPath;
        private ToolResultType mToolResultType;

        public override ToolResultType ToolResultType
        {
            get => mToolResultType;
            set => mToolResultType = value;
        }
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

        public delegate int ParamChangedDe(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj);
        public delegate void DrawShapeROIDe();
        public delegate void GenNccModelDe(HObject obj1, List<StepInfo> StepInfoList, out HObject objShow);
        public delegate void DeletPaintRoiDe();
        public delegate void DrawRoiDel(HObject obj1, int mType, string mRoiType, int mMarkSize, out HObject showRegion);
        public delegate void SureModelDe(HObject obj);
        public delegate void DeletModelDe();
        public delegate int LoadImageDel(out HImage image);
        public delegate int DrawRoi2Del(HObject obj1, string mRoiType, out HObject obj2);
        public delegate int DrawRoiFindRegionDel(HObject obj1, string mRoiType, out HObject obj2);


        [NonSerialized]
        public ParamChangedDe mParamChangedDe;
        [NonSerialized]
        public DrawShapeROIDe mDrawShapeROIDe;
        [NonSerialized]
        public GenNccModelDe mGenNccModelDe;
        [NonSerialized]
        public DeletPaintRoiDe mDeletPaintRoiDe;
        [NonSerialized]
        public DrawRoiDel mDrawRoiDel;
        [NonSerialized]
        public SureModelDe mSureModelDe;
        [NonSerialized]
        public DeletModelDe mDeletModelDe;
        [NonSerialized]
        public LoadImageDel mLoadImageDel;
        [NonSerialized]
        public DrawRoi2Del mDrawRoi2Del;
        [NonSerialized]
        public DrawRoiFindRegionDel mDrawRoiFindRegionDel;

        public ToolFindNccShapeModelParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.NccShapeModel;
            mToolType = ToolType.NccShapeModel;
            mStepInfo.mToolResultType = ToolResultType.ImageAlignData;
            mToolResultType = ToolResultType.ImageAlignData;

            mImageSourceStep = -1;
            mImageSourceMark = -1;
            NccModel = new HNCCModel(AppDomain.CurrentDomain.BaseDirectory + "\\AppConfig\\config\\Config2.dat");
            mModelRegion = new HRegion();
            mModelRegion.GenEmptyRegion();
            mFindRegion = new HRegion();
            mFindRegion.GenEmptyRegion();
            mModelCenterRow = 0;
            mModelCenterCol = 0;

            mNumLel = 6;
            CreateStartAngle = -45;
            CreateExtentAngle = 90;
            CreatePolarity = "use_polarity";
            ZoomParam = 0.1;

            FindStartAngle = -45;//查找起始角度
            FindExtentAngle = 90;//查找角度范围
            FindScore = 0.25;//查找分数
            Greediness = 0.5;//贪婪度
            Overloap = 0.5;//重叠度
            TimeOut = 30;//超时时间
            mImage = new HImage();
            mImage.GenEmptyObj();
            mIsSaveShapeModelImage = false;

            mShowName = "灰度匹配";
            mToolName = "灰度匹配";
            mStepInfo.mShowName = "灰度匹配";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";
            mNgReturnValue = 1;
        }
    }

    public class ToolFindNccShapeModel : ToolBase
    {
        ToolFindNccShapeModelParam mToolParam;
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
            set => mToolParam = value as ToolFindNccShapeModelParam;
        }

        public ToolFindNccShapeModel(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolFindNccShapeModelParam;

            BindDelegate(true);
            HOperatorSet.GenEmptyObj(out mTempRegion);
            HOperatorSet.GenEmptyObj(out mTempXlds);
        }

        public override int DebugRun(HObject objj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            if (mToolParam.ForceOK)
                return 0;
            return FindShapeModel(objj1, StepInfoList);
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            if (mToolParam.NccModel == null || !mToolParam.NccModel.IsInitialized())
                mToolParam.NccModel.ClearNccModel();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj)
        {
            JumpInfo StepJumpInfo;
            return DebugRun(obj1,StepInfoList, false, out StepJumpInfo);
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
                return 0;
            mToolParam.ResultString = "";
            HObject objFinal;
            if (mToolParam.mImageSourceStep > -1)
                objFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut.CopyObj(1, 1);
            else
                objFinal = obj1.CopyObj(1, 1);

            HTuple chanl;
            HOperatorSet.CountChannels(objFinal, out chanl);
            if (chanl != 1)
            {
                mToolParam.ResultString = "非单通道图片，请重新输入图片源";
                return mToolParam.NgReturnValue;
            }

            try
            {
                HTuple w, h;
                HObject zoomObj,reduceObj;
                HImage image;
                HOperatorSet.GetImageSize(objFinal, out w, out h);
                if (mToolParam.mFindRegion.Area > 0)
                {
                    HOperatorSet.ReduceDomain(objFinal, mToolParam.mFindRegion, out reduceObj);
                    objFinal.Dispose();
                    objFinal = reduceObj;
                }

                HOperatorSet.ZoomImageSize(objFinal, out zoomObj, w * mToolParam.ZoomParam, h * mToolParam.ZoomParam, "constant");
                image = new HImage(zoomObj);

                HTuple row = new HTuple();
                HTuple column = new HTuple();
                HTuple angle = new HTuple();
                HTuple score = new HTuple();
                HTuple s1, s2;
                mToolParam.NccModel.SetNccModelParam("timeout", mToolParam.TimeOut);
                HOperatorSet.CountSeconds(out s1);
                if (mToolParam.mFindRegion.Area > 0)
                {
                    image = image.ReduceDomain(mToolParam.mFindRegion);
                }
                mToolParam.NccModel.FindNccModel(image, (new HTuple(mToolParam.FindStartAngle)).TupleRad().D, (new HTuple(mToolParam.FindExtentAngle)).TupleRad().D, (HTuple)mToolParam.FindScore, 1,
                    mToolParam.Overloap, "true", 0, out row, out column, out angle, out score);
                HOperatorSet.CountSeconds(out s2);
                if (row.Length != 1)
                {
                    zoomObj.Dispose();
                    image.Dispose();
                    return mToolParam.NgReturnValue;
                }

                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mModelCenterRow;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mModelCenterCol;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = row.D / mToolParam.ZoomParam;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = column.D / mToolParam.ZoomParam;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[4] = angle.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[5] = score.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[6] = 1;

                objFinal.Dispose();
                image.Dispose();
                zoomObj.Dispose();
                return 0;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
                return -1;
            }
        }

        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mParamChangedDe += ParamChanged;
                mToolParam.mDrawShapeROIDe += CreateRoi;
                mToolParam.mGenNccModelDe += GenNccModel;
                mToolParam.mDeletPaintRoiDe += DeleRoi;
                mToolParam.mDrawRoiDel += DrawRoi;
                mToolParam.mDrawRoi2Del += DrawRoi2;
                mToolParam.mSureModelDe += SureModel;
                mToolParam.mDeletModelDe += DeleModel;
                mToolParam.mLoadImageDel += LoadImage;
                mToolParam.mDrawRoiFindRegionDel += DrawFindRegion;
            }
            else
            {
                mToolParam.mParamChangedDe = null;
                mToolParam.mDrawShapeROIDe = null;
                mToolParam.mGenNccModelDe = null;
                mToolParam.mDeletPaintRoiDe = null;
                mToolParam.mDrawRoiDel = null;
                mToolParam.mSureModelDe = null;
                mToolParam.mDeletModelDe = null;
                mToolParam.mLoadImageDel = null;
                mToolParam.mDrawRoi2Del = null;
                mToolParam.mDrawRoiFindRegionDel = null;
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

        public void GenNccModel(HObject obj1, List<StepInfo> StepInfoList, out HObject objShow)
        {
            HTuple area, row, column;
            HOperatorSet.GenEmptyObj(out objShow);
            try
            {
                mToolParam.ResultString = "";
                HObject objFinal;
                if (mToolParam.mImageSourceStep > -1)
                    objFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
                else
                    objFinal = obj1;
                HTuple chanl;
                HOperatorSet.CountChannels(objFinal, out chanl);
                if (chanl != 1)
                {
                    mToolParam.ResultString = "非单通道图片，请重新输入图片源";
                    return;
                }
                if (mToolParam.mModelRegion.Area <= 0)
                {
                    mToolParam.ResultString = "请先绘制模板区域";
                    return;
                }
                HObject obj2, obj4;
                HTuple a, b, c, d, HomMat2D;
                HTuple w, h;
                HObject zoom;
                HObject zoomImage;
                HOperatorSet.ReduceDomain(objFinal, mToolParam.mModelRegion, out obj2);
                HOperatorSet.GetImageSize(objFinal, out w, out h);
                HOperatorSet.ZoomImageSize(obj2, out zoom, w * mToolParam.ZoomParam, h * mToolParam.ZoomParam, "constant");
                HOperatorSet.ZoomImageSize(objFinal, out zoomImage, w * mToolParam.ZoomParam, h * mToolParam.ZoomParam, "constant");
                model = new HTuple();
                HOperatorSet.CreateNccModel(zoom, mToolParam.mNumLel, (new HTuple(mToolParam.CreateStartAngle)).TupleRad(), (new HTuple(mToolParam.CreateExtentAngle)).TupleRad()
                    , "auto", mToolParam.CreatePolarity, out model);
                HOperatorSet.FindNccModel(zoomImage, model, (new HTuple(mToolParam.FindStartAngle)).TupleRad(), (new HTuple(mToolParam.FindExtentAngle)).TupleRad(), 0.6, 1,
                    mToolParam.Overloap, "true", 0, out a, out b, out c, out d);
                if (a.Length < 1)
                {
                    mDrawWind.ClearWindow();
                    mDrawWind.SetColor("red");
                    mDrawWind.SetTposition(50, 50);
                    mDrawWind.WriteString("模型创建失败");
                    return;
                }
                HTuple a1, r1, c1;
                HOperatorSet.AreaCenter(zoom, out a1, out r1, out c1);
                zoom.Dispose();
                zoomImage.Dispose();
                mToolParam.mModelCenterRow = r1 / mToolParam.ZoomParam;
                mToolParam.mModelCenterCol = c1 / mToolParam.ZoomParam;
                double cc = a / mToolParam.ZoomParam;
                double dd = b / mToolParam.ZoomParam;
                HOperatorSet.VectorAngleToRigid(mToolParam.mModelCenterRow, mToolParam.mModelCenterCol, 0, cc, dd, c, out HomMat2D);
                HOperatorSet.AffineTransRegion(mToolParam.mModelRegion, out obj4, HomMat2D, "nearest_neighbor");

                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mModelCenterRow;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mModelCenterCol;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = a.D / mToolParam.ZoomParam;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = b.D / mToolParam.ZoomParam;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[4] = c.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[5] = d.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[6] = 1;
                mDrawWind.ClearWindow();
                mDrawWind.SetDraw("margin");
                mDrawWind.DispObj(obj1);
                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColor("red");
                mDrawWind.DispObj(obj4);
                mDrawWind.SetColor("blue");
                mDrawWind.DispObj(mToolParam.mModelRegion);
                mDrawWind.SetColor("green");
                mDrawWind.SetTposition(50, 50);
                mDrawWind.WriteString("模型创建成功");

                objShow = obj4;
                mTempXlds = obj4;
                obj2.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void SureModel(HObject obj)
        {
            if (model != null) 
            {
                mToolParam.NccModel = new HNCCModel(model.H);
                mToolParam.mIsSaveShapeModelImage = true;
                mToolParam.mImage = new HImage(obj);
            }

        }

        public void DeleModel()
        {
            //模板区域置空
            mToolParam.mModelRegion.GenEmptyRegion();
            //模板恢复默认
            mToolParam.NccModel = new HNCCModel(strConfigPath + "Config2.dat");
            mToolParam.mImage.GenEmptyObj();
            mToolParam.mIsSaveShapeModelImage = false;
        }

        public int FindShapeModel(HObject obj1, List<StepInfo> StepInfoList)
        {
            mToolParam.ResultString = "";
            HObject objFinal;
            if (mToolParam.mImageSourceStep > -1)
                objFinal = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut.CopyObj(1,1);
            else
                objFinal = obj1.CopyObj(1, 1);
            HTuple chanl;
            HOperatorSet.CountChannels(objFinal, out chanl);
            if (chanl != 1)
            {
                mToolParam.ResultString = "非单通道图片，请重新输入图片源";
                return 1;
            }

            try
            {
                HObject zoomObj,reduceObj;
                HTuple w, h;
                HOperatorSet.GetImageSize(objFinal, out w, out h);
                if (mToolParam.mFindRegion.Area > 0)
                {
                    HOperatorSet.ReduceDomain(objFinal, mToolParam.mFindRegion, out reduceObj);
                    objFinal.Dispose();
                    objFinal = reduceObj;
                }
                HImage image;
                HOperatorSet.ZoomImageSize(objFinal, out zoomObj, w * mToolParam.ZoomParam, h * mToolParam.ZoomParam, "constant");
                image = new HImage(zoomObj);

                HTuple row = new HTuple();
                HTuple column = new HTuple();
                HTuple angle = new HTuple();
                HTuple score = new HTuple();
                HTuple s1, s2;
                mToolParam.NccModel.SetNccModelParam("timeout", mToolParam.TimeOut);
                HOperatorSet.CountSeconds(out s1);
                         
                mToolParam.NccModel.FindNccModel(image, (new HTuple(mToolParam.FindStartAngle)).TupleRad().D, (new HTuple(mToolParam.FindExtentAngle)).TupleRad().D, (HTuple)mToolParam.FindScore, 1,
                    mToolParam.Overloap, "true", 0, out row, out column, out angle, out score);
                HOperatorSet.CountSeconds(out s2);
                if (row.Length != 1)
                {
                    mDrawWind.ClearWindow();
                    mToolParam.ResultString = "灰度匹配失败!";
                    image.Dispose();
                    return -1;
                }
                mToolParam.ResultString = "匹配耗时：" + ((s2.D - s1.D) * 1000).ToString("0.00") + "ms ;\r\n" + "匹配结果：分数：" + score.D.ToString("0.000") + "\r\n" + " Row:=" + row.D.ToString("0.00") + " Column:="
                    + column.D.ToString("0.00") + " 角度：=" + angle.D.ToString("0.00");
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[0] = mToolParam.mModelCenterRow;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[1] = mToolParam.mModelCenterCol;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[2] = row.D / mToolParam.ZoomParam;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[3] = column.D / mToolParam.ZoomParam;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[4] = angle.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[5] = score.D;
                mToolParam.StepInfo.mToolRunResul.mParamOutPut[6] = 1;
                HTuple HomMat2D;
                HObject affine1;
                HOperatorSet.VectorAngleToRigid(mToolParam.mModelCenterRow, mToolParam.mModelCenterCol, 0, row / mToolParam.ZoomParam, column / mToolParam.ZoomParam, angle, out HomMat2D);
                HOperatorSet.AffineTransRegion(mToolParam.mModelRegion, out affine1, HomMat2D, "nearest_neighbor");
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(objFinal);
                mDrawWind.SetLineWidth(2);
                mDrawWind.SetColored(12);
                mDrawWind.SetDraw("margin");
                mDrawWind.SetColor("green");
                mDrawWind.DispObj(affine1);
                mDrawWind.SetColor("magenta");
                mDrawWind.DispObj(mToolParam.mFindRegion);

                image.Dispose();
                affine1.Dispose();
                HomMat2D.Dispose();
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
                return 1;


        }

        public void DrawRoi(HObject obj1, int mType, string mRoiType, int mMarkSize, out HObject showRegion)
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
                            if (mType == 0)
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
                        mDrawWind.SetRgba(255, 0, 0, 60);
                        mDrawWind.DispObj(obj1);
                        mDrawWind.DispObj(mToolParam.mModelRegion);
                        mDrawWind.DispObj(obj);
                        HOperatorSet.SetSystem("flush_graphic", "true");
                        mDrawWind.SetTposition(50, 50);
                        mDrawWind.WriteString("");

                    }
                }
                mDrawWind.SetRgba(255, 0, 0, 60);
                mDrawWind.DispObj(obj1);
                mDrawWind.DispObj(mToolParam.mModelRegion);
                showRegion = mToolParam.mModelRegion;
                Thread.Sleep(20);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public int DrawFindRegion(HObject obj1, string mRoiType, out HObject showRegion)
        {
            mDrawWind.ClearWindow();
            mDrawWind.SetDraw("fill");
            mDrawWind.SetRgba(255, 0, 0, 120);
            if (mToolParam.mFindRegion != null) 
            {
                mDrawWind.DispObj(mToolParam.mFindRegion);
            }
            else
            {
                mToolParam.mFindRegion.GenEmptyObj();
            }

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
                    HOperatorSet.Union2(mToolParam.mFindRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mFindRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle1")
                {
                    double row, column, row2, column2;
                    HObject obj;
                    mDrawWind.DrawRectangle1(out row, out column, out row2, out column2);
                    HOperatorSet.GenRectangle1(out obj, row, column, row2, column2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mFindRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mFindRegion = new HRegion(temp);
                }
                else if (mRoiType == "rectangle2")
                {
                    double row, column, phi, length1, length2;
                    HObject obj;
                    mDrawWind.DrawRectangle2(out row, out column, out phi, out length1, out length2);
                    HOperatorSet.GenRectangle2(out obj, row, column, phi, length1, length2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mFindRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mFindRegion = new HRegion(temp);
                }
                else if (mRoiType == "ellipse")
                {
                    double row, column, phi, radius1, radius2;
                    HObject obj;
                    mDrawWind.DrawEllipse(out row, out column, out phi, out radius1, out radius2);
                    HOperatorSet.GenEllipse(out obj, row, column, phi, radius1, radius2);
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mFindRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mFindRegion = new HRegion(temp);
                }
                else if (mRoiType == "any")
                {
                    HRegion obj = mDrawWind.DrawRegion();
                    HObject temp = new HObject();
                    temp.GenEmptyObj();
                    HOperatorSet.Union2(mToolParam.mFindRegion, obj, out temp);
                    showRegion = temp;
                    mToolParam.mFindRegion = new HRegion(temp);
                }

                mDrawWind.SetRgba(255, 0, 0, 120);
                mDrawWind.ClearWindow();
                mDrawWind.DispObj(mToolParam.mFindRegion);
                return 0;
            }
            catch (System.Exception ex)
            {
                mToolParam.ResultString = ex.Message;
                return -1;
            }
        }

        public void DeleRoi()
        {
            mToolParam.mModelRegion.Dispose();
            mToolParam.mModelRegion.GenEmptyRegion();
        }

    }


}
