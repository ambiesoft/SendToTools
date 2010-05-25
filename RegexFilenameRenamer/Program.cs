using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace Ambiesoft.RegexFilenameRenamer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            SimpleCommandLineParser parser = new SimpleCommandLineParser(args);
            parser.addOption("rf", ARGUMENT_TYPE.MUST);
            parser.addOption("rt", ARGUMENT_TYPE.MUST);
            parser.addOption("ie", ARGUMENT_TYPE.MUSTNOT);
            parser.Parse();

            if (parser["rf"] == null || parser["rt"] == null)
            {
                MessageBox.Show("-rf と -rtは両方指定です。");
                return -1;
            }

            string f = parser["rf"].ToString();
            string t = parser["rt"].ToString();

            if (parser.MainargLength != 1)
            {
                MessageBox.Show("ファイルが指定されていません。");
                return -1;
            }

            try
            {
                Regex regf = new Regex(f);
                string orgFullorRelativeFileName = parser.get_Mainargs(0);

                bool isAlsoExt = null != parser["ie"];

                FileInfo fiorig = new FileInfo(orgFullorRelativeFileName);
                string orgFileName = fiorig.Name;
                string orgFolder = fiorig.DirectoryName;

                string newFileName = regf.Replace(orgFileName, t);
                fiorig.MoveTo(orgFolder + @"\" + newFileName);

                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}