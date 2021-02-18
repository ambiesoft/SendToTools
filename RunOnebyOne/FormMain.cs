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
using System.IO;
using NDesk.Options;
using Ambiesoft;

namespace RunOnebyOne
{
    public partial class FormMain : Form
    {
        readonly string SECTION_OPTION = "Option";
        readonly string SECTION_LOCATION = "Location";
        readonly string SECTION_APP_COMBO = "AppCombo";
        readonly string SECTION_ARG_COMBO = "ArgCombo";
        readonly int MAX_COMBO_SAVE = 64;
        readonly string KEY_COLUMN_WIDTH = "ListViewColumnWidth";

        string[] args_;
        public FormMain(string[] args)
        {
            args_ = args;

            InitializeComponent();

            lblCombo1TopLeft.Visible = false;
            lblCombo1BottomRight.Visible = false;
            lblCombo2TopLeft.Visible = false;
            lblCombo2BottomRight.Visible = false;

            // LoadFromIni
            HashIni ini = Profile.ReadAll(IniPath);
            AmbLib.LoadFormXYWH(this, SECTION_LOCATION, ini);
            AmbLib.LoadListViewColumnWidth(listMain, SECTION_OPTION, KEY_COLUMN_WIDTH, ini);
            AmbLib.LoadComboBox(cmbApplication, SECTION_APP_COMBO, MAX_COMBO_SAVE, ini);
            AmbLib.LoadComboBox(cmbArguments, SECTION_ARG_COMBO, MAX_COMBO_SAVE, ini);


            OptionSet p = new OptionSet()
                .Add("v|version", dummy => { ShowHelp(); Environment.Exit(0); })
                .Add("?|h|help", dummy => { ShowHelp(); Environment.Exit(0); })
                .Add("@=", reportFile => { ImportFromReport(reportFile); })
                .Add("dir=", dir => { ImportDirectory(dir); })
                ;


            // Parse CommandLine
            List<string> defaultArgs = p.Parse(args);
            foreach (string file in defaultArgs)
            {
                AddToList(file);
            }

            UpdateTitle();
        }

        string IniPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
                    Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ini");
            }
        }
        void ImportDirectory(string dir)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                foreach (var file in di.GetFiles())
                {
                    AddToList(file.FullName);
                }
            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex);
                Environment.Exit(1);
            }
        }
        void ImportFromReport(string reportFile)
        {
            if (string.IsNullOrEmpty(reportFile))
                return;

            try
            {
                foreach (string line in File.ReadAllLines(reportFile))
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    AddToList(line);
                }
            }
            catch(Exception ex)
            {
                CppUtils.Fatal(ex);
                Environment.Exit(1);
            }
        }
        void ShowHelp()
        {
            MessageBox.Show("help", Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void AddToList(string file)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = "";
            lvi.SubItems.Add(file);
            lvi.SubItems.Add(""); // result
                                  // lvi.Tag = new ItemInfo();
            
            listMain.Items.Add(lvi);

            btnRun.Enabled = true;
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

        int GetDoneCount()
        {
            int done = 0;
            foreach (ListViewItem lvi in listMain.Items)
            {
                ListViewItem.ListViewSubItem subIndicator = lvi.SubItems[0];

                if (subIndicator.Text == "*")
                    ++done;
            }
            return done;
        }
        int GetAllCount()
        {
            return listMain.Items.Count;
        }
        void UpdateTitle()
        {
            int done = GetDoneCount();
            int all = GetAllCount();
            string ratio = AmbLib.GetRatioString(done, all);
            string version = AmbLib.getAssemblyVersion(System.Reflection.Assembly.GetExecutingAssembly(), 3);
            Text = string.Format("{0}{1} {2}/{3} - {4} v{5}",
                Running ? (string.IsNullOrEmpty(ratio) ? "0" : ratio) : string.Empty,
                Running ? "%" : string.Empty,
                done,
                all,
                Application.ProductName,
                version);
        }
        private async void RunAsync(string exe,string args)
        {
            Running = true;
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
                await Task.Run(() =>
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    if(string.IsNullOrEmpty(exe) && string.IsNullOrEmpty(args))
                    {
                        // both is null
                        psi.FileName = file;
                    }
                    else if(string.IsNullOrEmpty(exe) && !string.IsNullOrEmpty(args))
                    {
                        // only args, but this fails if file is not an executable
                        psi.FileName = file;
                        psi.Arguments = args;
                    }
                    else if (!string.IsNullOrEmpty(exe) && string.IsNullOrEmpty(args))
                    {
                        // only exe
                        psi.FileName = exe;
                        psi.Arguments = AmbLib.doubleQuoteIfSpace(file);
                    }
                    else
                    {
                        // both exe and args
                        psi.FileName = exe;
                        psi.Arguments = string.Format("{0} {1}", args, AmbLib.doubleQuoteIfSpace(file));
                    }
                    psi.UseShellExecute = true;
                    try
                    {
                        Process proc = Process.Start(psi);
                        if (proc == null)
                        {
                            result = "null";
                        }
                        else
                        {
                            proc.WaitForExit();
                            result = proc.ExitCode.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                });
                subIndicator.Text = "*";
                subResult.Text = result;
                UpdateTitle();
            }

            if(!Cancelling)
            {
                if (IsListAllDone())
                {
                    //btnRun.Enabled = false;
                    //btnRun.Visible = false;
                    //btnRun.Text = Properties.Resources.DONE_BUTTON_TEXT;
                }
                else
                {
                    RunAsync(exe,args);
                }
            }
            Cancelling = false;
            Running = false;
        }
        bool running_;
        bool Running
        {
            get { return running_; }
            set
            {
                if(value)
                {
                    running_ = true;
                    btnRun.Text = Properties.Resources.PAUSE_BUTTON_TEXT;
                }
                else
                {
                    running_ = false;
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
                    btnClearResults.Enabled = false;
                }
                else
                {
                    btnRun.Enabled = true;
                    btnClearResults.Enabled = true;
                }
            }
        }
        void ClearAllListIndicator()
        {
            foreach (ListViewItem lvi in listMain.Items)
            {
                ListViewItem.ListViewSubItem subIndicator = lvi.SubItems[0];
                subIndicator.Text = "";
            }
        }
        void UpdateComboCommon(ComboBox cmb)
        {
            if (!cmb.Items.Contains(cmb.Text))
                cmb.Items.Add(cmb.Text);
        }
        void UpdateCombo()
        {
            UpdateComboCommon(cmbApplication);
            UpdateComboCommon(cmbArguments);
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (Running)
            {
                Cancelling = true;
                return;
            }
            Cancelling = false;
            if(IsListAllDone())
            {
                if (DialogResult.Yes != CppUtils.YesOrNo("Do it again?",MessageBoxDefaultButton.Button2))
                    return;
                ClearAllListIndicator();
            }
            UpdateCombo();
            RunAsync(cmbApplication.Text,cmbArguments.Text);
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

        private void btnReopenAsAdmin_Click(object sender, EventArgs e)
        {
            btnReopenAsAdmin.Enabled = false;
            try
            {
                if (AmbLib.StartAsAdmin())
                    this.Close();
                return;
            }
            catch (Exception ex)
            {
                CppUtils.Alert(this, ex);
            }
            finally
            {
                btnReopenAsAdmin.Enabled = true;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if(AmbLib.IsAdministrator())
            {
                this.Text += " (" + Properties.Resources.ADMINISTRATOR + ")";
                btnReopenAsAdmin.Visible = false;
            }
        }

        private void btnBrowseApp_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != ofdApplication.ShowDialog())
                return;
            cmbApplication.Text = ofdApplication.FileName;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Running)
                return;
            if (DialogResult.Yes != CppUtils.YesOrNo("Clear?", MessageBoxDefaultButton.Button2))
                return;

            ClearAllListIndicator();
        }

        void ClearSelectedListItems()
        {
            foreach (ListViewItem lvi in listMain.Items)
            {
                if (lvi.Selected)
                    listMain.Items.Remove(lvi);
            }
        }
        int GetSelectedItemCount()
        {
            return listMain.SelectedItems.Count;
        }
        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            int countSelected = GetSelectedItemCount();
            if (countSelected == 0)
                return;

            if(DialogResult.Yes != CppUtils.YesOrNo(string.Format("sure {0} items?",countSelected),
                MessageBoxDefaultButton.Button2))
            {
                return;
            }

            ClearSelectedListItems();
        }

        private void tsmiRemoveAll_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != CppUtils.YesOrNo(string.Format("sure all items?"),
                MessageBoxDefaultButton.Button2))
            {
                return;
            }

            listMain.Items.Clear();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            HashIni ini = Profile.ReadAll(IniPath);
            AmbLib.SaveFormXYWH(this, SECTION_LOCATION, ini);
            AmbLib.SaveListViewColumnWidth(listMain,SECTION_OPTION, KEY_COLUMN_WIDTH, ini);
            AmbLib.SaveComboBox(cmbApplication, SECTION_APP_COMBO, MAX_COMBO_SAVE, ini);
            AmbLib.SaveComboBox(cmbArguments, SECTION_ARG_COMBO, MAX_COMBO_SAVE, ini);
            if (!Profile.WriteAll(ini, IniPath))
                CppUtils.Alert("Failed to save ini");
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            {
                Point location = new Point(lblCombo1TopLeft.Left, lblCombo1TopLeft.Top);
                cmbApplication.Location = location;

                Size size = new Size(lblCombo1BottomRight.Left - (lblCombo1TopLeft.Left),
                    cmbApplication.Size.Height);
                cmbApplication.Size = size;
            }
            {
                Point location = new Point(lblCombo2TopLeft.Left, lblCombo2TopLeft.Top);
                cmbArguments.Location = location;

                Size size = new Size(lblCombo2BottomRight.Left - (lblCombo2TopLeft.Left),
                    cmbArguments.Size.Height);
                cmbArguments.Size = size;
            }


        }
    }
}
