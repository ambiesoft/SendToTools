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

                fi.MoveTo(dir + @"\" + newname);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }
    }
}