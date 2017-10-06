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
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRun.Location = new System.Drawing.Point(306, 217);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(185, 27);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "&Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 27);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtExe
            // 
            this.txtExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExe.Location = new System.Drawing.Point(12, 26);
            this.txtExe.Name = "txtExe";
            this.txtExe.ReadOnly = true;
            this.txtExe.Size = new System.Drawing.Size(591, 19);
            this.txtExe.TabIndex = 1;
            // 
            // txtArg
            // 
            this.txtArg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArg.Location = new System.Drawing.Point(12, 82);
            this.txtArg.Name = "txtArg";
            this.txtArg.Size = new System.Drawing.Size(591, 19);
            this.txtArg.TabIndex = 3;
            // 
            // lblExe
            // 
            this.lblExe.AutoSize = true;
            this.lblExe.Location = new System.Drawing.Point(10, 11);
            this.lblExe.Name = "lblExe";
            this.lblExe.Size = new System.Drawing.Size(24, 12);
            this.lblExe.TabIndex = 0;
            this.lblExe.Text = "&File";
            // 
            // lblArg
            // 
            this.lblArg.AutoSize = true;
            this.lblArg.Location = new System.Drawing.Point(10, 67);
            this.lblArg.Name = "lblArg";
            this.lblArg.Size = new System.Drawing.Size(60, 12);
            this.lblArg.TabIndex = 2;
            this.lblArg.Text = "&Arguments";
            // 
            // chkRunas
            // 
            this.chkRunas.AutoSize = true;
            this.chkRunas.Location = new System.Drawing.Point(12, 132);
            this.chkRunas.Name = "chkRunas";
            this.chkRunas.Size = new System.Drawing.Size(56, 16);
            this.chkRunas.TabIndex = 4;
            this.chkRunas.Text = "&Runas";
            this.chkRunas.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnRun;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(615, 256);
            this.Controls.Add(this.chkRunas);
            this.Controls.Add(this.lblArg);
            this.Controls.Add(this.lblExe);
            this.Controls.Add(this.txtArg);
            this.Controls.Add(this.txtExe);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Run with Arguments";
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

