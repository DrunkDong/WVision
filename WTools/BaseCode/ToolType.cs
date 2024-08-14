using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTools
{
    public enum ToolType
    {
        CameraImage = 1,
        RegionBlob = 2,
        Calibration = 3,
        ShapeModle = 4,
        FindLine = 5,
        FindCircle = 6,
        FindPoint = 7,
        TransformPoint = 8,
        TransformLine = 9,
        DistancePP = 10,
        DistancePL = 11,
        DistanceLL = 12,
        DecomposeRGB = 13,
        Threshold = 14,
        Rectangle1ToLine = 15,
        Rectangle2ToLine = 16,
        CheckLT = 17,
        CheckBQ = 18,
        HClassifiyAI = 19,
        VClassifiyAI = 20,
        HObjecDetect1 = 21,
        HObjecDetect2 = 22,
        Vrectangle1AI = 23,
        HsemanticAI = 24,
        VsemanticAI = 25,
        AngleLL = 26,
        DynThreshold = 27,
        Fft = 28,
        ScaleImage = 29,
        XiuDianCheck = 30,
        ScriptCode = 31,
        NccShapeModel = 32,
        CheckBQ2 = 33,
        CheckLP = 34,
        CheckYS = 35,
        CheckCAl = 36,
        CheckRGB = 37,
        GenBitmap = 38,
        FindLinePair = 39,
        FindOCRShape = 40,
        MeasureLinePairs = 41,
        CompareDistance = 42,
        CropImage = 43,
        DeepOcr = 44,
        CorrectImage = 45,
        None = 999
    }
}
