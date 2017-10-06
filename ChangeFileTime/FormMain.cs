using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChangeFileTime
{
    public partial class FormMain : Form
    {
        readonly FileInfo fi_;

        public FormMain(FileInfo fi)
        {
            fi_ = fi;
            InitializeComponent();
        }

        private void btnNowLWTime_Click(object sender, EventArgs e)
        {
            dtpLWTime.Value = DateTime.Now;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            fi_.LastWriteTime = dtpLWTime.Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            fi_.LastWriteTime = dtpLWTime.Value;
        }
    }
}