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

     

        private void FormConfirm_Load(object sender, EventArgs e)
        {
            Icon icon = new Icon(System.Drawing.SystemIcons.Question, 16, 16);
            pictQuestion.Image = icon.ToBitmap();
        }
    }
}
