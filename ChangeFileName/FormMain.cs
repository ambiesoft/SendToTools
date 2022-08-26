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
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using Ambiesoft;
using System.Web;

namespace ChangeFileName
{
    public partial class FormMain : Form, Miszou.ToolManager.ToolManagerCallback
    {
        static readonly string SECTION_SETTING = "settings";
        static readonly string KEY_SMARTDOUBLECLICKSELECTION = "SmartDoubleClickSelection";
        static readonly string KEY_MOVELASTSELECTEDFOLDERTOTOP = "MoveLastSelectedFolderToTop";
        static readonly string KEY_TOPMOST = "TopMost";
        static readonly string KEY_REGREXES_NAME = "RegExeName";
        static readonly string KEY_REGREXES_REGEX = "RegExeRegex";
        static readonly string KEY_REGREXES_REPLACEMENT = "RegExeReplacement";

        List<RegexItem> customRegexes_ = new List<RegexItem>();

        public static string IniFile
        {
            get { return AmbLib.GetIniPath(); }
        }
        public static string IniFolder
        {
            get
            {
                string inipath = AmbLib.GetIniPath();
                return System.IO.Path.GetDirectoryName(inipath);
            }
        }

        public FormMain()
        {
            InitializeComponent();

            tlpInfo.Margin = new Padding(0);
            tlpInfo.Padding = new Padding(0);

            HashIni ini = Profile.ReadAll(IniFile);
            int x, y;
            Ambiesoft.Profile.GetInt(SECTION_SETTING, "X", 60, out x, ini);
            Ambiesoft.Profile.GetInt(SECTION_SETTING, "Y", 60, out y, ini);

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
                Profile.GetInt(SECTION_SETTING, "Width", -1, out width, ini);
                Profile.GetInt(SECTION_SETTING, "Height", -1, out height, ini);
                if (width > 0 && height > 0)
                    this.Size = new Size(width, height);
            }


            int intval;
            bool boolval;
            if (Ambiesoft.Profile.GetInt(SECTION_SETTING, "AutoRun", 0, out intval, ini))
            {
                chkAutoRun.Checked = intval != 0;
            }

            Ambiesoft.Profile.GetBool(SECTION_SETTING, KEY_TOPMOST, false, out boolval, ini);
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked = boolval;


            Profile.GetBool(SECTION_SETTING, KEY_SMARTDOUBLECLICKSELECTION, true, out boolval, ini);
            tsmiSmartDoubleClickSelection.Checked = boolval;

            Profile.GetBool(SECTION_SETTING, KEY_MOVELASTSELECTEDFOLDERTOTOP, true, out boolval, ini);
            tsmiMoveLastSelectionFolderToTop.Checked = boolval;

            string[] arRegexName;
            Profile.GetStringArray(SECTION_SETTING, KEY_REGREXES_NAME,
                out arRegexName, ini);
            string[] arRegexRegex;
            Profile.GetStringArray(SECTION_SETTING, KEY_REGREXES_REGEX,
                out arRegexRegex, ini);
            string[] arRegexReplacement;
            Profile.GetStringArray(SECTION_SETTING, KEY_REGREXES_REPLACEMENT,
                out arRegexReplacement, ini);
            Debug.Assert(customRegexes_.Count == 0);
            for (int i = 0; i < arRegexRegex.Length; ++i)
            {
                string name = string.Empty;
                string regex = string.Empty;
                string replacement = string.Empty;
                try
                {
                    name = HttpUtility.UrlDecode(arRegexName[i]);
                }
                catch (Exception) { }
                regex = HttpUtility.UrlDecode(arRegexRegex[i]);
                try
                {
                    replacement = HttpUtility.UrlDecode(arRegexReplacement[i]);
                }
                catch (Exception) { }

                customRegexes_.Add(new RegexItem(name, regex, replacement));
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                txtName.SelectAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            Program.SafeProcessStart(this.txtName.Tag.ToString(), true);
        }



        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = Clipboard.GetText();
            }
            catch (Exception) { }
        }

        List<UndoInfo> _undoBuffer = new List<UndoInfo>();
        bool _unreDoing;
        int _currentUnreIndex = -1;
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (_unreDoing)
                return;

            if (txtName.Lines.Length > 1)
            {
                txtName.Text = txtName.Lines[0];
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

                _undoBuffer.Add(new UndoInfo(txtName.Text, txtName.SelectionStart, txtName.SelectionLength));
                ++_currentUnreIndex;
                return;
            }

        }

        private void btnTrash_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes != CppUtils.YesOrNo(string.Format(Properties.Resources.ARE_YOU_SURE_TO_TRASH, txtName.Tag.ToString()),
                MessageBoxDefaultButton.Button2))
            {
                return;
            }

            List<Control> cs = disableAll();
            try
            {
                //FileSystem.DeleteFile(
                //  this.txtName.Tag.ToString(),
                //  UIOption.OnlyErrorDialogs,
                //  RecycleOption.SendToRecycleBin);

                if(0==CppUtils.DeleteFile(this, this.txtName.Tag.ToString()))
                    Close();
            }
            catch (Exception)
            {
                //using (new Ambiesoft.CenteringDialog(this))
                //{
                //    MessageBox.Show(ex.Message,
                //    Application.ProductName,
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Exclamation);
                //}
            }
            finally
            {
                enableAll(cs);
            }
        }





        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            HashIni ini = Profile.ReadAll(IniFile);

            Profile.WriteInt(SECTION_SETTING, "X", Location.X, ini);
            Profile.WriteInt(SECTION_SETTING, "Y", Location.Y, ini);
            Profile.WriteInt(SECTION_SETTING, "Width", Size.Width, ini);
            Profile.WriteInt(SECTION_SETTING, "Height", Size.Height, ini);

            Profile.WriteBool(SECTION_SETTING, KEY_SMARTDOUBLECLICKSELECTION,
                tsmiSmartDoubleClickSelection.Checked, ini);
            Profile.WriteBool(SECTION_SETTING, KEY_MOVELASTSELECTEDFOLDERTOTOP,
                tsmiMoveLastSelectionFolderToTop.Checked, ini);

            // save regex
            List<string> regexNames = new List<string>();
            List<string> regexRegexes = new List<string>();
            List<string> regexReplacements = new List<string>();
            foreach (var regexitem in customRegexes_)
            {
                regexNames.Add(HttpUtility.UrlEncode(regexitem.Name));
                regexRegexes.Add(HttpUtility.UrlEncode(regexitem.RegexString));
                regexReplacements.Add(HttpUtility.UrlEncode(regexitem.Replacement));
            }
            Profile.WriteStringArray(SECTION_SETTING, KEY_REGREXES_NAME,
                regexNames.ToArray(), ini);
            Profile.WriteStringArray(SECTION_SETTING, KEY_REGREXES_REGEX,
                regexRegexes.ToArray(), ini);
            Profile.WriteStringArray(SECTION_SETTING, KEY_REGREXES_REPLACEMENT,
                regexReplacements.ToArray(), ini);
            
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

        void afterLoad(object sender, EventArgs e)
        {            
            // select all texts and put the cursor on top
            txtName.Focus();
            // key stroke 'END'-> 'Shift+HOME'
            SendKeys.Send("{END}+{HOME}");
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            string title = string.Format("{0} - {1}",
                AmbLib.doubleQuoteIfSpace(Path.GetFileName(txtName.Tag.ToString())),
                ProductName);
            this.Text = title;

            if (chkAutoRun.Checked || Program.run_)
            {
                Program.SafeProcessStart(this.txtName.Tag.ToString(), true);
            }

            try
            {
                FileInfo fi = new FileInfo(this.txtName.Tag.ToString());
                StringBuilder sb = new StringBuilder();
                sb.Append(Properties.Resources.Size);
                sb.Append(": ");
                sb.Append(SizeSuffix(fi.Length));
                lblFileInfo.Text = sb.ToString();
                lblExtention.Text = Properties.Resources.Extention + ": " + fi.Extension;
            }
            catch (Exception) { }

            BeginInvoke(new EventHandler(this.afterLoad), sender, e);
        }

        private void chkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            Ambiesoft.Profile.WriteInt(SECTION_SETTING, "AutoRun", chkAutoRun.Checked ? 1 : 0, IniFile);
        }

        private void btnCopyPath_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.txtName.Tag.ToString());
            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex.Message);
            }
        }






        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtName.Text);
            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex.Message);
            }
        }

        void showDamemojiError()
        {
            CppUtils.CenteredMessageBox(this,
                  Properties.Resources.FOLLOWING_UNABLE_FILENAME + Environment.NewLine + Program.damemoji,
                  Application.ProductName,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string newName = txtName.Text;
            if (string.IsNullOrEmpty(newName))
            {
                CppUtils.CenteredMessageBox(this,
                    Properties.Resources.ENTER_FILENAME,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (-1 != newName.IndexOfAny(Program.damemoji.ToCharArray()))
            {
                showDamemojiError();
                return;
            }

            List<Control> backToEnables = disableAll();
            if (!Program.RenameIt(this, txtName.Tag.ToString(), newName))
            {
                enableAll(backToEnables);
                return;
            }
            enableAll(backToEnables);
            this.DialogResult = DialogResult.OK;
            Close();
        }
        void enableAll(List<Control> cs)
        {
            foreach (Control c in cs)
                c.Enabled = true;
        }
        List<Control> disableAll()
        {
            List<Control> ret = new List<Control>();
            foreach(Control c in this.Controls)
            {
                if(c.Enabled)
                {
                    c.Enabled = false;
                    ret.Add(c);
                }
            }
            return ret;
        }
        private void btnExplorer_Click(object sender, EventArgs e)
        {
            string path = this.txtName.Tag.ToString();
            string arg = "/select,\"" + path + "\",/n";
            System.Diagnostics.Process.Start("explorer.exe", arg);
        }


        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
            Ambiesoft.Profile.WriteBool(SECTION_SETTING, KEY_TOPMOST, alwaysOnTopToolStripMenuItem.Checked, IniFile);
        }

     
        private void tsmPasteAtStart_Click(object sender, EventArgs e)
        {
            try
            {
                string addingText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(addingText))
                    txtName.Text = addingText + " " + txtName.Text;
            }
            catch (Exception) { }
        }
        private void tsmPasteAtEnd_Click(object sender, EventArgs e)
        {
            try
            {
                string addingText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(addingText))
                    txtName.Text = txtName.Text + " " + addingText;
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
                    
                    UndoInfo ui = _undoBuffer[_currentUnreIndex];

                    txtName.Text = ui.Text;
                    txtName.SelectionStart = ui.Start;
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

                    UndoInfo ui = _undoBuffer[_currentUnreIndex];
                    txtName.Text = ui.Text;
                    txtName.SelectionStart = ui.Start;
                }
            }
            catch (Exception)
            {

            }
            _unreDoing = false;
        }

   

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Application.ProductName);
            sb.Append(" version ");
            sb.Append(AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly(),3));
            sb.AppendLine();
            // sb.Append("Copyright 2018 Ambiesoft");
            sb.Append(AmbLib.getAssemblyCopyright(Assembly.GetExecutingAssembly()));
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
                Process.Start("https://ambiesoft.github.io/webjumper/?target=sendtotools");
            }
            catch (Exception ex)
            {
                CppUtils.Fatal(ex);
            }

        }

        bool IsZenkakuKatakana(char c)
        {
            // ヽ(0x30FD) ヾ(0x30FE)
            if (('ァ' <= c && c <= 'ヶ') ||
                ('ヽ' <= c && c <= 'ヾ'))
            {
                return true;
            }
            // ヷ(0x30F7) ～ ヺ(0x30FA)
            else if ('ヷ' <= c && c <= 'ヺ')
            {
                return true;
            }
            return false;
        }
        bool IsAsciiChar(char c)
        {
            if ('0' <= c && c <= '9')
                return true;
            if ('a' <= c && c <= 'z')
                return true;
            if ('A' <= c && c <= 'Z')
                return true;
            return false;
        }
        bool IsSpaceChar(char c)
        {
            return char.IsWhiteSpace(c);
        }
        enum MojiType {
            Unknown,
            ZenkakuKatakana,
            AsciiChar,
            SpaceChar
        }
        MojiType GetMojiType(char c)
        {
            if (IsZenkakuKatakana(c))
                return MojiType.ZenkakuKatakana;
            if (IsAsciiChar(c))
                return MojiType.AsciiChar;
            if (IsSpaceChar(c))
                return MojiType.SpaceChar;
            return MojiType.Unknown;
        }
        int _dbTick;
        private void txtName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!tsmiSmartDoubleClickSelection.Checked)
                return;

            _dbTick = Environment.TickCount;

            string selText = txtName.SelectedText;
            if (string.IsNullOrEmpty(selText))
                return;

            // If selection contains space between words,
            // Assumes that user does following action, in that case disable this function
            // 1, double-click
            // 2, drag to select more than one words
            foreach (char c in selText.Trim())
            {
                if (GetMojiType(c) == MojiType.SpaceChar)
                    return;
            }

            char startC = selText[0];
            MojiType startMT = GetMojiType(startC);

            // expand tail until mojitype are same
            int newEnd = txtName.SelectionStart + 1;
            for (; newEnd < txtName.TextLength; ++newEnd)
            {
                char c = txtName.Text[newEnd];
                if (GetMojiType(c) != startMT)
                    break;
            }
            int selLen = newEnd - txtName.SelectionStart;// +txtName.SelectionLength - 1;

            // expand front
            int newStart = txtName.SelectionStart;
            for(; newStart>=0;--newStart)
            {
                char c = txtName.Text[newStart];
                if (GetMojiType(c) != startMT)
                    break;
            }

            selLen += txtName.SelectionStart - newStart - 1;

            txtName.SelectionStart = newStart+1;
            txtName.SelectionLength = selLen;
        }
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetDoubleClickTime();
        private void txtName_MouseClick(object sender, MouseEventArgs e)
        {
            if (GetDoubleClickTime() > (Environment.TickCount - _dbTick))
            {
                Debug.WriteLine("Triple clicked.");
                onTripleClickText();
            }
        }
        void onTripleClickText()
        {
            txtName.SelectAll();
        }

        Miszou.ToolManager.Tools _mTools;
        static int _emptyToolsCount = -1;
        const string EXTERNAL_MACRO_FILE = "$(File)";
        Regex _extMacroRegex;

        string extRegTrans(Match match)
		{
			string x = match.ToString();

			if (x == EXTERNAL_MACRO_FILE)
			{
                x = txtName.Tag.ToString(); 
			}
			return x;
		}
        string ExpandToolMacros(string str)
        {
            if (_extMacroRegex == null)
                _extMacroRegex = new Regex("\\$\\([^)]*\\)");

            return _extMacroRegex.Replace(str, new MatchEvaluator(extRegTrans));
        }
        private void toolsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (_emptyToolsCount == -1)
            {
                _emptyToolsCount = toolsToolStripMenuItem.DropDownItems.Count;
            }

            string toolfile = Path.Combine(IniFolder, "ChangeFileNameTools.xml");
            List<Miszou.ToolManager.Macro> macroList = new List<Miszou.ToolManager.Macro>();
            macroList.Add(new Miszou.ToolManager.Macro(EXTERNAL_MACRO_FILE, "File"));

            List<Miszou.ToolManager.Macro> folderList = new List<Miszou.ToolManager.Macro>();
            ImageList toolImages = new ImageList();
            _mTools = null;
		    do
			{
				try
				{
					_mTools = new Miszou.ToolManager.Tools(
						toolfile,
						macroList,
						folderList,
						new Miszou.ToolManager.Tools.MacroExpander(ExpandToolMacros),
						toolImages);
				}
				catch (Exception ex)
				{
                    string message = string.Format(Properties.Resources.FAILED_TO_LOAD_TOOLXML,
                        toolfile, ex.Message);
					if (CppUtils.CenteredMessageBox(
                        message,
						Application.ProductName,
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Error) != DialogResult.OK)
					{
                        System.Environment.Exit(-1);
					}
				}
            } while (_mTools == null);

            int startIndex = toolsToolStripMenuItem.DropDownItems.IndexOf(tsmsBeforeTools);
            // int endIndex = toolsToolStripMenuItem.DropDownItems.IndexOf(tsmsAfterTools);

            while (toolsToolStripMenuItem.DropDownItems.Count > _emptyToolsCount)
                toolsToolStripMenuItem.DropDownItems.RemoveAt(startIndex + 1);

            if (_mTools.Count == 0)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = Properties.Resources.NO_EXTERNAL_TOOLS_REGISTERED;
				item.Enabled = false;
                toolsToolStripMenuItem.DropDownItems.Insert(startIndex + 1, item);
            }
            else
            {
                _mTools.BuildToolMenu(toolsToolStripMenuItem, startIndex + 1);
            }
        }

        public void OnToolManagerFormLoaded(Form form)
        {
            form.TopMost = TopMost;
        }

        private void addModifyToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
			{
                if (DialogResult.OK !=
                    _mTools.Edit(Miszou.ToolManager.Tools.EditFlags.AllowLockedUIEdit, this)
                    )
                {
                    return;
                }
			}
			catch (Exception ex)
			{
				CppUtils.Alert(ex.Message);
				return;
			}
        }

        private void tsmiRevealInFolder_Click(object sender, EventArgs e)
        {
            Ambiesoft.CppUtils.OpenFolder(this, txtName.Tag.ToString());
        }

        

        private void tsmiNewReplaceToo_Click(object sender, EventArgs e)
        {
            using (ReplaceToolDialog npt = new ReplaceToolDialog(txtName.Text))
            {
                if (DialogResult.OK != npt.ShowDialog(this))
                    return;
                customRegexes_.Add(new RegexItem(
                    npt.RegexName,
                    npt.RegExString,
                    npt.RegExReplacement));
            }
        }

        private void tsmiReplace_DropDownOpeningCommon(
            bool bAll,
            ToolStripMenuItem tsmi,
            ToolStripSeparator sep)
        {
            // arrange custom reg menus
            int sepIndex = tsmi.DropDownItems.IndexOf(sep);
            Debug.Assert(sepIndex >= 0);
            while(tsmi.DropDownItems.Count > (1+sepIndex))
                tsmi.DropDownItems.RemoveAt(sepIndex + 1);

            foreach(var ritem in customRegexes_)
            {
                var newItem = new ToolStripMenuItem();
                newItem.Text = ritem.Name;
                newItem.Tag = new RegexItem(
                    ritem.Name,
                    ritem.RegexString,
                    ritem.Replacement,
                    bAll);
                newItem.Click += NewItem_Click;
                tsmi.DropDownItems.Add(newItem);
            }
            if (customRegexes_.Count != 0)
                tsmi.DropDownItems.Add(new ToolStripSeparator());
            tsmi.DropDownItems.Add(tsmiReplaceTool);
        }
        private void tsmiReplaceAll_DropDownOpening(object sender, EventArgs e)
        {
            tsmiReplace_DropDownOpeningCommon(true, tsmiReplaceAll, sepBeforeCustomRegsAll);
        }
        private void tsmiReplaceSelection_DropDownOpening(object sender, EventArgs e)
        {
            bool bHasSelection = txtName.SelectionLength != 0;

            foreach (ToolStripItem tsi in tsmiReplaceSelection.DropDownItems)
            {
                tsi.Enabled = bHasSelection;
            }
            tsmiReplace_DropDownOpeningCommon(false, tsmiReplaceSelection, sepBeforeCustomRegsSelection);
        }


        private void NewItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            RegexItem ritem = item.Tag as RegexItem;
            ChangeSelectionCommon(new Converter((string input) =>
            {
                try
                {
                    return Regex.Replace(input, ritem.RegexString, ritem.Replacement);
                }
                catch(Exception ex)
                {
                    CppUtils.Alert(ex);
                    return string.Empty;
                }
            }),!ritem.IsAll);
        }

        private void tsmiEditOrDelete_DropDownOpeningCommon(bool bDelete,
            ToolStripMenuItem tsmi)
        {
            tsmi.DropDownItems.Clear();
            foreach (RegexItem ri in customRegexes_)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = ri.Name;
                item.Tag = ri;
                if(!bDelete)
                    item.Click += RegItemEdit_Click;
                else
                    item.Click += RegItemDelete_Click;
                tsmi.DropDownItems.Add(item);
            }
            if (customRegexes_.Count == 0)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = Properties.Resources.STR_NO_REPLACE_TOOLS;
                item.Enabled = false;
                tsmi.DropDownItems.Add(item);
            }
        }
        private void tsmiEditReplaceTool_DropDownOpening(object sender, EventArgs e)
        {
            tsmiEditOrDelete_DropDownOpeningCommon(false, tsmiEditReplaceTool);
        }
        private void tsmiDeleteReplaceTool_DropDownOpening(object sender, EventArgs e)
        {
            tsmiEditOrDelete_DropDownOpeningCommon(true, tsmiDeleteReplaceTool);
        }
        private void RegItemEdit_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            RegexItem ri = item.Tag as RegexItem;
            using (var dlg = new ReplaceToolDialog(txtName.Text, ri))
            {
                if (DialogResult.OK != dlg.ShowDialog(this))
                    return;
                ri.reset(dlg.RegexName, dlg.RegExString, dlg.RegExReplacement);
            }
        }
        private void RegItemDelete_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            RegexItem ri = item.Tag as RegexItem;

            if (DialogResult.Yes != CppUtils.YesOrNo(
                string.Format(Properties.Resources.STR_DOYOUDELETE_REGITEM, ri.Name),
                MessageBoxDefaultButton.Button2))
            {
                return;
            }

            if (!customRegexes_.Remove(ri))
                CppUtils.Alert(Properties.Resources.STR_FAILED_DELETE_REGITEM);
        }
    }
}