using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Ambiesoft;
namespace RunArgs
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
            parser.addOption("v", ARGUMENT_TYPE.MUST, "verb");
            parser.addOption("a", ARGUMENT_TYPE.MUST, "arguments");
            parser.addOption("type", ARGUMENT_TYPE.MUST, "type");
            parser.addOption("name", ARGUMENT_TYPE.MUST, "name");
            parser.Parse();

            string verb = parser["v"].ToString();// getString("v");
            string arguments = parser["a"].ToString();//.getString("a");
            string folder = parser.getMainargs(0);

            if (string.IsNullOrEmpty(verb))
            {
                MessageBox.Show("No Verb", Application.ProductName);
                return -1;
            }
            if (string.IsNullOrEmpty(arguments))
            {
                MessageBox.Show("No Arguments", Application.ProductName);
                return -1;
            }
            if (string.IsNullOrEmpty(folder))
            {
                MessageBox.Show("No Folder", Application.ProductName);
                return -1;
            }
            if(!Directory.Exists(folder))
            {
                MessageBox.Show(folder + " does not exist.", Application.ProductName);
                return -1;
            }


            string type = parser["type"].ToString();
            if (type != null)
            {
                if (type != "f" || type != "d")
                {
                    MessageBox.Show("wrong type", Application.ProductName);
                    return -1;
                }
            }

            string name = parser["name"].ToString();
               

            return 0;
        }
    }
}