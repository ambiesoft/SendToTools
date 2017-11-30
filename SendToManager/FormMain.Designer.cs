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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.txtEditName = new System.Windows.Forms.TextBox();
            this.cmbEditDirectory = new System.Windows.Forms.ComboBox();
            this.tsList = new System.Windows.Forms.ToolStrip();
            this.tsbNewItem = new System.Windows.Forms.ToolStripButton();
            this.tsbUp = new System.Windows.Forms.ToolStripButton();
            this.tsbDown = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbAssignNumber = new System.Windows.Forms.ToolStripButton();
            this.tsbDeploy = new System.Windows.Forms.ToolStripButton();
            this.tsbDisplace = new System.Windows.Forms.ToolStripButton();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCurrentInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSendToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.cmbEditBool = new System.Windows.Forms.ComboBox();
            this.cmbEditFile = new System.Windows.Forms.ComboBox();
            this.lvMain = new ListViewEx.ListViewEx();
            this.ctxList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refershToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsList.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.panelRoot.SuspendLayout();
            this.ctxList.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEditName
            // 
            resources.ApplyResources(this.txtEditName, "txtEditName");
            this.txtEditName.Name = "txtEditName";
            // 
            // cmbEditDirectory
            // 
            this.cmbEditDirectory.FormattingEnabled = true;
            this.cmbEditDirectory.Items.AddRange(new object[] {
            resources.GetString("cmbEditDirectory.Items")});
            resources.ApplyResources(this.cmbEditDirectory, "cmbEditDirectory");
            this.cmbEditDirectory.Name = "cmbEditDirectory";
            this.cmbEditDirectory.SelectionChangeCommitted += new System.EventHandler(this.cmbEditDirectory_SelectionChangeCommitted);
            // 
            // tsList
            // 
            this.tsList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewItem,
            this.tsbUp,
            this.tsbDown,
            this.tsbRefresh,
            this.tsbAssignNumber,
            this.tsbDeploy,
            this.tsbDisplace});
            resources.ApplyResources(this.tsList, "tsList");
            this.tsList.Name = "tsList";
            // 
            // tsbNewItem
            // 
            resources.ApplyResources(this.tsbNewItem, "tsbNewItem");
            this.tsbNewItem.Name = "tsbNewItem";
            this.tsbNewItem.Click += new System.EventHandler(this.tsbNewItem_Click);
            // 
            // tsbUp
            // 
            resources.ApplyResources(this.tsbUp, "tsbUp");
            this.tsbUp.Name = "tsbUp";
            this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
            // 
            // tsbDown
            // 
            resources.ApplyResources(this.tsbDown, "tsbDown");
            this.tsbDown.Name = "tsbDown";
            this.tsbDown.Click += new System.EventHandler(this.tsbDown_Click);
            // 
            // tsbRefresh
            // 
            resources.ApplyResources(this.tsbRefresh, "tsbRefresh");
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbAssignNumber
            // 
            resources.ApplyResources(this.tsbAssignNumber, "tsbAssignNumber");
            this.tsbAssignNumber.Name = "tsbAssignNumber";
            this.tsbAssignNumber.Click += new System.EventHandler(this.tsbAssignNumber_Click);
            // 
            // tsbDeploy
            // 
            resources.ApplyResources(this.tsbDeploy, "tsbDeploy");
            this.tsbDeploy.Name = "tsbDeploy";
            this.tsbDeploy.Click += new System.EventHandler(this.tsbDeploy_Click);
            // 
            // tsbDisplace
            // 
            resources.ApplyResources(this.tsbDisplace, "tsbDisplace");
            this.tsbDisplace.Name = "tsbDisplace";
            this.tsbDisplace.Click += new System.EventHandler(this.tsbDisplace_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.folderToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addInventoryToolStripMenuItem,
            this.toolStripMenuItem1});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            resources.ApplyResources(this.inventoryToolStripMenuItem, "inventoryToolStripMenuItem");
            this.inventoryToolStripMenuItem.DropDownOpening += new System.EventHandler(this.inventoryToolStripMenuItem_DropDownOpening);
            // 
            // addInventoryToolStripMenuItem
            // 
            this.addInventoryToolStripMenuItem.Name = "addInventoryToolStripMenuItem";
            resources.ApplyResources(this.addInventoryToolStripMenuItem, "addInventoryToolStripMenuItem");
            this.addInventoryToolStripMenuItem.Click += new System.EventHandler(this.addInventoryToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCurrentInventoryToolStripMenuItem,
            this.openSendToToolStripMenuItem});
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            resources.ApplyResources(this.folderToolStripMenuItem, "folderToolStripMenuItem");
            // 
            // openCurrentInventoryToolStripMenuItem
            // 
            this.openCurrentInventoryToolStripMenuItem.Name = "openCurrentInventoryToolStripMenuItem";
            resources.ApplyResources(this.openCurrentInventoryToolStripMenuItem, "openCurrentInventoryToolStripMenuItem");
            this.openCurrentInventoryToolStripMenuItem.Click += new System.EventHandler(this.openCurrentInventoryToolStripMenuItem_Click);
            // 
            // openSendToToolStripMenuItem
            // 
            this.openSendToToolStripMenuItem.Name = "openSendToToolStripMenuItem";
            resources.ApplyResources(this.openSendToToolStripMenuItem, "openSendToToolStripMenuItem");
            this.openSendToToolStripMenuItem.Click += new System.EventHandler(this.openSendToToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            resources.ApplyResources(this.optionToolStripMenuItem, "optionToolStripMenuItem");
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panelRoot
            // 
            this.panelRoot.Controls.Add(this.cmbEditBool);
            this.panelRoot.Controls.Add(this.cmbEditFile);
            this.panelRoot.Controls.Add(this.cmbEditDirectory);
            this.panelRoot.Controls.Add(this.txtEditName);
            this.panelRoot.Controls.Add(this.lvMain);
            this.panelRoot.Controls.Add(this.tsList);
            resources.ApplyResources(this.panelRoot, "panelRoot");
            this.panelRoot.Name = "panelRoot";
            // 
            // cmbEditBool
            // 
            this.cmbEditBool.FormattingEnabled = true;
            this.cmbEditBool.Items.AddRange(new object[] {
            resources.GetString("cmbEditBool.Items"),
            resources.GetString("cmbEditBool.Items1")});
            resources.ApplyResources(this.cmbEditBool, "cmbEditBool");
            this.cmbEditBool.Name = "cmbEditBool";
            // 
            // cmbEditFile
            // 
            this.cmbEditFile.FormattingEnabled = true;
            this.cmbEditFile.Items.AddRange(new object[] {
            resources.GetString("cmbEditFile.Items")});
            resources.ApplyResources(this.cmbEditFile, "cmbEditFile");
            this.cmbEditFile.Name = "cmbEditFile";
            this.cmbEditFile.SelectionChangeCommitted += new System.EventHandler(this.cmbEditFile_SelectionChangeCommitted);
            // 
            // lvMain
            // 
            this.lvMain.AllowColumnReorder = true;
            this.lvMain.ContextMenuStrip = this.ctxList;
            resources.ApplyResources(this.lvMain, "lvMain");
            this.lvMain.DoubleClickActivation = false;
            this.lvMain.FullRowSelect = true;
            this.lvMain.LineAfter = -1;
            this.lvMain.LineBefore = -1;
            this.lvMain.Name = "lvMain";
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            this.lvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMain_KeyDown);
            this.lvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDown);
            this.lvMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseMove);
            this.lvMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseUp);
            // 
            // ctxList
            // 
            this.ctxList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.refershToolStripMenuItem});
            this.ctxList.Name = "ctxList";
            resources.ApplyResources(this.ctxList, "ctxList");
            // 
            // duplicateToolStripMenuItem
            // 
            this.duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            resources.ApplyResources(this.duplicateToolStripMenuItem, "duplicateToolStripMenuItem");
            this.duplicateToolStripMenuItem.Click += new System.EventHandler(this.duplicateToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // refershToolStripMenuItem
            // 
            this.refershToolStripMenuItem.Name = "refershToolStripMenuItem";
            resources.ApplyResources(this.refershToolStripMenuItem, "refershToolStripMenuItem");
            this.refershToolStripMenuItem.Click += new System.EventHandler(this.refershToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRoot);
            this.Controls.Add(this.menuMain);
            this.Name = "FormMain";
            this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tsList.ResumeLayout(false);
            this.tsList.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            this.ctxList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStrip tsList;
        private System.Windows.Forms.ToolStripButton tsbUp;
        private System.Windows.Forms.ToolStripButton tsbDown;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
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
        private System.Windows.Forms.ComboBox cmbEditFile;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmbEditBool;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip ctxList;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refershToolStripMenuItem;
    }
}

