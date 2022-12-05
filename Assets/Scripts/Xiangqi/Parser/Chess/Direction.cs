using Parlot.Fluent;
using static Parlot.Fluent.Parsers;


namespace Xiangqi.Parser.Chess
{
    public static class Direction
    {
        public static readonly Parser<string> Forward = Terms.Text(".");
        public static readonly Parser<string> Backward = Terms.Text("/");
        public static readonly Parser<string> Sideways = Terms.Text("-");

        public static readonly Parser<string> Parser = OneOf(Forward, Backward, Sideways);
    }
}