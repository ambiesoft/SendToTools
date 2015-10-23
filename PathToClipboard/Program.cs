using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace PathToClipboard
{
    static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string line in args)
            {
                sb.AppendLine(line);
            }

            try
            {
                Clipboard.SetText(sb.ToString());
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