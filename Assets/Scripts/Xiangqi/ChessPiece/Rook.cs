using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement;
using Xiangqi.Util;

namespace Xiangqi.ChessPiece
{
    public class Rook : ChessPiece
    {
        protected override void Start()
        {
            base.Start();
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