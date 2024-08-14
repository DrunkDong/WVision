using System;
using System.Collections.Generic;
using System.Drawing;
using HalconDotNet;

namespace WTools
{
    [Serializable]
    public class ToolRunResult
    {
        public HObject mRegionOutPut;
        public HObject mImageOutPut;
        public HTuple mCameraParam;
        public HTuple mCameraPose;
        public double[] mParamOutPut;
        public List<string> mMesDataOutPut;
        public Bitmap mBitmap;

        public ToolRunResult()
        {
            mParamOutPut = new double[8];
            mMesDataOutPut = new List<string>();
        }

        public void Clear() 
        {
            HOperatorSet.GenEmptyObj(out mRegionOutPut);
            HOperatorSet.GenEmptyObj(out mImageOutPut);
            mCameraParam = new HTuple();
            mCameraPose = new HTuple();
            mParamOutPut = new double[8];
            mMesDataOutPut = new List<string>();
        } 
    }
}
