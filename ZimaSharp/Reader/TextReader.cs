using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    internal class TextReader
    {
        public static int SToUI(string str)
        {
            int result = 0;
            bool flag = int.TryParse(str, out result);
            if (flag)
            {
                return result;
            }
            return -1;
        }

        public static bool ReadChar(string text, int point, out char c)
        {
            if(point > text.Length - 1)
            {
                c = 'a';
                return false;
            }
            else
            {
                c = text[point];
                return true;
            }
        }

        public static bool CheckCharSame(string text1, int point1, string text2, int point2)
        {
            char c1;
            char c2;
            return (ReadChar(text1, point1, out c1) && ReadChar(text2, point2, out c2) && c1 == c2);
        }

        public static bool CheckContains(string text1, string text2, ref int point)
        {
            int length = text2.Length;
            for (int i = 0; i < length; i++)
            {
                if(!CheckCharSame(text1, point + i, text2, i))
                {
                    return false;
                }
            }
            point += length;
            return true;
        }

        public static bool BracketReader(string head, char start, char end, string text, ref int point, out string inner_text)
        {
            int bracket_count = 0;
            string head_text = head + start;
            inner_text = "";

            if(!CheckContains(text, head_text, ref point))
            {
                return false;
            }

            char c;
            int i = 0;
            while(ReadChar(text, point + i, out c))
            {
                if(start == c)
                {
                    bracket_count++;
                }
                else if(end == c)
                {
                    if(bracket_count == 0)
                    {
                        i++;
                        point += i;
                        return true;
                    }
                    bracket_count--;
                }
                inner_text += c;
                i++;
            }
            return false;
        }
    }
}
