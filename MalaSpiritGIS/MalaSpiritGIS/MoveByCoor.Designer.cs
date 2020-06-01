namespace MalaSpiritGIS
{
    partial class MoveByCoor
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
            this.DSure = new System.Windows.Forms.Button();
            this.DY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DSure);
            this.groupBox1.Controls.Add(this.DY);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.DX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(1, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // DSure
            // 
            this.DSure.Location = new System.Drawing.Point(144, 49);
            this.DSure.Name = "DSure";
            this.DSure.Size = new System.Drawing.Size(75, 23);
            this.DSure.TabIndex = 5;
            this.DSure.Text = "确认";
            this.DSure.UseVisualStyleBackColor = true;
            this.DSure.Click += new System.EventHandler(this.DSure_Click);
            // 
            // DY
            // 
            this.DY.Location = new System.Drawing.Point(38, 51);
            this.DY.Name = "DY";
            this.DY.Size = new System.Drawing.Size(100, 21);
            this.DY.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Δy：";
            // 
            // DX
            // 
            this.DX.Location = new System.Drawing.Point(38, 24);
            this.DX.Name = "DX";
            this.DX.Size = new System.Drawing.Size(100, 21);
            this.DX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Δx：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入坐标改变值";
            // 
            // MoveByCoor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 84);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveByCoor";
            this.ShowIcon = false;
            this.Text = "输入坐标移动";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DSure;
        private System.Windows.Forms.TextBox DY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}