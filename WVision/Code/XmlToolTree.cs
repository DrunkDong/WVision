using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WVision
{
    public class XmlToolTree
    {
        private string mPath;
        public List<string> mToolClassName;
        public List<List<string>> mToolList;
        public XmlToolTree(string path)
        {
            mToolClassName = new List<string>();
            mToolList = new List<List<string>>();
            mPath = path + "\\VisionToolCfg.xml";
        }

        public void GetToolClassNameFromXml()
        {
            mToolClassName = new List<string>();
            mToolList = new List<List<string>>();
            XmlDocument doc = new XmlDocument();
            doc.Load(mPath);
            XmlNode xn = doc.SelectSingleNode("ToolCfg");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xm in xnl)
            {
                XmlElement xe = (XmlElement)xm;
                mToolClassName.Add(xe.GetAttribute("name"));
                XmlNodeList xnl0 = xe.ChildNodes;
                List<string> lit = new List<string>();
                for (int i = 0; i < xnl0.Count; i++)
                {
                    lit.Add(xnl0.Item(i).InnerText);
                }
                mToolList.Add(lit);
            }
        }

        public void AddNoteToTreeView(TreeView tree, out List<string> ToolList)
        {
            ToolList = new List<string>();
            for (int i = 0; i < mToolClassName.Count; i++)
            {
                TreeNode RootNote = new TreeNode();
                RootNote.Text = mToolClassName[i];
                tree.Nodes.Add(RootNote);
                tree.SelectedNode = RootNote;
                for (int j = 0; j < mToolList[i].Count; j++)
                {
                    tree.SelectedNode.Nodes.Add(mToolList[i][j]);
                    ToolList.Add(mToolList[i][j]);
                }
            }
        }
    }
}
