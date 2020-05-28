namespace MalaSpiritGIS
{
    partial class MLMainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            dataFrame = new MLDataFrame.Dataframe();
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.createFeature = new System.Windows.Forms.Button();
            this.selectFeature = new System.Windows.Forms.Button();
            this.zoomIn = new System.Windows.Forms.Button();
            this.zoomOut = new System.Windows.Forms.Button();
            this.pan = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.Button();
            this.featureMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拖动图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动图形坐标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.裁剪ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mlMap = new MalaSpiritGIS.MLMap(dataFrame);  //初始化时就传入dataFrame防止数据不一致
            this.mlFeatureBox = new MalaSpiritGIS.MLFeatureBox(dataFrame);  //同上
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.featureMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(71, 26);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1067, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // createFeature
            // 
            this.createFeature.Location = new System.Drawing.Point(0, 32);
            this.createFeature.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createFeature.Name = "createFeature";
            this.createFeature.Size = new System.Drawing.Size(100, 31);
            this.createFeature.TabIndex = 3;
            this.createFeature.Text = "创建要素";
            this.createFeature.UseVisualStyleBackColor = true;
            this.createFeature.Click += new System.EventHandler(this.createFeature_Click);
            // 
            // selectFeature
            // 
            this.selectFeature.Location = new System.Drawing.Point(107, 32);
            this.selectFeature.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectFeature.Name = "selectFeature";
            this.selectFeature.Size = new System.Drawing.Size(100, 31);
            this.selectFeature.TabIndex = 4;
            this.selectFeature.Text = "选择要素";
            this.selectFeature.UseVisualStyleBackColor = true;
            this.selectFeature.Click += new System.EventHandler(this.selectFeature_Click);
            // 
            // zoomIn
            // 
            this.zoomIn.Location = new System.Drawing.Point(213, 32);
            this.zoomIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.Size = new System.Drawing.Size(100, 31);
            this.zoomIn.TabIndex = 5;
            this.zoomIn.Text = "放大";
            this.zoomIn.UseVisualStyleBackColor = true;
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.Location = new System.Drawing.Point(320, 32);
            this.zoomOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(100, 31);
            this.zoomOut.TabIndex = 6;
            this.zoomOut.Text = "缩小";
            this.zoomOut.UseVisualStyleBackColor = true;
            this.zoomOut.Click += new System.EventHandler(this.zoomOut_Click);
            // 
            // pan
            // 
            this.pan.Location = new System.Drawing.Point(427, 32);
            this.pan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pan.Name = "pan";
            this.pan.Size = new System.Drawing.Size(100, 31);
            this.pan.TabIndex = 7;
            this.pan.Text = "漫游";
            this.pan.UseVisualStyleBackColor = true;
            this.pan.Click += new System.EventHandler(this.pan_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(533, 32);
            this.query.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(100, 31);
            this.query.TabIndex = 8;
            this.query.Text = "条件查询";
            this.query.UseVisualStyleBackColor = true;
            // 
            // featureMenu
            // 
            this.featureMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.featureMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除图形ToolStripMenuItem,
            this.拖动图形ToolStripMenuItem,
            this.移动图形坐标ToolStripMenuItem,
            this.编辑节点ToolStripMenuItem,
            this.裁剪ToolStripMenuItem});
            this.featureMenu.Name = "featureMenu";
            this.featureMenu.Size = new System.Drawing.Size(179, 124);
            // 
            // 删除图形ToolStripMenuItem
            // 
            this.删除图形ToolStripMenuItem.Name = "删除图形ToolStripMenuItem";
            this.删除图形ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.删除图形ToolStripMenuItem.Text = "删除图形";
            // 
            // 拖动图形ToolStripMenuItem
            // 
            this.拖动图形ToolStripMenuItem.Name = "拖动图形ToolStripMenuItem";
            this.拖动图形ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.拖动图形ToolStripMenuItem.Text = "拖动图形";
            // 
            // 移动图形坐标ToolStripMenuItem
            // 
            this.移动图形坐标ToolStripMenuItem.Name = "移动图形坐标ToolStripMenuItem";
            this.移动图形坐标ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.移动图形坐标ToolStripMenuItem.Text = "移动图形(坐标)";
            // 
            // 编辑节点ToolStripMenuItem
            // 
            this.编辑节点ToolStripMenuItem.Name = "编辑节点ToolStripMenuItem";
            this.编辑节点ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.编辑节点ToolStripMenuItem.Text = "编辑节点";
            // 
            // 裁剪ToolStripMenuItem
            // 
            this.裁剪ToolStripMenuItem.Name = "裁剪ToolStripMenuItem";
            this.裁剪ToolStripMenuItem.Size = new System.Drawing.Size(178, 24);
            this.裁剪ToolStripMenuItem.Text = "裁剪";
            // 
            // mlMap
            // 
            this.mlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mlMap.BackColor = System.Drawing.Color.White;
            this.mlMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlMap.Location = new System.Drawing.Point(127, 65);
            this.mlMap.Name = "mlMap";
            this.mlMap.Size = new System.Drawing.Size(928, 466);
            this.mlMap.TabIndex = 9;
            this.mlMap.TrackingFinished += new MalaSpiritGIS.MLMap.TrackingFinishedHandle(this.mlMap_TrackingFinished);
            this.mlMap.DisplayScaleChanged += new MalaSpiritGIS.MLMap.DisplayScaleChangedHandle(this.mlMap_DisplayScaleChanged);
            this.mlMap.SelectingFinished += new MalaSpiritGIS.MLMap.SelectingFinishiedHandle(this.mlMap_SelectingFinished);
            this.mlMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mlMap_MouseMove);
            // 
            // mlFeatureBox
            // 
            this.mlFeatureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mlFeatureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlFeatureBox.Location = new System.Drawing.Point(0, 65);
            this.mlFeatureBox.Margin = new System.Windows.Forms.Padding(5);
            this.mlFeatureBox.MaximumSize = new System.Drawing.Size(119, 1350);
            this.mlFeatureBox.MinimumSize = new System.Drawing.Size(119, 2);
            this.mlFeatureBox.Name = "mlFeatureBox";
            this.mlFeatureBox.Size = new System.Drawing.Size(119, 466);
            this.mlFeatureBox.TabIndex = 2;
            // 
            // MLMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.mlMap);
            this.Controls.Add(this.query);
            this.Controls.Add(this.pan);
            this.Controls.Add(this.zoomOut);
            this.Controls.Add(this.zoomIn);
            this.Controls.Add(this.selectFeature);
            this.Controls.Add(this.createFeature);
            this.Controls.Add(this.mlFeatureBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MLMainForm";
            this.Text = "麻辣精灵GIS";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.featureMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private MLFeatureBox mlFeatureBox;
        private System.Windows.Forms.Button createFeature;
        private System.Windows.Forms.Button selectFeature;
        private System.Windows.Forms.Button zoomIn;
        private System.Windows.Forms.Button zoomOut;
        private System.Windows.Forms.Button pan;
        private System.Windows.Forms.Button query;
        private System.Windows.Forms.ContextMenuStrip featureMenu;
        private System.Windows.Forms.ToolStripMenuItem 删除图形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拖动图形ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动图形坐标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑节点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 裁剪ToolStripMenuItem;
        private MLMap mlMap;
    }
}

