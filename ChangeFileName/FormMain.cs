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

namespace ChangeFileName
{
    public partial class FormMain : Form
    {
        public static string IniFile
        {
            get { return Application.ExecutablePath + ".wini"; }
        }

        public FormMain()
        {
            InitializeComponent();

            if (File.Exists(IniFile))
            {
                int x, y;
                Ambiesoft.Profile.GetInt("settings", "X", 0, out x, IniFile);
                Ambiesoft.Profile.GetInt("settings", "Y", 0, out y, IniFile);

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
                }
            }            

            int intval;
            bool boolval;
            if (Ambiesoft.Profile.GetInt("settings", "AutoRun", 0, out intval, IniFile))
            {
                chkAutoRun.Checked = intval != 0;
            }

            Ambiesoft.Profile.GetBool("settings", "TopMost", false, out boolval, IniFile);
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

        private void textName_TextChanged(object sender, EventArgs e)
        {
            if (textName.Lines.Length == 1)
                return;

            if (textName.Lines.Length > 0)
                textName.Text = textName.Lines[0];
        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            using (new Ambiesoft.CenterWinDialog(this))
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
                using (new Ambiesoft.CenterWinDialog(this))
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
            Ambiesoft.Profile.WriteInt("settings", "X", Location.X, IniFile);
            Ambiesoft.Profile.WriteInt("settings", "Y", Location.Y, IniFile);
        }

        static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

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
            using (new Ambiesoft.CenterWinDialog(this))
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
                CenteredMessageBox.Show(this,
                    Properties.Resources.ENTER_FILENAME,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);

                return;
            }

            if (-1 != newName.IndexOfAny(Program.damemoji.ToCharArray()))
            {
                CenteredMessageBox.Show(this,
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
    }
}