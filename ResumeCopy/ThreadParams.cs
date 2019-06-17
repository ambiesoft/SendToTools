using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResumeCopy
{
    class ThreadParams
    {
        Control _parent;
        string _source;
        string _dest;

        public ThreadParams(Control parent, string s, string d)
        {
            _parent = parent;
            _source = s;
            _dest = d;
        }
        public Control Parent { get { return _parent; } }
        public string Source { get { return _source; } }
        public string Dest { get { return _dest; } }
        
    }
}
