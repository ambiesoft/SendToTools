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
using Microsoft.VisualBasic.FileIO;
using System.IO;
using Ambiesoft;
using System.Reflection;
using System.Diagnostics;



namespace ChangeFileName
{
    public partial class FormMain : Form
    {
        public static string IniFile
        {
            get { return AmbLib.GetIniPath(); }
        }

        public FormMain()
        {
            InitializeComponent();

            HashIni ini = Profile.ReadAll(IniFile);
            int x, y;
            Ambiesoft.Profile.GetInt("settings", "X", 60, out x, ini);
            Ambiesoft.Profile.GetInt("settings", "Y", 60, out y, ini);

            bool isin = false;
            foreach (Screen s in Screen.AllScreens)
            {
                Point pos = new Point(x, y);
                if (s.WorkingArea.Contains(pos))
                {
                    isin = true;
                    break;
                }
            }

            if (isin)
            {
                StartPosition = FormStartPosition.Manual;
                this.Location = new Point(x, y);

                int width, height;
                Profile.GetInt("settings", "Width", -1, out width, ini);
                Profile.GetInt("settings", "Height", -1, out height, ini);
                if (width > 0 && height > 0)
                    this.Size = new Size(width, height);
            }


            int intval;
            bool boolval;
            if (Ambiesoft.Profile.GetInt("settings", "AutoRun", 0, out intval, ini))
            {
                chkAutoRun.Checked = intval != 0;
            }

            Ambiesoft.Profile.GetBool("settings", "TopMost", false, out boolval, ini);
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked = boolval;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                textName.SelectAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            Program.SafeProcessStart(this.textName.Tag.ToString(), true);
        }



        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                textName.Text = Clipboard.GetText();
            }
            catch (Exception) { }
        }

        List<string> _undoBuffer = new List<string>();
        bool _unreDoing;
        int _currentUnreIndex = -1;
        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (_unreDoing)
                return;

            if (textName.Lines.Length > 1)
            {
                textName.Text = textName.Lines[0];
            }
            else
            {
                if (_currentUnreIndex >= 0)
                {
                    if (_currentUnreIndex < _undoBuffer.Count - 1)
                    {
                        _undoBuffer.RemoveRange(_currentUnreIndex + 1, _undoBuffer.Count - _currentUnreIndex - 1);
                    }
                }

                _undoBuffer.Add(textName.Text);
                ++_currentUnreIndex;
                return;
            }

        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            using (new Ambiesoft.CenteringDialog(this))
            {
                if (DialogResult.Yes != MessageBox.Show(string.Format(Properties.Resources.ARE_YOU_SURE_TO_TRASH, textName.Tag.ToString()),
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2))
                {
                    return;
                }
            }

            try
            {
                FileSystem.DeleteFile(
                  this.textName.Tag.ToString(),
                  UIOption.OnlyErrorDialogs,
                  RecycleOption.SendToRecycleBin);

                Close();
            }
            catch (Exception ex)
            {
                using (new Ambiesoft.CenteringDialog(this))
                {
                    MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                }
            }
        }





        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            HashIni ini = Profile.ReadAll(IniFile);

            Profile.WriteInt("settings", "X", Location.X, ini);
            Profile.WriteInt("settings", "Y", Location.Y, ini);
            Profile.WriteInt("settings", "Width", Size.Width, ini);
            Profile.WriteInt("settings", "Height", Size.Height, ini);

            if (!Profile.WriteAll(ini, IniFile))
            {
                CppUtils.Alert("failed saving ini.");
            }
        }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0 bytes"; }

            if(value < 1024)
            {
                return value.ToString() + " bytes";
            }
            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (chkAutoRun.Checked)
            {
                Program.SafeProcessStart(this.textName.Tag.ToString(), true);
            }

            try
            {
                FileInfo fi = new FileInfo(this.textName.Tag.ToString());
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.Size);
                sb.Append(": ");
                sb.Append(SizeSuffix(fi.Length));
                lblFileInfo.Text = sb.ToString();
            }
            catch (Exception) { }

            textName.SelectAll();
            textName.Focus();
        }

        private void chkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            Ambiesoft.Profile.WriteInt("settings", "AutoRun", chkAutoRun.Checked ? 1 : 0, IniFile);
        }

        private void btnCopyPath_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.textName.Tag.ToString());
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }


        void showError(string message)
        {
            using (new Ambiesoft.CenteringDialog(this))
            {
                MessageBox.Show(
                message,
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }




        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textName.Text);
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string newName = textName.Text;
            if (string.IsNullOrEmpty(newName))
            {
                CppUtils.CenteredMessageBox(this,
                    Properties.Resources.ENTER_FILENAME,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return;
            }

            if (-1 != newName.IndexOfAny(Program.damemoji.ToCharArray()))
            {
                CppUtils.CenteredMessageBox(this,
                    Properties.Resources.FOLLOWING_UNABLE_FILENAME + Environment.NewLine + Environment.NewLine + Program.damemoji,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnExplorer_Click(object sender, EventArgs e)
        {
            string path = this.textName.Tag.ToString();
            string arg = "/select,\"" + path + "\",/n";
            System.Diagnostics.Process.Start("explorer.exe", arg);
        }


        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
            Ambiesoft.Profile.WriteBool("settings", "TopMost", alwaysOnTopToolStripMenuItem.Checked, IniFile);
        }

        private void pasteTotailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string addingText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(addingText))
                    textName.Text = textName.Text + " " + addingText;
            }
            catch (Exception) { }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _unreDoing = true;
            try
            {
                if (_undoBuffer.Count == 0)
                    return;

                if (_currentUnreIndex < 0)
                    _currentUnreIndex = _undoBuffer.Count - 1;

                if (_currentUnreIndex > 0)
                {
                    _currentUnreIndex--;
                    string s = _undoBuffer[_currentUnreIndex];
                    // _undoBuffer.RemoveAt(_currentUnreIndex);

                    textName.Text = s;
                }
            }
            catch (Exception)
            {

            }
            _unreDoing = false;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _unreDoing = true;
            try
            {
                if (_undoBuffer.Count == 0)
                    return;

                if (_currentUnreIndex < _undoBuffer.Count - 1)
                {
                    _currentUnreIndex++;
                    string s = _undoBuffer[_currentUnreIndex];
                    textName.Text = s;
                }
            }
            catch (Exception)
            {

            }
            _unreDoing = false;
        }

        private void addModifyToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ModificationTool dlg = new ModificationTool())
            {
                dlg.ShowDialog();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Application.ProductName);
            sb.Append(" version ");
            sb.Append(AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly()));
            sb.AppendLine();
            sb.Append("Copyright 2017 Ambiesoft");
            CppUtils.CenteredMessageBox(this,
                sb.ToString(),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void goToWebPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://github.com/erasoni/SendToTools");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

        }
    }
}