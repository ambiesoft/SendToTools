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