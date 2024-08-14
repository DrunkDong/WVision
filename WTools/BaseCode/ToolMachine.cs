using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCommonTools;
using HalconDotNet;
using System.Drawing;
using System.Windows.Forms;

namespace WTools
{
    public class ToolMachine : SingletonTemplate<ToolMachine>
    {
        HObject mToolCurrImage;
        Bitmap mToolCurrBitmap;
        HImage mToolCurrHimage;
        DataGridView mDataGridView;
        HWindow mDrawWind;
        Panel mControl;
        List<HObject> mShowRegionList;
        HObject mCurrShowImage;

        public delegate void ChangeStateDel(int row, int state);
        public delegate void ChangeToolNameDel(int row, string name);
        public delegate void ChangeToolCostDel(int row, string cost);
        public delegate void EnableControlDel(bool isEnable);

        public ChangeStateDel mChangeState;
        public ChangeToolNameDel mChangeToolName;
        public ChangeToolCostDel mChangeToolCostTime;
        public EnableControlDel mEnableControlDel;

        public HObject ToolCurrImage
        {
            get => mToolCurrImage;
            set => mToolCurrImage = value;
        }
        public Bitmap ToolCurrBitmap
        {
            get => mToolCurrBitmap;
            set => mToolCurrBitmap = value;
        }
        public DataGridView DataGridView
        {
            get => mDataGridView;
            set => mDataGridView = value;
        }
        public HWindow DrawWind
        {
            get => mDrawWind;
            set => mDrawWind = value;
        }
        public HImage ToolCurrHimage
        {
            get => mToolCurrHimage;
            set => mToolCurrHimage = value;
        }
        public Panel Control
        {
            get => mControl;
            set => mControl = value;
        }
        public List<HObject> ShowRegionList
        {
            get => mShowRegionList;
            set => mShowRegionList = value;
        }
        public HObject CurrShowImage
        {
            get => mCurrShowImage;
            set => mCurrShowImage = value;
        }

        public ToolMachine()
        {
            mShowRegionList = new List<HObject>();
        }
    }
}
