using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;

namespace Xiangqi.ChessPieceLogic
{
    public class Advisor : ChessPiece
    {
        public Advisor(AbsoluteCell aCell, bool isDead, string side, string type, ChessPiece[,] chessboard) : base(
            aCell, isDead, side, type, chessboard)
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.UpRight }, 1),
                new(new List<Direction> { Direction.DownRight }, 1),
                new(new List<Direction> { Direction.DownLeft }, 1),
                new(new List<Direction> { Direction.UpLeft }, 1)
            };
            boundary = Boundary.Palace;
        }

        public override ChessPiece Clone(ChessPiece[,] newChessboard)
        {
            return new Advisor(new AbsoluteCell(aCell), isDead, side, type, newChessboard);
        }
    }
}