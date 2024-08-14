using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace WTools
{
    [Serializable]
    public class StepInfo
    {
        public string mShowName;
        public HObject mNGResult;
        public HObject mShowResult;
        public List<string> mShowString;
        public ToolRunResult mToolRunResul;
        public int mStepIndex;
        public ToolType mToolType;
        public int mInnerToolID;

        public StepInfo() 
        {
            mShowName = "";
            mShowString = new List<string>();
            mToolRunResul = new ToolRunResult();
            mStepIndex = 0;
            mInnerToolID = 0;
            mToolType = new ToolType();
        }
    }
}
