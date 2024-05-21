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
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictQuestion = new System.Windows.Forms.PictureBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.chkShowFullPath = new System.Windows.Forms.CheckBox();
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
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Tag = "";
            this.btnCancel.UseVisualStyleBackColor = true;
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
            // txtMessage
            // 
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            // 
            // chkShowAll
            // 
            resources.ApplyResources(this.chkShowAll, "chkShowAll");
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // chkShowFullPath
            // 
            resources.ApplyResources(this.chkShowFullPath, "chkShowFullPath");
            this.chkShowFullPath.Name = "chkShowFullPath";
            this.chkShowFullPath.UseVisualStyleBackColor = true;
            this.chkShowFullPath.CheckedChanged += new System.EventHandler(this.chkShowFullPath_CheckedChanged);
            // 
            // FormConfirm
            // 
            this.AcceptButton = this.btnYes;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.chkShowFullPath);
            this.Controls.Add(this.chkShowAll);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.pictQuestion);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnYes);
            this.Name = "FormConfirm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConfirm_FormClosed);
            this.Load += new System.EventHandler(this.FormConfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictQuestion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pictQuestion;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.CheckBox chkShowAll;
        internal System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.CheckBox chkShowFullPath;
    }
}