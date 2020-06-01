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
            MoveByDeltaCoor?.Invoke(this, (float)Convert.ToDouble(DX.Text), (float)Convert.ToDouble(DY.Text));
            this.DialogResult = DialogResult.OK;
        }
        private void DX_KeyPress(object sender, KeyPressEventArgs e)
        {
            Key_Press(e, DX);
        }
        private void DY_KeyPress(object sender, KeyPressEventArgs e)
        {
            Key_Press(e, DY);
        }
        private void Key_Press(KeyPressEventArgs e,TextBox textBox)
        {
            //数字0~9所对应的keychar为48~57，小数点是46，Backspace是8
            e.Handled = true;
            //输入0-9和Backspace del 有效
            if ((e.KeyChar >= 47 && e.KeyChar <= 58) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            if (e.KeyChar == 46)                       //小数点      
            {
                if (textBox.Text.Length <= 0)
                    e.Handled = true;           //小数点不能在第一位      
                else
                {
                    float f;
                    if (float.TryParse(textBox.Text + e.KeyChar.ToString(), out f))
                    {
                        e.Handled = false;
                    }
                }
            }
        }
    }
}
