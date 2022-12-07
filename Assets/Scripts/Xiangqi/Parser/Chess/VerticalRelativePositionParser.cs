using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class VerticalRelativePositionParser
    {
        public static readonly Parser<string> Front = Literals.Text("t");
        public static readonly Parser<string> Behind = Literals.Text("s");
        public static readonly Parser<string> Mid = Literals.Text("g");
        public static readonly Parser<string> FrontMid = Literals.Text("tg");
        public static readonly Parser<string> BehindMid = Literals.Text("sg");

        public static readonly Parser<string> Parser =
            OneOf(Front, Behind, Mid, FrontMid, BehindMid);
    }
}