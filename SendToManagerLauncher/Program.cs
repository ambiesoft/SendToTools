using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SendToManagerLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            string dll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"tools\SendToManager.dll");
            var asm = Assembly.LoadFrom(dll);
            
            Type t = asm.GetType("SendToManager.Program");
            MethodInfo mi = t.GetMethod("DllMain");
            mi.Invoke(null, null);
        }
    }
}
