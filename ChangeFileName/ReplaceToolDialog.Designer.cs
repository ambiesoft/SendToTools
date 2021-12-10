
namespace ChangeFileName
{
    partial class ReplaceToolDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceToolDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRegEx = new System.Windows.Forms.Label();
            this.txtRegEx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTestInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.epRegEx = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtReplacement = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegExName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.epRegEx)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblRegEx
            // 
            resources.ApplyResources(this.lblRegEx, "lblRegEx");
            this.lblRegEx.Name = "lblRegEx";
            // 
            // txtRegEx
            // 
            resources.ApplyResources(this.txtRegEx, "txtRegEx");
            this.txtRegEx.Name = "txtRegEx";
            this.txtRegEx.TextChanged += new System.EventHandler(this.txtRegEx_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtTestInput
            // 
            resources.ApplyResources(this.txtTestInput, "txtTestInput");
            this.txtTestInput.Name = "txtTestInput";
            this.txtTestInput.TextChanged += new System.EventHandler(this.txtTestInput_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtResult
            // 
            resources.ApplyResources(this.txtResult, "txtResult");
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            // 
            // epRegEx
            // 
            this.epRegEx.ContainerControl = this;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtReplacement
            // 
            resources.ApplyResources(this.txtReplacement, "txtReplacement");
            this.txtReplacement.Name = "txtReplacement";
            this.txtReplacement.TextChanged += new System.EventHandler(this.txtReplacement_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtRegExName
            // 
            resources.ApplyResources(this.txtRegExName, "txtRegExName");
            this.txtRegExName.Name = "txtRegExName";
            this.txtRegExName.TextChanged += new System.EventHandler(this.txtRegEx_TextChanged);
            // 
            // ReplaceToolDialog
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtTestInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReplacement);
            this.Controls.Add(this.txtRegExName);
            this.Controls.Add(this.txtRegEx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblRegEx);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MinimizeBox = false;
            this.Name = "ReplaceToolDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.ReplaceToolDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.epRegEx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRegEx;
        private System.Windows.Forms.TextBox txtRegEx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTestInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ErrorProvider epRegEx;
        private System.Windows.Forms.TextBox txtReplacement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRegExName;
        private System.Windows.Forms.Label label4;
    }
}