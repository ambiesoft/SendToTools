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
            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.STR_NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (args.Length > 1)
            {
                for (int i = 0; i < args.Length; ++i)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(Application.ExecutablePath, "\""+args[i]+"\"");
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                }
                return;
            }
            //string theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            string theFileName = args[0];

            if (!System.IO.File.Exists(theFileName))
            {
                MessageBox.Show(String.Format(Properties.Resources.STR_FILE0NOTFOUNT, theFileName),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            DateTime now = DateTime.Now;
            System.IO.FileInfo fi = new System.IO.FileInfo(theFileName);
            fi.LastAccessTime = now;
            fi.LastWriteTime = now;

            showtip(5000, Application.ProductName, Properties.Resources.STR_TOUCHED,Properties.Resources.icon);
        }
    }
}