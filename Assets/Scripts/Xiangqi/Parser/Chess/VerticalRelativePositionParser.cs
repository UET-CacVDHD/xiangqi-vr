using Parlot.Fluent;
using Xiangqi.Enum.Command;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class VerticalRelativePositionParser
    {
        public static readonly Parser<string> Front = Literals.Text(RelativePosition.Front);
        public static readonly Parser<string> Back = Literals.Text(RelativePosition.Back);
        public static readonly Parser<string> Mid = Literals.Text(RelativePosition.Mid);
        public static readonly Parser<string> FrontMid = Literals.Text(RelativePosition.FrontMid);
        public static readonly Parser<string> BackMid = Literals.Text(RelativePosition.BackMid);

        public static readonly Parser<string> Parser =
            OneOf(Front, Back, Mid, FrontMid, BackMid);
    }
}