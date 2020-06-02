using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MalaSpiritGIS
{
    public class MLDataFrame  //数据框，负责记录所有图层的信息，所以还是独立成一个类比较好
    {
        public class Dataframe
        {
            public List<Layer> layers;  //数据框包括多个图层
            public int index;  //正在操作的图层
            public Dataframe()
            {
                layers = new List<Layer>();
                index = -1;
            }
            public bool selected()  //判断是否有操作图层，如果index为-1则没有
            {
                return index > -1;
            }
            public void cancelSelected()  //变成无操作图层状态
            {
                if (selected())
                {
                    layers[index].name.BorderStyle = BorderStyle.None;  //取消上一个操作图层的框
                    index = -1;  //设置index
                }
            }
            public void setSelected(int _index)  //变成有操作图层状态
            {
                cancelSelected();  //先取消上一个操作状态
                index = _index;
                layers[index].name.BorderStyle = BorderStyle.FixedSingle;
            }
            public void moveUp()  //将操作中的图层上移
            {
                if (selected())
                {
                    if (index != 0)  //如果不在顶部
                    {
                        layers[index].moveUp();  //上下移
                        layers[index - 1].moveDown();
                        Layer layer = layers[index];
                        layers[index] = layers[index - 1];
                        layers[--index] = layer;
                    }
                }
            }
            public void moveDown()  //将操作中的图层下移
            {
                if (selected())
                {
                    if (index != layers.Count - 1)
                    {
                        layers[index].moveDown();  //上下移
                        layers[index + 1].moveUp();
                        Layer layer = layers[index];
                        layers[index] = layers[index + 1];
                        layers[++index] = layer;
                    }
                }
            }
            public void moveTop()  //将操作中的图层移至顶部
            {
                if (selected())
                {
                    while (index != 0)  //如果不在顶部
                    {
                        layers[index].moveUp();  //上下移
                        layers[index - 1].moveDown();
                        Layer layer = layers[index];
                        layers[index] = layers[index - 1];
                        layers[--index] = layer;
                    }
                }
            }
            public void moveBottom()  //将操作中的图层移至底部
            {
                if (selected())
                {
                    while (index != layers.Count - 1)
                    {
                        layers[index].moveDown();  //上下移
                        layers[index + 1].moveUp();
                        Layer layer = layers[index];
                        layers[index] = layers[index + 1];
                        layers[++index] = layer;
                    }
                }
            }
            public void rename(string newName)  //修改图层名字
            {
                layers[index].name.Text = newName;
                //之后要素类的名字也要改
                layers[index].featureClass.EditName(newName);
            }
        }

        public class Layer  //图层
        {
            public Label sign, name;  //图层包括符号（·/—/■）和图层名字
            public MLFeatureClass featureClass;  //以及数据实体的要素类
            //public static int id = 0;  //唯一编号
            public int index;  //在数据框data中的索引


            public int renderMethod;//渲染方法，0:单一符号法,1:唯一值法，2:分级法
            public string PointSign;//点符号类型
            public float PointSize;//点符号大小
            public string LineStyle;//线条风格（实线或虚线）
            public Color CurColor;//图层符号的颜色
            public float LineWidth;//线符号和面符号的轮廓宽度
            public List<Color> RenderColors;//渲染用颜色
            public string _selectedValue;//渲染用值字段；
            public int annotateIndex=-1;
            public Color annotateColor = Color.Black;
            public string annotateFontStyle = "Adobe Gothic Std";
            public float annotateFontSize = 10;


            public Layer(FeatureType type, int _index,uint id=uint.MaxValue)
            {
                index = _index;
                sign = new Label();
                name = new Label();
                switch (type)
                {
                    case FeatureType.POINT:
                        sign.Text = "·";
                        name.Text = "新建点图层";
                        break;
                    case FeatureType.MULTIPOINT:
                        sign.Text = "·";
                        name.Text = "新建多点图层";
                        break;
                    case FeatureType.POLYLINE:
                        sign.Text = "—";
                        name.Text = "新建线图层";
                        break;
                    case FeatureType.POLYGON:
                        sign.Text = "■";
                        name.Text = "新建面图层";
                        break;
                }
                
                LineWidth = 1;//轮廓宽度为1
                LineStyle = "Solid";//实线
                PointSign = "FilledCircle";//初始化默认点符号类型为实心圆
                PointSize = 2;//点符号大小初始为2
                renderMethod = 0;//初始默认使用单一符号法渲染
                if (id == uint.MaxValue)
                    featureClass = new MLFeatureClass(MLMainForm.FeatureProcessor, name.Text, type);  //新建一个要素类
                else
                {
                    featureClass = MLMainForm.FeatureProcessor.LoadFeatureClass(id);
                    name.Text = featureClass.Name;
                }
                    
                sign.Width = 12;  //长宽固定
                name.Width = 78;

                int y = 25 * index + 30;  //新图层的显示位置可以推算
                sign.Location = new Point(0, y);
                name.Location = new Point(12, y);
            }

            public Layer(MLFeatureClass fc, int _index)
            {
                index = _index;
                sign = new Label();
                name = new Label();
                switch (fc.featureType)
                {
                    case FeatureType.POINT:
                        sign.Text = "·";
                        break;
                    case FeatureType.MULTIPOINT:
                        sign.Text = "·";
                        break;
                    case FeatureType.POLYLINE:
                        sign.Text = "—";
                        break;
                    case FeatureType.POLYGON:
                        sign.Text = "■";
                        break;
                }

                LineWidth = 1;//轮廓宽度为1
                LineStyle = "Solid";//实线
                PointSign = "FilledCircle";//初始化默认点符号类型为实心圆
                PointSize = 2;//点符号大小初始为2
                featureClass = fc;
                name.Text = fc.Name;

                sign.Width = 12;  //长宽固定
                name.Width = 78;

                int y = 25 * index + 30;  //新图层的显示位置可以推算
                sign.Location = new Point(0, y);
                name.Location = new Point(12, y);
            }
            public void moveUp()  //本图层上移
            {
                --index;  //对应在数据框data中的索引也要-1
                sign.Location = new Point(0, sign.Location.Y - 25);
                name.Location = new Point(12, name.Location.Y - 25);
            }
            public void moveDown()  //本图层下移
            {
                ++index;
                sign.Location = new Point(0, sign.Location.Y + 25);
                name.Location = new Point(12, name.Location.Y + 25);
            }
        }
    }
}
