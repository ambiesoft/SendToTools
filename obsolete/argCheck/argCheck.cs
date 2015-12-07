using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace argCheck
{
    static class argCheck
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("total length = " + System.Environment.CommandLine.Length);
            sb.AppendLine("arg count = " + args.Length);

            foreach (string item in args)
            {
                sb.AppendLine(item);
            }


            MessageBox.Show(sb.ToString(),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}