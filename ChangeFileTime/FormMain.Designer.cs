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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtpCRTime = new System.Windows.Forms.DateTimePicker();
            this.dtpLATime = new System.Windows.Forms.DateTimePicker();
            this.btnNowCRTime = new System.Windows.Forms.Button();
            this.btnNowLATime = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNowForAllTime = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            resources.ApplyResources(this.txtFileName, "txtFileName");
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            // 
            // dtpLWTime
            // 
            resources.ApplyResources(this.dtpLWTime, "dtpLWTime");
            this.dtpLWTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLWTime.Name = "dtpLWTime";
            // 
            // btnNowLWTime
            // 
            resources.ApplyResources(this.btnNowLWTime, "btnNowLWTime");
            this.btnNowLWTime.Name = "btnNowLWTime";
            this.btnNowLWTime.UseVisualStyleBackColor = true;
            this.btnNowLWTime.Click += new System.EventHandler(this.btnNowLWTime_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dtpCRTime
            // 
            resources.ApplyResources(this.dtpCRTime, "dtpCRTime");
            this.dtpCRTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCRTime.Name = "dtpCRTime";
            // 
            // dtpLATime
            // 
            resources.ApplyResources(this.dtpLATime, "dtpLATime");
            this.dtpLATime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLATime.Name = "dtpLATime";
            // 
            // btnNowCRTime
            // 
            resources.ApplyResources(this.btnNowCRTime, "btnNowCRTime");
            this.btnNowCRTime.Name = "btnNowCRTime";
            this.btnNowCRTime.UseVisualStyleBackColor = true;
            this.btnNowCRTime.Click += new System.EventHandler(this.btnNowCRTime_Click);
            // 
            // btnNowLATime
            // 
            resources.ApplyResources(this.btnNowLATime, "btnNowLATime");
            this.btnNowLATime.Name = "btnNowLATime";
            this.btnNowLATime.UseVisualStyleBackColor = true;
            this.btnNowLATime.Click += new System.EventHandler(this.btnNowLATime_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnNowForAllTime
            // 
            resources.ApplyResources(this.btnNowForAllTime, "btnNowForAllTime");
            this.btnNowForAllTime.Name = "btnNowForAllTime";
            this.btnNowForAllTime.UseVisualStyleBackColor = true;
            this.btnNowForAllTime.Click += new System.EventHandler(this.btnNowForAllTime_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnNowForAllTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNowLATime);
            this.Controls.Add(this.btnNowCRTime);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnNowLWTime);
            this.Controls.Add(this.dtpLATime);
            this.Controls.Add(this.dtpCRTime);
            this.Controls.Add(this.dtpLWTime);
            this.Controls.Add(this.txtFileName);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtFileName;
        internal System.Windows.Forms.DateTimePicker dtpLWTime;
        internal System.Windows.Forms.DateTimePicker dtpCRTime;
        internal System.Windows.Forms.DateTimePicker dtpLATime;

        private System.Windows.Forms.Button btnNowLWTime;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNowCRTime;
        private System.Windows.Forms.Button btnNowLATime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNowForAllTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnApply;
    }
}

