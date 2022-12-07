using Parlot.Fluent;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class ChessTypeParser
    {
        public static readonly Parser<string> Rook = Literals.Text("X");
        public static readonly Parser<string> Horse = Literals.Text("M");
        public static readonly Parser<string> Elephant = Literals.Text("T");
        public static readonly Parser<string> Advisor = Literals.Text("S");
        public static readonly Parser<string> General = Literals.Text("G").Or(Literals.Text("Tg"));
        public static readonly Parser<string> Cannon = Literals.Text("P");
        public static readonly Parser<string> Soldier = Literals.Text("B");

        public static readonly Parser<string> Parser =
            OneOf(Rook, Horse, Elephant, Advisor, General, Cannon, Soldier);
    }
}