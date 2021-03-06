using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    class NounWrapperReader : YReader
    {
        Assets.Type type;
        string display_str = "";

        public NounWrapperReader()
        {

        }

        public override bool Execution(string text, ref int point)
        {
            return ReadNoun(text, ref point);
        }

        public override string GetDisplayStr()
        {
            return display_str;
        }

        public bool ReadNoun(string text, ref int point)
        {
            int now_point = point;
            foreach(var type in ZimaSharpConverter.Types)
            {
                NounReader nr = new(type);
                if(ReadAndMerge(nr, text, ref now_point))
                {
                    this.type = type;
                    point = now_point;
                    display_str = nr.GetDisplayStr();
                    return true;
                }
            }
            return false;
        }

    }
}
