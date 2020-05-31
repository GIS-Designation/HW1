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
        public static MLFeatureProcessor FeatureProcessor;
        public MLMainForm()
        {
            dataFrame = new MLDataFrame.Dataframe();
            FeatureProcessor = new MLFeatureProcessor();
            InitializeComponent();
            mlFeatureBox.attributeTable.SelectingFeatureChanged += new AttributeTable.SelectingFeatureChangedHandle(attributeTable_SelectingFeatureChanged);
            FeatureProcessor.RecordsChangedHandle += new MLFeatureProcessor.RecordsChanged(mlRecordBox.RefreshRecords);
            FeatureProcessor.RefreshRecords();
            ShowScale();
        }
        public static Dataframe dataFrame;  //实例化在InitializeComponent函数的第一行，这样可以保证数据的同步性
        private void createFeature_Click(object sender, EventArgs e)  //点击“创建要素”
        {
            mlMap.TrackFeature();  //开始追踪新要素
        }

        private void selectFeature_Click(object sender, EventArgs e)  //点击“选择要素”
        {
            mlMap.SelectFeature();
        }
        private void zoomIn_Click(object sender, EventArgs e)  //点击“放大”
        {
            mlMap.ZoomIn();
        }

        private void zoomOut_Click(object sender, EventArgs e)  //点击“缩小”
        {
            mlMap.ZoomOut();
        }

        private void pan_Click(object sender, EventArgs e)  //点击“漫游”
        {
            mlMap.Pan();
        }
        private void zoomToLayer_Click(object sender, EventArgs e)  //点击“缩放至图层”
        {
            mlMap.zoomToLayer();
        }
        private void mlMap_MouseMove(object sender, MouseEventArgs e)  //鼠标移动，改变状态栏的坐标
        {
            PointF sMouseLocation = new PointF(e.Location.X, e.Location.Y);
            PointF sPointOnMap = mlMap.ToMapPoint(sMouseLocation);
            toolStripStatusLabel1.Text = "X:" + sPointOnMap.X.ToString("0.00") + " Y:" + sPointOnMap.Y.ToString("0.00");
        }
        private void mlMap_TrackingFinished(object sender, MLFeature feature)  //追踪结束的事件，由MLMap里双击事件触发
        {
            
            dataFrame.layers[dataFrame.index].featureClass.AddFeaure(feature);  //将新的要素添加到要素类中
            mlMap.Refresh();
        }

        private void mlMap_DisplayScaleChanged(object sender)  //比例尺改变，改变状态栏中的比例尺
        {
            ShowScale();
        }
        private void mlMap_SelectingFinished(object sender, RectangleF box)  //选择结束，由MLMap中的MouseUp触发，选中要素高亮
        {
            mlMap.selectedFeatures = mlMap.SelectByBox(box);
            mlMap.Refresh();
        }
        private void attributeTable_SelectingFeatureChanged(object sender,int[] selectingIndexes)//属性表进行选择
        {
            mlMap.selectedFeatures = new List<MLFeature>();
            if (selectingIndexes != null)
            {
                foreach (int i in selectingIndexes)
                    mlMap.selectedFeatures.Add(dataFrame.layers[dataFrame.index].featureClass.GetFeature(i));
            }
            mlMap.Refresh();
        }
        private void ShowScale()  //展示比例尺
        {
            toolStripStatusLabel2.Text = "1:" + mlMap.DisplayScale.ToString("0.00");
        }
    }
}
