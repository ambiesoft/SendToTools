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
    }
}
