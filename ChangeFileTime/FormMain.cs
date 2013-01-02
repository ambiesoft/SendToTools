using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChangeFileTime
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnNowLWTime_Click(object sender, EventArgs e)
        {
            dtpLWTime.Value = DateTime.Now;
        }
    }
}