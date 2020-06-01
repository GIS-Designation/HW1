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
    public partial class Query : Form
    {
        public Query()
        {
            InitializeComponent();
        }

        private int _index = -1;

        private void Query_Load(object sender, EventArgs e) //加载时就图层下拉栏中加入所有图层名称
        {
            int i = 0;
            while(i != MLMainForm.dataFrame.layers.Count)
            {
                LayerList.Items.Add(MLMainForm.dataFrame.layers[i].name.Text);
                i++;
            }
            if (MLMainForm.dataFrame.index >= 0)
            {
                FieldList.Items.Clear();
                _index = MLMainForm.dataFrame.index;
                LayerList.SelectedIndex = _index;
                label4.Text = "Select * From " + LayerList.SelectedItem.ToString() + " Where";
            }
        }
        private void LayerList_SelectedIndexChanged(object sender, EventArgs e) //字段栏添加
        {
            FieldList.Items.Clear();
            _index = LayerList.SelectedIndex;
            MLMainForm.dataFrame.index = _index;
            label4.Text = "Select * From " + LayerList.SelectedItem.ToString() + " Where";
            for (int j = 0; j < MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Columns.Count; j++)
                FieldList.Items.Add(MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Columns[j].ColumnName);
        }

        private void FieldList_SelectedIndexChanged(object sender, EventArgs e) //唯一值栏添加
        {
            ValueList.Items.Clear();
            if(MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Columns[FieldList.SelectedIndex].DataType == typeof(string))
            {
                for (int i = 0; i < MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Rows.Count; i++)
                {
                    bool _flag = true;
                    for(int j = 0; j < ValueList.Items.Count; j ++)
                    {
                        if (("'" + MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Rows[i][FieldList.SelectedIndex] + "'").ToString() == ValueList.Items[j].ToString())
                            _flag = false;
                    }
                    if(_flag)
                        ValueList.Items.Add("'"+MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Rows[i][FieldList.SelectedIndex]+"'");
                }
                    
            }
            else
            {
                for (int i = 0; i < MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Rows.Count; i++)
                {
                    bool _flag = true;
                    for (int j = 0; j < ValueList.Items.Count; j++)
                    {
                        if (MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Rows[i][FieldList.SelectedIndex].ToString() == ValueList.Items[j].ToString())
                            _flag = false;
                    }
                    if (_flag)
                        ValueList.Items.Add(MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Rows[i][FieldList.SelectedIndex]);
                }
            }
        }


        #region 界面上输入查询语句
        private void FieldList_DoubleClick(object sender, EventArgs e)
        {
            SQLTextBox.Text += FieldList.SelectedItem.ToString();
        }

        private void ValueList_DoubleClick(object sender, EventArgs e)
        {
            SQLTextBox.Text += ValueList.SelectedItem.ToString();
        }

        private void Bt_equal_Click(object sender, EventArgs e)
        {
            SQLTextBox.Text += " = ";
        }

        private void Bt_gt_Click(object sender, EventArgs e)
        {
            SQLTextBox.Text += " > ";
        }

        private void Bt_lt_Click(object sender, EventArgs e)
        {
            SQLTextBox.Text += " < ";
        }

        private void Bt_unequal_Click(object sender, EventArgs e)
        {
            SQLTextBox.Text += " <> ";
        }

        private void Bt_ge_Click(object sender, EventArgs e)
        {
            SQLTextBox.Text += " >= ";
        }

        private void Bt_le_Click(object sender, EventArgs e)
        {
            SQLTextBox.Text += " <= ";
        }
        #endregion

        #region 事件

        //通过属性表选择的要素发生改变时
        public delegate void SearchingFinishedHandle(object sender, int[] indexes);
        public event SearchingFinishedHandle SearchingFinished;


        #endregion
        private void Bt_OK_Click(object sender, EventArgs e)
        {
            DataRow[] dr = MLMainForm.dataFrame.layers[_index].featureClass.AttributeData.Select(SQLTextBox.Text.ToString());//筛选结果
            if (dr.Length > 0) //存在符合条件的要素
            {
                int[] selectingFeatureIndexes = new int[dr.Length]; //需要高亮的要素序号数组
                int _temp = 0;
                foreach (DataRow drN in dr)
                {
                    selectingFeatureIndexes[_temp] = (int)(uint)drN[0];
                    _temp++;
                }
                SearchingFinished?.Invoke(this, selectingFeatureIndexes);
            }
            else
                MessageBox.Show("未找到符合条件的要素");
            this.Close();
        }

        private void Bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
