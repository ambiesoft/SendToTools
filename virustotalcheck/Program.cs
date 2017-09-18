using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace virustotalcheck
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Ambiesoft.AmbLib.setRegMaxIE(11000);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
