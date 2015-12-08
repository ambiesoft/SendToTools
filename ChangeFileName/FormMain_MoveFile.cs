using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChangeFileName
{
    public partial class FormMain : Form
    {
        private void moveToAndClose(string path)
        {
            try
            {
                System.IO.FileInfo fiorig = new System.IO.FileInfo(this.textName.Tag.ToString());
                string destfilename = textName.Text + fiorig.Extension;
                string destfn = System.IO.Path.Combine(path, destfilename);
                fiorig.MoveTo(destfn);
                this.Close();
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        private static readonly int DEFAULT_MAX_DIR_COUNT = 8;
        private int MaxDirCount
        {
            get
            {
                int ret;
                Ambiesoft.Profile.GetInt("setting", "maxdircount", DEFAULT_MAX_DIR_COUNT, out ret, IniFile);
                return ret;
            }
        }
        private void itemNewFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            using (new CenterWinDialog(this))
            {
                if (DialogResult.OK != fbd.ShowDialog(this))
                    return;
            }
            List<string> dirs = new List<string>(DiskDirs);

            dirs.RemoveAll(n => n.Equals(fbd.SelectedPath, StringComparison.OrdinalIgnoreCase));
            dirs.Insert(0, fbd.SelectedPath);

            if (dirs.Count > MaxDirCount)
            {
                dirs = dirs.GetRange(0, MaxDirCount);
            }
            DiskDirs = dirs.ToArray();

            moveToAndClose(fbd.SelectedPath);
        }
        private void itemExistingFolder_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if(item==null)
            {
                return;
            }
            string dir = item.Tag as string;
            if(dir==null || !System.IO.Directory.Exists(dir))
            {
                // LANG
                showError(dir + " is not a directory");
                return;
            }
            moveToAndClose(dir);
        }

        private string[] DiskDirs
        {
            get
            {
                string[] dirs;
                Ambiesoft.Profile.GetStringArray("settings", "movetodirs", out dirs, IniFile);
                return dirs;
            }
            set
            {
                if(!Ambiesoft.Profile.WriteStringArray("settings", "movetodirs", value, IniFile))
                {
                    // LANG
                    showError("save failed");
                }
            }
        }
        private void btnMoveTo_Click(object sender, EventArgs e)
        {
            menuMoveTo.Items.Clear();

            foreach (string dir in DiskDirs)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Click += new EventHandler(itemExistingFolder_Click);
                item.Text = dir; // System.IO.Path.GetDirectoryName(dir);
                item.Tag = dir;
                menuMoveTo.Items.Add(item);
            }

            menuMoveTo.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem itemNewFolder = new ToolStripMenuItem();
            itemNewFolder.Click += new EventHandler(itemNewFolder_Click);
            itemNewFolder.Text = "&New Folder...";
            menuMoveTo.Items.Add(itemNewFolder);

            Point pt = btnMoveTo.Location;
            pt.Y += btnMoveTo.Size.Height;
            menuMoveTo.Show(this.PointToScreen(pt));
        }



    }
}

