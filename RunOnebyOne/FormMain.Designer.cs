namespace RunOnebyOne
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listMain = new System.Windows.Forms.ListView();
            this.chIndicator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReopenAsAdmin = new System.Windows.Forms.Button();
            this.labelApplication = new System.Windows.Forms.Label();
            this.btnBrowseApp = new System.Windows.Forms.Button();
            this.ofdApplication = new System.Windows.Forms.OpenFileDialog();
            this.lblArguments = new System.Windows.Forms.Label();
            this.btnClearResults = new System.Windows.Forms.Button();
            this.cmbApplication = new System.Windows.Forms.ComboBox();
            this.cmbArguments = new System.Windows.Forms.ComboBox();
            this.lblCombo1TopLeft = new System.Windows.Forms.Label();
            this.lblCombo1BottomRight = new System.Windows.Forms.Label();
            this.lblCombo2TopLeft = new System.Windows.Forms.Label();
            this.lblCombo2BottomRight = new System.Windows.Forms.Label();
            this.txtActualArg = new System.Windows.Forms.TextBox();
            this.lblActualArgTopLeft = new System.Windows.Forms.Label();
            this.lblActualArgBottomRight = new System.Windows.Forms.Label();
            this.btnBrowseMacro = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.cmList.SuspendLayout();
            this.SuspendLayout();
            // 
            // listMain
            // 
            this.listMain.AllowDrop = true;
            resources.ApplyResources(this.listMain, "listMain");
            this.listMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndicator,
            this.chFile,
            this.chResult});
            this.listMain.ContextMenuStrip = this.cmList;
            this.listMain.FullRowSelect = true;
            this.listMain.HideSelection = false;
            this.listMain.Name = "listMain";
            this.listMain.UseCompatibleStateImageBehavior = false;
            this.listMain.View = System.Windows.Forms.View.Details;
            this.listMain.SelectedIndexChanged += new System.EventHandler(this.listMain_SelectedIndexChanged);
            this.listMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.listMain_DragDrop);
            this.listMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.listMain_DragEnter);
            this.listMain.DragOver += new System.Windows.Forms.DragEventHandler(this.listMain_DragOver);
            // 
            // chIndicator
            // 
            resources.ApplyResources(this.chIndicator, "chIndicator");
            // 
            // chFile
            // 
            resources.ApplyResources(this.chFile, "chFile");
            // 
            // chResult
            // 
            resources.ApplyResources(this.chResult, "chResult");
            // 
            // cmList
            // 
            this.cmList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemove,
            this.toolStripMenuItem1,
            this.tsmiRemoveAll});
            this.cmList.Name = "cmList";
            resources.ApplyResources(this.cmList, "cmList");
            // 
            // tsmiRemove
            // 
            this.tsmiRemove.Name = "tsmiRemove";
            resources.ApplyResources(this.tsmiRemove, "tsmiRemove");
            this.tsmiRemove.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // tsmiRemoveAll
            // 
            this.tsmiRemoveAll.Name = "tsmiRemoveAll";
            resources.ApplyResources(this.tsmiRemoveAll, "tsmiRemoveAll");
            this.tsmiRemoveAll.Click += new System.EventHandler(this.tsmiRemoveAll_Click);
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReopenAsAdmin
            // 
            resources.ApplyResources(this.btnReopenAsAdmin, "btnReopenAsAdmin");
            this.btnReopenAsAdmin.Name = "btnReopenAsAdmin";
            this.btnReopenAsAdmin.UseVisualStyleBackColor = true;
            this.btnReopenAsAdmin.Click += new System.EventHandler(this.btnReopenAsAdmin_Click);
            // 
            // labelApplication
            // 
            resources.ApplyResources(this.labelApplication, "labelApplication");
            this.labelApplication.Name = "labelApplication";
            // 
            // btnBrowseApp
            // 
            resources.ApplyResources(this.btnBrowseApp, "btnBrowseApp");
            this.btnBrowseApp.Name = "btnBrowseApp";
            this.btnBrowseApp.UseVisualStyleBackColor = true;
            this.btnBrowseApp.Click += new System.EventHandler(this.btnBrowseApp_Click);
            // 
            // ofdApplication
            // 
            this.ofdApplication.DefaultExt = "exe";
            resources.ApplyResources(this.ofdApplication, "ofdApplication");
            // 
            // lblArguments
            // 
            resources.ApplyResources(this.lblArguments, "lblArguments");
            this.lblArguments.Name = "lblArguments";
            // 
            // btnClearResults
            // 
            resources.ApplyResources(this.btnClearResults, "btnClearResults");
            this.btnClearResults.Name = "btnClearResults";
            this.btnClearResults.UseVisualStyleBackColor = true;
            this.btnClearResults.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbApplication
            // 
            resources.ApplyResources(this.cmbApplication, "cmbApplication");
            this.cmbApplication.FormattingEnabled = true;
            this.cmbApplication.Name = "cmbApplication";
            // 
            // cmbArguments
            // 
            resources.ApplyResources(this.cmbArguments, "cmbArguments");
            this.cmbArguments.FormattingEnabled = true;
            this.cmbArguments.Name = "cmbArguments";
            this.cmbArguments.TextChanged += new System.EventHandler(this.cmbArguments_TextChanged);
            // 
            // lblCombo1TopLeft
            // 
            resources.ApplyResources(this.lblCombo1TopLeft, "lblCombo1TopLeft");
            this.lblCombo1TopLeft.Name = "lblCombo1TopLeft";
            // 
            // lblCombo1BottomRight
            // 
            resources.ApplyResources(this.lblCombo1BottomRight, "lblCombo1BottomRight");
            this.lblCombo1BottomRight.Name = "lblCombo1BottomRight";
            // 
            // lblCombo2TopLeft
            // 
            resources.ApplyResources(this.lblCombo2TopLeft, "lblCombo2TopLeft");
            this.lblCombo2TopLeft.Name = "lblCombo2TopLeft";
            // 
            // lblCombo2BottomRight
            // 
            resources.ApplyResources(this.lblCombo2BottomRight, "lblCombo2BottomRight");
            this.lblCombo2BottomRight.Name = "lblCombo2BottomRight";
            // 
            // txtActualArg
            // 
            resources.ApplyResources(this.txtActualArg, "txtActualArg");
            this.txtActualArg.Name = "txtActualArg";
            this.txtActualArg.ReadOnly = true;
            // 
            // lblActualArgTopLeft
            // 
            resources.ApplyResources(this.lblActualArgTopLeft, "lblActualArgTopLeft");
            this.lblActualArgTopLeft.Name = "lblActualArgTopLeft";
            // 
            // lblActualArgBottomRight
            // 
            resources.ApplyResources(this.lblActualArgBottomRight, "lblActualArgBottomRight");
            this.lblActualArgBottomRight.Name = "lblActualArgBottomRight";
            // 
            // btnBrowseMacro
            // 
            resources.ApplyResources(this.btnBrowseMacro, "btnBrowseMacro");
            this.btnBrowseMacro.Name = "btnBrowseMacro";
            this.btnBrowseMacro.UseVisualStyleBackColor = true;
            this.btnBrowseMacro.Click += new System.EventHandler(this.btnBrowseMacro_Click);
            // 
            // btnAbout
            // 
            resources.ApplyResources(this.btnAbout, "btnAbout");
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnBrowseMacro);
            this.Controls.Add(this.txtActualArg);
            this.Controls.Add(this.lblActualArgBottomRight);
            this.Controls.Add(this.lblCombo2BottomRight);
            this.Controls.Add(this.lblCombo1BottomRight);
            this.Controls.Add(this.lblActualArgTopLeft);
            this.Controls.Add(this.lblCombo2TopLeft);
            this.Controls.Add(this.lblCombo1TopLeft);
            this.Controls.Add(this.cmbArguments);
            this.Controls.Add(this.cmbApplication);
            this.Controls.Add(this.btnClearResults);
            this.Controls.Add(this.btnBrowseApp);
            this.Controls.Add(this.lblArguments);
            this.Controls.Add(this.labelApplication);
            this.Controls.Add(this.btnReopenAsAdmin);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.listMain);
            this.Name = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.cmList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listMain;
        private System.Windows.Forms.ColumnHeader chIndicator;
        private System.Windows.Forms.ColumnHeader chFile;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ColumnHeader chResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReopenAsAdmin;
        private System.Windows.Forms.Label labelApplication;
        private System.Windows.Forms.Button btnBrowseApp;
        private System.Windows.Forms.OpenFileDialog ofdApplication;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.Button btnClearResults;
        private System.Windows.Forms.ContextMenuStrip cmList;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveAll;
        private System.Windows.Forms.ComboBox cmbApplication;
        private System.Windows.Forms.ComboBox cmbArguments;
        private System.Windows.Forms.Label lblCombo1TopLeft;
        private System.Windows.Forms.Label lblCombo1BottomRight;
        private System.Windows.Forms.Label lblCombo2TopLeft;
        private System.Windows.Forms.Label lblCombo2BottomRight;
        private System.Windows.Forms.TextBox txtActualArg;
        private System.Windows.Forms.Label lblActualArgTopLeft;
        private System.Windows.Forms.Label lblActualArgBottomRight;
        private System.Windows.Forms.Button btnBrowseMacro;
        private System.Windows.Forms.Button btnAbout;
    }
}

