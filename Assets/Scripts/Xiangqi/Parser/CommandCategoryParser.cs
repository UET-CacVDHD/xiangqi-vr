using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser
{
    public static class CommandCategoryParser
    {
        public static readonly Parser<string> Standard = Literals.Text("std");
        public static readonly Parser<string> Extended = Literals.Text("ext");
        public static readonly Parser<string> Meta = Literals.Text("meta");

        public static readonly Parser<string> Parser =
            OneOf(Standard, Extended, Meta);
    }
}