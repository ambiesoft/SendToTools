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

using NDesk.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Ambiesoft;

namespace touch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                doit(args);
            }
            catch(InfoException ex)
            {
                CppUtils.Info(ex.Message);
            }
            catch (Exception ex)
            {
                CppUtils.Alert(ex);
            }
        }

        static void showtip(int waitspan, string title, string tiptext, System.Drawing.Icon icon)
        {
            NotifyIcon ni = new NotifyIcon();
            ni.BalloonTipTitle = title;
            ni.BalloonTipText = tiptext;

            /*
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();
            foreach (string resourceName in asm.GetManifestResourceNames())
            {
                MessageBox.Show(resourceName);
            }
            System.IO.Stream stream = asm.GetManifestResourceStream("PathToClipboard.Icon.icon.ico");  
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            */

            ni.Icon = icon;

            ni.Text = title;
            ni.Visible = true;
            ni.ShowBalloonTip(waitspan);
            System.Threading.Thread.Sleep(waitspan);
            ni.Dispose();
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        static void doit(string[] args)
        {
            bool recursive = false;
            int depth = -1;
            bool touchfolder = false;
            bool followlink = false;
            bool notooltip = false;
            bool show_help = false;
            bool show_version = false;
            var p = new OptionSet() {
                    { 
                        "r|recursive", 
                        "Touch recursively",
                        v => { recursive = v!=null;}
                    },
                    { 
                        "d|depth=", 
                        "Directory depth of recursive operation, 0 for specifying only arg itself, -1 for inifinite depth.",
                        v => { depth=int.Parse(v);}
                    },
                    { 
                        "f|folder", 
                        "Touch folders as well as files",
                        v => { touchfolder = v!=null;}
                    },
                    {
                        "l|followlink",
                        "Follow links",
                        v => { followlink = v!=null;}
                    },
                    {
                        "n|notooltip",
                        "No tooltip",
                        v => { notooltip = v!=null;}
                    },
                    {
                       "h|help|?",
                        "show this message and exit",
                        v => show_help = v != null
                    },
                    {
                       "v|versiopn",
                        "show version",
                        v => show_version = v != null
                    }
                };

            List<string> extra = p.SafeParse(args);

            if(show_help)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                p.WriteOptionDescriptions(sw);

                throw new InfoException(sb.ToString());
            }
            if(show_version)
            {
                throw new InfoException(string.Format("{0} v{1}",
                    Application.ProductName,
                    AmbLib.getAssemblyVersion(System.Reflection.Assembly.GetExecutingAssembly(), 3)));
            }
            if (extra.Count < 1)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.STR_NO_ARGUMENTS);
                sb.AppendLine();
                sb.AppendLine();
                StringWriter sw = new StringWriter(sb);
                p.WriteOptionDescriptions(sw);

                throw new Exception(sb.ToString());
            }

            
            if(((GetKeyState(0x10) & 0x8000) != 0)      || // VK_SHIFT
               ((GetKeyState(0x11) & 0x8000) != 0 )     || // VK_CONTROL
               ((GetKeyState(0x02) & 0x8000) != 0) )        // VK_RBUTTION
            {
                // check input has any folders.
                bool hasFolder = false;
                foreach (string filename in extra)
                {
                    if (Directory.Exists(filename))
                    {
                        hasFolder = true;
                        break;
                    }
                }

                if (hasFolder)
                {
                    if (DialogResult.Yes != CppUtils.YesOrNo(Properties.Resources.STR_CONFIRM_ALL_SUB,
                        MessageBoxDefaultButton.Button2))
                    {
                        return;
                    }
                    recursive = true;
                    depth = -1;
                }
            }
            DateTime now = DateTime.Now;
            int touchedCount = 0;
            var untouchabled = new Dictionary<string, Exception>();
            foreach (string filename in extra)
            {
                dotouch(now, filename, touchfolder, recursive, followlink, 0, depth, ref touchedCount, untouchabled);
            }
            if(untouchabled.Count!=0)
            {
                var sb = new StringBuilder();
                sb.AppendLine(Properties.Resources.STR_TOUCH_FAILED);
                sb.AppendLine();

                foreach(string key in untouchabled.Keys)
                {
                    sb.Append(key);
                    sb.Append(" (");
                    sb.Append(untouchabled[key].Message);
                    sb.Append(")");
                    sb.AppendLine();
                }
                CppUtils.Alert(sb.ToString());
            }
            if (!notooltip)
            {
                showtip(5000, Application.ProductName,
                    string.Format(Properties.Resources.STR_TOUCHED, touchedCount), Properties.Resources.icon);
            }
        }

        static bool isDepthReached(int curdepth, int maxdepth)
        {
            if (maxdepth == -1)
                return false;
            if (maxdepth < 0)
                return true;

            return curdepth > maxdepth;
        }

        static void dotouch(DateTime now, string filename, bool touchfolder, bool recursive, bool followlink, int curdepth, int maxdepth, ref int touchedCount, Dictionary<string, Exception> untouchabled)
        {
            if (isDepthReached(curdepth , maxdepth))
                return;

            try
            {
                if (File.Exists(filename))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(filename);
                        fi.LastAccessTime = now;
                        fi.LastWriteTime = now;
                        touchedCount++;
                    }
                    catch(Exception ex)
                    {
                        untouchabled.Add(filename, ex);
                    }
                }
                else if (Directory.Exists(filename))
                {
                    var di = new DirectoryInfo(filename);
                    if (touchfolder)
                    {
                        try
                        {
                            di.LastAccessTime = now;
                            di.LastWriteTime = now;
                            touchedCount++;
                        }
                        catch(Exception ex)
                        {
                            untouchabled.Add(filename,ex);
                        }
                    }
                    
                    if (recursive)
                    {
                        FileInfo[] fis = di.GetFiles();
                        foreach (FileInfo fi in fis)
                        {
                            dotouch(now, fi.FullName, touchfolder, recursive, followlink, curdepth + 1, maxdepth, ref touchedCount, untouchabled);
                        }
                        
                        DirectoryInfo[] dis = di.GetDirectories();
                        foreach (DirectoryInfo di2 in dis)
                        {
                            if (followlink || !IsSymbolic(di2.FullName))
                            {
                                dotouch(now, di2.FullName, touchfolder, recursive, followlink, curdepth + 1, maxdepth, ref touchedCount, untouchabled);
                            }
                        }
                    }
                }
                else
                {
                    untouchabled.Add(filename, new Exception("Unknown path type"));
                }
            }
            catch(Exception ex)
            {
                untouchabled.Add(filename,ex);
            }
        }
        private static bool IsSymbolic(string path)
        {
            try
            {
                FileInfo pathInfo = new FileInfo(path);
                return pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
            }
            catch { }
            return false;
        }
    }
}