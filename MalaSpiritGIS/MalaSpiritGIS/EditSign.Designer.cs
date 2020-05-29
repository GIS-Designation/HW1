namespace MalaSpiritGIS
{
    partial class EditSign
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
            this.tpgPoint = new System.Windows.Forms.TabPage();
            this.btnFilledConcentricCircles = new System.Windows.Forms.Button();
            this.btnHollowConcentricCircles = new System.Windows.Forms.Button();
            this.btnFilledTriangle = new System.Windows.Forms.Button();
            this.btnHollowTriangle = new System.Windows.Forms.Button();
            this.btnFilledSquare = new System.Windows.Forms.Button();
            this.btnHollowSquare = new System.Windows.Forms.Button();
            this.btnFilledCircle = new System.Windows.Forms.Button();
            this.btnHollowCircle = new System.Windows.Forms.Button();
            this.tpgPolyline = new System.Windows.Forms.TabPage();
            this.btnDashedLine = new System.Windows.Forms.Button();
            this.btnSolidLine = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.npdSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpgPoint.SuspendLayout();
            this.tpgPolyline.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npdSize)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgPoint);
            this.tabControl1.Controls.Add(this.tpgPolyline);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(308, 315);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgPoint
            // 
            this.tpgPoint.AutoScroll = true;
            this.tpgPoint.Controls.Add(this.btnFilledConcentricCircles);
            this.tpgPoint.Controls.Add(this.btnHollowConcentricCircles);
            this.tpgPoint.Controls.Add(this.btnFilledTriangle);
            this.tpgPoint.Controls.Add(this.btnHollowTriangle);
            this.tpgPoint.Controls.Add(this.btnFilledSquare);
            this.tpgPoint.Controls.Add(this.btnHollowSquare);
            this.tpgPoint.Controls.Add(this.btnFilledCircle);
            this.tpgPoint.Controls.Add(this.btnHollowCircle);
            this.tpgPoint.Location = new System.Drawing.Point(4, 22);
            this.tpgPoint.Name = "tpgPoint";
            this.tpgPoint.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPoint.Size = new System.Drawing.Size(300, 289);
            this.tpgPoint.TabIndex = 0;
            this.tpgPoint.Text = "点符号";
            this.tpgPoint.UseVisualStyleBackColor = true;
            // 
            // btnFilledConcentricCircles
            // 
            this.btnFilledConcentricCircles.BackColor = System.Drawing.Color.White;
            this.btnFilledConcentricCircles.Image = global::MalaSpiritGIS.Properties.Resources.FilledConcentricCircles;
            this.btnFilledConcentricCircles.Location = new System.Drawing.Point(116, 162);
            this.btnFilledConcentricCircles.Name = "btnFilledConcentricCircles";
            this.btnFilledConcentricCircles.Size = new System.Drawing.Size(75, 48);
            this.btnFilledConcentricCircles.TabIndex = 7;
            this.btnFilledConcentricCircles.Text = "实心同心圆";
            this.btnFilledConcentricCircles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilledConcentricCircles.UseVisualStyleBackColor = false;
            this.btnFilledConcentricCircles.Click += new System.EventHandler(this.btnFilledConcentricCircles_Click);
            // 
            // btnHollowConcentricCircles
            // 
            this.btnHollowConcentricCircles.BackColor = System.Drawing.Color.White;
            this.btnHollowConcentricCircles.Image = global::MalaSpiritGIS.Properties.Resources.HollowConcentricCircles;
            this.btnHollowConcentricCircles.Location = new System.Drawing.Point(16, 162);
            this.btnHollowConcentricCircles.Name = "btnHollowConcentricCircles";
            this.btnHollowConcentricCircles.Size = new System.Drawing.Size(75, 48);
            this.btnHollowConcentricCircles.TabIndex = 6;
            this.btnHollowConcentricCircles.Text = "空心同心圆";
            this.btnHollowConcentricCircles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHollowConcentricCircles.UseVisualStyleBackColor = false;
            this.btnHollowConcentricCircles.Click += new System.EventHandler(this.btnHollowConcentricCircles_Click);
            // 
            // btnFilledTriangle
            // 
            this.btnFilledTriangle.BackColor = System.Drawing.Color.White;
            this.btnFilledTriangle.Image = global::MalaSpiritGIS.Properties.Resources.FilledTriangle;
            this.btnFilledTriangle.Location = new System.Drawing.Point(214, 91);
            this.btnFilledTriangle.Name = "btnFilledTriangle";
            this.btnFilledTriangle.Size = new System.Drawing.Size(75, 48);
            this.btnFilledTriangle.TabIndex = 5;
            this.btnFilledTriangle.Text = "实心三角形";
            this.btnFilledTriangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilledTriangle.UseVisualStyleBackColor = false;
            this.btnFilledTriangle.Click += new System.EventHandler(this.btnFilledTriangle_Click);
            // 
            // btnHollowTriangle
            // 
            this.btnHollowTriangle.BackColor = System.Drawing.Color.White;
            this.btnHollowTriangle.Image = global::MalaSpiritGIS.Properties.Resources.HollowTriangle;
            this.btnHollowTriangle.Location = new System.Drawing.Point(116, 91);
            this.btnHollowTriangle.Name = "btnHollowTriangle";
            this.btnHollowTriangle.Size = new System.Drawing.Size(75, 48);
            this.btnHollowTriangle.TabIndex = 4;
            this.btnHollowTriangle.Text = "空心三角形";
            this.btnHollowTriangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHollowTriangle.UseVisualStyleBackColor = false;
            this.btnHollowTriangle.Click += new System.EventHandler(this.btnHollowTriangle_Click);
            // 
            // btnFilledSquare
            // 
            this.btnFilledSquare.BackColor = System.Drawing.Color.White;
            this.btnFilledSquare.Image = global::MalaSpiritGIS.Properties.Resources.FilledSquare;
            this.btnFilledSquare.Location = new System.Drawing.Point(16, 91);
            this.btnFilledSquare.Name = "btnFilledSquare";
            this.btnFilledSquare.Size = new System.Drawing.Size(75, 48);
            this.btnFilledSquare.TabIndex = 3;
            this.btnFilledSquare.Text = "实心正方形";
            this.btnFilledSquare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilledSquare.UseVisualStyleBackColor = false;
            this.btnFilledSquare.Click += new System.EventHandler(this.btnFilledSquare_Click);
            // 
            // btnHollowSquare
            // 
            this.btnHollowSquare.BackColor = System.Drawing.Color.White;
            this.btnHollowSquare.Image = global::MalaSpiritGIS.Properties.Resources.HollowSquare;
            this.btnHollowSquare.Location = new System.Drawing.Point(214, 18);
            this.btnHollowSquare.Name = "btnHollowSquare";
            this.btnHollowSquare.Size = new System.Drawing.Size(75, 47);
            this.btnHollowSquare.TabIndex = 2;
            this.btnHollowSquare.Text = "空心正方形";
            this.btnHollowSquare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHollowSquare.UseVisualStyleBackColor = false;
            this.btnHollowSquare.Click += new System.EventHandler(this.btnHollowSquare_Click);
            // 
            // btnFilledCircle
            // 
            this.btnFilledCircle.BackColor = System.Drawing.Color.White;
            this.btnFilledCircle.Image = global::MalaSpiritGIS.Properties.Resources.FilledCircle;
            this.btnFilledCircle.Location = new System.Drawing.Point(116, 18);
            this.btnFilledCircle.Name = "btnFilledCircle";
            this.btnFilledCircle.Size = new System.Drawing.Size(75, 48);
            this.btnFilledCircle.TabIndex = 1;
            this.btnFilledCircle.Text = "实心圆";
            this.btnFilledCircle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilledCircle.UseVisualStyleBackColor = false;
            this.btnFilledCircle.Click += new System.EventHandler(this.btnFilledCircle_Click);
            // 
            // btnHollowCircle
            // 
            this.btnHollowCircle.BackColor = System.Drawing.Color.White;
            this.btnHollowCircle.Image = global::MalaSpiritGIS.Properties.Resources.HollowCircle;
            this.btnHollowCircle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHollowCircle.Location = new System.Drawing.Point(16, 18);
            this.btnHollowCircle.Name = "btnHollowCircle";
            this.btnHollowCircle.Size = new System.Drawing.Size(75, 48);
            this.btnHollowCircle.TabIndex = 0;
            this.btnHollowCircle.Text = "空心圆";
            this.btnHollowCircle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHollowCircle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHollowCircle.UseVisualStyleBackColor = false;
            this.btnHollowCircle.Click += new System.EventHandler(this.btnHollowCircle_Click);
            // 
            // tpgPolyline
            // 
            this.tpgPolyline.Controls.Add(this.btnDashedLine);
            this.tpgPolyline.Controls.Add(this.btnSolidLine);
            this.tpgPolyline.Location = new System.Drawing.Point(4, 22);
            this.tpgPolyline.Name = "tpgPolyline";
            this.tpgPolyline.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPolyline.Size = new System.Drawing.Size(300, 289);
            this.tpgPolyline.TabIndex = 1;
            this.tpgPolyline.Text = "线符号";
            this.tpgPolyline.UseVisualStyleBackColor = true;
            // 
            // btnDashedLine
            // 
            this.btnDashedLine.BackColor = System.Drawing.Color.White;
            this.btnDashedLine.Image = global::MalaSpiritGIS.Properties.Resources.DashedLine;
            this.btnDashedLine.Location = new System.Drawing.Point(156, 25);
            this.btnDashedLine.Name = "btnDashedLine";
            this.btnDashedLine.Size = new System.Drawing.Size(115, 48);
            this.btnDashedLine.TabIndex = 5;
            this.btnDashedLine.Text = "虚线";
            this.btnDashedLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDashedLine.UseVisualStyleBackColor = false;
            this.btnDashedLine.Click += new System.EventHandler(this.btnDashedLine_Click);
            // 
            // btnSolidLine
            // 
            this.btnSolidLine.BackColor = System.Drawing.Color.White;
            this.btnSolidLine.Image = global::MalaSpiritGIS.Properties.Resources.SolidLine;
            this.btnSolidLine.Location = new System.Drawing.Point(20, 25);
            this.btnSolidLine.Name = "btnSolidLine";
            this.btnSolidLine.Size = new System.Drawing.Size(113, 48);
            this.btnSolidLine.TabIndex = 4;
            this.btnSolidLine.Text = "实线";
            this.btnSolidLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSolidLine.UseVisualStyleBackColor = false;
            this.btnSolidLine.Click += new System.EventHandler(this.btnSolidLine_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(350, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 122);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "当前符号";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.npdSize);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Controls.Add(this.lblColor);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(350, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "属性设置";
            // 
            // npdSize
            // 
            this.npdSize.DecimalPlaces = 2;
            this.npdSize.Location = new System.Drawing.Point(63, 66);
            this.npdSize.Name = "npdSize";
            this.npdSize.Size = new System.Drawing.Size(120, 21);
            this.npdSize.TabIndex = 4;
            this.npdSize.ValueChanged += new System.EventHandler(this.npdSize_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "大小：";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(109, 22);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(75, 23);
            this.btnColor.TabIndex = 2;
            this.btnColor.Text = "选择";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // lblColor
            // 
            this.lblColor.BackColor = System.Drawing.Color.White;
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(63, 22);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(40, 23);
            this.lblColor.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(350, 296);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(445, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditSign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 344);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Name = "EditSign";
            this.Text = "修改要素符号";
            this.Load += new System.EventHandler(this.EditSign_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpgPoint.ResumeLayout(false);
            this.tpgPolyline.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.npdSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgPoint;
        private System.Windows.Forms.TabPage tpgPolyline;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown npdSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHollowCircle;
        private System.Windows.Forms.Button btnFilledCircle;
        private System.Windows.Forms.Button btnHollowSquare;
        private System.Windows.Forms.Button btnFilledConcentricCircles;
        private System.Windows.Forms.Button btnHollowConcentricCircles;
        private System.Windows.Forms.Button btnFilledTriangle;
        private System.Windows.Forms.Button btnHollowTriangle;
        private System.Windows.Forms.Button btnFilledSquare;
        private System.Windows.Forms.Button btnDashedLine;
        private System.Windows.Forms.Button btnSolidLine;
    }
}