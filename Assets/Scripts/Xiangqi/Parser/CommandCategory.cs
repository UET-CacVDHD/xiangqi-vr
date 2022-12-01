using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser
{
    public static class CommandCategory
    {
        public static readonly Parser<string> Standard = Terms.Text("std");
        public static readonly Parser<string> Extended = Terms.Text("ext");
        public static readonly Parser<string> Meta = Terms.Text("meta");

        public static readonly Parser<string> Parser =
            OneOf(Standard, Extended, Meta).ElseError("Cannot parse command category");
    }
}