using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CreateSizedFile
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            args = new string[]{@"c:\t\"};
            if (args.Length < 1)
            {
                MessageBox.Show("引数がありません",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            // string shortcutfile = @"C:\Documents and Settings\xpenpen\SendTo\VCExpress.exe へのショートカット.lnk";
            string dir = args[0];

            if (!System.IO.Directory.Exists(dir))
            {
                MessageBox.Show("フォルダ " + dir + " は存在しません",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            FormMain f = new FormMain();
            f.dir_ = dir;
            if(DialogResult.OK != f.ShowDialog())
                return;
            Int64 fsize = f.FileSize;
            if (fsize < 0)
            {
                MessageBox.Show("ファイルサイズがマイナスです。",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }
            if (fsize > ((Int64)2 * 1024 * 1024 * 1024))
            {
                if (DialogResult.Yes != MessageBox.Show(string.Format("本当に {0}バイトのファイルを作成しますか？", fsize),
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    return;
                }
            }
            try
            {

                if (System.IO. File.Exists(f.Filename))
                {
                    if (DialogResult.Yes != MessageBox.Show(string.Format("ファイル{0}はすでに存在します。上書きしますか？", f.Filename),
                        Application.ProductName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                System.IO.FileStream fs = System.IO.File.Create(f.Filename, 1, System.IO.FileOptions.RandomAccess);
                fs.SetLength(fsize);
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}