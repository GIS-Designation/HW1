namespace MalaSpiritGIS
{
    partial class MapRender
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgSimple = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSimpleColor = new System.Windows.Forms.Button();
            this.lblSimpleColor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tpgUniqueValue = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ColorBar1 = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UniqueValueList = new System.Windows.Forms.ComboBox();
            this.tpgRange = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.ColorBar2 = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rangeValueList = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpgSimple.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpgUniqueValue.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpgRange.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgSimple);
            this.tabControl1.Controls.Add(this.tpgUniqueValue);
            this.tabControl1.Controls.Add(this.tpgRange);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(639, 384);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgSimple
            // 
            this.tpgSimple.Controls.Add(this.groupBox2);
            this.tpgSimple.Controls.Add(this.groupBox1);
            this.tpgSimple.Location = new System.Drawing.Point(4, 22);
            this.tpgSimple.Name = "tpgSimple";
            this.tpgSimple.Padding = new System.Windows.Forms.Padding(3);
            this.tpgSimple.Size = new System.Drawing.Size(631, 358);
            this.tpgSimple.TabIndex = 0;
            this.tpgSimple.Text = "单一符号法";
            this.tpgSimple.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSimpleColor);
            this.groupBox2.Controls.Add(this.lblSimpleColor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(58, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "属性设置";
            // 
            // btnSimpleColor
            // 
            this.btnSimpleColor.Location = new System.Drawing.Point(312, 39);
            this.btnSimpleColor.Name = "btnSimpleColor";
            this.btnSimpleColor.Size = new System.Drawing.Size(75, 23);
            this.btnSimpleColor.TabIndex = 2;
            this.btnSimpleColor.Text = "选择";
            this.btnSimpleColor.UseVisualStyleBackColor = true;
            this.btnSimpleColor.Click += new System.EventHandler(this.btnSimpleColor_Click);
            // 
            // lblSimpleColor
            // 
            this.lblSimpleColor.BackColor = System.Drawing.Color.White;
            this.lblSimpleColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSimpleColor.Location = new System.Drawing.Point(188, 39);
            this.lblSimpleColor.Name = "lblSimpleColor";
            this.lblSimpleColor.Size = new System.Drawing.Size(100, 23);
            this.lblSimpleColor.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色：";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(58, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前符号";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // tpgUniqueValue
            // 
            this.tpgUniqueValue.Controls.Add(this.label7);
            this.tpgUniqueValue.Controls.Add(this.label6);
            this.tpgUniqueValue.Controls.Add(this.label5);
            this.tpgUniqueValue.Controls.Add(this.listBox1);
            this.tpgUniqueValue.Controls.Add(this.groupBox4);
            this.tpgUniqueValue.Controls.Add(this.groupBox3);
            this.tpgUniqueValue.Location = new System.Drawing.Point(4, 22);
            this.tpgUniqueValue.Name = "tpgUniqueValue";
            this.tpgUniqueValue.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUniqueValue.Size = new System.Drawing.Size(631, 358);
            this.tpgUniqueValue.TabIndex = 1;
            this.tpgUniqueValue.Text = "唯一值法";
            this.tpgUniqueValue.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(250, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "计数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "符号";
            // 
            // listBox1
            // 
            this.listBox1.ColumnWidth = 100;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(30, 106);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(472, 220);
            this.listBox1.TabIndex = 2;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ColorBar1);
            this.groupBox4.Location = new System.Drawing.Point(291, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(218, 43);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "色带";
            // 
            // ColorBar1
            // 
            this.ColorBar1.FormattingEnabled = true;
            this.ColorBar1.Location = new System.Drawing.Point(6, 17);
            this.ColorBar1.Name = "ColorBar1";
            this.ColorBar1.Size = new System.Drawing.Size(205, 20);
            this.ColorBar1.TabIndex = 0;
            this.ColorBar1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColorBar1_DrawItem);
            this.ColorBar1.SelectedIndexChanged += new System.EventHandler(this.ColorBar1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UniqueValueList);
            this.groupBox3.Location = new System.Drawing.Point(30, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(218, 43);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "值字段";
            // 
            // UniqueValueList
            // 
            this.UniqueValueList.FormattingEnabled = true;
            this.UniqueValueList.Location = new System.Drawing.Point(6, 17);
            this.UniqueValueList.Name = "UniqueValueList";
            this.UniqueValueList.Size = new System.Drawing.Size(205, 20);
            this.UniqueValueList.TabIndex = 0;
            this.UniqueValueList.SelectedIndexChanged += new System.EventHandler(this.UniqueValueList_SelectedIndexChanged);
            // 
            // tpgRange
            // 
            this.tpgRange.Controls.Add(this.label4);
            this.tpgRange.Controls.Add(this.label3);
            this.tpgRange.Controls.Add(this.label2);
            this.tpgRange.Controls.Add(this.listBox2);
            this.tpgRange.Controls.Add(this.groupBox5);
            this.tpgRange.Controls.Add(this.groupBox6);
            this.tpgRange.Location = new System.Drawing.Point(4, 22);
            this.tpgRange.Name = "tpgRange";
            this.tpgRange.Size = new System.Drawing.Size(631, 358);
            this.tpgRange.TabIndex = 2;
            this.tpgRange.Text = "分级法";
            this.tpgRange.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "标注";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "范围";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "符号";
            // 
            // listBox2
            // 
            this.listBox2.ColumnWidth = 100;
            this.listBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(34, 112);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(472, 220);
            this.listBox2.TabIndex = 5;
            this.listBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox2_DrawItem);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ColorBar2);
            this.groupBox5.Location = new System.Drawing.Point(295, 25);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(218, 43);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "色带";
            // 
            // ColorBar2
            // 
            this.ColorBar2.FormattingEnabled = true;
            this.ColorBar2.Location = new System.Drawing.Point(6, 17);
            this.ColorBar2.Name = "ColorBar2";
            this.ColorBar2.Size = new System.Drawing.Size(205, 20);
            this.ColorBar2.TabIndex = 0;
            this.ColorBar2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColorBar2_DrawItem);
            this.ColorBar2.SelectedIndexChanged += new System.EventHandler(this.ColorBar2_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rangeValueList);
            this.groupBox6.Location = new System.Drawing.Point(34, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(218, 43);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "值字段";
            // 
            // rangeValueList
            // 
            this.rangeValueList.FormattingEnabled = true;
            this.rangeValueList.Location = new System.Drawing.Point(6, 17);
            this.rangeValueList.Name = "rangeValueList";
            this.rangeValueList.Size = new System.Drawing.Size(205, 20);
            this.rangeValueList.TabIndex = 0;
            this.rangeValueList.SelectedIndexChanged += new System.EventHandler(this.rangeValueList_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(469, 410);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(572, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MapRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 445);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "MapRender";
            this.Text = "图层渲染";
            this.Load += new System.EventHandler(this.MapRender_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpgSimple.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpgUniqueValue.ResumeLayout(false);
            this.tpgUniqueValue.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tpgRange.ResumeLayout(false);
            this.tpgRange.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgSimple;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tpgUniqueValue;
        private System.Windows.Forms.TabPage tpgRange;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSimpleColor;
        private System.Windows.Forms.Button btnSimpleColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox UniqueValueList;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox ColorBar1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox ColorBar2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox rangeValueList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}