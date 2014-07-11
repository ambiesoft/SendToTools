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
                MessageBox.Show(String.Format("ƒtƒ@ƒCƒ‹ {0} ‚Í‘¶Ý‚µ‚Ü‚¹‚ñ", theFileName),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            DateTime now = DateTime.Now;
            System.IO.FileInfo fi = new System.IO.FileInfo(theFileName);
            fi.LastAccessTime = now;
            fi.LastWriteTime = now;
        }
    }
}