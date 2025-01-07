namespace MoreSendToOption
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMoreSendtoFolder = new System.Windows.Forms.Label();
            this.txtMoreSendtoFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkShowArgToTarget = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(173, 122);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(129, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(308, 122);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblMoreSendtoFolder
            // 
            this.lblMoreSendtoFolder.AutoSize = true;
            this.lblMoreSendtoFolder.Location = new System.Drawing.Point(12, 9);
            this.lblMoreSendtoFolder.Name = "lblMoreSendtoFolder";
            this.lblMoreSendtoFolder.Size = new System.Drawing.Size(103, 13);
            this.lblMoreSendtoFolder.TabIndex = 100;
            this.lblMoreSendtoFolder.Text = "More Sendto &Folder:";
            // 
            // txtMoreSendtoFolder
            // 
            this.txtMoreSendtoFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoreSendtoFolder.Location = new System.Drawing.Point(15, 25);
            this.txtMoreSendtoFolder.Name = "txtMoreSendtoFolder";
            this.txtMoreSendtoFolder.Size = new System.Drawing.Size(382, 20);
            this.txtMoreSendtoFolder.TabIndex = 200;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(403, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(34, 23);
            this.btnBrowse.TabIndex = 300;
            this.btnBrowse.Text = "&...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkShowArgToTarget
            // 
            this.chkShowArgToTarget.AutoSize = true;
            this.chkShowArgToTarget.Location = new System.Drawing.Point(15, 72);
            this.chkShowArgToTarget.Name = "chkShowArgToTarget";
            this.chkShowArgToTarget.Size = new System.Drawing.Size(213, 17);
            this.chkShowArgToTarget.TabIndex = 400;
            this.chkShowArgToTarget.Text = "&Show arguments to target app on Menu";
            this.chkShowArgToTarget.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(449, 157);
            this.Controls.Add(this.chkShowArgToTarget);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtMoreSendtoFolder);
            this.Controls.Add(this.lblMoreSendtoFolder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(465, 196);
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMoreSendtoFolder;
        private System.Windows.Forms.TextBox txtMoreSendtoFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkShowArgToTarget;
    }
}

