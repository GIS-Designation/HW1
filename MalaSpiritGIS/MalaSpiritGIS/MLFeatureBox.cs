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
        private static int constructCount=0;//第一次调用构造函数是FeatureBox，第二次是RecordBox
        public MLFeatureBox()  //接收从mainForm传递来的dataFrame
        {
            data = MLMainForm.dataFrame;
            attributeTable = new AttributeTable();
            InitializeComponent();
            if (constructCount == 0)
            {
                this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.showBoxMenu);
                ++constructCount;
            }

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
            MLMainForm.mlmap.ClearSelectedFeatures();
        }
        public void addLayer(FeatureType type,uint id=uint.MaxValue,string filePath=null)
        {
            Layer layer;
            if (filePath == null)
            {
                layer = new Layer(type, data.layers.Count, id);
            }
            else
            {
                layer = new Layer(MLMainForm.FeatureProcessor.LoadFeatureClassFromShapefile(filePath), data.layers.Count);
            }
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
                    MLMainForm.mlmap.Refresh();
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
            MLMainForm.mlmap.ClearSelectedFeatures();
        }

        private void down_Click(object sender, EventArgs e)
        {
            data.moveDown();
            MLMainForm.mlmap.ClearSelectedFeatures();
        }

        private void top_Click(object sender, EventArgs e)
        {
            data.moveTop();
            MLMainForm.mlmap.ClearSelectedFeatures();
        }

        private void bottom_Click(object sender, EventArgs e)
        {
            data.moveBottom();
            MLMainForm.mlmap.ClearSelectedFeatures();
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
            MLMainForm.mlmap.ClearSelectedFeatures();
        }

        private void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (attributeTable.IsDisposed)
            {
                attributeTable = new AttributeTable();
            }
            attributeTable.BindData(data.layers[data.index].featureClass);
            attributeTable.Show();
        }

        private void 加载图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < data.layers.Count; ++i)
            {
                if(data.layers[i].featureClass.ID== MLMainForm.FeatureProcessor.Records[curRecordIndex].ID)
                {
                    MessageBox.Show("图层已加载", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            MLMainForm.featureBox.addLayer(MLMainForm.FeatureProcessor.Records[curRecordIndex].Type, MLMainForm.FeatureProcessor.Records[curRecordIndex].ID);
            MLMainForm.mlmap.Refresh();
        }

        int curRecordIndex;
        public void RefreshRecords()
        {
            for(int i = 0; i < MLMainForm.FeatureProcessor.Records.Count; ++i)
            {
                int y = 25 * i + 30;  //新图层的显示位置可以推算
                if (!Controls.Contains(MLMainForm.FeatureProcessor.Records[i].SignLabel))
                {
                    MLMainForm.FeatureProcessor.Records[i].NameLabel.MouseClick += new MouseEventHandler(showRecordMenu);
                    Controls.Add(MLMainForm.FeatureProcessor.Records[i].SignLabel);
                    Controls.Add(MLMainForm.FeatureProcessor.Records[i].NameLabel);
                }
                MLMainForm.FeatureProcessor.Records[i].SignLabel.SetBounds(0, y, 12, 23);
                MLMainForm.FeatureProcessor.Records[i].NameLabel.SetBounds(15, y, 78, 23);
                void showRecordMenu(object sender2, MouseEventArgs e)  //点击文字触发的操作
                {
                    if (e.Button == MouseButtons.Right)  //如果是右键，就打开菜单
                    {
                        recordMenu.Show(MousePosition.X, MousePosition.Y);
                    }
                    curRecordIndex = (int)Math.Floor((decimal)(((Label)sender2).Location.Y - 30) / 25);
                }
            }
        }

        private void 保存图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MLMainForm.FeatureProcessor.SaveFeatureClass(data.layers[data.index].featureClass);
        }

        private void 渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            MapRender sMapRender = new MapRender();
            sMapRender.CurLayer = data.layers[data.index];
            sMapRender.color = data.layers[data.index].CurColor;
            sMapRender.renderMethod = data.layers[data.index].renderMethod;
            sMapRender.selectedValue = data.layers[data.index]._selectedValue;
            //sMapRender.colors = data.layers[data.index].RenderColors;
            if (sMapRender.ShowDialog(this) == DialogResult.OK)
            {
                data.layers[data.index] = sMapRender.CurLayer;
                data.layers[data.index].CurColor = sMapRender.color;
                data.layers[data.index].renderMethod = sMapRender.renderMethod;
                data.layers[data.index]._selectedValue = sMapRender.selectedValue;
                data.layers[data.index].RenderColors = sMapRender.colors;
                MLMainForm.mlmap.Refresh();
            }
        }

        private void 标注要素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Annotation anno = new Annotation();
            if (DialogResult.OK == anno.ShowDialog(this))
            {
                data.layers[data.index].annotateIndex = anno.annoIndex;
                data.layers[data.index].annotateColor = anno.annoColor;
                data.layers[data.index].annotateFontStyle = anno.annoString;
                data.layers[data.index].annotateFontSize = anno.annoSize;
                MLMainForm.mlmap.Refresh();
            }
        }
    }
}
