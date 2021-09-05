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

using Ambiesoft;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChangeFileTime
{
    static class Program
    {
        [STAThread]
        static void Main(String[] args)
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new FormMain());
            if (args.Length < 1)
            {
                CppUtils.Alert(Properties.Resources.NO_ARGUMENTS);
                return;
            }

            //string theFileName = @"C:\Documents and Settings\tt\My Documents\Productivity Distribution, Firm Heterogeneity, and Agglomeration.pdf";
            string theFileName = args[0];

            if (!System.IO.File.Exists(theFileName) )//&& !System.IO.Directory.Exists(theFileName))
            {
                CppUtils.Alert(string.Format(Properties.Resources.FILE_NOT_EXIST, theFileName));
                return;
            }



            try
            {
                FormMain fm = new FormMain(theFileName);
                fm.ShowDialog();
            }
            catch (Exception e)
            {
                CppUtils.Alert(e);
            }

        }
    }
}