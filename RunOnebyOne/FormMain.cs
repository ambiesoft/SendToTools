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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunOnebyOne
{
    public partial class FormMain : Form
    {
        public FormMain(string[] args)
        {
            InitializeComponent();

            Text = Application.ProductName;

            foreach (string file in args)
            {
                AddToList(file);
            }
        }

        private void AddToList(string file)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = "";
            lvi.SubItems.Add(file);
            lvi.SubItems.Add(""); // result
                                  // lvi.Tag = new ItemInfo();

            listMain.Items.Add(lvi);
        }

        bool IsListAllDone()
        {
            foreach (ListViewItem lvi in listMain.Items)
            {
                ListViewItem.ListViewSubItem subIndicator = lvi.SubItems[0];

                if (subIndicator.Text != "*")
                    return false;
            }
            return true;
        }

        private async void RunAsync()
        {
            Doing = true;
            foreach (ListViewItem lvi in listMain.Items)
            {
                if (Cancelling)
                    break;

                ListViewItem.ListViewSubItem subIndicator = lvi.SubItems[0];
                ListViewItem.ListViewSubItem subFile = lvi.SubItems[1];
                ListViewItem.ListViewSubItem subResult = lvi.SubItems[2];

                if (subIndicator.Text == "*")
                    continue;
                    
                subIndicator.Text = "=";
                string file = subFile.Text;
                string result = "";
                await Task.Run(() => {
                   ProcessStartInfo psi = new ProcessStartInfo();
                   psi.FileName = file;
                   psi.UseShellExecute = true;
                   try
                   {
                        Process proc = Process.Start(psi);
                        proc.WaitForExit();

                        result = proc.ExitCode.ToString();
                   }
                   catch (Exception ex)
                   {
                        result = ex.Message;
                   }
               });
                subIndicator.Text = "*";
                subResult.Text = result;
            }

            if(!Cancelling)
            {
                if (IsListAllDone())
                {
                    btnRun.Enabled = false;
                    btnRun.Visible = false;
                    btnRun.Text = Properties.Resources.DONE_BUTTON_TEXT;
                }
                else
                {
                    RunAsync();
                }
            }
            Cancelling = false;
            Doing = false;
        }
        bool doing_;
        bool Doing
        {
            get { return doing_; }
            set
            {
                if(value)
                {
                    doing_ = true;
                    btnRun.Text = Properties.Resources.PAUSE_BUTTON_TEXT;
                }
                else
                {
                    doing_ = false;
                    btnRun.Text = Properties.Resources.RUN_BUTTON_TEXT;
                }
            }
        }
        bool cancelling_;
        bool Cancelling
        {
            get { return cancelling_; }
            set
            {
                cancelling_ = value;
                if (cancelling_)
                {
                    btnRun.Text = Properties.Resources.CANCELLING_BUTTON_TEXT;
                    btnRun.Enabled = false;
                }
                else
                {
                    btnRun.Enabled = true;
                }
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (Doing)
            {
                Cancelling = true;
                return;
            }
            Cancelling = false;
            RunAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listMain_DragEnter(object sender, DragEventArgs e)
        {
            listMain_DragOver(sender, e);
        }

        private void listMain_DragOver(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void listMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                object o = e.Data.GetData(DataFormats.FileDrop);
                string[] files = (string[])o;

                foreach (string s in files)
                    AddToList(s);
            }
        }
    }
}
