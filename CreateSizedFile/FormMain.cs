//BSD 2-Clause License
//
//Copyright (c) 2017, Ambiesoft
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this
//  list of conditions and the following disclaimer.
//
//* Redistributions in binary form must reproduce the above copyright notice,
//  this list of conditions and the following disclaimer in the documentation
//  and/or other materials provided with the distribution.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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
                CppUtils.CenteredMessageBox("OK",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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