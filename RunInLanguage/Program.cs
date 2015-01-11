using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace RunInLanguage
{
    static class Program
    {
        static void dowork(string[] args)
        {
            if (args.Length < 2)
            {
                MessageBox.Show(Properties.Resources.ArgNotExist);
                return;
            }

            string prog = "";
            string lang = "";
            string progarg = "";
            for (int i = 0; i < args.Length; ++i)
            {
                string t;
                if (args[i].IndexOf(' ') >= 0)
                {
                    t = "\"" + args[i] + "\"";
                }
                else
                {
                    t = args[i];
                }

                if (i == 1)
                {
                    prog = t;
                }
                else if (i == 0)
                {
                    lang = t;
                }
                else
                {
                    progarg += t;
                }
            }

            //string cmdline = System.Environment.CommandLine;
            //cmdline=cmdline.TrimStart(' ');
            //if(cmdline[0]=='\"')
            //{
            //    int i = cmdline.IndexOf('\"',  1);
            //    if(i<0)
            //    {
            //        throw new Exception("Unknown Error");
            //    }
            //    cmdline = cmdline.Substring(i+1);
            //    cmdline = cmdline.TrimStart();
            //}
            //else if(cmdline[0]==' ')
            //{
            //    int i = cmdline.IndexOf(' ',  1);
            //    if(i<0)
            //    {
            //        throw new Exception("Unknown Error");
            //    }
            //    cmdline = cmdline.Substring(i+1);
            //    cmdline = cmdline.TrimStart();
            //}


            System.Environment.SetEnvironmentVariable("__COMPAT_LAYER", "#APPLICATIONLOCALE");
            //System.Environment.SetEnvironmentVariable("AppLocaleID", "1252");
            System.Environment.SetEnvironmentVariable("AppLocaleID", lang);

            Process.Start(prog, progarg);
        }
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                dowork(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
       
    }
}