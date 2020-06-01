using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileManagerObject FM = new FileManagerObject(textBox1.Text);
            treeView1.Nodes.Add(LV(FM));
        }

        private TreeNode LV(FileManagerObject i_FM)
        {
            TreeNode instLV = new TreeNode();
            instLV.Text = i_FM.Object.Name;
            if (i_FM.isDirectory)
            {
                foreach (EntityRef<FileManagerObject> instref in i_FM.Childs)
                {
                    TreeNode instLLV = LV(instref.Entity);
                    instLV.Nodes.Add(instLLV);
                }

            }
            return instLV;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
    }
}
