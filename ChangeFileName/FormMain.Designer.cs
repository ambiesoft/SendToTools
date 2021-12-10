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
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnTrash = new System.Windows.Forms.Button();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.menuMoveTo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmReplace = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpper = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnderbar2hyphen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCn2Jp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmitoHiragana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitoKatakana = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmitoHankaku = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitoZenkaku = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmitoHankakuKana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitoZenkakuKana = new System.Windows.Forms.ToolStripMenuItem();
            this.sepBeforeCustomRegsAll = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiReplaceTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewReplaceToo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditReplaceTool = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteReplaceTool = new System.Windows.Forms.ToolStripMenuItem();
            this.dummyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReplaceAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmReplaceSelection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamableSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLowerSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpperSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrimSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveSpaceSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnderbar2hyphenSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCn2JpSel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmitoHiraganaSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitoKatakanaSel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmitoHankakuSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitoZenkakuSel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmitoHankakuKanaSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitoZenkakuKanaSel = new System.Windows.Forms.ToolStripMenuItem();
            this.sepBeforeCustomRegsSelection = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDummy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReplaceSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTotailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSmartDoubleClickSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveLastSelectionFolderToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmsBeforeTools = new System.Windows.Forms.ToolStripSeparator();
            this.tsmsAfterTools = new System.Windows.Forms.ToolStripSeparator();
            this.addModifyToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToWebPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteTotailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblExtention = new System.Windows.Forms.Label();
            this.cmLaunch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRevealInFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLaunch = new SplitButtonDemo.SplitButton();
            this.btnPaste = new SplitButtonDemo.SplitButton();
            this.cmReplace.SuspendLayout();
            this.cmReplaceSelection.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.cmPaste.SuspendLayout();
            this.tlpInfo.SuspendLayout();
            this.cmLaunch.SuspendLayout();
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
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtName_MouseClick);
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtName_MouseDoubleClick);
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
            resources.ApplyResources(this.menuMoveTo, "menuMoveTo");
            this.menuMoveTo.Name = "menuMoveTo";
            // 
            // cmReplace
            // 
            resources.ApplyResources(this.cmReplace, "cmReplace");
            this.cmReplace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamable,
            this.tsmiLower,
            this.tsmiUpper,
            this.tsmiTrim,
            this.tsmiRemoveSpace,
            this.tsmiUnderbar2hyphen,
            this.tsmiCn2Jp,
            this.toolStripMenuItem3,
            this.tsmitoHiragana,
            this.tsmitoKatakana,
            this.toolStripMenuItem6,
            this.tsmitoHankaku,
            this.tsmitoZenkaku,
            this.toolStripMenuItem7,
            this.tsmitoHankakuKana,
            this.tsmitoZenkakuKana,
            this.sepBeforeCustomRegsAll,
            this.tsmiReplaceTool});
            this.cmReplace.Name = "cmReplace";
            // 
            // tsmiMakeFileNamable
            // 
            resources.ApplyResources(this.tsmiMakeFileNamable, "tsmiMakeFileNamable");
            this.tsmiMakeFileNamable.Name = "tsmiMakeFileNamable";
            this.tsmiMakeFileNamable.Click += new System.EventHandler(this.ToFileNamable_Click);
            // 
            // tsmiLower
            // 
            resources.ApplyResources(this.tsmiLower, "tsmiLower");
            this.tsmiLower.Name = "tsmiLower";
            this.tsmiLower.Click += new System.EventHandler(this.ToLower_Click);
            // 
            // tsmiUpper
            // 
            resources.ApplyResources(this.tsmiUpper, "tsmiUpper");
            this.tsmiUpper.Name = "tsmiUpper";
            this.tsmiUpper.Click += new System.EventHandler(this.ToUpper_Click);
            // 
            // tsmiTrim
            // 
            resources.ApplyResources(this.tsmiTrim, "tsmiTrim");
            this.tsmiTrim.Name = "tsmiTrim";
            this.tsmiTrim.Click += new System.EventHandler(this.Trim_Click);
            // 
            // tsmiRemoveSpace
            // 
            resources.ApplyResources(this.tsmiRemoveSpace, "tsmiRemoveSpace");
            this.tsmiRemoveSpace.Name = "tsmiRemoveSpace";
            this.tsmiRemoveSpace.Click += new System.EventHandler(this.ToRemoveSpace_Click);
            // 
            // tsmiUnderbar2hyphen
            // 
            resources.ApplyResources(this.tsmiUnderbar2hyphen, "tsmiUnderbar2hyphen");
            this.tsmiUnderbar2hyphen.Name = "tsmiUnderbar2hyphen";
            this.tsmiUnderbar2hyphen.Click += new System.EventHandler(this.tsmiUnderbar2hyphen_Click);
            // 
            // tsmiCn2Jp
            // 
            resources.ApplyResources(this.tsmiCn2Jp, "tsmiCn2Jp");
            this.tsmiCn2Jp.Name = "tsmiCn2Jp";
            this.tsmiCn2Jp.Click += new System.EventHandler(this.tsmiCn2Jp_Click);
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // tsmitoHiragana
            // 
            resources.ApplyResources(this.tsmitoHiragana, "tsmitoHiragana");
            this.tsmitoHiragana.Name = "tsmitoHiragana";
            this.tsmitoHiragana.Click += new System.EventHandler(this.tsmitoHiragana_Click);
            // 
            // tsmitoKatakana
            // 
            resources.ApplyResources(this.tsmitoKatakana, "tsmitoKatakana");
            this.tsmitoKatakana.Name = "tsmitoKatakana";
            this.tsmitoKatakana.Click += new System.EventHandler(this.tsmitoKatakana_Click);
            // 
            // toolStripMenuItem6
            // 
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            // 
            // tsmitoHankaku
            // 
            resources.ApplyResources(this.tsmitoHankaku, "tsmitoHankaku");
            this.tsmitoHankaku.Name = "tsmitoHankaku";
            this.tsmitoHankaku.Click += new System.EventHandler(this.tsmitoHankaku_Click);
            // 
            // tsmitoZenkaku
            // 
            resources.ApplyResources(this.tsmitoZenkaku, "tsmitoZenkaku");
            this.tsmitoZenkaku.Name = "tsmitoZenkaku";
            this.tsmitoZenkaku.Click += new System.EventHandler(this.tsmitoZenkaku_Click);
            // 
            // toolStripMenuItem7
            // 
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            // 
            // tsmitoHankakuKana
            // 
            resources.ApplyResources(this.tsmitoHankakuKana, "tsmitoHankakuKana");
            this.tsmitoHankakuKana.Name = "tsmitoHankakuKana";
            this.tsmitoHankakuKana.Click += new System.EventHandler(this.tsmitoHankakuKana_Click);
            // 
            // tsmitoZenkakuKana
            // 
            resources.ApplyResources(this.tsmitoZenkakuKana, "tsmitoZenkakuKana");
            this.tsmitoZenkakuKana.Name = "tsmitoZenkakuKana";
            this.tsmitoZenkakuKana.Click += new System.EventHandler(this.tsmitoZenkakuKana_Click);
            // 
            // sepBeforeCustomRegsAll
            // 
            resources.ApplyResources(this.sepBeforeCustomRegsAll, "sepBeforeCustomRegsAll");
            this.sepBeforeCustomRegsAll.Name = "sepBeforeCustomRegsAll";
            // 
            // tsmiReplaceTool
            // 
            resources.ApplyResources(this.tsmiReplaceTool, "tsmiReplaceTool");
            this.tsmiReplaceTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewReplaceToo,
            this.tsmiEditReplaceTool,
            this.tsmiDeleteReplaceTool});
            this.tsmiReplaceTool.Name = "tsmiReplaceTool";
            // 
            // tsmiNewReplaceToo
            // 
            resources.ApplyResources(this.tsmiNewReplaceToo, "tsmiNewReplaceToo");
            this.tsmiNewReplaceToo.Name = "tsmiNewReplaceToo";
            this.tsmiNewReplaceToo.Click += new System.EventHandler(this.tsmiNewReplaceToo_Click);
            // 
            // tsmiEditReplaceTool
            // 
            resources.ApplyResources(this.tsmiEditReplaceTool, "tsmiEditReplaceTool");
            this.tsmiEditReplaceTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem});
            this.tsmiEditReplaceTool.Name = "tsmiEditReplaceTool";
            this.tsmiEditReplaceTool.DropDownOpening += new System.EventHandler(this.tsmiEditReplaceTool_DropDownOpening);
            // 
            // dummyToolStripMenuItem
            // 
            resources.ApplyResources(this.dummyToolStripMenuItem, "dummyToolStripMenuItem");
            this.dummyToolStripMenuItem.Name = "dummyToolStripMenuItem";
            // 
            // tsmiDeleteReplaceTool
            // 
            resources.ApplyResources(this.tsmiDeleteReplaceTool, "tsmiDeleteReplaceTool");
            this.tsmiDeleteReplaceTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dummyToolStripMenuItem1});
            this.tsmiDeleteReplaceTool.Name = "tsmiDeleteReplaceTool";
            this.tsmiDeleteReplaceTool.DropDownOpening += new System.EventHandler(this.tsmiDeleteReplaceTool_DropDownOpening);
            // 
            // dummyToolStripMenuItem1
            // 
            resources.ApplyResources(this.dummyToolStripMenuItem1, "dummyToolStripMenuItem1");
            this.dummyToolStripMenuItem1.Name = "dummyToolStripMenuItem1";
            // 
            // tsmiReplaceAll
            // 
            resources.ApplyResources(this.tsmiReplaceAll, "tsmiReplaceAll");
            this.tsmiReplaceAll.DropDown = this.cmReplace;
            this.tsmiReplaceAll.Name = "tsmiReplaceAll";
            this.tsmiReplaceAll.DropDownOpening += new System.EventHandler(this.tsmiReplaceAll_DropDownOpening);
            // 
            // cmReplaceSelection
            // 
            resources.ApplyResources(this.cmReplaceSelection, "cmReplaceSelection");
            this.cmReplaceSelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamableSel,
            this.tsmiLowerSel,
            this.tsmiUpperSel,
            this.tsmiTrimSel,
            this.tsmiRemoveSpaceSel,
            this.tsmiUnderbar2hyphenSel,
            this.tsmiCn2JpSel,
            this.toolStripMenuItem4,
            this.tsmitoHiraganaSel,
            this.tsmitoKatakanaSel,
            this.toolStripMenuItem5,
            this.tsmitoHankakuSel,
            this.tsmitoZenkakuSel,
            this.toolStripMenuItem8,
            this.tsmitoHankakuKanaSel,
            this.tsmitoZenkakuKanaSel,
            this.sepBeforeCustomRegsSelection,
            this.tsmiDummy});
            this.cmReplaceSelection.Name = "cmReplace";
            this.cmReplaceSelection.OwnerItem = this.tsmiReplaceSelection;
            // 
            // tsmiMakeFileNamableSel
            // 
            resources.ApplyResources(this.tsmiMakeFileNamableSel, "tsmiMakeFileNamableSel");
            this.tsmiMakeFileNamableSel.Name = "tsmiMakeFileNamableSel";
            this.tsmiMakeFileNamableSel.Click += new System.EventHandler(this.ToMakeFileNamableSel_Click);
            // 
            // tsmiLowerSel
            // 
            resources.ApplyResources(this.tsmiLowerSel, "tsmiLowerSel");
            this.tsmiLowerSel.Name = "tsmiLowerSel";
            this.tsmiLowerSel.Click += new System.EventHandler(this.ToLowerSel_Click);
            // 
            // tsmiUpperSel
            // 
            resources.ApplyResources(this.tsmiUpperSel, "tsmiUpperSel");
            this.tsmiUpperSel.Name = "tsmiUpperSel";
            this.tsmiUpperSel.Click += new System.EventHandler(this.ToUpperSel_Click);
            // 
            // tsmiTrimSel
            // 
            resources.ApplyResources(this.tsmiTrimSel, "tsmiTrimSel");
            this.tsmiTrimSel.Name = "tsmiTrimSel";
            this.tsmiTrimSel.Click += new System.EventHandler(this.TrimSel_Click);
            // 
            // tsmiRemoveSpaceSel
            // 
            resources.ApplyResources(this.tsmiRemoveSpaceSel, "tsmiRemoveSpaceSel");
            this.tsmiRemoveSpaceSel.Name = "tsmiRemoveSpaceSel";
            this.tsmiRemoveSpaceSel.Click += new System.EventHandler(this.ToRemoveSpaceSel_Click);
            // 
            // tsmiUnderbar2hyphenSel
            // 
            resources.ApplyResources(this.tsmiUnderbar2hyphenSel, "tsmiUnderbar2hyphenSel");
            this.tsmiUnderbar2hyphenSel.Name = "tsmiUnderbar2hyphenSel";
            this.tsmiUnderbar2hyphenSel.Click += new System.EventHandler(this.tsmiUnderbar2hyphenSel_Click);
            // 
            // tsmiCn2JpSel
            // 
            resources.ApplyResources(this.tsmiCn2JpSel, "tsmiCn2JpSel");
            this.tsmiCn2JpSel.Name = "tsmiCn2JpSel";
            this.tsmiCn2JpSel.Click += new System.EventHandler(this.tsmiCn2JpSel_Click);
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // tsmitoHiraganaSel
            // 
            resources.ApplyResources(this.tsmitoHiraganaSel, "tsmitoHiraganaSel");
            this.tsmitoHiraganaSel.Name = "tsmitoHiraganaSel";
            this.tsmitoHiraganaSel.Click += new System.EventHandler(this.tsmitoHiraganaSel_Click);
            // 
            // tsmitoKatakanaSel
            // 
            resources.ApplyResources(this.tsmitoKatakanaSel, "tsmitoKatakanaSel");
            this.tsmitoKatakanaSel.Name = "tsmitoKatakanaSel";
            this.tsmitoKatakanaSel.Click += new System.EventHandler(this.tsmitoKatakanaSel_Click);
            // 
            // toolStripMenuItem5
            // 
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            // 
            // tsmitoHankakuSel
            // 
            resources.ApplyResources(this.tsmitoHankakuSel, "tsmitoHankakuSel");
            this.tsmitoHankakuSel.Name = "tsmitoHankakuSel";
            this.tsmitoHankakuSel.Click += new System.EventHandler(this.tsmitoHankakuSel_Click);
            // 
            // tsmitoZenkakuSel
            // 
            resources.ApplyResources(this.tsmitoZenkakuSel, "tsmitoZenkakuSel");
            this.tsmitoZenkakuSel.Name = "tsmitoZenkakuSel";
            this.tsmitoZenkakuSel.Click += new System.EventHandler(this.tsmitoZenkakuSel_Click);
            // 
            // toolStripMenuItem8
            // 
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            // 
            // tsmitoHankakuKanaSel
            // 
            resources.ApplyResources(this.tsmitoHankakuKanaSel, "tsmitoHankakuKanaSel");
            this.tsmitoHankakuKanaSel.Name = "tsmitoHankakuKanaSel";
            this.tsmitoHankakuKanaSel.Click += new System.EventHandler(this.tsmitoHankakuKanaSel_Click);
            // 
            // tsmitoZenkakuKanaSel
            // 
            resources.ApplyResources(this.tsmitoZenkakuKanaSel, "tsmitoZenkakuKanaSel");
            this.tsmitoZenkakuKanaSel.Name = "tsmitoZenkakuKanaSel";
            this.tsmitoZenkakuKanaSel.Click += new System.EventHandler(this.tsmitoZenkakuKanaSel_Click);
            // 
            // sepBeforeCustomRegsSelection
            // 
            resources.ApplyResources(this.sepBeforeCustomRegsSelection, "sepBeforeCustomRegsSelection");
            this.sepBeforeCustomRegsSelection.Name = "sepBeforeCustomRegsSelection";
            // 
            // tsmiDummy
            // 
            resources.ApplyResources(this.tsmiDummy, "tsmiDummy");
            this.tsmiDummy.Name = "tsmiDummy";
            // 
            // tsmiReplaceSelection
            // 
            resources.ApplyResources(this.tsmiReplaceSelection, "tsmiReplaceSelection");
            this.tsmiReplaceSelection.DropDown = this.cmReplaceSelection;
            this.tsmiReplaceSelection.Name = "tsmiReplaceSelection";
            this.tsmiReplaceSelection.DropDownOpening += new System.EventHandler(this.tsmiReplaceSelection_DropDownOpening);
            // 
            // lblFileInfo
            // 
            resources.ApplyResources(this.lblFileInfo, "lblFileInfo");
            this.lblFileInfo.Name = "lblFileInfo";
            // 
            // menuMain
            // 
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.tsmiReplaceAll,
            this.tsmiReplaceSelection,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Name = "menuMain";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExplorerToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // openInExplorerToolStripMenuItem
            // 
            resources.ApplyResources(this.openInExplorerToolStripMenuItem, "openInExplorerToolStripMenuItem");
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.btnExplorer_Click);
            // 
            // editToolStripMenuItem
            // 
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.copyToolStripMenuItem,
            this.copyPathToolStripMenuItem,
            this.toolStripMenuItem1,
            this.pasteToolStripMenuItem,
            this.pasteTotailToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            // 
            // undoToolStripMenuItem
            // 
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // copyPathToolStripMenuItem
            // 
            resources.ApplyResources(this.copyPathToolStripMenuItem, "copyPathToolStripMenuItem");
            this.copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            this.copyPathToolStripMenuItem.Click += new System.EventHandler(this.btnCopyPath_Click);
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // pasteToolStripMenuItem
            // 
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // pasteTotailToolStripMenuItem
            // 
            resources.ApplyResources(this.pasteTotailToolStripMenuItem, "pasteTotailToolStripMenuItem");
            this.pasteTotailToolStripMenuItem.Name = "pasteTotailToolStripMenuItem";
            this.pasteTotailToolStripMenuItem.Click += new System.EventHandler(this.pasteTotailToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOptions,
            this.tsmsBeforeTools,
            this.tsmsAfterTools,
            this.addModifyToolToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.toolsToolStripMenuItem_DropDownOpening);
            // 
            // tsmiOptions
            // 
            resources.ApplyResources(this.tsmiOptions, "tsmiOptions");
            this.tsmiOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSmartDoubleClickSelection,
            this.tsmiMoveLastSelectionFolderToTop});
            this.tsmiOptions.Name = "tsmiOptions";
            // 
            // tsmiSmartDoubleClickSelection
            // 
            resources.ApplyResources(this.tsmiSmartDoubleClickSelection, "tsmiSmartDoubleClickSelection");
            this.tsmiSmartDoubleClickSelection.CheckOnClick = true;
            this.tsmiSmartDoubleClickSelection.Name = "tsmiSmartDoubleClickSelection";
            // 
            // tsmiMoveLastSelectionFolderToTop
            // 
            resources.ApplyResources(this.tsmiMoveLastSelectionFolderToTop, "tsmiMoveLastSelectionFolderToTop");
            this.tsmiMoveLastSelectionFolderToTop.CheckOnClick = true;
            this.tsmiMoveLastSelectionFolderToTop.Name = "tsmiMoveLastSelectionFolderToTop";
            // 
            // tsmsBeforeTools
            // 
            resources.ApplyResources(this.tsmsBeforeTools, "tsmsBeforeTools");
            this.tsmsBeforeTools.Name = "tsmsBeforeTools";
            // 
            // tsmsAfterTools
            // 
            resources.ApplyResources(this.tsmsAfterTools, "tsmsAfterTools");
            this.tsmsAfterTools.Name = "tsmsAfterTools";
            // 
            // addModifyToolToolStripMenuItem
            // 
            resources.ApplyResources(this.addModifyToolToolStripMenuItem, "addModifyToolToolStripMenuItem");
            this.addModifyToolToolStripMenuItem.Name = "addModifyToolToolStripMenuItem";
            this.addModifyToolToolStripMenuItem.Click += new System.EventHandler(this.addModifyToolToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            resources.ApplyResources(this.windowToolStripMenuItem, "windowToolStripMenuItem");
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            resources.ApplyResources(this.alwaysOnTopToolStripMenuItem, "alwaysOnTopToolStripMenuItem");
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.goToWebPageToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // aboutToolStripMenuItem
            // 
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // goToWebPageToolStripMenuItem
            // 
            resources.ApplyResources(this.goToWebPageToolStripMenuItem, "goToWebPageToolStripMenuItem");
            this.goToWebPageToolStripMenuItem.Name = "goToWebPageToolStripMenuItem";
            this.goToWebPageToolStripMenuItem.Click += new System.EventHandler(this.goToWebPageToolStripMenuItem_Click);
            // 
            // cmPaste
            // 
            resources.ApplyResources(this.cmPaste, "cmPaste");
            this.cmPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteTotailToolStripMenuItem1});
            this.cmPaste.Name = "cmPaste";
            // 
            // pasteTotailToolStripMenuItem1
            // 
            resources.ApplyResources(this.pasteTotailToolStripMenuItem1, "pasteTotailToolStripMenuItem1");
            this.pasteTotailToolStripMenuItem1.Name = "pasteTotailToolStripMenuItem1";
            this.pasteTotailToolStripMenuItem1.Click += new System.EventHandler(this.pasteTotailToolStripMenuItem_Click);
            // 
            // tlpInfo
            // 
            resources.ApplyResources(this.tlpInfo, "tlpInfo");
            this.tlpInfo.Controls.Add(this.lblFileInfo, 0, 0);
            this.tlpInfo.Controls.Add(this.lblExtention, 1, 0);
            this.tlpInfo.Name = "tlpInfo";
            // 
            // lblExtention
            // 
            resources.ApplyResources(this.lblExtention, "lblExtention");
            this.lblExtention.Name = "lblExtention";
            // 
            // cmLaunch
            // 
            resources.ApplyResources(this.cmLaunch, "cmLaunch");
            this.cmLaunch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRevealInFolder});
            this.cmLaunch.Name = "cmLaunch";
            // 
            // tsmiRevealInFolder
            // 
            resources.ApplyResources(this.tsmiRevealInFolder, "tsmiRevealInFolder");
            this.tsmiRevealInFolder.Name = "tsmiRevealInFolder";
            this.tsmiRevealInFolder.Click += new System.EventHandler(this.tsmiRevealInFolder_Click);
            // 
            // btnLaunch
            // 
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.ContextMenuStrip = this.cmLaunch;
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.ButtonClick += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnPaste
            // 
            resources.ApplyResources(this.btnPaste, "btnPaste");
            this.btnPaste.ContextMenuStrip = this.cmPaste;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.ButtonClick += new System.EventHandler(this.btnPaste_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.tlpInfo);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTrash);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.btnOK);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.cmReplace.ResumeLayout(false);
            this.cmReplaceSelection.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.cmPaste.ResumeLayout(false);
            this.tlpInfo.ResumeLayout(false);
            this.tlpInfo.PerformLayout();
            this.cmLaunch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.CheckBox chkAutoRun;
        private System.Windows.Forms.Button btnMoveTo;
        private System.Windows.Forms.ContextMenuStrip menuMoveTo;
        private System.Windows.Forms.ContextMenuStrip cmReplace;
        private System.Windows.Forms.ToolStripMenuItem tsmiMakeFileNamable;
        private System.Windows.Forms.ToolStripMenuItem tsmiLower;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpper;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrim;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSpace;
        private System.Windows.Forms.ContextMenuStrip cmReplaceSelection;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiReplaceAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiReplaceSelection;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private SplitButtonDemo.SplitButton btnPaste;
        private System.Windows.Forms.ContextMenuStrip cmPaste;
        private System.Windows.Forms.ToolStripMenuItem pasteTotailToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModifyToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToWebPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmitoHiragana;
        private System.Windows.Forms.ToolStripMenuItem tsmitoHiraganaSel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmitoKatakana;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmitoKatakanaSel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tsmitoHankaku;
        private System.Windows.Forms.ToolStripMenuItem tsmitoZenkaku;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmitoHankakuSel;
        private System.Windows.Forms.ToolStripMenuItem tsmitoZenkakuSel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem tsmitoHankakuKana;
        private System.Windows.Forms.ToolStripMenuItem tsmitoZenkakuKana;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem tsmitoHankakuKanaSel;
        private System.Windows.Forms.ToolStripMenuItem tsmitoZenkakuKanaSel;
        private System.Windows.Forms.TableLayoutPanel tlpInfo;
        private System.Windows.Forms.Label lblExtention;
        private System.Windows.Forms.ToolStripSeparator tsmsBeforeTools;
        private System.Windows.Forms.ToolStripSeparator tsmsAfterTools;
        private SplitButtonDemo.SplitButton btnLaunch;
        private System.Windows.Forms.ContextMenuStrip cmLaunch;
        private System.Windows.Forms.ToolStripMenuItem tsmiRevealInFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiSmartDoubleClickSelection;
        private System.Windows.Forms.ToolStripMenuItem tsmiMoveLastSelectionFolderToTop;
        private System.Windows.Forms.ToolStripSeparator sepBeforeCustomRegsAll;
        private System.Windows.Forms.ToolStripSeparator sepBeforeCustomRegsSelection;
        private System.Windows.Forms.ToolStripMenuItem tsmiDummy;
        private System.Windows.Forms.ToolStripMenuItem tsmiReplaceTool;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewReplaceToo;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditReplaceTool;
        private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteReplaceTool;
        private System.Windows.Forms.ToolStripMenuItem dummyToolStripMenuItem1;
    }
}