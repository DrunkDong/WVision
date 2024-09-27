using WCommonTools;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTools
{
    [Serializable]

    public class ToolGenBitmapParam : ToolParamBase
    {
        public int mImageSourceStep;//图像源
        public int mImageSourceMark;//图像源

        private StepInfo mStepInfo;
        private string mShowName;
        private string mToolName;
        private ToolType mToolType;
        private JumpInfo mStepJumpInfo;
        private string mResultString;
        private bool mForceOK;
        private int mNgReturnValue;
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

        public override string ResultString
        {
            get => mResultString;
            set => mResultString = value;
        }

        public delegate int ParamChangedDe(HObject obj1,List<StepInfo> StepInfoList, bool ShowObj);
        [NonSerialized]
        public ParamChangedDe mParamChangedDe;

        public ToolGenBitmapParam()
        {
            mStepInfo = new StepInfo();
            mStepInfo.mToolType = ToolType.GenBitmap;
            mToolType = ToolType.GenBitmap;
            mStepInfo.mToolResultType = ToolResultType.Bitmap;
            mToolResultType = ToolResultType.Bitmap;

            StepInfo.mShowName = "Bitmap转换";
            mShowName = "Bitmap转换";
            mToolName = "Bitmap转换";
            mStepJumpInfo = new JumpInfo();
            mResultString = "";

            mImageSourceStep = -1;
            mImageSourceMark = -1;
            NgReturnValue = 1;
        }
    }

    public class ToolGenBitmap : ToolBase
    {
        ToolGenBitmapParam mToolParam;
        HTuple mDebugWind;
        HTuple mDefectWind;
        HWindow mDrawWind;

        public override ToolParamBase ToolParam
        {
            get => mToolParam;
            set => mToolParam = value as ToolGenBitmapParam;
        }

        public ToolGenBitmap(ToolParamBase toolParam)
        {
            mToolParam = toolParam as ToolGenBitmapParam;
            BindDelegate(true);
        }
        public override int DebugRun(HObject objj1,List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo)
        {
            StepJumpInfo = new JumpInfo();
            mToolParam.ResultString = "";
            //是否屏蔽功能
            if (mToolParam.ForceOK)
                return 0;
            GenBitmap(objj1, StepInfoList);
            return 0;
        }

        public override ResStatus Dispose()
        {
            mToolParam.StepInfo.mToolRunResul.mImageOutPut?.Dispose();
            return ResStatus.OK;
        }

        public override int ParamChanged(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj)
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
            mToolParam.ResultString = "";
            //是否屏蔽功能
            if (mToolParam.ForceOK)
                return 0;
            HObject obj;
            if (mToolParam.mImageSourceStep > -1)
                obj = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
            else
                obj = obj1;
            if (obj == null)
            {
                mToolParam.StepInfo.mToolRunResul.mBitmap = null;
                return mToolParam.NgReturnValue;
            }
            HTuple channel;
            HOperatorSet.CountChannels(obj, out channel);
            Bitmap map = null;
            if (channel == 3)
                map = GenertateRGBBitmap45(obj);
            else if (channel == 1)
                map = GenertateGrayBitmap45(obj);
            if (map == null)
                return mToolParam.NgReturnValue;
            mToolParam.StepInfo.mToolRunResul.mBitmap = map;
            return 0;
        }



        public override ResStatus BindDelegate(bool IsBind)
        {
            if (IsBind)
            {
                mToolParam.mParamChangedDe += ParamChanged;
            }
            else
            {
                mToolParam.mParamChangedDe = null;
            }

            return ResStatus.OK;
        }
        private Bitmap GenertateRGBBitmap45(HObject image)
        {
            try
            {
                HTuple hred, hgreen, hblue, type, width, height;

                HOperatorSet.GetImagePointer3(image, out hred, out hgreen, out hblue, out type, out width, out height);
                Bitmap res = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                Rectangle rect = new Rectangle(0, 0, width, height);
                BitmapData bitmapData = res.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                int imglength = width * height;
                unsafe
                {
                    byte* bptr = (byte*)bitmapData.Scan0;
                    byte* r = ((byte*)hred.L);//4.5版本以下使用的是i(int)
                    byte* g = ((byte*)hgreen.L);
                    byte* b = ((byte*)hblue.L);
                    for (int i = 0; i < imglength; i++)
                    {
                        bptr[i * 4] = (b)[i];
                        bptr[i * 4 + 1] = (g)[i];
                        bptr[i * 4 + 2] = (r)[i];
                        bptr[i * 4 + 3] = 255;
                    }
                }

                res.UnlockBits(bitmapData);
                return res;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog("trans Image Error \r" + ex);
                return null;
            }
        }

        private Bitmap GenertateGrayBitmap45(HObject image)
        {
            try
            {
                HTuple hpoint, type, width, height;
                Bitmap res;
                const int Alpha = 255;
                long[] ptr = new long[2];
                HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);

                res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                ColorPalette pal = res.Palette;
                for (int i = 0; i <= 255; i++)
                {
                    pal.Entries[i] = Color.FromArgb(Alpha, i, i, i);
                }
                res.Palette = pal;
                Rectangle rect = new Rectangle(0, 0, width, height);
                BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                ptr[0] = bitmapData.Scan0.ToInt64();
                ptr[1] = hpoint.L;
                if (width % 4 == 0)
                    CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
                else
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        ptr[1] += width;
                        CopyMemory(ptr[0], ptr[1], width * PixelSize);
                        ptr[0] += bitmapData.Stride;
                    }
                }
                res.UnlockBits(bitmapData);
                return res;
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteExceptionLog("trans Image Error \r" + ex);
                return null;
            }

        }

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
        private extern static void CopyMemory(long dest, long source, long size);

        private void GenBitmap(HObject obj1, List<StepInfo> StepInfoList)
        {
            HObject obj;
            if (mToolParam.mImageSourceStep > -1)
                obj = StepInfoList[mToolParam.mImageSourceStep - 1].mToolRunResul.mImageOutPut;
            else
                obj = obj1;
            if (obj == null)
            {
                mToolParam.StepInfo.mToolRunResul.mBitmap = null;
                return;
            }
            HTuple channel;
            HOperatorSet.CountChannels(obj, out channel);
            Bitmap map = null;
            if (channel == 3)
                map = GenertateRGBBitmap45(obj);
            else if (channel == 1)
                map = GenertateGrayBitmap45(obj);
            mToolParam.StepInfo.mToolRunResul.mBitmap = map;
        }
    }
}
