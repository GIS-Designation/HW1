namespace MalaSpiritGIS
{
    partial class MLFeatureBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MLFeatureBox));
            this.up = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.top = new System.Windows.Forms.Button();
            this.bottom = new System.Windows.Forms.Button();
            this.boxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建点图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建多点图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建线图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建面图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.修改图层名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开属性表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标注要素ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加载图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boxMenu.SuspendLayout();
            this.layerMenu.SuspendLayout();
            this.recordMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // up
            // 
            this.up.BackColor = System.Drawing.Color.White;
            this.up.Location = new System.Drawing.Point(2, 2);
            this.up.Margin = new System.Windows.Forms.Padding(0);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(20, 20);
            this.up.TabIndex = 0;
            this.up.Text = "↑";
            this.up.UseVisualStyleBackColor = false;
            this.up.Click += new System.EventHandler(this.up_Click);
            // 
            // down
            // 
            this.down.BackColor = System.Drawing.Color.White;
            this.down.Location = new System.Drawing.Point(24, 2);
            this.down.Margin = new System.Windows.Forms.Padding(0);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(20, 20);
            this.down.TabIndex = 1;
            this.down.Text = "↓";
            this.down.UseVisualStyleBackColor = false;
            this.down.Click += new System.EventHandler(this.down_Click);
            // 
            // top
            // 
            this.top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("top.BackgroundImage")));
            this.top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.top.Location = new System.Drawing.Point(46, 2);
            this.top.Margin = new System.Windows.Forms.Padding(0);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(20, 20);
            this.top.TabIndex = 2;
            this.top.UseVisualStyleBackColor = true;
            this.top.Click += new System.EventHandler(this.top_Click);
            // 
            // bottom
            // 
            this.bottom.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bottom.BackgroundImage")));
            this.bottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bottom.Location = new System.Drawing.Point(68, 2);
            this.bottom.Margin = new System.Windows.Forms.Padding(0);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(20, 20);
            this.bottom.TabIndex = 3;
            this.bottom.UseVisualStyleBackColor = true;
            this.bottom.Click += new System.EventHandler(this.bottom_Click);
            // 
            // boxMenu
            // 
            this.boxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.boxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建点图层ToolStripMenuItem,
            this.新建多点图层ToolStripMenuItem,
            this.新建线图层ToolStripMenuItem,
            this.新建面图层ToolStripMenuItem});
            this.boxMenu.Name = "boxMenu";
            this.boxMenu.Size = new System.Drawing.Size(149, 92);
            // 
            // 新建点图层ToolStripMenuItem
            // 
            this.新建点图层ToolStripMenuItem.Name = "新建点图层ToolStripMenuItem";
            this.新建点图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建点图层ToolStripMenuItem.Text = "新建点图层";
            this.新建点图层ToolStripMenuItem.Click += new System.EventHandler(this.新建点图层ToolStripMenuItem_Click);
            // 
            // 新建多点图层ToolStripMenuItem
            // 
            this.新建多点图层ToolStripMenuItem.Name = "新建多点图层ToolStripMenuItem";
            this.新建多点图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建多点图层ToolStripMenuItem.Text = "新建多点图层";
            this.新建多点图层ToolStripMenuItem.Click += new System.EventHandler(this.新建多点图层ToolStripMenuItem_Click);
            // 
            // 新建线图层ToolStripMenuItem
            // 
            this.新建线图层ToolStripMenuItem.Name = "新建线图层ToolStripMenuItem";
            this.新建线图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建线图层ToolStripMenuItem.Text = "新建线图层";
            this.新建线图层ToolStripMenuItem.Click += new System.EventHandler(this.新建线图层ToolStripMenuItem_Click);
            // 
            // 新建面图层ToolStripMenuItem
            // 
            this.新建面图层ToolStripMenuItem.Name = "新建面图层ToolStripMenuItem";
            this.新建面图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建面图层ToolStripMenuItem.Text = "新建面图层";
            this.新建面图层ToolStripMenuItem.Click += new System.EventHandler(this.新建面图层ToolStripMenuItem_Click);
            // 
            // layerMenu
            // 
            this.layerMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.layerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改图层名称ToolStripMenuItem,
            this.删除图层ToolStripMenuItem,
            this.保存图层ToolStripMenuItem,
            this.打开属性表ToolStripMenuItem,
            this.渲染ToolStripMenuItem,
            this.标注要素ToolStripMenuItem});
            this.layerMenu.Name = "contextMenuStrip1";
            this.layerMenu.Size = new System.Drawing.Size(149, 136);
            // 
            // 修改图层名称ToolStripMenuItem
            // 
            this.修改图层名称ToolStripMenuItem.Name = "修改图层名称ToolStripMenuItem";
            this.修改图层名称ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.修改图层名称ToolStripMenuItem.Text = "修改图层名称";
            this.修改图层名称ToolStripMenuItem.Click += new System.EventHandler(this.修改图层名称ToolStripMenuItem_Click);
            // 
            // 删除图层ToolStripMenuItem
            // 
            this.删除图层ToolStripMenuItem.Name = "删除图层ToolStripMenuItem";
            this.删除图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.删除图层ToolStripMenuItem.Text = "删除图层";
            this.删除图层ToolStripMenuItem.Click += new System.EventHandler(this.删除图层ToolStripMenuItem_Click);
            // 
            // 保存图层ToolStripMenuItem
            // 
            this.保存图层ToolStripMenuItem.Name = "保存图层ToolStripMenuItem";
            this.保存图层ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.保存图层ToolStripMenuItem.Text = "保存图层";
            this.保存图层ToolStripMenuItem.Click += new System.EventHandler(this.保存图层ToolStripMenuItem_Click);
            // 
            // 打开属性表ToolStripMenuItem
            // 
            this.打开属性表ToolStripMenuItem.Name = "打开属性表ToolStripMenuItem";
            this.打开属性表ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开属性表ToolStripMenuItem.Text = "打开属性表";
            this.打开属性表ToolStripMenuItem.Click += new System.EventHandler(this.打开属性表ToolStripMenuItem_Click);
            // 
            // 渲染ToolStripMenuItem
            // 
            this.渲染ToolStripMenuItem.Name = "渲染ToolStripMenuItem";
            this.渲染ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.渲染ToolStripMenuItem.Text = "渲染";
            this.渲染ToolStripMenuItem.Click += new System.EventHandler(this.渲染ToolStripMenuItem_Click);
            // 
            // 标注要素ToolStripMenuItem
            // 
            this.标注要素ToolStripMenuItem.Name = "标注要素ToolStripMenuItem";
            this.标注要素ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.标注要素ToolStripMenuItem.Text = "标注要素";
            this.标注要素ToolStripMenuItem.Click += new System.EventHandler(this.标注要素ToolStripMenuItem_Click);
            // 
            // recordMenu
            // 
            this.recordMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载图层ToolStripMenuItem});
            this.recordMenu.Name = "contextMenuStrip1";
            this.recordMenu.Size = new System.Drawing.Size(125, 26);
            // 
            // 加载图层ToolStripMenuItem
            // 
            this.加载图层ToolStripMenuItem.Name = "加载图层ToolStripMenuItem";
            this.加载图层ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.加载图层ToolStripMenuItem.Text = "加载图层";
            this.加载图层ToolStripMenuItem.Click += new System.EventHandler(this.加载图层ToolStripMenuItem_Click);
            // 
            // MLFeatureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.bottom);
            this.Controls.Add(this.top);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.MaximumSize = new System.Drawing.Size(90, 1080);
            this.MinimumSize = new System.Drawing.Size(90, 2);
            this.Name = "MLFeatureBox";
            this.Size = new System.Drawing.Size(88, 362);
            this.boxMenu.ResumeLayout(false);
            this.layerMenu.ResumeLayout(false);
            this.recordMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button top;
        private System.Windows.Forms.Button bottom;
        private System.Windows.Forms.ContextMenuStrip boxMenu;
        private System.Windows.Forms.ToolStripMenuItem 新建点图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建线图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建面图层ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip layerMenu;
        private System.Windows.Forms.ToolStripMenuItem 修改图层名称ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开属性表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建多点图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 渲染ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标注要素ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip recordMenu;
        private System.Windows.Forms.ToolStripMenuItem 加载图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存图层ToolStripMenuItem;
    }
}
