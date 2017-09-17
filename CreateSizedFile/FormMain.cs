using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ambiesoft;

namespace CreateSizedFile
{
    public partial class FormMain : Form
    {
        internal string dir_="";
      

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

        Int64 GetCreatingFileSize()
        {
            Int64 ret;
            if (!Int64.TryParse(txtSize.Text, out ret))
                return -1;
            return ret;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Int64 fsize = GetCreatingFileSize();
            if (fsize < 0)
            {
                MessageBox.Show(Properties.Resources.FILESIZE_NOTBE_MINUS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }
            if (fsize > ((Int64)2 * 1024 * 1024 * 1024))
            {
                if (DialogResult.Yes != MessageBox.Show(
                    string.Format(Properties.Resources.ARE_YOU_GOING_TO_CREATE, fsize),
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    return;
                }
            }
            try
            {

                if (File.Exists(Filename))
                {
                    if (DialogResult.Yes != MessageBox.Show(
                        string.Format(Properties.Resources.ALREADY_EXISTS_DO_YOU_WANT_TO_OVERRIDE, Filename),
                        Application.ProductName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                FileStream fs = File.Create(
                    Filename, 
                    1,
                    System.IO.FileOptions.RandomAccess);
                if (chkRandom.Checked)
                {
                    System.Random r = new Random();
                    for (long i = 0; i < fsize; ++i)
                    {
                        byte[] b = new byte[1];
                        r.NextBytes(b);
                        fs.WriteByte(b[0]);
                    }
                }
                else if (chkZero.Checked)
                {
                    for (long i = 0; i < fsize; ++i)
                    {
                        fs.WriteByte(0);
                    }
                }
                else
                {
                    fs.SetLength(fsize);
                }
                fs.Close();
                AmbLib.Info("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    }
}