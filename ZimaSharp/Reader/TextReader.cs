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
            
            if(text == null || point > text.Length - 1)
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

        public static bool ReadAndCheck(string text1, string text2, ref int point)
        {
            if(point >= text1.Length || !text1.Substring(point).StartsWith(text2))
            {
                return false;
            }
            point += text2.Length;
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

        public static List<string> Split(string text, char c)
        {
            return text.Split(c).ToList();
        }

        public static List<string> Split(string text, char c, List<(char start, char end)> brackets)
        {
            List<string> text_list = new();
            Dictionary<(char, char), int> counter = new();

            foreach (var bracket in brackets)
            {
                counter.Add(bracket, 0);
            }

            int point = 0;
            char read_c;

            string tmp_string = "";
            bool bracket_flag = false;

            while (ReadChar(text, point, out read_c))
            {
                if (c == read_c && !bracket_flag)
                {
                    text_list.Add(tmp_string);
                    tmp_string = "";
                    point++;
                    continue;
                }

                tmp_string += read_c;
                bracket_flag = false;
                foreach (var bracket in brackets)
                {
                    if (bracket.start == read_c)
                    {
                        counter[bracket]++;
                    }
                    else if (bracket.end == read_c && counter[bracket] > 0)
                    {
                        counter[bracket]--;
                    }
                    if (counter[bracket] != 0)
                    {
                        bracket_flag = true;
                    }
                }
                point++;
            }
            if (!string.IsNullOrEmpty(tmp_string))
            {
                text_list.Add(tmp_string);
            }
            return text_list;
        }

        public static List<string> Split(string text, char c, params (char start, char end)[] brackets)
        {
            return Split(text, c, brackets.ToList());
        }
    }
}
