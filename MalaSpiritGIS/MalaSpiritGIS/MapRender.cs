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
            //fillList();
        }

        #region 字段
        private Layer _CurLayer;//当前处理的图层
        private Color _color;//要素符号的颜色
        private int _renderMethod;//渲染方法，0：单一符号法；1：唯一值法；2：分级法
        private ColorRamp _colorRamp;//当前选择的色带
        private string _selectedValue;//值字段；
        private List<Color> _colors = new List<Color>();//渲染用的颜色



        private List<ColorRamp> myColorRamps = new List<ColorRamp>();//渐变色带列表
        /// <summary>
        /// 色带类
        /// </summary>
        public class ColorRamp
        {
            public Color fromColor { get; set; }
            public Color toColor { get; set; }
        }
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
        /// <summary>
        /// 获取或设置当前渐变色带
        /// </summary>
        public ColorRamp colorRamp
        {
            get { return _colorRamp; }
            set { _colorRamp = value; }
        }
        /// <summary>
        /// 获取或设置所选颜色列表
        /// </summary>
        public List<Color> colors
        {
            get { return _colors; }
            set { _colors = value; }
        }

        public string selectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value; }
        }
        #endregion


        #region  私有函数
        //显示当前颜色
        private void ShowSimpleColor()
        {
            lblSimpleColor.BackColor = _color;
        }
        //最大值
        private int Max(int a,int b,int c)
        {
            int max = a;
            if (max < b)
                max = b;
            if (max < c)
                max = c;
            return max;
        }

        //最小值
        private int Min(int a,int b,int c)
        {
            int min = a;
            if (min > b)
                min = b;
            if (min > c)
                min = c;
            return min;
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
                g.DrawLine(pen, -100, 0, 100, 0);
            }
            else if (_CurLayer.featureClass.featureType == FeatureType.POLYGON)
            {
                //如果是多边形要素
                g.FillRectangle(brush, -100, -10, 200, 20);
                g.DrawRectangle(pen, -100, -10, 200, 20);
            }
            pen.Dispose();
            brush.Dispose();
        }


        #endregion


        #region 确定和取消按钮
        //确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region 窗体加载

        private void MapRender_Load(object sender, EventArgs e)
        {
            ShowSimpleColor();
            fillList();

            UniqueValueList.Items.Clear();
            for (int i = 0; i < CurLayer.featureClass.AttributeData.Columns.Count; i++)
                UniqueValueList.Items.Add(CurLayer.featureClass.AttributeData.Columns[i].ColumnName);

            ColorBar1.DrawMode = DrawMode.OwnerDrawFixed;
            ColorBar1.DropDownStyle = ComboBoxStyle.DropDownList;
            ColorBar1.ItemHeight = 18;
            ColorBar1.BeginUpdate();
            ColorBar1.Items.Clear();
            for (int i = 0; i < myColorRamps.Count; i++)
            {
                ColorBar1.Items.Add(myColorRamps[i].fromColor.Name);
            }
            ColorBar1.EndUpdate();

            rangeValueList.Items.Clear();
            for (int i = 0; i < CurLayer.featureClass.AttributeData.Columns.Count; i++)
                rangeValueList.Items.Add(CurLayer.featureClass.AttributeData.Columns[i].ColumnName);

            ColorBar2.DrawMode = DrawMode.OwnerDrawFixed;
            ColorBar2.DropDownStyle = ComboBoxStyle.DropDownList;
            ColorBar2.ItemHeight = 18;
            ColorBar2.BeginUpdate();
            ColorBar2.Items.Clear();
            for (int i = 0; i < myColorRamps.Count; i++)
            {
                ColorBar2.Items.Add(myColorRamps[i].fromColor.Name);
            }
            ColorBar2.EndUpdate();
        }


        #endregion

        #region 唯一值法

        //选择唯一值字段
        private void UniqueValueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedValue = UniqueValueList.SelectedItem.ToString();
            if (_colors != null)
            {
                _colors.Clear();
            }
            if (listBox1.Items != null)
            {
                listBox1.Items.Clear();
            }
        }

        //显示渐变色带
        private void ColorBar1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Index < 0)
                return;

            Rectangle rect = e.Bounds;
            Color fc = myColorRamps[e.Index].fromColor;
            Color tc = myColorRamps[e.Index].toColor;
            //选择线性渐变刷子
            LinearGradientBrush brush = new LinearGradientBrush(rect, fc, tc, 0, false);

            rect.Inflate(-1, -1);
            // 填充颜色
            g.FillRectangle(brush, rect);
            // 绘制边框
            g.DrawRectangle(Pens.Black, rect);

        }

        //重绘listbox,实时显示颜色条
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Index < 0)
                return;

            e.DrawBackground();
            SolidBrush brush = new SolidBrush(_colors[e.Index]);

            // Draw the current item text based on the current Font and the custom brush settings.
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        //选择色带
        private void ColorBar1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_colors != null)
            {
                _colors.Clear();
            }
            _selectedValue = UniqueValueList.SelectedItem.ToString();
            _colorRamp = myColorRamps[ColorBar1.SelectedIndex];
            if(listBox1.Items != null)
            {
                listBox1.Items.Clear();
            }
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            SolidBrush brush = new SolidBrush(_color);
            int i;
            Random rd = new Random();
            for (i = 0;i<CurLayer.featureClass.Count;i++)
            {
                int r = rd.Next(_colorRamp.fromColor.R, 255);
                Color c = Color.FromArgb(r, _colorRamp.fromColor.G,_colorRamp.fromColor.B);
                _colors.Add(c);
                Font f = new Font("宋体", 9.0f, FontStyle.Bold);
                if(CurLayer.featureClass.featureType == FeatureType.POINT || CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
                {
                    string str = "·             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue]+ "                         1";
                    listBox1.Items.Add(str);
                }
                else if(CurLayer.featureClass.featureType == FeatureType.POLYLINE)
                {
                    string str = "—             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue] + "                         1";
                    listBox1.Items.Add(str);
                }
                else
                {
                    string str = "■             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue] + "                         1";
                    listBox1.Items.Add(str);
                }
            }
            _renderMethod = 1;
        }
        #endregion

        #region 分级法

        //选择分级字段
        private void rangeValueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedValue = rangeValueList.SelectedItem.ToString();
            if (_colors != null)
            {
                _colors.Clear();
            }
            if(listBox2 != null)
            {
                listBox2.Items.Clear();
            }
            
        }

        //绘制色带
        private void ColorBar2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Index < 0)
                return;

            Rectangle rect = e.Bounds;
            Color fc = myColorRamps[e.Index].fromColor;
            Color tc = myColorRamps[e.Index].toColor;
            //选择线性渐变刷子
            LinearGradientBrush brush = new LinearGradientBrush(rect, fc, tc, 0, false);

            rect.Inflate(-1, -1);
            // 填充颜色
            g.FillRectangle(brush, rect);
            // 绘制边框
            g.DrawRectangle(Pens.Black, rect);
        }

        //重绘listbox,实时显示颜色条
        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Index < 0)
                return;

            e.DrawBackground();
            SolidBrush brush = new SolidBrush(_colors[e.Index]);

            // Draw the current item text based on the current Font and the custom brush settings.
            e.Graphics.DrawString(listBox2.Items[e.Index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        //选择色带
        private void ColorBar2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_colors != null)
            {
                _colors.Clear();
            }
            _colorRamp = myColorRamps[ColorBar2.SelectedIndex];
            _selectedValue = rangeValueList.SelectedItem.ToString();
            if (listBox2.Items != null)
            {
                listBox2.Items.Clear();
            }
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            SolidBrush brush = new SolidBrush(_color);
            int i;
            if(CurLayer.featureClass.Count > 5)
            {
                float max = (float)Convert.ToDouble(CurLayer.featureClass.AttributeData.Rows[0][_selectedValue]);
                float min = (float)Convert.ToDouble(CurLayer.featureClass.AttributeData.Rows[0][_selectedValue]);
                for (i = 0; i < CurLayer.featureClass.Count; i++)
                {
                    if (max < (float)Convert.ToDouble(CurLayer.featureClass.AttributeData.Rows[i][_selectedValue]))
                    {
                        max = (float)Convert.ToDouble(CurLayer.featureClass.AttributeData.Rows[i][_selectedValue]);
                    }
                    if (min > (float)Convert.ToDouble(CurLayer.featureClass.AttributeData.Rows[i][_selectedValue]))
                    {
                        min = (float)Convert.ToDouble(CurLayer.featureClass.AttributeData.Rows[i][_selectedValue]);
                    }
                }
                float step = (max - min) / 5;
                int r = _colorRamp.fromColor.R;
                //Color c = Color.FromArgb(r, _colorRamp.fromColor.G, _colorRamp.fromColor.B);
                for (i = 0; i < 5; i++)
                {
                    r = r + 10;
                    Color c = Color.FromArgb(r, _colorRamp.fromColor.G, _colorRamp.fromColor.B);
                    _colors.Add(c);
                    float smax = min + step * i;

                    if (CurLayer.featureClass.featureType == FeatureType.POINT || CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
                    {
                        string str = "·             " + min + "——" + smax + "             " + min + "——" + smax;
                        listBox2.Items.Add(str);
                    }
                    else if (CurLayer.featureClass.featureType == FeatureType.POLYLINE)
                    {
                        string str = "—             " + min + "——" + smax + "             " + min + "——" + smax;
                        listBox2.Items.Add(str);
                    }
                    else
                    {
                        string str = "■             " + min + "——" + smax + "             " + min + "——" + smax;
                        listBox2.Items.Add(str);
                    }
                   
                    
                }
            }
            else
            {
                int r = _colorRamp.fromColor.R;
                for (i = 0; i < CurLayer.featureClass.Count; i++)
                {
                    r = r + 10;
                    Color c = Color.FromArgb(r, _colorRamp.fromColor.G, _colorRamp.fromColor.B);
                    _colors.Add(c);
                    if (CurLayer.featureClass.featureType == FeatureType.POINT || CurLayer.featureClass.featureType == FeatureType.MULTIPOINT)
                    {
                        string str = "·             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue] + "——" + "             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue];
                        listBox2.Items.Add(str);
                    }
                    else if (CurLayer.featureClass.featureType == FeatureType.POLYLINE)
                    {
                        string str = "—             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue] + "——" + "             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue];
                        listBox2.Items.Add(str);
                    }
                    else
                    {
                        string str = "■             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue] + "——" + "             " + CurLayer.featureClass.AttributeData.Rows[i][_selectedValue];
                        listBox2.Items.Add(str);
                    }
                }
            }
            _renderMethod = 2;

        }
        #endregion

        #region 渐变色带列表初始化
        private void fillList()//渐变色带初始化
        {
            ColorRamp colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(176, 176, 176);
            colorRamp.toColor = Color.FromArgb(255, 0, 0);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(0, 0, 0);
            colorRamp.toColor = Color.FromArgb(255, 255, 255);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(104, 204, 255);
            colorRamp.toColor = Color.FromArgb(0, 0, 224);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(111, 229, 232);
            colorRamp.toColor = Color.FromArgb(46, 100, 140);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(103, 245, 234);
            colorRamp.toColor = Color.FromArgb(48, 207, 146);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(116, 242, 237);
            colorRamp.toColor = Color.FromArgb(21, 79, 74);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(140, 236, 170);
            colorRamp.toColor = Color.FromArgb(102, 72, 48);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(156, 85, 31);
            colorRamp.toColor = Color.FromArgb(33, 130, 145);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(48, 100, 102);
            colorRamp.toColor = Color.FromArgb(110, 70, 45);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(114, 47, 39);
            colorRamp.toColor = Color.FromArgb(69, 117, 181);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(145, 0, 245);
            colorRamp.toColor = Color.FromArgb(0, 245, 245);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(182, 237, 240);
            colorRamp.toColor = Color.FromArgb(9, 9, 145);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(175, 240, 233);
            colorRamp.toColor = Color.FromArgb(255, 252, 255);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(118, 219, 211);
            colorRamp.toColor = Color.FromArgb(255, 252, 255);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(200, 219, 219);
            colorRamp.toColor = Color.FromArgb(69, 69, 69);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(204, 255, 204);
            colorRamp.toColor = Color.FromArgb(14, 204, 14);
            myColorRamps.Add(colorRamp);

            colorRamp = new ColorRamp();
            colorRamp.fromColor = Color.FromArgb(120, 245, 233);
            colorRamp.toColor = Color.FromArgb(34, 102, 51);
            myColorRamps.Add(colorRamp);
        }





        #endregion

        
    }
}
