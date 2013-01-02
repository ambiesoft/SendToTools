namespace ChangeFileTime
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
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.dtpLWTime = new System.Windows.Forms.DateTimePicker();
            this.btnNowLWTime = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(268, 19);
            this.txtFileName.TabIndex = 0;
            // 
            // dtpLWTime
            // 
            this.dtpLWTime.Location = new System.Drawing.Point(21, 69);
            this.dtpLWTime.Name = "dtpLWTime";
            this.dtpLWTime.Size = new System.Drawing.Size(119, 19);
            this.dtpLWTime.TabIndex = 1;
            // 
            // btnNowLWTime
            // 
            this.btnNowLWTime.Location = new System.Drawing.Point(146, 69);
            this.btnNowLWTime.Name = "btnNowLWTime";
            this.btnNowLWTime.Size = new System.Drawing.Size(75, 23);
            this.btnNowLWTime.TabIndex = 2;
            this.btnNowLWTime.Text = "&Now";
            this.btnNowLWTime.UseVisualStyleBackColor = true;
            this.btnNowLWTime.Click += new System.EventHandler(this.btnNowLWTime_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(202, 241);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnNowLWTime);
            this.Controls.Add(this.dtpLWTime);
            this.Controls.Add(this.txtFileName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtFileName;
        internal System.Windows.Forms.DateTimePicker dtpLWTime;
        private System.Windows.Forms.Button btnNowLWTime;
        private System.Windows.Forms.Button btnOK;

    }
}

