using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MalaSpiritGIS.MLDataFrame;

namespace MalaSpiritGIS
{
    public partial class Annotation : Form
    {
        public int annoIndex;
        public Color annoColor;
        public string annoString;
        public float annoSize;
        public Layer CurLayer;
        public Annotation()
        {
            InitializeComponent();
        }

        private void Annotation_Load(object sender, EventArgs e)
        {
            CurLayer = MLMainForm.dataFrame.layers[MLMainForm.dataFrame.index];
            labelLayerName.Text = CurLayer.name.Text;
            UniqueValueList.Items.Clear();
            UniqueValueList.Items.Add("无");
            for (int i = 0; i < CurLayer.featureClass.AttributeData.Columns.Count; i++)
                UniqueValueList.Items.Add(CurLayer.featureClass.AttributeData.Columns[i].ColumnName);
            UniqueValueList.SelectedIndex = Math.Max(0, CurLayer.annotateIndex);

            lblColor.BackColor = CurLayer.annotateColor;
            labelFont.Text = CurLayer.annotateFontStyle;
            npdSize.Value = (decimal)CurLayer.annotateFontSize;
            annoColor = lblColor.BackColor;
            annoString = labelFont.Text;
            annoSize = (float)npdSize.Value;
        }

        private void UniqueValueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            annoIndex = UniqueValueList.SelectedIndex - 1;
        }

        private void btnTrackingColor_Click(object sender, EventArgs e)
        {
            ColorDialog sDialog = new ColorDialog();
            sDialog.Color = annoColor;
            if (sDialog.ShowDialog(this) == DialogResult.OK)
            {
                annoColor = sDialog.Color;
                lblColor.BackColor = annoColor;
            }
            sDialog.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FontDialog ff = new FontDialog();
            if (ff.ShowDialog() == DialogResult.OK)
            {
                annoString = ff.Font.Name;
                labelFont.Text = annoString;
                annoSize = ff.Font.Size;
                npdSize.Value = (decimal)annoSize;
            }
        }

        private void npdSize_ValueChanged(object sender, EventArgs e)
        {
            annoSize = (float)npdSize.Value;
        }
    }
}
