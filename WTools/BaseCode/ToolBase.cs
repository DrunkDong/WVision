using System;
using System.Collections.Generic;
using System.Drawing;
using WCommonTools;
using HalconDotNet;

namespace WTools
{
    [Serializable]
    public abstract class ToolBase
    {
        public abstract ToolParamBase ToolParam { get; set; }
        public abstract int ParamChanged(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj);
        public abstract int DebugRun(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo);
        public abstract int ToolRun(HObject obj1, List<StepInfo> StepInfoList, bool ShowObj, out JumpInfo StepJumpInfo);
        public abstract ResStatus SetRunWind(HTuple DefectWind);
        public abstract ResStatus BindDelegate(bool IsBind);
        public abstract ResStatus SetDebugWind(HTuple DebugWind, HWindow DrawWind);
        public abstract ResStatus Dispose();

        public virtual ResStatus InitAiResources()
        {
            return ResStatus.Virtual;
        }

        protected ToolBase()
        {

        }
    }
}
