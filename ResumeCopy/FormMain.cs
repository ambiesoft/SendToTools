using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResumeCopy
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                if (DialogResult.OK != dlg.ShowDialog())
                    return;

                txtSource.Text = dlg.FileName;
            }
        }

        private void btnBrowseDest_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                if (DialogResult.OK != dlg.ShowDialog())
                    return;

                txtDest.Text = dlg.FileName;
            }
        }

        private long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && string.Compare(drive.Name, driveName, true) == 0)
                {
                    return drive.AvailableFreeSpace;
                }
            }
            return -1;
        }
          
        private void timerFreeSpace_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDest.Text))
                return;
            if (txtDest.Text.Length < 3)
                return;

            string drive = txtDest.Text.Substring(0, 3);
            sslMain.Text = string.Format(Properties.Resources.FREE_SPACE,
                GetTotalFreeSpace(drive), drive);
        }

        void Info(string message)
        {
            if (InvokeRequired)
            {
                // When called from the thread, call in ui thread and wait.    
                this.EndInvoke(this.BeginInvoke(new Action(() => this.Info(message))));
                return;
            }
 
            MessageBox.Show(message);
        }
        void Alert(string message)
        {
            if (InvokeRequired)
            {
                // When called from the thread, call in ui thread and wait.    
                this.EndInvoke(this.BeginInvoke(new Action(() => this.Alert(message))));
                return;
            }
 
            MessageBox.Show(message);
        }
        
        bool AskRetry(string message)
        {
            if(InvokeRequired)
            {
                // When called from the thread, call in ui thread and wait.
                return (bool)this.EndInvoke(this.BeginInvoke(new Action<bool>(() => return this.AskRetry(message))));
            }
            return MessageBox.Show(message,
                Application.ProductName,
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Exclamation) != DialogResult.Cancel;
        }

        void onEndThread()
        {
            _thCopy.Join();
            _thCopy = null;
        }
        Thread _thCopy;
        int _buffsize = 4096;
        void startOfThread(object obj)
        {
            ThreadParams tp = (ThreadParams)obj;
            try
            {
                startOfThread2(tp);
                Info(Properties.Resources.COPY_SUCCEEDED);
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
                return;
            }
            tp.Parent.BeginInvoke(new Action(onEndThread));
        }
        void startOfThread2(ThreadParams tp) 
        {
            using (FileStream fsSource = new FileStream(tp.Source, FileMode.Open, FileAccess.Read))
            using (FileStream fsDest = new FileStream(tp.Dest, FileMode.CreateNew, FileAccess.Write))
            {
                byte[] buff = new byte[_buffsize];

                int readsize = 0;
                while ((readsize = fsSource.Read(buff, 0, _buffsize)) > 0)
                {
                    try
                    {
                        fsDest.Write(buff, 0, readsize);
                    }
                    catch (Exception ex)
                    {
                        // If the writing failed, ask and retry
                        if (!AskRetry(ex.Message))
                            return;
                    }
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if(_thCopy != null)
            {
                Alert(Properties.Resources.COPY_ALREADY_RUNNING);
                return;
            }

            _thCopy = new Thread(new ParameterizedThreadStart(startOfThread));
            _thCopy.Start(new ThreadParams(this, txtSource.Text, txtDest.Text));
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

      
    }
}
