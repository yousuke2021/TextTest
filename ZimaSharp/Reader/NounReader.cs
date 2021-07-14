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
        List<string> argument_list = new();
        

        public NounReader(Assets.Type type)
        {
            this.type = type;
            head = type.Name;
            start = '(';
            end = ')';
        }

        public override bool Execution(string text, ref int point)
        {
            string arguments_text;
            if(!Read(text, ref point, out arguments_text))
            {
                return false;
            }
            ReadAreguments(arguments_text, ref point);
            //Debug.WriteLine(arguments_text);
            return true;
        }

        private bool ReadAreguments(string arguments_text, ref int point)
        {
            argument_list.Clear();
            if (arguments_text == "")
            {
                argument_list.Add(type.Base);
                return true;
            }

            List<string> arguments = arguments_text.Split(',').ToList();
            string first_argument = arguments.First();
            foreach(var noun in type.Nouns)
            {
                Debug.WriteLine(noun.Key);
                if(noun.Key == first_argument)
                {
                    argument_list.Add(noun.DisplayStr);
                    Debug.WriteLine(noun.DisplayStr);
                    break;
                }
            }
            if (argument_list.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
