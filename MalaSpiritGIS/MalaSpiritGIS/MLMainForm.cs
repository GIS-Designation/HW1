using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MalaSpiritGIS
{
    public partial class MLMainForm : Form
    {
        public MLMainForm()
        {
            InitializeComponent();
            MLFeatureProcessor fp = new MLFeatureProcessor();
            dataFrame = this.mlFeatureBox;
        }
        MLFeatureBox dataFrame;

        private void createFeature_Click(object sender, EventArgs e)
        {
            if(dataFrame.data.index > -1)
            {
                //进行要素创建
            }
            else
            {
                MessageBox.Show("请先点击目标图层");
            }
        }
    }
}
