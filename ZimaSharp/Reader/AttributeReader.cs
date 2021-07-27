using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    internal class AttributeReader : XReader
    {
        class ERRORS
        {
            internal static Error.Error ARLEADY_EXISTS_ERROR = new("属性が重複しています");
        }

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
            int count = 0;
            foreach(var attribute in attribute_list)
            {
                if(count < attribute_list.Count - 1)
                {
                    display_str += string.Format("{0}が{1}で ", attribute.Key, attribute.Value.GetDisplayStr());
                }
                else
                {
                    display_str += string.Format("{0}が{1}", attribute.Key, attribute.Value.GetDisplayStr());
                }
                count++;
            }
            return display_str;
        }

        private bool ReadAttributes(string attributes_text, int point)
        {
            List<string> attributes = XReader.Split(attributes_text, Assets.Separator.Comma);
            foreach(var attribute in attributes)
            {
                ReadAttribute(attribute, point);
            }
            return true;
        }

        private bool ReadAttribute(string attribute, int attribute_point)
        {
            foreach (var search_attribute in type.Attributes)
            {
                int point = 0;
                if(!TextReader.ReadAndCheck(attribute, string.Format("{0}=", search_attribute.Name), ref point))
                {
                    continue;
                }
                NounWrapperReader nwr = new();
                if (!nwr.Execution(attribute, ref point) || point != attribute.Length)
                {
                    break;
                }
                if (!attribute_list.ContainsKey(search_attribute.DisplayStr))
                {
                    attribute_list.Add(search_attribute.DisplayStr, nwr);
                }
                else
                {
                    error_list.AddError(ERRORS.ARLEADY_EXISTS_ERROR, attribute_point);
                }
                
                break;
            }
            return false;
        }
    }
}
