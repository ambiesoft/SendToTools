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
using System.IO;
using System.Xml;

using Ambiesoft;
using NDesk.Options;
using System.Text;
using System.Reflection;

namespace DotNet4Runnable
{
    static class Program
    {
        static string ddd(string[] args)
        {
            bool opListVersions = false;
            bool opShowHelp = false;
            var p = new OptionSet() {
                    {
                        "l|list",
                        "List versions",
                        v => { opListVersions = v!=null;}
                    },
                    {
                        "h|help",
                        "Show Help",
                        v => { opShowHelp = v!=null;}
                    },
                };

            List<string> extra = p.SafeParse(args);

            if (opShowHelp)
            {
                MessageBox.Show("Help not ready",
                    string.Format("{0} v{1}",
                    Application.ProductName, AmbLib.getAssemblyVersion(
                        Assembly.GetExecutingAssembly(), 3)));
                System.Environment.Exit(0);
            }

            if (extra.Count != 1)
            {
                StringBuilder sb = new StringBuilder();
                if (extra.Count == 0)
                    sb.Append(Properties.Resources.NO_ARGUMENTS);
                else
                    sb.Append(Properties.Resources.MORETHAN_ONEARG);
                sb.AppendLine();
                sb.AppendLine();
                StringWriter sw = new StringWriter(sb);
                p.WriteOptionDescriptions(sw);

                throw new Exception(sb.ToString());
            }
            return extra[0];
        }
        static void dowork(string arg)
        {
            FileInfo fi = new FileInfo(arg);
            if (fi.Extension.ToLower() != ".exe")
                throw new Exception(string.Format(Properties.Resources.NOTEXECUTABLE, fi.FullName));

            FileInfo ficonfig = new FileInfo(fi.FullName + ".config");
            if (ficonfig.Exists && ficonfig.Length != 0)
            {
                XmlDocument doc = new XmlDocument();

                doc.Load(ficonfig.FullName);
                XmlNode root = doc.DocumentElement;
                if (root.Name != "configuration")
                {
                    throw new Exception(
                        string.Format(Properties.Resources.NOTCONFIGFILE, ficonfig.FullName));
                }
                XmlNode startupNode = root.SelectSingleNode("startup");

                if (startupNode == null)
                {
                    XmlElement elem = root.OwnerDocument.CreateElement("startup");
                    elem.InnerXml = "<supportedRuntime version=\"v4.0\" /><supportedRuntime version=\"v2.0.50727\" />";
                    root.AppendChild(elem);
                    doc.Save(ficonfig.FullName);
                    CppUtils.Info(Properties.Resources.NEWLYADDED);
                    return;
                }

                // check startupnode begins with value lower than 4.0
                int firstver = -1;
                XmlNode afterNode = null;
                foreach (XmlNode node in startupNode.ChildNodes)
                {
                    if (node.Name != "supportedRuntime")
                        continue;

                    if (node.Attributes["version"] == null)
                        continue;
                    string ver = node.Attributes["version"].Value;
                    if (string.IsNullOrEmpty(ver))
                        continue;
                    if (ver.Length < 2)
                        continue;
                    if (ver[0] != 'v')
                        continue;

                    if (!int.TryParse(ver[1].ToString(), out firstver))
                        continue;

                    afterNode = node;
                    break;
                }

                if(firstver >= 4)
                {
                    CppUtils.Info(Properties.Resources.DN4ISSET);
                    return;
                }

                // append v4 at first of startup
                XmlElement firstv4Elem = startupNode.OwnerDocument.CreateElement("supportedRuntime");
                firstv4Elem.SetAttribute("version", "v4.0");
                startupNode.PrependChild(firstv4Elem);
                doc.Save(ficonfig.FullName);
                CppUtils.Info(Properties.Resources.NEWLYADDED);
            }
            else
            {
                File.WriteAllText(ficonfig.FullName, Properties.Resources.configstring);
                CppUtils.Info(string.Format(Properties.Resources.NEWLYCREATED,
                    ficonfig.FullName));
            }
        }
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            try
            {
                dowork(ddd(args));
            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex);
            }
        }
    }
}