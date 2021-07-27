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
        static class ERRORS
        {
            internal static Error.Error ARGUMENTS_ERROR = new("引数が不正です");
            internal static Error.Error ATTRIBUTES_ERROR = new("属性が不正です");
        }

        Assets.Type search_type;
        Assets.Type reality_type;
        List<NounWrapperReader> argument_list = new();
        AttributeReader ar = null;

        string display_noun = "";

        public Assets.Type Type { get { return reality_type; } }

        public NounReader(Assets.Type type)
        {
            search_type = type;
            reality_type = search_type;
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
            if(!ReadArguments(arguments_text, point))
            {
                return false;
            }
            if (reality_type.Attributes.Count == 0)
            {
                return true;
            }

            AttributeReader ar = new(search_type);
            int ar_point = point;
            if(!ar.Execution(text, ref point))
            {
                if (ar.Errors.Exist)
                {
                    error_list.AddError(ERRORS.ATTRIBUTES_ERROR, ar_point);
                    error_list.MergeErrors(ar.Errors, ar_point);
                }
                return true;
            }
            
            this.ar = ar;

            return true;
        }

        public override string GetDisplayStr()
        {
            string display_str = "";

            foreach(var argument in argument_list)
            {
                display_str += string.Format("{0}の", argument.GetDisplayStr());
            }
            if (ar != null)
            {
                display_str += string.Format("（{0}）のような", ar.GetDisplayStr());
            }
            display_str += display_noun;
            return display_str;
        }

        private bool ReadArguments(string arguments_text, int point)
        {
            argument_list.Clear();
            if (arguments_text == "")
            {
                display_noun = search_type.Base;
                return true;
            }

            List<string> arguments = XReader.Split(arguments_text, Assets.Separator.Comma);
            
            string first_argument = arguments.First();
            if (arguments.Count > 1 && first_argument == "base")
            { 
                display_noun = search_type.Base;
            }
            else
            {
                foreach (var noun in search_type.Nouns)
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
