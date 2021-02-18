namespace RunWithArgs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtExe = new System.Windows.Forms.TextBox();
            this.txtArg = new System.Windows.Forms.TextBox();
            this.lblExe = new System.Windows.Forms.Label();
            this.lblArg = new System.Windows.Forms.Label();
            this.chkRunas = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRun.Name = "btnRun";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtExe
            // 
            resources.ApplyResources(this.txtExe, "txtExe");
            this.txtExe.Name = "txtExe";
            this.txtExe.ReadOnly = true;
            // 
            // txtArg
            // 
            resources.ApplyResources(this.txtArg, "txtArg");
            this.txtArg.Name = "txtArg";
            // 
            // lblExe
            // 
            resources.ApplyResources(this.lblExe, "lblExe");
            this.lblExe.Name = "lblExe";
            // 
            // lblArg
            // 
            resources.ApplyResources(this.lblArg, "lblArg");
            this.lblArg.Name = "lblArg";
            // 
            // chkRunas
            // 
            resources.ApplyResources(this.chkRunas, "chkRunas");
            this.chkRunas.Name = "chkRunas";
            this.chkRunas.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnRun;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.chkRunas);
            this.Controls.Add(this.lblArg);
            this.Controls.Add(this.lblExe);
            this.Controls.Add(this.txtArg);
            this.Controls.Add(this.txtExe);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.TextBox txtExe;
        internal System.Windows.Forms.TextBox txtArg;
        private System.Windows.Forms.Label lblExe;
        private System.Windows.Forms.Label lblArg;
        private System.Windows.Forms.CheckBox chkRunas;
    }
}

