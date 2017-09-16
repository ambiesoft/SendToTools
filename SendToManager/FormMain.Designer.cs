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
            this.lvMain = new ListViewEx.ListViewEx();
            this.txtEditName = new System.Windows.Forms.TextBox();
            this.cmbEditDirectory = new System.Windows.Forms.ComboBox();
            this.tsList = new System.Windows.Forms.ToolStrip();
            this.tsbUp = new System.Windows.Forms.ToolStripButton();
            this.tsbDown = new System.Windows.Forms.ToolStripButton();
            this.tsbNewItem = new System.Windows.Forms.ToolStripButton();
            this.tsbAssignNumber = new System.Windows.Forms.ToolStripButton();
            this.tsbDeploy = new System.Windows.Forms.ToolStripButton();
            this.tsbDisplace = new System.Windows.Forms.ToolStripButton();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCurrentInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSendToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsList.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.panelRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.AllowColumnReorder = true;
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.DoubleClickActivation = false;
            this.lvMain.FullRowSelect = true;
            this.lvMain.Location = new System.Drawing.Point(0, 25);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(789, 472);
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            // 
            // txtEditName
            // 
            this.txtEditName.Location = new System.Drawing.Point(39, 121);
            this.txtEditName.Name = "txtEditName";
            this.txtEditName.Size = new System.Drawing.Size(61, 20);
            this.txtEditName.TabIndex = 3;
            // 
            // cmbEditDirectory
            // 
            this.cmbEditDirectory.FormattingEnabled = true;
            this.cmbEditDirectory.Location = new System.Drawing.Point(0, 0);
            this.cmbEditDirectory.Name = "cmbEditDirectory";
            this.cmbEditDirectory.Size = new System.Drawing.Size(121, 21);
            this.cmbEditDirectory.TabIndex = 2;
            this.cmbEditDirectory.Visible = false;
            // 
            // tsList
            // 
            this.tsList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUp,
            this.tsbDown,
            this.tsbNewItem,
            this.tsbRefresh,
            this.tsbAssignNumber,
            this.tsbDeploy,
            this.tsbDisplace});
            this.tsList.Location = new System.Drawing.Point(0, 0);
            this.tsList.Name = "tsList";
            this.tsList.Size = new System.Drawing.Size(789, 25);
            this.tsList.TabIndex = 1;
            // 
            // tsbUp
            // 
            this.tsbUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbUp.Image")));
            this.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUp.Name = "tsbUp";
            this.tsbUp.Size = new System.Drawing.Size(43, 22);
            this.tsbUp.Text = "Up";
            this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
            // 
            // tsbDown
            // 
            this.tsbDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbDown.Image")));
            this.tsbDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDown.Name = "tsbDown";
            this.tsbDown.Size = new System.Drawing.Size(60, 22);
            this.tsbDown.Text = "Down";
            this.tsbDown.Click += new System.EventHandler(this.tsbDown_Click);
            // 
            // tsbNewItem
            // 
            this.tsbNewItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewItem.Image")));
            this.tsbNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewItem.Name = "tsbNewItem";
            this.tsbNewItem.Size = new System.Drawing.Size(86, 22);
            this.tsbNewItem.Text = "New Item";
            this.tsbNewItem.Click += new System.EventHandler(this.tsbNewItem_Click);
            // 
            // tsbAssignNumber
            // 
            this.tsbAssignNumber.Image = ((System.Drawing.Image)(resources.GetObject("tsbAssignNumber.Image")));
            this.tsbAssignNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAssignNumber.Name = "tsbAssignNumber";
            this.tsbAssignNumber.Size = new System.Drawing.Size(115, 22);
            this.tsbAssignNumber.Text = "Assign Number";
            this.tsbAssignNumber.Click += new System.EventHandler(this.tsbAssignNumber_Click);
            // 
            // tsbDeploy
            // 
            this.tsbDeploy.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeploy.Image")));
            this.tsbDeploy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeploy.Name = "tsbDeploy";
            this.tsbDeploy.Size = new System.Drawing.Size(67, 22);
            this.tsbDeploy.Text = "Deploy";
            this.tsbDeploy.Click += new System.EventHandler(this.tsbDeploy_Click);
            // 
            // tsbDisplace
            // 
            this.tsbDisplace.Image = ((System.Drawing.Image)(resources.GetObject("tsbDisplace.Image")));
            this.tsbDisplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDisplace.Name = "tsbDisplace";
            this.tsbDisplace.Size = new System.Drawing.Size(75, 22);
            this.tsbDisplace.Text = "Displace";
            this.tsbDisplace.Click += new System.EventHandler(this.tsbDisplace_Click);
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
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.inventoryToolStripMenuItem.Text = "&Inventory";
            this.inventoryToolStripMenuItem.DropDownOpening += new System.EventHandler(this.inventoryToolStripMenuItem_DropDownOpening);
            // 
            // dummyToolStripMenuItem
            // 
            this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
            this.dummyToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.dummyToolStripMenuItem.Text = "<dummy>";
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCurrentInventoryToolStripMenuItem,
            this.openSendToToolStripMenuItem});
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.folderToolStripMenuItem.Text = "&Folder";
            // 
            // openCurrentInventoryToolStripMenuItem
            // 
            this.openCurrentInventoryToolStripMenuItem.Name = "openCurrentInventoryToolStripMenuItem";
            this.openCurrentInventoryToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.openCurrentInventoryToolStripMenuItem.Text = "Open Current &Inventory";
            this.openCurrentInventoryToolStripMenuItem.Click += new System.EventHandler(this.openCurrentInventoryToolStripMenuItem_Click);
            // 
            // openSendToToolStripMenuItem
            // 
            this.openSendToToolStripMenuItem.Name = "openSendToToolStripMenuItem";
            this.openSendToToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.openSendToToolStripMenuItem.Text = "Open &SendTo";
            this.openSendToToolStripMenuItem.Click += new System.EventHandler(this.openSendToToolStripMenuItem_Click);
            // 
            // panelRoot
            // 
            this.panelRoot.Controls.Add(this.lvMain);
            this.panelRoot.Controls.Add(this.tsList);
            this.panelRoot.Controls.Add(this.txtEditName);
            this.panelRoot.Controls.Add(this.cmbEditDirectory);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 24);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(789, 497);
            this.panelRoot.TabIndex = 7;
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(71, 22);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 521);
            this.Controls.Add(this.panelRoot);
            this.Controls.Add(this.menuMain);
            this.Name = "FormMain";
            this.Text = "SendTo Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tsList.ResumeLayout(false);
            this.tsList.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.ComboBox cmbEditDirectory;
        private System.Windows.Forms.TextBox txtEditName;
        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.ToolStripButton tsbDisplace;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
    }
}

