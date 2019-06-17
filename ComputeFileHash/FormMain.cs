using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputeFileHash
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.Text = Application.ProductName;
        }

        void Info(string message)
        {
            Debug.Assert(!InvokeRequired);
            MessageBox.Show(message);
        }

        bool YesNo(string message)
        {
            return Ambiesoft.CppUtils.YesOrNo(message) == DialogResult.Yes;
        }
        void Alert(string message)
        {
            Debug.Assert(!InvokeRequired);
            MessageBox.Show(message);
        }

        delegate void VRDeletgate(ThreadReaderInterface tri);

        int _totalThreadCount = 0;
        void OnStartReaderThread(ThreadReaderInterface tri)
        {
            Debug.Assert(!InvokeRequired);
            ++_totalThreadCount;
        }

        void OnEndingFileReaderThread(ThreadReaderInterface tri)
        {
            Debug.Assert(!InvokeRequired);
            tri.ClosePipes();
        }

        delegate void VCloseThread(Thread th);
        void closeThread(Thread th)
        {
            th.Join();
        }
        void OnEndedFileReaderThread(ThreadReaderInterface tri)
        {
            Debug.Assert(!InvokeRequired);

            this.BeginInvoke(new VCloseThread(closeThread), tri.TheThread);
            --_totalThreadCount;
        }

        delegate void VCDelegate(ThreadComputeInterface tci);

        void OnStartComputeThread(ThreadComputeInterface tci)
        {
            Debug.Assert(!InvokeRequired);
            ++_totalThreadCount;
        }
        void OnEndingComputeThread(ThreadComputeInterface tci)
        {
            ListViewItem item = new ListViewItem();
            item.Text = tci.HashMethod;
            var subItem = new ListViewItem.ListViewSubItem();
            subItem.Text = tci.Result;
            item.SubItems.Add(subItem);
            lvMain.Items.Add(item);
        }
        void OnEndedComputeThread(ThreadComputeInterface tci)
        {
            Debug.Assert(!InvokeRequired);
            this.BeginInvoke(new VCloseThread(closeThread), tci.TheThread);
            --_totalThreadCount;
        }
        delegate bool BSDelegate(string message);
        bool AskRetry(string message)
        {
            if (InvokeRequired)
            {
                // When called from the thread, call in ui thread and wait.
                IAsyncResult ar = this.BeginInvoke(new BSDelegate(AskRetry), message);
                object o = this.EndInvoke(ar);
                return (bool)o;
            }
            return MessageBox.Show(message,
                Application.ProductName,
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Exclamation) != DialogResult.Cancel;
        }

        void StartOfFileReaderThread(object obj)
        {
            ThreadReaderInterface tri = (ThreadReaderInterface)obj;
            tri.Parent.BeginInvoke(new VRDeletgate(OnStartReaderThread), tri);
            try
            {
                // tri.Server.WaitForConnection();
                using (var fs = new FileStream(tri.Filename, FileMode.Open, FileAccess.Read))
                {
                    int buffsize = 4096 * 1024;
                    byte[] buffer = new byte[buffsize];
                    int readsize = 0;

                    do
                    {
                        for (; ; )
                        {
                            try
                            {
                                readsize = fs.Read(buffer, 0, buffsize);
                                break;
                            }
                            catch (Exception ex)
                            {
                                string message = string.Format("Read Error: File='{0}', Pos='{1}', Exception='{2}'",
                                    tri.Filename,
                                    fs.Position,
                                    ex.Message);
                                if (!AskRetry(message))
                                    throw ex;
                            }
                        }
                        tri.WriteToServers(buffer, 0, readsize);
                    } while (readsize > 0);
                }
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
            }

            tri.Parent.EndInvoke(tri.Parent.BeginInvoke(new VRDeletgate(OnEndingFileReaderThread), tri));
            tri.Parent.BeginInvoke(new VRDeletgate(OnEndedFileReaderThread), tri);
        }
        void StartOfComputeThread(object obj)
        {
            ThreadComputeInterface ti = (ThreadComputeInterface)obj;
            ti.Parent.BeginInvoke(new VCDelegate(OnStartComputeThread), ti);
            try
            {
                using (HashAlgorithm ha = ti.CreateHashAlgorithm())
                {
                    byte[] result = ha.ComputeHash(ti.Pipe);
                    ti.Result = BitConverter.ToString(ha.Hash).Replace("-","");
                }
            }
            catch(Exception ex)
            {
                Alert(ex.Message);
            }
            ti.ClosePipe();
            ti.Parent.EndInvoke(ti.Parent.BeginInvoke(new VCDelegate(OnEndingComputeThread), ti));
            ti.Parent.BeginInvoke(new VCDelegate(OnEndedComputeThread), ti);
        }


        private void btnCompute_Click(object sender, EventArgs e)
        {
            string filename = txtFile.Text;
            if(string.IsNullOrEmpty(filename))
            {
                Alert(Properties.Resources.EMPTY_FILENAME);
                return;
            }
            if(!File.Exists(filename))
            { 
                Alert(string.Format(Properties.Resources.FILE_NOT_EXIST, filename));
                return;
            }

            // var pipeName = "testpipe";
            // var server = new NamedPipeServerStream(pipeName, PipeDirection.Out);

            List<AnonymousPipeServerStream> servers = new List<AnonymousPipeServerStream>();

            // Create Compute Thread for MD5
            var serverMD5 = new AnonymousPipeServerStream(PipeDirection.Out,HandleInheritability.Inheritable);
            servers.Add(serverMD5);
            var thComputeMD5 = new Thread(StartOfComputeThread);
            thComputeMD5.Start(new ThreadComputeInterface(
                this, serverMD5.GetClientHandleAsString(), "MD5", thComputeMD5));

            // Create Compute Thread for Sha1
            var serverSha1 = new AnonymousPipeServerStream(PipeDirection.Out,HandleInheritability.Inheritable);
            servers.Add(serverSha1);
            var thComputeSha1 = new Thread(StartOfComputeThread);
            thComputeSha1.Start(new ThreadComputeInterface(
                this, serverSha1.GetClientHandleAsString(), "Sha1", thComputeSha1));


            // Create FileReader and PipeWriter Thread
            var thReader = new Thread(StartOfFileReaderThread);
            thReader.Start(new ThreadReaderInterface(
                this, filename, servers.ToArray(), thReader));
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using(var dlg = new OpenFileDialog())
            {
                if (DialogResult.OK != dlg.ShowDialog())
                    return;

                txtFile.Text = dlg.FileName;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_totalThreadCount > 0)
            {
                if(!YesNo("Quit?"))
                {
                    e.Cancel = true;
                    return;
                }
            }
            GC.WaitForPendingFinalizers();
            // System.Environment.Exit(0);
        }
    }
}
