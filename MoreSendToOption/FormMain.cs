using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ambiesoft;

namespace MoreSendToOption
{
    public partial class FormMain : Form
    {
        static readonly string SECTION_OPTION = "Option";
        static readonly string KEY_MORESENDTO_FOLDER = "MoreSendtoFolder";
        static string IniPath
        {
            get
            {
                return Path.Combine(
                    Path.GetDirectoryName(Application.ExecutablePath),
                    "MoreSendTo.ini");
            }
        }
        public FormMain()
        {
            InitializeComponent();

            this.Text = string.Format(Application.ProductName);

            HashIni ini = Profile.ReadAll(IniPath);
            string folder;
            Profile.GetString(SECTION_OPTION, KEY_MORESENDTO_FOLDER, string.Empty, out folder, ini);

            txtMoreSendtoFolder.Text=folder; 
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string folder = AmbLib.GetOpenFolderDialog("AAA");
            if (folder == null)
                return;

            txtMoreSendtoFolder.Text = folder;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            HashIni ini = Profile.ReadAll(IniPath);
            Profile.WriteString(SECTION_OPTION, KEY_MORESENDTO_FOLDER, txtMoreSendtoFolder.Text, ini);

            if(!Profile.WriteAll(ini,IniPath))
            {
                MessageBox.Show("AAA","CCC",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
