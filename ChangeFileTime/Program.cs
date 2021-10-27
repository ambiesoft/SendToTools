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

namespace ChangeFileTime
{
    static class Program
    {
        static readonly int MAX_CONFIRMLESS_OPEN_COUNT = 16;

        [STAThread]
        static void Main(String[] argsOriginal)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new FormMain());
            if (argsOriginal.Length < 1)
            {
                CppUtils.Alert(Properties.Resources.NO_ARGUMENTS);
                return;
            }

            List<string> args = new List<string>();
            foreach (string arg in argsOriginal)
            {
                //if (arg == "-h" || arg == "/h" || arg == "--help")
                //{
                //    StringBuilder sb = new StringBuilder();
                //    sb.AppendLine(Properties.Resources.HELP);
                //    CppUtils.Alert(sb.ToString());
                //    return;
                //}
                //if (arg == "/run")
                //{
                //    run_ = true;
                //    continue;
                //}
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
                                "\"" + args[i] + "\"");
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



            //string theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            string theFileName = args[0];

            if (!System.IO.File.Exists(theFileName) )//&& !System.IO.Directory.Exists(theFileName))
            {
                CppUtils.Alert(string.Format(Properties.Resources.FILE_NOT_EXIST, theFileName));
                return;
            }



            try
            {
                FormMain fm = new FormMain(theFileName);
                fm.ShowDialog();
            }
            catch (Exception e)
            {
                CppUtils.Alert(e);
            }

        }
    }
}