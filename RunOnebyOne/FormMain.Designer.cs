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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listMain = new System.Windows.Forms.ListView();
            this.chIndicator = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRun = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
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
            this.listMain.Name = "listMain";
            this.listMain.UseCompatibleStateImageBehavior = false;
            this.listMain.View = System.Windows.Forms.View.Details;
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
            // FormMain
            // 
            this.AcceptButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.listMain);
            this.Name = "FormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listMain;
        private System.Windows.Forms.ColumnHeader chIndicator;
        private System.Windows.Forms.ColumnHeader chFile;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ColumnHeader chResult;
        private System.Windows.Forms.Button btnClose;
    }
}

