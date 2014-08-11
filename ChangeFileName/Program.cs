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

        static readonly string damemoji = "\\/:,;*?\"<>|";
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
                MessageBox.Show("ファイル " + theFileName + " は存在しません",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            System.IO.FileInfo fi = new System.IO.FileInfo(theFileName);

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
                        MessageBox.Show("ファイル名を入力してください",
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk);

                        continue;
                    }

                    if ( -1 != newName.IndexOfAny(damemoji.ToCharArray()))
                    {
                        MessageBox.Show("以下の文字はファイル名には使えません\r\n" + damemoji,
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