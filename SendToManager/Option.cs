using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendToManager
{
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
        }

        private void btnLVColor1_Click(object sender, EventArgs e)
        {
            using(ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnLVColor1.BackColor;
                if (DialogResult.OK != dlg.ShowDialog())
                    return;

                btnLVColor1.BackColor = dlg.Color;
            }
        }

        private void btnLVColor2_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.Color = btnLVColor2.BackColor;
                if (DialogResult.OK != dlg.ShowDialog())
                    return;

                btnLVColor2.BackColor = dlg.Color;
            }
        }

        Color save1, save2;
        private void Option_Load(object sender, EventArgs e)
        {
            save1 = btnLVColor1.BackColor;
            save2 = btnLVColor2.BackColor;
        }
        private void Option_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this.DialogResult != DialogResult.OK)
            {
                btnLVColor1.BackColor = save1;
                btnLVColor2.BackColor = save2;
            }
        }

       
    }
}
