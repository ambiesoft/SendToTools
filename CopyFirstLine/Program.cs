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

using Ambiesoft;

namespace CopyFirstLine
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length < 1)
            {
                CppUtils.Alert(Properties.Resources.NO_ARG);
                return;
            }

            try
            {
                string[] lines = System.IO.File.ReadAllLines(args[0]);
                if (lines.Length == 0)
                {
                    CppUtils.Alert(Properties.Resources.NO_FILE_CONTENT);
                    return;
                }

                if (lines[0].Length == 0)
                {
                    CppUtils.Alert(Properties.Resources.EMPTY_FIRST_LINE);
                    return;

                }
                
                
                Clipboard.SetText(lines[0]);

                // https://www.flickr.com/photos/thotmeglynn/5161731232/sizes/q/

                NotifyIcon ni = new NotifyIcon();
                ni.BalloonTipTitle = Application.ProductName;
                ni.BalloonTipText = Properties.Resources.FIRST_LINE_IS_SET_ON_CLIPBOARD;
                ni.Icon = Properties.Resources.icon;

                ni.Text = Application.ProductName;
                ni.Visible = true;
                ni.ShowBalloonTip(5 * 1000);

                System.Threading.Thread.Sleep(5 * 1000);
                ni.Dispose();

            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex);
            }

        }
    }
}
