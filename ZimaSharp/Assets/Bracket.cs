using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Assets
{
    internal class Bracket
    {
        char start;
        char end;

        public char Start { get{ return start; } }
        public char End { get { return end; } }

        public Bracket(char start, char end)
        {
            this.start = start;
            this.end = end;
        }

        public Bracket(string bracket)
        {
            this.start = bracket[0];
            this.end = bracket[1];
        }
    }
}
