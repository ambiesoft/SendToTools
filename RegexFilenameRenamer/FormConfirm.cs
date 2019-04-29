using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ambiesoft.RegexFilenameRenamer
{



    public partial class FormConfirm : Form
    {
        public FormConfirm()
        {
            InitializeComponent();
        }



        private void NoRichTextChange(RichTextBox RichTextBoxCtrl)
        {
            int lParam;
            lParam = NativeMethods.SendMessage(RichTextBoxCtrl.Handle, NativeMethods.EM_GETLANGOPTIONS, 0, 0);
            lParam &= ~NativeMethods.IMF_DUALFONT;
            NativeMethods.SendMessage(RichTextBoxCtrl.Handle, NativeMethods.EM_SETLANGOPTIONS, 0, lParam);
        }

        internal string initialTextAll_;
        internal string initialTextChanging_;
        private void FormConfirm_Load(object sender, EventArgs e)
        {
            // NoRichTextChange(rtxtMessage);

            txtMessage.Text = initialTextChanging_;

            txtMessage.SelectAll();
            // txtMessage.SelectionFont = rtxtMessage.Font;
            txtMessage.Select(0, 0);

            Icon icon = new Icon(System.Drawing.SystemIcons.Question, 16, 16);
            pictQuestion.Image = icon.ToBitmap();
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowAll.Checked)
                txtMessage.Text = initialTextAll_;
            else
                txtMessage.Text = initialTextChanging_;
        }
    }
}
