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
        readonly string initialFile_;
        public FormMain(string initialFile)
        {
            initialFile_ = initialFile;
            InitializeComponent();

            txtFilename.Text = initialFile;
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
            Int64 multi = 1;
            string result = txtSize.Text.ToUpper();

            if(false)
            { }
            else if (result.EndsWith("K"))
            {
                multi = 1000;
                result = result.Substring(0, result.Length - 1);
            }
            else if (result.EndsWith("KI"))
            {
                multi = 1024;
                result = result.Substring(0, result.Length - 2);
            }

            else if (result.EndsWith("M"))
            {
                multi = 1000 * 1000;
                result = result.Substring(0, result.Length - 1);
            }
            else if (result.EndsWith("MI"))
            {
                multi = 1024 * 1024;
                result = result.Substring(0, result.Length - 2);
            }

            else if (result.EndsWith("G"))
            {
                multi = 1000 * 1000 * 1000;
                result = result.Substring(0, result.Length - 1);
            }
            else if (result.EndsWith("GI"))
            {
                multi = 1024 * 1024 * 1024;
                result = result.Substring(0, result.Length - 2);
            }



            Int64 ret;
            if (!Int64.TryParse(result, out ret))
                return -1;
            return ret * multi;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Int64 fsize = GetCreatingFileSize();
            if(fsize==-1)
            {
                CppUtils.CenteredMessageBox(Properties.Resources.INVALID_FILE_SIZE,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (fsize < 0)
            {
                CppUtils.CenteredMessageBox(Properties.Resources.FILESIZE_NOTBE_MINUS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (fsize > ((Int64)2 * 1024 * 1024 * 1024))
            {
                if (DialogResult.Yes != CppUtils.CenteredMessageBox(
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

                if (File.Exists(txtFilename.Text))
                {
                    if (DialogResult.Yes != CppUtils.CenteredMessageBox(
                        string.Format(Properties.Resources.ALREADY_EXISTS_DO_YOU_WANT_TO_OVERRIDE, txtFilename.Text),
                        Application.ProductName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                FileStream fs = File.Create(
                    txtFilename.Text,
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
                CppUtils.CenteredMessageBox(Properties.Resources.FILE_CREATED,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CppUtils.CenteredMessageBox(
                    ex.Message,
                    ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                if (DialogResult.OK != dlg.ShowDialog())
                    return;

                txtFilename.Text = dlg.FileName;
            }            
        }

        private void txtSize_TextChanged(object sender, EventArgs e)
        {
            Int64 actualSize = GetCreatingFileSize();
            if (actualSize >= 0)
                lblActualSize.Text = string.Format(Properties.Resources.SIZE_IN_BYTE_IS, actualSize);
            else
                lblActualSize.Text = Properties.Resources.INVALID_FILE_SIZE;
        }
    }
}