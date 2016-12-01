using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendtoCommon;
using System.Diagnostics;

namespace RunasArg
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string theArguments;
            if (args.Length < 1)
            {
                MessageBox.Show(Properties.Resources.NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Shortcut Files|*.lnk";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            theArguments = CommonFunction.getAllArgs(1);
            if(string.IsNullOrEmpty(theArguments))
            {
                MessageBox.Show(Properties.Resources.NO_ARGUMENTS,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = ofd.FileName;
            startInfo.Arguments = theArguments;
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";
            startInfo.Arguments = theArguments;
            startInfo.WorkingDirectory = System.IO.Directory.GetParent(CommonFunction.undq(CommonFunction.getAllArgs(theArguments, 0, true))).FullName;


            try
            {
                Process proc = Process.Start(startInfo);
                // proc.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain());
        }
    }
}
