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
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using Ambiesoft;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Ambiesoft.RegexFilenameRenamer
{
    static class Program
    {
        enum VirtualKeyStates : int
        {
            VK_LBUTTON = 0x01,
            VK_RBUTTON = 0x02,
            VK_CANCEL = 0x03,
            VK_MBUTTON = 0x04,
            //
            VK_XBUTTON1 = 0x05,
            VK_XBUTTON2 = 0x06,
            //
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            //
            VK_CLEAR = 0x0C,
            VK_RETURN = 0x0D,
            //
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            //
            VK_KANA = 0x15,
            VK_HANGEUL = 0x15,  /* old name - should be here for compatibility */
            VK_HANGUL = 0x15,
            VK_JUNJA = 0x17,
            VK_FINAL = 0x18,
            VK_HANJA = 0x19,
            VK_KANJI = 0x19,
            //
            VK_ESCAPE = 0x1B,
            //
            VK_CONVERT = 0x1C,
            VK_NONCONVERT = 0x1D,
            VK_ACCEPT = 0x1E,
            VK_MODECHANGE = 0x1F,
            //
            VK_SPACE = 0x20,
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,
            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,
            VK_SELECT = 0x29,
            VK_PRINT = 0x2A,
            VK_EXECUTE = 0x2B,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_HELP = 0x2F,
            //
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_APPS = 0x5D,
            //
            VK_SLEEP = 0x5F,
            //
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SEPARATOR = 0x6C,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_F13 = 0x7C,
            VK_F14 = 0x7D,
            VK_F15 = 0x7E,
            VK_F16 = 0x7F,
            VK_F17 = 0x80,
            VK_F18 = 0x81,
            VK_F19 = 0x82,
            VK_F20 = 0x83,
            VK_F21 = 0x84,
            VK_F22 = 0x85,
            VK_F23 = 0x86,
            VK_F24 = 0x87,
            //
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            //
            VK_OEM_NEC_EQUAL = 0x92,   // '=' key on numpad
            //
            VK_OEM_FJ_JISHO = 0x92,   // 'Dictionary' key
            VK_OEM_FJ_MASSHOU = 0x93,   // 'Unregister word' key
            VK_OEM_FJ_TOUROKU = 0x94,   // 'Register word' key
            VK_OEM_FJ_LOYA = 0x95,   // 'Left OYAYUBI' key
            VK_OEM_FJ_ROYA = 0x96,   // 'Right OYAYUBI' key
            //
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
            //
            VK_BROWSER_BACK = 0xA6,
            VK_BROWSER_FORWARD = 0xA7,
            VK_BROWSER_REFRESH = 0xA8,
            VK_BROWSER_STOP = 0xA9,
            VK_BROWSER_SEARCH = 0xAA,
            VK_BROWSER_FAVORITES = 0xAB,
            VK_BROWSER_HOME = 0xAC,
            //
            VK_VOLUME_MUTE = 0xAD,
            VK_VOLUME_DOWN = 0xAE,
            VK_VOLUME_UP = 0xAF,
            VK_MEDIA_NEXT_TRACK = 0xB0,
            VK_MEDIA_PREV_TRACK = 0xB1,
            VK_MEDIA_STOP = 0xB2,
            VK_MEDIA_PLAY_PAUSE = 0xB3,
            VK_LAUNCH_MAIL = 0xB4,
            VK_LAUNCH_MEDIA_SELECT = 0xB5,
            VK_LAUNCH_APP1 = 0xB6,
            VK_LAUNCH_APP2 = 0xB7,
            //
            VK_OEM_1 = 0xBA,   // ';:' for US
            VK_OEM_PLUS = 0xBB,   // '+' any country
            VK_OEM_COMMA = 0xBC,   // ',' any country
            VK_OEM_MINUS = 0xBD,   // '-' any country
            VK_OEM_PERIOD = 0xBE,   // '.' any country
            VK_OEM_2 = 0xBF,   // '/?' for US
            VK_OEM_3 = 0xC0,   // '`~' for US
            //
            VK_OEM_4 = 0xDB,  //  '[{' for US
            VK_OEM_5 = 0xDC,  //  '\|' for US
            VK_OEM_6 = 0xDD,  //  ']}' for US
            VK_OEM_7 = 0xDE,  //  ''"' for US
            VK_OEM_8 = 0xDF,
            //
            VK_OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
            VK_OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
            VK_ICO_HELP = 0xE3,  //  Help key on ICO
            VK_ICO_00 = 0xE4,  //  00 key on ICO
            //
            VK_PROCESSKEY = 0xE5,
            //
            VK_ICO_CLEAR = 0xE6,
            //
            VK_PACKET = 0xE7,
            //
            VK_OEM_RESET = 0xE9,
            VK_OEM_JUMP = 0xEA,
            VK_OEM_PA1 = 0xEB,
            VK_OEM_PA2 = 0xEC,
            VK_OEM_PA3 = 0xED,
            VK_OEM_WSCTRL = 0xEE,
            VK_OEM_CUSEL = 0xEF,
            VK_OEM_ATTN = 0xF0,
            VK_OEM_FINISH = 0xF1,
            VK_OEM_COPY = 0xF2,
            VK_OEM_AUTO = 0xF3,
            VK_OEM_ENLW = 0xF4,
            VK_OEM_BACKTAB = 0xF5,
            //
            VK_ATTN = 0xF6,
            VK_CRSEL = 0xF7,
            VK_EXSEL = 0xF8,
            VK_EREOF = 0xF9,
            VK_PLAY = 0xFA,
            VK_ZOOM = 0xFB,
            VK_NONAME = 0xFC,
            VK_PA1 = 0xFD,
            VK_OEM_CLEAR = 0xFE
        }
        [DllImport("USER32.dll")]
        static extern short GetKeyState(VirtualKeyStates nVirtKey);

        static string GetHelpMessage()
        {
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.Append(Path.GetFileName(Application.ExecutablePath));
            sbMessage.Append(" ");
            sbMessage.AppendLine("/rf REGEXSEARCH /rt REPLACE [options] file1 [file2 [file3...]]");
            sbMessage.AppendLine();

            sbMessage.AppendLine("  /rf REGEXSEARCH");
            sbMessage.AppendLine("    Use () for grouping.");
            sbMessage.AppendLine("  /rfu REGEXSEARCH_UTF8UrlEncoded");
            sbMessage.AppendLine("    Same as /rf but url-encoded.");
            
            sbMessage.AppendLine("  /rt REPLACE");
            sbMessage.AppendLine("    Use \"\" for empty string.");
            sbMessage.AppendLine("    Use $1 to refer to the group.");
            sbMessage.AppendLine("  /rtu REPLACE_UTF8UrlEncoded");
            sbMessage.AppendLine("    Same as /rt but url-encoded.");
            
            sbMessage.AppendLine("  /ie");
            sbMessage.AppendLine("    Extension will be included for rename operation.");
            
            sbMessage.AppendLine("  /ic");
            sbMessage.AppendLine("    Ignore Case.");
            
            sbMessage.AppendLine("  /cf");
            sbMessage.AppendLine("    Show confirm dialog before renaming (default).");
            sbMessage.AppendLine("  /ncf");
            sbMessage.AppendLine("    Do not show confirm dialog before renaming.");

            sbMessage.AppendLine("  /ca");
            sbMessage.AppendLine("    Check input by showing argv.");
            
            sbMessage.AppendLine("  /glob");
            sbMessage.AppendLine("    Input files contain globs.");

            sbMessage.AppendLine();
            sbMessage.AppendLine("Examples:");
            sbMessage.AppendLine("  Replace \"a\" with \"b\"");
            sbMessage.Append("  > " + Path.GetFileName(Application.ExecutablePath));
            sbMessage.Append(" /rf a /rt b /cf [file]");
            sbMessage.AppendLine();
            sbMessage.AppendLine();

            sbMessage.AppendLine("  Replace continuous space with single space");
            sbMessage.Append("  > " + Path.GetFileName(Application.ExecutablePath));
            sbMessage.Append(" /rf \"\\s+\" /rt \" \" /cf [file]");
            sbMessage.AppendLine();


            return sbMessage.ToString();
        }
        static void ShowHelp()
        {
            CppUtils.CenteredMessageBox(GetHelpMessage(),
                string.Format("{0} ver{1}",
                Application.ProductName,
                AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly(), 3)),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        static string getProperName(FileInfo fi, bool isAlsoExt)
        {
            string ret = Path.GetFileNameWithoutExtension(fi.Name);
            if (isAlsoExt)
                ret += fi.Extension;
            return ret;
        }

        static void ShowAlert(string message)
        {
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.AppendLine(message);
            sbMessage.AppendLine();
            sbMessage.AppendLine(GetHelpMessage());
            CppUtils.CenteredMessageBox(sbMessage.ToString(),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        [STAThread]
        static int Main(string[] args)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SimpleCommandLineParserWritable parser = new SimpleCommandLineParserWritable(args);
            parser.addOption("rf", ARGUMENT_TYPE.MUST);
            parser.addOption("rfu", ARGUMENT_TYPE.MUST);
            parser.addOption("rt", ARGUMENT_TYPE.MUST);
            parser.addOption("rtu", ARGUMENT_TYPE.MUST);

            parser.addOption("ie", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("ic", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("cf", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("ncf", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("ca", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("glob", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("h", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("?", ARGUMENT_TYPE.MUSTNOT);
            
            parser.Parse();

            if(parser.getInvalidOptions().Length != 0)
            {
                StringBuilder sbMessage = new StringBuilder();
                sbMessage.AppendLine(Properties.Resources.INVALID_OPTION);
                foreach (string s in parser.getInvalidOptions())
                    sbMessage.AppendLine(s);

                sbMessage.AppendLine();
                sbMessage.AppendLine(Properties.Resources.HYPHEN_EXPLANATION);

                ShowAlert(sbMessage.ToString());
                return 1;
            }
            if (parser["h"] != null || parser["?"] != null)
            {
                ShowHelp();
                return 0;
            }


            StringBuilder sbCheckArguments = new StringBuilder();
            using (var prepareForm = new FormPrepare(parser))
            {
                if (parser["rf"] != null)
                {
                    sbCheckArguments.Append("rf:");
                    sbCheckArguments.AppendLine(parser["rf"].ToString());
                }
                if (parser["rfu"] != null)
                {
                    sbCheckArguments.Append("rfu:");
                    sbCheckArguments.AppendLine(parser["rfu"].ToString());
                }

                if (parser["rt"] != null)
                {
                    sbCheckArguments.Append("rt:");
                    sbCheckArguments.AppendLine(parser["rt"].ToString());
                }
                if (parser["rtu"] != null)
                {
                    sbCheckArguments.Append("rtu:");
                    sbCheckArguments.AppendLine(parser["rtu"].ToString());
                }

                if (parser["ie"] != null)
                {
                    sbCheckArguments.AppendLine("ie");
                }
                if (parser["ic"] != null)
                {
                    sbCheckArguments.AppendLine("ic");
                }
                if (parser["ca"] != null)
                    sbCheckArguments.AppendLine("ca");

                if (parser["cf"] != null)
                {
                    sbCheckArguments.AppendLine("cf");
                }
                if (parser["ncf"] != null)
                {
                    sbCheckArguments.AppendLine("ncf");
                }
                if (parser["glob"] != null)
                {
                    sbCheckArguments.AppendLine("glob");
                }

                if (parser["h"] != null)
                    sbCheckArguments.AppendLine("h");
                if (parser["?"] != null)
                    sbCheckArguments.AppendLine("?");

                sbCheckArguments.AppendLine();
                sbCheckArguments.AppendLine(Properties.Resources.DO_YOU_WANT_TO_CONTINUE);

                if(GetKeyState(VirtualKeyStates.VK_SHIFT) < 0 ||
                    GetKeyState(VirtualKeyStates.VK_CONTROL) < 0)
                {
                    if (DialogResult.OK != prepareForm.ShowDialog())
                        return 0;

                }
            }

            if (parser["ca"] != null)
            {
                // check argument
                if (DialogResult.Yes != CppUtils.CenteredMessageBox(sbCheckArguments.ToString(),
                    Application.ProductName + " " + "check arg",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2))
                {
                    return 0;
                }
            }
        
            if (parser["rf"] != null && parser["rfu"] != null)
            {
                ShowAlert("TODO");
                return 1;
            }
            if (parser["rt"] != null && parser["rtu"] != null)
            {
                ShowAlert("TODO");
                return 1;
            }

            if (parser["rf"] == null && parser["rfu"] == null)
            {
                ShowAlert(Properties.Resources.MUST_SPECIFY_RF_RT);
                return 1;
            }
            if (parser["rt"] == null && parser["rtu"] == null)
            {
                ShowAlert(Properties.Resources.MUST_SPECIFY_RF_RT);
                return 1;
            }

            string strRegFind = string.Empty;
            if (parser["rf"] != null)
                strRegFind = parser["rf"].ToString();
            else if (parser["rfu"] != null)
                strRegFind = System.Web.HttpUtility.UrlDecode(parser["rfu"].ToString());
            else
                throw new Exception(Properties.Resources.UNEXPECTED_ERROR);

            string strTarget = string.Empty;
            if (parser["rt"] != null)
                strTarget = parser["rt"].ToString();
            else if (parser["rtu"] != null)
                strTarget = System.Web.HttpUtility.UrlDecode(parser["rtu"].ToString());
            else
                throw new Exception(Properties.Resources.UNEXPECTED_ERROR);
            

            if (parser.MainargLength == 0)
            {
                ShowAlert(Properties.Resources.NO_FILE);
                return 1;
            }

            Regex regf = null;
            try
            {
                if (parser["ic"] != null)
                    regf = new Regex(strRegFind, RegexOptions.IgnoreCase);
                else
                    regf = new Regex(strRegFind);
            }
            catch (Exception ex)
            {
                CppUtils.CenteredMessageBox(ex.Message);
                return 1;
            }
            bool isAlsoExt = null != parser["ie"];
            if(parser["cf"] != null && parser["ncf"]!=null)
            {
                ShowAlert(Properties.Resources.BOTH_CF_NCF_SPECIFIED);
                return 1;
            }
            bool dryrun = parser["ncf"] == null;
            Dictionary <string, string> targets = new Dictionary<string,string>();

            string[] mainArgs = ConstructMainArgs(parser);            
            try
            {
                foreach(string orgFullorRelativeFileName in mainArgs)
                {
                    FileInfo fiorig = new FileInfo(orgFullorRelativeFileName);
                    string orgFileName = getProperName(fiorig, isAlsoExt);
                    string orgFolder = fiorig.DirectoryName;

                    string newFileName = regf.Replace(orgFileName, strTarget);
                    if (!isAlsoExt)
                        newFileName += fiorig.Extension;

                    targets.Add(fiorig.FullName, orgFolder + @"\" + newFileName);
                }

                if (dryrun)
                {
                    List<ChangeFile> changeFiles = new List<ChangeFile>();

                    bool bRenameExists = false;
                    foreach (string org in targets.Keys)
                    {
                        if (org != targets[org])
                        {
                            bRenameExists = true;
                            changeFiles.Add(new ChangeFile(org, targets[org], true));
                        }
                        else
                        {
                            changeFiles.Add(new ChangeFile(org, targets[org], false));
                        }
                    }

                    using (FormConfirm form = new FormConfirm(changeFiles))
                    {
                        form.Text = Application.ProductName + " " + Properties.Resources.CONFIRM;
                        form.lblMessage.Text = !bRenameExists ?
                            Properties.Resources.NO_FILES_TO_RENAME:
                            Properties.Resources.DO_YOU_WANT_TO_PERFORM;
                        form.btnYes.Enabled = bRenameExists;
                        if (DialogResult.Yes != form.ShowDialog())
                            return 0;
                    }
                }

                foreach( string org in targets.Keys)
                {
                    if (org != targets[org])
                    {
                        if (Directory.Exists(org))
                        {
                            if (!tryMoveFile(org, targets[org], Directory.Move))
                                return 1;
                        }
                        else if (File.Exists(org))
                        {
                            if (!tryMoveFile(org, targets[org], File.Move))
                                return 1;
                        }
                        else
                        {
                            CppUtils.Alert(string.Format(Properties.Resources.FILE_NOT_EXIST, org));
                        }
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                CppUtils.Fatal(e.Message);
                return -1;
            }
        }

        delegate void MoveFileOrDirectory(string source, string dest);
        static bool tryMoveFile(string source, string dest, MoveFileOrDirectory moveFunc)
        {
            // Loop while retrying
            for (; ; )
            {
                try
                {
                    moveFunc(source, dest);
                    return true;
                }
                catch (Exception ex)
                {
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.AppendLine(string.Format(Properties.Resources.FAILED_TO_MOVE_S, source));
                    sbMessage.AppendLine();
                    sbMessage.AppendLine(ex.Message);

                    DialogResult result = CppUtils.CenteredMessageBox(
                        sbMessage.ToString(),
                        Application.ProductName,
                        MessageBoxButtons.AbortRetryIgnore,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Abort)
                        return false;
                    else if (result == DialogResult.Retry)
                        continue;
                    else if (result == DialogResult.Ignore)
                        return true;
                    else
                    {
                        Debug.Assert(false);
                        return false;
                    }
                }
            }
        }
        internal static string[] ConstructMainArgs(SimpleCommandLineParser parser)
        {
            List<string> ret = new List<string>();
            bool isBlobbing = parser["glob"] != null;
            for (int i = 0; i < parser.MainargLength; ++i)
            {
                string file = parser.getMainargs(i);
                if (isBlobbing && file.IndexOf('*') >= 0)
                {
                    //if (file == "*")
                    //    file = @".\*";
                    DirectoryInfo di=null;
                    if (Path.IsPathRooted(file))
                        di = new DirectoryInfo(Path.GetDirectoryName(file));
                    else
                        di = new DirectoryInfo(".");

                    FileInfo[] allfi = di.GetFiles(Path.GetFileName(file));
                    // bool isAdded = false;
                    foreach (FileInfo f in allfi)
                    {
                        // isAdded = true;
                        ret.Add(f.FullName);
                    }
                    //if(!isAdded)
                    //{
                    //    // not a glob and does not exist, or it wa directory
                    //    ret.Add(file);
                    //}
                }
                else
                {
                    ret.Add(file);
                }
            }
            return ret.ToArray();
        }
    }
}