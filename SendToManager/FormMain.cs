using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IWshRuntimeLibrary;
using System.Diagnostics;
using Ambiesoft;

namespace SendToManager
{
    public partial class FormMain : Form
    {
        public static readonly string INVENTORY_COMPONENT_NAME = "inventory";

        public static readonly string SECTION_OPTION = "Option";
        public static readonly string KEY_CURRENT_INVENTORY = "CurrentInventory";
        
        
        public FormMain()
        {
            InitializeComponent();
        }

        // SortedList<int, FileInfo> allitems = new SortedList<int, FileInfo>();

        string SendToFolder
        {
            get
            {
                return System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            }
        }

        private bool IsNumbered(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (name.Length <= 3)
                return false;
            if (!char.IsDigit(name[0]))
                return false;
            if (!char.IsDigit(name[1]))
                return false;
            if (name[2] != ' ')
                return false;

            return true;
        }
        int GetNumber(string name)
        {
            int ret = 0;
            if (!Int32.TryParse(name.Substring(0, 2), out ret))
                return -1;
            return ret;
        }

        string SetNumber(int num, string name)
        {
            return string.Format("{0:D2} {1}", num, name);
        }
        string UnsetNumber(string name)
        {
            if (!IsNumbered(name))
                return name;

            return name.Substring(3);
        }
        private void UpdateList()
        {
            UpdateList(false);
        }
       
        string InventoryDir
        {
            get
            {
                return Path.Combine(Program.ConfigDir , INVENTORY_COMPONENT_NAME);
            }
        }
        string CurrentInventoryFolder
        {
            get
            {
                return Path.Combine(InventoryDir, CurrentInventory);
            }
        }
        private void UpdateList(bool setNumber)
        {
            if (setNumber)
            {
                //DirectoryInfo di = new System.IO.DirectoryInfo(CurrentInventoryFolder);
                //FileInfo[] fis = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                //int currentMaxNumber = 0;
                //foreach (FileInfo fi in fis)
                //{
                //    if (IsNumbered(fi.Name))
                //    {
                //        int num = GetNumber(fi.Name);
                //        currentMaxNumber = Math.Max(currentMaxNumber, num);
                //    }
                //}

                int newNumber = 0;
                List<string> moveFroms = new List<string>();
                List<string> moveTos = new List<string>();
                string dir = null;
                foreach (ListViewItem item in lvMain.Items)
                {
                    LVInfo info = (LVInfo)item.Tag;
                    Debug.Assert(dir == null || dir == info.ParentDir);
                    dir = info.ParentDir;
                    string oldName = info.FileName;
                    string newName = info.FileName;
                    if (IsNumbered(newName))
                    {
                        newName = UnsetNumber(newName);
                    }

                    newName = SetNumber(++newNumber, newName);
                    if (oldName != newName)
                    {
                        moveFroms.Add(Path.Combine(dir, oldName));
                        moveTos.Add(Path.Combine(dir, newName));
                    }
                }
                Debug.Assert(moveFroms.Count == moveTos.Count);
                if (moveFroms.Count != 0)
                {
                    int ret = CppUtils.MoveFiles(moveFroms.ToArray(), moveTos.ToArray());
                    if(ret != 0 && ret != 128)
                    {
                        AmbLib.Alert(Properties.Resources.FAILED_TO_MOVE_FILES);
                        return;
                    }
                }
            }

            lvMain.Items.Clear();
            {
                DirectoryInfo di = new System.IO.DirectoryInfo(CurrentInventoryFolder);
                System.IO.FileInfo[] fis = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                Array.Sort(fis,
                    delegate(FileInfo f1, FileInfo f2)
                    {
                        return f1.Name.CompareTo(f2.Name);
                    }
                );

                foreach (FileInfo fi in fis)
                {
                    if (!fi.IsReadOnly &&
                        (fi.Attributes & FileAttributes.Hidden) == 0 &&
                        string.Compare(fi.Extension, ".lnk", true) == 0)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = fi.Name;
                        item.Tag = new LVInfo(fi.FullName);
                        lvMain.Items.Add(item);
                    }
                }
            }
        }
        string AppDir
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath);
            }
        }

        void constructInventory()
        {
            inventoryToolStripMenuItem.DropDownItems.Clear();
            try
            {
                if (!Directory.Exists(InventoryDir))
                {
                    if (!Program.YesOrNo(Properties.Resources.DO_YOU_WANT_TO_CREATE_DEFAULT_INVENTORY))
                    {
                        Environment.Exit(0);
                        return;
                    }

                    Directory.CreateDirectory(InventoryDir);
                    string mainDir = Path.Combine(InventoryDir, "Main");
                    Directory.CreateDirectory(mainDir);
                    currentInventory_ = "Main";
                }

                DirectoryInfo di = new DirectoryInfo(InventoryDir);
                foreach (DirectoryInfo inv in di.GetDirectories())
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Text = inv.Name;

                    inventoryToolStripMenuItem.DropDownItems.Add(tsmi);
                }
            }
            catch (Exception ex)
            {
                Program.Alert(ex);
            }
        }
        HashIni loadedIni_;
        HashIni LoadedIni
        {
            get
            {
                if(loadedIni_==null)
                {
                    loadedIni_ = Profile.ReadAll(Program.IniFile);
                }
                return loadedIni_;
            }
        }
        string currentInventory_;
        string CurrentInventory
        {
            get
            {
                if(currentInventory_==null)
                {
                    Profile.GetString(SECTION_OPTION, KEY_CURRENT_INVENTORY, "Main", out currentInventory_, LoadedIni);
                }
                return currentInventory_;
            }
        }
        void UpdateTitle()
        {
            StringBuilder sb=new StringBuilder();
            sb.Append(CurrentInventory).Append(" | ").Append(Application.ProductName);

            this.Text = sb.ToString();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            constructInventory();

            UpdateList();
            UpdateTitle();
        }

        //private void lvMain_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        //{

        //}

        //private void lvMain_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        //{

        //    FileInfo fi = allitems[e.ItemIndex];

        //    var item = new ListViewItem();
        //    item.Text = string.Format("{0:D2}", e.ItemIndex + 1);
        //    item.SubItems.Add(fi.Name);


        //    e.Item = item;
        //}



        private void CreateShortcut(string shortcutfile, string targetpath)
        {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = shortcutfile;
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            // shortcut.Description = "New shortcut for a Notepad";
            // shortcut.Hotkey = "Ctrl+Shift+N";
            shortcut.TargetPath = targetpath;
            shortcut.Save();
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

                string shortcutfilefullpath = Path.Combine(CurrentInventoryFolder, shortcutfile);
                if(System.IO.File.Exists(shortcutfilefullpath))
                {
                    if(DialogResult.Yes != CenteredMessageBox.Show(
                        this,
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

                try
                {
                    CreateShortcut(shortcutfilefullpath, ofd.FileName);
                }
                catch(Exception ex)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine(Properties.Resources.SHORTCUT_CREATION_FAILED);
                    sb.AppendLine(ex.Message);

                    CenteredMessageBox.Show(
                        this,
                        sb.ToString(),
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                UpdateList();
            }
        }

        void UpDown(bool bDown)
        {
            if (lvMain.SelectedItems.Count <= 0)
                return;

            ListViewItem item = lvMain.SelectedItems[0];
            if (item == null)
                return;

            int index = lvMain.SelectedIndices[0];
            int nextI = -1;
            if (bDown)
            {
                if (index + 1 >= lvMain.Items.Count)
                    return;
                nextI = index + 1;
            }
            else
            {
                if (index <= 0)
                    return;
                nextI = index - 1;
            }
            lvMain.Items.Remove(item);
            lvMain.Items.Insert(nextI, item);
        }
        private void tsbUp_Click(object sender, EventArgs e)
        {
            UpDown(false);
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
            UpDown(true);
        }

        private void assignNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateList(true);
        }

        private void inventoryToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

        }

        private void deployToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string src = Path.Combine(CurrentInventoryFolder, "*.lnk");
            string dst = SendToFolder;

            CppUtils.CopyFile(src, dst);
        }
    }
}