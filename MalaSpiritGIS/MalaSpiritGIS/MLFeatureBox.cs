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

            addFeatureClass();// 测试
        }

        #region 属性
        List<MLFeatureClass> layers;
        #endregion
        /// <summary>
        /// 添加一个图层，仅供展示，功能还未具体制作
        /// </summary>
        public void addFeatureClass()//MLFeatureClass layer)
        {
            Label label = new Label();
            label.Location = new Point(3, 30);
            label.Text = "testtt";
            Controls.Add(label);
            label.MouseClick += new System.Windows.Forms.MouseEventHandler(showLayerMenu);
        }

        private void showBoxMenu(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                boxMenu.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void showLayerMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                layerMenu.Show(MousePosition.X, MousePosition.Y);
            }
        }
    }
}
