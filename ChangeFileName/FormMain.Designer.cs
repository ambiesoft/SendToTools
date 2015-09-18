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
            this.removeSpaceSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnModify = new ChangeFileName.MenuButton();
            this.menuMoveTo.SuspendLayout();
            this.cmModify.SuspendLayout();
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
            // btnTrim
            // 
            resources.ApplyResources(this.btnTrim, "btnTrim");
            this.btnTrim.Name = "btnTrim";
            this.btnTrim.UseVisualStyleBackColor = true;
            this.btnTrim.Click += new System.EventHandler(this.btnTrim_Click);
            // 
            // btnTrash
            // 
            resources.ApplyResources(this.btnTrash, "btnTrash");
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.UseVisualStyleBackColor = true;
            this.btnTrash.Click += new System.EventHandler(this.btnTrash_Click);
            // 
            // btnToLower
            // 
            resources.ApplyResources(this.btnToLower, "btnToLower");
            this.btnToLower.Name = "btnToLower";
            this.btnToLower.UseVisualStyleBackColor = true;
            this.btnToLower.Click += new System.EventHandler(this.btnToLower_Click);
            // 
            // btnToUpper
            // 
            resources.ApplyResources(this.btnToUpper, "btnToUpper");
            this.btnToUpper.Name = "btnToUpper";
            this.btnToUpper.UseVisualStyleBackColor = true;
            this.btnToUpper.Click += new System.EventHandler(this.btnToUpper_Click);
            // 
            // btnFN
            // 
            resources.ApplyResources(this.btnFN, "btnFN");
            this.btnFN.Name = "btnFN";
            this.btnFN.UseVisualStyleBackColor = true;
            this.btnFN.Click += new System.EventHandler(this.btnFN_Click);
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
            this.trimToolStripMenuItem,
            this.removeSpaceToolStripMenuItem,
            this.removeSpaceSelectionToolStripMenuItem});
            this.cmModify.Name = "ccmModify";
            resources.ApplyResources(this.cmModify, "cmModify");
            // 
            // tsmiMakeFileNamable
            // 
            this.tsmiMakeFileNamable.Name = "tsmiMakeFileNamable";
            resources.ApplyResources(this.tsmiMakeFileNamable, "tsmiMakeFileNamable");
            this.tsmiMakeFileNamable.Click += new System.EventHandler(this.btnFN_Click);
            // 
            // tsmiLower
            // 
            this.tsmiLower.Name = "tsmiLower";
            resources.ApplyResources(this.tsmiLower, "tsmiLower");
            this.tsmiLower.Click += new System.EventHandler(this.btnToLower_Click);
            // 
            // tsmiUpper
            // 
            this.tsmiUpper.Name = "tsmiUpper";
            resources.ApplyResources(this.tsmiUpper, "tsmiUpper");
            this.tsmiUpper.Click += new System.EventHandler(this.btnToUpper_Click);
            // 
            // trimToolStripMenuItem
            // 
            this.trimToolStripMenuItem.Name = "trimToolStripMenuItem";
            resources.ApplyResources(this.trimToolStripMenuItem, "trimToolStripMenuItem");
            this.trimToolStripMenuItem.Click += new System.EventHandler(this.btnTrim_Click);
            // 
            // removeSpaceToolStripMenuItem
            // 
            this.removeSpaceToolStripMenuItem.Name = "removeSpaceToolStripMenuItem";
            resources.ApplyResources(this.removeSpaceToolStripMenuItem, "removeSpaceToolStripMenuItem");
            this.removeSpaceToolStripMenuItem.Click += new System.EventHandler(this.removeSpaceToolStripMenuItem_Click);
            // 
            // removeSpaceSelectionToolStripMenuItem
            // 
            this.removeSpaceSelectionToolStripMenuItem.Name = "removeSpaceSelectionToolStripMenuItem";
            resources.ApplyResources(this.removeSpaceSelectionToolStripMenuItem, "removeSpaceSelectionToolStripMenuItem");
            this.removeSpaceSelectionToolStripMenuItem.Click += new System.EventHandler(this.removeSpaceSelectionToolStripMenuItem_Click);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnModify
            // 
            this.btnModify.ContextMenuStrip = this.cmModify;
            resources.ApplyResources(this.btnModify, "btnModify");
            this.btnModify.Name = "btnModify";
            this.btnModify.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.btnMoveTo);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.btnCopyPath);
            this.Controls.Add(this.btnTrash);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnFN);
            this.Controls.Add(this.btnToLower);
            this.Controls.Add(this.btnToUpper);
            this.Controls.Add(this.btnTrim);
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
        private System.Windows.Forms.ToolStripMenuItem removeSpaceSelectionToolStripMenuItem;
    }
}