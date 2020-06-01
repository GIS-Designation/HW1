namespace MalaSpiritGIS
{
    partial class MLMap
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.featureMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拖动图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动图形坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.合并ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.featureMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // featureMenu
            // 
            this.featureMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除图形ToolStripMenuItem,
            this.拖动图形ToolStripMenuItem,
            this.移动图形坐标ToolStripMenuItem,
            this.编辑节点ToolStripMenuItem,
            this.合并ToolStripMenuItem});
            this.featureMenu.Name = "contextMenuStrip1";
            this.featureMenu.Size = new System.Drawing.Size(181, 136);
            // 
            // 删除图形ToolStripMenuItem
            // 
            this.删除图形ToolStripMenuItem.Name = "删除图形ToolStripMenuItem";
            this.删除图形ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.删除图形ToolStripMenuItem.Text = "删除图形";
            this.删除图形ToolStripMenuItem.Click += new System.EventHandler(this.删除图形ToolStripMenuItem_Click);
            // 
            // 拖动图形ToolStripMenuItem
            // 
            this.拖动图形ToolStripMenuItem.Name = "拖动图形ToolStripMenuItem";
            this.拖动图形ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.拖动图形ToolStripMenuItem.Text = "拖动图形";
            this.拖动图形ToolStripMenuItem.Click += new System.EventHandler(this.拖动图形ToolStripMenuItem_Click);
            // 
            // 移动图形坐标ToolStripMenuItem
            // 
            this.移动图形坐标ToolStripMenuItem.Name = "移动图形坐标ToolStripMenuItem";
            this.移动图形坐标ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.移动图形坐标ToolStripMenuItem.Text = "移动图形(坐标)";
            this.移动图形坐标ToolStripMenuItem.Click += new System.EventHandler(this.移动图形坐标ToolStripMenuItem_Click);
            // 
            // 编辑节点ToolStripMenuItem
            // 
            this.编辑节点ToolStripMenuItem.Name = "编辑节点ToolStripMenuItem";
            this.编辑节点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.编辑节点ToolStripMenuItem.Text = "编辑节点";
            this.编辑节点ToolStripMenuItem.Click += new System.EventHandler(this.编辑节点ToolStripMenuItem_Click);
            // 
            // 合并ToolStripMenuItem
            // 
            this.合并ToolStripMenuItem.Name = "合并ToolStripMenuItem";
            this.合并ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.合并ToolStripMenuItem.Text = "合并";
            this.合并ToolStripMenuItem.Click += new System.EventHandler(this.合并ToolStripMenuItem_Click);
            // 
            // MLMap
            // 
            this.DoubleBuffered = true;
            this.Name = "MLMap";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MLPaint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MLMouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MLMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MLMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MLMouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MLMouseWheel);
            this.featureMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip featureMenu;
        private System.Windows.Forms.ToolStripMenuItem 删除图形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拖动图形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动图形坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 合并ToolStripMenuItem;
    }
}
