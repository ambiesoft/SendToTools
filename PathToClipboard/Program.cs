using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PathToClipboard
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

            try
            {
                Clipboard.SetText(args[0]);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}