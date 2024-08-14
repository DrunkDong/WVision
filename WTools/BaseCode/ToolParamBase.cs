using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTools
{
    [Serializable]
    public abstract class ToolParamBase
    {
        protected ToolParamBase()
        {

        }

        public virtual bool ForceOK { get; set; }

        public virtual int NgReturnValue { get; set; }

        public abstract StepInfo StepInfo { get; set; }

        public abstract string ShowName { get; set; }

        public abstract string ToolName { get; set; }

        public abstract ToolType ToolType { get; set; }

        public abstract JumpInfo StepJumpInfo { get; set; }

        public virtual string ResultString { get; set; }

    }
}
