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
using System.Windows.Forms;
using System.IO;

using Ambiesoft;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NDesk.Options;
using System.Text;

namespace SendToManager
{
    public class OkException : Exception
    {
        public OkException(string s) : base(s)
        {
            
        }
    }
    public static class Program
    {
        static readonly string ProductName = "SendToManager";

        private static string configDir_;
        private static string applyInventory_;
        private static bool isApplyInventoryNoConfirm_;
        private static bool isShowHelp_;
        
        internal static string ApplyInventory
        {
            get
            {
                return applyInventory_;
            }
        }
        internal static bool IsApplyNoConfirm
        {
            get
            {
                return isApplyInventoryNoConfirm_;
            }
        }
        internal static String ConfigDir
        {
            get
            {
                if (configDir_ != null)
                    return configDir_;

                return configDir_ = FolderConfigHelper.GetConfigPath();
            }
        }
        internal static String IniFile
        {
            get
            {
                return Path.Combine(ConfigDir, ProductName+".ini");
            }
        }
        static bool preRun()
        {
            if(!FolderConfigHelper.IsFolderAccessible(ConfigDir))
            {
                CppUtils.Alert(string.Format(Properties.Resources.ALERT_DIRECTORY_UNAVAILABLE, ConfigDir));
                return false;
            }

            return true;
        }

        // https://stackoverflow.com/a/35018042
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        private struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        static private void BringWindowToFront(IntPtr wdwIntPtr)
        {
            // IntPtr wdwIntPtr = FindWindow(null, "Put_your_window_title_here");

            //get the hWnd of the process
            Windowplacement placement = new Windowplacement();
            GetWindowPlacement(wdwIntPtr, ref placement);

            // Check if window is minimized
            if (placement.showCmd == 2)
            {
                //the window is hidden so we restore it
                ShowWindow(wdwIntPtr, ShowWindowEnum.Restore);
            }

            //set user's focus to the window
            SetForegroundWindow(wdwIntPtr);
        }

        static void parseCommand(string[] args)
        {
            var p = new OptionSet() {
                    { 
                        "apply=", 
                        "Apply inventory.",
                        inv => {
                            applyInventory_ = inv;
                        }
                    },
                    {
                        "y",
                        "No confirm dialog shown.",
                        b => {
                            isApplyInventoryNoConfirm_ = true;
                        }
                    },
                    {
                        "h",
                        "Show help.",
                        b => {
                            isShowHelp_ = true;
                        }
                    },
                };


            List<string> extra = p.Parse(args);
            if (extra.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.UNKNOWN_OPTION);
                sb.AppendLine();
                sb.AppendLine();
                StringWriter sw = new StringWriter(sb);
                p.WriteOptionDescriptions(sw);

                throw new Exception(sb.ToString());
            }
            if(isShowHelp_)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                StringWriter sw = new StringWriter(sb);
                p.WriteOptionDescriptions(sw);
                throw new OkException(sb.ToString());
            }
        }

        // [STAThread]
        public static void DllMain()
        {
            if (!preRun())
                return;

            try
            {
                parseCommand(new List<string>(Environment.GetCommandLineArgs()).GetRange(1, Environment.GetCommandLineArgs().Length - 1).ToArray());
            }
            catch(OkException ex)
            {
                CppUtils.Info(ex.Message);
                return;
            }
            catch(Exception ex)
            {
                CppUtils.Fatal(ex.Message);
                return;
            }

            string livingfile = Path.Combine(ConfigDir, "running");
            FileStream fsRunning = null;
            // string pidfile = Path.Combine(ConfigDir, "pid");

            // Duplicate instance check only needed if app launch without argument
            // = normal launch
            if (String.IsNullOrEmpty(Program.ApplyInventory))
            {
                try
                {
                    fsRunning = File.Open(livingfile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    //FileStream fsPid = File.Open(pidfile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    //int pid = Process.GetCurrentProcess().Id;
                    //byte[] b = BitConverter.GetBytes(pid);
                    //fsPid.Write(b, 0, b.Length);
                    //fsPid.Close();
                }
                catch (Exception)
                {
                    // multiple instance
                    try
                    {
                        //FileStream fsPid = File.Open(pidfile, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        //byte[] b = new byte[4];
                        //Array.Clear(b, 0, b.Length);

                        //fsPid.Read(b, 0, 4);
                        //int pid = BitConverter.ToInt32(b, 0);
                        //Process.GetProcessById(pid);

                        // OK, anothor process is running
                        Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Application.ExecutablePath));

                        // Activate the first application we find with this name
                        if (p.Length > 0)
                        {
                            
                            BringWindowToFront(p[0].MainWindowHandle);
                            ShowWindow(p[0].MainWindowHandle, ShowWindowEnum.Restore);
                            // MessageBox.Show(p[0].MainWindowHandle.ToString());
                        }
                        else
                        {
                            //string handlefile = Path.Combine(ConfigDir, "winhandle");

                            //using (StreamReader sr = new StreamReader(handlefile))
                            //{
                            //    string s = sr.ReadLine();
                            //    long l;
                            //    long.TryParse(s, out l);
                            //    SetForegroundWindow((IntPtr)l);
                            //}
                        }
                        return;
                    }
                    catch (Exception)
                    {
                        // still failed, uncertain what is going on , then continue.
                    }
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            if(fsRunning != null)
                fsRunning.Close();
        }
    }
}