namespace ChangeFileName
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.textName = new System.Windows.Forms.TextBox();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnTrash = new System.Windows.Forms.Button();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.menuMoveTo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmModify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpper = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnderbar2hyphen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCn2Jp = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmModifySelection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamableSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLowerSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpperSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrimSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveSpaceSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnderbar2hyphenSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCn2JpSel = new System.Windows.Forms.ToolStripMenuItem();
            this.modifySelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTotailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPaste = new System.Windows.Forms.Button();
            this.cmModify.SuspendLayout();
            this.cmModifySelection.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // textName
            // 
            resources.ApplyResources(this.textName, "textName");
            this.textName.Name = "textName";
            this.textName.TextChanged += new System.EventHandler(this.textName_TextChanged);
            // 
            // btnLaunch
            // 
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnTrash
            // 
            resources.ApplyResources(this.btnTrash, "btnTrash");
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.UseVisualStyleBackColor = true;
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // chkAutoRun
            // 
            resources.ApplyResources(this.chkAutoRun, "chkAutoRun");
            this.chkAutoRun.Name = "chkAutoRun";
            this.chkAutoRun.UseVisualStyleBackColor = true;
            this.chkAutoRun.CheckedChanged += new System.EventHandler(this.chkAutoRun_CheckedChanged);
            // 
            // btnMoveTo
            // 
            resources.ApplyResources(this.btnMoveTo, "btnMoveTo");
            this.btnMoveTo.Name = "btnMoveTo";
            this.btnMoveTo.UseVisualStyleBackColor = true;
            this.btnMoveTo.Click += new System.EventHandler(this.btnMoveTo_Click);
            // 
            // menuMoveTo
            // 
            this.menuMoveTo.Name = "menuMoveTo";
            resources.ApplyResources(this.menuMoveTo, "menuMoveTo");
            // 
            // cmModify
            // 
            this.cmModify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamable,
            this.tsmiLower,
            this.tsmiUpper,
            this.tsmiTrim,
            this.tsmiRemoveSpace,
            this.tsmiUnderbar2hyphen,
            this.tsmiCn2Jp});
            this.cmModify.Name = "ccmModify";
            this.cmModify.OwnerItem = this.modifyToolStripMenuItem;
            resources.ApplyResources(this.cmModify, "cmModify");
            // 
            // tsmiMakeFileNamable
            // 
            this.tsmiMakeFileNamable.Name = "tsmiMakeFileNamable";
            resources.ApplyResources(this.tsmiMakeFileNamable, "tsmiMakeFileNamable");
            this.tsmiMakeFileNamable.Click += new System.EventHandler(this.ToFileNamable_Click);
            // 
            // tsmiLower
            // 
            this.tsmiLower.Name = "tsmiLower";
            resources.ApplyResources(this.tsmiLower, "tsmiLower");
            this.tsmiLower.Click += new System.EventHandler(this.ToLower_Click);
            // 
            // tsmiUpper
            // 
            this.tsmiUpper.Name = "tsmiUpper";
            resources.ApplyResources(this.tsmiUpper, "tsmiUpper");
            this.tsmiUpper.Click += new System.EventHandler(this.ToUpper_Click);
            // 
            // tsmiTrim
            // 
            this.tsmiTrim.Name = "tsmiTrim";
            resources.ApplyResources(this.tsmiTrim, "tsmiTrim");
            this.tsmiTrim.Click += new System.EventHandler(this.Trim_Click);
            // 
            // tsmiRemoveSpace
            // 
            this.tsmiRemoveSpace.Name = "tsmiRemoveSpace";
            resources.ApplyResources(this.tsmiRemoveSpace, "tsmiRemoveSpace");
            this.tsmiRemoveSpace.Click += new System.EventHandler(this.ToRemoveSpace_Click);
            // 
            // tsmiUnderbar2hyphen
            // 
            this.tsmiUnderbar2hyphen.Name = "tsmiUnderbar2hyphen";
            resources.ApplyResources(this.tsmiUnderbar2hyphen, "tsmiUnderbar2hyphen");
            this.tsmiUnderbar2hyphen.Click += new System.EventHandler(this.tsmiUnderbar2hyphen_Click);
            // 
            // tsmiCn2Jp
            // 
            this.tsmiCn2Jp.Name = "tsmiCn2Jp";
            resources.ApplyResources(this.tsmiCn2Jp, "tsmiCn2Jp");
            this.tsmiCn2Jp.Click += new System.EventHandler(this.tsmiCn2Jp_Click);
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.DropDown = this.cmModify;
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            resources.ApplyResources(this.modifyToolStripMenuItem, "modifyToolStripMenuItem");
            // 
            // cmModifySelection
            // 
            this.cmModifySelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamableSel,
            this.tsmiLowerSel,
            this.tsmiUpperSel,
            this.tsmiTrimSel,
            this.tsmiRemoveSpaceSel,
            this.tsmiUnderbar2hyphenSel,
            this.tsmiCn2JpSel});
            this.cmModifySelection.Name = "ccmModify";
            this.cmModifySelection.OwnerItem = this.modifySelectionToolStripMenuItem;
            resources.ApplyResources(this.cmModifySelection, "cmModifySelection");
            // 
            // tsmiMakeFileNamableSel
            // 
            this.tsmiMakeFileNamableSel.Name = "tsmiMakeFileNamableSel";
            resources.ApplyResources(this.tsmiMakeFileNamableSel, "tsmiMakeFileNamableSel");
            this.tsmiMakeFileNamableSel.Click += new System.EventHandler(this.ToMakeFileNamableSel_Click);
            // 
            // tsmiLowerSel
            // 
            this.tsmiLowerSel.Name = "tsmiLowerSel";
            resources.ApplyResources(this.tsmiLowerSel, "tsmiLowerSel");
            this.tsmiLowerSel.Click += new System.EventHandler(this.ToLowerSel_Click);
            // 
            // tsmiUpperSel
            // 
            this.tsmiUpperSel.Name = "tsmiUpperSel";
            resources.ApplyResources(this.tsmiUpperSel, "tsmiUpperSel");
            this.tsmiUpperSel.Click += new System.EventHandler(this.ToUpperSel_Click);
            // 
            // tsmiTrimSel
            // 
            this.tsmiTrimSel.Name = "tsmiTrimSel";
            resources.ApplyResources(this.tsmiTrimSel, "tsmiTrimSel");
            this.tsmiTrimSel.Click += new System.EventHandler(this.TrimSel_Click);
            // 
            // tsmiRemoveSpaceSel
            // 
            this.tsmiRemoveSpaceSel.Name = "tsmiRemoveSpaceSel";
            resources.ApplyResources(this.tsmiRemoveSpaceSel, "tsmiRemoveSpaceSel");
            this.tsmiRemoveSpaceSel.Click += new System.EventHandler(this.ToRemoveSpaceSel_Click);
            // 
            // tsmiUnderbar2hyphenSel
            // 
            this.tsmiUnderbar2hyphenSel.Name = "tsmiUnderbar2hyphenSel";
            resources.ApplyResources(this.tsmiUnderbar2hyphenSel, "tsmiUnderbar2hyphenSel");
            this.tsmiUnderbar2hyphenSel.Click += new System.EventHandler(this.tsmiUnderbar2hyphenSel_Click);
            // 
            // tsmiCn2JpSel
            // 
            this.tsmiCn2JpSel.Name = "tsmiCn2JpSel";
            resources.ApplyResources(this.tsmiCn2JpSel, "tsmiCn2JpSel");
            this.tsmiCn2JpSel.Click += new System.EventHandler(this.tsmiCn2JpSel_Click);
            // 
            // modifySelectionToolStripMenuItem
            // 
            this.modifySelectionToolStripMenuItem.DropDown = this.cmModifySelection;
            this.modifySelectionToolStripMenuItem.Name = "modifySelectionToolStripMenuItem";
            resources.ApplyResources(this.modifySelectionToolStripMenuItem, "modifySelectionToolStripMenuItem");
            // 
            // lblFileInfo
            // 
            resources.ApplyResources(this.lblFileInfo, "lblFileInfo");
            this.lblFileInfo.Name = "lblFileInfo";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.modifyToolStripMenuItem,
            this.modifySelectionToolStripMenuItem,
            this.windowToolStripMenuItem});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExplorerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            resources.ApplyResources(this.openInExplorerToolStripMenuItem, "openInExplorerToolStripMenuItem");
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.btnExplorer_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.copyPathToolStripMenuItem,
            this.toolStripMenuItem1,
            this.pasteToolStripMenuItem,
            this.pasteTotailToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // copyPathToolStripMenuItem
            // 
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            resources.ApplyResources(this.copyPathToolStripMenuItem, "copyPathToolStripMenuItem");
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.btnCopyPath_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // pasteTotailToolStripMenuItem
            // 
            this.pasteTotailToolStripMenuItem.Name = "pasteTotailToolStripMenuItem";
            resources.ApplyResources(this.pasteTotailToolStripMenuItem, "pasteTotailToolStripMenuItem");
            this.pasteTotailToolStripMenuItem.Click += new System.EventHandler(this.pasteTotailToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            resources.ApplyResources(this.alwaysOnTopToolStripMenuItem, "alwaysOnTopToolStripMenuItem");
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // btnPaste
            // 
            resources.ApplyResources(this.btnPaste, "btnPaste");
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.lblFileInfo);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnTrash);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.cmModify.ResumeLayout(false);
            this.cmModifySelection.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.CheckBox chkAutoRun;
        private System.Windows.Forms.Button btnMoveTo;
        private System.Windows.Forms.ContextMenuStrip menuMoveTo;
        private System.Windows.Forms.ContextMenuStrip cmModify;
        private System.Windows.Forms.ToolStripMenuItem tsmiMakeFileNamable;
        private System.Windows.Forms.ToolStripMenuItem tsmiLower;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpper;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrim;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSpace;
        private System.Windows.Forms.ContextMenuStrip cmModifySelection;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSpaceSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiMakeFileNamableSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiLowerSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpperSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrimSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnderbar2hyphen;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnderbar2hyphenSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiCn2Jp;
        private System.Windows.Forms.ToolStripMenuItem tsmiCn2JpSel;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteTotailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifySelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Button btnPaste;
    }
}