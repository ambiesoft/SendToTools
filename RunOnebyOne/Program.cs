using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ambiesoft;

namespace RunOnebyOne
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            FormMain form = new FormMain(args);
            AmbLib.SetFontAll(form);
            Application.Run(form);
        }
    }
}
