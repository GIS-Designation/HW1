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
        public static MLFeatureBox featureBox;
        public MLMainForm()
        {
            dataFrame = new MLDataFrame.Dataframe();
            FeatureProcessor = new MLFeatureProcessor();
            InitializeComponent();
            featureBox = mlFeatureBox;
            mlFeatureBox.attributeTable.SelectingFeatureChanged += new AttributeTable.SelectingFeatureChangedHandle(attributeTable_SelectingFeatureChanged);
            FeatureProcessor.RecordsChangedHandle += new MLFeatureProcessor.RecordsChanged(mlRecordBox.RefreshRecords);
            FeatureProcessor.RefreshRecords();
            ShowScale();
            mlmap = mlMap;
        }
        public static MLMap mlmap;
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
            if (dataFrame.selected())
            {
                mlMap.zoomToLayer();
            }
            else
            {
                MessageBox.Show("请先选中图层");
            }
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
            mlMap.SetSelectedFeatures(mlMap.SelectByBox(box));
            mlMap.Refresh();
        }
        private void attributeTable_SelectingFeatureChanged(object sender,int[] selectingIndexes)//属性表进行选择
        {
            mlMap.ClearSelectedFeatures();
            if (selectingIndexes != null)
            {
                foreach (int i in selectingIndexes)
                {
                    mlMap.AddSelectedFeatures(dataFrame.index, i);
                }
            }
            mlMap.Refresh();
        }
        private void ShowScale()  //展示比例尺
        {
            toolStripStatusLabel2.Text = "1:" + mlMap.DisplayScale.ToString("0.00");
        }

        private void 导入Shapefile文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog shpFileDialog = new OpenFileDialog();
            shpFileDialog.Title = "打开Shapefile文件";
            //shpFileDialog.InitialDirectory = @"C:\Users\Kuuhakuj\Documents\PKU\大三下\GIS设计和应用\china_shp\Province_9";
            shpFileDialog.Filter = "SHP文件(*.shp)|*.shp";
            shpFileDialog.RestoreDirectory = true;
            if (DialogResult.OK == shpFileDialog.ShowDialog())
            {
                featureBox.addLayer(FeatureType.POINT, 0, shpFileDialog.FileName);
                mlmap.Refresh();
            }
        }

        private void Query_Click(object sender, EventArgs e)
        {
            Query q = new Query();
            q.SearchingFinished += new Query.SearchingFinishedHandle(attributeTable_SelectingFeatureChanged);
            q.Show();
        }

        private void 加载栅格底图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog();
            imageFileDialog.Title = "加载栅格底图";
            imageFileDialog.InitialDirectory = @"C:\Users\Kuuhakuj\Documents\PKU\大三下\GIS设计和应用\china_shp\Province_9";
            imageFileDialog.Filter = "JPG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png|所有文件(*.*)|*.*";
            imageFileDialog.RestoreDirectory = true;
            if (DialogResult.OK == imageFileDialog.ShowDialog())
            {
                mlmap.image = Image.FromFile(imageFileDialog.FileName);
                mlmap.Refresh();
            }
        }

        private void 导出当前地图至图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Title = "导出图片";
            sfg.Filter = "JPG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png|所有文件(*.*)|*.*";
            sfg.InitialDirectory = @"C:\Users\Kuuhakuj\Documents\PKU\大三下\GIS设计和应用\china_shp\Province_9";
            sfg.RestoreDirectory = true;
            if (sfg.ShowDialog() == DialogResult.OK)
            {
                Image image = mlmap.Output();
                string fileType = sfg.FileName.Substring(sfg.FileName.LastIndexOf(".") + 1);
                switch (fileType)
                {

                }
                image.Save(sfg.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }

        }
    }
}
