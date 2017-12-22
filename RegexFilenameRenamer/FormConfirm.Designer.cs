namespace Ambiesoft.RegexFilenameRenamer
{
    partial class FormConfirm
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
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictQuestion = new System.Windows.Forms.PictureBox();
            this.rtxtMessage = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictQuestion)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(199, 196);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(113, 24);
            this.btnYes.TabIndex = 40;
            this.btnYes.Text = "&Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // btnNo
            // 
            this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Location = new System.Drawing.Point(318, 196);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(113, 24);
            this.btnNo.TabIndex = 10;
            this.btnNo.Tag = "";
            this.btnNo.Text = "&No";
            this.btnNo.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(58, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(35, 13);
            this.lblMessage.TabIndex = 20;
            this.lblMessage.Tag = "";
            this.lblMessage.Text = "label1";
            // 
            // pictQuestion
            // 
            this.pictQuestion.Location = new System.Drawing.Point(12, 12);
            this.pictQuestion.Name = "pictQuestion";
            this.pictQuestion.Size = new System.Drawing.Size(40, 38);
            this.pictQuestion.TabIndex = 41;
            this.pictQuestion.TabStop = false;
            // 
            // rtxtMessage
            // 
            this.rtxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtMessage.Location = new System.Drawing.Point(12, 56);
            this.rtxtMessage.Name = "rtxtMessage";
            this.rtxtMessage.ReadOnly = true;
            this.rtxtMessage.Size = new System.Drawing.Size(419, 134);
            this.rtxtMessage.TabIndex = 30;
            this.rtxtMessage.Text = "";
            this.rtxtMessage.WordWrap = false;
            // 
            // FormConfirm
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(443, 231);
            this.Controls.Add(this.rtxtMessage);
            this.Controls.Add(this.pictQuestion);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Name = "FormConfirm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormConfirm";
            this.Load += new System.EventHandler(this.FormConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictQuestion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        internal System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pictQuestion;
        internal System.Windows.Forms.RichTextBox rtxtMessage;
    }
}