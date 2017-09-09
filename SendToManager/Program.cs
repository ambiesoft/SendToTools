using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

using Ambiesoft;

namespace SendToManager
{
    public static class Program
    {
        internal static String IniFile
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SendToManager.ini");
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