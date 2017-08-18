using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IWshRuntimeLibrary;

namespace SendToManager
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        SortedList<int, FileInfo> allitems = new SortedList<int, FileInfo>();

        string SendToFolder
        {
            get
            {
                return System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            }
        }

        private void UpdateList()
        {
            allitems.Clear();
            string dir = SendToFolder;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
            System.IO.FileInfo[] fis = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            Array.Sort(fis,
                delegate (FileInfo f1, FileInfo f2)
                {
                    return f1.Name.CompareTo(f2.Name);
                }
            );

            int i = 0;
            foreach (FileInfo fi in fis)
            {
                if (!fi.IsReadOnly &&
                    (fi.Attributes & FileAttributes.Hidden) == 0 &&
                    string.Compare(fi.Extension, ".lnk", true) == 0)
                {
                    allitems.Add(i++, fi);
                }
            }

            lvMain.VirtualListSize = allitems.Count;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void lvMain_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {

        }

        private void lvMain_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {

            FileInfo fi = allitems[e.ItemIndex];

            var item = new ListViewItem();
            item.Text = string.Format("{0:D2}", e.ItemIndex + 1);
            item.SubItems.Add(fi.Name);


            e.Item = item;
        }



        private bool CreateShortcut(string shortcutfile, string targetpath)
        {
            try
            {
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutAddress = shortcutfile;
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                // shortcut.Description = "New shortcut for a Notepad";
                // shortcut.Hotkey = "Ctrl+Shift+N";
                shortcut.TargetPath = targetpath;
                shortcut.Save();
                return true;
            }
            catch(Exception)
            { }
            return false;
        }

        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // ofd.DefaultExt = "exe";
                ofd.Filter = @"Executable (.exe)|*.exe|All Files (*.*)|*.*";
                if (DialogResult.OK != ofd.ShowDialog())
                    return;

                FileInfo fi = new FileInfo(ofd.FileName);
                string shortcutfile = Path.GetFileNameWithoutExtension(fi.Name) + ".lnk";

                string shortcutfilefullpath = Path.Combine(SendToFolder, shortcutfile);
                if(System.IO.File.Exists(shortcutfilefullpath))
                {
                    if(DialogResult.Yes != MessageBox.Show(
                        string.Format(Properties.Resources.SHORTCUT_ALREADY_EXISTS,shortcutfilefullpath),
                        Application.ProductName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2
                        ))
                    {
                        return;
                    }
                }

                if(!CreateShortcut(shortcutfilefullpath, ofd.FileName))
                {
                    MessageBox.Show(
                        Properties.Resources.SHORTCUT_CREATION_FAILED,
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                UpdateList();
            }
        }
    }
}