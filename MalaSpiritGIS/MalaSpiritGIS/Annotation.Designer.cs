namespace MalaSpiritGIS
{
    partial class Annotation
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLayerName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UniqueValueList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnTrackingColor = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelFont = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.npdSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npdSize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.npdSize);
            this.groupBox1.Controls.Add(this.labelSize);
            this.groupBox1.Controls.Add(this.labelFont);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnTrackingColor);
            this.groupBox1.Controls.Add(this.lblColor);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.UniqueValueList);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelLayerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前图层：";
            // 
            // labelLayerName
            // 
            this.labelLayerName.AutoSize = true;
            this.labelLayerName.Location = new System.Drawing.Point(78, 17);
            this.labelLayerName.Name = "labelLayerName";
            this.labelLayerName.Size = new System.Drawing.Size(0, 12);
            this.labelLayerName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "注记字段：";
            // 
            // UniqueValueList
            // 
            this.UniqueValueList.FormattingEnabled = true;
            this.UniqueValueList.Location = new System.Drawing.Point(79, 47);
            this.UniqueValueList.Name = "UniqueValueList";
            this.UniqueValueList.Size = new System.Drawing.Size(127, 20);
            this.UniqueValueList.TabIndex = 3;
            this.UniqueValueList.SelectedIndexChanged += new System.EventHandler(this.UniqueValueList_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(137, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(233, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnTrackingColor
            // 
            this.btnTrackingColor.Location = new System.Drawing.Point(213, 78);
            this.btnTrackingColor.Name = "btnTrackingColor";
            this.btnTrackingColor.Size = new System.Drawing.Size(75, 23);
            this.btnTrackingColor.TabIndex = 11;
            this.btnTrackingColor.Text = "选择";
            this.btnTrackingColor.UseVisualStyleBackColor = true;
            this.btnTrackingColor.Click += new System.EventHandler(this.btnTrackingColor_Click);
            // 
            // lblColor
            // 
            this.lblColor.BackColor = System.Drawing.Color.White;
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(79, 82);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(127, 12);
            this.lblColor.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "注记颜色：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(211, 114);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "选择";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "注记字体";
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(77, 119);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(0, 12);
            this.labelFont.TabIndex = 15;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(10, 146);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(65, 12);
            this.labelSize.TabIndex = 16;
            this.labelSize.Text = "注记字号：";
            // 
            // npdSize
            // 
            this.npdSize.DecimalPlaces = 2;
            this.npdSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.npdSize.Location = new System.Drawing.Point(81, 144);
            this.npdSize.Name = "npdSize";
            this.npdSize.Size = new System.Drawing.Size(120, 21);
            this.npdSize.TabIndex = 17;
            this.npdSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.npdSize.ValueChanged += new System.EventHandler(this.npdSize_ValueChanged);
            // 
            // Annotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 273);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Annotation";
            this.Text = "设置注记字段";
            this.Load += new System.EventHandler(this.Annotation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npdSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox UniqueValueList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTrackingColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown npdSize;
    }
}