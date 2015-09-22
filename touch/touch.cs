using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace touch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                doit(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }

        static void showtip(int waitspan, string title, string tiptext, System.Drawing.Icon icon)
        {
            NotifyIcon ni = new NotifyIcon();
            ni.BalloonTipTitle = title;
            ni.BalloonTipText = tiptext;

            /*
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
            foreach (string resourceName in asm.GetManifestResourceNames())
            {
                MessageBox.Show(resourceName);
            }
            System.IO.Stream stream = asm.GetManifestResourceStream("PathToClipboard.Icon.icon.ico");  
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            */

            ni.Icon = icon;

            ni.Text = title;
            ni.Visible = true;
            ni.ShowBalloonTip(waitspan);
            System.Threading.Thread.Sleep(waitspan);
            ni.Dispose();
        }
        static void doit(string[] args)
        {
            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.STR_NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }
            if (args.Length > 1)
            {
                for (int i = 0; i < args.Length; ++i)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(Application.ExecutablePath, "\""+args[i]+"\"");
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                }
                return;
            }
            //string theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            string theFileName = args[0];

            if (!System.IO.File.Exists(theFileName))
            {
                MessageBox.Show(String.Format(Properties.Resources.STR_FILE0NOTFOUNT, theFileName),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            DateTime now = DateTime.Now;
            System.IO.FileInfo fi = new System.IO.FileInfo(theFileName);
            fi.LastAccessTime = now;
            fi.LastWriteTime = now;

            showtip(5000, Application.ProductName, Properties.Resources.STR_TOUCHED,Properties.Resources.icon);
        }
    }
}