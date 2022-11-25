using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

namespace Xiangqi.ChessPieceLogic
{
    public class Cannon : ChessPiece
    {
        public Cannon(AbsoluteCell aCell, bool isDead, string side, string type) : base(aCell, isDead, side, type)
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, Constants.BoardRows),
                new(new List<Direction> { Direction.Right }, Constants.BoardCols),
                new(new List<Direction> { Direction.Down }, Constants.BoardRows),
                new(new List<Direction> { Direction.Left }, Constants.BoardCols)
            };
            boundary = Boundary.Full;
        }
    }
}