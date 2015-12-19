using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CopyFileContent
{
    static class Program
    {
        enum ConvertType
        {
            Text,
            Image,
        }
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

            string file="";
            ConvertType ct = ConvertType.Text;
            if (args.Length == 1)
            {
                file = args[0];
            }
            else if (args.Length == 2)
            {
                if (args[0] == "-t")
                    ct = ConvertType.Text;
                else if (args[0] == "-i")
                    ct = ConvertType.Image;
                else
                    throw new Exception(string.Format(Properties.Resources.UNKNOWN_OPTION, args[1]));

                file = args[1];

            }
            else
            {
                throw new Exception(Properties.Resources.TOO_MANY_OPTIONS);
            }

            try
            {
                if (ct == ConvertType.Text)
                {
                    string all = System.IO.File.ReadAllText(file);
                    Clipboard.SetText(all);
                }
                else
                {
                    throw new Exception("Unimplemented");
                }

                NotifyIcon ni = new NotifyIcon();
                ni.BalloonTipTitle = Application.ProductName;
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