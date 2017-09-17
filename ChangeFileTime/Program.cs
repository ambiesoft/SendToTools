using Ambiesoft;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChangeFileTime
{
    static class Program
    {
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new FormMain());
            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            //string theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            string theFileName = args[0];

            if (!System.IO.File.Exists(theFileName))
            {
                MessageBox.Show(string.Format(Properties.Resources.FILE_NOT_EXIST,theFileName),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }


            System.IO.FileInfo fi = new System.IO.FileInfo(theFileName);
            DateTime oldCreationTime = fi.CreationTime;
            DateTime oldLATime = fi.LastAccessTime;
            DateTime oldLWTime = fi.LastWriteTime;


            do
            {
                try
                {
                    FormMain fm = new FormMain();
                    AmbLib.SetFontAll(fm);

                    fm.Text = Application.ProductName;
                    fm.txtFileName.Text = theFileName;
                    fm.dtpLWTime.Tag = oldLWTime;
                    fm.dtpLWTime.Value = oldLWTime;
                    if (DialogResult.OK != fm.ShowDialog())
                    {
                        return;
                    }

                    if (oldLWTime != fm.dtpLWTime.Value)
                    {
                        fi.LastWriteTime = fm.dtpLWTime.Value;
                    }

                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                }
            } while (true);
        }        
    }
}