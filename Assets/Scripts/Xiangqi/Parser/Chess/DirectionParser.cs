using Parlot.Fluent;
using Xiangqi.Enum.Command;
using static Parlot.Fluent.Parsers;


namespace Xiangqi.Parser.Chess
{
    public static class DirectionParser
    {
        public static readonly Parser<string> Forward = Literals.Text(DirectionCode.Forward);
        public static readonly Parser<string> Backward = Literals.Text(DirectionCode.Backward);
        public static readonly Parser<string> Sideways = Literals.Text(DirectionCode.Sideways);

        public static readonly Parser<string> Parser = OneOf(Forward, Backward, Sideways);
    }
}