using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class ChessType
    {
        public static readonly Parser<string> Rook = Terms.Text("X");
        public static readonly Parser<string> Horse = Terms.Text("M");
        public static readonly Parser<string> Elephant = Terms.Text("T");
        public static readonly Parser<string> Advisor = Terms.Text("S");
        public static readonly Parser<string> General = Terms.Text("G").Or(Terms.Text("Tg"));
        public static readonly Parser<string> Cannon = Terms.Text("P");
        public static readonly Parser<string> Soldier = Terms.Text("B");

        public static readonly Parser<string> Parser =
            OneOf(Rook, Horse, Elephant, Advisor, General, Cannon, Soldier)
                .ElseError("Cannot parse chess piece");
    }
}