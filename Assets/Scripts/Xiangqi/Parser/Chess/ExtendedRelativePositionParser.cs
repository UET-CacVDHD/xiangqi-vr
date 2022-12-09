using Parlot.Fluent;
using Xiangqi.Enum.Command;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class ExtendedRelativePositionParser
    {
        public static readonly Parser<string> Front = Literals.Text(RelativePosition.Front);
        public static readonly Parser<string> Back = Literals.Text(RelativePosition.Back);
        public static readonly Parser<string> Mid = Literals.Text(RelativePosition.Mid);
        public static readonly Parser<string> FrontMid = Literals.Text(RelativePosition.FrontMid);
        public static readonly Parser<string> BackMid = Literals.Text(RelativePosition.BackMid);
        public static readonly Parser<string> Left = Literals.Text(RelativePosition.Left);
        public static readonly Parser<string> Right = Literals.Text(RelativePosition.Right);
        public static readonly Parser<string> LeftMid = Literals.Text(RelativePosition.LeftMid);
        public static readonly Parser<string> RightMid = Literals.Text(RelativePosition.RightMid);

        public static readonly Parser<string> Parser =
            OneOf(FrontMid, BackMid, LeftMid, RightMid, Front, Back, Mid, Left, Right);
    }
}