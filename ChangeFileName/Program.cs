using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChangeFileName
{
    static class Program
    {
        static internal bool SafeProcessStart(string s, bool showerrorbox)
        {
            try
            {
                System.Diagnostics.Process.Start(s);
                return true;
            }
            catch (System.Exception e)
            {
                if (showerrorbox)
                {
                    MessageBox.Show(e.Message,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            return false;
        }

        static readonly internal string damemoji = "\\/:;*?\"<>|";
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
            
            string theFileName = args[0];
            // theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            if (!System.IO.File.Exists(theFileName))
            {
                MessageBox.Show(string.Format(Properties.Resources.FILE_NOT_FOUND, theFileName),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            System.IO.FileInfo fi = new System.IO.FileInfo(theFileName.Trim());

            string oldname = fi.Name;
            string oldext = fi.Extension;
            string newName = fi.Name;
            if (!string.IsNullOrEmpty(oldext))
            {
                newName = newName.Replace(oldext, "");
            }
            do
            {
                try
                {
                    FormMain fm = new FormMain();
                    fm.textName.Text = newName;
                    fm.textName.Tag = theFileName;
                    if (DialogResult.OK != fm.ShowDialog())
                        return;

                    newName = fm.textName.Text;

                    if (oldname == (newName + oldext))
                    {
                        return;
                    }

                    if ( string.IsNullOrEmpty(oldext ) )
                    {
                        newName = newName.TrimEnd(' ');
                    }

                    if (string.IsNullOrEmpty(newName))
                    {
                        MessageBox.Show(Properties.Resources.ENTER_FILENAME,
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk);

                        continue;
                    }

                    if ( -1 != newName.IndexOfAny(damemoji.ToCharArray()))
                    {
                        MessageBox.Show(Properties.Resources.FOLLOWING_UNABLE_FILENAME + Environment.NewLine + Environment.NewLine  + damemoji,
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk);

                        continue;
                    }

                    string dir = fi.Directory.FullName;
                    fi.MoveTo(dir + @"\" + newName + oldext);
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