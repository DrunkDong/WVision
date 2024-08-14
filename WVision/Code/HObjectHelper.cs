using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVision
{
    public static class HObjectHelper
    {
        public static bool ObjectValided(HObject Obj)
        {
            if (Obj == null)
            {
                return false;
            }
            if (!Obj.IsInitialized())
            {
                return false;
            }
            if (Obj.CountObj() < 1)
            {
                return false;
            }
            return true;
        }
    }
}
