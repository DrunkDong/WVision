using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTools
{

    public enum RoiType
    {
        None = 0,
        Rectangle1 = 1,
        Rectangle2 = 2,
        Circle = 3,
        Line = 4
    }

    [Serializable]
    public class RoiCircle
    {
        public double mRow;
        public double mColumn;
        public double mRadius;

        public RoiCircle()
        {
            mRow = 0;
            mColumn = 0;
            mRadius = 0;
        }
    }

    [Serializable]
    public class Rectangle1
    {
        public double mRow1;
        public double mColumn1;
        public double mRow2;
        public double mColumn2;

        public Rectangle1()
        {
            mRow1 = 0;
            mColumn1 = 0;
            mRow2 = 0;
            mColumn2 = 0;
        }
    }
    [Serializable]
    public class Rectangle2
    {
        public double mRow;
        public double mColumn;
        public double mLength1;
        public double mLength2;
        public double mPhi;

        public Rectangle2()
        {
            mRow = 0;
            mColumn = 0;
            mLength1 = 0;
            mLength2 = 0;
            mPhi = 0;
        }
    }
    [Serializable]
    public class Line
    {
        public double mRow1;
        public double mColumn1;
        public double mRow2;
        public double mColumn2;

        public Line()
        {
            mRow1 = 0;
            mColumn1 = 0;
            mRow2 = 0;
            mColumn2 = 0;
        }
    }
}
