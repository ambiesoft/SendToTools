namespace SendToManager
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.lvMain = new System.Windows.Forms.ListView();
            this.chNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pgItem = new System.Windows.Forms.PropertyGrid();
            this.spMain = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addNewItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).BeginInit();
            this.spMain.Panel1.SuspendLayout();
            this.spMain.Panel2.SuspendLayout();
            this.spMain.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNo,
            this.chName});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.FullRowSelect = true;
            this.lvMain.Location = new System.Drawing.Point(0, 0);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(263, 457);
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.VirtualMode = true;
            this.lvMain.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvMain_RetrieveVirtualItem);
            this.lvMain.VirtualItemsSelectionRangeChanged += new System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventHandler(this.lvMain_VirtualItemsSelectionRangeChanged);
            // 
            // chNo
            // 
            this.chNo.Text = "No.";
            this.chNo.Width = 24;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 300;
            // 
            // pgItem
            // 
            this.pgItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgItem.LineColor = System.Drawing.SystemColors.ControlDark;
            this.pgItem.Location = new System.Drawing.Point(0, 0);
            this.pgItem.Name = "pgItem";
            this.pgItem.Size = new System.Drawing.Size(522, 457);
            this.pgItem.TabIndex = 4;
            // 
            // spMain
            // 
            this.spMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spMain.Location = new System.Drawing.Point(0, 24);
            this.spMain.Name = "spMain";
            // 
            // spMain.Panel1
            // 
            this.spMain.Panel1.Controls.Add(this.lvMain);
            // 
            // spMain.Panel2
            // 
            this.spMain.Panel2.Controls.Add(this.pgItem);
            this.spMain.Size = new System.Drawing.Size(789, 457);
            this.spMain.SplitterDistance = 263;
            this.spMain.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewItemToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(789, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addNewItemToolStripMenuItem
            // 
            this.addNewItemToolStripMenuItem.Name = "addNewItemToolStripMenuItem";
            this.addNewItemToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.addNewItemToolStripMenuItem.Text = "Add &New Item";
            this.addNewItemToolStripMenuItem.Click += new System.EventHandler(this.addNewItemToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 481);
            this.Controls.Add(this.spMain);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormMain";
            this.Text = "SendTo Manager";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.spMain.Panel1.ResumeLayout(false);
            this.spMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).EndInit();
            this.spMain.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chNo;
        private System.Windows.Forms.PropertyGrid pgItem;
        private System.Windows.Forms.SplitContainer spMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addNewItemToolStripMenuItem;
    }
}

