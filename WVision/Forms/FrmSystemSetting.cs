using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace WVision
{
    public partial class FrmSystemSetting : UIForm
    {
        public FrmSystemSetting()
        {
            InitializeComponent();
        }
        Machine mMachine;
        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            mMachine = Machine.GetInstance();
            InitParam();
            this.ActiveControl = uiButton_Save;
        }

        private void UiButton_SelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择匹配目录"; ;//左上角提示
            string path = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;//获取选中文件路径
                if (path != "")
                {
                    uiRichTextBox_SavePath.Text = path;
                }
            }
        }

        private void GetParam()
        {
            mMachine.SettingInfo.SaveImagePath = uiRichTextBox_SavePath.Text;
            mMachine.SettingInfo.ModbusIP = uiipTextBox_Modbus.Text;
            //mMachine.SettingInfo.PlateIP = uiipTextBox_Plate.Text;
            //mMachine.SettingInfo.RobotIP = uiipTextBox_Robot.Text;
            //mMachine.SettingInfo.MotionCardIP = uiipTextBox_MotionCardIP.Text;
            mMachine.SettingInfo.ModbusPort = (int)numericUpDown_ModbusPort.Value;
            //mMachine.SettingInfo.PlatePort = (int)numericUpDown_PlatePort.Value;
            //mMachine.SettingInfo.RobotPort = (int)numericUpDown_RobotPort.Value;
            mMachine.SettingInfo.SaveOKMode = uiComboBox_SaveOKMode.SelectedIndex;
            mMachine.SettingInfo.SaveNGMode = uiComboBox_SaveNgMode.SelectedIndex;
            mMachine.SettingInfo.SaveImagePath = uiRichTextBox_SavePath.Text;
            mMachine.SettingInfo.LowYeild = (double)numericUpDown_LowYield.Value;
            mMachine.SettingInfo.LowDiskCapacity = (double)numericUpDown_LowDiskCapacity.Value;
            mMachine.SettingInfo.AnomalyCount = (double)numericUpDown_AnomalyCount.Value;
        }

        private void InitParam()
        {
            uiRichTextBox_SavePath.Text = mMachine.SettingInfo.SaveImagePath;
            uiipTextBox_Modbus.Text = mMachine.SettingInfo.ModbusIP;
            //uiipTextBox_Plate.Text = mMachine.SettingInfo.PlateIP;
            //uiipTextBox_Robot.Text = mMachine.SettingInfo.RobotIP;
            //uiipTextBox_MotionCardIP.Text = mMachine.SettingInfo.MotionCardIP;
            numericUpDown_ModbusPort.Value = mMachine.SettingInfo.ModbusPort;
            //numericUpDown_PlatePort.Value = mMachine.SettingInfo.PlatePort;
            //numericUpDown_RobotPort.Value = mMachine.SettingInfo.RobotPort;
            uiComboBox_SaveOKMode.SelectedIndex = mMachine.SettingInfo.SaveOKMode;
            uiComboBox_SaveNgMode.SelectedIndex = mMachine.SettingInfo.SaveNGMode;
            uiRichTextBox_SavePath.Text = mMachine.SettingInfo.SaveImagePath;
            numericUpDown_LowYield.Value = (decimal)mMachine.SettingInfo.LowYeild;
            numericUpDown_LowDiskCapacity.Value = (decimal)mMachine.SettingInfo.LowDiskCapacity;
            numericUpDown_AnomalyCount.Value = (decimal)mMachine.SettingInfo.AnomalyCount;
        }

        private void UiButton_Save_Click(object sender, EventArgs e)
        {
            GetParam();
            mMachine.SerializeFuc(mMachine.SettingInfoSavePath, mMachine.SettingInfo);
            mMachine.SavePath = mMachine.SettingInfo.SaveImagePath;
        }
    }
}
