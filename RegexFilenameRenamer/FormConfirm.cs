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

        internal string initialText_;
        private void FormConfirm_Load(object sender, EventArgs e)
        {
            // NoRichTextChange(rtxtMessage);

            rtxtMessage.Text = initialText_;

            rtxtMessage.SelectAll();
            rtxtMessage.SelectionFont = rtxtMessage.Font;
            rtxtMessage.Select(0, 0);

            Icon icon = new Icon(System.Drawing.SystemIcons.Question, 16, 16);
            pictQuestion.Image = icon.ToBitmap();
        }
    }
}
