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
            this.btnTrim = new System.Windows.Forms.Button();
            this.btnTrash = new System.Windows.Forms.Button();
            this.btnToLower = new System.Windows.Forms.Button();
            this.btnToUpper = new System.Windows.Forms.Button();
            this.btnFN = new System.Windows.Forms.Button();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.btnCopyPath = new System.Windows.Forms.Button();
            this.btnMoveTo = new System.Windows.Forms.Button();
            this.menuMoveTo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aaaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmModify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMakeFileNamable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpper = new System.Windows.Forms.ToolStripMenuItem();
            this.trimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnModify = new ChangeFileName.MenuButton();
            this.menuMoveTo.SuspendLayout();
            this.cmModify.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = null;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // textName
            // 
            this.textName.AccessibleDescription = null;
            this.textName.AccessibleName = null;
            resources.ApplyResources(this.textName, "textName");
            this.textName.BackgroundImage = null;
            this.textName.Font = null;
            this.textName.Name = "textName";
            this.textName.TextChanged += new System.EventHandler(this.textName_TextChanged);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // btnLaunch
            // 
            this.btnLaunch.AccessibleDescription = null;
            this.btnLaunch.AccessibleName = null;
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.BackgroundImage = null;
            this.btnLaunch.Font = null;
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.AccessibleDescription = null;
            this.btnPaste.AccessibleName = null;
            resources.ApplyResources(this.btnPaste, "btnPaste");
            this.btnPaste.BackgroundImage = null;
            this.btnPaste.Font = null;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnTrim
            // 
            this.btnTrim.AccessibleDescription = null;
            this.btnTrim.AccessibleName = null;
            resources.ApplyResources(this.btnTrim, "btnTrim");
            this.btnTrim.BackgroundImage = null;
            this.btnTrim.Font = null;
            this.btnTrim.Name = "btnTrim";
            this.btnTrim.UseVisualStyleBackColor = true;
            this.btnTrim.Click += new System.EventHandler(this.btnTrim_Click);
            // 
            // btnTrash
            // 
            this.btnTrash.AccessibleDescription = null;
            this.btnTrash.AccessibleName = null;
            resources.ApplyResources(this.btnTrash, "btnTrash");
            this.btnTrash.BackgroundImage = null;
            this.btnTrash.Font = null;
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.UseVisualStyleBackColor = true;
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // btnToLower
            // 
            this.btnToLower.AccessibleDescription = null;
            this.btnToLower.AccessibleName = null;
            resources.ApplyResources(this.btnToLower, "btnToLower");
            this.btnToLower.BackgroundImage = null;
            this.btnToLower.Font = null;
            this.btnToLower.Name = "btnToLower";
            this.btnToLower.UseVisualStyleBackColor = true;
            this.btnToLower.Click += new System.EventHandler(this.btnToLower_Click);
            // 
            // btnToUpper
            // 
            this.btnToUpper.AccessibleDescription = null;
            this.btnToUpper.AccessibleName = null;
            resources.ApplyResources(this.btnToUpper, "btnToUpper");
            this.btnToUpper.BackgroundImage = null;
            this.btnToUpper.Font = null;
            this.btnToUpper.Name = "btnToUpper";
            this.btnToUpper.UseVisualStyleBackColor = true;
            this.btnToUpper.Click += new System.EventHandler(this.btnToUpper_Click);
            // 
            // btnFN
            // 
            this.btnFN.AccessibleDescription = null;
            this.btnFN.AccessibleName = null;
            resources.ApplyResources(this.btnFN, "btnFN");
            this.btnFN.BackgroundImage = null;
            this.btnFN.Font = null;
            this.btnFN.Name = "btnFN";
            this.btnFN.UseVisualStyleBackColor = true;
            this.btnFN.Click += new System.EventHandler(this.btnFN_Click);
            // 
            // chkAutoRun
            // 
            this.chkAutoRun.AccessibleDescription = null;
            this.chkAutoRun.AccessibleName = null;
            resources.ApplyResources(this.chkAutoRun, "chkAutoRun");
            this.chkAutoRun.BackgroundImage = null;
            this.chkAutoRun.Font = null;
            this.chkAutoRun.Name = "chkAutoRun";
            this.chkAutoRun.UseVisualStyleBackColor = true;
            this.chkAutoRun.CheckedChanged += new System.EventHandler(this.chkAutoRun_CheckedChanged);
            // 
            // btnCopyPath
            // 
            this.btnCopyPath.AccessibleDescription = null;
            this.btnCopyPath.AccessibleName = null;
            resources.ApplyResources(this.btnCopyPath, "btnCopyPath");
            this.btnCopyPath.BackgroundImage = null;
            this.btnCopyPath.Font = null;
            this.btnCopyPath.Name = "btnCopyPath";
            this.btnCopyPath.UseVisualStyleBackColor = true;
            this.btnCopyPath.Click += new System.EventHandler(this.btnCopyPath_Click);
            // 
            // btnMoveTo
            // 
            this.btnMoveTo.AccessibleDescription = null;
            this.btnMoveTo.AccessibleName = null;
            resources.ApplyResources(this.btnMoveTo, "btnMoveTo");
            this.btnMoveTo.BackgroundImage = null;
            this.btnMoveTo.Font = null;
            this.btnMoveTo.Name = "btnMoveTo";
            this.btnMoveTo.UseVisualStyleBackColor = true;
            this.btnMoveTo.Click += new System.EventHandler(this.btnMoveTo_Click);
            // 
            // menuMoveTo
            // 
            this.menuMoveTo.AccessibleDescription = null;
            this.menuMoveTo.AccessibleName = null;
            resources.ApplyResources(this.menuMoveTo, "menuMoveTo");
            this.menuMoveTo.BackgroundImage = null;
            this.menuMoveTo.Font = null;
            this.menuMoveTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aaaToolStripMenuItem});
            this.menuMoveTo.Name = "menuMoveTo";
            // 
            // aaaToolStripMenuItem
            // 
            this.aaaToolStripMenuItem.AccessibleDescription = null;
            this.aaaToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.aaaToolStripMenuItem, "aaaToolStripMenuItem");
            this.aaaToolStripMenuItem.BackgroundImage = null;
            this.aaaToolStripMenuItem.Name = "aaaToolStripMenuItem";
            this.aaaToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // cmModify
            // 
            this.cmModify.AccessibleDescription = null;
            this.cmModify.AccessibleName = null;
            resources.ApplyResources(this.cmModify, "cmModify");
            this.cmModify.BackgroundImage = null;
            this.cmModify.Font = null;
            this.cmModify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMakeFileNamable,
            this.tsmiLower,
            this.tsmiUpper,
            this.trimToolStripMenuItem,
            this.removeSpaceToolStripMenuItem});
            this.cmModify.Name = "ccmModify";
            // 
            // tsmiMakeFileNamable
            // 
            this.tsmiMakeFileNamable.AccessibleDescription = null;
            this.tsmiMakeFileNamable.AccessibleName = null;
            resources.ApplyResources(this.tsmiMakeFileNamable, "tsmiMakeFileNamable");
            this.tsmiMakeFileNamable.BackgroundImage = null;
            this.tsmiMakeFileNamable.Name = "tsmiMakeFileNamable";
            this.tsmiMakeFileNamable.ShortcutKeyDisplayString = null;
            this.tsmiMakeFileNamable.Click += new System.EventHandler(this.btnFN_Click);
            // 
            // tsmiLower
            // 
            this.tsmiLower.AccessibleDescription = null;
            this.tsmiLower.AccessibleName = null;
            resources.ApplyResources(this.tsmiLower, "tsmiLower");
            this.tsmiLower.BackgroundImage = null;
            this.tsmiLower.Name = "tsmiLower";
            this.tsmiLower.ShortcutKeyDisplayString = null;
            this.tsmiLower.Click += new System.EventHandler(this.btnToLower_Click);
            // 
            // tsmiUpper
            // 
            this.tsmiUpper.AccessibleDescription = null;
            this.tsmiUpper.AccessibleName = null;
            resources.ApplyResources(this.tsmiUpper, "tsmiUpper");
            this.tsmiUpper.BackgroundImage = null;
            this.tsmiUpper.Name = "tsmiUpper";
            this.tsmiUpper.ShortcutKeyDisplayString = null;
            this.tsmiUpper.Click += new System.EventHandler(this.btnToUpper_Click);
            // 
            // trimToolStripMenuItem
            // 
            this.trimToolStripMenuItem.AccessibleDescription = null;
            this.trimToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.trimToolStripMenuItem, "trimToolStripMenuItem");
            this.trimToolStripMenuItem.BackgroundImage = null;
            this.trimToolStripMenuItem.Name = "trimToolStripMenuItem";
            this.trimToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.trimToolStripMenuItem.Click += new System.EventHandler(this.btnTrim_Click);
            // 
            // removeSpaceToolStripMenuItem
            // 
            this.removeSpaceToolStripMenuItem.AccessibleDescription = null;
            this.removeSpaceToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.removeSpaceToolStripMenuItem, "removeSpaceToolStripMenuItem");
            this.removeSpaceToolStripMenuItem.BackgroundImage = null;
            this.removeSpaceToolStripMenuItem.Name = "removeSpaceToolStripMenuItem";
            this.removeSpaceToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.removeSpaceToolStripMenuItem.Click += new System.EventHandler(this.removeSpaceToolStripMenuItem_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleDescription = null;
            this.btnCopy.AccessibleName = null;
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.BackgroundImage = null;
            this.btnCopy.Font = null;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnModify
            // 
            this.btnModify.AccessibleDescription = null;
            this.btnModify.AccessibleName = null;
            resources.ApplyResources(this.btnModify, "btnModify");
            this.btnModify.BackgroundImage = null;
            this.btnModify.ContextMenuStrip = this.cmModify;
            this.btnModify.Font = null;
            this.btnModify.Name = "btnModify";
            this.btnModify.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnCopyPath);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.btnTrash);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnFN);
            this.Controls.Add(this.btnToLower);
            this.Controls.Add(this.btnToUpper);
            this.Controls.Add(this.btnTrim);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.menuMoveTo.ResumeLayout(false);
            this.cmModify.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnTrim;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.Button btnToLower;
        private System.Windows.Forms.Button btnToUpper;
        private System.Windows.Forms.Button btnFN;
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
        private System.Windows.Forms.ToolStripMenuItem trimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSpaceToolStripMenuItem;
        private System.Windows.Forms.Button btnCopy;
    }
}