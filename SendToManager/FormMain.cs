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
using System.IO;
using IWshRuntimeLibrary;
using System.Diagnostics;
using Ambiesoft;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SendToManager
{
    public partial class FormMain : Form
    {
        static readonly string INVENTORY_COMPONENT_NAME = "inventory";

        static readonly string SECTION_OPTION = "Option";
        static readonly string KEY_CURRENT_INVENTORY = "CurrentInventory";
        static readonly string KEY_COLUMN = "Column";
        static readonly string KEY_LISTVIEWCOLOR1 = "ListViewColor1";
        static readonly string KEY_LISTVIEWCOLOR2 = "ListViewColor2";




        static readonly string COLUMN_NAME = "Name";
        static readonly string COLUMN_PATH = "Path";
        static readonly string COLUMN_ARGUMENTS = "Arguments";
        static readonly string COLUMN_WORKINGDIRECTORY = "WorkingDirectory";
        static readonly string COLUMN_ICONPATH = "IconPath";
        static readonly string COLUMN_ICONINDEX = "IconIndex";
        static readonly string COLUMN_RUNASADMIN = "RunAsAdmin";

        private ListViewEx.ListViewEx lvMain;


        public FormMain()
        {
            InitializeComponent();

            HashIni ini = Profile.ReadAll(Program.IniFile);

            AmbLib.LoadFormXYWH(this, SECTION_OPTION, ini);

            int intval = 0;
            if (Profile.GetInt(SECTION_OPTION, KEY_LISTVIEWCOLOR1, Color.White.ToArgb(), out intval, ini))
                option_.btnLVColor1.BackColor = Color.FromArgb(intval);
            if (Profile.GetInt(SECTION_OPTION, KEY_LISTVIEWCOLOR2, Color.White.ToArgb(), out intval, ini))
                option_.btnLVColor2.BackColor = Color.FromArgb(intval);

            Debug.Assert(lvMain.Columns.Count == 0);

            {
                ColumnHeader chName = new ColumnHeader();
                chName.Name = COLUMN_NAME;
                chName.Text = Properties.Resources.COLUMN_NAME;
                chName.Width = 150;
                chName.Tag = new ColumnInfo(txtEditName);
                lvMain.Columns.Add(chName);
            }

            {
                ColumnHeader chPath = new ColumnHeader();
                chPath.Name = COLUMN_PATH;
                chPath.Text = Properties.Resources.COLUMN_PATH;
                chPath.Width = 150;
                chPath.Tag = new ColumnInfo(cmbEditFile);
                lvMain.Columns.Add(chPath);
            }

            {
                ColumnHeader chArguments = new ColumnHeader();
                chArguments.Name = COLUMN_ARGUMENTS;
                chArguments.Text = Properties.Resources.COLUMN_ARGUMENTS;
                chArguments.Width = 50;
                chArguments.Tag = new ColumnInfo(txtEditName);
                lvMain.Columns.Add(chArguments);
            }

            {
                ColumnHeader chWorkingDirectory = new ColumnHeader();
                chWorkingDirectory.Name = COLUMN_WORKINGDIRECTORY;
                chWorkingDirectory.Text = Properties.Resources.COLUMN_WORKINGDIRECTORY;
                chWorkingDirectory.Width = 150;
                chWorkingDirectory.Tag = new ColumnInfo(cmbEditDirectory);
                lvMain.Columns.Add(chWorkingDirectory);
            }

            {
                ColumnHeader chIconPath = new ColumnHeader();
                chIconPath.Name = COLUMN_ICONPATH;
                chIconPath.Text = Properties.Resources.COLUMN_ICONPATH;
                chIconPath.Width = 50;
                chIconPath.Tag = new ColumnInfo(cmbEditFile);
                lvMain.Columns.Add(chIconPath);
            }

            {
                ColumnHeader chIconIndex = new ColumnHeader();
                chIconIndex.Name = COLUMN_ICONINDEX;
                chIconIndex.Text = Properties.Resources.COLUMN_ICONINDEX;
                chIconIndex.Width = 50;
                chIconIndex.Tag = new ColumnInfo(txtEditName);
                lvMain.Columns.Add(chIconIndex);
            }

            {
                ColumnHeader chRunAsAdmin = new ColumnHeader();
                chRunAsAdmin.Name = COLUMN_RUNASADMIN;
                chRunAsAdmin.Text = Properties.Resources.COLUMN_RUNASADMIN;
                chRunAsAdmin.Width = 50;
                chRunAsAdmin.Tag = new ColumnInfo(cmbEditBool);
                lvMain.Columns.Add(chRunAsAdmin);
            }

            AmbLib.LoadListViewColumnWidth(lvMain, SECTION_OPTION, KEY_COLUMN, ini);

            // lvMain.SmallImageList = sysImageList_;

            lvMain.SubItemClicked += lvMain_SubItemClicked;
            lvMain.SubItemEndEditing += lvMain_SubItemEndEditing;

            lvMain.ListViewItemSorter = new ListViewColumnSorter();
            lvMain.ColumnClick += lvMain_ColumnClick;

            lvMain.DoubleClickActivation = true;

            lvMain.Font = SystemFonts.IconTitleFont;
            txtEditName.Font = SystemFonts.IconTitleFont;
            cmbEditDirectory.Font = SystemFonts.IconTitleFont;

            Application.Idle += Application_Idle;
        }

        void lvMain_ColumnClick(object sender, ColumnClickEventArgs eventArgs)
        {
            var sorter = (ListViewColumnSorter)lvMain.ListViewItemSorter;
            if (sorter == null)
                lvMain.ListViewItemSorter = sorter = new ListViewColumnSorter();

            if (eventArgs.Column == sorter.SortColumn)
            {
                // Inverser le sens de tri en cours pour cette colonne.
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // DÈfinir le numÈro de colonne ÅEtrier ; par dÈfaut sur croissant.
                sorter.SortColumn = eventArgs.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // ProcÈder au tri avec les nouvelles options.
            lvMain.Sort();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (!IsHandleCreated || IsDisposed)
                return;

            bool hasItem = lvMain.Items.Count != 0;
            bool hasSelItem = lvMain.SelectedItems.Count != 0;

            tsbUp.Enabled = upToolStripMenuItem.Enabled = hasSelItem;
            tsbDown.Enabled = downToolStripMenuItem.Enabled = hasSelItem;

            tsbNumber.Enabled = numberToolStripMenuItem.Enabled = hasItem;
            tsbDeploy.Enabled = deployToolStripMenuItem.Enabled = hasItem;
        }

        string GetColumnName(int index)
        {
            for (int i = 0; i < lvMain.Columns.Count; ++i)
            {
                if (index == i)
                    return lvMain.Columns[i].Name;
            }
            Debug.Assert(false);
            return null;
        }

        void UpdateItem(ListViewItem item)
        {
            LVInfo info = (LVInfo)item.Tag;
            item.Text = Path.GetFileNameWithoutExtension(info.FileName);


            {
                NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
                IntPtr himl = NativeMethods.SHGetFileInfo(info.FullName,
                                                128, //FileAttribute.Normal,
                                                ref shfi,
                                                (uint)Marshal.SizeOf(shfi),
                                                16 | //SHGFI_USEFILEATTRIBUTES 
                                                NativeMethods.SHGFI_DISPLAYNAME |
                                                NativeMethods.SHGFI_SYSICONINDEX |
                                                NativeMethods.SHGFI_SMALLICON |
                                                0
                                                );
                Debug.Assert(himl == hSysImgList); // should be the same imagelist as the one we set
                item.ImageIndex = shfi.iIcon;
            }

            LinkData lnk = new LinkData(info.FullName, this);

            foreach (ColumnHeader ch in lvMain.Columns)
            {
                if (ch.Name == COLUMN_NAME)
                    continue;

                ListViewItem.ListViewSubItem sub = item.SubItems[ch.Name];
                if (sub == null)
                {
                    sub = new ListViewItem.ListViewSubItem();
                    sub.Name = ch.Name;
                    item.SubItems.Add(sub);
                }

                if (ch.Name == COLUMN_PATH)
                {
                    sub.Text = lnk.Path;
                }
                else if (ch.Name == COLUMN_ARGUMENTS)
                {
                    sub.Text = lnk.Arguments;
                }
                else if (ch.Name == COLUMN_WORKINGDIRECTORY)
                {
                    sub.Text = lnk.WorkingDirectory;
                }
                else if (ch.Name == COLUMN_ICONPATH)
                {
                    sub.Text = lnk.IconPath;
                }
                else if (ch.Name == COLUMN_ICONINDEX)
                {
                    sub.Text = lnk.IconIndex.ToString();
                }
                else if (ch.Name == COLUMN_RUNASADMIN)
                {
                    sub.Text = lnk.IsRunAsAdmin.ToString();
                }
                else
                {
                    Debug.Assert(false);
                }
            }

            //if(lvMain.Items.Count!=0)
            //    lvMain.RedrawItems(0, lvMain.Items.Count-1, false);
        }

        EdittingInfo ei_ = new EdittingInfo();
        void lvMain_SubItemEndEditing(object sender, ListViewEx.SubItemEndEditingEventArgs e)
        {
            if (e.Cancel)
                return;

            string resultText = e.DisplayText;
            if (ei_.HasResult)
                e.DisplayText = resultText = ei_.Result;

            lvMain_SubItemEndEditingWork(e.Item, e.SubItem, e.DisplayText, resultText);

            e.DisplayText = e.Item.SubItems[e.SubItem].Text;
        }
        void lvMain_SubItemEndEditingWork(ListViewItem item, int subitem, string disptext, string resultText)
        {
            try
            {
                LVInfo lvinfo = (LVInfo)item.Tag;
                LinkData lnk = new LinkData(lvinfo.FullName, this);

                string column = GetColumnName(subitem);
                if (column == COLUMN_NAME)
                {
                    try
                    {
                        string from = lvinfo.FullName;
                        string to = Path.Combine(lvinfo.ParentDir, disptext + ".lnk");
                        System.IO.File.Move(from, to);
                        item.Tag = new LVInfo(to);
                    }
                    catch (Exception ex)
                    {
                        Alert(ex.Message);
                    }
                }
                else if (column == COLUMN_PATH)
                {
                    lnk.Path = resultText;
                }
                else if (column == COLUMN_ARGUMENTS)
                {
                    lnk.Arguments = resultText;
                }
                else if (column == COLUMN_WORKINGDIRECTORY)
                {
                    lnk.WorkingDirectory = resultText;
                }
                else if (column == COLUMN_ICONPATH)
                {
                    lnk.IconPath = resultText;
                }
                else if (column == COLUMN_ICONINDEX)
                {
                    lnk.IconIndex = int.Parse(resultText);
                }
                else if (column == COLUMN_RUNASADMIN)
                {
                    lnk.IsRunAsAdmin = bool.Parse(resultText);
                }
                else
                {
                    Debug.Assert(false);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
            }

            UpdateItem(item);
          
        }

        Control GetEdittingControl(ColumnHeader ch)
        {
            ColumnInfo ci = (ColumnInfo)ch.Tag;
            return ci.EdittingControl;
        }
        void lvMain_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
        {
            ColumnHeader ch = null;
            for (int i = 0; i < lvMain.Columns.Count; ++i)
            {
                if (i == e.SubItem)
                {
                    ch = lvMain.Columns[i];
                }
            }
            Debug.Assert(ch != null);

            ei_.Clear();
            ei_.Initial = e.Item.SubItems[e.SubItem].Text;
            ei_.item_ = e.Item;
            ei_.subitem_ = e.SubItem;
            Control edittingControl = GetEdittingControl(ch);
            lvMain.StartEditing(edittingControl, e.Item, e.SubItem);
        }
        static bool InfoBalloon(Form win, string message, bool noParent)
        {
            string path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            string sbexe = Path.Combine(Path.GetDirectoryName(path), "ShowBalloon.exe");
            if (!System.IO.File.Exists(sbexe))
                return false;
            string myexe = (new System.Uri(Assembly.GetEntryAssembly().CodeBase)).LocalPath;

            StringBuilder sbArg = new StringBuilder();
            sbArg.Append("/title:");
            sbArg.Append(System.Web.HttpUtility.UrlEncode(win.ProductName));
            sbArg.Append(" ");
            sbArg.Append(string.Format("/icon:\"{0}\"",myexe));
            sbArg.Append(" ");
            sbArg.Append("/iconindex:0");
            sbArg.Append(" ");
            sbArg.Append("/balloonicon:1");
            sbArg.Append(" ");
            sbArg.Append(System.Web.HttpUtility.UrlEncode(message));

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = sbexe;
            psi.Arguments = sbArg.ToString();

            Process pro = new Process();
            pro.StartInfo = psi;
            try
            {
                return pro.Start();
            }
            catch { }
            return false;
        }

        static void Info(Form win, string message, bool noParent)
        {
            CppUtils.CenteredMessageBox(noParent ? null : win,
                message,
                win.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        static void Info(Form win, string message)
        {
            Info(win, message, false);
        }

        void Info(string message)
        {
            Info(this, message);
        }

        static void Alert(Form win, string message, bool noParent)
        {
            CppUtils.CenteredMessageBox(noParent ? null : win,
                message,
                win.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        static void Alert(Form win, string message)
        {
            Alert(win, message, false);
        }
        void Alert(string message)
        {
            Alert(this, message);
        }

        static DialogResult YesOrNo(Form win, string message, bool noParent)
        {
            return CppUtils.CenteredMessageBox(noParent ? null : win,
                message,
                win.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
        }
        static DialogResult YesOrNoOrCancel(Form win, string message, bool noParent)
        {
            return CppUtils.CenteredMessageBox(noParent ? null : win,
                message,
                win.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
        }
        static DialogResult YesOrNo(Form win, string message)
        {
            return YesOrNo(win, message, false);
        }
        DialogResult YesOrNo(string message)
        {
            return YesOrNo(this, message);
        }
        static string SendToFolder
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
        static string ToolsDir
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "tools");
            }
        }
        static string InventoryRootDir
        {
            get
            {
                return Path.Combine(Program.ConfigDir, INVENTORY_COMPONENT_NAME);
            }
        }
        static string GetInventoryFolder(string inv)
        {
            return Path.Combine(InventoryRootDir, inv);
        }
        string CurrentInventoryFolder
        {
            get
            {
                string ret = Path.Combine(InventoryRootDir, CurrentInventory);
                if (!Directory.Exists(ret))
                {
                    Directory.CreateDirectory(ret);
                }
                return ret;
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
                    if (ret != 0 && ret != 128)
                    {
                        Alert(Properties.Resources.FAILED_TO_MOVE_FILES);
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

                bool colored = false;
                foreach (FileInfo fi in fis)
                {
                    if (!fi.IsReadOnly &&
                        (fi.Attributes & FileAttributes.Hidden) == 0 &&
                        string.Compare(fi.Extension, ".lnk", true) == 0)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Tag = new LVInfo(fi.FullName);

                        UpdateItem(item);
                        item.BackColor = colored ?
                            option_.btnLVColor1.BackColor :
                            option_.btnLVColor2.BackColor;
                        colored = !colored;


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

        void insertDefaultItems()
        {
            KeyValuePair<string, string>[] kvpArr = new KeyValuePair<string, string>[] {
new KeyValuePair<string, string>(@"CommandPrompt.bat", Properties.Resources.TOOL_EXPLANATION_COMMANDLINE),
new KeyValuePair<string, string>(@"ChangeFileName.exe", Properties.Resources.TOOL_EXPLANATION_CHANGEFILENAME),
new KeyValuePair<string, string>(@"ChangeFileTime.exe", Properties.Resources.TOOL_EXPLANATION_CHANGEFILETIME),
new KeyValuePair<string, string>(@"CopyFileContent.exe", Properties.Resources.TOOL_EXPLANATION_COPYFILECONTENT),
new KeyValuePair<string, string>(@"CopyFirstLine.exe", Properties.Resources.TOOL_EXPLANATION_COPYFIRSTLINE),
new KeyValuePair<string, string>(@"DotNet4Runnable.exe", Properties.Resources.TOOL_EXPLANATION_DOTNET4RUNNABLE),
new KeyValuePair<string, string>(@"MoveTo.exe", Properties.Resources.TOOL_EXPLANATION_MOVETO),
new KeyValuePair<string, string>(@"CopyPath.exe", Properties.Resources.TOOL_EXPLANATION_COPYPATH),
new KeyValuePair<string, string>(@"RegexFilenameRenamer.exe", Properties.Resources.TOOL_EXPLANATION_REGEXFILENAMERENAMER),

// new KeyValuePair<string, string>(@"RenameToFoldername.exe", Properties.Resources.TOOL_EXPLANATION_RENAMETOFOLDERNAME),
new KeyValuePair<string, string>(@"RunFileAs.exe", Properties.Resources.TOOL_EXPLANATION_RUNASFILE),
new KeyValuePair<string, string>(@"RunOnebyOne.exe", Properties.Resources.TOOL_EXPLANATION_RUNONEBYONE),
new KeyValuePair<string, string>(@"RunWithArgs.exe", Properties.Resources.TOOL_EXPLANATION_RUNWITHARGS),
// new KeyValuePair<string, string>(@"SendToSender.exe", Properties.Resources.TOOL_EXPLANATION_SENDTOSENDER),
new KeyValuePair<string, string>(@"touch.exe", Properties.Resources.TOOL_EXPLANATION_TOUCH),
new KeyValuePair<string, string>(@"Switch3264.exe", Properties.Resources.TOOL_EXPLANATION_SWITCH3264),


//@"ShortcutRenamer.exe",
//@"ShortcutRenamer.vshost.exe",
//@"ShowEnv.exe",
//@"Switch3264.exe",


                    };


            foreach (var kv in kvpArr)
            {
                string targetfullpath = Path.Combine(ToolsDir, kv.Key);
                string shortcutfilefullpath = Path.Combine(CurrentInventoryFolder, kv.Value + ".lnk");
                if(System.IO.File.Exists(shortcutfilefullpath))
                {
                    switch(YesOrNoOrCancel(this, 
                        string.Format(Properties.Resources.ARE_YOU_SURE_REPLACE_FILE,shortcutfilefullpath),
                        false))
                    {
                        case DialogResult.No:
                            continue;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                CreateShortcutWSH(shortcutfilefullpath, targetfullpath);
            }

        }
        void constructInventory()
        {
            while (inventoryToolStripMenuItem.DropDownItems.Count > 2)
                inventoryToolStripMenuItem.DropDownItems.RemoveAt(2);

            try
            {
                bool bCreateDefaultItems = false;
                if (!Directory.Exists(InventoryRootDir))
                {
                    if (DialogResult.Yes != CppUtils.YesOrNo(Properties.Resources.DO_YOU_WANT_TO_CREATE_DEFAULT_INVENTORY))
                    {
                        Environment.Exit(0);
                        return;
                    }

                    Directory.CreateDirectory(InventoryRootDir);
                    string mainDir = Path.Combine(InventoryRootDir, "Main");
                    Directory.CreateDirectory(mainDir);
                    currentInventory_ = "Main";

                    bCreateDefaultItems = true;
                }

                DirectoryInfo di = new DirectoryInfo(InventoryRootDir);
                foreach (DirectoryInfo inv in di.GetDirectories())
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Text = inv.Name;
                    tsmi.Click += invectory_Click;
                    tsmi.Checked = CurrentInventory == inv.Name;
                    inventoryToolStripMenuItem.DropDownItems.Add(tsmi);
                }

                if (bCreateDefaultItems)
                {
                    insertDefaultItems();
                }
            }
            catch (Exception ex)
            {
                CppUtils.Alert(ex);
            }
        }

        void invectory_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            CurrentInventory = tsmi.Text;
        }


        HashIni loadedIni_;
        HashIni LoadedIni
        {
            get
            {
                if (loadedIni_ == null)
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
                if (currentInventory_ == null)
                {
                    Profile.GetString(SECTION_OPTION, KEY_CURRENT_INVENTORY, "Main", out currentInventory_, LoadedIni);
                }

                return currentInventory_;
            }
            set
            {
                if (string.Compare(value, currentInventory_, true) == 0)
                    return;

                if (!Profile.WriteString(SECTION_OPTION, KEY_CURRENT_INVENTORY, value, Program.IniFile))
                {
                    Alert(Properties.Resources.FAILED_TO_SAVE_SETTING);
                }
                currentInventory_ = value;
                constructInventory();
                UpdateList();
                UpdateTitle();
            }
        }
        void UpdateTitle()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CurrentInventory).Append(" | ").Append(ProductName);

            this.Text = sb.ToString();
        }
        IntPtr hSysImgList;
        private void FormMain_Load(object sender, EventArgs e)
        {
            NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
            hSysImgList = NativeMethods.SHGetFileInfo("",
                                                             0,
                                                             ref shfi,
                                                             (uint)Marshal.SizeOf(shfi),
                                                             NativeMethods.SHGFI_SYSICONINDEX
                                                              | NativeMethods.SHGFI_SMALLICON);
            Debug.Assert(hSysImgList != IntPtr.Zero);  // cross our fingers and hope to succeed!

            // Set the ListView control to use that image list.
            IntPtr hOldImgList = NativeMethods.SendMessage(lvMain.Handle,
                                                           NativeMethods.LVM_SETIMAGELIST,
                                                           NativeMethods.LVSIL_SMALL,
                                                           hSysImgList);

            // If the ListView control already had an image list, delete the old one.
            if (hOldImgList != IntPtr.Zero)
            {
                NativeMethods.ImageList_Destroy(hOldImgList);
            }


            // Set up the ListView control's basic properties.
            // Put it in "Details" mode, create a column so that "Details" mode will work,
            // and set its theme so it will look like the one used by Explorer.
            NativeMethods.SetWindowTheme(lvMain.Handle, "Explorer", null);

            // Get the items from the file system, and add each of them to the ListView,
            // complete with their corresponding name and icon indices.

            constructInventory();
            if (!string.IsNullOrEmpty(Program.ApplyInventory))
            {
                string folder = GetInventoryFolder(Program.ApplyInventory);
                if (!Directory.Exists(folder))
                {
                    Alert(this, string.Format(Properties.Resources.INVENTORY_NOT_EXIST, Program.ApplyInventory), true);
                }
                else
                {
                    Deploy(this, folder, Program.ApplyInventory, true);
                }
                Close();
                return;
            }
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



        private void CreateShortcutWSH(string shortcutfile, string targetpath)
        {
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = shortcutfile;
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            // shortcut.Description = "New shortcut for a Notepad";
            // shortcut.Hotkey = "Ctrl+Shift+N";
            shortcut.TargetPath = targetpath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetpath);
            shortcut.Save();
        }


        void UpDown(bool bDown)
        {
            if (lvMain.SelectedItems.Count <= 0)
                return;

            int selCount = lvMain.SelectedItems.Count;
            ListViewItem[] selItems = new ListViewItem[selCount];
            lvMain.SelectedItems.CopyTo(selItems, 0);
            if (bDown)
            {
                List<ListViewItem> revs = new List<ListViewItem>(selItems);
                revs.Reverse();
                selItems = revs.ToArray(); ;
            }

            try
            {
                lvMain.BeginUpdate();
                foreach (var item in selItems)
                {
                    // ListViewItem item = selItems[i];
                    if (item == null)
                        return;

                    int index = item.Index;
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
            }
            finally
            {
                lvMain.EndUpdate();
            }
        }
        private void tsbUp_Click(object sender, EventArgs e)
        {

            UpDown(false);

        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
            UpDown(true);
        }


        private void inventoryToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            constructInventory();
        }

        private void deployToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void openCurrentInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(CurrentInventoryFolder);
        }

        private void openSendToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(SendToFolder);
        }

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMain.SelectedItems.Count <= 0)
                return;

            //string path = lvMain.SelectedItems[0].Text;
            //path = Path.Combine(CurrentInventoryFolder, path);
            //LinkData linkData = new LinkData(path);
            //pgItem.SelectedObject = linkData;
        }

        static bool IsManagedFile(string filename)
        {
            string data;
            if (Helper.ReadAlternateStream(filename, out data))
            {
                if (data == "1")
                {
                    return true;
                }
            }
            return false;
        }
        static bool Displace(Form form, string message, bool commandRunOnly)
        {
            do
            {
                List<string> toRemoves = new List<string>();
                DirectoryInfo di = new DirectoryInfo(SendToFolder);
                FileInfo[] filesOnSendto = di.GetFiles("*.lnk", SearchOption.TopDirectoryOnly);
                foreach (FileInfo fi in filesOnSendto)
                {
                    if (IsManagedFile(fi.FullName))
                    {
                        toRemoves.Add(fi.FullName);
                    }
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(message);
                sb.AppendLine();

                foreach (string f in toRemoves)
                    sb.AppendLine("\"" + f + "\"");

                if (toRemoves.Count > 0)
                {
                    if (!Program.IsApplyNoConfirm)
                    {
                        DialogResult dr = YesOrNoOrCancel(form, sb.ToString(), commandRunOnly);
                        if (dr == DialogResult.No)
                        {
                            break;
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            return false;
                        }
                    }

                    if (0 != CppUtils.DeleteFiles(toRemoves.ToArray()))
                    {
                        Alert(form, Properties.Resources.FAILED_TO_REMOVE_FILES);
                        return false;
                    }
                }
            } while (false);
            return true;
        }

        private void tsbDeploy_Click(object sender, EventArgs e)
        {
            Deploy(this, CurrentInventoryFolder, CurrentInventory);
        }
        internal static void Deploy(Form form, string currentInvFolder, string curinv)
        {
            Deploy(form, currentInvFolder, curinv, false);
        }
        internal static void Deploy(Form form, string currentInvFolder, string curinv, bool commandRunOnly)
        {
            // first remove deployed shortcuts
            if (!Displace(form, Properties.Resources.DO_YOU_WANT_TO_REMOVE_FILES_BEFORE_DEPLOY, commandRunOnly))
                return;

            try
            {
                DirectoryInfo di = new DirectoryInfo(currentInvFolder);
                FileInfo[] srcFis = di.GetFiles("*.lnk", SearchOption.TopDirectoryOnly);


                // do copy
                string src = Path.Combine(currentInvFolder, "*.lnk");
                string dst = SendToFolder;

                int ret = CppUtils.CopyFile(src, dst);
                if (ret != 0 && ret != 1)
                {
                    Alert(form, Properties.Resources.FAILED_TO_COPY_FILES);
                    return;
                }

                // put alternate info
                foreach (FileInfo fi in srcFis)
                {
                    string fulltarget = Path.Combine(SendToFolder, fi.Name);
                    if (!Helper.WriteAlternateStream(fulltarget, "1"))
                    {
                        Alert(form, Properties.Resources.FAILED_TO_COPY_FILES);
                        return;
                    }
                }

                
                InfoBalloon(form, string.Format(Properties.Resources.INVENTORY_DEPLOYED, curinv), commandRunOnly);
            }
            catch (Exception ex)
            {
                Alert(form, ex.Message);
            }
        }

        private void tsbAssignNumber_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != CppUtils.CenteredMessageBox(this,
                Properties.Resources.ASK_ASSIGN_NUMBERS,
                ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question))
            {
                return;
            }
            UpdateList(true);
        }

        private void tsbNewItem_Click(object sender, EventArgs e)
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
                if (System.IO.File.Exists(shortcutfilefullpath))
                {
                    if (DialogResult.Yes !=
                        YesOrNo(
                        string.Format(
                        Properties.Resources.SHORTCUT_ALREADY_EXISTS, shortcutfilefullpath))
                        )
                    {
                        return;
                    }
                }

                try
                {
                    CreateShortcutWSH(shortcutfilefullpath, ofd.FileName);
                }
                catch (Exception ex)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine(Properties.Resources.SHORTCUT_CREATION_FAILED);
                    sb.AppendLine(ex.Message);

                    CppUtils.CenteredMessageBox(
                        this,
                        sb.ToString(),
                        ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                UpdateList();
            }


        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbDisplace_Click(object sender, EventArgs e)
        {
            Displace(this, Properties.Resources.DO_YOU_WANT_TO_REMOVE_FILES, false);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            HashIni ini = Profile.ReadAll(Program.IniFile);

            AmbLib.SaveFormXYWH(this, SECTION_OPTION, ini);

            AmbLib.SaveListViewColumnWidth(lvMain, SECTION_OPTION, KEY_COLUMN, ini);


            Profile.WriteInt(SECTION_OPTION, KEY_LISTVIEWCOLOR1, option_.btnLVColor1.BackColor.ToArgb(), ini);
            Profile.WriteInt(SECTION_OPTION, KEY_LISTVIEWCOLOR2, option_.btnLVColor2.BackColor.ToArgb(), ini);

            if (!Profile.WriteAll(ini, Program.IniFile))
            {
                Alert(Properties.Resources.FAILED_TO_SAVE_SETTING);
            }
        }

        private void RefreshItems()
        {
            lvMain.BeginUpdate();
            UpdateList();
            lvMain.EndUpdate();
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void lvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (lvMain.SelectedItems.Count != 0)
                {
                    lvMain.StartEditing(GetEdittingControl(lvMain.Columns["Name"]),
                        lvMain.SelectedItems[0], 0);
                }
            }
        }

        private void cmbEditDirectory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                string defaultvalue = ei_.Initial;
                fbd.SelectedPath = defaultvalue;
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    cmbEditDirectory.Text = defaultvalue;
                    lvMain.EndEditing(false);
                    return;
                }

                lvMain_SubItemEndEditingWork(ei_.item_, ei_.subitem_, fbd.SelectedPath, fbd.SelectedPath);
                //ei_.Result = fbd.SelectedPath;
                //lvMain.EndEditing(true);
            }
        }

        private void cmbEditFile_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (System.IO.File.Exists(ei_.Initial))
                {
                    ofd.InitialDirectory = Path.GetDirectoryName(ei_.Initial);
                    ofd.FileName = Path.GetFileName(ei_.Initial);
                }
                if (DialogResult.OK != ofd.ShowDialog())
                {
                    cmbEditFile.Text = ei_.Initial;
                    lvMain.EndEditing(false);
                    return;
                }

                // ei_.Result = ofd.FileName;
                lvMain_SubItemEndEditingWork(ei_.item_, ei_.subitem_, ofd.FileName, ofd.FileName);
                // lvMain.EndEditing(true);
            }
        }

        // https://stackoverflow.com/a/283917
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private string getToolVersion(string exe, bool bNative)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Path.GetFileNameWithoutExtension(exe));
            sb.Append(" version ");
            try
            {
                string fullpath = Path.Combine(AssemblyDirectory, exe);
                if (bNative)
                {
                    sb.Append(CppUtils.GetNativeVersionString(fullpath));
                }
                else
                {
                    sb.Append(AssemblyName.GetAssemblyName(fullpath).Version.ToString());
                }
            }
            catch
            {
            }
            return sb.ToString();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ProductName);
            sb.Append(" version ");
            sb.Append(Assembly.GetExecutingAssembly().GetName().Version.Major.ToString());
            sb.Append(".");
            sb.Append(Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString());
            sb.Append(".");
            sb.Append(Assembly.GetExecutingAssembly().GetName().Version.Build.ToString());

            sb.AppendLine();
            sb.AppendLine();

            sb.AppendLine(getToolVersion("ChangeFileName.exe",false));
            sb.AppendLine(getToolVersion("ChangeFileTime.exe", false));
            sb.AppendLine(getToolVersion("CopyFileContent.exe", false));
            sb.AppendLine(getToolVersion("CopyFirstLine.exe", false));
            sb.AppendLine(getToolVersion("CopyPath.exe", true));
            sb.AppendLine(getToolVersion("DotNet4Runnable.exe", false));
            sb.AppendLine(getToolVersion("MoveTo.exe", true));
            sb.AppendLine(getToolVersion("RegexFilenameRenamer.exe", false));
            sb.AppendLine(getToolVersion("RunFileAs.exe", true));
            sb.AppendLine(getToolVersion("RunOnebyOne.exe", false));
            sb.AppendLine(getToolVersion("RunWithArgs.exe", false));
            sb.AppendLine(getToolVersion("Switch3264.exe", true));
            sb.AppendLine(getToolVersion("touch.exe", false));

            
            CppUtils.CenteredMessageBox(this,
                sb.ToString(),
                ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        Option option_ = new Option();
        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != option_.ShowDialog())
                return;

            UpdateList();
        }

        // The LVItem being dragged
        private ListViewItem _itemDnD = null;
        private bool _mouseDowning;
        private int _mouseDownStartTick;
        private void lvMain_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDowning = true;
            _mouseDownStartTick = Environment.TickCount;
            _itemDnD = lvMain.GetItemAt(e.X, e.Y);
        }

        private void lvMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDowning && (Environment.TickCount - _mouseDownStartTick) > 100)
            {

            }
            else
            {
                _itemDnD = null;
            }
            if (_itemDnD == null)
                return;

            // drag begines
            lvMain.ListViewItemSorter = null;

            // Show the user that a drag operation is happening
            Cursor = Cursors.Hand;

            // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
            int lastItemBottom = Math.Min(e.Y, lvMain.Items[lvMain.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

            // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
            ListViewItem itemOver = lvMain.GetItemAt(0, lastItemBottom);

            if (itemOver == null)
                return;

            Rectangle rc = itemOver.GetBounds(ItemBoundsPortion.Entire);
            if (e.Y < rc.Top + (rc.Height / 2))
            {
                lvMain.LineBefore = itemOver.Index;
                lvMain.LineAfter = -1;
            }
            else
            {
                lvMain.LineBefore = -1;
                lvMain.LineAfter = itemOver.Index;
            }

            // invalidate the LV so that the insertion line is shown
            lvMain.Invalidate();
        }

        private void lvMain_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDowning = false;
            if (_itemDnD == null)
                return;

            try
            {
                // calculate the bottom of the last item in the LV so that you don't have to stop your drag at the last item
                int lastItemBottom = Math.Min(e.Y, lvMain.Items[lvMain.Items.Count - 1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1);

                // use 0 instead of e.X so that you don't have to keep inside the columns while dragging
                ListViewItem itemOver = lvMain.GetItemAt(0, lastItemBottom);

                if (itemOver == null)
                    return;

                Rectangle rc = itemOver.GetBounds(ItemBoundsPortion.Entire);

                // find out if we insert before or after the item the mouse is over
                bool insertBefore;
                if (e.Y < rc.Top + (rc.Height / 2))
                {
                    insertBefore = true;
                }
                else
                {
                    insertBefore = false;
                }

                if (_itemDnD != itemOver) // if we dropped the item on itself, nothing is to be done
                {
                    if (insertBefore)
                    {
                        lvMain.Items.Remove(_itemDnD);
                        lvMain.Items.Insert(itemOver.Index, _itemDnD);
                    }
                    else
                    {
                        lvMain.Items.Remove(_itemDnD);
                        lvMain.Items.Insert(itemOver.Index + 1, _itemDnD);
                    }
                }

                // clear the insertion line
                lvMain.LineAfter =
                lvMain.LineBefore = -1;

                lvMain.Invalidate();

            }
            finally
            {
                // finish drag&drop operation
                _itemDnD = null;
                Cursor = Cursors.Default;
            }
        }

        private void addInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string target = null;
            if (!GetTextDialog.DoModalDialog(this,
                Properties.Resources.ENTER_INVECTORY_NAME,
                Properties.Resources.INVENTORY_NAME,
                ref target))
            {
                return;
            }

            CurrentInventory = target;
        }

        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            lvMain.EndEditing(false);
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> dupSrc = new List<string>();
            List<string> dupDst = new List<string>();
            foreach (ListViewItem lvi in lvMain.SelectedItems)
            {
                string filename = GetLVInfo(lvi).FullName;
                if (!System.IO.File.Exists(filename))
                {
                    Alert(string.Format(Properties.Resources.SELECTED_FILE_DOESNOT_EXIST_REFRESH_FIRST,
                        filename));
                    return;
                }

                string dir = Path.GetDirectoryName(filename);
                string name = Path.GetFileNameWithoutExtension(filename);
                string ext = Path.GetExtension(filename);

                string dstFull;
                for (int i = 1; ; ++i)
                {
                    string newname = name + "_" + i.ToString();
                    string test = Path.Combine(dir, newname) + ext;
                    if (!System.IO.File.Exists(test))
                    {
                        dstFull = test;
                        break;
                    }
                }
                dupSrc.Add(filename);
                dupDst.Add(dstFull);
            }
            if (dupSrc.Count == 0)
                return;
            if (dupSrc.Count != dupDst.Count)
            {
                Alert("Unexpected Error");
                return;
            }
            CppUtils.CopyFiles(dupSrc.ToArray(), dupDst.ToArray());
            RefreshItems();
        }

        LVInfo GetLVInfo(ListViewItem lvi)
        {
            if (lvi == null)
                return null;

            return (LVInfo)lvi.Tag;
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> delItems = new List<ListViewItem>();
            foreach (ListViewItem lvi in lvMain.SelectedItems)
            {
                delItems.Add(lvi);
            }

            List<string> delFiles = new List<string>();
            foreach (ListViewItem lvi in delItems)
            {
                LVInfo lvInfo = GetLVInfo(lvi);
                string filename = lvInfo.FullName;
                if (System.IO.File.Exists(filename))
                {
                    delFiles.Add(filename);
                }
            }
            if (delFiles.Count == 0)
                return;

            if(DialogResult.Yes != CppUtils.YesOrNo(this,
                string.Format(Properties.Resources.ARE_YOU_SURE_DELETE_FILE,delFiles.Count),
                MessageBoxDefaultButton.Button2))
            {
                return;
            }

            if (0 == CppUtils.DeleteFiles(delFiles.ToArray()))
            {
                foreach (var item in delItems)
                    lvMain.Items.Remove(item);
            }
        }

        private void refershToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void insertDefaultItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor preCursor = Cursor.Current;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                insertDefaultItems();
                RefreshItems();
            }
            finally
            {
                Cursor.Current = preCursor;
            }
        }
    }
}