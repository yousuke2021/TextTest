using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    internal abstract class XReader : YReader
    {
        protected string head;
        protected Assets.Bracket bracket;
        protected string text = "";

        public void SetBracket(Assets.Bracket bracket)
        {
            this.bracket = bracket;
        }

        public bool Read(string text, ref int point, out string inner_text)
        {
            return TextReader.BracketReader(head, bracket.Start, bracket.End, text, ref point, out inner_text);
        }

        public bool BracketReader(string head, Assets.Bracket bracket, string text, ref int point, out string inner_text)
        {
            return TextReader.BracketReader(head, bracket.Start, bracket.End, text, ref point, out inner_text);
        }

        public abstract override bool Execution(string text, ref int point);

        public abstract override string GetDisplayStr();

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
