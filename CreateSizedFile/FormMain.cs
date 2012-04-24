using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CreateSizedFile
{
    public partial class FormMain : Form
    {
        internal string dir_="";
        internal Int64 FileSize
        {
            get
            {
                Int64 ret;
                Int64.TryParse(txtSize.Text, out ret);
                return ret;
            }
        }

        internal string Filename
        {
            get 
            {
                if (Path.IsPathRooted(txtFilename.Text))
                    return txtFilename.Text;

                return dir_ + @"\" + txtFilename.Text; 
            }
        }
        public FormMain()
        {
            InitializeComponent();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            txtDir.Text = dir_;
        }

        private void chkRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandom.Checked && chkZero.Checked)
            {
                chkZero.Checked = false;
            }
        }

        private void chkZero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandom.Checked && chkZero.Checked)
            {
                chkRandom.Checked = false;
            }
        }
    }
}