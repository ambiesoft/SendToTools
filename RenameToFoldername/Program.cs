using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RenameToFoldername
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

            // string origfilename = @"C:\Documents and Settings\gator\デスクトップ\KKFOL\No.Starch.-.Autotools.2010.RETAiL.eBOOk-rebOOk\ddd.pdf";
            string origfilename = args[0];

            if (!System.IO.File.Exists(origfilename))
            {
                MessageBox.Show("ファイル " + origfilename + " は存在しません",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(origfilename);
                System.IO.DirectoryInfo di = fi.Directory;

                string toExt = fi.Extension;
                string toName = di.Name;

                fi.MoveTo(System.IO.Path.Combine(di.FullName, toName + toExt));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }
    }
}