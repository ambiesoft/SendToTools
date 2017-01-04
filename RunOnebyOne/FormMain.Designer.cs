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
            this.listMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIndicator,
            this.chFile,
            this.chResult});
            this.listMain.Location = new System.Drawing.Point(12, 12);
            this.listMain.Name = "listMain";
            this.listMain.Size = new System.Drawing.Size(631, 203);
            this.listMain.TabIndex = 0;
            this.listMain.UseCompatibleStateImageBehavior = false;
            this.listMain.View = System.Windows.Forms.View.Details;
            this.listMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.listMain_DragDrop);
            this.listMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.listMain_DragEnter);
            this.listMain.DragOver += new System.Windows.Forms.DragEventHandler(this.listMain_DragOver);
            // 
            // chIndicator
            // 
            this.chIndicator.Text = "     ";
            // 
            // chFile
            // 
            this.chFile.Text = "File to launch";
            this.chFile.Width = 487;
            // 
            // chResult
            // 
            this.chResult.Text = "Result";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRun.Location = new System.Drawing.Point(12, 223);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(95, 24);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "&Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(548, 223);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 258);
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

