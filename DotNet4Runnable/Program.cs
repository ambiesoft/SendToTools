using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Xml;
namespace DotNet4Runnable
{
    static class Program
    {
        static void dowork(string arg)
        {
            FileInfo fi = new FileInfo(arg);
            if (fi.Extension.ToLower() != ".exe")
                throw new Exception("Not an Executable file");

            FileInfo ficonfig = new FileInfo(fi.FullName + ".config");
            if (ficonfig.Exists)
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(ficonfig.FullName);
                XmlNode root = doc.DocumentElement;
                XmlNode myNode = root.SelectSingleNode("startup");
                bool needwrite = false;
                if(myNode==null)
                {
                    XmlElement elem = root.OwnerDocument.CreateElement("startup");
                    elem.InnerXml = "<supportedRuntime version=\"v4.0\" /><supportedRuntime version=\"v2.0.50727\" />";
                    root.AppendChild(elem);
                    myNode = elem;
                    needwrite = true;
                }
                XmlNode c1 = myNode.ChildNodes[0];
                XmlNode c2 = myNode.ChildNodes[1];

                if ((c1.Name == "supportedRuntime" && c1.Attributes["version"].Value == "v4.0") &&
                    (c2.Name == "supportedRuntime" && c2.Attributes["version"].Value == "v2.0.50727"))
                {
                    if (needwrite)
                        doc.Save(ficonfig.FullName);

                    MessageBox.Show("Already exists and written.",
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                doc.Save(ficonfig.FullName);
                throw new Exception("Unknown content");
            }
            else
            {
                File.WriteAllText(ficonfig.FullName, Properties.Resources.configstring);
            }
        }
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length < 1)
            {
                MessageBox.Show("No Arguments",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dowork(args[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}