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

namespace ShortcutRenamer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string ballonmessage = "Unknown Error";

            if (args.Length < 1)
            {
                MessageBox.Show("引数がありません",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            // string shortcutfile = @"C:\Documents and Settings\xpenpen\SendTo\VCExpress.exe へのショートカット.lnk";
            string shortcutfile = args[0];

            if (!System.IO.File.Exists(shortcutfile))
            {
                MessageBox.Show("ファイル " + shortcutfile + " は存在しません",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(shortcutfile);
                string dir = fi.Directory.FullName;
                string oldname = fi.Name;

                string newname = oldname.Replace(".exe へのショートカット", "");

                if (newname == oldname)
                {
                    ballonmessage = "newname = oldname";
                }
                else
                {
                    fi.MoveTo(dir + @"\" + newname);
                    ballonmessage = "Succeeded";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }

            NotifyIcon ni = new NotifyIcon();
            ni.BalloonTipTitle = Application.ProductName;
            ni.BalloonTipText = ballonmessage;
            ni.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            ni.Text = Application.ProductName;
            ni.Visible = true;
            ni.ShowBalloonTip(5000);
            System.Threading.Thread.Sleep(5000);
            ni.Dispose();
        }
    }
}