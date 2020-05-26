namespace MalaSpiritGIS
{
    partial class InputDialog
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
            this.inputBox = new System.Windows.Forms.TextBox();
            this.sure = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 12);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(198, 25);
            this.inputBox.TabIndex = 0;
            // 
            // sure
            // 
            this.sure.Location = new System.Drawing.Point(120, 43);
            this.sure.Name = "sure";
            this.sure.Size = new System.Drawing.Size(90, 35);
            this.sure.TabIndex = 1;
            this.sure.Text = "确认";
            this.sure.UseVisualStyleBackColor = true;
            this.sure.Click += new System.EventHandler(this.sure_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(12, 43);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(90, 35);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // InputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 84);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.sure);
            this.Controls.Add(this.inputBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "输入";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Input_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button sure;
        private System.Windows.Forms.Button cancel;
    }
}