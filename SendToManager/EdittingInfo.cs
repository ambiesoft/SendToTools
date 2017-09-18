using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
