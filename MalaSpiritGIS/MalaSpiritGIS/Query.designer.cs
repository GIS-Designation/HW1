namespace MalaSpiritGIS
{
    partial class Query
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
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_cancel = new System.Windows.Forms.Button();
            this.LayerList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FieldList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ValueList = new System.Windows.Forms.ListBox();
            this.bt_equal = new System.Windows.Forms.Button();
            this.bt_gt = new System.Windows.Forms.Button();
            this.bt_lt = new System.Windows.Forms.Button();
            this.bt_le = new System.Windows.Forms.Button();
            this.bt_ge = new System.Windows.Forms.Button();
            this.bt_unequal = new System.Windows.Forms.Button();
            this.SQLTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_OK
            // 
            this.bt_OK.Location = new System.Drawing.Point(565, 595);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(134, 55);
            this.bt_OK.TabIndex = 0;
            this.bt_OK.Text = "确定";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.Bt_OK_Click);
            // 
            // bt_cancel
            // 
            this.bt_cancel.Location = new System.Drawing.Point(725, 595);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(134, 55);
            this.bt_cancel.TabIndex = 1;
            this.bt_cancel.Text = "关闭";
            this.bt_cancel.UseVisualStyleBackColor = true;
            this.bt_cancel.Click += new System.EventHandler(this.Bt_cancel_Click);
            // 
            // LayerList
            // 
            this.LayerList.FormattingEnabled = true;
            this.LayerList.Location = new System.Drawing.Point(161, 37);
            this.LayerList.Name = "LayerList";
            this.LayerList.Size = new System.Drawing.Size(635, 32);
            this.LayerList.TabIndex = 2;
            this.LayerList.SelectedIndexChanged += new System.EventHandler(this.LayerList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "图层：";
            // 
            // FieldList
            // 
            this.FieldList.FormattingEnabled = true;
            this.FieldList.ItemHeight = 24;
            this.FieldList.Location = new System.Drawing.Point(57, 133);
            this.FieldList.Name = "FieldList";
            this.FieldList.Size = new System.Drawing.Size(242, 268);
            this.FieldList.TabIndex = 4;
            this.FieldList.SelectedIndexChanged += new System.EventHandler(this.FieldList_SelectedIndexChanged);
            this.FieldList.DoubleClick += new System.EventHandler(this.FieldList_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "字段";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "唯一值";
            // 
            // ValueList
            // 
            this.ValueList.FormattingEnabled = true;
            this.ValueList.ItemHeight = 24;
            this.ValueList.Location = new System.Drawing.Point(316, 133);
            this.ValueList.Name = "ValueList";
            this.ValueList.Size = new System.Drawing.Size(242, 268);
            this.ValueList.TabIndex = 6;
            this.ValueList.DoubleClick += new System.EventHandler(this.ValueList_DoubleClick);
            // 
            // bt_equal
            // 
            this.bt_equal.Location = new System.Drawing.Point(629, 173);
            this.bt_equal.Name = "bt_equal";
            this.bt_equal.Size = new System.Drawing.Size(88, 38);
            this.bt_equal.TabIndex = 8;
            this.bt_equal.Text = "=";
            this.bt_equal.UseVisualStyleBackColor = true;
            this.bt_equal.Click += new System.EventHandler(this.Bt_equal_Click);
            // 
            // bt_gt
            // 
            this.bt_gt.Location = new System.Drawing.Point(629, 238);
            this.bt_gt.Name = "bt_gt";
            this.bt_gt.Size = new System.Drawing.Size(88, 38);
            this.bt_gt.TabIndex = 9;
            this.bt_gt.Text = ">";
            this.bt_gt.UseVisualStyleBackColor = true;
            this.bt_gt.Click += new System.EventHandler(this.Bt_gt_Click);
            // 
            // bt_lt
            // 
            this.bt_lt.Location = new System.Drawing.Point(629, 302);
            this.bt_lt.Name = "bt_lt";
            this.bt_lt.Size = new System.Drawing.Size(88, 38);
            this.bt_lt.TabIndex = 10;
            this.bt_lt.Text = "<";
            this.bt_lt.UseVisualStyleBackColor = true;
            this.bt_lt.Click += new System.EventHandler(this.Bt_lt_Click);
            // 
            // bt_le
            // 
            this.bt_le.Location = new System.Drawing.Point(743, 302);
            this.bt_le.Name = "bt_le";
            this.bt_le.Size = new System.Drawing.Size(88, 38);
            this.bt_le.TabIndex = 13;
            this.bt_le.Text = "<=";
            this.bt_le.UseVisualStyleBackColor = true;
            this.bt_le.Click += new System.EventHandler(this.Bt_le_Click);
            // 
            // bt_ge
            // 
            this.bt_ge.Location = new System.Drawing.Point(743, 238);
            this.bt_ge.Name = "bt_ge";
            this.bt_ge.Size = new System.Drawing.Size(88, 38);
            this.bt_ge.TabIndex = 12;
            this.bt_ge.Text = ">=";
            this.bt_ge.UseVisualStyleBackColor = true;
            this.bt_ge.Click += new System.EventHandler(this.Bt_ge_Click);
            // 
            // bt_unequal
            // 
            this.bt_unequal.Location = new System.Drawing.Point(743, 173);
            this.bt_unequal.Name = "bt_unequal";
            this.bt_unequal.Size = new System.Drawing.Size(88, 38);
            this.bt_unequal.TabIndex = 11;
            this.bt_unequal.Text = "<>";
            this.bt_unequal.UseVisualStyleBackColor = true;
            this.bt_unequal.Click += new System.EventHandler(this.Bt_unequal_Click);
            // 
            // SQLTextBox
            // 
            this.SQLTextBox.Location = new System.Drawing.Point(61, 470);
            this.SQLTextBox.Multiline = true;
            this.SQLTextBox.Name = "SQLTextBox";
            this.SQLTextBox.Size = new System.Drawing.Size(770, 95);
            this.SQLTextBox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 440);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "Select * From Layer Where";
            // 
            // Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 676);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SQLTextBox);
            this.Controls.Add(this.bt_le);
            this.Controls.Add(this.bt_ge);
            this.Controls.Add(this.bt_unequal);
            this.Controls.Add(this.bt_lt);
            this.Controls.Add(this.bt_gt);
            this.Controls.Add(this.bt_equal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ValueList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FieldList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LayerList);
            this.Controls.Add(this.bt_cancel);
            this.Controls.Add(this.bt_OK);
            this.Name = "Query";
            this.Text = "Query";
            this.Load += new System.EventHandler(this.Query_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_cancel;
        private System.Windows.Forms.ComboBox LayerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox FieldList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox ValueList;
        private System.Windows.Forms.Button bt_equal;
        private System.Windows.Forms.Button bt_gt;
        private System.Windows.Forms.Button bt_lt;
        private System.Windows.Forms.Button bt_le;
        private System.Windows.Forms.Button bt_ge;
        private System.Windows.Forms.Button bt_unequal;
        private System.Windows.Forms.TextBox SQLTextBox;
        private System.Windows.Forms.Label label4;
    }
}