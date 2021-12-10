using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Ambiesoft;

namespace ChangeFileName
{
    public partial class ReplaceToolDialog : Form
    {
        public ReplaceToolDialog(string testInput, RegexItem itemToEdit)
        {
            InitializeComponent();

            txtTestInput.Text = testInput;
            if (itemToEdit != null)
            {
                txtRegExName.Text = itemToEdit.Name;
                txtRegEx.Text = itemToEdit.RegexString;
                txtReplacement.Text = itemToEdit.Replacement;
            }
        }
        public ReplaceToolDialog(string testInput) : this(testInput, null) { }
        public string RegexName
        {
            get { return txtRegExName.Text; }
        }
        public string RegExString
        {
            get { return txtRegEx.Text; }
        }
        public string RegExReplacement
        {
            get { return txtReplacement.Text; }
        }
        void update()
        {
            try
            {
                Regex reg = new Regex(txtRegEx.Text);
                btnOK.Enabled = true;
                epRegEx.SetError(lblRegEx, string.Empty);

                txtResult.Text =
                    reg.Replace(txtTestInput.Text, txtReplacement.Text);
            }
            catch (Exception ex)
            {
                btnOK.Enabled = false;
                epRegEx.SetError(lblRegEx, ex.Message);
                return;
            }
        }
        private void txtRegEx_TextChanged(object sender, EventArgs e)
        {
            update();
        }

        private void txtReplacement_TextChanged(object sender, EventArgs e)
        {
            update();
        }

        private void ReplaceToolDialog_Shown(object sender, EventArgs e)
        {
            txtRegExName.Focus();
        }

        private void txtTestInput_TextChanged(object sender, EventArgs e)
        {
            update();
        }
    }
}
