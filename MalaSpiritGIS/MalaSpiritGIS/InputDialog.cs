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
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        public delegate void TextEventHandler(string text);
        public TextEventHandler TextHandler;

        private void sure_Click(object sender, EventArgs e)
        {
            if (null != TextHandler)
            {
                TextHandler.Invoke(inputBox.Text);
                DialogResult = DialogResult.OK;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Keys.Enter == (Keys)e.KeyChar)
            {
                if (null != TextHandler)
                {
                    TextHandler.Invoke(inputBox.Text);
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
