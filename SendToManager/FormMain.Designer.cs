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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lvMain = new System.Windows.Forms.ListView();
            this.chNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pgItem = new System.Windows.Forms.PropertyGrid();
            this.spMain = new System.Windows.Forms.SplitContainer();
            this.tsList = new System.Windows.Forms.ToolStrip();
            this.tsbUp = new System.Windows.Forms.ToolStripButton();
            this.tsbDown = new System.Windows.Forms.ToolStripButton();
            this.tsbNewItem = new System.Windows.Forms.ToolStripButton();
            this.tsbAssignNumber = new System.Windows.Forms.ToolStripButton();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCurrentInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSendToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDeploy = new System.Windows.Forms.ToolStripButton();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).BeginInit();
            this.spMain.Panel1.SuspendLayout();
            this.spMain.Panel2.SuspendLayout();
            this.spMain.SuspendLayout();
            this.tsList.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNo,
            this.chName});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.FullRowSelect = true;
            this.lvMain.Location = new System.Drawing.Point(0, 25);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(423, 472);
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            // 
            // chNo
            // 
            this.chNo.Text = "No.";
            this.chNo.Width = 144;
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
            this.pgItem.Size = new System.Drawing.Size(362, 497);
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
            this.spMain.Panel1.Controls.Add(this.tsList);
            // 
            // spMain.Panel2
            // 
            this.spMain.Panel2.Controls.Add(this.pgItem);
            this.spMain.Size = new System.Drawing.Size(789, 497);
            this.spMain.SplitterDistance = 423;
            this.spMain.TabIndex = 5;
            // 
            // tsList
            // 
            this.tsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUp,
            this.tsbDown,
            this.tsbNewItem,
            this.tsbAssignNumber,
            this.tsbDeploy});
            this.tsList.Location = new System.Drawing.Point(0, 0);
            this.tsList.Name = "tsList";
            this.tsList.Size = new System.Drawing.Size(423, 25);
            this.tsList.TabIndex = 1;
            // 
            // tsbUp
            // 
            this.tsbUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbUp.Image")));
            this.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUp.Name = "tsbUp";
            this.tsbUp.Size = new System.Drawing.Size(40, 22);
            this.tsbUp.Text = "Up";
            this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
            // 
            // tsbDown
            // 
            this.tsbDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbDown.Image")));
            this.tsbDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDown.Name = "tsbDown";
            this.tsbDown.Size = new System.Drawing.Size(54, 22);
            this.tsbDown.Text = "Down";
            this.tsbDown.Click += new System.EventHandler(this.tsbDown_Click);
            // 
            // tsbNewItem
            // 
            this.tsbNewItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewItem.Image")));
            this.tsbNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewItem.Name = "tsbNewItem";
            this.tsbNewItem.Size = new System.Drawing.Size(73, 22);
            this.tsbNewItem.Text = "New Item";
            this.tsbNewItem.Click += new System.EventHandler(this.tsbNewItem_Click);
            // 
            // tsbAssignNumber
            // 
            this.tsbAssignNumber.Image = ((System.Drawing.Image)(resources.GetObject("tsbAssignNumber.Image")));
            this.tsbAssignNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAssignNumber.Name = "tsbAssignNumber";
            this.tsbAssignNumber.Size = new System.Drawing.Size(98, 22);
            this.tsbAssignNumber.Text = "Assign Number";
            this.tsbAssignNumber.Click += new System.EventHandler(this.tsbAssignNumber_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.folderToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(789, 24);
            this.menuMain.TabIndex = 6;
            this.menuMain.Text = "menuStrip1";
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.inventoryToolStripMenuItem.Text = "&Inventory";
            this.inventoryToolStripMenuItem.DropDownOpening += new System.EventHandler(this.inventoryToolStripMenuItem_DropDownOpening);
            // 
            // dummyToolStripMenuItem
            // 
            this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
            this.dummyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dummyToolStripMenuItem.Text = "<dummy>";
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCurrentInventoryToolStripMenuItem,
            this.openSendToToolStripMenuItem});
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.folderToolStripMenuItem.Text = "&Folder";
            // 
            // openCurrentInventoryToolStripMenuItem
            // 
            this.openCurrentInventoryToolStripMenuItem.Name = "openCurrentInventoryToolStripMenuItem";
            this.openCurrentInventoryToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openCurrentInventoryToolStripMenuItem.Text = "Open Current &Inventory";
            this.openCurrentInventoryToolStripMenuItem.Click += new System.EventHandler(this.openCurrentInventoryToolStripMenuItem_Click);
            // 
            // openSendToToolStripMenuItem
            // 
            this.openSendToToolStripMenuItem.Name = "openSendToToolStripMenuItem";
            this.openSendToToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.openSendToToolStripMenuItem.Text = "Open &SendTo";
            this.openSendToToolStripMenuItem.Click += new System.EventHandler(this.openSendToToolStripMenuItem_Click);
            // 
            // tsbDeploy
            // 
            this.tsbDeploy.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeploy.Image")));
            this.tsbDeploy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeploy.Name = "tsbDeploy";
            this.tsbDeploy.Size = new System.Drawing.Size(60, 22);
            this.tsbDeploy.Text = "Deploy";
            this.tsbDeploy.Click += new System.EventHandler(this.tsbDeploy_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 521);
            this.Controls.Add(this.spMain);
            this.Controls.Add(this.menuMain);
            this.Name = "FormMain";
            this.Text = "SendTo Manager";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.spMain.Panel1.ResumeLayout(false);
            this.spMain.Panel1.PerformLayout();
            this.spMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).EndInit();
            this.spMain.ResumeLayout(false);
            this.tsList.ResumeLayout(false);
            this.tsList.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chNo;
        private System.Windows.Forms.PropertyGrid pgItem;
        private System.Windows.Forms.SplitContainer spMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStrip tsList;
        private System.Windows.Forms.ToolStripButton tsbUp;
        private System.Windows.Forms.ToolStripButton tsbDown;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbAssignNumber;
        private System.Windows.Forms.ToolStripButton tsbNewItem;
        private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCurrentInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSendToToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbDeploy;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

