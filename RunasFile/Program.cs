using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

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

        static string getAllArgs(int start)
        {
            return getAllArgs(Environment.CommandLine, start);
        }
        static string getAllArgs(string cmdline, int start)
        {
            return getAllArgs(cmdline, start, false);
        }
        static string getAllArgs(string cmdline, int start, bool bGetFirst)
        {
            cmdline = cmdline.TrimStart();
            string org = cmdline;

            char dq = '\0';
            int phase = 0;
            string result = string.Empty;
            int i = 0;
            for (i = 0; i < cmdline.Length; ++i)
            {
                char c = cmdline[i];
                if (phase == 0)
                {
                    if (c == '"' || c == '\'')
                    {
                        dq = c;
                    }
                    else
                    {
                        result += c;
                    }
                    phase = 1;
                }
                else if (phase == 1)
                {
                    if (dq != '\0')
                    {
                        if (dq == c)
                        {
                            break;
                        }
                        else
                        {
                            result += c;
                            continue;
                        }
                    }
                    else
                    {
                        if (char.IsWhiteSpace(c))
                        {
                            break;
                        }
                        else
                        {
                            result += c;
                            continue;
                        }
                    }
                }
            }

            string remain = string.Empty;
            if (cmdline.Length > i)
            {
                remain = cmdline.Substring(i + 1);
            }
            remain = remain.TrimStart();

            if (bGetFirst)
            {
                if (start <= 0)
                {
                    return result;
                }

                string prev = string.Empty;
                if (dq != '\0')
                {
                    prev += dq;
                    prev += result;
                    prev += dq;
                }
                else
                {
                    prev = result;
                }

                return prev + " " + getAllArgs(remain, start - 1, bGetFirst);
            }
            else
            {
                if (start <= 1)
                {
                    return remain;
                }

                return getAllArgs(remain, start - 1);
            }
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

                theArguments = getAllArgs(2);


                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = getAllArgs(theArguments, 0, true);
                startInfo.Arguments = getAllArgs(theArguments, 1);
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
                theArguments = getAllArgs(1);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Application.ExecutablePath;
                startInfo.UseShellExecute = true;
                startInfo.Verb = IsAdmin() ? null : "runas";
                startInfo.Arguments = "/run " + theArguments;
                startInfo.WorkingDirectory = System.IO.Directory.GetParent(undq(getAllArgs(theArguments, 0, true))).FullName; ;


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

        static string undq(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (s[0] == '"' || s[0] == '\'')
            {
                s = s.Trim(s[0]);
            }

            return s;
        }
    }
}