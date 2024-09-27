using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTools
{
    [Serializable]
    public enum ToolResultType
    {
        Image = 1,
        ImageAlignData = 2,
        ImageBitmap = 3,
        Line = 4,
        Circle = 5,
        Point = 6,
        DoubleValue = 7,
        Region = 8,
        OCR = 9,
        Bitmap = 9,
        None = 999
    }
}
