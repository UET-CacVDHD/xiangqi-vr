using Parlot.Fluent;
using Xiangqi.Enum.Command;
using static Parlot.Fluent.Parsers;


namespace Xiangqi.Parser.Chess
{
    public static class ExtendedDirectionParser
    {
        public static readonly Parser<string> Forward = Literals.Text(DirectionCode.Forward);
        public static readonly Parser<string> Backward = Literals.Text(DirectionCode.Backward);
        public static readonly Parser<string> Sideways = Literals.Text(DirectionCode.Sideways);
        public static readonly Parser<string> Left = Literals.Text(DirectionCode.Left);
        public static readonly Parser<string> Right = Literals.Text(DirectionCode.Right);
        public static readonly Parser<string> ForwardLeft = Literals.Text(DirectionCode.ForwardLeft);
        public static readonly Parser<string> ForwardRight = Literals.Text(DirectionCode.ForwardRight);
        public static readonly Parser<string> BackwardLeft = Literals.Text(DirectionCode.BackwardLeft);
        public static readonly Parser<string> BackwardRight = Literals.Text(DirectionCode.BackwardRight);


        public static readonly Parser<string> Parser =
            OneOf(Forward, Backward, Sideways, Left, Right, ForwardLeft,
                ForwardRight, BackwardLeft, BackwardRight);
    }
}