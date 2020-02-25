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
                    CppUtils.Fatal(e.Message);
                }
            }

            return false;
        }


        internal static bool run_;
        static readonly internal string damemoji = "\\/:;*?\"<>|";
        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        static void Main(string[] argsOriginal)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (argsOriginal.Length < 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.NO_ARGUMENTS);
                sb.AppendLine();
                sb.AppendLine();

                sb.AppendLine(Properties.Resources.HELP);
                CppUtils.Alert(sb.ToString());
                return;
            }

            List<string> args = new List<string>();
            foreach(string arg in argsOriginal)
            {
                if(arg=="/run")
                {
                    run_ = true;
                    continue;
                }
                args.Add(arg);
            }
            
            if (args.Count > 1)
            {
                StringBuilder sbNotFounds = new StringBuilder();
                for (int i = 0; i < args.Count; ++i)
                {
                    try
                    {
                        if (File.Exists(args[i]) || Directory.Exists(args[i]))
                        {

                            System.Diagnostics.Process.Start(
                                Application.ExecutablePath, 
                                (run_ ? "/run ": "") +  "\"" + args[i] + "\"");
                        }
                        else
                        {
                            sbNotFounds.AppendLine(args[i]);
                        }
                    }
                    catch (System.Exception e)
                    {
                        CppUtils.Fatal(e.Message);
                    }
                }

                if (sbNotFounds.Length != 0)
                {
                    CppUtils.Alert(Properties.Resources.FOLLOWING_DOESNOT_EXIST + "\r\n\r\n" + sbNotFounds.ToString());
                }
                return;
            }
            
            string theFileName = args[0];
            // theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            if (!File.Exists(theFileName) && !Directory.Exists(theFileName) )
            {
                CppUtils.Alert(string.Format(Properties.Resources.FILE_NOT_FOUND, theFileName));
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
                    CppUtils.Alert(e.Message);
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

            if(!File.Exists(oldfull) && !Directory.Exists(oldfull))
            {
                CppUtils.Alert(string.Format(Properties.Resources.SOURCEFILE_NOT_FOUND, oldfull));
                return false;
            }

            if (string.IsNullOrEmpty(newName))
            {
                CppUtils.Alert(Properties.Resources.ENTER_FILENAME);
                return false;
            }

            if (-1 != newName.IndexOfAny(damemoji.ToCharArray()))
            {
                CppUtils.Alert(Properties.Resources.FOLLOWING_UNABLE_FILENAME + Environment.NewLine + Environment.NewLine + damemoji);
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