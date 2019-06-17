using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ComputeFileHash
{
    class ThreadComputeInterface
    {
        AnonymousPipeClientStream _client;
        string _method;
        Thread _thread;
        Control _parent;
        string _result;
        public ThreadComputeInterface(Control parent, string pipeString, string method, Thread thread)
        {
            _parent = parent;
            _client = new AnonymousPipeClientStream(pipeString);
            _method = method;
            _thread = thread;
        }
        public Control Parent { get { return _parent; } }
        public AnonymousPipeClientStream Pipe { get { return _client; } }
        public Thread TheThread { get { return _thread; } }
        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }
        public string HashMethod { get { return _method; } }
        public HashAlgorithm CreateHashAlgorithm()
        {
            if (string.Compare(_method, "MD5", true) == 0)
                return MD5.Create();
            else if (string.Compare(_method, "Sha1", true) == 0)
                return SHA1.Create();

            return null;
        }

        public void ClosePipe()
        {
            _client.Close();
            _client.Dispose();
            _client = null;
        }
    }

    class ThreadReaderInterface
    {
        Control _parent;
        AnonymousPipeServerStream[] _servers;
        string _filename;
        Thread _thread;
        public ThreadReaderInterface(Control parent,
            string filename,
            AnonymousPipeServerStream[] server, 
            Thread thread)
        {
            _parent = parent;
            _filename = filename;
            _servers = server;
            _thread = thread;
        }
        public Control Parent { get { return _parent; } }
        public string Filename { get { return _filename; } }
        public Thread TheThread { get { return _thread; } }
        public void WriteToServers(byte[] buffer, int offset, int count)
        {
            foreach (var server in _servers)
                server.Write(buffer, offset, count);
        }
        

        public void ClosePipes()
        {
            foreach (var server in _servers)
            {
                server.Close();
                server.Dispose();
            }
            _servers = null;
        }

       
    }

}
