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

using Ambiesoft;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ChangeFileName
{
    static class Program
    {
        static internal bool SafeProcessStart(string s, bool showerrorbox)
        {
            try
            {
                System.Diagnostics.Process.Start(s);
                return true;
            }
            catch (System.Exception e)
            {
                if (showerrorbox)
                {
                    MessageBox.Show(e.Message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            return false;
        }



        static readonly internal string damemoji = "\\/:;*?\"<>|";
        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.NO_ARGUMENTS);
                sb.AppendLine();
                sb.AppendLine();

                sb.AppendLine(Properties.Resources.HELP);
                MessageBox.Show(
                    sb.ToString(),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }
            if (args.Length > 1)
            {
                StringBuilder sbNotFounds = new StringBuilder();
                for (int i = 0; i < args.Length; ++i)
                {
                    try
                    {
                        if(File.Exists(args[i]) || Directory.Exists(args[i]))
                            System.Diagnostics.Process.Start(Application.ExecutablePath, "\""+args[i]+"\"");
                        else
                        {
                            sbNotFounds.AppendLine(args[i]);
                        }
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                }

                if (sbNotFounds.Length != 0)
                {
                    MessageBox.Show(Properties.Resources.FOLLOWING_DOESNOT_EXIST + "\r\n\r\n" + sbNotFounds.ToString(),
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk);
                }
                return;
            }
            
            string theFileName = args[0];
            // theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            if (!File.Exists(theFileName) && !Directory.Exists(theFileName) )
            {
                MessageBox.Show(string.Format(Properties.Resources.FILE_NOT_FOUND, theFileName),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            //System.IO.FileInfo fi = new System.IO.FileInfo(theFileName.Trim());
            //string olddir = fi.Directory.FullName;
            //string oldname = fi.Name;
            //string oldext = fi.Extension;
            bool bIsFolder = Directory.Exists(theFileName);
            string newName = bIsFolder ? Path.GetFileName(theFileName) : Path.GetFileNameWithoutExtension(theFileName);

            //if (!string.IsNullOrEmpty(oldext))
            //{
            //    newName = newName.Replace(oldext, "");
            //}

            do
            {
                try
                {
                    FormMain fm = new FormMain();
                    AmbLib.SetFontAll(fm);

                    fm.txtName.Text = newName;
                    fm.txtName.Tag = theFileName;
                    if (DialogResult.OK != fm.ShowDialog())
                        return;

                    //newName = fm.txtName.Text;

                    //if (!RenameIt(olddir, newName, oldname, oldext))
                    //    continue;

                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                }
            } while (true);
        }

        internal static bool RenameIt(IWin32Window win, string oldfull, string newName)
        {
            //if (oldname == (newName + oldext))
            //{
            //    return true;
            //}

            //if (string.IsNullOrEmpty(oldext))
            //{
            //    newName = newName.TrimEnd(' ');
            //}

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show(Properties.Resources.ENTER_FILENAME,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return false;
            }

            if (-1 != newName.IndexOfAny(damemoji.ToCharArray()))
            {
                MessageBox.Show(Properties.Resources.FOLLOWING_UNABLE_FILENAME + Environment.NewLine + Environment.NewLine + damemoji,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return false;
            }

            string olddir = Path.GetDirectoryName(oldfull);
            string oldext = Directory.Exists(oldfull) ? string.Empty : Path.GetExtension(oldfull);
            string newfull = Path.Combine(olddir, newName + oldext);

            if (String.Compare(oldfull, newfull, true) == 0)
                return true;

            int ret = Ambiesoft.CppUtils.MoveFile(win, oldfull, newfull);
            return ret == 0;

        }

    }
}