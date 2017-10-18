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

using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace touch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                doit(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }

        static void showtip(int waitspan, string title, string tiptext, System.Drawing.Icon icon)
        {
            NotifyIcon ni = new NotifyIcon();
            ni.BalloonTipTitle = title;
            ni.BalloonTipText = tiptext;

            /*
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
            foreach (string resourceName in asm.GetManifestResourceNames())
            {
                MessageBox.Show(resourceName);
            }
            System.IO.Stream stream = asm.GetManifestResourceStream("PathToClipboard.Icon.icon.ico");  
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            */

            ni.Icon = icon;

            ni.Text = title;
            ni.Visible = true;
            ni.ShowBalloonTip(waitspan);
            System.Threading.Thread.Sleep(waitspan);
            ni.Dispose();
        }
        static void doit(string[] args)
        {
            bool recursive;
            int depth;
            bool touchfolder;
            var p = new OptionSet() {
                    { 
                        "r|recursive", 
                        "Touch recursively",
                        v => { recursive = v!=null;}
                    },
                    { 
                        "d|depth=", 
                        "Directory depth of recursive operation, 0 for specifying only arg itself, -1 for inifinite depth.",
                        v => { depth=int.Parse(v);}
                    },
                    { 
                        "f|folder", 
                        "Touch folders as well as files",
                        v => { touchfolder = v!=null;}
                    }
                };

            List<string> extra = p.Parse(args);
            if (extra.Count < 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.STR_NO_ARGUMENTS);
                sb.AppendLine();
                sb.AppendLine();
                StringWriter sw = new StringWriter(sb);
                p.WriteOptionDescriptions(sw);

                throw new Exception(sb.ToString());
            }
            //if (args.Length > 1)
            //{
            //    for (int i = 0; i < args.Length; ++i)
            //    {
            //        try
            //        {
            //            System.Diagnostics.Process.Start(Application.ExecutablePath, "\""+args[i]+"\"");
            //        }
            //        catch (System.Exception e)
            //        {
            //            MessageBox.Show(e.Message,
            //                    Application.ProductName,
            //                    MessageBoxButtons.OK,
            //                    MessageBoxIcon.Error);
            //        }
            //    }
            //    return;
            //}
            //string theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";

            DateTime now = DateTime.Now;
            foreach (string filename in extra)
            {
                dotouch(now, filename);
            }
            showtip(5000, Application.ProductName, Properties.Resources.STR_TOUCHED,Properties.Resources.icon);
        }

        static List<string> untouchabled_ = new List<string>();
        static void dotouch(DateTime now, string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    FileInfo fi = new FileInfo(filename);
                    fi.LastAccessTime = now;
                    fi.LastWriteTime = now;
                }
                else if (Directory.Exists(filename))
                {
                    var di = new DirectoryInfo(filename);
                    di.LastAccessTime = now;
                    di.LastWriteTime = now;
                }
                else
                {
                    untouchabled_.Add(filename);
                }
            }
            catch
            {
                untouchabled_.Add(filename);
            }
        }
    }
}