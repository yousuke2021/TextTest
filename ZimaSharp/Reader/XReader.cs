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
        protected string text = "";

        public void SetBracket(Assets.Bracket bracket)
        {
            start = bracket.Start;
            end = bracket.End;
        }

        public bool Read(string text, ref int point, out string inner_text)
        {
            return TextReader.BracketReader(head, start, end, text, ref point, out inner_text);
        }

        public abstract bool Execution(string text, ref int point);

        public abstract string GetDisplayStr();

        public static List<string> Split(string text, char c)
        {
            List<(char, char)> bracket_list = new();
            foreach(var bracket in Assets.Brackets.Self)
            {
                bracket_list.Add((bracket.Start, bracket.End));
            }
            return TextReader.Split(text, c, bracket_list);
        }


    }
}
