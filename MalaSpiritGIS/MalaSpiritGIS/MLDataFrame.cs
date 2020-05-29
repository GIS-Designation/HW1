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
            }
        }

        public class Layer  //图层
        {
            public Label sign, name;  //图层包括符号（·/—/■）和图层名字
            public MLFeatureClass featureClass;  //以及数据实体的要素类
            //public static int id = 0;  //唯一编号
            public int index;  //在数据框data中的索引


            public string PointSign;//点符号类型
            public float PointSize;//点符号大小
            public string LineStyle;//线条风格（实线或虚线）
            public Color CurColor;//图层符号的颜色
            public float LineWidth;//线符号和面符号的轮廓宽度


            public Layer(FeatureType type, int _index)
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
                featureClass = new MLFeatureClass((uint)id, name.Text, type, new double[4]);  //新建一个要素类
                featureClass.AddAttributeField("ID", typeof(int));
                featureClass.AddAttributeField("i1", typeof(int));
                featureClass.AddAttributeField("i2", typeof(int));
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
