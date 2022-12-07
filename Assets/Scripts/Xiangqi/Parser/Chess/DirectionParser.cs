using Parlot.Fluent;
using static Parlot.Fluent.Parsers;


namespace Xiangqi.Parser.Chess
{
    public static class DirectionParser
    {
        public static readonly Parser<string> Forward = Literals.Text(".");
        public static readonly Parser<string> Backward = Literals.Text("/");
        public static readonly Parser<string> Sideways = Literals.Text("-");

        public static readonly Parser<string> Parser = OneOf(Forward, Backward, Sideways);
    }
}