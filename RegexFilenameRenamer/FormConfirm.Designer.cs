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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfirm));
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
            resources.ApplyResources(this.btnYes, "btnYes");
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Name = "btnYes";
            this.btnYes.UseVisualStyleBackColor = true;
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Name = "btnNo";
            this.btnNo.Tag = "";
            this.btnNo.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Tag = "";
            // 
            // pictQuestion
            // 
            resources.ApplyResources(this.pictQuestion, "pictQuestion");
            this.pictQuestion.Name = "pictQuestion";
            this.pictQuestion.TabStop = false;
            // 
            // rtxtMessage
            // 
            resources.ApplyResources(this.rtxtMessage, "rtxtMessage");
            this.rtxtMessage.Name = "rtxtMessage";
            this.rtxtMessage.ReadOnly = true;
            // 
            // FormConfirm
            // 
            this.AcceptButton = this.btnYes;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.Controls.Add(this.rtxtMessage);
            this.Controls.Add(this.pictQuestion);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Name = "FormConfirm";
            this.ShowIcon = false;
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