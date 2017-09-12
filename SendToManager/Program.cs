using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

using Ambiesoft;

namespace SendToManager
{
    public static class Program
    {
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

                HashIni ini = Profile.ReadAll(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "folder.ini"));

                int pathtype;
                string folder = null;
                Profile.GetInt("FolderConfig", "PathType", -1, out pathtype, ini);
                switch (pathtype)
                {
                    case -1:
                    case 0:
                        folder = Path.GetDirectoryName(Application.ExecutablePath);
                        break;
                    case 1:
                        folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        folder = Path.Combine(folder, "Ambiesoft\\" + Application.ProductName);
                        break;
                    case 2:
                        folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        folder = Path.Combine(folder, "Ambiesoft\\" + Application.ProductName);
                        break;
                    case 3:
                        Profile.GetString("FolderConfig", "folder", null, out folder, ini);
                        break;
                    default:
                        Error("Invalid pathtype");
                        Environment.Exit(1);
                        break;
                }

                Directory.CreateDirectory(folder);
                if (!Directory.Exists(folder))
                {
                    Error(string.Format("Invalid folder {0}", folder));
                    Environment.Exit(1);
                }

                return configDir_ = folder;
            }
        }
        internal static String IniFile
        {
            get
            {
                return Path.Combine(ConfigDir, Application.ProductName+".ini");
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
            int val = Environment.TickCount;
            Profile.WriteInt("Test", "WriteTest", val, IniFile);
            int ret;
            Profile.GetInt("Test", "WriteTest", 0, out ret, IniFile);
            if(val != ret)
            {
                Alert(Properties.Resources.ALERT_DIRECTORY_UNAVAILABLE);
                return false;
            }
            return true;
        }

        // [STAThread]
        public static void DllMain()
        {
            if (!preRun())
                return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}