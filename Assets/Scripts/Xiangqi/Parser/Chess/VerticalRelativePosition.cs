using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class VerticalRelativePosition
    {
        public static readonly Parser<string> Front = Terms.Text("t");
        public static readonly Parser<string> Behind = Terms.Text("s");
        public static readonly Parser<string> Mid = Terms.Text("g");
        public static readonly Parser<string> FrontMid = Terms.Text("tg");
        public static readonly Parser<string> BehindMid = Terms.Text("sg");

        public static readonly Parser<string> Parser =
            OneOf(Front, Behind, Mid, FrontMid, BehindMid).ElseError("Cannot parse vertical relative position");
    }
}