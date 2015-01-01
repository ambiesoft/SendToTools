using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SendToSender
{
    static class Program
    {
        static string dowork(string arg)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(arg);

            string shortcutPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo),
                fi.Name.Substring(0, fi.Name.Length-fi.Extension.Length) + ".lnk");
            //ショートカットのリンク先

            string targetPath = arg;

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
                MessageBox.Show("No Arguments",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string ret = dowork(args[0]);
                MessageBox.Show("Created\r\n" + ret,
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