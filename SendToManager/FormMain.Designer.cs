﻿namespace SendToManager
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
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
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
            this.tsList.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.panelRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            resources.ApplyResources(this.lvMain, "lvMain");
            this.lvMain.AllowColumnReorder = true;
            this.lvMain.DoubleClickActivation = false;
            this.lvMain.FullRowSelect = true;
            this.lvMain.Name = "lvMain";
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
            // 
            // txtEditName
            // 
            resources.ApplyResources(this.txtEditName, "txtEditName");
            this.txtEditName.Name = "txtEditName";
            // 
            // cmbEditDirectory
            // 
            resources.ApplyResources(this.cmbEditDirectory, "cmbEditDirectory");
            this.cmbEditDirectory.FormattingEnabled = true;
            this.cmbEditDirectory.Name = "cmbEditDirectory";
            // 
            // tsList
            // 
            resources.ApplyResources(this.tsList, "tsList");
            this.tsList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUp,
            this.tsbDown,
            this.tsbNewItem,
            this.tsbRefresh,
            this.tsbAssignNumber,
            this.tsbDeploy,
            this.tsbDisplace});
            this.tsList.Name = "tsList";
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
            // tsbNewItem
            // 
            resources.ApplyResources(this.tsbNewItem, "tsbNewItem");
            this.tsbNewItem.Name = "tsbNewItem";
            this.tsbNewItem.Click += new System.EventHandler(this.tsbNewItem_Click);
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
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.inventoryToolStripMenuItem,
            this.folderToolStripMenuItem});
            this.menuMain.Name = "menuMain";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // closeToolStripMenuItem
            // 
            resources.ApplyResources(this.closeToolStripMenuItem, "closeToolStripMenuItem");
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            resources.ApplyResources(this.inventoryToolStripMenuItem, "inventoryToolStripMenuItem");
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.DropDownOpening += new System.EventHandler(this.inventoryToolStripMenuItem_DropDownOpening);
            // 
            // dummyToolStripMenuItem
            // 
            resources.ApplyResources(this.dummyToolStripMenuItem, "dummyToolStripMenuItem");
            this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
            // 
            // folderToolStripMenuItem
            // 
            resources.ApplyResources(this.folderToolStripMenuItem, "folderToolStripMenuItem");
            this.folderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCurrentInventoryToolStripMenuItem,
            this.openSendToToolStripMenuItem});
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            // 
            // openCurrentInventoryToolStripMenuItem
            // 
            resources.ApplyResources(this.openCurrentInventoryToolStripMenuItem, "openCurrentInventoryToolStripMenuItem");
            this.openCurrentInventoryToolStripMenuItem.Name = "openCurrentInventoryToolStripMenuItem";
            this.openCurrentInventoryToolStripMenuItem.Click += new System.EventHandler(this.openCurrentInventoryToolStripMenuItem_Click);
            // 
            // openSendToToolStripMenuItem
            // 
            resources.ApplyResources(this.openSendToToolStripMenuItem, "openSendToToolStripMenuItem");
            this.openSendToToolStripMenuItem.Name = "openSendToToolStripMenuItem";
            this.openSendToToolStripMenuItem.Click += new System.EventHandler(this.openSendToToolStripMenuItem_Click);
            // 
            // panelRoot
            // 
            resources.ApplyResources(this.panelRoot, "panelRoot");
            this.panelRoot.Controls.Add(this.lvMain);
            this.panelRoot.Controls.Add(this.tsList);
            this.panelRoot.Controls.Add(this.txtEditName);
            this.panelRoot.Controls.Add(this.cmbEditDirectory);
            this.panelRoot.Name = "panelRoot";
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRoot);
            this.Controls.Add(this.menuMain);
            this.Name = "FormMain";
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

