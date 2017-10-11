using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using Ambiesoft;

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
            sb.AppendLine("    Use \"\" for empty string.)");
            sb.AppendLine("    Use $1 to refer to the group.)");
            sb.AppendLine("  /ie");
            sb.AppendLine("    Include extension for operation.");
            sb.AppendLine("  /ic");
            sb.AppendLine("    Ignore Case.");
            sb.AppendLine("  /cf");
            sb.AppendLine("    Show confirm dialog before renaming.");
            sb.AppendLine("  /ca");
            sb.AppendLine("    Check input by showing argv.");
            sb.AppendLine("  /blob");
            sb.AppendLine("    Input files contain blob.");

            return sb.ToString();
        }
        static void ShowHelp()
        {
            AmbLib.Info(GetHelpMessage());
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
            AmbLib.Alert(sb.ToString());
        }

        [STAThread]
        static int Main(string[] args)
        {
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

                MessageBox.Show(sb.ToString(),
                    Application.ProductName + " " + "check arg",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return 0;
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
                AmbLib.Alert(ex.Message);
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
                        sbDry.AppendFormat("\"{0}\" -> \"{1}\"", org, targets[org]);
                        sbDry.AppendLine();
                        sbDry.AppendLine();
                    }
                    sbDry.AppendLine();
                    sbDry.AppendLine(Properties.Resources.DO_YOU_WANT_TO_PERFORM);

                    if(DialogResult.Yes != MessageBox.Show(
                        sbDry.ToString(),
                        Application.ProductName + " " + Properties.Resources.CONFIRM,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2))
                    {
                        return 0;
                    }
                }

                foreach( string org in targets.Keys)
                {
                    if (Directory.Exists(org))
                        Directory.Move(org, targets[org]);
                    else
                        File.Move(org,targets[org]);
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