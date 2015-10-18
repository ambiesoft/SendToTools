using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace PathToClipboard
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

            try
            {
                Clipboard.SetText(args[0]);
               	int waitspan = 5*1000;
                NotifyIcon ni = new NotifyIcon();
                ni.BalloonTipTitle = Application.ProductName;
                ni.BalloonTipText = Properties.Resources.CLIPBOARDSET;

                /*
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
                foreach (string resourceName in asm.GetManifestResourceNames())
                {
                    MessageBox.Show(resourceName);
                }
                System.IO.Stream stream = asm.GetManifestResourceStream("PathToClipboard.Icon.icon.ico");  
                System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                */

                ni.Icon = Properties.Resources.icon;
                
                ni.Text = Application.ProductName;
                ni.Visible = true;
                ni.ShowBalloonTip(waitspan);
                System.Threading.Thread.Sleep(waitspan);
                ni.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    e.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}