namespace CreateSizedFile
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
            this.lblFilename = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDir = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.chkZero = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(12, 46);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(53, 12);
            this.lblFilename.TabIndex = 2;
            this.lblFilename.Text = "&Filename:";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(14, 61);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(364, 19);
            this.txtFilename.TabIndex = 3;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(12, 83);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(58, 12);
            this.lblSize.TabIndex = 4;
            this.lblSize.Text = "&Size(byte):";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(14, 98);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(364, 19);
            this.txtSize.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(222, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(303, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(12, 9);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(39, 12);
            this.lblDir.TabIndex = 0;
            this.lblDir.Text = "&Folder:";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(14, 24);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(364, 19);
            this.txtDir.TabIndex = 1;
            // 
            // chkRandom
            // 
            this.chkRandom.AutoSize = true;
            this.chkRandom.Location = new System.Drawing.Point(14, 123);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(153, 16);
            this.chkRandom.TabIndex = 6;
            this.chkRandom.Text = "Fill with &Random Number";
            this.chkRandom.UseVisualStyleBackColor = true;
            this.chkRandom.CheckedChanged += new System.EventHandler(this.chkRandom_CheckedChanged);
            // 
            // chkZero
            // 
            this.chkZero.AutoSize = true;
            this.chkZero.Location = new System.Drawing.Point(14, 145);
            this.chkZero.Name = "chkZero";
            this.chkZero.Size = new System.Drawing.Size(92, 16);
            this.chkZero.TabIndex = 7;
            this.chkZero.Text = "Fill with &Zero";
            this.chkZero.UseVisualStyleBackColor = true;
            this.chkZero.CheckedChanged += new System.EventHandler(this.chkZero_CheckedChanged);
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(391, 205);
            this.Controls.Add(this.chkZero);
            this.Controls.Add(this.chkRandom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblFilename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Create Sized File";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.TextBox txtDir;
        internal System.Windows.Forms.CheckBox chkRandom;
        internal System.Windows.Forms.CheckBox chkZero;
    }
}