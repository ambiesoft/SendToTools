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
            this.txtCurrentDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInputs = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtExpandedInput = new System.Windows.Forms.TextBox();
            this.lblExpandedInput = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search (Regex):";
            // 
            // txtSearchRegex
            // 
            this.txtSearchRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchRegex.Location = new System.Drawing.Point(127, 5);
            this.txtSearchRegex.Name = "txtSearchRegex";
            this.txtSearchRegex.Size = new System.Drawing.Size(475, 23);
            this.txtSearchRegex.TabIndex = 1;
            // 
            // txtReplace
            // 
            this.txtReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplace.Location = new System.Drawing.Point(127, 35);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(475, 23);
            this.txtReplace.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Replace:";
            // 
            // chkIncludeExtention
            // 
            this.chkIncludeExtention.AutoSize = true;
            this.chkIncludeExtention.Location = new System.Drawing.Point(15, 74);
            this.chkIncludeExtention.Name = "chkIncludeExtention";
            this.chkIncludeExtention.Size = new System.Drawing.Size(127, 19);
            this.chkIncludeExtention.TabIndex = 4;
            this.chkIncludeExtention.Text = "&Include Extention";
            this.chkIncludeExtention.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreCase
            // 
            this.chkIgnoreCase.AutoSize = true;
            this.chkIgnoreCase.Location = new System.Drawing.Point(15, 96);
            this.chkIgnoreCase.Name = "chkIgnoreCase";
            this.chkIgnoreCase.Size = new System.Drawing.Size(96, 19);
            this.chkIgnoreCase.TabIndex = 4;
            this.chkIgnoreCase.Text = "Ignore &Case";
            this.chkIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // chkShowConfirm
            // 
            this.chkShowConfirm.AutoSize = true;
            this.chkShowConfirm.Location = new System.Drawing.Point(15, 117);
            this.chkShowConfirm.Name = "chkShowConfirm";
            this.chkShowConfirm.Size = new System.Drawing.Size(108, 19);
            this.chkShowConfirm.TabIndex = 4;
            this.chkShowConfirm.Text = "&Show Confirm";
            this.chkShowConfirm.UseVisualStyleBackColor = true;
            // 
            // txtCurrentDirectory
            // 
            this.txtCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentDirectory.Location = new System.Drawing.Point(127, 151);
            this.txtCurrentDirectory.Name = "txtCurrentDirectory";
            this.txtCurrentDirectory.Size = new System.Drawing.Size(475, 23);
            this.txtCurrentDirectory.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current &Directory:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "&Inputs:";
            // 
            // txtInputs
            // 
            this.txtInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputs.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtInputs.Location = new System.Drawing.Point(12, 204);
            this.txtInputs.Multiline = true;
            this.txtInputs.Name = "txtInputs";
            this.txtInputs.Size = new System.Drawing.Size(590, 86);
            this.txtInputs.TabIndex = 8;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(328, 497);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(134, 31);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(468, 497);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(134, 31);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtExpandedInput
            // 
            this.txtExpandedInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExpandedInput.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.txtExpandedInput.Location = new System.Drawing.Point(0, 0);
            this.txtExpandedInput.Multiline = true;
            this.txtExpandedInput.Name = "txtExpandedInput";
            this.txtExpandedInput.Size = new System.Drawing.Size(590, 152);
            this.txtExpandedInput.TabIndex = 11;
            // 
            // lblExpandedInput
            // 
            this.lblExpandedInput.AutoSize = true;
            this.lblExpandedInput.Location = new System.Drawing.Point(9, 307);
            this.lblExpandedInput.Name = "lblExpandedInput";
            this.lblExpandedInput.Size = new System.Drawing.Size(109, 15);
            this.lblExpandedInput.TabIndex = 12;
            this.lblExpandedInput.Text = "&Expanded Inputs:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtExpandedInput);
            this.panel1.Location = new System.Drawing.Point(12, 325);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 152);
            this.panel1.TabIndex = 13;
            // 
            // FormPrepare
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(622, 540);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblExpandedInput);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInputs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCurrentDirectory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkShowConfirm);
            this.Controls.Add(this.chkIgnoreCase);
            this.Controls.Add(this.chkIncludeExtention);
            this.Controls.Add(this.txtReplace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchRegex);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "FormPrepare";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtCurrentDirectory;
        private System.Windows.Forms.TextBox txtInputs;
        private System.Windows.Forms.TextBox txtExpandedInput;
        private System.Windows.Forms.Label lblExpandedInput;
        private System.Windows.Forms.Panel panel1;
    }
}