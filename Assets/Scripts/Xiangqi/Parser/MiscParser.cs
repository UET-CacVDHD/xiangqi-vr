using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser
{
    public static class MiscParser
    {
        public static readonly Parser<char> Separator = Literals.Char(':');
        public static readonly Parser<char> TagStart = Literals.Char('#');
    }
}