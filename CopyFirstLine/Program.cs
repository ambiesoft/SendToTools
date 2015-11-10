using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CopyFirstFile
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
                if (lines.Length == 0)
                {
                    MessageBox.Show(Properties.Resources.NO_FILE_CONTENT,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                    return;
                }

                if (lines[0].Length == 0)
                {
                    MessageBox.Show(Properties.Resources.EMPTY_FIRST_LINE,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
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
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    }
}
