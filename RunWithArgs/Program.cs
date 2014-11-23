using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RunWithArgs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox.Show("No Args");
                return -1;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            FormMain f = new FormMain();
            f.txtExe.Text = args[0];
            f.ShowDialog();
            f.Dispose();

            return 0;
        }
    }
}