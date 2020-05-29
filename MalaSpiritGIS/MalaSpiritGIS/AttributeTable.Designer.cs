﻿namespace MalaSpiritGIS
{
    partial class AttributeTable
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.增加字段ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attributeView = new System.Windows.Forms.DataGridView();
            this.开始编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按属性查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attributeView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.增加字段ToolStripMenuItem,
            this.开始编辑ToolStripMenuItem,
            this.停止编辑ToolStripMenuItem,
            this.按属性查询ToolStripMenuItem,
            this.全部选择ToolStripMenuItem,
            this.取消选择ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(600, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 增加字段ToolStripMenuItem
            // 
            this.增加字段ToolStripMenuItem.Name = "增加字段ToolStripMenuItem";
            this.增加字段ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.增加字段ToolStripMenuItem.Text = "增加字段";
            this.增加字段ToolStripMenuItem.Click += new System.EventHandler(this.增加字段ToolStripMenuItem_Click);
            // 
            // attributeView
            // 
            this.attributeView.AllowUserToAddRows = false;
            this.attributeView.AllowUserToDeleteRows = false;
            this.attributeView.AllowUserToOrderColumns = true;
            this.attributeView.AllowUserToResizeColumns = false;
            this.attributeView.AllowUserToResizeRows = false;
            this.attributeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.attributeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.attributeView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.attributeView.Location = new System.Drawing.Point(0, 25);
            this.attributeView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.attributeView.Name = "attributeView";
            this.attributeView.RowHeadersWidth = 51;
            this.attributeView.RowTemplate.Height = 27;
            this.attributeView.Size = new System.Drawing.Size(600, 334);
            this.attributeView.TabIndex = 1;
            // 
            // 开始编辑ToolStripMenuItem
            // 
            this.开始编辑ToolStripMenuItem.Name = "开始编辑ToolStripMenuItem";
            this.开始编辑ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开始编辑ToolStripMenuItem.Text = "开始编辑";
            // 
            // 停止编辑ToolStripMenuItem
            // 
            this.停止编辑ToolStripMenuItem.Name = "停止编辑ToolStripMenuItem";
            this.停止编辑ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.停止编辑ToolStripMenuItem.Text = "停止编辑";
            // 
            // 按属性查询ToolStripMenuItem
            // 
            this.按属性查询ToolStripMenuItem.Name = "按属性查询ToolStripMenuItem";
            this.按属性查询ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.按属性查询ToolStripMenuItem.Text = "按属性查询";
            // 
            // 全部选择ToolStripMenuItem
            // 
            this.全部选择ToolStripMenuItem.Name = "全部选择ToolStripMenuItem";
            this.全部选择ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.全部选择ToolStripMenuItem.Text = "全部选择";
            // 
            // 取消选择ToolStripMenuItem
            // 
            this.取消选择ToolStripMenuItem.Name = "取消选择ToolStripMenuItem";
            this.取消选择ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.取消选择ToolStripMenuItem.Text = "取消选择";
            // 
            // AttributeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.attributeView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AttributeTable";
            this.Text = "AttributeTable";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attributeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 增加字段ToolStripMenuItem;
        private System.Windows.Forms.DataGridView attributeView;
        private System.Windows.Forms.ToolStripMenuItem 开始编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按属性查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消选择ToolStripMenuItem;
    }
}