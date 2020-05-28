using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static MalaSpiritGIS.MLDataFrame;

namespace MalaSpiritGIS
{
    public partial class MLMainForm : Form
    {
        public MLMainForm()
        {
            InitializeComponent();
            MLFeatureProcessor fp = new MLFeatureProcessor();
            ShowScale();
        }
        Dataframe dataFrame;  //实例化在InitializeComponent函数的第一行，这样可以保证数据的同步性
        private void createFeature_Click(object sender, EventArgs e)
        {
            mlMap.TrackFeature();
        }

        private void selectFeature_Click(object sender, EventArgs e)
        {
            mlMap.SelectFeature();
        }
        private void zoomIn_Click(object sender, EventArgs e)
        {
            mlMap.ZoomIn();
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            mlMap.ZoomOut();
        }

        private void pan_Click(object sender, EventArgs e)
        {
            mlMap.Pan();
        }
        private void mlMap_MouseMove(object sender, MouseEventArgs e)
        {
            PointF sMouseLocation = new PointF(e.Location.X, e.Location.Y);
            PointF sPointOnMap = mlMap.ToMapPoint(sMouseLocation);
            toolStripStatusLabel1.Text = "X:" + sPointOnMap.X.ToString("0.00") + " Y:" + sPointOnMap.Y.ToString("0.00");
        }
        private void mlMap_TrackingFinished(object sender, MLFeature feature)
        {
            object[] v = new object[4];
            v[0] = 1;
            v[1] = 1;
            v[2] = 1;
            v[3] = 1;
            dataFrame.layers[dataFrame.index].featureClass.AddFeaure(feature, v);
            mlMap.Refresh();
        }

        private void mlMap_DisplayScaleChanged(object sender)
        {
            ShowScale();
        }
        private void mlMap_SelectingFinished(object sender, RectangleF box)
        {
            mlMap.selectedFeatures = mlMap.SelectByBox(box);
            mlMap.Refresh();
        }

        private void ShowScale()
        {
            toolStripStatusLabel2.Text = "1:" + mlMap.DisplayScale.ToString("0.00");
        }

    }
}
