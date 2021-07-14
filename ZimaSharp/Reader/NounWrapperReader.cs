using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    class NounWrapperReader : XReader
    {
        Assets.Type type;

        public NounWrapperReader()
        {

        }

        public override bool Execution(string text, ref int point)
        {
            return ReadNoun(text, ref point);
        }

        public bool ReadNoun(string text, ref int point)
        {
            int now_point = point;
            foreach(var type in ZimaSharpConverter.Types)
            {
                NounReader nr = new(type);
                if(nr.Execution(text, ref now_point))
                {
                    this.type = type;
                    point = now_point;
                    return true;
                }
            }
            return false;
        }
    }
}
