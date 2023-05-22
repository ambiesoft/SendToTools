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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChangeFileName
{
    static class Program
    {
        static readonly int MAX_CONFIRMLESS_OPEN_COUNT = 16;

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
                if(arg=="-h" || arg=="/h" || arg=="--help")
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Properties.Resources.HELP);
                    CppUtils.Alert(sb.ToString());
                    return;
                }
                else if(arg=="/run")
                {
                    run_ = true;
                    continue;
                }
                args.Add(arg);
            }

            if (args.Count > MAX_CONFIRMLESS_OPEN_COUNT)
            {
                if (DialogResult.Yes != CppUtils.YesOrNo(
                    string.Format(Properties.Resources.ARE_YOU_SURE_TO_OPEN_S_FILES,
                    args.Count),
                    MessageBoxDefaultButton.Button2))
                {
                    return;
                }
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
            
            string theFileName = args[0].Trim();
            if (!File.Exists(theFileName) && !Directory.Exists(theFileName) )
            {
                CppUtils.Alert(string.Format(Properties.Resources.FILE_NOT_FOUND, theFileName));
                return;
            }

            bool bIsFolder = Directory.Exists(theFileName);
            string newName = bIsFolder ? Path.GetFileName(theFileName) : Path.GetFileNameWithoutExtension(theFileName);

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

                    break;
                }
                catch (Exception e)
                {
                    CppUtils.Alert(e.Message);
                }
            } while (true);
        }

        static string GetFullPathWithCase(string oldfull)
        {
            string dir = Path.GetDirectoryName(oldfull);
            string name = Path.GetFileName(oldfull);
            if (File.Exists(oldfull))
                return Directory.GetFiles(dir, name).FirstOrDefault();
            return Directory.GetDirectories(dir, name).FirstOrDefault();
        }

        internal static bool checkPathLength(string path)
        {
            if (path.Length > 260)
            {
                if (DialogResult.Yes != CppUtils.YesOrNo(Properties.Resources.STR_FILENAME_LENGTH_IS_OVER_260,
                    MessageBoxDefaultButton.Button2))
                {
                    return false;
                }
            }
            return true;
        }
        internal static bool RenameIt(IWin32Window win, string oldfull, string newName)
        {
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

            if (!checkPathLength(newfull))
                return false;

            if (String.Compare(oldfull, newfull, true) == 0)
            {
                string oldfullcase = GetFullPathWithCase(oldfull);

                // user supplied path is not same as the physical path
                if (String.Compare(oldfullcase, newfull, true) == 0 &&
                    String.Compare(oldfullcase, newfull, false) != 0)
                {
                    oldfull = oldfullcase;

                    // only case is different
                    if (Path.GetDirectoryName(oldfull) != Path.GetDirectoryName(newfull))
                        return false;

                    // use ntfs transaction
                    string tmp;
                    for (int i = 0; ; i++)
                    {
                        tmp = oldfull + "trans" + (i == 0 ? "" : i.ToString());
                        if (File.Exists(tmp) || Directory.Exists(tmp))
                            continue;
                        break;
                    }
                    List<KeyValuePair<string, string>> srcdests = new List<KeyValuePair<string, string>>();
                    srcdests.Add(new KeyValuePair<string, string>(oldfull, tmp));
                    srcdests.Add(new KeyValuePair<string, string>(tmp, newfull));

                    if (!CppUtils.MoveFileAtomic(srcdests))
                        return false;

                    // Refresh Desktop
                    string exeRD = Path.Combine(
                        Path.GetDirectoryName(Application.ExecutablePath),
                        "RefreshDesktop.exe");
                    if (File.Exists(exeRD))
                    {
                        Process.Start(exeRD, "--wait 3");
                    }
                    return true;
                }
                return true;
            }
            int ret = Ambiesoft.CppUtils.MoveFile(win, oldfull, newfull);
            return ret == 0;
        }
    }
}