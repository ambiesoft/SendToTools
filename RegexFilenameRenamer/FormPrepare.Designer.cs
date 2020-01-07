namespace Ambiesoft.RegexFilenameRenamer
{
    partial class FormPrepare
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchRegex = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIncludeExtention = new System.Windows.Forms.CheckBox();
            this.chkIgnoreCase = new System.Windows.Forms.CheckBox();
            this.chkShowConfirm = new System.Windows.Forms.CheckBox();
            this.chkContainsGlobs = new System.Windows.Forms.CheckBox();
            this.txtCurrentDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInputs = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search (Regex):";
            // 
            // txtSearchRegex
            // 
            this.txtSearchRegex.Location = new System.Drawing.Point(102, 6);
            this.txtSearchRegex.Name = "txtSearchRegex";
            this.txtSearchRegex.Size = new System.Drawing.Size(500, 20);
            this.txtSearchRegex.TabIndex = 1;
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(102, 32);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(500, 20);
            this.txtReplace.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Replace:";
            // 
            // chkIncludeExtention
            // 
            this.chkIncludeExtention.AutoSize = true;
            this.chkIncludeExtention.Location = new System.Drawing.Point(12, 61);
            this.chkIncludeExtention.Name = "chkIncludeExtention";
            this.chkIncludeExtention.Size = new System.Drawing.Size(108, 17);
            this.chkIncludeExtention.TabIndex = 4;
            this.chkIncludeExtention.Text = "&Include Extention";
            this.chkIncludeExtention.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreCase
            // 
            this.chkIgnoreCase.AutoSize = true;
            this.chkIgnoreCase.Location = new System.Drawing.Point(12, 84);
            this.chkIgnoreCase.Name = "chkIgnoreCase";
            this.chkIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.chkIgnoreCase.TabIndex = 4;
            this.chkIgnoreCase.Text = "Ignore &Case";
            this.chkIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // chkShowConfirm
            // 
            this.chkShowConfirm.AutoSize = true;
            this.chkShowConfirm.Location = new System.Drawing.Point(12, 107);
            this.chkShowConfirm.Name = "chkShowConfirm";
            this.chkShowConfirm.Size = new System.Drawing.Size(91, 17);
            this.chkShowConfirm.TabIndex = 4;
            this.chkShowConfirm.Text = "&Show Confirm";
            this.chkShowConfirm.UseVisualStyleBackColor = true;
            // 
            // chkContainsGlobs
            // 
            this.chkContainsGlobs.AutoSize = true;
            this.chkContainsGlobs.Location = new System.Drawing.Point(12, 130);
            this.chkContainsGlobs.Name = "chkContainsGlobs";
            this.chkContainsGlobs.Size = new System.Drawing.Size(97, 17);
            this.chkContainsGlobs.TabIndex = 4;
            this.chkContainsGlobs.Text = "&Contains Globs";
            this.chkContainsGlobs.UseVisualStyleBackColor = true;
            // 
            // txtCurrentDirectory
            // 
            this.txtCurrentDirectory.Location = new System.Drawing.Point(102, 151);
            this.txtCurrentDirectory.Name = "txtCurrentDirectory";
            this.txtCurrentDirectory.Size = new System.Drawing.Size(500, 20);
            this.txtCurrentDirectory.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current &Directory:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "&Inputs:";
            // 
            // txtInputs
            // 
            this.txtInputs.Location = new System.Drawing.Point(12, 208);
            this.txtInputs.Multiline = true;
            this.txtInputs.Name = "txtInputs";
            this.txtInputs.Size = new System.Drawing.Size(590, 217);
            this.txtInputs.TabIndex = 8;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(348, 457);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(134, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(488, 457);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(134, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormPrepare
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(634, 492);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInputs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCurrentDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkContainsGlobs);
            this.Controls.Add(this.chkShowConfirm);
            this.Controls.Add(this.chkIgnoreCase);
            this.Controls.Add(this.chkIncludeExtention);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchRegex);
            this.Controls.Add(this.label1);
            this.Name = "FormPrepare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtSearchRegex;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.CheckBox chkIncludeExtention;
        private System.Windows.Forms.CheckBox chkIgnoreCase;
        private System.Windows.Forms.CheckBox chkShowConfirm;
        private System.Windows.Forms.CheckBox chkContainsGlobs;
        private System.Windows.Forms.TextBox txtCurrentDirectory;
        private System.Windows.Forms.TextBox txtInputs;
    }
}