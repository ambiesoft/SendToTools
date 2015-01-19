using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace RunasFile
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string theFileName;
            if (args.Length < 1)
            {
                MessageBox.Show("No arguments",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }
            if (args.Length == 1)
            {
                theFileName = args[0];
                if (!System.IO.File.Exists(theFileName))
                {
                    MessageBox.Show(theFileName + " not exists",
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                    return;
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.ExecutablePath;
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
                startInfo.Arguments = "/run \"" + theFileName + "\"";
                startInfo.WorkingDirectory = System.IO.Directory.GetParent(theFileName).FullName; ;
                

                try
                {
                    Process proc = Process.Start(startInfo);
                    // proc.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return;

            }
            else if (args.Length == 2)
            {
                if (args[0] != "/run")
                {
                    MessageBox.Show("args[0] is not /run",
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                    return;
                }
                theFileName = args[1];



                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = theFileName;
                startInfo.UseShellExecute = true;
                startInfo.Verb = "open";
                //startInfo.Arguments = "start \"\" \"" + theFileName + "\"";
                //startInfo.Arguments = theFileName;

                try
                {
                    Process proc = Process.Start(startInfo);
                    // proc.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return;
            }
            else
            {
                MessageBox.Show("Too many arguments",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;

            }
        }
    }
}