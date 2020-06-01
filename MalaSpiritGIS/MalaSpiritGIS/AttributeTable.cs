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
        public int[] selectingFeatureIndexes;
        bool onEditing;
        DataTable a;
        
        public AttributeTable()
        {
            InitializeComponent();
            onEditing = false;
            停止编辑ToolStripMenuItem.Enabled = false;
            selectingFeatureIndexes = null;
            
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

        private void 开始编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onEditing = true;
            停止编辑ToolStripMenuItem.Enabled = true;
            开始编辑ToolStripMenuItem.Enabled = false;
        }

        private void 停止编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onEditing = false;
            停止编辑ToolStripMenuItem.Enabled = false;
            开始编辑ToolStripMenuItem.Enabled = true;
            attributeView.EndEdit();
        }

        private void attributeView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (onEditing)
            {
                if (e.ColumnIndex > 1)
                {
                    attributeView.CurrentCell = attributeView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    attributeView.BeginEdit(true);
                }
            }
        }

        private void 全部选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectingFeatureIndexes = new int[attributeView.Rows.Count];
            for (int i = 0; i < attributeView.Rows.Count; ++i)
            {
                attributeView.Rows[i].Selected = true;
                selectingFeatureIndexes[i] = i;
            }
            SelectingFeatureChanged?.Invoke(this, selectingFeatureIndexes);

        }

        private void 取消选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < attributeView.Rows.Count; ++i)
            {
                attributeView.Rows[i].Selected = false;
            }
            SelectingFeatureChanged?.Invoke(this, null);
        }

        private void attributeView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!onEditing)
            {
                if (e.ColumnIndex >= 0)
                {
                    for (int i = 0; i < attributeView.Rows.Count; ++i)
                    {
                        if (attributeView.Rows[i].HeaderCell.Selected || attributeView.Rows[i].Cells[e.ColumnIndex].Selected)
                        {
                            attributeView.Rows[i].Selected = true;
                        }
                    }
                }
                else
                {
                    if (e.RowIndex >= 0)
                        attributeView.Rows[e.RowIndex].Selected = true;
                }
                selectingFeatureIndexes = new int[attributeView.SelectedRows.Count];
                for (int i = 0; i < attributeView.SelectedRows.Count; ++i)
                {
                    selectingFeatureIndexes[i] = (int)(uint)attributeView.SelectedRows[i].Cells[0].Value;
                }
                SelectingFeatureChanged?.Invoke(this, selectingFeatureIndexes);
            }
        }

        private void attributeView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int temp_int;
            double temp_double;
            if (attributeView.Columns[e.ColumnIndex].ValueType == typeof(int)&&!int.TryParse(e.FormattedValue.ToString(),out temp_int))
            {
                e.Cancel = true;
                MessageBox.Show("输入属性值不符合字段类型","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (attributeView.Columns[e.ColumnIndex].ValueType == typeof(double) && !double.TryParse(e.FormattedValue.ToString(), out temp_double))
            {
                e.Cancel = true;
                MessageBox.Show("输入属性值不符合字段类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
