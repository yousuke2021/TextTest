using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    internal abstract class XReader
    {
        protected Error.ErrorList error_list = new();
        protected string head;
        protected char start;
        protected char end;
        protected string text;

        public bool Read(string text, ref int point, out string inner_text)
        {
            return TextReader.BracketReader(head, start, end, text, ref point, out inner_text);
        }

        public abstract bool Execution(string text, ref int point);
    }
}
