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


namespace ChangeFileName
{
    public partial class FormMain : Form
    {


        delegate string Converter(string s);
        void ChangeSelectionCommon(Converter converter)
        {
            if (textName.SelectionLength == 0)
                return;

            int start = textName.SelectionStart;

            string s = textName.SelectedText;
            s = converter.Invoke(s);
            textName.SelectedText = s;

            textName.Select(start, s.Length);
            textName.Focus();
        }


        private void ToFileNamable_Click(object sender, EventArgs e)
        {
            textName.Text = Ambiesoft.AmbLib.GetFilaNamableName(textName.Text);
        }
        private void ToMakeFileNamableSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(Ambiesoft.AmbLib.GetFilaNamableName);
        }


        string tolower(string s)
        {
            return s.ToLower();
        }
        private void ToLower_Click(object sender, EventArgs e)
        {
            textName.Text = tolower(textName.Text);
        }
        private void ToLowerSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(tolower);
        }


        string toupper(string s)
        {
            return s.ToUpper();
        }
        private void ToUpper_Click(object sender, EventArgs e)
        {
            textName.Text = toupper(textName.Text);
        }
        private void ToUpperSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(toupper);
        }


        string trim(string s)
        {
            return s.Trim();
        }
        private void Trim_Click(object sender, EventArgs e)
        {
            textName.Text = trim(textName.Text);
        }
        private void TrimSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(trim);
        }


        private string RemoveSpace(string s)
        {
            s = s.Replace(" ", "");
            s = s.Replace("Å@", "");
            return s;
        }
        private void ToRemoveSpace_Click(object sender, EventArgs e)
        {
            textName.Text = RemoveSpace(textName.Text);
        }
        private void ToRemoveSpaceSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(RemoveSpace);
        }


        private string Underbar2Hyphen(string s)
        {
            s = s.Replace("_", "-");
            return s;
        }
        private void tsmiUnderbar2hyphen_Click(object sender, EventArgs e)
        {
            textName.Text = Underbar2Hyphen(textName.Text);
        }
        private void tsmiUnderbar2hyphenSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(Underbar2Hyphen);
        }


        private string Cn2Jp(string s)
        {
            Dictionary<string, string> table = new Dictionary<string, string>();

            try
            {
                string thefile = "\\\\Inpsrv\\Share\\pass\\text\\jpcn.txt";

                using (StreamReader sr = new StreamReader(thefile, Encoding.UTF8))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        string[] ar = line.Split('\t');

                        if (ar == null || ar.Length != 2)
                        {
                            continue;
                        }

                        table[ar[1]] = ar[0];
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return s;
            }
            catch (Exception)
            {
                return s;
            }

            string ret = string.Empty;
            foreach (char c in s)
            {
                if (table.ContainsKey(c.ToString()))
                {
                    ret += table[c.ToString()];
                }
                else
                {
                    ret += c;
                }
            }
            return ret;
        }
        private void tsmiCn2Jp_Click(object sender, EventArgs e)
        {
            textName.Text = Cn2Jp(textName.Text);
        }
        private void tsmiCn2JpSel_Click(object sender, EventArgs e)
        {
            ChangeSelectionCommon(Cn2Jp);
        }

    }
}