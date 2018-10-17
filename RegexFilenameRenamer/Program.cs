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

namespace Ambiesoft.RegexFilenameRenamer
{
    static class Program
    {
        static string GetHelpMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Path.GetFileName(Application.ExecutablePath));
            sb.Append(" ");
            sb.AppendLine("/rf REGEXSEARCH /rt REPLACE [/ca] [/ic] [/ie] [/dr] [/blob] file1 [file2 [file3...]]");
            sb.AppendLine();
            sb.AppendLine("  /rf REGEXSEARCH");
            sb.AppendLine("    Use () for grouping.");
            sb.AppendLine("  /ft REPLACE");
            sb.AppendLine("    Use \"\" for empty string.");
            sb.AppendLine("    Use $1 to refer to the group.");
            sb.AppendLine("  /ie");
            sb.AppendLine("    Extension will be included for rename operation.");
            sb.AppendLine("  /ic");
            sb.AppendLine("    Ignore Case.");
            sb.AppendLine("  /cf");
            sb.AppendLine("    Show confirm dialog before renaming.");
            sb.AppendLine("  /ca");
            sb.AppendLine("    Check input by showing argv.");
            sb.AppendLine("  /blob");
            sb.AppendLine("    Input files contain blobs.");

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
                Application.ProductName,
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

            SimpleCommandLineParser parser = new SimpleCommandLineParser(args);
            parser.addOption("rf", ARGUMENT_TYPE.MUST);
            parser.addOption("rt", ARGUMENT_TYPE.MUST);
            parser.addOption("ie", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("ic", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("cf", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("ca", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("blob", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("h", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("?", ARGUMENT_TYPE.MUSTNOT);
            parser.Parse();
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

                if (parser["rt"] != null)
                {
                    sb.Append("rt:");
                    sb.AppendLine(parser["rt"].ToString());
                }

                if (parser["ie"] != null)
                    sb.AppendLine("ie");
                if (parser["ic"] != null)
                    sb.AppendLine("ic");
                if (parser["ca"] != null)
                    sb.AppendLine("ca");
                if (parser["cf"] != null)
                    sb.AppendLine("cf");
                if (parser["blob"] != null)
                    sb.AppendLine("blob");
                if (parser["h"] != null)
                    sb.AppendLine("h");
                if (parser["?"] != null)
                    sb.AppendLine("?");

                sb.AppendLine();
                sb.AppendLine(Properties.Resources.DO_YOU_WANT_TO_CONTINUE);

                if (DialogResult.Yes != MessageBox.Show(sb.ToString(),
                    Application.ProductName + " " + "check arg",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2))
                {
                    return 0;
                }
            }
            if (parser["rf"] == null || parser["rt"] == null)
            {
                ShowAlert(Properties.Resources.MUST_SPECIFY_RF_RT);
                return 1;
            }

            string f = parser["rf"].ToString();
            string t = parser["rt"].ToString();

            if (parser.MainargLength == 0)
            {
                ShowAlert(Properties.Resources.NO_FILE);
                return 1;
            }

            Regex regf = null;
            try
            {
                if (parser["ic"] != null)
                    regf = new Regex(f, RegexOptions.IgnoreCase);
                else
                    regf = new Regex(f);
            }
            catch (Exception ex)
            {
                CppUtils.CenteredMessageBox(ex.Message);
                return 1;
            }
            bool isAlsoExt = null != parser["ie"];
            bool dryrun = parser["cf"] != null;
            Dictionary <string, string> targets = new Dictionary<string,string>();

            string[] mainArgs = ConstructMainArgs(parser);
            try
            {
                foreach(string orgFullorRelativeFileName in mainArgs)// (int i = 0; i < parser.MainargLength; ++i)
                {
                    FileInfo fiorig = new FileInfo(orgFullorRelativeFileName);
                    string orgFileName = getProperName(fiorig, isAlsoExt);
                    string orgFolder = fiorig.DirectoryName;

                    string newFileName = regf.Replace(orgFileName, t);
                    if (!isAlsoExt)
                        newFileName += fiorig.Extension;

                    //if (dryrun)
                    //{
                    //    sbDry.AppendLine(string.Format("\"{0}\" -> \"{1}\"",
                    //        fiorig.FullName, orgFolder + @"\" + newFileName));
                    //}
                    //else
                    //{
                    //    fiorig.MoveTo(orgFolder + @"\" + newFileName);
                    //}
                    targets.Add(fiorig.FullName, orgFolder + @"\" + newFileName);
                }

                if (dryrun)
                {
                    StringBuilder sbDry = new StringBuilder();
                    foreach( string org in targets.Keys)
                    {
                        if (org != targets[org])
                        {
                            sbDry.AppendFormat("\"{0}\" ->\n\"{1}\"", 
                                Path.GetFileName(org),
                                Path.GetFileName(targets[org]));
                        }
                        else
                        {
                            sbDry.AppendFormat("\"{0}\" -> " + Properties.Resources.NO_CHANGE,
                                Path.GetFileName(org),
                                Path.GetFileName(targets[org]));
                        }
                        sbDry.AppendLine();
                        sbDry.AppendLine();
                    }
                    //sbDry.AppendLine();
                    //sbDry.AppendLine(Properties.Resources.DO_YOU_WANT_TO_PERFORM);


                    using(FormConfirm form = new FormConfirm())
                    {
                        // form.rtxtMessage.Font = form.lblMessage.Font;

                        form.Text = Application.ProductName + " " + Properties.Resources.CONFIRM;
                        form.lblMessage.Text = Properties.Resources.DO_YOU_WANT_TO_PERFORM;
                        form.initialText_ = sbDry.ToString();
                        if (DialogResult.Yes != form.ShowDialog())
                            return 0;
                    }
                    //if(DialogResult.Yes != MessageBox.Show(
                    //    sbDry.ToString(),
                    //    Application.ProductName + " " + Properties.Resources.CONFIRM,
                    //    MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Question,
                    //    MessageBoxDefaultButton.Button2))
                    //{
                    //    return 0;
                    //}
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
                MessageBox.Show(
                    e.Message, Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
            bool isBlobbing = parser["blob"] != null;
            for (int i = 0; i < parser.MainargLength; ++i)
            {
                string file = parser.getMainargs(i);
                if (isBlobbing)
                {
                    DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(file));

                    FileInfo[] allfi = di.GetFiles(Path.GetFileName(file));
                    bool isAdded = false;
                    foreach (FileInfo f in allfi)
                    {
                        isAdded = true;
                        ret.Add(f.FullName);
                    }
                    if(!isAdded)
                    {
                        // not a blob and does not exist, or it wa directory
                        ret.Add(file);
                    }
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