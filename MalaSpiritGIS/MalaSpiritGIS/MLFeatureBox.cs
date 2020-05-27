using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MalaSpiritGIS
{
    public partial class MLFeatureBox : UserControl
    {
        public MLFeatureBox()
        {
            InitializeComponent();
            data = new Dataframe(this);
        }
        public Dataframe data;  //数据框，由于软件只支持一个数据框，因此全局变量只要有一个Dataframe就够了
        public class Layer  //图层
        {
            public Label sign, name;  //图层包括符号（·/—/■）和图层名字
            public MLFeatureClass featureClass;  //以及数据实体的要素类
            public static int id = 0;  //唯一编号
            public int index;  //在数据框data中的索引
            public Layer(FeatureType type, int _index, ControlCollection controls)
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
                sign.Width = 12;  //长宽固定
                name.Width = 78;

                int y = 25 * index + 30;  //新图层的显示位置可以推算
                sign.Location = new Point(0, y);
                name.Location = new Point(12, y);
                ++id;

                controls.Add(name);  //添加到控件中
                controls.Add(sign);
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
            public void remove(ControlCollection controls)  //从controls中删除本图层
            {
                controls.Remove(name);
                controls.Remove(sign);
            }
        }
        public class Dataframe
        {
            public List<Layer> layers;  //数据框包括多个图层
            public MLFeatureBox self;  //指向界面，用于绑定一些事件属性
            public int index;  //正在操作的图层
            public Dataframe(MLFeatureBox that)
            {
                layers = new List<Layer>();
                self = that;
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
            public void add(FeatureType type)  //新加一条数据
            {
                Layer layer = new Layer(type, layers.Count,self.Controls) ;
                //绑定事件
                layer.sign.MouseClick += new MouseEventHandler(editSign);  //点击符号的操作
                layer.name.MouseClick += new MouseEventHandler(showLayerMenu);  //点击文字的操作
                void editSign(object sender, MouseEventArgs e)  //点击符号触发的操作，修改符号
                {
                    MessageBox.Show("这里将来会跳出符号修改窗口");
                }

                void showLayerMenu(object sender, MouseEventArgs e)  //点击文字触发的操作
                {
                    if (e.Button == MouseButtons.Right)  //如果是右键，就打开菜单
                    {
                        self.layerMenu.Show(MousePosition.X, MousePosition.Y);
                    }
                    setSelected(layer.index);  //不管左键右键都会使图层被选中
                }
                layers.Add(layer);
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
            public void delete()  //删除图层
            {
                layers[index].remove(self.Controls);
                layers.RemoveAt(index);
                while(index != layers.Count)
                {
                    layers[index].moveUp();
                    ++index;
                }
                index = -1;
            }
        }

        private void showBoxMenu(object sender, MouseEventArgs e)  //右间空白区域打开菜单
        {
            if (e.Button == MouseButtons.Right)
            {
                boxMenu.Show(MousePosition.X, MousePosition.Y);
            }
            data.cancelSelected();  //会变成无图层选中状态
        }

        private void 新建点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.add(FeatureType.POINT);
        }

        private void 新建多点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.add(FeatureType.MULTIPOINT);
        }

        private void 新建线图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.add(FeatureType.POLYLINE);
        }

        private void 新建面图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.add(FeatureType.POLYGON);
        }

        private void up_Click(object sender, EventArgs e)
        {
            data.moveUp();
        }

        private void down_Click(object sender, EventArgs e)
        {
            data.moveDown();
        }

        private void top_Click(object sender, EventArgs e)
        {
            data.moveTop();
        }

        private void bottom_Click(object sender, EventArgs e)
        {
            data.moveBottom();
        }

        private void 修改图层名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "";
            InputDialog input = new InputDialog();
            input.TextHandler = (str) => { name = str; };
            if (input.ShowDialog() == DialogResult.OK)
            {
                data.rename(name);
            }
        }

        private void 删除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data.delete();
        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttributeTable table = new AttributeTable();
            table.ShowDialog();
        }
    }
}
