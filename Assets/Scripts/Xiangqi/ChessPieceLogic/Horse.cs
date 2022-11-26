using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;

namespace Xiangqi.ChessPieceLogic
{
    public class Horse : ChessPiece
    {
        public Horse(AbsoluteCell aCell, bool isDead, string side, string type, ChessPiece[,] chessboard) : base(aCell,
            isDead, side, type, chessboard)
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up, Direction.UpLeft }, 1),
                new(new List<Direction> { Direction.Up, Direction.UpRight }, 1),
                new(new List<Direction> { Direction.Right, Direction.UpRight }, 1),
                new(new List<Direction> { Direction.Right, Direction.DownRight }, 1),
                new(new List<Direction> { Direction.Down, Direction.DownLeft }, 1),
                new(new List<Direction> { Direction.Down, Direction.DownRight }, 1),
                new(new List<Direction> { Direction.Left, Direction.DownLeft }, 1),
                new(new List<Direction> { Direction.Left, Direction.UpLeft }, 1)
            };
            boundary = Boundary.Full;
        }

        public override ChessPiece Clone(ChessPiece[,] newChessboard)
        {
            return new Horse(new AbsoluteCell(aCell), isDead, side, type, newChessboard);
        }
    }
}