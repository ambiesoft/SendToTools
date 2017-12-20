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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RunWithArgs
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        [DllImport("shell32.dll")]
        static extern int FindExecutable(string lpFile, string lpDirectory, [Out] StringBuilder lpResult);

        string getExe(string file)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                FindExecutable(txtExe.Text, "", sb);
                return sb.ToString();
            }
            catch { }
            return string.Empty;
        }
        bool isExe(string file)
        {
            switch (ShellFileGetInfo.GetExeType(file))
            {
                //case ShellFileGetInfo.ShellFileType.Unknown:
                //    // System.Diagnostics.Debug.WriteLine("Unknown: " + file);
                //    return false;
                case ShellFileGetInfo.ShellFileType.Dos:
                    // System.Diagnostics.Debug.WriteLine("DOS: " + file);
                    return true;
                case ShellFileGetInfo.ShellFileType.Windows:
                    // System.Diagnostics.Debug.WriteLine("Windows: " + file);
                    return true;
                case ShellFileGetInfo.ShellFileType.Console:
                    // System.Diagnostics.Debug.WriteLine("Console: " + file);
                    return true;
            }
            return false;
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            string fileName = txtExe.Text;
            string arguments = txtArg.Text;

            // When user tries to launch normal file with arguments,
            // We'll find executable and append original argument after
            // user-input argument.
            if (!string.IsNullOrEmpty(txtArg.Text) && !isExe(txtExe.Text))
            {

                string exe = getExe(txtExe.Text);
                fileName = exe;
                if (!string.IsNullOrEmpty(exe))
                {
                    if (!string.IsNullOrEmpty(arguments))
                    {
                        arguments += " " + Ambiesoft.AmbLib.doubleQuoteIfSpace(txtExe.Text);
                    }
                    else
                    {
                        arguments = Ambiesoft.AmbLib.doubleQuoteIfSpace(txtExe.Text);
                    }
                }

            }
            ProcessStartInfo si = new ProcessStartInfo();
            si.UseShellExecute = true;
            si.FileName = fileName;
            si.Arguments = arguments;
            if (chkRunas.Checked)
                si.Verb = "runas";
            si.WorkingDirectory = System.IO.Path.GetDirectoryName(txtExe.Text);
            
            try
            {
                Process.Start(si);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}