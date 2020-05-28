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
            this.SuspendLayout();
            // 
            // MLMap
            // 
            this.Name = "MLMap";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MLPaint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MLMouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MLMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MLMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MLMouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MLMouseWheel);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
