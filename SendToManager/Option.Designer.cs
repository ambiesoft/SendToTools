namespace SendToManager
{
    partial class Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Option));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnLVColor1 = new System.Windows.Forms.Button();
            this.btnLVColor2 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLVColor1
            // 
            resources.ApplyResources(this.btnLVColor1, "btnLVColor1");
            this.btnLVColor1.Name = "btnLVColor1";
            this.btnLVColor1.UseVisualStyleBackColor = true;
            this.btnLVColor1.Click += new System.EventHandler(this.btnLVColor1_Click);
            // 
            // btnLVColor2
            // 
            resources.ApplyResources(this.btnLVColor2, "btnLVColor2");
            this.btnLVColor2.Name = "btnLVColor2";
            this.btnLVColor2.UseVisualStyleBackColor = true;
            this.btnLVColor2.Click += new System.EventHandler(this.btnLVColor2_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Option
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnLVColor2);
            this.Controls.Add(this.btnLVColor1);
            this.Name = "Option";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Option_FormClosed);
            this.Load += new System.EventHandler(this.Option_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        internal System.Windows.Forms.Button btnLVColor1;
        internal System.Windows.Forms.Button btnLVColor2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}