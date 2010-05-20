using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            parser.addOption("reg", ARGUMENT_TYPE.MUST);
            parser.addOption("at", ARGUMENT_TYPE.MUST);
            parser.addOption("ah", ARGUMENT_TYPE.MUST);
            parser.addOption("sb", ARGUMENT_TYPE.MUST);
            parser.addOption("sa", ARGUMENT_TYPE.MUST);
            parser.addOption("sr", ARGUMENT_TYPE.MUST);
            parser.Parse();

            if (!parser["sr"])
            {
                return -1;
            }

            string sr = parser["sr"].ToString();

            if (parser.MainargLength != 1)
            {
                return -1;
            }

            string orgFileName = parser.get_Mainargs[0];


        }
    }
}