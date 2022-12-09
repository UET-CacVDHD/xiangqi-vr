using Parlot.Fluent;
using Xiangqi.Enum;
using static Parlot.Fluent.Parsers;

namespace Xiangqi.Parser.Chess
{
    public static class ChessTypeParser
    {
        public static readonly Parser<string> Rook = Literals.Text(ChessType.Rook);
        public static readonly Parser<string> Horse = Literals.Text(ChessType.Horse);
        public static readonly Parser<string> Elephant = Literals.Text(ChessType.Elephant);
        public static readonly Parser<string> Advisor = Literals.Text(ChessType.Advisor);

        public static readonly Parser<string> General =
            Literals.Text(ChessType.General).Or(Literals.Text(ChessType.GeneralAlt));

        public static readonly Parser<string> Cannon = Literals.Text(ChessType.Cannon);
        public static readonly Parser<string> Soldier = Literals.Text(ChessType.Soldier);

        public static readonly Parser<string> Parser =
            OneOf(Rook, Horse, Elephant, Advisor, General, Cannon, Soldier);
    }
}