using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MalaSpiritGIS
{
    public partial class MLMap : UserControl
    {
        public MLMap()
        {
            InitializeComponent();
        }
        private MLFeatureBox.Dataframe dataFrame;  //记录数据
        public void getDataFrame(MLFeatureBox.Dataframe df)  //同步数据
        {
            dataFrame = df;
        }



        //设计时属性变量
        private Color fillColor = Color.Tomato;  //多边形填充色
        private Color boundaryColor = Color.Black;  //多边形边界色
        private Color trackingColor = Color.DarkGreen;  //描绘多边形的颜色

        //运行时属性变量
        private float displayScale = 1F;  //显示比例尺的倒数
        private List<List<int>> selectedFeatures = new List<List<int>>();  //选中要素集合

        //内部变量
        private float mOffsetX = 0, mOffsetY = 0;  //窗口左上点的地图坐标
        private int mMapOpStyle = 0;  //当前地图操作类型，0无，1放大，2缩小，3漫游，4创建要素，5选择要素
        private MLFeature mTrackingFeature;  //用户正在描绘的要素*********************************************************
        private FeatureType mTrackingType;  //用户正在描绘的要素种类****************************************************
        private PointF mMouseLocation = new PointF();  //鼠标当前的位置，用于漫游、拉框等
        private PointF mStartPoint = new PointF();  //记录鼠标按下时的位置，用于拉框

        //鼠标光标
        private Cursor mCur_Cross = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.Cross.ico"));
        private Cursor mCur_ZoomIn = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.ZoomIn.ico"));
        private Cursor mCur_ZoomOut = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.ZoomOut.ico"));
        private Cursor mCur_PanUp = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.PanUp.ico"));

        //常量
        private const float mcBoundaryWidth = 1F; //多边形边界宽度，单位像素
        private const float mcTrackingWidth = 1F; //描绘多边形的边界宽度，单位像素
        private const float mcVertexHandleSize = 7F;  //描绘多边形顶点手柄的大小，单位像素
        private const float mcZoomRatio = 1.2F;  //缩放系数
        private Color mcSelectingBoxColor = Color.DarkGreen;  //选择盒的颜色
        private const float mcSelectingBoxWidth = 2F;  //选择盒边界宽度，单位像素
        private Color mcSelectionColor = Color.Cyan;  //选中要素的颜色

        //运行时属性
        [Browsable(false)]
        public double DisplayScale
        {
            get { return displayScale; }
        }

        #region 方法

        /// 
        /// 将地图坐标转换为屏幕坐标
        /// 
        public PointF FromMapPoint(PointF point)
        {
            PointF sPoint = new PointF();
            sPoint.X = (point.X - mOffsetX) / displayScale;
            sPoint.Y = (point.Y - mOffsetY) / displayScale;
            return sPoint;
        }

        public PointF ToMapPoint(PointF point)
        {
            PointF sPoint = new PointF();
            sPoint.X = point.X * displayScale + mOffsetX;
            sPoint.Y = point.Y * displayScale + mOffsetY;
            return sPoint;
        }

        public void ZoomByCenter(PointF center, float ratio)
        {
            displayScale /= ratio;

            float sOffsetX, sOffsetY;  //定义新的偏移量
            sOffsetX = mOffsetX + (1 - 1 / ratio) * (center.X - mOffsetX);
            sOffsetY = mOffsetY + (1 - 1 / ratio) * (center.Y - mOffsetY);

            mOffsetX = sOffsetX;
            mOffsetY = sOffsetY;

            DisplayScaleChanged?.Invoke(this);
        }

        public void ZoomIn()
        {
            mMapOpStyle = 1;  //记录操作状态
            this.Cursor = mCur_ZoomIn;  //更改鼠标光标
        }

        public void ZoomOut()
        {
            mMapOpStyle = 2;  //记录操作状态
            this.Cursor = mCur_ZoomOut;  //更改鼠标光标
        }

        public void Pan()
        {
            mMapOpStyle = 3;
            this.Cursor = mCur_PanUp;
        }
        public void TrackFeature()
        {
            mMapOpStyle = 4;
            this.Cursor = mCur_Cross;
        }
        /// <summary>
        /// 将地图操作设置为选择要素状态
        /// </summary>
        public void SelectFeature()
        {
            mMapOpStyle = 5;
            this.Cursor = Cursors.Arrow;
        }
        public List<List<int>> SelectByBox(RectangleF box)
        {
            //List<Polygon> sSels = new List<Polygon>();
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < dataFrame.layers.Count; ++i)
            {
                MLFeatureClass layer = dataFrame.layers[i].featureClass;
                for (int j = 0; j < layer.Count; ++j)
                {
                    MLFeature feature = layer.GetFeature(j);
                    //对feature和框进行判别，若feature在框内就是被选中的
                }
            }
            return result;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 用户输入多边形结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="polygon"></param>
        [Browsable(true), Description("用户创建要素结束")]
        public delegate void TrackingFinishedHandle(object sender, MLFeature feature);
        public event TrackingFinishedHandle TrackingFinished;
        public delegate void DisplayScaleChangedHandle(object sender);
        /// <summary>
        /// 显示比例尺发生了变化
        /// </summary>
        [Browsable(true), Description("显示比例尺发生了变化")]
        public event DisplayScaleChangedHandle DisplayScaleChanged;

        public delegate void SelectingFinishiedHandle(object sender, RectangleF box);
        /// <summary>
        /// 用户进行选择操作结束
        /// </summary>
        [Browsable(true), Description("用户进行选择操作结束")]
        public event SelectingFinishiedHandle SelectingFinished;

        #endregion

        #region 私有函数

        //绘制多边形
        private void DrawFeatureClass(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(boundaryColor, mcBoundaryWidth);
            for (int i = dataFrame.layers.Count - 1;i != -1; --i)
            {
                MLFeatureClass fc = dataFrame.layers[i].featureClass;
                for (int j = 0;j != fc.Count; ++j)
                {
                    switch (fc.Type)
                    {
                        case FeatureType.POINT:
                            PointD point = ((MLPoint)fc.GetFeature(j)).Point;
                            PointF sScreenPoint = FromMapPoint(new PointF((float)point.X, (float)point.Y));
                            g.DrawRectangle(pen, sScreenPoint.X, sScreenPoint.Y, 1, 1);
                            break;
                        case FeatureType.MULTIPOINT:
                            PointD[] ps = ((MLMultiPoint)fc.GetFeature(j)).Points;
                            for(int k = 0;k != ps.Length; ++k)
                            {
                                PointF sp = FromMapPoint(new PointF((float)ps[k].X, (float)ps[k].Y));
                                g.DrawRectangle(pen, sp.X, sp.Y, 1, 1);
                            }
                            break;
                        case FeatureType.POLYLINE:
                            PolylineD[] segs = ((MLPolyline)fc.GetFeature(j)).Segments;
                            for (int k = 0; k != segs.Length; ++k)
                            {
                                for (int h = 1; h != segs[k].Count; ++h)
                                {
                                    PointD p1 = segs[k].GetPoint(h - 1);
                                    PointD p2 = segs[k].GetPoint(h);
                                    PointF sp1 = FromMapPoint(new PointF((float)p1.X, (float)p1.Y));
                                    PointF sp2 = FromMapPoint(new PointF((float)p2.X, (float)p2.Y));
                                    g.DrawLine(pen, sp1, sp2);
                                }
                            }
                            break;
                        case FeatureType.POLYGON:
                            PolygonD polygon = ((MLPolygon)fc.GetFeature(j)).Polygon;
                            //太复杂了暂时不写
                            break;
                    }
                }
            }
            pen.Dispose();
            //sPolygonBrush.Dispose();
        }

        private void DrawTrackingFeatures(Graphics g)
        {
            switch (FeatureType.POINT)//mTrackingFeature.FeatureType)
            {
                case FeatureType.POINT:
                    break;
                case FeatureType.MULTIPOINT:
                    break;
                case FeatureType.POLYLINE:
                    break;
                case FeatureType.POLYGON:
                    break;
            }
            //int sPointCount = mTrackingPolygon.PointCount;
            //if (sPointCount == 0)
            //    return;
            ////坐标转换
            //PointF[] sScreenPoints = new PointF[sPointCount];
            //for (int i = 0; i < sPointCount; ++i)
            //{
            //    PointD sScreenPoint = FromMapPoint(mTrackingPolygon.GetPoint(i));
            //    sScreenPoints[i].X = (float)sScreenPoint.X;
            //    sScreenPoints[i].Y = (float)sScreenPoint.Y;
            //}
            //Pen sTrackingPen = new Pen(_TrackingColor, mcTrackingWidth);
            //if (sPointCount > 1)
            //{
            //    g.DrawLines(sTrackingPen, sScreenPoints);
            //}
            //SolidBrush sVertexBrush = new SolidBrush(_TrackingColor);
            //for (int i = 0; i < sPointCount; ++i)
            //{
            //    RectangleF sRect = new RectangleF(sScreenPoints[i].X - mcVertexHandleSize / 2, sScreenPoints[i].Y - mcVertexHandleSize / 2, mcVertexHandleSize, mcVertexHandleSize);
            //    g.FillRectangle(sVertexBrush, sRect);
            //}
            //if (mMapOpStyle == 4)
            //{
            //    if (sPointCount == 1)
            //    {
            //        g.DrawLine(sTrackingPen, sScreenPoints[0], mMouseLocation);
            //    }
            //    else
            //    {
            //        g.DrawLine(sTrackingPen, sScreenPoints[0], mMouseLocation);
            //        g.DrawLine(sTrackingPen, sScreenPoints[sPointCount - 1], mMouseLocation);
            //    }
            //}
            //sTrackingPen.Dispose();
            //sVertexBrush.Dispose();
        }

        private void DrawSelectedFeatures(Graphics g)
        {
            //int sPolygonCount = _SelectedPolygons.Count;
            //Pen sPolygonPen = new Pen(mcSelectionColor, 2);
            //for (int i = 0; i < sPolygonCount; ++i)
            //{
            //    int sPointCount = _SelectedPolygons[i].PointCount;
            //    PointF[] sScreenPoints = new PointF[sPointCount];
            //    for (int j = 0; j < sPointCount; ++j)
            //    {
            //        PointD sScreenPoint = FromMapPoint(_SelectedPolygons[i].Points[j]);
            //        sScreenPoints[j].X = (float)sScreenPoint.X;
            //        sScreenPoints[j].Y = (float)sScreenPoint.Y;
            //    }
            //    g.DrawPolygon(sPolygonPen, sScreenPoints);
            //}
            //sPolygonPen.Dispose();
        }
        #endregion

        #region 母版事件处理
        private void MLMouseDown(object sender, MouseEventArgs e)
        {
            switch (mMapOpStyle)
            {
                case 0:
                    break;
                case 1:  //放大
                    if (e.Button == MouseButtons.Left)
                    {
                        PointF sMouseLocation = new PointF(e.Location.X, e.Location.Y);
                        PointF sPoint = ToMapPoint(sMouseLocation);
                        ZoomByCenter(sPoint, mcZoomRatio);
                        Refresh();
                    }
                    break;
                case 2:  //缩小
                    if (e.Button == MouseButtons.Left)
                    {
                        PointF sMouseLocation = new PointF(e.Location.X, e.Location.Y);
                        PointF sPoint = ToMapPoint(sMouseLocation);
                        ZoomByCenter(sPoint, 1 / mcZoomRatio);
                        Refresh();
                    }
                    break;
                case 3:  //漫游
                    if (e.Button == MouseButtons.Left)
                    {
                        mMouseLocation.X = e.Location.X;
                        mMouseLocation.Y = e.Location.Y;
                    }
                    break;
                case 4:  //输入要素
                    if (e.Button == MouseButtons.Left && e.Clicks == 1)
                    {

                        if (dataFrame.index > -1)
                        {
                            MLFeatureClass layer = dataFrame.layers[dataFrame.index].featureClass;
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

                        //PointF sScreenPoint = new PointF(e.Location.X, e.Location.Y);
                        //PointF sMapPoint = ToMapPoint(sScreenPoint);
                        //mTrackingPolygon.AddPoint(sMapPoint);
                        //Refresh();
                    }
                    break;
                case 5:  //选择
                    if (e.Button == MouseButtons.Left)
                    {
                        mStartPoint = e.Location;
                    }
                    break;
            }
        }

        private void MLMouseMove(object sender, MouseEventArgs e)
        {
            switch (mMapOpStyle)
            {
                case 0:
                    break;
                case 1:  //放大
                    break;
                case 2:  //缩小
                    break;
                case 3:  //漫游
                    if (e.Button == MouseButtons.Left)
                    {
                        PointF sPreMouseLocation = new PointF(mMouseLocation.X, mMouseLocation.Y);
                        PointF sPrePoint = ToMapPoint(sPreMouseLocation);
                        PointF sCurMouseLocation = new PointF(e.Location.X, e.Location.Y);
                        PointF sCurPoint = ToMapPoint(sCurMouseLocation);
                        //修改偏移量
                        mOffsetX = mOffsetX + sPrePoint.X - sCurPoint.X;
                        mOffsetY = mOffsetY + sPrePoint.Y - sCurPoint.Y;
                        Refresh();
                        mMouseLocation.X = e.Location.X;
                        mMouseLocation.Y = e.Location.Y;
                    }
                    break;
                case 4:  //输入多边形
                    mMouseLocation.X = e.Location.X;
                    mMouseLocation.Y = e.Location.Y;
                    Refresh();
                    break;
                case 5:  //选择
                    if (e.Button == MouseButtons.Left)
                    {
                        Refresh();
                        Graphics g = Graphics.FromHwnd(this.Handle);
                        Pen sBoxPen = new Pen(mcSelectingBoxColor, mcSelectingBoxWidth);
                        float sMinX = Math.Min(mStartPoint.X, e.Location.X);
                        float sMaxX = Math.Max(mStartPoint.X, e.Location.X);
                        float sMinY = Math.Min(mStartPoint.Y, e.Location.Y);
                        float sMaxY = Math.Max(mStartPoint.Y, e.Location.Y);
                        g.DrawRectangle(sBoxPen, sMinX, sMinY, sMaxX - sMinX, sMaxY - sMinY);
                        g.Dispose();
                    }
                    break;
            }
        }

        private void MLMouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        private void MLMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                PointF sCenterPoint = new PointF(this.ClientSize.Width / 2,
                    this.ClientSize.Height / 2);  //屏幕中心点
                PointF sCenterPointOnMap = ToMapPoint(sCenterPoint); //中心的地图坐标
                ZoomByCenter(sCenterPointOnMap, mcZoomRatio);
                Refresh();
            }
            else
            {
                PointF sCenterPoint = new PointF(this.ClientSize.Width / 2,
                    this.ClientSize.Height / 2);  //屏幕中心点
                PointF sCenterPointOnMap = ToMapPoint(sCenterPoint); //中心的地图坐标
                ZoomByCenter(sCenterPointOnMap, 1 / mcZoomRatio);
                Refresh();
            }
        }

        private void MLMouseUp(object sender, MouseEventArgs e)
        {
            switch (mMapOpStyle)
            {
                case 0:
                    break;
                case 1:  //放大
                    break;
                case 2:  //缩小
                    break;
                case 3:  //漫游
                    break;
                case 4:  //输入多边形
                    break;
                case 5:  //选择
                    if (e.Button == MouseButtons.Left)
                    {
                        float sMinX = Math.Min(mStartPoint.X, e.Location.X);
                        float sMaxX = Math.Max(mStartPoint.X, e.Location.X);
                        float sMinY = Math.Min(mStartPoint.Y, e.Location.Y);
                        float sMaxY = Math.Max(mStartPoint.Y, e.Location.Y);
                        PointF sTopLeft = new PointF(sMinX, sMinY);
                        PointF sBottomRight = new PointF(sMaxX, sMaxY);
                        PointF sTopLeftOnMap = ToMapPoint(sTopLeft);
                        PointF sBottomRightOnMap = ToMapPoint(sBottomRight);
                        RectangleF sSelBox = new RectangleF(sTopLeftOnMap.X, sBottomRightOnMap.X, sTopLeftOnMap.Y, sBottomRightOnMap.Y);
                        Refresh();
                        SelectingFinished?.Invoke(this, sSelBox);
                    }
                    break;
            }
        }

        //母版重绘
        private void MLPaint(object sender, PaintEventArgs e)
        {
            //绘制所有多边形
            DrawFeatureClass(e.Graphics);

            //绘制跟踪多边形
            DrawTrackingFeatures(e.Graphics);

            //绘制选中多边形
            DrawSelectedFeatures(e.Graphics);
        }


        #endregion
    }
}
