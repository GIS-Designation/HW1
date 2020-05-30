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
            this.components = new System.ComponentModel.Container();
            this.mlMap = new MalaSpiritGIS.MLMap();
            this.mlFeatureBox = new MalaSpiritGIS.MLFeatureBox();
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
            this.zoomToLayer = new System.Windows.Forms.Button();
            this.mlRecordBox = new MalaSpiritGIS.MLFeatureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.featureMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mlMap
            // 
            this.mlMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mlMap.BackColor = System.Drawing.Color.White;
            this.mlMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mlMap.Location = new System.Drawing.Point(92, 52);
            this.mlMap.Margin = new System.Windows.Forms.Padding(2);
            this.mlMap.Name = "mlMap";
            this.mlMap.Size = new System.Drawing.Size(644, 373);
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
            this.mlFeatureBox.Location = new System.Drawing.Point(0, 52);
            this.mlFeatureBox.Margin = new System.Windows.Forms.Padding(4);
            this.mlFeatureBox.MaximumSize = new System.Drawing.Size(90, 1080);
            this.mlFeatureBox.MinimumSize = new System.Drawing.Size(90, 2);
            this.mlFeatureBox.Name = "mlFeatureBox";
            this.mlFeatureBox.Size = new System.Drawing.Size(90, 373);
            this.mlFeatureBox.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(836, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(836, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // createFeature
            // 
            this.createFeature.Location = new System.Drawing.Point(0, 26);
            this.createFeature.Margin = new System.Windows.Forms.Padding(2);
            this.createFeature.Name = "createFeature";
            this.createFeature.Size = new System.Drawing.Size(75, 25);
            this.createFeature.TabIndex = 3;
            this.createFeature.Text = "创建要素";
            this.createFeature.UseVisualStyleBackColor = true;
            this.createFeature.Click += new System.EventHandler(this.createFeature_Click);
            // 
            // selectFeature
            // 
            this.selectFeature.Location = new System.Drawing.Point(80, 26);
            this.selectFeature.Margin = new System.Windows.Forms.Padding(2);
            this.selectFeature.Name = "selectFeature";
            this.selectFeature.Size = new System.Drawing.Size(75, 25);
            this.selectFeature.TabIndex = 4;
            this.selectFeature.Text = "选择要素";
            this.selectFeature.UseVisualStyleBackColor = true;
            this.selectFeature.Click += new System.EventHandler(this.selectFeature_Click);
            // 
            // zoomIn
            // 
            this.zoomIn.Location = new System.Drawing.Point(160, 26);
            this.zoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.Size = new System.Drawing.Size(75, 25);
            this.zoomIn.TabIndex = 5;
            this.zoomIn.Text = "放大";
            this.zoomIn.UseVisualStyleBackColor = true;
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.Location = new System.Drawing.Point(240, 26);
            this.zoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(75, 25);
            this.zoomOut.TabIndex = 6;
            this.zoomOut.Text = "缩小";
            this.zoomOut.UseVisualStyleBackColor = true;
            this.zoomOut.Click += new System.EventHandler(this.zoomOut_Click);
            // 
            // pan
            // 
            this.pan.Location = new System.Drawing.Point(320, 26);
            this.pan.Margin = new System.Windows.Forms.Padding(2);
            this.pan.Name = "pan";
            this.pan.Size = new System.Drawing.Size(75, 25);
            this.pan.TabIndex = 7;
            this.pan.Text = "漫游";
            this.pan.UseVisualStyleBackColor = true;
            this.pan.Click += new System.EventHandler(this.pan_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(400, 26);
            this.query.Margin = new System.Windows.Forms.Padding(2);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(75, 25);
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
            this.featureMenu.Size = new System.Drawing.Size(157, 114);
            // 
            // 删除图形ToolStripMenuItem
            // 
            this.删除图形ToolStripMenuItem.Name = "删除图形ToolStripMenuItem";
            this.删除图形ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.删除图形ToolStripMenuItem.Text = "删除图形";
            // 
            // 拖动图形ToolStripMenuItem
            // 
            this.拖动图形ToolStripMenuItem.Name = "拖动图形ToolStripMenuItem";
            this.拖动图形ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.拖动图形ToolStripMenuItem.Text = "拖动图形";
            // 
            // 移动图形坐标ToolStripMenuItem
            // 
            this.移动图形坐标ToolStripMenuItem.Name = "移动图形坐标ToolStripMenuItem";
            this.移动图形坐标ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.移动图形坐标ToolStripMenuItem.Text = "移动图形(坐标)";
            // 
            // 编辑节点ToolStripMenuItem
            // 
            this.编辑节点ToolStripMenuItem.Name = "编辑节点ToolStripMenuItem";
            this.编辑节点ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.编辑节点ToolStripMenuItem.Text = "编辑节点";
            // 
            // 裁剪ToolStripMenuItem
            // 
            this.裁剪ToolStripMenuItem.Name = "裁剪ToolStripMenuItem";
            this.裁剪ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.裁剪ToolStripMenuItem.Text = "裁剪";
            // 
            // zoomToLayer
            // 
            this.zoomToLayer.Location = new System.Drawing.Point(480, 26);
            this.zoomToLayer.Margin = new System.Windows.Forms.Padding(2);
            this.zoomToLayer.Name = "zoomToLayer";
            this.zoomToLayer.Size = new System.Drawing.Size(75, 25);
            this.zoomToLayer.TabIndex = 10;
            this.zoomToLayer.Text = "缩放至图层";
            this.zoomToLayer.UseVisualStyleBackColor = true;
            this.zoomToLayer.Click += new System.EventHandler(this.zoomToLayer_Click);
            // 
            // mlRecordBox
            // 
            this.mlRecordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mlRecordBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlRecordBox.Location = new System.Drawing.Point(742, 52);
            this.mlRecordBox.Margin = new System.Windows.Forms.Padding(4);
            this.mlRecordBox.MaximumSize = new System.Drawing.Size(90, 1080);
            this.mlRecordBox.MinimumSize = new System.Drawing.Size(90, 2);
            this.mlRecordBox.Name = "mlRecordBox";
            this.mlRecordBox.Size = new System.Drawing.Size(90, 373);
            this.mlRecordBox.TabIndex = 11;
            // 
            // MLMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 450);
            this.Controls.Add(this.mlRecordBox);
            this.Controls.Add(this.zoomToLayer);
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
        private System.Windows.Forms.Button zoomToLayer;
        private MLFeatureBox mlRecordBox;
    }
}

