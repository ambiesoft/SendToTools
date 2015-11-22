using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SendToManager
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
            System.IO.FileInfo[] fis = di.GetFiles();
            Array.Sort(fis,
                delegate(FileInfo f1, FileInfo f2)
                {
                    return f1.Name.CompareTo(f2.Name);
                }
            );

            foreach (FileInfo fi in fis)
            {
                if (!fi.IsReadOnly &&
                    (fi.Attributes & FileAttributes.Hidden)==0 &&
                    string.Compare(fi.Extension, ".lnk", true)==0 )
                {
                    lvMain.Items.Add(Path.GetFileNameWithoutExtension(fi.Name));
                }
            }
        }
    }
}