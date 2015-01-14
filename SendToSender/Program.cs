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