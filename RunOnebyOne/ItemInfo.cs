using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunOnebyOne
{
    class ItemInfo
    {
        enum ItemState
        {
            NORMAL,
            RUNNING,
            FINISHED,
        }

        ItemState state_;

        public ItemInfo() { }

        public bool IsFinished
        {
            get { return state_ == ItemState.FINISHED; }
            set
            {
                if (value)
                    state_ = ItemState.FINISHED;
                else
                    state_ = ItemState.NORMAL;
            }
        }
        public bool IsRunning
        {
            get { return state_ == ItemState.RUNNING; }
            set
            {
                if (value)
                    state_ = ItemState.RUNNING;
                else
                    state_ = ItemState.NORMAL;
            }
        }
    }
}
