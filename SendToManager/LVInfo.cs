using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SendToManager
{
    class LVInfo
    {
        private string fullName_;

        public LVInfo(string fullpath)
        {
            fullName_ = fullpath;
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(fullName_);
            }
        }
        public string ParentDir
        {
            get
            {
                return Path.GetDirectoryName(fullName_);
            }
        }
    }
}
