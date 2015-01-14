using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SendToSender
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(txtProgram.Text);
                int li = fi.Name.LastIndexOf(fi.Extension);
                txtName.Text = fi.Name.Substring(0, li);
            }
            catch (Exception) { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK == this.DialogResult)
            {
                if (string.IsNullOrEmpty(txtName.Text) ||
                    string.IsNullOrEmpty(txtProgram.Text))
                {
                    MessageBox.Show("error",
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }


    }
}