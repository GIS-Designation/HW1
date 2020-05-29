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
    public partial class AttributeTable : Form
    {
        MLFeatureClass curFeaClass;
        bool onEditing;
        public AttributeTable()
        {
            InitializeComponent();
            onEditing = false;
        }

        public void BindData(MLFeatureClass _curFeaClass)
        {
            curFeaClass = _curFeaClass;
            attributeView.DataSource = curFeaClass.AttributeData;
        }

        private void 增加字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFieldForm addFieldForm = new AddFieldForm();
            if (DialogResult.OK == addFieldForm.ShowDialog(this))
            {
                curFeaClass.AddAttributeField(addFieldForm.FieldName, addFieldForm.FieldType);
                attributeView.Refresh();
            }
            addFieldForm.Dispose();
        }

        #region 事件

        //通过属性表选择的要素发生改变时
        public delegate void SelectingFeatureChangedHandle(object sender, int[] indexes);
        public event SelectingFeatureChangedHandle SelectingFeatureChanged;


        #endregion
    }
}
