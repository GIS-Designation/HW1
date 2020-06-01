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
            this.tpgUniqueValue = new System.Windows.Forms.TabPage();
            this.tpgRange = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSimpleColor = new System.Windows.Forms.Label();
            this.btnSimpleColor = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpgSimple.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgSimple);
            this.tabControl1.Controls.Add(this.tpgUniqueValue);
            this.tabControl1.Controls.Add(this.tpgRange);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
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
            // tpgUniqueValue
            // 
            this.tpgUniqueValue.Location = new System.Drawing.Point(4, 22);
            this.tpgUniqueValue.Name = "tpgUniqueValue";
            this.tpgUniqueValue.Padding = new System.Windows.Forms.Padding(3);
            this.tpgUniqueValue.Size = new System.Drawing.Size(652, 358);
            this.tpgUniqueValue.TabIndex = 1;
            this.tpgUniqueValue.Text = "唯一值法";
            this.tpgUniqueValue.UseVisualStyleBackColor = true;
            // 
            // tpgRange
            // 
            this.tpgRange.Location = new System.Drawing.Point(4, 22);
            this.tpgRange.Name = "tpgRange";
            this.tpgRange.Size = new System.Drawing.Size(652, 358);
            this.tpgRange.TabIndex = 2;
            this.tpgRange.Text = "分级法";
            this.tpgRange.UseVisualStyleBackColor = true;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色：";
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
            // MapRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "MapRender";
            this.Text = "图层渲染";
            this.tabControl1.ResumeLayout(false);
            this.tpgSimple.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}