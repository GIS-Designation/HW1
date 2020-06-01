using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MalaSpiritGIS
{
    public partial class MoveByCoor : Form
    {
        public MoveByCoor()
        {
            InitializeComponent();
        }
        [Browsable(true), Description("确认移动偏差值")]
        public delegate void MoveByDeltaCoorHandle(object sender, float dx,float dy);
        public event MoveByDeltaCoorHandle MoveByDeltaCoor;

        private void DSure_Click(object sender, EventArgs e)
        {
            double dx, dy;
            if(double.TryParse(DX.Text,out dx) &&double.TryParse(DY.Text,out dy))
            {
                MoveByDeltaCoor?.Invoke(this, (float)dx, (float)dy);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }
    }
}
