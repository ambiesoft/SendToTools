using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                string srcfilename = this.textName.Tag.ToString();
                if (File.Exists(srcfilename))
                {
                    System.IO.FileInfo fiorig = new System.IO.FileInfo(srcfilename);

                    string destfilename = textName.Text + fiorig.Extension;
                    string destfn = System.IO.Path.Combine(path, destfilename);
                    fiorig.MoveTo(destfn);
                }
                else if(Directory.Exists(srcfilename))
                {
                    DirectoryInfo diorig = new DirectoryInfo(srcfilename);

                    string destfilename = textName.Text + diorig.Extension;
                    string destfn = System.IO.Path.Combine(path, destfilename);
                    // diorig.MoveTo(destfn);

                    // Directory.Move(this.textName.Tag.ToString(), destfn);

                    if(File.Exists(destfn) || Directory.Exists(destfn))
                    {
                        showError("\"" + destfn + "\" " + "already exists.");
                        return;
                    }
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(srcfilename, destfn);
                }
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


            var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
            dlg1.Description = "Select a folder to extract to:";
            dlg1.ShowNewFolderButton = true;
            dlg1.ShowEditBox = true;
            //dlg1.NewStyle = false;
            //dlg1.SelectedPath = txtExtractDirectory.Text;
            dlg1.ShowFullPathInEditBox = true;
            // dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;

            // Show the FolderBrowserDialog.
            DialogResult result = dlg1.ShowDialog();
            if (result != DialogResult.OK)
            {
                // txtExtractDirectory.Text = dlg1.SelectedPath;
                return;
            }


            //FolderBrowserDialog fbd = new FolderBrowserDialog();
            //using (new Ambiesoft.CenterWinDialog(this))
            //{
            //    if (DialogResult.OK != fbd.ShowDialog(this))
            //        return;
            //}
            List<string> dirs = new List<string>(DiskDirs);

            dirs.RemoveAll(n => n.Equals(dlg1.SelectedPath, StringComparison.OrdinalIgnoreCase));
            dirs.Insert(0, dlg1.SelectedPath);

            if (dirs.Count > MaxDirCount)
            {
                dirs = dirs.GetRange(0, MaxDirCount);
            }
            DiskDirs = dirs.ToArray();

            moveToAndClose(dlg1.SelectedPath);
        }
        private void itemClearFolder_Click(object sender, EventArgs e)
        {
            DiskDirs = new string[0];
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
            bool hasItems = false;
            foreach (string dir in DiskDirs)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Click += new EventHandler(itemExistingFolder_Click);
                item.Text = dir; // System.IO.Path.GetDirectoryName(dir);
                item.Tag = dir;
                menuMoveTo.Items.Add(item);
                hasItems = true;
            }

            menuMoveTo.Items.Add(new ToolStripSeparator());


            ToolStripMenuItem itemNewFolder = new ToolStripMenuItem();
            itemNewFolder.Click += new EventHandler(itemNewFolder_Click);
            itemNewFolder.Text = Properties.Resources.NEW_FOLDER_DDD;
            menuMoveTo.Items.Add(itemNewFolder);

            if (hasItems)
            {
                ToolStripMenuItem itemClearFolder = new ToolStripMenuItem();
                itemClearFolder.Click += new EventHandler(itemClearFolder_Click);
                itemClearFolder.Text = Properties.Resources.CLEAR;
                menuMoveTo.Items.Add(itemClearFolder);
            }



            Point pt = btnMoveTo.Location;
            pt.Y += btnMoveTo.Size.Height;
            menuMoveTo.Show(this.PointToScreen(pt));
        }



    }
}

