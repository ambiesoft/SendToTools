using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;


namespace ChangeFileName
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            string inifile = Application.ExecutablePath + ".wini";

            int x, y;
            Ambiesoft.Profile.Profile.GetInt("settings", "X", 0, out x, inifile);
            Ambiesoft.Profile.Profile.GetInt("settings", "Y", 0, out y, inifile);
            this.Location = new Point(x, y);

            int val;
            if (Ambiesoft.Profile.Profile.GetInt("settings", "AutoRun", 0, out val, inifile))
            {
                chkAutoRun.Checked = val != 0;
            }
        }


        private void btnLaunch_Click(object sender, EventArgs e)
        {
            Program.SafeProcessStart(this.textName.Tag.ToString(), true);
        }

        private void btnTrim_Click(object sender, EventArgs e)
        {
            textName.Text = textName.Text.Trim();
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
            if (DialogResult.Yes != MessageBox.Show("Are you sure to trash " + textName.Tag.ToString(),
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2))
            {
                return;
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
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void btnToLower_Click(object sender, EventArgs e)
        {
            textName.Text = textName.Text.ToLower();
        }

        private void btnToUpper_Click(object sender, EventArgs e)
        {
            textName.Text = textName.Text.ToUpper();
        }

        private void btnFN_Click(object sender, EventArgs e)
        {
            String fn = textName.Text;
            fn = fn.Replace("<", "");
            fn = fn.Replace(">", "");
            fn = fn.Replace(":", "");
            fn = fn.Replace("\"", "");
            fn = fn.Replace("/", "");
            fn = fn.Replace("\\", "");
            fn = fn.Replace("|", "");
            fn = fn.Replace("?", "");
            fn = fn.Replace("*", "");
            fn = fn.Replace(",", "");

            textName.Text = fn;
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            String inifile = Application.ExecutablePath + ".wini";
            Ambiesoft.Profile.Profile.WriteInt("settings", "X", Location.X, inifile);
            Ambiesoft.Profile.Profile.WriteInt("settings", "Y", Location.Y, inifile);
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (chkAutoRun.Checked)
            {
                Program.SafeProcessStart(this.textName.Tag.ToString(), true);
            }
        }

        private void chkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            String inifile = Application.ExecutablePath + ".wini";
            Ambiesoft.Profile.Profile.WriteInt("settings", "AutoRun", chkAutoRun.Checked ? 1 : 0, inifile);
        }

        private void btnCopyPath_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.textName.Tag.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void moveToAndClose(string path)
        {
            try
            {
                System.IO.FileInfo fiorig = new System.IO.FileInfo(this.textName.Tag.ToString());
                string destfn = System.IO.Path.Combine(path, fiorig.Name);
                fiorig.MoveTo(destfn);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemNewFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (DialogResult.OK != fbd.ShowDialog(this))
                return;

            moveToAndClose(fbd.SelectedPath);
        }
        private void itemCurR_Click(object sender, EventArgs e)
        {
            moveToAndClose(@"\\Thexp\Share\CurR");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuMoveTo.Items.Clear();

            ToolStripMenuItem itemCurR = new ToolStripMenuItem();
            itemCurR.Click += new EventHandler(itemCurR_Click);
            itemCurR.Text = "&CurR";
            menuMoveTo.Items.Add(itemCurR);


            menuMoveTo.Items.Add(new ToolStripSeparator());
            
            ToolStripMenuItem itemNewFolder = new ToolStripMenuItem();
            itemNewFolder.Click += new EventHandler(itemNewFolder_Click);
            itemNewFolder.Text = "&New Folder...";
            menuMoveTo.Items.Add(itemNewFolder);

            Point pt = btnMoveTo.Location;
            pt.Y += btnMoveTo.Size.Height;
            menuMoveTo.Show(this.PointToScreen(pt));
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

    }
}