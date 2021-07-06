using NDesk.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputeFileHash
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            string inputFile = null;

            var p = new OptionSet();

            try
            {
                var extraCommandLineArgs = p.SafeParse(args);
                foreach (var arg in extraCommandLineArgs)
                {
                    inputFile = arg;
                    break;
                }
            }
            catch (Exception ex)
            {
                Ambiesoft.CppUtils.Alert(ex);
                return 1;
            }
            Application.Run(new FormMain(inputFile));
            return 0;
        }
    }
}
