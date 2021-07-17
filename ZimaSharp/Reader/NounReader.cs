using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{


    internal class NounReader : XReader
    {
        Assets.Type type;
        List<NounWrapperReader> argument_list = new();

        string display_noun = "";

        public NounReader(Assets.Type type)
        {
            this.type = type;
            head = type.Name;
            SetBracket(Assets.Brackets.Type1);
        }

        public override bool Execution(string text, ref int point)
        {
            string arguments_text;
            if(!Read(text, ref point, out arguments_text))
            {
                return false;
            }
            ReadAreguments(arguments_text, point);
            return true;
        }

        public override string GetDisplayStr()
        {
            string display_str = "";

            foreach(var argument in argument_list)
            {
                display_str += string.Format("{0}の", argument.DisplayStr);
            }
            display_str += display_noun;
            return display_str;
        }

        private bool ReadAreguments(string arguments_text, int point)
        {
            argument_list.Clear();
            if (arguments_text == "")
            {
                display_noun = type.Base;
                return true;
            }

            List<string> arguments = XReader.Split(arguments_text, Assets.Separator.Comma);
            
            string first_argument = arguments.First();
            if (arguments.Count > 1 && first_argument == "base")
            {
                display_noun = type.Base;
            }
            else
            {
                foreach (var noun in type.Nouns)
                {
                    if (noun.Key == first_argument)
                    {
                        display_noun = noun.DisplayStr;
                        break;
                    }
                }
            }
            
            foreach(var argument in arguments.Skip(1))
            {
                NounWrapperReader nwr = new();
                int argument_point = 0;
                
                if (!nwr.Execution(argument, ref argument_point) || argument_point != argument.Length)
                {
                    break;
                }
                argument_list.Add(nwr);
            }

            if (display_noun == "")
            {
                return false;
            }

            return true;
        }
    }
}
