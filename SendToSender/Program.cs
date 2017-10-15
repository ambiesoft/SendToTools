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
using System.IO;

namespace SendToSender
{
    static class Program
    {
        static string dowork(string name,string program,string arguments)
        {
            // System.IO.FileInfo fi = new System.IO.FileInfo(arg);

            string shortcutPath = Path.Combine(
                System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo),
                name + ".lnk");

            string targetPath = program;

            //WshShellを作成
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
            object shell = Activator.CreateInstance(t);

            //WshShortcutを作成
            object shortcut = t.InvokeMember("CreateShortcut",
                System.Reflection.BindingFlags.InvokeMethod, null, shell,
                new object[] { shortcutPath });

            //リンク先
            t.InvokeMember("TargetPath",
                System.Reflection.BindingFlags.SetProperty, null, shortcut,
                new object[] { targetPath });
            //アイコンのパス
            t.InvokeMember("IconLocation",
                System.Reflection.BindingFlags.SetProperty, null, shortcut,
                new object[] { targetPath + ",0" });
            //その他のプロパティも同様に設定できるため、省略

            //ショートカットを作成
            t.InvokeMember("Save",
                System.Reflection.BindingFlags.InvokeMethod,
                null, shortcut, null);

            //後始末
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);

            return shortcutPath;
        }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.NO_ARGUMENT,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMain frm = new FormMain();
            frm.txtProgram.Text = args[0];
            if (DialogResult.OK != frm.ShowDialog())
                return;
            
            try
            {
                string ret = dowork(frm.txtName.Text, frm.txtProgram.Text, frm.txtArguments.Text);
                MessageBox.Show(Properties.Resources.SHORTCUT_CREATED + "\r\n" + ret,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}