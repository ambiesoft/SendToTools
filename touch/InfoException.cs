using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace touch
{
    internal class InfoException : Exception
    {
        public InfoException()
        {
        }

        public InfoException(string message)
            : base(message)
        {
        }

        public InfoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
