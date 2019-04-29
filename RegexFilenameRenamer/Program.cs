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

namespace Ambiesoft.RegexFilenameRenamer
{
    static class Program
    {
        static string GetHelpMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Path.GetFileName(Application.ExecutablePath));
            sb.Append(" ");
            sb.AppendLine("/rf REGEXSEARCH /rt REPLACE [options] file1 [file2 [file3...]]");
            sb.AppendLine();

            sb.AppendLine("  /rf REGEXSEARCH");
            sb.AppendLine("    Use () for grouping.");
            sb.AppendLine("  /rfu REGEXSEARCH_UTF8UrlEncoded");
            sb.AppendLine("    Same as /rf but url-encoded.");
            
            sb.AppendLine("  /rt REPLACE");
            sb.AppendLine("    Use \"\" for empty string.");
            sb.AppendLine("    Use $1 to refer to the group.");
            sb.AppendLine("  /rtu REPLACE_UTF8UrlEncoded");
            sb.AppendLine("    Same as /rt but url-encoded.");
            
            sb.AppendLine("  /ie");
            sb.AppendLine("    Extension will be included for rename operation.");
            
            sb.AppendLine("  /ic");
            sb.AppendLine("    Ignore Case.");
            
            sb.AppendLine("  /cf");
            sb.AppendLine("    Show confirm dialog before renaming (default).");
            sb.AppendLine("  /ncf");
            sb.AppendLine("    Do not show confirm dialog before renaming.");

            sb.AppendLine("  /ca");
            sb.AppendLine("    Check input by showing argv.");
            
            sb.AppendLine("  /glob");
            sb.AppendLine("    Input files contain globs.");

            sb.AppendLine();
            sb.AppendLine("Examples:");
            sb.AppendLine("  Replace \"a\" with \"b\"");
            sb.Append("  > " + Path.GetFileName(Application.ExecutablePath));
            sb.Append(" /rf a /rt b /cf [file]");
            sb.AppendLine();
            sb.AppendLine();

            sb.AppendLine("  Replace continuous space with single space");
            sb.Append("  > " + Path.GetFileName(Application.ExecutablePath));
            sb.Append(" /rf \"\\s+\" /rt \" \" /cf [file]");
            sb.AppendLine();


            return sb.ToString();
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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            sb.AppendLine();
            sb.AppendLine(GetHelpMessage());
            CppUtils.CenteredMessageBox(sb.ToString(),
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

            SimpleCommandLineParser parser = new SimpleCommandLineParser(args);
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
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Properties.Resources.INVALID_OPTION);
                foreach (string s in parser.getInvalidOptions())
                    sb.AppendLine(s);

                sb.AppendLine();
                sb.AppendLine(Properties.Resources.HYPHEN_EXPLANATION);

                ShowAlert(sb.ToString());
                return 1;
            }
            if (parser["h"] != null || parser["?"] != null)
            {
                ShowHelp();
                return 0;
            }
            if (parser["ca"] != null)
            {
                // check argument
                StringBuilder sb = new StringBuilder();
                if (parser["rf"] != null)
                {
                    sb.Append("rf:");
                    sb.AppendLine(parser["rf"].ToString());
                }
                if (parser["rfu"] != null)
                {
                    sb.Append("rfu:");
                    sb.AppendLine(parser["rfu"].ToString());
                }

                if (parser["rt"] != null)
                {
                    sb.Append("rt:");
                    sb.AppendLine(parser["rt"].ToString());
                }
                if (parser["rtu"] != null)
                {
                    sb.Append("rtu:");
                    sb.AppendLine(parser["rtu"].ToString());
                }

                if (parser["ie"] != null)
                    sb.AppendLine("ie");
                if (parser["ic"] != null)
                    sb.AppendLine("ic");
                if (parser["ca"] != null)
                    sb.AppendLine("ca");
                if (parser["cf"] != null)
                    sb.AppendLine("cf");
                if (parser["ncf"] != null)
                    sb.AppendLine("ncf");
                if (parser["glob"] != null)
                    sb.AppendLine("glob");
                if (parser["h"] != null)
                    sb.AppendLine("h");
                if (parser["?"] != null)
                    sb.AppendLine("?");

                sb.AppendLine();
                sb.AppendLine(Properties.Resources.DO_YOU_WANT_TO_CONTINUE);

                if (DialogResult.Yes != CppUtils.CenteredMessageBox(sb.ToString(),
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
                    StringBuilder sbDryAll = new StringBuilder();
                    StringBuilder sbDryChanging = new StringBuilder();
                    bool bRenameExists = false;
                    foreach (string org in targets.Keys)
                    {
                        if (org != targets[org])
                        {
                            bRenameExists = true;
                            string tmp = string.Format("\"{0}\" ->\r\n\"{1}\"", 
                                Path.GetFileName(org),
                                Path.GetFileName(targets[org]));
                            
                            sbDryAll.Append(tmp);

                            sbDryChanging.Append(tmp);
                            sbDryChanging.AppendLine();
                            sbDryChanging.AppendLine();
                        }
                        else
                        {
                            sbDryAll.AppendFormat("\"{0}\" -> " + Properties.Resources.NO_CHANGE,
                                Path.GetFileName(org),
                                Path.GetFileName(targets[org]));
                        }
                        sbDryAll.AppendLine();
                        sbDryAll.AppendLine();
                    }

                    using (FormConfirm form = new FormConfirm())
                    {
                        form.Text = Application.ProductName + " " + Properties.Resources.CONFIRM;
                        form.lblMessage.Text = !bRenameExists ?
                            Properties.Resources.NO_FILES_TO_RENAME:
                            Properties.Resources.DO_YOU_WANT_TO_PERFORM;
                        form.initialTextAll_ = sbDryAll.ToString();
                        form.initialTextChanging_ = sbDryChanging.ToString();
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
        private static string[] ConstructMainArgs(SimpleCommandLineParser parser)
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
                    bool isAdded = false;
                    foreach (FileInfo f in allfi)
                    {
                        isAdded = true;
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