using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WCommonTools;


namespace WTools
{
    public class ToolOperation:SingletonTemplate<ToolOperation>
    {
        public ToolOperation()
        {
        }

        //序列化工具
        public ResStatus SerializableTool(ToolParamBase tool, out byte[] buff)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream serializationStream = new MemoryStream();
            buff = null;
            try
            {
                switch (tool.ToolType)
                {
                    case ToolType.RegionBlob:
                        formatter.Serialize(serializationStream, (ToolBlobParam)tool);
                        break;
                    case ToolType.HObjecDetect1:
                        formatter.Serialize(serializationStream, (ToolHalconR1Param)tool);
                        break;
                    case ToolType.FindLine:
                        formatter.Serialize(serializationStream, (ToolCheckLineParam)tool);
                        break;
                    case ToolType.AngleLL:
                        formatter.Serialize(serializationStream, (ToolAngleLLParam)tool);
                        break;
                    case ToolType.DistanceLL:
                        formatter.Serialize(serializationStream, (ToolDistanceLLParam)tool);
                        break;
                    case ToolType.ShapeModle:
                        formatter.Serialize(serializationStream, (ToolFindShapeModelParam)tool);
                        break;
                    case ToolType.ScaleImage:
                        formatter.Serialize(serializationStream, (ToolScaleImageParam)tool);
                        break;
                    case ToolType.HsemanticAI:
                        formatter.Serialize(serializationStream, (ToolHalconS1Param)tool);
                        break;
                    case ToolType.NccShapeModel:
                        formatter.Serialize(serializationStream, (ToolFindNccShapeModelParam)tool);
                        break;
                    case ToolType.GenBitmap:
                        formatter.Serialize(serializationStream, (ToolGenBitmapParam)tool);
                        break;
                    case ToolType.DecomposeRGB:
                        formatter.Serialize(serializationStream, (ToolDecomposeRGBParam)tool);
                        break;
                    case ToolType.CompareDistance:
                        formatter.Serialize(serializationStream, (ToolCompareDistanceParam)tool);
                        break;
                    case ToolType.CropImage:
                        formatter.Serialize(serializationStream, (ToolCropImageParam)tool);
                        break;
                    case ToolType.FindCircle:
                        formatter.Serialize(serializationStream, (ToolFindCircleParam)tool);
                        break;
                    case ToolType.DistancePL:
                        formatter.Serialize(serializationStream, (ToolDistancePLParam)tool);
                        break;
                    case ToolType.DistancePP:
                        formatter.Serialize(serializationStream, (ToolDistancePPParam)tool);
                        break;
                    case ToolType.HClassifiyAI:
                        formatter.Serialize(serializationStream, (ToolHalconClassifyParam)tool);
                        break;
                    case ToolType.DeepOcr:
                        formatter.Serialize(serializationStream, (ToolHalconDeepOcrParam)tool);
                        break;
                    case ToolType.CorrectImage:
                        formatter.Serialize(serializationStream, (ToolCorrectImageParam)tool);
                        break;
                    default:
                        break;
                }

                long length = serializationStream.Length;
                buff = new byte[(length + 8L) + 4L];

                byte[] a = BitConverter.GetBytes((long)((length + 8L) + 4L));
                byte[] b = BitConverter.GetBytes((int)tool.ToolType);
                byte[] c = serializationStream.ToArray();

                Array.Copy(a, buff, 8L);
                Array.Copy(b, 0, buff, 8L, 4L);
                Array.Copy(c, 0, buff, 12L, length);
                return ResStatus.OK;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return ResStatus.Error;
            }
        }

        //反序列化工具
        public ResStatus DeserializableTool(byte[] buff, out ToolParamBase tool)
        {
            tool = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream serializationStream = new MemoryStream();
                int num = BitConverter.ToInt32(buff, 0);
                serializationStream.Write(buff, 4, buff.Length - 4);
                serializationStream.Seek(0L, SeekOrigin.Begin);
                ToolType type = (ToolType)num;

                switch (type)
                {
                    case ToolType.RegionBlob:
                        tool = (ToolBlobParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.HObjecDetect1:
                        tool = (ToolHalconR1Param)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.FindLine:
                        tool = (ToolCheckLineParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.AngleLL:
                        tool = (ToolAngleLLParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.DistanceLL:
                        tool = (ToolDistanceLLParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.ShapeModle:
                        tool = (ToolFindShapeModelParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.ScaleImage:
                        tool = (ToolScaleImageParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.HsemanticAI:
                        tool = (ToolHalconS1Param)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.NccShapeModel:
                        tool = (ToolFindNccShapeModelParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.GenBitmap:
                        tool = (ToolGenBitmapParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.DecomposeRGB:
                        tool = (ToolDecomposeRGBParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.CompareDistance:
                        tool = (ToolCompareDistanceParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.CropImage:
                        tool = (ToolCropImageParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.FindCircle:
                        tool = (ToolFindCircleParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.DistancePL:
                        tool = (ToolDistancePLParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.DistancePP:
                        tool = (ToolDistancePPParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.HClassifiyAI:
                        tool = (ToolHalconClassifyParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.DeepOcr:
                        tool = (ToolHalconDeepOcrParam)formatter.Deserialize(serializationStream);
                        break;
                    case ToolType.CorrectImage:
                        tool = (ToolCorrectImageParam)formatter.Deserialize(serializationStream);
                        break;
                    default:
                        break;
                }
                return ResStatus.OK;
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionLog(ex.Message);
                return ResStatus.Error;
            }
        }

        //序列化工具列表
        public ResStatus SaveToolList(string filename, List<ToolBase> toolList)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            ToolOperation op = new ToolOperation();
            long datalength = 0;
            try
            {
                //读取本地文件
                fs = new FileStream(filename, FileMode.Create);
                bw = new BinaryWriter(fs);
                fs.Seek(sizeof(long), SeekOrigin.Begin);
                //将参数类打成byte[]
                foreach (ToolBase item in toolList)
                {
                    byte[] buff;
                    op.SerializableTool(item.ToolParam, out buff);
                    datalength += buff.Length;
                    bw.Write(buff, 0, buff.Length);
                }
                fs.Seek(0, SeekOrigin.Begin);
                bw.Write(BitConverter.GetBytes(datalength), 0, sizeof(long));
                bw.Close();
                fs.Close();
                return ResStatus.OK;
            }
            catch (System.Exception ex)
            {
                if (bw != null)
                    bw.Close();
                if (fs != null)
                    fs.Close();
                LogHelper.WriteExceptionLog("SaveToolList;" + ex.Message);
                return ResStatus.OpFailed;
            }
        }

        //反序列化工具列表
        public ResStatus ReadToolList(string filename, out List<ToolBase> toolList)
        {
            toolList = new List<ToolBase>();
            FileStream fs1 = null;
            BinaryReader br = null;
            long lengthdata = 0;
            long lengthcurr = 0;
            try
            {

                fs1 = new FileStream(filename, FileMode.Open);
                br = new BinaryReader(fs1);
                byte[] buff1 = new byte[8];
                br.Read(buff1, 0, 8);
                lengthdata = BitConverter.ToInt64(buff1, 0);

                while (lengthdata > 0)
                {
                    lengthcurr = 0;
                    buff1 = new byte[8];
                    br.Read(buff1, 0, 8);
                    //获取对象字节长度
                    lengthcurr = BitConverter.ToInt64(buff1, 0);
                    buff1 = new byte[lengthcurr - sizeof(long)];
                    br.Read(buff1, 0, (int)(lengthcurr - sizeof(long)));
                    //生成工具参数对象
                    ToolParamBase tool;
                    DeserializableTool(buff1, out tool);
                    //生成工具
                    ToolBase toolrun;
                    toolrun = GetToolBaseFromType(tool);
                    //添加工具
                    toolList.Add(toolrun);
                    lengthdata -= lengthcurr;
                }
                fs1.Close();
                br.Close();
                return ResStatus.OK;

            }
            catch (System.Exception ex)
            {
                if (fs1 != null)
                    fs1.Close();
                if (br != null)
                    br.Close();
                LogHelper.WriteExceptionLog("ReadToolList;" + ex.Message);
                return ResStatus.OpFailed;
            }
        }

        //通过ToolType生成ToolBase
        public ToolBase GetToolBaseFromType(ToolParamBase param)
        {
            ToolBase tool = null;
            switch (param.ToolType)
            {
                case ToolType.RegionBlob:
                    ToolBlob tool2 = new ToolBlob(param);
                    tool = tool2;
                    break;
                case ToolType.HObjecDetect1:
                    ToolHalconR1 tool3 = new ToolHalconR1(param);
                    tool = tool3;
                    break;
                case ToolType.FindLine:
                    ToolCheckLine tool6 = new ToolCheckLine(param);
                    tool = tool6;
                    break;
                case ToolType.AngleLL:
                    ToolAngleLL tool7 = new ToolAngleLL(param);
                    tool = tool7;
                    break;
                case ToolType.DistanceLL:
                    ToolDistanceLL tool8 = new ToolDistanceLL(param);
                    tool = tool8;
                    break;
                case ToolType.ShapeModle:
                    ToolFindShapeModel tool9 = new ToolFindShapeModel(param);
                    tool = tool9;
                    break;
                case ToolType.ScaleImage:
                    ToolScaleImage tool12 = new ToolScaleImage(param);
                    tool = tool12;
                    break;
                case ToolType.HsemanticAI:
                    ToolHalconS1 tool14 = new ToolHalconS1(param);
                    tool = tool14;
                    break;
                case ToolType.NccShapeModel:
                    ToolFindNccShapeModel tool15 = new ToolFindNccShapeModel(param);
                    tool = tool15;
                    break;
                case ToolType.GenBitmap:
                    ToolGenBitmap tool18 = new ToolGenBitmap(param);
                    tool = tool18;
                    break;
                case ToolType.DecomposeRGB:
                    ToolDecomposeRGB tool19 = new ToolDecomposeRGB(param);
                    tool = tool19;
                    break;
                case ToolType.CompareDistance:
                    ToolCompareDistance tool22 = new ToolCompareDistance(param);
                    tool = tool22;
                    break;
                case ToolType.CropImage:
                    ToolCropImage tool23 = new ToolCropImage(param);
                    tool = tool23;
                    break;
                case ToolType.FindCircle:
                    ToolFindCircle tool24 = new ToolFindCircle(param);
                    tool = tool24;
                    break;
                case ToolType.DistancePL:
                    ToolDistancePL tool25 = new ToolDistancePL(param);
                    tool = tool25;
                    break;
                case ToolType.DistancePP:
                    ToolDistancePP tool26 = new ToolDistancePP(param);
                    tool = tool26;
                    break;
                case ToolType.HClassifiyAI:
                    ToolHalconClassify tool27 = new ToolHalconClassify(param);
                    tool = tool27;
                    break;
                case ToolType.DeepOcr:
                    ToolHalconDeepOcr tool28 = new ToolHalconDeepOcr(param);
                    tool = tool28;
                    break;
                case ToolType.CorrectImage:
                    ToolCorrectImage tool29 = new ToolCorrectImage(param);
                    tool = tool29;
                    break;
                default:
                    break;
            }
            return tool;
        }

        //通过string生产ToolType
        public ToolType GetToolNameFromStr(string str)
        {
            if (str == "查找直线")
                return ToolType.FindLine;
            else if (str == "相机标定")
                return ToolType.Calibration;
            else if (str == "露铜检测")
                return ToolType.CheckLT;
            else if (str == "A面崩缺")
                return ToolType.CheckBQ;
            else if (str == "直线到直线")
                return ToolType.DistanceLL;
            else if (str == "分类器")
                return ToolType.HClassifiyAI;
            else if (str == "目标检测1")
                return ToolType.HObjecDetect1;
            else if (str == "目标检测2")
                return ToolType.HObjecDetect2;
            else if (str == "语义分割")
                return ToolType.HsemanticAI;
            else if (str == "角度测量")
                return ToolType.AngleLL;
            else if (str == "区域提取")
                return ToolType.RegionBlob;
            else if (str == "模板匹配")
                return ToolType.ShapeModle;
            else if (str == "动态分割")
                return ToolType.DynThreshold;
            else if (str == "傅里叶变换")
                return ToolType.Fft;
            else if (str == "灰度缩放")
                return ToolType.ScaleImage;
            else if (str == "锈点检测")
                return ToolType.XiuDianCheck;
            else if (str == "灰度匹配")
                return ToolType.NccShapeModel;
            else if (str == "C面崩缺")
                return ToolType.CheckBQ2;
            else if (str == "料片检测")
                return ToolType.CheckLP;
            else if (str == "A面异色")
                return ToolType.CheckYS;
            else if (str == "变量计算")
                return ToolType.CheckCAl;
            else if (str == "颜色识别")
                return ToolType.CheckRGB;
            else if (str == "Bitmap转换")
                return ToolType.GenBitmap;
            else if (str == "通道分解")
                return ToolType.DecomposeRGB;
            else if (str == "查找直线对")
                return ToolType.FindLinePair;
            else if (str == "测量工具")
                return ToolType.MeasureLinePairs;
            else if (str == "间距比较")
                return ToolType.CompareDistance;
            else if (str == "裁剪图像")
                return ToolType.CropImage;
            else if (str == "查找圆")
                return ToolType.FindCircle;
            else if (str == "点到直线")
                return ToolType.DistancePL;
            else if (str == "点到点")
                return ToolType.DistancePP;
            else if (str == "分类器")
                return ToolType.HClassifiyAI;
            else if (str == "深度OCR")
                return ToolType.DeepOcr;
            else if (str == "图像矫正")
                return ToolType.CorrectImage;
            else
                return ToolType.None;
        }

        //通过ToolType生成ToolBase和ToolPage
        public void AddToolFromList(ToolType Type, int ID, out ToolBase mTool, out UserControl mUi)
        {
            mTool = null;
            mUi = null;
            if (Type == ToolType.FindLine)
            {
                ToolParamBase toolparam = new ToolCheckLineParam();
                toolparam.StepInfo.mShowName = "查找直线";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolCheckLine(toolparam);
                UToolFindLine ui = new UToolFindLine();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.DistanceLL)
            {
                ToolParamBase toolparam = new ToolDistanceLLParam();
                toolparam.StepInfo.mShowName = "直线到直线";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolDistanceLL(toolparam);
                UToolDistanceLL ui = new UToolDistanceLL();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.AngleLL)
            {
                ToolParamBase toolparam = new ToolAngleLLParam();
                toolparam.StepInfo.mShowName = "夹角测量";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolAngleLL(toolparam);
                UToolAngleLL ui = new UToolAngleLL();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.RegionBlob)
            {
                ToolParamBase toolparam = new ToolBlobParam();
                toolparam.StepInfo.mShowName = "区域提取";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolBlob(toolparam);
                UToolBlob ui = new UToolBlob();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.HObjecDetect1)
            {
                ToolParamBase toolparam = new ToolHalconR1Param();
                toolparam.StepInfo.mShowName = "目标检测1";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolHalconR1(toolparam);
                UToolHalconR1 ui = new UToolHalconR1();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.ShapeModle)
            {
                ToolParamBase toolparam = new ToolFindShapeModelParam();
                toolparam.StepInfo.mShowName = "模板匹配";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolFindShapeModel(toolparam);
                UToolFindShapeModel ui = new UToolFindShapeModel();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.ScaleImage)
            {
                ToolParamBase toolparam = new ToolScaleImageParam();
                toolparam.StepInfo.mShowName = "灰度缩放";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolScaleImage(toolparam);
                UToolScaleImage ui = new UToolScaleImage();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.HsemanticAI)
            {
                ToolParamBase toolparam = new ToolHalconS1Param();
                toolparam.StepInfo.mShowName = "语义分割";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolHalconS1(toolparam);
                UToolHalconS1 ui = new UToolHalconS1();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.NccShapeModel)
            {
                ToolParamBase toolparam = new ToolFindNccShapeModelParam();
                toolparam.StepInfo.mShowName = "灰度匹配";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolFindNccShapeModel(toolparam);
                UToolFindNccShapeModel ui = new UToolFindNccShapeModel();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.GenBitmap)
            {
                ToolParamBase toolparam = new ToolGenBitmapParam();
                toolparam.StepInfo.mShowName = "Bitmap转换";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolGenBitmap(toolparam);
                UToolGenBitmap ui = new UToolGenBitmap();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.DecomposeRGB)
            {
                ToolParamBase toolparam = new ToolDecomposeRGBParam();
                toolparam.StepInfo.mShowName = "通道分解";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolDecomposeRGB(toolparam);
                UToolDecomposeRGB ui = new UToolDecomposeRGB();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.CompareDistance)
            {
                ToolParamBase toolparam = new ToolCompareDistanceParam();
                toolparam.StepInfo.mShowName = "间距比较";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolCompareDistance(toolparam);
                UToolCompareDistance ui = new UToolCompareDistance();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.CropImage)
            {
                ToolParamBase toolparam = new ToolCropImageParam();
                toolparam.StepInfo.mShowName = "裁剪图像";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolCropImage(toolparam);
                UToolCropImage ui = new UToolCropImage();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.FindCircle)
            {
                ToolParamBase toolparam = new ToolFindCircleParam();
                toolparam.StepInfo.mShowName = "查找圆";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolFindCircle(toolparam);
                UToolFindCircle ui = new UToolFindCircle();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.DistancePL)
            {
                ToolParamBase toolparam = new ToolDistancePLParam();
                toolparam.StepInfo.mShowName = "点到直线";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolDistancePL(toolparam);
                UToolDistancePL ui = new UToolDistancePL();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.DistancePP)
            {
                ToolParamBase toolparam = new ToolDistancePPParam();
                toolparam.StepInfo.mShowName = "点到点";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolDistancePP(toolparam);
                UToolDistancePP ui = new UToolDistancePP();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.HClassifiyAI)
            {
                ToolParamBase toolparam = new ToolHalconClassifyParam();
                toolparam.StepInfo.mShowName = "分类器";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolHalconClassify(toolparam);
                UToolHalconClassify ui = new UToolHalconClassify();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.DeepOcr)
            {
                ToolParamBase toolparam = new ToolHalconDeepOcrParam();
                toolparam.StepInfo.mShowName = "深度OCR";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolHalconDeepOcr(toolparam);
                UToolHalconDeepOcr ui = new UToolHalconDeepOcr();
                mTool = tool;
                mUi = ui;
            }
            else if (Type == ToolType.CorrectImage)
            {
                ToolParamBase toolparam = new ToolCorrectImageParam();
                toolparam.StepInfo.mShowName = "图像矫正";
                toolparam.StepInfo.mInnerToolID = ID;
                ToolBase tool = new ToolCorrectImage(toolparam);
                UToolCorrectImage ui = new UToolCorrectImage();
                mTool = tool;
                mUi = ui;
            }
        }

        //通过ToolBase生成ToolPage
        public void GetPageFromToolBase(ToolBase tool, out UserControl page)
        {
            page = null;
            if (tool.ToolParam.ToolType == ToolType.RegionBlob)
            {
                UToolBlob ui = new UToolBlob();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.HObjecDetect1)
            {
                UToolHalconR1 ui = new UToolHalconR1();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.AngleLL)
            {
                UToolAngleLL ui = new UToolAngleLL();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.DistanceLL)
            {
                UToolDistanceLL ui = new UToolDistanceLL();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.FindLine)
            {
                UToolFindLine ui = new UToolFindLine();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.ShapeModle)
            {
                UToolFindShapeModel ui = new UToolFindShapeModel();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.ScaleImage)
            {
                UToolScaleImage ui = new UToolScaleImage();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.HsemanticAI)
            {
                UToolHalconS1 ui = new UToolHalconS1();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.NccShapeModel)
            {
                UToolFindNccShapeModel ui = new UToolFindNccShapeModel();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.GenBitmap)
            {
                UToolGenBitmap ui = new UToolGenBitmap();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.DecomposeRGB)
            {
                UToolDecomposeRGB ui = new UToolDecomposeRGB();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.CompareDistance)
            {
                UToolCompareDistance ui = new UToolCompareDistance();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.CropImage)
            {
                UToolCropImage ui = new UToolCropImage();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.FindCircle)
            {
                UToolFindCircle ui = new UToolFindCircle();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.DistancePL)
            {
                UToolDistancePL ui = new UToolDistancePL();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.DistancePP)
            {
                UToolDistancePP ui = new UToolDistancePP();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.HClassifiyAI)
            {
                UToolHalconClassify ui = new UToolHalconClassify();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.DeepOcr)
            {
                UToolHalconDeepOcr ui = new UToolHalconDeepOcr();
                page = ui;
            }
            else if (tool.ToolParam.ToolType == ToolType.CorrectImage)
            {
                UToolCorrectImage ui = new UToolCorrectImage();
                page = ui;
            }
        }
    }
}
