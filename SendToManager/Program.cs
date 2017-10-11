using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

using Ambiesoft;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SendToManager
{
    public static class Program
    {
        static readonly string ProductName = "SendToManager";

        private static string configDir_;
        
        internal static void Error(string message)
        {
            MessageBox.Show(message,
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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
        internal static void Alert(string msg)
        {
            MessageBox.Show(msg,
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
        internal static void Alert(Exception ex)
        {
            Alert(ex.Message);
        }
        internal static bool YesOrNo(string msg)
        {
            return MessageBox.Show(msg,
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }
        static bool preRun()
        {
            if(!FolderConfigHelper.IsFolderAccessible(ConfigDir))
            {
                Alert(string.Format(
                    Properties.Resources.ALERT_DIRECTORY_UNAVAILABLE, ConfigDir));
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

        // [STAThread]
        public static void DllMain()
        {
            if (!preRun())
                return;

            string livingfile = Path.Combine(ConfigDir, "running");
            FileStream fsRunning = null;
            // string pidfile = Path.Combine(ConfigDir, "pid");
            
            try
            {
                fsRunning = File.Open(livingfile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                //FileStream fsPid = File.Open(pidfile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                //int pid = Process.GetCurrentProcess().Id;
                //byte[] b = BitConverter.GetBytes(pid);
                //fsPid.Write(b, 0, b.Length);
                //fsPid.Close();
            }
            catch(Exception)
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
                        BringWindowToFront(p[0].MainWindowHandle);
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
                catch (Exception )
                { 
                    // still failed, uncertain what is going on , then continue.
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            fsRunning.Close();
            
        }
    }
}