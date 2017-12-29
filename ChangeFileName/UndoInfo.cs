using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChangeFileName
{
    class UndoInfo
    {
        string text_;
        int start_;
        int len_;

        public UndoInfo(string t,int s,int len)
        {
            text_ = t;
            start_ = s;
            len_ = len;
        }
        public string Text
        {
            get { return text_; }
        }
        public int Start
        {
            get { return start_; }
        }
    }
}
