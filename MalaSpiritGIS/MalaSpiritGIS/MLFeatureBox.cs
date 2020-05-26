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
            data = new Dataframe();
        }

        #region 属性

        Dataframe data;

        #endregion

        class Dataframe
        {
            public List<MLFeatureClass> layers;
            public List<Label> signs, names;
            public int index;
            public Dataframe()
            {
                signs = new List<Label>();
                names = new List<Label>();
                layers = new List<MLFeatureClass>();
                index = 0;
            }
            public bool selected()
            {
                return index != layers.Count;
            }
            public void add(ControlCollection controls, MLFeatureClass layer, Label name, Label sign)
            {
                if (!selected())
                {
                    index += 1;
                }
                layers.Add(layer);
                names.Add(name);
                signs.Add(sign);
                controls.Add(name);
                controls.Add(sign);
                int y = 25 * layers.Count + 5;  //新建记录待显示的位置
                sign.Location = new Point(0, y);
                name.Location = new Point(12, y);
                sign.Width = 12;
                name.Width = 78;
            }
            public void cancelSelected()
            {
                if (selected())
                {
                    names[index].BorderStyle = BorderStyle.None;
                    index = layers.Count;
                }
            }
            public void setSelected(int newIndex)
            {
                cancelSelected();
                index = newIndex;
                names[index].BorderStyle = BorderStyle.FixedSingle;
            }
            private void move(int delta)
            {
                int pos = index + delta;
                MLFeatureClass layer = layers[pos];
                layers[pos] = layers[index];
                layers[index] = layer;

                Label name = names[pos];
                names[pos] = names[index];
                names[index] = name;

                Label sign = signs[pos];
                signs[pos] = signs[index];
                signs[index] = sign;

                index = pos;
            }
            public void moveUp()
            {
                if (selected() && index != 0)
                {
                    move(-1);
                    refresh();
                }
            }
            public void moveDown()
            {
                if (selected() && index != layers.Count - 1)
                {
                    move(1);
                    refresh();
                }
            }
            public void moveTop()
            {
                if (selected())
                {
                    while (index != 0)
                    {
                        move(-1);
                    }
                    refresh();
                }
            }
            public void moveBottom()
            {
                if (selected())
                {
                    while (index != layers.Count - 1)
                    {
                        move(1);
                    }
                    refresh();
                }
            }
            public void refresh()
            {
                for (int i = 0; i != layers.Count; ++i)
                {
                    int y = 25 * i + 30;
                    signs[i].Location = new Point(0, y);
                    names[i].Location = new Point(12, y);
                }
            }
            public void rename(string newName)
            {
                names[index].Text = newName;
                //之后图层的名字也要改
            }
            public void delete(ControlCollection controls)
            {
                layers.RemoveAt(index);
                controls.Remove(signs[index]);
                controls.Remove(names[index]);
                signs.RemoveAt(index);
                names.RemoveAt(index);
                index = layers.Count;
                refresh();
            }
        }
        public void addFeatureClass(FeatureType type)  //添加一个图层，因为MLFeatureClass还未完善，所以暂不支持从现有要素类创建
        {
            Label name = new Label();  //新建记录的文字信息（图层的名字）
            Label sign = new Label();  //新建记录的符号信息（图层对应的要素类型）
            switch (type)  //按类型初始化新建记录
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
            int index = data.layers.Count;  //新建记录的索引，也是新建图层的索引
            MLFeatureClass layer = new MLFeatureClass((uint)index, name.Text, type, new double[4]);  //新建一个图层
            data.add(Controls, layer, name, sign);

            sign.MouseClick += new MouseEventHandler(editSign);  //点击符号的操作
            name.MouseClick += new MouseEventHandler(showLayerMenu);  //点击文字的操作

            void editSign(object sender, MouseEventArgs e)  //点击符号触发的操作，修改符号
            {
                MessageBox.Show("这里将来会跳出符号修改窗口");
            }

            void showLayerMenu(object sender, MouseEventArgs e)  //点击文字触发的操作
            {
                if (e.Button == MouseButtons.Right)  //如果是右键，就打开菜单
                {
                    layerMenu.Show(MousePosition.X, MousePosition.Y);
                }
                data.setSelected(index);
            }
        }

        private void showBoxMenu(object sender, MouseEventArgs e)  //右间空白区域打开菜单
        {
            if (e.Button == MouseButtons.Right)
            {
                boxMenu.Show(MousePosition.X, MousePosition.Y);
            }
            data.cancelSelected();
        }

        private void 新建点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFeatureClass(FeatureType.POINT);
        }

        private void 新建多点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFeatureClass(FeatureType.MULTIPOINT);
        }

        private void 新建线图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFeatureClass(FeatureType.POLYLINE);
        }

        private void 新建面图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addFeatureClass(FeatureType.POLYGON);
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
            data.delete(Controls);
        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttributeTable table = new AttributeTable();
            table.ShowDialog();
        }
    }
}
