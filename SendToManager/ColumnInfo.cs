using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendToManager
{
    class ColumnInfo
    {
        Control c_;
        public ColumnInfo(Control c)
        {
            c_ = c;
        }
        public Control EdittingControl
        {
            get { return c_; }
        }
    }
}
