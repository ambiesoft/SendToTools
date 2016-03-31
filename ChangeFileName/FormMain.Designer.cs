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
            this.label1 = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnTrash = new System.Windows.Forms.Button();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.btnCopyPath = new System.Windows.Forms.Button();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.menuMoveTo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aaaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmModify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpper = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrim = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.Button();
            this.cmModifySelection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamableSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLowerSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpperSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrimSel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveSpaceSel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModifySelection = new ChangeFileName.MenuButton();
            this.btnModify = new ChangeFileName.MenuButton();
            this.tsmiUnderbar2hyphen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnderbar2hyphenSel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMoveTo.SuspendLayout();
            this.cmModify.SuspendLayout();
            this.cmModifySelection.SuspendLayout();
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
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // textName
            // 
            resources.ApplyResources(this.textName, "textName");
            this.textName.Name = "textName";
            this.textName.TextChanged += new System.EventHandler(this.textName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnLaunch
            // 
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnPaste
            // 
            resources.ApplyResources(this.btnPaste, "btnPaste");
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
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
            // btnCopyPath
            // 
            resources.ApplyResources(this.btnCopyPath, "btnCopyPath");
            this.btnCopyPath.Name = "btnCopyPath";
            this.btnCopyPath.UseVisualStyleBackColor = true;
            this.btnCopyPath.Click += new System.EventHandler(this.btnCopyPath_Click);
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
            this.menuMoveTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aaaToolStripMenuItem});
            this.menuMoveTo.Name = "menuMoveTo";
            resources.ApplyResources(this.menuMoveTo, "menuMoveTo");
            // 
            // aaaToolStripMenuItem
            // 
            this.aaaToolStripMenuItem.Name = "aaaToolStripMenuItem";
            resources.ApplyResources(this.aaaToolStripMenuItem, "aaaToolStripMenuItem");
            // 
            // cmModify
            // 
            this.cmModify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamable,
            this.tsmiLower,
            this.tsmiUpper,
            this.tsmiTrim,
            this.tsmiRemoveSpace,
            this.tsmiUnderbar2hyphen});
            this.cmModify.Name = "ccmModify";
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
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cmModifySelection
            // 
            this.cmModifySelection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamableSel,
            this.tsmiLowerSel,
            this.tsmiUpperSel,
            this.tsmiTrimSel,
            this.tsmiRemoveSpaceSel,
            this.tsmiUnderbar2hyphenSel});
            this.cmModifySelection.Name = "ccmModify";
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
            // btnModifySelection
            // 
            this.btnModifySelection.ContextMenuStrip = this.cmModifySelection;
            resources.ApplyResources(this.btnModifySelection, "btnModifySelection");
            this.btnModifySelection.Name = "btnModifySelection";
            this.btnModifySelection.UseVisualStyleBackColor = true;
            // 
            // btnModify
            // 
            this.btnModify.ContextMenuStrip = this.cmModify;
            resources.ApplyResources(this.btnModify, "btnModify");
            this.btnModify.Name = "btnModify";
            this.btnModify.UseVisualStyleBackColor = true;
            // 
            // tsmiUnderbar2hyphen
            // 
            this.tsmiUnderbar2hyphen.Name = "tsmiUnderbar2hyphen";
            resources.ApplyResources(this.tsmiUnderbar2hyphen, "tsmiUnderbar2hyphen");
            this.tsmiUnderbar2hyphen.Click += new System.EventHandler(this.tsmiUnderbar2hyphen_Click);
            // 
            // tsmiUnderbar2hyphenSel
            // 
            this.tsmiUnderbar2hyphenSel.Name = "tsmiUnderbar2hyphenSel";
            resources.ApplyResources(this.tsmiUnderbar2hyphenSel, "tsmiUnderbar2hyphenSel");
            this.tsmiUnderbar2hyphenSel.Click += new System.EventHandler(this.tsmiUnderbar2hyphenSel_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnCopyPath);
            this.Controls.Add(this.btnTrash);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.btnModifySelection);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnPaste);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuMoveTo.ResumeLayout(false);
            this.cmModify.ResumeLayout(false);
            this.cmModifySelection.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.CheckBox chkAutoRun;
        private System.Windows.Forms.Button btnCopyPath;
        private System.Windows.Forms.Button btnMoveTo;
        private System.Windows.Forms.ContextMenuStrip menuMoveTo;
        private System.Windows.Forms.ToolStripMenuItem aaaToolStripMenuItem;
        private MenuButton btnModify;
        private System.Windows.Forms.ContextMenuStrip cmModify;
        private System.Windows.Forms.ToolStripMenuItem tsmiMakeFileNamable;
        private System.Windows.Forms.ToolStripMenuItem tsmiLower;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpper;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrim;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSpace;
        private System.Windows.Forms.Button btnCopy;
        private MenuButton btnModifySelection;
        private System.Windows.Forms.ContextMenuStrip cmModifySelection;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSpaceSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiMakeFileNamableSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiLowerSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpperSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrimSel;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnderbar2hyphen;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnderbar2hyphenSel;
    }
}