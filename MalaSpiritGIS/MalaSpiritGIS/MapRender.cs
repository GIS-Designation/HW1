using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MalaSpiritGIS.MLDataFrame;

namespace MalaSpiritGIS
{
    public partial class MapRender : Form
    {
        public MapRender()
        {
            InitializeComponent();
        }

        #region 字段
        private Layer _CurLayer;//当前处理的图层
        private Color _color;//要素符号的颜色
        private int _renderMethod;//渲染方法，0：单一符号法；1：唯一值法；2：分级法
        #endregion


        #region   属性
        /// <summary>
        /// 获取或设置当前处理图层
        /// </summary>
        public Layer CurLayer
        {
            get { return _CurLayer; }
            set { _CurLayer = value; }
        }
        /// <summary>
        /// 获取或设置当前符号颜色
        /// </summary>
        public Color color
        {
            get { return _color; }
            set { _color = value; }
        }
        /// <summary>
        /// 获取或设置地图渲染方法
        /// </summary>
        public int renderMethod
        {
            get { return _renderMethod; }
            set { _renderMethod = value; }
        }

        #endregion

        
        #region  私有函数
        //显示当前颜色
        private void ShowSimpleColor()
        {
            lblSimpleColor.BackColor = _color;
        }
        #endregion

        #region 单一符号法

        //颜色选择按钮
        private void btnSimpleColor_Click(object sender, EventArgs e)
        {
            ColorDialog sDialog = new ColorDialog();
            sDialog.Color = _color;
            if (sDialog.ShowDialog(this) == DialogResult.OK)
            {
                _color = sDialog.Color;
                _CurLayer.CurColor = sDialog.Color;
                ShowSimpleColor();
                _renderMethod = 0;
            }
            sDialog.Dispose();
            groupBox1.Refresh();
        }

        //绘制当前符号
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(_color, 1);
            SolidBrush brush = new SolidBrush(_color);
            e.Graphics.TranslateTransform(250, 50);//移动原点
            if (_CurLayer.featureClass.featureType == FeatureType.POINT || _CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
            {
                //如果是点或多点要素
                PointF sScreenPoint = new PointF(0, 0);
                string PointSign = _CurLayer.PointSign;
                float _size = CurLayer.PointSize;
                if (PointSign == "HollowCircle")//空心圆
                {
                    g.DrawEllipse(pen, sScreenPoint.X - _size, sScreenPoint.Y - _size, _size * 2, _size * 2);
                }
                else if (PointSign == "FilledCircle")//实心圆
                {
                    g.FillEllipse(brush, sScreenPoint.X - _size, sScreenPoint.Y - _size, _size * 2, _size * 2);
                }
                else if (PointSign == "HollowSquare")//空心正方形
                {
                    g.DrawRectangle(pen, sScreenPoint.X - _size, sScreenPoint.Y - _size, _size * 2, _size * 2);
                }
                else if (PointSign == "FilledSquare")//实心正方形
                {
                    g.FillRectangle(brush, sScreenPoint.X - _size, sScreenPoint.Y - _size, _size * 2, _size * 2);
                }
                else if (PointSign == "HollowTriangle")//空心三角形
                {
                    PointF p1 = new PointF(sScreenPoint.X, sScreenPoint.Y - _size);
                    PointF p2 = new PointF(sScreenPoint.X - _size, sScreenPoint.Y + _size);
                    PointF p3 = new PointF(sScreenPoint.X + _size, sScreenPoint.Y + _size);
                    g.DrawLine(pen, p1, p2);
                    g.DrawLine(pen, p3, p2);
                    g.DrawLine(pen, p1, p3);
                }
                else if (PointSign == "FilledTriangle")//实心三角形
                {
                    PointF p1 = new PointF(sScreenPoint.X, sScreenPoint.Y - _size);
                    PointF p2 = new PointF(sScreenPoint.X - _size, sScreenPoint.Y + _size);
                    PointF p3 = new PointF(sScreenPoint.X + _size, sScreenPoint.Y + _size);
                    PointF[] points = { p1, p2, p3 };
                    FillMode newFillMode = FillMode.Winding;
                    g.FillPolygon(brush, points, newFillMode);
                }
                else if (PointSign == "HollowConcentricCircles")//空心同心圆
                {
                    g.DrawEllipse(pen, sScreenPoint.X - _size, sScreenPoint.Y - _size, _size * 2, _size * 2);
                    g.DrawEllipse(pen, sScreenPoint.X - _size * 2, sScreenPoint.Y - _size * 2, _size * 4, _size * 4);
                }
                else if (PointSign == "FilledConcentricCircles")//实心同心圆
                {
                    g.FillEllipse(brush, sScreenPoint.X - _size, sScreenPoint.Y - _size, _size * 2, _size * 2);
                    g.DrawEllipse(pen, sScreenPoint.X - _size * 2, sScreenPoint.Y - _size * 2, _size * 4, _size * 4);
                }
            }
            else if (_CurLayer.featureClass.featureType == FeatureType.POLYLINE)
            {
                //如果是线要素
                if (_CurLayer.LineStyle == "Solid")
                {
                    pen.DashStyle = DashStyle.Solid;
                }
                else
                {
                    pen.DashStyle = DashStyle.Dash;
                }
                pen.Width = CurLayer.LineWidth;//笔的粗细等于此时的轮廓宽度
                g.DrawLine(pen, -10, 0, 10, 0);
            }
            else if (_CurLayer.featureClass.featureType == FeatureType.POLYGON)
            {
                //如果是多边形要素
                g.FillRectangle(brush, -10, 10, 20, 20);
                g.DrawRectangle(pen, -10, 10, 20, 20);
            }
            pen.Dispose();
            brush.Dispose();
        }

        #endregion


    }
}
