﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChangeFileName
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

            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(theFileName);
                
                string oldname = fi.Name;
                string oldext = fi.Extension;
                string newName = fi.Name;
                if ( !string.IsNullOrEmpty(oldext) )
                {
                    newName = newName.Replace(oldext, "");
                }

                if (!Ambiesoft.GetTextDialog.DoModalDialog(null,
                    "ファイル名を変更",
                    "ファイル名",
                    ref newName))
                {
                    return;
                }

                if (oldname == (newName + oldext))
                {
                    return;
                }

                string dir = fi.Directory.FullName;
                fi.MoveTo(dir + @"\" + newName + oldext);
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