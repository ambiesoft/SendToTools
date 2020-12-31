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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Ambiesoft;

namespace ChangeFileName
{
    public partial class FormMain : Form
    {
        private void moveToAndClose(string path)
        {
            List<Control> csBack = disableAll();
            try
            {
                string srcfilename = this.txtName.Tag.ToString();
                if (File.Exists(srcfilename))
                {
                    System.IO.FileInfo fiorig = new System.IO.FileInfo(srcfilename);

                    string destfilename = txtName.Text + fiorig.Extension;
                    string destfullname = System.IO.Path.Combine(path, destfilename);

                    //bool overwrite = false;
                    //if (File.Exists(destfullname))
                    //{
                    //    if (DialogResult.Yes != Ambiesoft.CppUtils.YesOrNo(
                    //        string.Format(Properties.Resources.DESTINATION_EXISTS, destfullname),
                    //        MessageBoxDefaultButton.Button2))
                    //    {
                    //        return;
                    //    }
                    //    overwrite = true;
                    //}

                    // why?
                    //fiorig.CopyTo(destfn, overwrite);
                    //fiorig.Delete();

                    if (0 != Ambiesoft.CppUtils.MoveFile(srcfilename, destfullname))
                        return;
                }
                else if (Directory.Exists(srcfilename))
                {
                    DirectoryInfo diorig = new DirectoryInfo(srcfilename);

                    string destfilename = txtName.Text + diorig.Extension;
                    string destfullname = path;

                    if (0 != Ambiesoft.CppUtils.MoveFile(srcfilename, destfullname))
                        return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex.Message);
            }
            finally
            {
                enableAll(csBack);
            }
        }

        private static readonly int DEFAULT_MAX_DIR_COUNT = 64;
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
            //var dlg1 = new Ionic.Utils.FolderBrowserDialogEx();
            //dlg1.Description = "Select a folder to extract to:";
            //dlg1.ShowNewFolderButton = true;
            //dlg1.ShowEditBox = true;
            ////dlg1.NewStyle = false;
            ////dlg1.SelectedPath = txtExtractDirectory.Text;
            //dlg1.ShowFullPathInEditBox = true;
            //// dlg1.RootFolder = System.Environment.SpecialFolder.MyComputer;

            //// Show the FolderBrowserDialog.
            //DialogResult result = dlg1.ShowDialog();
            //if (result != DialogResult.OK)
            //{
            //    // txtExtractDirectory.Text = dlg1.SelectedPath;
            //    return;
            //}
            //string selectedPath = dlg1.SelectedPath;

            string selectedPath = Ambiesoft.CppUtils.GetSelectedFolder(this, Application.ProductName);
            if (string.IsNullOrEmpty(selectedPath))
                return;

            List<string> dirs = new List<string>(DiskDirs);

            dirs.RemoveAll(n => n.Equals(selectedPath, StringComparison.OrdinalIgnoreCase));
            dirs.Insert(0, selectedPath);

            if (dirs.Count > MaxDirCount)
            {
                dirs = dirs.GetRange(0, MaxDirCount);
            }
            DiskDirs = dirs.ToArray();

            moveToAndClose(selectedPath);
        }
        private void itemSort_Click(object sender, EventArgs e)
        {
            string[] tmp = DiskDirs;
            Array.Sort(tmp);
            DiskDirs = tmp;
        }
        private void itemClearFolder_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != CppUtils.YesOrNo(Properties.Resources.ARE_YOU_SURE_CLEAR_ALL_ITEMS,
                  MessageBoxDefaultButton.Button2))
            {
                return;
            }
            DiskDirs = new string[0];
        }
        private void itemClearNonExistent_Click(object sender, EventArgs e)
        {
            var newItems = new List<string>();
            foreach (string dir in DiskDirs)
            {
                if (!Directory.Exists(dir))
                {
                    continue;
                }
                newItems.Add(dir);
            }
            DiskDirs = newItems.ToArray();
        }

        private void itemExistingFolder_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
            {
                return;
            }
            string dir = item.Tag as string;
            if (dir == null)
            {
                // LANG
                CppUtils.Fatal(dir + " is not a directory");
                return;
            }
            else if (!System.IO.Directory.Exists(dir))
            {
                if (DialogResult.Yes != CppUtils.YesOrNo(string.Format(Properties.Resources.DIR_NOT_EXIST_DO_YOU_WANT_TO_REMOVE, dir),
                    MessageBoxDefaultButton.Button2))
                {
                    return;
                }
                removeFromDiskDirs(dir);
                return;
            }
            moveToAndClose(dir);
        }
        private void removeFromDiskDirs(string dir)
        {
            string[] dirs = DiskDirs;
            List<string> newdirs = new List<string>();
            foreach (string s in dirs)
            {
                if (s.ToLower() == dir.ToLower())
                    continue;

                newdirs.Add(s);
            }
            DiskDirs = newdirs.ToArray();
        }
        private string[] DiskDirs
        {
            get
            {
                string[] dirs;
                Ambiesoft.Profile.GetStringArray(SECTION_SETTING, "movetodirs", out dirs, IniFile);
                return dirs;
            }
            set
            {
                if (!Ambiesoft.Profile.WriteStringArray(SECTION_SETTING, "movetodirs", value, IniFile))
                {
                    // LANG
                    CppUtils.Fatal("save failed");
                }
            }
        }
        private void btnMoveTo_Click(object sender, EventArgs e)
        {
            menuMoveTo.Items.Clear();
            bool hasItems = false;
            bool hasNonExistant = false;
            foreach (string dir in DiskDirs)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Click += new EventHandler(itemExistingFolder_Click);
                item.Text = dir; // System.IO.Path.GetDirectoryName(dir);
                if (!Directory.Exists(dir))
                {
                    item.Text += " ";
                    item.Text += Properties.Resources.NOT_EXISTS;
                    hasNonExistant = true;
                }
                item.Tag = dir;
                menuMoveTo.Items.Add(item);
                hasItems = true;
            }

            menuMoveTo.Items.Add(new ToolStripSeparator());


            // Add 'Others' Menu
            ToolStripMenuItem itemOthers = new ToolStripMenuItem(Properties.Resources.OTHERS);
            {
                ToolStripMenuItem itemNewFolder = new ToolStripMenuItem();
                itemNewFolder.Click += new EventHandler(itemNewFolder_Click);
                itemNewFolder.Text = Properties.Resources.NEW_FOLDER_DDD;
                itemOthers.DropDownItems.Add(itemNewFolder);

                itemOthers.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem itemSort = new ToolStripMenuItem();
                itemSort.Enabled = hasItems;
                itemSort.Click += new EventHandler(itemSort_Click);
                itemSort.Text = Properties.Resources.SORT;
                itemOthers.DropDownItems.Add(itemSort);

                itemOthers.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem itemClearFolder = new ToolStripMenuItem();
                itemClearFolder.Enabled = hasItems;
                itemClearFolder.Click += new EventHandler(itemClearFolder_Click);
                itemClearFolder.Text = Properties.Resources.CLEAR_ALL;
                itemOthers.DropDownItems.Add(itemClearFolder);

                ToolStripMenuItem itemClearNonExistant = new ToolStripMenuItem();
                itemClearNonExistant.Enabled = hasNonExistant;
                itemClearNonExistant.Click += new EventHandler(itemClearNonExistent_Click);
                itemClearNonExistant.Text = Properties.Resources.CLEAR_NON_EXISTENT;
                itemOthers.DropDownItems.Add(itemClearNonExistant);

            }
            menuMoveTo.Items.Add(itemOthers);


            // Show menu
            Point pt = btnMoveTo.Location;
            pt.Y += btnMoveTo.Size.Height;
            menuMoveTo.Show(this.PointToScreen(pt));
        }



    }
}

