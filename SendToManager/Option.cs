//BSD 2-Clause License
//
//Copyright (c) 2017, Ambiesoft
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:
//
//* Redistributions of source code must retain the above copyright notice, this
//  list of conditions and the following disclaimer.
//
//* Redistributions in binary form must reproduce the above copyright notice,
//  this list of conditions and the following disclaimer in the documentation
//  and/or other materials provided with the distribution.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

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
