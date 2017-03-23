using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace virustotalcheck
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            browser.Navigate("https://www.virustotal.com/");
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (sender != browser)
                return;

            HtmlElement elem = browser.Document.GetElementById("url");
            if (elem == null)
                return;

            elem.SetAttribute("value", @"S:\Users\Hohn\Desktop\Public API version 2.0 - VirusTotal.pdf");
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlElement elem = browser.Document.GetElementById("url");
            if (elem == null)
                return;

            //var s = document.createElement('script');
            //s.setAttribute('src', 'http://jquery.com/src/jquery-latest.js');
            //document.getElementsByTagName('body')[0].appendChild(s);
            elem.SetAttribute("value", @"S:\Users\Hohn\Desktop\Public API version 2.0 - VirusTotal.pdf");
            elem.SetAttribute("name", @"S:\Users\Hohn\Desktop\Public API version 2.0 - VirusTotal.pdf");

            browser.Document.GetElementById("url").InnerText = "aaa";
            //browser.Navigate("GetElementById('url').Name = 'aaa';");
        }
    }
}
