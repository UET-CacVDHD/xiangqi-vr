using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement;
using Xiangqi.Util;

namespace Xiangqi.ChessPiece
{
    public class Cannon : ChessPiece
    {
        private void Start()
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, Constant.BoardRows),
                new(new List<Direction> { Direction.Right }, Constant.BoardCols),
                new(new List<Direction> { Direction.Down }, Constant.BoardRows),
                new(new List<Direction> { Direction.Left }, Constant.BoardCols)
            };
            boundary = Boundary.Full;
        }
    }
}