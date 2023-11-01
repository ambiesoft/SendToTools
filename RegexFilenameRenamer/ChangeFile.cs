using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambiesoft.RegexFilenameRenamer
{
    public class ChangeFile
    {
        string before_;
        string after_;
        bool changed_;

        public ChangeFile(string b,string a, bool c)
        {
            before_ = b;
            after_ = a;
            changed_ = c;
        }

        public string Before
        {
            get { return before_; }
        }
        public string After
        {
            get { return after_; }
        }

        public bool Changed { get { return changed_; } }
    }
}
