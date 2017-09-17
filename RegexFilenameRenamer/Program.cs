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
        static void ShowHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/rf REGEXSEARCH /rt REPLACE [/ie] [/dr]");
            sb.AppendLine();
            sb.AppendLine("/ie");
            sb.AppendLine("  Also replace extention.");
            sb.AppendLine("/dr");
            sb.AppendLine("  Dryrun.");

            AmbLib.Info(sb.ToString());
        }

        static string getProperName(FileInfo fi, bool isAlsoExt)
        {
            string ret = Path.GetFileNameWithoutExtension(fi.Name);
            if (isAlsoExt)
                ret += fi.Extension;
            return ret;
        }
        [STAThread]
        static int Main(string[] args)
        {
            SimpleCommandLineParser parser = new SimpleCommandLineParser(args);
            parser.addOption("rf", ARGUMENT_TYPE.MUST);
            parser.addOption("rt", ARGUMENT_TYPE.MUST);
            parser.addOption("ie", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("dr", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("h", ARGUMENT_TYPE.MUSTNOT);
            parser.addOption("?", ARGUMENT_TYPE.MUSTNOT);
            parser.Parse();
            if(parser["h"] != null || parser["?"]!=null)
            {
                ShowHelp();
                return 0;
            }
            if (parser["rf"] == null || parser["rt"] == null)
            {
                AmbLib.Alert(Properties.Resources.MUST_SPECIFY_RF_RT);
                return 1;
            }

            string f = parser["rf"].ToString();
            string t = parser["rt"].ToString();

            if (parser.MainargLength != 1)
            {
                AmbLib.Alert(Properties.Resources.NO_FILE);
                return 1;
            }

            Regex regf = null;
            try
            {
                regf = new Regex(f);
            }
            catch(Exception ex)
            {
                AmbLib.Alert(ex.Message);
                return 1;
            }
            bool isAlsoExt = null != parser["ie"];
            bool dryrun = parser["dr"] != null;
            StringBuilder sbDry = new StringBuilder();
            try
            {
                for (int i=0 ; i < parser.MainargLength;++i)
                {
                    string orgFullorRelativeFileName = parser.getMainargs(i);

                    FileInfo fiorig = new FileInfo(orgFullorRelativeFileName);
                    string orgFileName = getProperName(fiorig, isAlsoExt);
                    string orgFolder = fiorig.DirectoryName;

                    string newFileName = regf.Replace(orgFileName, t);
                    if (!isAlsoExt)
                        newFileName += fiorig.Extension;

                    if (dryrun)
                    {
                        sbDry.AppendLine(string.Format("\"{0}\" -> \"{1}\"",
                            fiorig.FullName, orgFolder + @"\" + newFileName));
                    }
                    else
                    {
                        fiorig.MoveTo(orgFolder + @"\" + newFileName);
                    }
                }

                if(dryrun)
                {
                    AmbLib.Info(sbDry.ToString());
                }
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    e.Message,Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}