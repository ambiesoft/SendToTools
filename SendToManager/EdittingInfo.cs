using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendToManager
{
    class EdittingInfo
    {
        string initial_;
        string result_;

        public EdittingInfo() { }

        public void Clear()
        {
            initial_ = result_ = null;
            item_ = null;
            subitem_ = -1;
        }
        public string Initial
        { 
            get { return initial_; }
            set { initial_ = value; }
        }
        public bool HasResult
        {
            get { return result_ != null; }
        }
        public string Result
        {
            get { return result_; }
            set { result_ = value; }
        }
        public ListViewItem item_;
        public int subitem_;
    }
}
