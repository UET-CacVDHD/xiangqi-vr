using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser
{
    public static class Misc
    {
        public static readonly Parser<char> Separator = Terms.Char(':');
    }
}