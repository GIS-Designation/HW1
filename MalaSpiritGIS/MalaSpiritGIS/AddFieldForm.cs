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
    public partial class AddFieldForm : Form
    {
        string fieldName;
        Type fieldType;
        Type[] typeList = { typeof(int), typeof(double), typeof(string) };
        public AddFieldForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxFieldName.Text.Equals(""))
            {
                MessageBox.Show("字段名为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (listBoxFieldType.SelectedIndex == -1)
            {
                MessageBox.Show("未选择字段类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            fieldName = textBoxFieldName.Text;
            fieldType = typeList[listBoxFieldType.SelectedIndex];
        }

        public string FieldName { get { return fieldName; } }
        public Type FieldType { get { return fieldType; } }
    }
}
