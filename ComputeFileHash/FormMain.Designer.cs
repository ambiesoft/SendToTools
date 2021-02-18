namespace ComputeFileHash
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
            this.txtFile = new System.Windows.Forms.TextBox();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.progMain = new System.Windows.Forms.ProgressBar();
            this.lvMain = new System.Windows.Forms.ListView();
            this.colMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCompute = new System.Windows.Forms.Button();
            this.chkSha1 = new System.Windows.Forms.CheckBox();
            this.chkMD5 = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(12, 11);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(580, 23);
            this.txtFile.TabIndex = 0;
            // 
            // panelRoot
            // 
            this.panelRoot.Controls.Add(this.progMain);
            this.panelRoot.Controls.Add(this.lvMain);
            this.panelRoot.Controls.Add(this.btnCompute);
            this.panelRoot.Controls.Add(this.chkSha1);
            this.panelRoot.Controls.Add(this.chkMD5);
            this.panelRoot.Controls.Add(this.btnBrowse);
            this.panelRoot.Controls.Add(this.txtFile);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Size = new System.Drawing.Size(644, 277);
            this.panelRoot.TabIndex = 1;
            // 
            // progMain
            // 
            this.progMain.Location = new System.Drawing.Point(12, 236);
            this.progMain.Name = "progMain";
            this.progMain.Size = new System.Drawing.Size(469, 29);
            this.progMain.TabIndex = 6;
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMethod,
            this.colValue});
            this.lvMain.HideSelection = false;
            this.lvMain.Location = new System.Drawing.Point(12, 92);
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(620, 137);
            this.lvMain.TabIndex = 5;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            // 
            // colMethod
            // 
            this.colMethod.Text = "Method";
            this.colMethod.Width = 87;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 489;
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(487, 236);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(145, 29);
            this.btnCompute.TabIndex = 4;
            this.btnCompute.Text = "&Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // chkSha1
            // 
            this.chkSha1.AutoSize = true;
            this.chkSha1.Location = new System.Drawing.Point(67, 50);
            this.chkSha1.Name = "chkSha1";
            this.chkSha1.Size = new System.Drawing.Size(55, 19);
            this.chkSha1.TabIndex = 3;
            this.chkSha1.Text = "&Sha1";
            this.chkSha1.UseVisualStyleBackColor = true;
            // 
            // chkMD5
            // 
            this.chkMD5.AutoSize = true;
            this.chkMD5.Location = new System.Drawing.Point(12, 50);
            this.chkMD5.Name = "chkMD5";
            this.chkMD5.Size = new System.Drawing.Size(52, 19);
            this.chkMD5.TabIndex = 2;
            this.chkMD5.Text = "&MD5";
            this.chkMD5.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(598, 8);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(34, 21);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "&...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(644, 277);
            this.Controls.Add(this.panelRoot);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "FormMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.ProgressBar progMain;
        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader colMethod;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.CheckBox chkSha1;
        private System.Windows.Forms.CheckBox chkMD5;
        private System.Windows.Forms.Button btnBrowse;
    }
}

