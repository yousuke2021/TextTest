using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Assets
{
    internal static class Brackets
    {
        public static readonly Bracket Type1 = new Bracket("()");
        public static readonly Bracket Type2 = new Bracket("{}");
        public static readonly Bracket Type3 = new Bracket("[]");

        public static readonly List<Bracket> Self = new() { Type1, Type2, Type3 };
    }
}
