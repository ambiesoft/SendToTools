using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using SendtoCommon;

namespace RunasFile
{
    static class Program
    {
        static bool IsAdmin()
        {
            //if(Environment.OSVersion.Version.Major <= 5)
            //{ // XP
            //    return true;
            //}

            //WindowsIdentity identity = WindowsIdentity.GetCurrent();
            //WindowsPrincipal principal = new WindowsPrincipal(identity);
            //return principal.IsInRole(WindowsBuiltInRole.Administrator);

            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

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



            if (args[0] == "/run")
            {
                if (args.Length < 2)
                {
                    MessageBox.Show(string.Format(Properties.Resources.NO_ARGUMENTS_AFTER_RUN),
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
                    return;
                }

                theArguments = CommonFunction.getAllArgs(2);


                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = CommonFunction.getAllArgs(theArguments, 0, true);
                startInfo.Arguments = CommonFunction.getAllArgs(theArguments, 1);
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
                    MessageBox.Show(ex.Message, Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                return;
            }
            else  // not with /run
            {
                theArguments = CommonFunction.getAllArgs(1);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.ExecutablePath;
                startInfo.UseShellExecute = true;
                startInfo.Verb = IsAdmin() ? null : "runas";
                startInfo.Arguments = "/run " + theArguments;
                startInfo.WorkingDirectory = System.IO.Directory.GetParent(CommonFunction.undq(CommonFunction.getAllArgs(theArguments, 0, true))).FullName; ;


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
                return;
            }


        }


    }
}