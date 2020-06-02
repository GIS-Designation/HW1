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
using System.Drawing.Drawing2D;

namespace MalaSpiritGIS
{
    public partial class MLMap : UserControl
    {
        public MLMap()  //接收从mainForm传递来的dataFrame
        {
            dataFrame = MLMainForm.dataFrame;
            InitializeComponent();
        }
        private Dataframe dataFrame;  //记录数据
        //设计时属性变量
        public Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Blue, Color.DarkBlue, Color.Violet, Color.Pink };  //线条、填充色
        //这里枚举一下点符号类型，以便绘制点要素
        public string[] PointSigns = { "HollowCircle", "FilledCircle", "HollowSquare", "FilledSquare", "HollowTriangle", "FilledTriangle", "HollowConcentricCircles", "FilledConcentricCircles" };
        public string[] LineStyles = { "Solid", "Dash" };//线条类型，实线或虚线
        private Color trackingColor = Color.DarkGreen;  //追踪中要素的颜色
        public Image image;

        //运行时属性变量
        private float displayScale = 1F;  //显示比例尺的倒数
        public class Selected
        {
            public int numLayer, numFeature;
            public Selected(int _numLayer, int _numFeature)
            {
                numLayer = _numLayer;
                numFeature = _numFeature;
            }
        }
        private List<Selected> selectedFeatures = new List<Selected>();  //选中要素集合
        private List<MLFeature> mergeFeatures = new List<MLFeature>();
        public void SetSelectedFeatures(List<Selected> s) { selectedFeatures = s; }
        public void ClearSelectedFeatures() { selectedFeatures = new List<Selected>(); Refresh(); }
        public void AddSelectedFeatures(int numLayer, int numFeature) { selectedFeatures.Add(new Selected(numLayer, numFeature)); }
        //内部变量
        private float offsetX = 0, offsetY = 0;  //窗口左上点的地图坐标
        private int mapOpStyle = 0;  //当前地图操作类型，0无，1放大，2缩小，3漫游，4创建要素，5选择要素,6移动要素,7编辑节点,8裁剪图形
        private List<PointD> trackingFeature = new List<PointD>();  //用户正在描绘的要素
        private PointF mouseLocation = new PointF();  //鼠标当前的位置，用于漫游、拉框等
        private PointF startPoint = new PointF();  //记录鼠标按下时的位置，用于拉框
        private static class EditPoint
        {
            public static PointD p;
            public static bool isLinkPoint;
            public static int numLayer, numPolygon, numRing;
        }
        //鼠标光标
        private Cursor cur_Cross = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.Cross.ico"));
        private Cursor cur_ZoomIn = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.ZoomIn.ico"));
        private Cursor cur_ZoomOut = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.ZoomOut.ico"));
        private Cursor cur_PanUp = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.PanUp.ico"));
        private Cursor cur_MoveFeature = new Cursor(Assembly.GetExecutingAssembly().GetManifestResourceStream("MalaSpiritGIS.Resources.moveFeature.ico"));

        //常量
        private const float boundaryWidth = 1F; //完成追踪的要素边界宽度，单位像素
        private const float trackingWidth = 1F; //追踪中要素的边界宽度，单位像素
        private const float vertexHandleSize = 7F;  //描绘多边形顶点手柄的大小，单位像素
        private const float zoomRatio = 1.2F;  //缩放系数
        private Color selectingBoxColor = Color.DarkGreen;  //选择盒的颜色
        private const float selectingBoxWidth = 2F;  //选择盒边界宽度，单位像素
        private Color selectedColor = Color.Cyan;  //选中要素的颜色

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
            sPoint.X = (point.X - offsetX) / displayScale;
            sPoint.Y = -(point.Y - offsetY) / displayScale + Height;
            return sPoint;
        }

        public PointF ToMapPoint(PointF point)
        {
            PointF sPoint = new PointF();
            sPoint.X = point.X * displayScale + offsetX;
            sPoint.Y = (Height - point.Y) * displayScale + offsetY;
            return sPoint;
        }

        public void ZoomByCenter(PointF center, float ratio)
        {
            displayScale /= ratio;

            float sOffsetX, sOffsetY;  //定义新的偏移量
            sOffsetX = offsetX + (1 - 1 / ratio) * (center.X - offsetX);
            sOffsetY = offsetY - (1 - 1 / ratio) * (offsetY - center.Y);

            offsetX = sOffsetX;
            offsetY = sOffsetY;

            DisplayScaleChanged?.Invoke(this);  //触发DisplayScaleChanged事件
        }

        public void ZoomIn()
        {
            mapOpStyle = 1;  //记录操作状态
            this.Cursor = cur_ZoomIn;  //更改鼠标光标
        }

        public void ZoomOut()
        {
            mapOpStyle = 2;  //记录操作状态
            this.Cursor = cur_ZoomOut;  //更改鼠标光标
        }

        public void Pan()
        {
            mapOpStyle = 3;
            this.Cursor = cur_PanUp;
        }
        public void TrackFeature()  //开始或继续创建要素
        {
            if (dataFrame.index > -1)  //如果有图层被选中
            {
                mapOpStyle = 4;
                this.Cursor = cur_Cross;
            }
            else
            {
                MessageBox.Show("请先点击目标图层");
            }

        }
        /// <summary>
        /// 将地图操作设置为选择要素状态
        /// </summary>
        public void SelectFeature()  //选择要素
        {
            mapOpStyle = 5;
            this.Cursor = Cursors.Arrow;
        }
        public List<Selected> SelectByBox(RectangleF box)  //返回完全位于box中的要素
        {
            List<Selected> result = new List<Selected>();
            for (int i = 0; i < dataFrame.layers.Count; ++i)  //遍历图层
            {
                MLFeatureClass layer = dataFrame.layers[i].featureClass;
                for (int j = 0; j < layer.Count; ++j)  //遍历图层中的要素
                {
                    switch (layer.Type)
                    {
                        case FeatureType.POINT:
                            if (MLPointInBox((MLPoint)layer.GetFeature(j), box))
                                result.Add(new Selected(i, j));
                            break;
                        case FeatureType.MULTIPOINT:
                            if (MLMultiPointInBox((MLMultiPoint)layer.GetFeature(j), box))
                                result.Add(new Selected(i, j));
                            break;
                        case FeatureType.POLYLINE:
                            if (MLPolylineInBox((MLPolyline)layer.GetFeature(j), box))
                                result.Add(new Selected(i, j));
                            break;
                        case FeatureType.POLYGON:
                            if (MLPolygonInBox((MLPolygon)layer.GetFeature(j), box))
                                result.Add(new Selected(i, j));
                            break;
                    }
                }
            }
            return result;
        }
        public void zoomToLayer()
        {
            MLFeatureClass fc = dataFrame.layers[dataFrame.index].featureClass;
            displayScale = (float)Math.Max((fc.XMax - fc.XMin) / this.Width, (fc.YMax - fc.YMin) / this.Height);
            offsetX = (float)fc.XMin;
            offsetY = (float)fc.YMin;
            Refresh();
            DisplayScaleChanged?.Invoke(this);
        }

        public Image Output()
        {
            Bitmap bmp = new Bitmap(Width, Height);
            DrawToBitmap(bmp, new Rectangle(0, 0, Width, Height));
            return bmp;
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
        private bool PointDInBox(PointD p, RectangleF rec)  //点p完全位于rec中
        {
            return (p.X > rec.X && p.X < rec.X + rec.Width && p.Y > rec.Y && p.Y < rec.Y + rec.Height);
        }
        private bool MLPointInBox(MLPoint p, RectangleF rec)  //点要素p完全位于rec中
        {
            return PointDInBox(p.Point, rec);
        }
        private bool MLMultiPointInBox(MLMultiPoint m, RectangleF rec)  //多点要素m完全位于rec中
        {
            PointD[] ps = m.Points;
            for (int i = 0; i != ps.Length; ++i)
            {
                if (!PointDInBox(ps[i], rec))
                {
                    return false;
                }
            }
            return true;
        }
        private bool MLPolylineInBox(MLPolyline p, RectangleF rec)  //线要素p完全位于rec中
        {
            PolylineD[] polylines = p.Segments;
            for (int i = 0; i != polylines.Length; ++i)
            {
                PolylineD polyline = polylines[i];
                for (int j = 0; j != polyline.Count; ++j)
                {
                    if (!PointDInBox(polyline.GetPoint(j), rec))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool MLPolygonInBox(MLPolygon p, RectangleF rec)  //面要素p完全位于rec中
        {
            PolygonD polygon = p.Polygon;
            for (int i = 0; i != polygon.Count; ++i)
            {
                PolylineD polyline = polygon.GetRing(i);
                for (int j = 0; j != polyline.Count; ++j)
                {
                    if (!PointDInBox(polyline.GetPoint(j), rec))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool PointFInPointD(PointF p, PointD pd)
        {
            const int LOGICAL_0 = 5;  //像素
            PointF np = FromMapPoint(new PointF((float)pd.X, (float)pd.Y));
            float dx = np.X - p.X;
            float dy = np.Y - p.Y;
            return dx * dx + dy * dy < LOGICAL_0 * LOGICAL_0;
        }
        private bool PointInMLPoint(PointF p, MLPoint m)  //p是显示坐标，不是地图坐标
        {
            return PointFInPointD(p, m.Point);
        }
        private bool PointInMLMultiPoint(PointF p, MLMultiPoint m)  //p是显示坐标，不是地图坐标
        {
            PointD[] ps = m.Points;
            for (int i = 0; i != ps.Length; ++i)
            {
                if (PointFInPointD(p, ps[i]))
                    return true;
            }
            return false;
        }
        private bool PointFInPolygon(PointF p, PointD[] mapPoints)
        {
            PointF[] ps = new PointF[mapPoints.Length];
            for (int i = 0; i != mapPoints.Length; ++i)
                ps[i] = FromMapPoint(new PointF((float)mapPoints[i].X, (float)mapPoints[i].Y));
            int c = 0;
            for (int i = 1, j = 0; i != ps.Length; j = i++)
            {
                if (((ps[i].Y > p.Y) != (ps[j].Y > p.Y)) && (p.X < (ps[j].X - ps[i].X) * (p.Y - ps[i].Y) / (ps[j].Y - ps[i].Y) + ps[i].X))
                    ++c;
            }
            return c % 2 != 0;
        }
        private bool PointFInLine(PointF p, PointD p1, PointD p2)
        {
            const int LOGICAL_0 = 5;
            double deltaY = p1.Y - p2.Y;
            double theta = Math.Abs(deltaY) < 1e-5 ? Math.PI / 2 : Math.Atan(-(p1.X - p2.X) / deltaY);
            double dx = Math.Cos(theta) * LOGICAL_0;
            double dy = Math.Sin(theta) * LOGICAL_0;
            PointD pd1 = new PointD(p1.X - dx, p1.Y - dy);
            PointD pd2 = new PointD(p1.X + dx, p1.Y + dy);
            PointD pd3 = new PointD(p2.X + dx, p2.Y + dy);
            PointD pd4 = new PointD(p2.X - dx, p2.Y - dy);
            return PointFInPolygon(p, new PointD[] { pd1, pd2, pd3, pd4, pd1 });

        }
        private bool PointInMLPolyline(PointF p, MLPolyline m)  //p是显示坐标，不是地图坐标
        {
            PolylineD[] lines = m.Segments;
            for (int i = 0; i != lines.Length; ++i)
            {
                PolylineD line = lines[i];
                for (int j = 1; j != line.Count; ++j)
                {
                    if (PointFInLine(p, line.GetPoint(j - 1), line.GetPoint(j)))
                        return true;
                }
                for (int j = 0; j != line.Count; ++j)
                {
                    if (PointFInPointD(p, line.GetPoint(j)))
                        return true;
                }
            }
            return false;
        }
        private bool PointInMLPolygon(PointF p, MLPolygon m)
        {
            PolygonD pd = m.Polygon;
            for (int i = 0; i != pd.Count; ++i)
            {
                if (PointFInPolygon(p, pd.GetRing(i).GetPoints()))
                    return true;
            }
            return false;
        }
        private void PaintPoint(MLFeature feature, Brush brush, Pen pen, Graphics g, string PointSign, float size)  //绘制点要素
        {
            PointD point = ((MLPoint)feature).Point;
            PointF sScreenPoint = FromMapPoint(new PointF((float)point.X, (float)point.Y));

            if (PointSign == PointSigns[0])//空心圆
            {
                g.DrawEllipse(pen, sScreenPoint.X - size, sScreenPoint.Y - size, size * 2, size * 2);
            }
            else if (PointSign == PointSigns[1])//实心圆
            {
                g.FillEllipse(brush, sScreenPoint.X - size, sScreenPoint.Y - size, size * 2, size * 2);
            }
            else if (PointSign == PointSigns[2])//空心正方形
            {
                g.DrawRectangle(pen, sScreenPoint.X - size, sScreenPoint.Y - size, size * 2, size * 2);
            }
            else if (PointSign == PointSigns[3])//实心正方形
            {
                g.FillRectangle(brush, sScreenPoint.X - size, sScreenPoint.Y - size, size * 2, size * 2);
            }
            else if (PointSign == PointSigns[4])//空心三角形
            {
                PointF p1 = new PointF(sScreenPoint.X, sScreenPoint.Y - size);
                PointF p2 = new PointF(sScreenPoint.X - size, sScreenPoint.Y + size);
                PointF p3 = new PointF(sScreenPoint.X + size, sScreenPoint.Y + size);
                g.DrawLine(pen, p1, p2);
                g.DrawLine(pen, p3, p2);
                g.DrawLine(pen, p1, p3);
            }
            else if (PointSign == PointSigns[5])//实心三角形
            {
                PointF p1 = new PointF(sScreenPoint.X, sScreenPoint.Y - size);
                PointF p2 = new PointF(sScreenPoint.X - size, sScreenPoint.Y + size);
                PointF p3 = new PointF(sScreenPoint.X + size, sScreenPoint.Y + size);
                PointF[] points = { p1, p2, p3 };
                FillMode newFillMode = FillMode.Winding;
                g.FillPolygon(brush, points, newFillMode);
            }
            else if (PointSign == PointSigns[6])//空心同心圆
            {
                g.DrawEllipse(pen, sScreenPoint.X - size, sScreenPoint.Y - size, size * 2, size * 2);
                g.DrawEllipse(pen, sScreenPoint.X - size * 2, sScreenPoint.Y - size * 2, size * 4, size * 4);
            }
            else if (PointSign == PointSigns[7])//实心同心圆
            {
                g.FillEllipse(brush, sScreenPoint.X - size, sScreenPoint.Y - size, size * 2, size * 2);
                g.DrawEllipse(pen, sScreenPoint.X - size * 2, sScreenPoint.Y - size * 2, size * 4, size * 4);
            }
        }
        private void PaintMultiPoint(MLFeature feature, Brush brush, Pen pen, Graphics g, string PointSign, float size)  //绘制多点要素
        {
            PointD[] ps = ((MLMultiPoint)feature).Points;
            for (int k = 0; k != ps.Length; ++k)
            {
                PointF sp = FromMapPoint(new PointF((float)ps[k].X, (float)ps[k].Y));
                if (PointSign == PointSigns[0])//空心圆
                {
                    g.DrawEllipse(pen, sp.X - size, sp.Y - size, size * 2, size * 2);
                }
                else if (PointSign == PointSigns[1])//实心圆
                {
                    g.FillEllipse(brush, sp.X - size, sp.Y - size, size * 2, size * 2);
                }
                else if (PointSign == PointSigns[2])//空心正方形
                {
                    g.DrawRectangle(pen, sp.X - size, sp.Y - size, size * 2, size * 2);
                }
                else if (PointSign == PointSigns[3])//实心正方形
                {
                    g.FillRectangle(brush, sp.X - size, sp.Y - size, size * 2, size * 2);
                }
                else if (PointSign == PointSigns[4])//空心三角形
                {
                    PointF p1 = new PointF(sp.X, sp.Y - size);
                    PointF p2 = new PointF(sp.X - size, sp.Y + size);
                    PointF p3 = new PointF(sp.X + size, sp.Y + size);
                    g.DrawLine(pen, p1, p2);
                    g.DrawLine(pen, p3, p2);
                    g.DrawLine(pen, p1, p3);
                }
                else if (PointSign == PointSigns[5])//实心三角形
                {
                    PointF p1 = new PointF(sp.X, sp.Y - size);
                    PointF p2 = new PointF(sp.X - size, sp.Y + size);
                    PointF p3 = new PointF(sp.X + size, sp.Y + size);
                    PointF[] points = { p1, p2, p3 };
                    FillMode newFillMode = FillMode.Winding;
                    g.FillPolygon(brush, points, newFillMode);
                }
                else if (PointSign == PointSigns[6])//空心同心圆
                {
                    g.DrawEllipse(pen, sp.X - size, sp.Y - size, size * 2, size * 2);
                    g.DrawEllipse(pen, sp.X - size * 2, sp.Y - size * 2, size * 4, size * 4);
                }
                else if (PointSign == PointSigns[7])//实心同心圆
                {
                    g.FillEllipse(brush, sp.X - size, sp.Y - size, size * 2, size * 2);
                    g.DrawEllipse(pen, sp.X - size * 2, sp.Y - size * 2, size * 4, size * 4);
                }
            }
        }
        private void PaintPolyline(MLFeature feature, Pen pen, Graphics g, string LineStyle)  //绘制线要素
        {
            if (LineStyle == "Solid")
            {
                pen.DashStyle = DashStyle.Solid;
            }
            else
            {
                pen.DashStyle = DashStyle.Dash;
            }
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
        private void PaintPolygon(MLFeature feature, Brush brush, Pen pen, Graphics g)  //绘制面要素，仅支持简单面
        {
            PolygonD polygon = ((MLPolygon)feature).Polygon;
            for (int k = 0; k != polygon.Count; ++k)
            {
                PolylineD ring = polygon.GetRing(k);
                PointF[] ps = new PointF[ring.Count];
                for (int i = 0; i != ps.Length; ++i)
                    ps[i] = FromMapPoint(new PointF((float)ring.GetPoint(i).X, (float)ring.GetPoint(i).Y));
                g.FillPolygon(brush, ps);
                g.DrawPolygon(pen, ps);
            }
        }
        private void SelectPolygon(MLFeature feature, Pen pen, Graphics g)  //不填充仅描边
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
        }
        //绘制要素
        private void DrawFeatureClass(Graphics g)  //画出所有图层中的所有要素
        {
            if (dataFrame != null && dataFrame.layers != null)  //必须要有图层才能开始画
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int i = 0; i <dataFrame.layers.Count; ++i)  //从最后一个图层开始画，越上层的越不会被遮盖
                {
                    Color color = dataFrame.layers[i].CurColor;
                    Brush brush = new SolidBrush(color);
                    Pen pen = new Pen(color, boundaryWidth);
                    MLFeatureClass fc = dataFrame.layers[i].featureClass;
                    List<Color> colors = dataFrame.layers[i].RenderColors;
                    int render = dataFrame.layers[i].renderMethod;
                    string _selectedValue = dataFrame.layers[i]._selectedValue;
                    for (int j = 0; j != fc.Count; ++j)
                    {
                        switch (fc.Type)
                        {
                            case FeatureType.POINT:
                                if (render == 0)
                                {
                                    PaintPoint(fc.GetFeature(j), brush, pen, g, dataFrame.layers[i].PointSign, dataFrame.layers[i].PointSize);
                                }
                                else if (render == 1)
                                {
                                    brush = new SolidBrush(colors[j]);
                                    pen = new Pen(colors[j], boundaryWidth);
                                    PaintPoint(fc.GetFeature(j), brush, pen, g, dataFrame.layers[i].PointSign, dataFrame.layers[i].PointSize);
                                }
                                else if (render == 2)
                                {

                                    int k;
                                    float max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    float min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    for (k = 0; k < dataFrame.layers[i].featureClass.Count; k++)
                                    {
                                        if (max < (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                        if (min > (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                    }
                                    float step = (max - min) / 5;
                                    float cur = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[j][_selectedValue]);
                                    int index = Convert.ToInt32(Math.Floor(Convert.ToDecimal((cur - min) / step)));
                                    index = Math.Min(colors.Count - 1, index);
                                    brush = new SolidBrush(colors[index]);
                                    pen = new Pen(colors[index], boundaryWidth);
                                    PaintPoint(fc.GetFeature(j), brush, pen, g, dataFrame.layers[i].PointSign, dataFrame.layers[i].PointSize);

                                }

                                break;
                            case FeatureType.MULTIPOINT:
                                if (render == 0)
                                {
                                    PaintMultiPoint(fc.GetFeature(j), brush, pen, g, dataFrame.layers[i].PointSign, dataFrame.layers[i].PointSize);
                                }
                                else if (render == 1)
                                {
                                    brush = new SolidBrush(colors[j]);
                                    pen = new Pen(colors[j], boundaryWidth);
                                    PaintMultiPoint(fc.GetFeature(j), brush, pen, g, dataFrame.layers[i].PointSign, dataFrame.layers[i].PointSize);
                                }
                                else if (render == 2)
                                {

                                    int k;
                                    float max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    float min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    for (k = 0; k < dataFrame.layers[i].featureClass.Count; k++)
                                    {
                                        if (max < (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                        if (min > (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                    }
                                    float step = (max - min) / 5;
                                    float cur = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[j][_selectedValue]);
                                    int index = Convert.ToInt32(Math.Floor(Convert.ToDecimal((cur - min) / step)));
                                    index = Math.Min(colors.Count - 1, index);
                                    brush = new SolidBrush(colors[index]);
                                    pen = new Pen(colors[index], boundaryWidth);
                                    PaintMultiPoint(fc.GetFeature(j), brush, pen, g, dataFrame.layers[i].PointSign, dataFrame.layers[i].PointSize);

                                }

                                break;
                            case FeatureType.POLYLINE:
                                pen.Width = dataFrame.layers[i].LineWidth;
                                if (render == 0)
                                {
                                    PaintPolyline(fc.GetFeature(j), pen, g, dataFrame.layers[i].LineStyle);
                                }
                                else if (render == 1)
                                {
                                    brush = new SolidBrush(colors[j]);
                                    pen = new Pen(colors[j], boundaryWidth);
                                    PaintPolyline(fc.GetFeature(j), pen, g, dataFrame.layers[i].LineStyle);
                                }
                                else if (render == 2)
                                {

                                    int k;
                                    float max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    float min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    for (k = 0; k < dataFrame.layers[i].featureClass.Count; k++)
                                    {
                                        if (max < (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                        if (min > (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                    }
                                    float step = (max - min) / 5;
                                    float cur = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[j][_selectedValue]);
                                    int index = Convert.ToInt32(Math.Floor(Convert.ToDecimal((cur - min) / step)));
                                    index = Math.Min(colors.Count - 1, index);
                                    brush = new SolidBrush(colors[index]);
                                    pen = new Pen(colors[index], boundaryWidth);
                                    PaintPolyline(fc.GetFeature(j), pen, g, dataFrame.layers[i].LineStyle);
                                }
                                //PaintPolyline(fc.GetFeature(j), pen, g,dataFrame.layers[i].LineStyle);
                                break;
                            case FeatureType.POLYGON:
                                pen.Width = dataFrame.layers[i].LineWidth;
                                if (render == 0)
                                {
                                    PaintPolygon(fc.GetFeature(j), brush, pen, g);
                                }
                                else if (render == 1)
                                {
                                    brush = new SolidBrush(colors[j]);
                                    pen = new Pen(colors[j], boundaryWidth);
                                    PaintPolygon(fc.GetFeature(j), brush, pen, g);
                                }
                                else if (render == 2)
                                {

                                    int k;
                                    float max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    float min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[0][_selectedValue]);
                                    for (k = 0; k < dataFrame.layers[i].featureClass.Count; k++)
                                    {
                                        if (max < (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            max = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                        if (min > (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]))
                                        {
                                            min = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[k][_selectedValue]);
                                        }
                                    }
                                    float step = (max - min) / 5;
                                    float cur = (float)Convert.ToDouble(dataFrame.layers[i].featureClass.AttributeData.Rows[j][_selectedValue]);
                                    int index = Convert.ToInt32(Math.Floor(Convert.ToDecimal((cur - min) / step)));
                                    index = Math.Min(colors.Count - 1, index);
                                    brush = new SolidBrush(colors[index]);
                                    pen = new Pen(colors[index], boundaryWidth);
                                    PaintPolygon(fc.GetFeature(j), brush, pen, g);
                                }
                                //PaintPolygon(fc.GetFeature(j), brush, pen, g);
                                break;
                        }
                    }
                    pen.Dispose();
                    brush.Dispose();
                }
            }
        }

        private void LoadAnnotation(Graphics g)
        {
            if (dataFrame != null && dataFrame.layers != null)  //必须要有图层才能开始画
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int i = dataFrame.layers.Count - 1; i != -1; --i)  //从最后一个图层开始画，越上层的越不会被遮盖
                {

                    int annoIndex = dataFrame.layers[i].annotateIndex;
                    if (annoIndex > -1)
                    {
                        Brush brush = new SolidBrush(dataFrame.layers[i].annotateColor);
                        Font font = new Font(dataFrame.layers[i].annotateFontStyle, dataFrame.layers[i].annotateFontSize, FontStyle.Bold);
                        MLFeatureClass fc = dataFrame.layers[i].featureClass;
                        string anno;
                        for (int j = 0; j != fc.Count; ++j)
                        {
                            PointF fromP = new PointF((float)(fc.GetFeature(j).XMin + fc.GetFeature(j).XMax) / 2, (float)(fc.GetFeature(j).YMin + fc.GetFeature(j).YMax) / 2);
                            PointF ToP = FromMapPoint(fromP);
                            anno = fc.GetAttributeCell(j, annoIndex).ToString();
                            ToP.X -= anno.Length * 10;
                            g.DrawString(anno, font, brush, ToP);
                        }
                        brush.Dispose();
                        font.Dispose();
                    }
                }

            }
        }


        private void DrawTrackingFeatures(Graphics g)  //描绘正在追踪中的要素
        {
            int sPointCount = trackingFeature.Count;
            if (sPointCount != 0)  //当然前提是已经点下了至少一个点
            {
                FeatureType mTrackingType = dataFrame.layers[dataFrame.index].featureClass.featureType;
                if (mTrackingType != FeatureType.POINT)  //如果类型是点要素，就不必绘制
                {
                    //坐标转换
                    PointF[] sScreenPoints = new PointF[sPointCount];
                    for (int i = 0; i < sPointCount; ++i)
                    {
                        PointF sScreenPoint = FromMapPoint(new PointF((float)trackingFeature[i].X, (float)trackingFeature[i].Y));
                        sScreenPoints[i].X = (float)sScreenPoint.X;
                        sScreenPoints[i].Y = (float)sScreenPoint.Y;
                    }
                    SolidBrush sVertexBrush = new SolidBrush(trackingColor);  //把所有点先以方格的形式绘制出来
                    for (int i = 0; i < sPointCount; ++i)
                    {
                        RectangleF sRect = new RectangleF(sScreenPoints[i].X - vertexHandleSize / 2, sScreenPoints[i].Y - vertexHandleSize / 2, vertexHandleSize, vertexHandleSize);
                        g.FillRectangle(sVertexBrush, sRect);
                    }
                    if (mTrackingType != FeatureType.MULTIPOINT)  //如果是多点要素就不必连线了，但如果是线要素或者面要素就需要连线
                    {
                        Pen sTrackingPen = new Pen(trackingColor, trackingWidth);
                        if (sPointCount > 1)  //如果有至少2个点，就需要把点与点连接起来
                        {
                            g.DrawLines(sTrackingPen, sScreenPoints);
                        }
                        if (mapOpStyle == 4)  //如果处于创建要素的过程中
                        {
                            g.DrawLine(sTrackingPen, sScreenPoints[sPointCount - 1], mouseLocation);  //需要连接鼠标和最后一个点
                            if (sPointCount > 1 && mTrackingType == FeatureType.POLYGON)  //如果是面要素，还需要让曲线闭合
                                g.DrawLine(sTrackingPen, sScreenPoints[0], mouseLocation);
                        }
                        sTrackingPen.Dispose();
                    }
                    sVertexBrush.Dispose();
                }
            }
        }

        private void DrawSelectedFeatures(Graphics g)  //画出被选中的要素
        {
            if (selectedFeatures.Count != 0)  //如果有要素被选中
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Pen pen = new Pen(selectedColor, 2);
                SolidBrush brush = new SolidBrush(selectedColor);
                for (int i = selectedFeatures.Count - 1; i != -1; --i)
                {
                    MLFeature fc = dataFrame.layers[selectedFeatures[i].numLayer].featureClass.GetFeature(selectedFeatures[i].numFeature);
                    switch (fc.FeatureType)
                    {
                        case FeatureType.POINT:
                            PaintPoint(fc, brush, pen, g, dataFrame.layers[selectedFeatures[i].numLayer].PointSign, dataFrame.layers[selectedFeatures[i].numLayer].PointSize);
                            break;
                        case FeatureType.MULTIPOINT:
                            PaintMultiPoint(fc, brush, pen, g, dataFrame.layers[selectedFeatures[i].numLayer].PointSign, dataFrame.layers[selectedFeatures[i].numLayer].PointSize);
                            break;
                        case FeatureType.POLYLINE:
                            pen.Width = dataFrame.layers[selectedFeatures[i].numLayer].LineWidth + 2;
                            PaintPolyline(fc, pen, g, dataFrame.layers[selectedFeatures[i].numLayer].LineStyle);
                            break;
                        case FeatureType.POLYGON:
                            pen.Width = dataFrame.layers[selectedFeatures[i].numLayer].LineWidth + 2;
                            SelectPolygon(fc, pen, g);
                            break;
                    }
                }
                brush.Dispose();
                pen.Dispose();
            }
        }
        private void DrawMergeFeatures(Graphics g)  //画出待融合的要素
        {
            if (mergeFeatures.Count > 1 && selectedFeatures.Count != 0)  //如果有要素被选中
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Pen pen = new Pen(selectedColor, 2);
                SolidBrush brush = new SolidBrush(selectedColor);
                for (int i = 1; i != mergeFeatures.Count; ++i)
                {
                    switch (dataFrame.layers[selectedFeatures[0].numLayer].featureClass.featureType)
                    {
                        case FeatureType.POINT:
                            PaintPoint(mergeFeatures[i], brush, pen, g, dataFrame.layers[selectedFeatures[0].numLayer].PointSign, dataFrame.layers[selectedFeatures[0].numLayer].PointSize);
                            break;
                        case FeatureType.MULTIPOINT:
                            PaintMultiPoint(mergeFeatures[i], brush, pen, g, dataFrame.layers[selectedFeatures[0].numLayer].PointSign, dataFrame.layers[selectedFeatures[0].numLayer].PointSize);
                            break;
                        case FeatureType.POLYLINE:
                            pen.Width = dataFrame.layers[selectedFeatures[0].numLayer].LineWidth + 2;
                            PaintPolyline(mergeFeatures[i], pen, g, dataFrame.layers[selectedFeatures[0].numLayer].LineStyle);
                            break;
                        case FeatureType.POLYGON:
                            pen.Width = dataFrame.layers[selectedFeatures[0].numLayer].LineWidth + 2;
                            SelectPolygon(mergeFeatures[i], pen, g);
                            break;
                    }
                }
                brush.Dispose();
                pen.Dispose();
            }
        }
        #endregion

        #region 母版事件处理
        private void MLMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (mapOpStyle)
                {
                    case 0:
                        break;
                    case 1:  //放大
                        ZoomByCenter(ToMapPoint(new PointF(e.Location.X, e.Location.Y)), zoomRatio);
                        Refresh();
                        break;
                    case 2:  //缩小
                        ZoomByCenter(ToMapPoint(new PointF(e.Location.X, e.Location.Y)), 1 / zoomRatio);
                        Refresh();
                        break;
                    case 3:  //漫游
                        mouseLocation.X = e.Location.X;
                        mouseLocation.Y = e.Location.Y;
                        break;
                    case 4:  //输入要素
                        if (e.Clicks == 1)
                        {
                            PointF p = ToMapPoint(new PointF(e.Location.X, e.Location.Y));
                            switch (dataFrame.layers[dataFrame.index].featureClass.featureType)
                            {
                                case FeatureType.POINT:  //如果是点要素，那点击即创建
                                    MLPoint point = new MLPoint(new PointD(p.X, p.Y));
                                    if (TrackingFinished != null)
                                    {
                                        TrackingFinished(this, point);
                                    }
                                    break;
                                default:
                                    trackingFeature.Add(new PointD(p.X, p.Y));
                                    Refresh();
                                    break;
                            }
                        }
                        break;
                    case 5:  //选择
                        startPoint = e.Location;
                        break;
                    case 6:  //开始移动图形
                        startPoint = e.Location;
                        break;
                    case 7:  //开始编辑节点;
                        MLFeature fc = dataFrame.layers[selectedFeatures[0].numLayer].featureClass.GetFeature(selectedFeatures[0].numFeature);
                        EditPoint.isLinkPoint = false;
                        startPoint = e.Location;
                        switch (fc.FeatureType)
                        {
                            case FeatureType.POINT:
                                if (PointFInPointD(e.Location, ((MLPoint)fc).Point))
                                {
                                    EditPoint.p = ((MLPoint)fc).Point;
                                    return;
                                }
                                break;
                            case FeatureType.MULTIPOINT:
                                PointD[] ps = ((MLMultiPoint)fc).Points;
                                for (int i = 0; i != ps.Length; ++i)
                                {
                                    if (PointFInPointD(e.Location, ps[i]))
                                    {
                                        EditPoint.p = ps[i];
                                        return;
                                    }
                                }
                                break;
                            case FeatureType.POLYLINE:
                                PolylineD[] polylines = ((MLPolyline)fc).Segments;
                                for (int i = 0; i != polylines.Length; ++i)
                                {
                                    for (int j = 0; j != polylines[i].Count; ++j)
                                    {
                                        if (PointFInPointD(e.Location, polylines[i].GetPoint(j)))
                                        {
                                            EditPoint.p = polylines[i].GetPoint(j);
                                            return;
                                        }
                                    }
                                }
                                break;
                            case FeatureType.POLYGON:
                                PolygonD pd = ((MLPolygon)fc).Polygon;
                                for (int i = 0; i != pd.Count; ++i)
                                {
                                    if (PointFInPointD(e.Location, pd.GetRing(i).GetPoint(0)))
                                    {
                                        PolylineD pld = pd.GetRing(i);
                                        pld.RemoveLastOne();
                                        pld.AddPoint(pld.GetPoint(0));
                                        EditPoint.p = pld.GetPoint(0);
                                        EditPoint.isLinkPoint = true;
                                        EditPoint.numLayer = selectedFeatures[0].numLayer;
                                        EditPoint.numPolygon = selectedFeatures[0].numFeature;
                                        EditPoint.numRing = i;
                                        return;
                                    }
                                    for (int j = 0; j != pd.GetRing(i).Count - 1; ++j)
                                    {
                                        if (PointFInPointD(e.Location, pd.GetRing(i).GetPoint(j)))
                                        {
                                            EditPoint.p = pd.GetRing(i).GetPoint(j);
                                            return;
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 8:
                        MLFeatureClass mfc = dataFrame.layers[selectedFeatures[0].numLayer].featureClass;
                        for (int i = 0; i != mfc.Count; ++i)
                        {
                            if (!mergeFeatures.Contains(mfc.GetFeature(i)))
                            {
                                switch (mfc.featureType)
                                {
                                    case FeatureType.POINT:
                                        break;
                                    case FeatureType.MULTIPOINT:
                                        MLMultiPoint mp = (MLMultiPoint)mfc.GetFeature(i);
                                        for (int j = 0; j != mp.Points.Length; ++j)
                                        {
                                            if (PointFInPointD(e.Location, mp.Points[j]))
                                            {
                                                mergeFeatures.Add(mp);
                                                Refresh();
                                                return;
                                            }
                                        }
                                        break;
                                    case FeatureType.POLYLINE:
                                        MLPolyline polyline = (MLPolyline)mfc.GetFeature(i);
                                        for (int j = 0; j != polyline.Segments.Length; ++j)
                                        {
                                            for (int k = 1; k != polyline.Segments[j].Count; ++k)
                                            {
                                                if (PointFInLine(e.Location, polyline.Segments[j].GetPoint(k - 1), polyline.Segments[j].GetPoint(k)))
                                                {
                                                    mergeFeatures.Add(polyline);
                                                    Refresh();
                                                    return;
                                                }
                                            }
                                        }
                                        break;
                                    case FeatureType.POLYGON:
                                        MLPolygon polygon = (MLPolygon)mfc.GetFeature(i);
                                        for (int j = 0; j != polygon.Polygon.Count; ++j)
                                        {
                                            if (PointFInPolygon(e.Location, polygon.Polygon.GetRing(j).GetPoints()))
                                            {
                                                mergeFeatures.Add(polygon);
                                                Refresh();
                                                return;
                                            }
                                        }
                                        break;
                                }
                            }

                        }
                        break;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i != selectedFeatures.Count; ++i)
                {
                    bool selected = false;
                    MLFeature fc = dataFrame.layers[selectedFeatures[i].numLayer].featureClass.GetFeature(selectedFeatures[i].numFeature);
                    switch (fc.FeatureType)
                    {
                        case FeatureType.POINT:
                            if (PointInMLPoint(e.Location, (MLPoint)fc))
                                selected = true;
                            break;
                        case FeatureType.MULTIPOINT:
                            if (PointInMLMultiPoint(e.Location, (MLMultiPoint)fc))
                                selected = true;
                            break;
                        case FeatureType.POLYLINE:
                            if (PointInMLPolyline(e.Location, (MLPolyline)fc))
                                selected = true;
                            break;
                        case FeatureType.POLYGON:
                            if (PointInMLPolygon(e.Location, (MLPolygon)fc))
                                selected = true;
                            break;
                    }
                    if (selected)
                    {
                        selectedFeatures = new List<Selected>() { selectedFeatures[i] };
                        Refresh();
                        featureMenu.Show(MousePosition.X, MousePosition.Y);
                        return;
                    }
                }
            }
        }

        private void MLMouseMove(object sender, MouseEventArgs e)
        {
            switch (mapOpStyle)
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
                        PointF sPreMouseLocation = new PointF(mouseLocation.X, mouseLocation.Y);
                        PointF sPrePoint = ToMapPoint(sPreMouseLocation);
                        PointF sCurMouseLocation = new PointF(e.Location.X, e.Location.Y);
                        PointF sCurPoint = ToMapPoint(sCurMouseLocation);
                        //修改偏移量
                        offsetX = offsetX + sPrePoint.X - sCurPoint.X;
                        offsetY = offsetY + sPrePoint.Y - sCurPoint.Y;
                        Refresh();
                        mouseLocation.X = e.Location.X;
                        mouseLocation.Y = e.Location.Y;
                    }
                    break;
                case 4:  //输入多边形
                    mouseLocation.X = e.Location.X;
                    mouseLocation.Y = e.Location.Y;
                    Refresh();
                    break;
                case 5:  //选择
                    if (e.Button == MouseButtons.Left)
                    {
                        Refresh();
                        Graphics g = Graphics.FromHwnd(this.Handle);
                        Pen sBoxPen = new Pen(selectingBoxColor, selectingBoxWidth);
                        float sMinX = Math.Min(startPoint.X, e.Location.X);
                        float sMaxX = Math.Max(startPoint.X, e.Location.X);
                        float sMinY = Math.Min(startPoint.Y, e.Location.Y);
                        float sMaxY = Math.Max(startPoint.Y, e.Location.Y);
                        g.DrawRectangle(sBoxPen, sMinX, sMinY, sMaxX - sMinX, sMaxY - sMinY);
                        g.Dispose();
                    }
                    break;
                case 6:  //移动图形
                    if (e.Button == MouseButtons.Left)
                    {
                        MLFeature fe = dataFrame.layers[selectedFeatures[0].numLayer].featureClass.GetFeature(selectedFeatures[0].numFeature);
                        fe.Move(displayScale*( e.Location.X - startPoint.X),displayScale*( startPoint.Y - e.Location.Y));
                        startPoint = e.Location;
                        Refresh();
                    }
                    break;
                case 7:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (EditPoint.p != null)
                        {
                            EditPoint.p.Move(displayScale * (e.Location.X - startPoint.X), displayScale * (startPoint.Y - e.Location.Y));
                            startPoint = e.Location;
                            Refresh();
                        }
                    }
                    break;
            }
        }

        private void MLMouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch (mapOpStyle)
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
                        switch (dataFrame.layers[dataFrame.index].featureClass.featureType)
                        {
                            case FeatureType.POINT:
                                //这个是真没有要执行的
                                break;
                            case FeatureType.MULTIPOINT:
                                PointD[] multipointPs = new PointD[trackingFeature.Count];
                                for (int i = 0; i != multipointPs.Length; ++i)
                                    multipointPs[i] = trackingFeature[i];
                                MLMultiPoint multipoint = new MLMultiPoint(multipointPs);
                                if (TrackingFinished != null)
                                {
                                    TrackingFinished(this, multipoint);
                                }
                                trackingFeature.Clear();
                                break;
                            case FeatureType.POLYLINE:
                                PointD[] polylinePs = new PointD[trackingFeature.Count];
                                for (int i = 0; i != polylinePs.Length; ++i)
                                    polylinePs[i] = trackingFeature[i];
                                MLPolyline polyline = new MLPolyline(polylinePs);
                                if (TrackingFinished != null)
                                {
                                    TrackingFinished(this, polyline);
                                }
                                trackingFeature.Clear();
                                break;
                            case FeatureType.POLYGON:
                                if (trackingFeature.Count >= 3)
                                {
                                    trackingFeature.Add(new PointD(trackingFeature[0].X, trackingFeature[0].Y));
                                    PointD[] polygonPs = new PointD[trackingFeature.Count];
                                    for (int i = 0; i != polygonPs.Length; ++i)
                                        polygonPs[i] = trackingFeature[i];
                                    MLPolygon polygon = new MLPolygon(new PolylineD(polygonPs));
                                    if (TrackingFinished != null)
                                    {
                                        TrackingFinished(this, polygon);
                                    }
                                    trackingFeature.Clear();
                                }
                                break;
                        }
                    }
                    break;
                case 5:  //选择
                    break;
                case 8:
                    if (e.Button == MouseButtons.Left)
                    {
                        MLFeatureClass fc = dataFrame.layers[selectedFeatures[0].numLayer].featureClass;
                        for (int i = 1; i != mergeFeatures.Count; ++i)
                        {
                            switch (fc.featureType)
                            {
                                case FeatureType.POINT:
                                    break;
                                case FeatureType.MULTIPOINT:
                                    MLMultiPoint parent = (MLMultiPoint)mergeFeatures[0];
                                    MLMultiPoint child = (MLMultiPoint)mergeFeatures[i];
                                    for (int j = 0; j != child.Points.Length; ++j)
                                        parent.AddPoint(child.Points[j]);
                                    fc.RemoveFeaure(child);
                                    break;
                                case FeatureType.POLYLINE:
                                    MLPolyline parent2 = (MLPolyline)mergeFeatures[0];
                                    MLPolyline child2 = (MLPolyline)mergeFeatures[i];
                                    for (int j = 0; j != child2.Segments.Length; ++j)
                                        parent2.AddLine(child2.Segments[j]);
                                    fc.RemoveFeaure(child2);
                                    break;
                                case FeatureType.POLYGON:
                                    MLPolygon parent3 = (MLPolygon)mergeFeatures[0];
                                    MLPolygon child3 = (MLPolygon)mergeFeatures[i];
                                    for (int j = 0; j != child3.Polygon.Count; ++j)
                                        parent3.AddPolygon(child3.Polygon.GetRing(j));
                                    fc.RemoveFeaure(child3);
                                    break;
                            }
                        }
                        selectedFeatures.Clear();
                        mergeFeatures.Clear();
                        mapOpStyle = 0;
                        Refresh();
                        //mergeFeatures = new List<MLFeature>() { dataFrame.layers[selectedFeatures[0].numLayer].featureClass.GetFeature(selectedFeatures[0].numFeature) };
                        MessageBox.Show("完成合并");
                    }
                    break;
            }
        }
        private void MLMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                PointF sCenterPoint = new PointF(this.ClientSize.Width / 2, this.ClientSize.Height / 2);  //屏幕中心点
                PointF sCenterPointOnMap = ToMapPoint(sCenterPoint); //中心的地图坐标
                ZoomByCenter(sCenterPointOnMap, zoomRatio);
                Refresh();
            }
            else
            {
                PointF sCenterPoint = new PointF(this.ClientSize.Width / 2, this.ClientSize.Height / 2);  //屏幕中心点
                PointF sCenterPointOnMap = ToMapPoint(sCenterPoint); //中心的地图坐标
                ZoomByCenter(sCenterPointOnMap, 1 / zoomRatio);
                Refresh();
            }
        }
        private void MLMouseUp(object sender, MouseEventArgs e)
        {
            switch (mapOpStyle)
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
                        float sMinX = Math.Min(startPoint.X, e.Location.X);
                        float sMaxX = Math.Max(startPoint.X, e.Location.X);
                        float sMinY = Math.Min(startPoint.Y, e.Location.Y);
                        float sMaxY = Math.Max(startPoint.Y, e.Location.Y);
                        PointF sTopLeft = new PointF(sMinX, sMinY);
                        PointF sBottomRight = new PointF(sMaxX, sMaxY);
                        PointF sTopLeftOnMap = ToMapPoint(sTopLeft);
                        PointF sBottomRightOnMap = ToMapPoint(sBottomRight);
                        float width = sBottomRightOnMap.X - sTopLeftOnMap.X;
                        float height = sTopLeftOnMap.Y - sBottomRightOnMap.Y;
                        RectangleF sSelBox = new RectangleF(sTopLeftOnMap.X, sBottomRightOnMap.Y, width, height);
                        Refresh();
                        SelectingFinished?.Invoke(this, sSelBox);
                    }
                    break;
                case 6:
                    break;
                case 7:
                    if (EditPoint.isLinkPoint)
                    {
                        MLPolygon mp = (MLPolygon)dataFrame.layers[EditPoint.numLayer].featureClass.GetFeature(EditPoint.numPolygon);
                        PolylineD pd = mp.Polygon.GetRing(EditPoint.numRing);
                        pd.RemoveLastOne();
                        pd.AddPoint(new PointD(pd.GetPoint(0).X, pd.GetPoint(0).Y));
                    }
                    EditPoint.p = null;
                    break;
            }
        }

        private void 删除图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataFrame.layers[selectedFeatures[0].numLayer].featureClass.RemoveFeaure(selectedFeatures[0].numFeature);
            selectedFeatures.Clear();
            if (mapOpStyle == 6)
                mapOpStyle = 0;
            Refresh();
        }

        private void 拖动图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapOpStyle = 6;
            this.Cursor = cur_MoveFeature;
        }

        private void 移动图形坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveByCoor bc = new MoveByCoor();
            bc.MoveByDeltaCoor += new MoveByCoor.MoveByDeltaCoorHandle(MoveByDeltaCoorFunction);
            bc.ShowDialog();
        }
        private void MoveByDeltaCoorFunction(object sender, float dx, float dy)
        {
            dataFrame.layers[selectedFeatures[0].numLayer].featureClass.GetFeature(selectedFeatures[0].numFeature).Move(dx, dy);
            Refresh();
        }

        private void 编辑节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapOpStyle = 7;
            this.Cursor = cur_Cross;
        }
        private void 合并ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapOpStyle = 8;
            this.Cursor = cur_Cross;
            mergeFeatures = new List<MLFeature>() { dataFrame.layers[selectedFeatures[0].numLayer].featureClass.GetFeature(selectedFeatures[0].numFeature) };
        }

        //母版重绘
        private void MLPaint(object sender, PaintEventArgs e)
        {
            if (image != null)
            {
                Graphics g = e.Graphics;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                float scale = Math.Min((float)Height / image.Height, (float)Width / image.Width);
                g.DrawImage(image, new RectangleF((-scale * image.Width + Width) / 2, (-scale * image.Height + Height) / 2, scale * image.Width, scale * image.Height));
            }
            //绘制所有多边形
            DrawFeatureClass(e.Graphics);

            //绘制跟踪多边形
            DrawTrackingFeatures(e.Graphics);

            //绘制选中多边形
            DrawSelectedFeatures(e.Graphics);

            DrawMergeFeatures(e.Graphics);

            LoadAnnotation(e.Graphics);
        }


        #endregion
    }
}
