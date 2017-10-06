using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace RunWithArgs
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ProcessStartInfo si = new ProcessStartInfo();
            si.FileName = txtExe.Text;
            si.Arguments = txtArg.Text;
            if (chkRunas.Checked)
                si.Verb = "runas";
            si.WorkingDirectory = System.IO.Path.GetDirectoryName(txtExe.Text);
            
            try
            {
                Process.Start(si);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}