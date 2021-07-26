using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Error
{
    internal class Error
    {
        int index = 0;
        string message;

        public int Index { get { return index; } }
        public string Message { get{ return message; } }

        public Error(string message)
        {
            this.message = message;
        }

        public Error(int index, string message)
        {
            this.index = index;
            this.message = message;
        }

        public Error SetIndex(int index)
        {
            this.index = index;
            return this;
        }

    }
}
