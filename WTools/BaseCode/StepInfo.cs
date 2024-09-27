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
        public List<string> mShowString;
        public ToolRunResult mToolRunResul;
        public int mStepIndex;
        public ToolType mToolType;
        public ToolResultType mToolResultType;
        public int mInnerToolID;

        [NonSerialized]
        public HObject mNGResult;
        [NonSerialized]
        public HObject mShowResult;

        public StepInfo()
        {
            HOperatorSet.GenEmptyObj(out mNGResult);
            HOperatorSet.GenEmptyObj(out mShowResult);
            mShowName = "";
            mShowString = new List<string>();
            mToolRunResul = new ToolRunResult();
            mStepIndex = 0;
            mInnerToolID = 0;
            mToolType = new ToolType();
            mToolResultType = new ToolResultType();
        }

        public void ClearStepInfo()
        {
            HOperatorSet.GenEmptyObj(out mNGResult);
            HOperatorSet.GenEmptyObj(out mShowResult);
            mShowString = new List<string>();
            mToolRunResul.Clear();
        }
    }
}
