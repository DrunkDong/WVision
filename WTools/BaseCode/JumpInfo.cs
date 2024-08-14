using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTools
{
    [Serializable]
    public class JumpInfo
    {
        public bool mEndRun = false;
        public bool mJumpEnable = false;
        public bool mJumpFlag = false;
        public int mJumpStepID = 0;
        public bool mNGJumpFlag = true;
        public int mStepID = 0;
        public string mAiLabel = "";
        public ToolType mToolType = ToolType.None;
        public string OcrName = "";
        public HObject mShowRegion = null;

    }
}
