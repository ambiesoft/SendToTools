//BSD 2-Clause License
//
//Copyright (c) 2017, Ambiesoft
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this
//  list of conditions and the following disclaimer.
//
//* Redistributions in binary form must reproduce the above copyright notice,
//  this list of conditions and the following disclaimer in the documentation
//  and/or other materials provided with the distribution.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;

namespace SendToManagerLauncher
{
    static class Program
    {
        static Program()
        {
            string tooldir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                "tools");
            Environment.CurrentDirectory = tooldir;
        }

        static bool IsRelaunch(string[] args)
        {
            foreach (string arg in args)
                if (arg == "--relaunch")
                    return true;
            return false;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string tooldir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                "tools");
            SetDllDirectory(tooldir);

            // Add 'tools' to PATH so that this app is runnable in a environemnt
            // where no vc2019 library exists
            if (Environment.GetEnvironmentVariable("PATH").ToLower().IndexOf(tooldir.ToLower()) < 0)
            {
                // for safety measure not to sure to going infinite loop
                if(IsRelaunch(args))
                {
                    MessageBox.Show("--relaunch but tools int not in PATH. Quitting");
                    return;
                }
                Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + tooldir);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Application.ExecutablePath;

                List<string> sb = new List<string>();
                foreach(string arg in args)
                {
                    if(arg.Length > 0 && arg[0] != '"' && arg.IndexOf(' ') >= 0)
                    {
                        sb.Add("\"" + arg + "\"");
                    }
                    else
                        sb.Add(arg);
                }
                sb.Add("--relaunch");

                psi.Arguments = string.Join(" ", sb.ToArray());
                psi.UseShellExecute = true;

                Process.Start(psi);
                return;
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            string dll = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"tools\SendToManager.dll");
            var asm = Assembly.LoadFrom(dll);
            
            Type t = asm.GetType("SendToManager.Program");
            MethodInfo mi = t.GetMethod("DllMain");
            mi.Invoke(null, null);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetDllDirectory(string lpPathName);
    }
}
