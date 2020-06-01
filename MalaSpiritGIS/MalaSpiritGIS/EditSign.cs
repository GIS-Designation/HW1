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
    //修改要素符号
    public partial class EditSign : Form
    {
        public EditSign()
        {
            InitializeComponent();
        }

        #region 字段
        private Layer _CurLayer;//当前处理的图层
        private Color _color;//要素符号的颜色
        private float _size;//符号大小，对线符号来说是宽度，对多边形符号来说是轮廓宽度，点符号大小直接在layer里面修改属性值
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
        /// 获取或设置当前符号大小或宽度
        /// </summary>
        public float size
        {
            get { return _size; }
            set { _size = value; }
        }

        #endregion

        #region 窗体和控件事件处理

        //颜色选择按钮
        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog sDialog = new ColorDialog();
            sDialog.Color = _color;
            if (sDialog.ShowDialog(this) == DialogResult.OK)
            {
                _color = sDialog.Color;
                _CurLayer.CurColor = sDialog.Color;
                ShowColor();
            }
            sDialog.Dispose();
            groupBox1.Refresh();
        }

        //修改符号大小
        private void npdSize_ValueChanged(object sender, EventArgs e)
        {
            if(_CurLayer.featureClass.featureType == FeatureType.POINT || _CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
            {
                //如果是点或多点要素,修改符号大小
                _CurLayer.PointSize = (float)npdSize.Value;
            }
            else
            {
                //线要素和面要素修改轮廓大小
                _size = (float)npdSize.Value;
                _CurLayer.LineWidth = _size;
            }
            groupBox1.Refresh();
        }

        //确定按钮
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //取消按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //绘制当前符号
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(_color, 1);
            SolidBrush brush = new SolidBrush(_color);
            e.Graphics.TranslateTransform(100, 61);//移动原点
            if (_CurLayer.featureClass.featureType == FeatureType.POINT || _CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
            {
                //如果是点或多点要素
                PointF sScreenPoint = new PointF(0, 0);
                string PointSign = _CurLayer.PointSign;
                _size = (float)npdSize.Value;
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
            else if(_CurLayer.featureClass.featureType == FeatureType.POLYLINE)
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
                pen.Width = _size;//笔的粗细等于此时的轮廓宽度
                g.DrawLine(pen, -10, 0, 10, 0);
            }
            else if(_CurLayer.featureClass.featureType == FeatureType.POLYGON)
            {
                //如果是多边形要素
                g.FillRectangle(brush, -10, -10, 20, 20);
                g.DrawRectangle(pen, -10, -10, 20, 20);
            }
            //this.Refresh();
            pen.Dispose();
            brush.Dispose();
            //g.Dispose();
        }

        //装载窗体
        private void EditSign_Load(object sender, EventArgs e)
        {
            ShowColor();
            ShowSize();
            if(_CurLayer.featureClass.featureType == FeatureType.POINT || _CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
            {
                //若为点要素或多点要素
                tabControl1.SelectedTab = tpgPoint;
            }
            else if (_CurLayer.featureClass.featureType == FeatureType.POLYLINE)
            {
                //若为线要素
                tabControl1.SelectedTab = tpgPolyline;
            }
            else
            {
                tabControl1.Enabled = false;
            }
        }
        #endregion

        #region  私有函数
        //显示当前颜色
        private void ShowColor()
        {
            lblColor.BackColor = _color;
        }
        //显示大小
        private void ShowSize()
        {
            if(_CurLayer.featureClass.featureType == FeatureType.POINT || _CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
            {
                //若为点要素或多点要素
                label2.Text = "大小：";
                npdSize.Value = (decimal)_CurLayer.PointSize;
            }
            else if (_CurLayer.featureClass.featureType == FeatureType.POLYLINE)
            {
                //若为线要素或多边形要素
                label2.Text = "宽度：";
                npdSize.Value = (decimal)_CurLayer.LineWidth;
            }
            else
            {
                //若为多边形要素
                npdSize.Enabled = false;
            }
        }


        #endregion

        #region 修改点符号类型
        //空心圆
        private void btnHollowCircle_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "HollowCircle";
            groupBox1.Refresh();
        }
        //实心圆
        private void btnFilledCircle_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "FilledCircle";
            groupBox1.Refresh();
        }
        //空心正方形
        private void btnHollowSquare_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "HollowSquare";
            groupBox1.Refresh();
        }
        //实心正方形
        private void btnFilledSquare_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "FilledSquare";
            groupBox1.Refresh();
        }
        //空心三角形
        private void btnHollowTriangle_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "HollowTriangle";
            groupBox1.Refresh();
        }
        //实心三角形
        private void btnFilledTriangle_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "FilledTriangle";
            groupBox1.Refresh();
        }
        //空心同心圆
        private void btnHollowConcentricCircles_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "HollowConcentricCircles";
            groupBox1.Refresh();
        }
        //实心同心圆
        private void btnFilledConcentricCircles_Click(object sender, EventArgs e)
        {
            _CurLayer.PointSign = "FilledConcentricCircles";
            groupBox1.Refresh();
        }


        #endregion

        #region 修改线符号类型
        private void btnSolidLine_Click(object sender, EventArgs e)
        {
            _CurLayer.LineStyle = "Solid";
            groupBox1.Refresh();
        }

        private void btnDashedLine_Click(object sender, EventArgs e)
        {
            _CurLayer.LineStyle = "Dash";
            groupBox1.Refresh();
        }
        #endregion

        
    }
}
