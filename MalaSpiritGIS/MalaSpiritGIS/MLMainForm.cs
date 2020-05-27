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
                MLFeatureClass layer = dataFrame.data.layers[dataFrame.data.index].featureClass;
                switch (layer.featureType)
                {
                    case FeatureType.POINT:
                        //创建点要素，创建完成后往layer中添加
                        break;
                    case FeatureType.MULTIPOINT:
                        //创建多点要素
                        break;
                    case FeatureType.POLYLINE:
                        //创建线要素
                        break;
                    case FeatureType.POLYGON:
                        //创建面要素
                        break;
                }
            }
            else
            {
                MessageBox.Show("请先点击目标图层");
            }
        }

        private void selectFeature_Click(object sender, EventArgs e)
        {
            //假设完成了框的绘制
            for(int i = 0;i < dataFrame.data.layers.Count; ++i)
            {
                MLFeatureClass layer = dataFrame.data.layers[i].featureClass;
                for(int j = 0;j < layer.Count; ++j)
                {
                    MLFeature feature = layer.GetFeature(j);
                    //对feature和框进行判别，若feature在框内就是被选中的
                }
            }
        }
    }
}
