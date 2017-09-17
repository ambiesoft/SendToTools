using Ambiesoft;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CreateSizedFile
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            args = new string[]{@"c:\t\"};
            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            string dir = args[0];

            if (!System.IO.Directory.Exists(dir))
            {
                MessageBox.Show(string.Format(Properties.Resources.FOLDER_NOT_EXIST,dir),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            FormMain f = new FormMain();
            AmbLib.SetFontAll(f);
            f.dir_ = dir;
            if(DialogResult.OK != f.ShowDialog())
                return;
        }
    }
}