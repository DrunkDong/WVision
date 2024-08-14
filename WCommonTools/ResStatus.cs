using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCommonTools
{
    public enum ResStatus
    {
        DeviceInvalid = -30,
        DeviceError = -29,
        DeviceTimeout = -28,
        DeviceOpFaild = -27,
        OpFailed = -20,
        ReadFailed = -19,
        WriteFailed = -18,
        NoFile = -17,
        NoParam = -16,
        ExceptionErr = -8,
        PositionErr = -7,
        CallBackErr = -6,
        ResponseErr = -5,
        ResultErr = -4,
        ParamErr = -3,
        RunErr = -2,
        Error = -1,
        OK = 0,
        IsBusying = 1,
        NotInProcedure = 2,
        TimeOut = 3,
        RunTimeOut = 4,
        OpTimeOut = 5,
        ResponseTimeOut = 6,
        MoveTimeOut = 7,
        SaveTimeOut = 8,
        Virtual = 9,
        OutOfLimit = 20
    }
}
