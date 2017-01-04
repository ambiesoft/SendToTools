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
