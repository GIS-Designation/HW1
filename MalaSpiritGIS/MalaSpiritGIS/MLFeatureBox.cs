using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MalaSpiritGIS.MLDataFrame;

namespace MalaSpiritGIS
{
    public partial class MLFeatureBox : UserControl
    {
        public AttributeTable attributeTable;
        public MLFeatureBox(Dataframe df)  //接收从mainForm传递来的dataFrame
        {
            data = df;
            attributeTable = new AttributeTable();
            InitializeComponent();
        }
        public Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.Blue, Color.DarkBlue, Color.Violet, Color.Pink };  //线条、填充色
        public Dataframe data;  //数据框，由于软件只支持一个数据框，因此全局变量只要有一个Dataframe就够了
        private void showBoxMenu(object sender, MouseEventArgs e)  //右间空白区域打开菜单
        {
            if (e.Button == MouseButtons.Right)
            {
                boxMenu.Show(MousePosition.X, MousePosition.Y);
            }
            data.cancelSelected();  //会变成无图层选中状态
        }
        private void addLayer(FeatureType type)
        {
            Layer layer = new Layer(type, data.layers.Count);
            //绑定事件
            layer.sign.MouseClick += new MouseEventHandler(editSign);  //点击符号的操作
            layer.name.MouseClick += new MouseEventHandler(showLayerMenu);  //点击文字的操作
            layer.CurColor = colors[data.layers.Count];
            void editSign(object sender, MouseEventArgs e)  //点击符号触发的操作，修改符号
            {
                EditSign sEditSign = new EditSign();
                sEditSign.CurLayer = layer;
                sEditSign.color = layer.CurColor;
                sEditSign.size = layer.LineWidth;
                if(sEditSign.ShowDialog(this) == DialogResult.OK)
                {
                    layer = sEditSign.CurLayer;
                    layer.CurColor = sEditSign.color;
                    layer.LineWidth = sEditSign.size;
                }
            }

            void showLayerMenu(object sender, MouseEventArgs e)  //点击文字触发的操作
            {
                if (e.Button == MouseButtons.Right)  //如果是右键，就打开菜单
                {
                    layerMenu.Show(MousePosition.X, MousePosition.Y);
                }
                data.setSelected(layer.index);  //不管左键右键都会使图层被选中
            }
            data.layers.Add(layer);
            Controls.Add(layer.name);
            Controls.Add(layer.sign);
        }
        private void 新建点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayer(FeatureType.POINT);
        }

        private void 新建多点图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayer(FeatureType.MULTIPOINT);
        }

        private void 新建线图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayer(FeatureType.POLYLINE);
        }

        private void 新建面图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addLayer(FeatureType.POLYGON);
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
            Controls.Remove(data.layers[data.index].name);
            Controls.Remove(data.layers[data.index].sign);
            data.layers.RemoveAt(data.index);
            while (data.index != data.layers.Count)
            {
                data.layers[data.index].moveUp();
                ++data.index;
            }
            data.index = -1;
        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            attributeTable.BindData(data.layers[data.index].featureClass);
            attributeTable.Show();
        }
    }
}
