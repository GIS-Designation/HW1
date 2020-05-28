﻿using System;
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
            dataFrame = this.mlFeatureBox.data;  //获取图层记录区域的数据
            mlMap.getDataFrame(dataFrame);  //将数据传递给绘制区域
            ShowScale();
        }
        MLFeatureBox.Dataframe dataFrame;

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
            //mcMap.AddPolygon(polygon);
            //mcMap.Refresh();
        }

        private void mlMap_DisplayScaleChanged(object sender)
        {
            ShowScale();
        }
        private void mlMap_SelectingFinished(object sender, RectangleF box)
        {
            //MyMapObjects.Polygon[] sPolygons = mcMap.SelectByBox(box);
            //mcMap.SelectedPolygons = sPolygons;
            //mcMap.Refresh();
        }

        private void ShowScale()
        {
            toolStripStatusLabel2.Text = "1:" + mlMap.DisplayScale.ToString("0.00");
        }

    }
}
