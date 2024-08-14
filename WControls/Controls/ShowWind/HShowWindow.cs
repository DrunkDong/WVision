using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using WCommonTools;

namespace WControls
{
    [ToolboxItem(false)]
    public partial class HShowWindow : UserControl
    {
        HSmartWindowControl window;
        bool mIsInit;
        HImage mCurrImage;
        int mImageHeight;
        int mImageWidth;

        public HWindow ShowWindow
        {
            get{ return Window.HalconWindow; }
        }

        public bool IsInit
        {
            get => mIsInit;
            set => mIsInit = value;
        }
        public HSmartWindowControl Window
        {
            get => window;
            set => window = value;
        }
        public HImage CurrImage
        {
            get => mCurrImage;
            set => mCurrImage = value;
        }

        public HShowWindow()
        {
            HOperatorSet.SetWindowAttr("background_color", "gray");
            InitializeComponent();
            mImageHeight = 0;
            mImageWidth = 0;
            mIsInit = false;
            window = new HSmartWindowControl();
            Window.Location = new Point(0, 0);
            Window.Dock = DockStyle.Fill;
            this.Controls.Add(Window);
            Window.MouseWheel += Window.HSmartWindowControl_MouseWheel;

        }


        public void DispObj(HObject obj)
        {
            if (obj != null && obj.IsInitialized()) 
            {
                CurrImage?.Dispose();
                CurrImage = new HImage(obj);
                ShowWindow.DispObj(obj);
                CurrImage.GetImageSize(out HTuple width, out HTuple height);
                if (mImageHeight != height.I || mImageWidth != width.I) 
                {
                    window.SetFullImagePart();
                    mImageHeight = height.I;
                    mImageWidth = width.I;
                }
            }
        }


        private void 自适应缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Window.SetFullImagePart();
        }

        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HImage save = new HImage();
                if (mCurrImage == null || !mCurrImage.IsInitialized())
                    return;
                save = mCurrImage.CopyObj(1, 1);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "BMP图像|*.bmp|所有文件|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (String.IsNullOrEmpty(sfd.FileName))
                        return;
                    save.WriteImage("bmp", 0, sfd.FileName);
                    save.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex);
            }
        }
    }
}
