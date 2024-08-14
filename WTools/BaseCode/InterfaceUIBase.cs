using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using WCommonTools;

namespace WTools
{
    public interface InterfaceUIBase
    {
        ToolParamBase ToolParam { get; set; }
        List<StepInfo> StepInfoList { get; set; }
        int StepIndex { get; set; }
        ResStatus SetDebugRunWind(HTuple mShowWind,HWindow mDrawWind);
        ResStatus InitRoiShow();
        ResStatus ShowCurrMes();
    }
}
