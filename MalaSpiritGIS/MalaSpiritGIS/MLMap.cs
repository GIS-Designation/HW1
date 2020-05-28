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
using static MalaSpiritGIS.MLDataFrame;

namespace MalaSpiritGIS
{
    public partial class MLMap : UserControl
    {
        public MLMap(Dataframe df)  //接收从mainForm传递来的dataFrame
        {
            dataFrame = df;
            InitializeComponent();
        }
        public class SelectedFeature
        {
            private int numLayer, numFeature;
            public SelectedFeature(int _numLayer, int _numFeature)
            {
                numLayer = _numLayer;
                numFeature = _numFeature;
            }
            public int La { get { return numLayer; } }
            public int Fe { get { return numFeature; } }
        }
        private Dataframe dataFrame;  //记录数据
        //设计时属性变量
        private Color fillColor = Color.Tomato;  //多边形填充色
        private Color boundaryColor = Color.Black;  //多边形边界色
        private Color trackingColor = Color.DarkGreen;  //描绘多边形的颜色

        //运行时属性变量
        private float displayScale = 1F;  //显示比例尺的倒数
        public List<SelectedFeature> selectedFeatures = new List<SelectedFeature>();  //选中要素集合

        //内部变量
        private float mOffsetX = 0, mOffsetY = 0;  //窗口左上点的地图坐标
        private int mMapOpStyle = 0;  //当前地图操作类型，0无，1放大，2缩小，3漫游，4创建要素，5选择要素
        private List<PointD> mTrackingFeature = new List<PointD>();  //用户正在描绘的要素
        private FeatureType mTrackingType;  //用户正在描绘的要素种类
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
            if (dataFrame.index > -1)
            {
                mMapOpStyle = 4;
                this.Cursor = mCur_Cross;
                mTrackingType = dataFrame.layers[dataFrame.index].featureClass.featureType;
            }
            else
            {
                MessageBox.Show("请先点击目标图层");
            }

        }
        /// <summary>
        /// 将地图操作设置为选择要素状态
        /// </summary>
        public void SelectFeature()
        {
            mMapOpStyle = 5;
            this.Cursor = Cursors.Arrow;
        }
        public List<SelectedFeature> SelectByBox(RectangleF box)
        {
            List<SelectedFeature> result = new List<SelectedFeature>();
            for (int i = 0; i < dataFrame.layers.Count; ++i)
            {
                MLFeatureClass layer = dataFrame.layers[i].featureClass;
                for (int j = 0; j < layer.Count; ++j)
                {
                    switch (layer.Type)
                    {
                        case FeatureType.POINT:
                            if (MLPointInBox((MLPoint)layer.GetFeature(j), box))
                                result.Add(new SelectedFeature(i, j));
                            break;
                        case FeatureType.MULTIPOINT:
                            if (MLMultiPointInBox((MLMultiPoint)layer.GetFeature(j), box))
                                result.Add(new SelectedFeature(i, j));
                            break;
                        case FeatureType.POLYLINE:
                            if (MLPolylineInBox((MLPolyline)layer.GetFeature(j), box))
                                result.Add(new SelectedFeature(i, j));
                            break;
                        case FeatureType.POLYGON:
                            if (MLPolygonInBox((MLPolygon)layer.GetFeature(j), box))
                                result.Add(new SelectedFeature(i, j));
                            break;
                    }
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
        private bool PointDInBox(PointD p,RectangleF rec)
        {
            return (p.X > rec.X && p.X < rec.X + rec.Width && p.Y > rec.Y && p.Y < rec.Y + rec.Height);
        }
        private bool MLPointInBox(MLPoint p, RectangleF rec)
        {
            return PointDInBox(p.Point, rec);
        }
        private bool MLMultiPointInBox(MLMultiPoint m,RectangleF rec)
        {
            PointD[] ps = m.Points;
            for(int i = 0;i != ps.Length; ++i)
            {
                if (!PointDInBox(ps[i], rec))
                {
                    return false;
                }
            }
            return true;
        }
        private bool MLPolylineInBox(MLPolyline p,RectangleF rec)
        {
            PolylineD[] polylines = p.Segments;
            for(int i = 0;i != polylines.Length; ++i)
            {
                PolylineD polyline = polylines[i];
                for(int j = 0;j != polyline.Count; ++j)
                {
                    if (!PointDInBox(polyline.GetPoint(j), rec))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool MLPolygonInBox(MLPolygon p,RectangleF rec)
        {
            PolygonD polygon = p.Polygon;
            for(int i = 0;i != polygon.Count; ++i)
            {
                PolylineD polyline = polygon.GetRing(i);
                for(int j = 0;j != polyline.Count; ++j)
                {
                    if (!PointDInBox(polyline.GetPoint(j), rec))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void PaintPoint(MLFeature feature,Pen pen,Graphics g)
        {
            PointD point = ((MLPoint)feature).Point;
            PointF sScreenPoint = FromMapPoint(new PointF((float)point.X, (float)point.Y));
            g.DrawRectangle(pen, sScreenPoint.X, sScreenPoint.Y, 1, 1);
        }
        private void PaintMultiPoint(MLFeature feature, Pen pen, Graphics g)
        {
            PointD[] ps = ((MLMultiPoint)feature).Points;
            for (int k = 0; k != ps.Length; ++k)
            {
                PointF sp = FromMapPoint(new PointF((float)ps[k].X, (float)ps[k].Y));
                g.DrawRectangle(pen, sp.X, sp.Y, 1, 1);
            }
        }
        private void PaintPolyline(MLFeature feature, Pen pen, Graphics g)
        {
            PolylineD[] segs = ((MLPolyline)feature).Segments;
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
        }
        private void PaintPolygon(MLFeature feature, Pen pen, Graphics g)
        {
            PolygonD polygon = ((MLPolygon)feature).Polygon;
            for (int k = 0; k != polygon.Count; ++k)
            {
                PolylineD ring = polygon.GetRing(k);
                for (int h = 1; h != ring.Count; ++h)
                {
                    PointD p1 = ring.GetPoint(h - 1);
                    PointD p2 = ring.GetPoint(h);
                    PointF sp1 = FromMapPoint(new PointF((float)p1.X, (float)p1.Y));
                    PointF sp2 = FromMapPoint(new PointF((float)p2.X, (float)p2.Y));
                    g.DrawLine(pen, sp1, sp2);
                }
            }
            //多边形还需要上色，但是这里先画出轮廓来吧。
        }
        //绘制要素
        private void DrawFeatureClass(Graphics g)
        {
            if (dataFrame.layers != null)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Pen pen = new Pen(boundaryColor, mcBoundaryWidth);
                for (int i = dataFrame.layers.Count - 1; i != -1; --i)
                {
                    MLFeatureClass fc = dataFrame.layers[i].featureClass;
                    for (int j = 0; j != fc.Count; ++j)
                    {
                        switch (fc.Type)
                        {
                            case FeatureType.POINT:
                                PaintPoint(fc.GetFeature(j), pen, g);
                                break;
                            case FeatureType.MULTIPOINT:
                                PaintMultiPoint(fc.GetFeature(j), pen, g);
                                break;
                            case FeatureType.POLYLINE:
                                PaintPolyline(fc.GetFeature(j), pen, g);
                                break;
                            case FeatureType.POLYGON:
                                PaintPolygon(fc.GetFeature(j), pen, g);
                                break;
                        }
                    }
                }
                pen.Dispose();
                //sPolygonBrush.Dispose();
            }
        }

        private void DrawTrackingFeatures(Graphics g)
        {
            if(mTrackingType != FeatureType.POINT)
            {
                int sPointCount = mTrackingFeature.Count;
                if (sPointCount == 0)
                    return;
                //坐标转换
                PointF[] sScreenPoints = new PointF[sPointCount];
                for (int i = 0; i < sPointCount; ++i)
                {
                    PointF sScreenPoint = FromMapPoint(new PointF((float)mTrackingFeature[i].X, (float)mTrackingFeature[i].Y));
                    sScreenPoints[i].X = (float)sScreenPoint.X;
                    sScreenPoints[i].Y = (float)sScreenPoint.Y;
                }
                SolidBrush sVertexBrush = new SolidBrush(trackingColor);
                for (int i = 0; i < sPointCount; ++i)
                {
                    RectangleF sRect = new RectangleF(sScreenPoints[i].X - mcVertexHandleSize / 2, sScreenPoints[i].Y - mcVertexHandleSize / 2, mcVertexHandleSize, mcVertexHandleSize);
                    g.FillRectangle(sVertexBrush, sRect);
                }
                if(mTrackingType != FeatureType.MULTIPOINT)
                {
                    Pen sTrackingPen = new Pen(trackingColor, mcTrackingWidth);
                    if (sPointCount > 1)
                    {
                        g.DrawLines(sTrackingPen, sScreenPoints);
                    }
                    if (mTrackingType == FeatureType.POLYGON && mMapOpStyle == 4)
                    {
                        if (sPointCount == 1)
                        {
                            g.DrawLine(sTrackingPen, sScreenPoints[0], mMouseLocation);
                        }
                        else
                        {
                            g.DrawLine(sTrackingPen, sScreenPoints[0], mMouseLocation);
                            g.DrawLine(sTrackingPen, sScreenPoints[sPointCount - 1], mMouseLocation);
                        }
                    }
                    sTrackingPen.Dispose();
                }
                sVertexBrush.Dispose();
            }
        }

        private void DrawSelectedFeatures(Graphics g)
        {
            if (selectedFeatures.Count != 0)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Pen pen = new Pen(mcSelectingBoxColor, 2);
                for (int i = selectedFeatures.Count - 1; i != -1; --i)
                {
                    SelectedFeature s = selectedFeatures[i];
                    MLFeatureClass fc = dataFrame.layers[s.La].featureClass;
                    switch (fc.Type)
                    {
                        case FeatureType.POINT:
                            PaintPoint(fc.GetFeature(s.Fe), pen, g);
                            break;
                        case FeatureType.MULTIPOINT:
                            PaintMultiPoint(fc.GetFeature(s.Fe), pen, g);
                            break;
                        case FeatureType.POLYLINE:
                            PaintPolyline(fc.GetFeature(s.Fe), pen, g);
                            break;
                        case FeatureType.POLYGON:
                            PaintPolygon(fc.GetFeature(s.Fe), pen, g);
                            break;
                    }
                }
                pen.Dispose();
            }
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
                        PointF p = ToMapPoint(new PointF(e.Location.X, e.Location.Y));
                        switch (mTrackingType)
                        {
                            case FeatureType.POINT:
                                MLPoint point = new MLPoint(new PointD(p.X, p.Y));
                                if (TrackingFinished != null)
                                {
                                    TrackingFinished(this, point);
                                }
                                break;
                            default:
                                mTrackingFeature.Add(new PointD(p.X, p.Y));
                                Refresh();
                                break;
                        }
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
                    if (e.Button == MouseButtons.Left)
                    {
                        switch (mTrackingType)
                        {
                            case FeatureType.POINT:
                                //这个是真没有要执行的
                                break;
                            case FeatureType.MULTIPOINT:
                                PointD[] multipointPs = new PointD[mTrackingFeature.Count];
                                for (int i = 0; i != multipointPs.Length; ++i)
                                    multipointPs[i] = mTrackingFeature[i];
                                MLMultiPoint multipoint = new MLMultiPoint(multipointPs);
                                if (TrackingFinished != null)
                                {
                                    TrackingFinished(this, multipoint);
                                }
                                break;
                            case FeatureType.POLYLINE:
                                PointD[] polylinePs = new PointD[mTrackingFeature.Count];
                                for (int i = 0; i != polylinePs.Length; ++i)
                                    polylinePs[i] = mTrackingFeature[i];
                                MLPolyline polyline = new MLPolyline(polylinePs);
                                if (TrackingFinished != null)
                                {
                                    TrackingFinished(this, polyline);
                                }
                                break;
                            case FeatureType.POLYGON:
                                if (mTrackingFeature.Count >= 3)
                                {
                                    PointD[] polygonPs = new PointD[mTrackingFeature.Count];
                                    for (int i = 0; i != polygonPs.Length; ++i)
                                        polygonPs[i] = mTrackingFeature[i];
                                    MLPolygon polygon = new MLPolygon(new PolylineD(polygonPs));
                                    if (TrackingFinished != null)
                                    {
                                        TrackingFinished(this,polygon);
                                    }
                                }
                                break;
                        }
                        mTrackingFeature.Clear();
                    }
                    break;
                case 5:  //选择
                    break;
            }
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
