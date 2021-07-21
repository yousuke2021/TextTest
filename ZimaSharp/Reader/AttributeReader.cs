using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    class AttributeReader : XReader
    {
        Assets.Type type;
        Dictionary<string, NounWrapperReader> attribute_list = new();

        public AttributeReader(Assets.Type type)
        {
            this.type = type;
            head = "=>";
            SetBracket(Assets.Brackets.Type2);
        }

        public override bool Execution(string text, ref int point)
        {
            string attributes_text;
            char c;
            
            if (!Read(text, ref point, out attributes_text))
            {
                return false;
            }
            if (!ReadAttributes(attributes_text, point))
            {
                return false;
            }
            return true;
        }

        public override string GetDisplayStr()
        {
            string display_str = "";

            return display_str;
        }

        private bool ReadAttributes(string attributes_text, int point)
        {
            List<string> attributes = XReader.Split(attributes_text, Assets.Separator.Comma);
            foreach(var attribute in attributes)
            {
                ReadAttribute(attribute);
            }
            return true;
        }

        private bool ReadAttribute(string attribute)
        {
            foreach (var attribute_name in type.Attributes.Select(x => x.Name))
            {
                int point = 0;
                if(!TextReader.ReadAndCheck(attribute, string.Format("{0}=", attribute_name), ref point))
                {
                    continue;
                }
                NounWrapperReader nwr = new();
                if (!nwr.Execution(attribute, ref point) || point != attribute.Length)
                {
                    break;
                }
                Debug.WriteLine(nwr.DisplayStr);
                break;
            }
            return false;
        }
    }
}
