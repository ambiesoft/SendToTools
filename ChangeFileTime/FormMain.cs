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
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ambiesoft;

namespace ChangeFileTime
{
    public partial class FormMain : Form
    {
        System.IO.FileInfo _fi;
        string _theFile;

        public FormMain(string theFileName)
        {
            InitializeComponent();
            
            AmbLib.SetFontAll(this);
            txtFileName.Text = theFileName;

            TheFile = theFileName;
            RefreshTimes();
        }

        string TheFile
        {
            get { return _theFile; }
            set
            {
                _theFile = value;
                this.Text = string.Format("{0} | {1}",
                    Path.GetFileName(value), Properties.Resources.TITLE);
            }
        }

        private void btnNowCRTime_Click(object sender, EventArgs e)
        {
            dtpCRTime.Value = DateTime.Now;
        }
        private void btnNowLWTime_Click(object sender, EventArgs e)
        {
            dtpLWTime.Value = DateTime.Now;
        }
        private void btnNowLATime_Click(object sender, EventArgs e)
        {
            dtpLATime.Value = DateTime.Now;
        }
        private void btnNowForAllTime_Click(object sender, EventArgs e)
        {
            dtpCRTime.Value = dtpLWTime.Value = dtpLATime.Value = DateTime.Now;
        }

        void DoUpdate()
        {
            if (!dtpCRTime.Value.Equals(dtpCRTime.Tag))
            {
                _fi.CreationTime = dtpCRTime.Value;
                dtpCRTime.Tag = dtpCRTime.Value;
            }
            if (!dtpLWTime.Value.Equals(dtpLWTime.Tag))
            {
                _fi.LastWriteTime = dtpLWTime.Value;
                dtpLWTime.Tag = dtpLWTime.Value;
            }
            if (!dtpLATime.Value.Equals(dtpLATime.Tag))
            {
                _fi.LastAccessTime = dtpLATime.Value;
                dtpLATime.Tag = dtpLATime.Value;
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
        }

        void RefreshTimes()
        {
            _fi = new System.IO.FileInfo(_theFile);

            dtpCRTime.Tag = _fi.CreationTime;
            dtpCRTime.Value = _fi.CreationTime;

            dtpLWTime.Tag = _fi.LastWriteTime;
            dtpLWTime.Value = _fi.LastWriteTime;

            dtpLATime.Tag = _fi.LastAccessTime;
            dtpLATime.Value = _fi.LastAccessTime;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTimes();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }
    }
}