using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ambiesoft.RegexFilenameRenamer
{
    public partial class FormConfirm : Form
    {
        const string SECTION = "FormConfirm";
        const string KEY_SHOWALL = "ShowAll";
        const string KEY_SHOWFULLPATH = "ShowFullPath";

        readonly List<ChangeFile> _changeFiles;
        public FormConfirm(List<ChangeFile> changeFiles)
        {
            _changeFiles = changeFiles;

            InitializeComponent();
        }

        private void NoRichTextChange(RichTextBox RichTextBoxCtrl)
        {
            int lParam;
            lParam = NativeMethods.SendMessage(RichTextBoxCtrl.Handle, NativeMethods.EM_GETLANGOPTIONS, 0, 0);
            lParam &= ~NativeMethods.IMF_DUALFONT;
            NativeMethods.SendMessage(RichTextBoxCtrl.Handle, NativeMethods.EM_SETLANGOPTIONS, 0, lParam);
        }

        private string GetTextFromChangeFiles(bool onlyChanged, bool bFullPath)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var changeFile in _changeFiles)
            {
                if (changeFile.Changed)
                {
                    string tmp = string.Format("\"{0}\" ->\r\n\"{1}\"",
                        bFullPath ? changeFile.Before : Path.GetFileName(changeFile.Before),
                        bFullPath ? changeFile.After : Path.GetFileName(changeFile.After));

                    sb.Append(tmp);
                    sb.AppendLine();
                    sb.AppendLine();
                }
                else
                {
                    if (!onlyChanged)
                    {
                        string tmp = string.Format("\"{0}\" ->\r\n{1}",
                            bFullPath ? changeFile.Before : Path.GetFileName(changeFile.Before),
                            Properties.Resources.NO_CHANGE);

                        sb.Append(tmp);
                        sb.AppendLine();
                        sb.AppendLine();
                    }
                }
            }
            return sb.ToString();
        }
        private void FormConfirm_Load(object sender, EventArgs e)
        {
            txtMessage.Text = GetTextFromChangeFiles(true,false);

            txtMessage.SelectAll();
            txtMessage.Select(0, 0);

            Icon icon = new Icon(System.Drawing.SystemIcons.Question, 16, 16);
            pictQuestion.Image = icon.ToBitmap();

            // Load from ini
            bool boolval;
            HashIni ini = Profile.ReadAll(Program.IniPath, false);

            Profile.GetBool(SECTION, KEY_SHOWALL, false, out boolval, ini);
            chkShowAll.Checked = boolval;

            Profile.GetBool(SECTION, KEY_SHOWFULLPATH, false, out boolval, ini);
            chkShowFullPath.Checked = boolval;

            chkCommon();

            txtMessage.SelectAll();
            txtMessage.Select(0, 0);
        }

        private void chkCommon()
        {
            txtMessage.Text = GetTextFromChangeFiles(!chkShowAll.Checked, chkShowFullPath.Checked);
        }
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            chkCommon();
        }

        private void chkShowFullPath_CheckedChanged(object sender, EventArgs e)
        {
            chkCommon();
        }

        private void FormConfirm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HashIni ini = Profile.ReadAll(Program.IniPath, false);

            Profile.WriteBool(SECTION, KEY_SHOWALL, chkShowAll.Checked, ini);
            Profile.WriteBool(SECTION, KEY_SHOWFULLPATH, chkShowFullPath.Checked, ini);
            
            if(!Profile.WriteAll(ini,Program.IniPath))
            {
                MessageBox.Show("TODO: failed to write ini");
            }
        }
    }
}
