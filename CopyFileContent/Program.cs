using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CopyFileContent
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
                MessageBox.Show(Properties.Resources.NO_ARG,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            try
            {
                string[] lines = System.IO.File.ReadAllLines(args[0]);
                // Clipboard.SetText(lines[0],TextDataFormat.);

                NotifyIcon ni = new NotifyIcon();
                ni.BalloonTipTitle = "btttt";
                ni.BalloonTipText = Properties.Resources.CLIPBOARDSET;
                ni.Icon = Properties.Resources.icon;

                ni.Text = Application.ProductName;
                ni.Visible = true;
                ni.ShowBalloonTip(5*1000);

                System.Threading.Thread.Sleep(5 * 1000);
                ni.Dispose();

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