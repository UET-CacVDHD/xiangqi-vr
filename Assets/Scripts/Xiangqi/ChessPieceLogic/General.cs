using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;

namespace Xiangqi.ChessPieceLogic
{
    public class General : ChessPiece
    {
        public General(AbsoluteCell aCell, bool isDead, string side, string type) : base(aCell, isDead, side, type)
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, 1),
                new(new List<Direction> { Direction.Right }, 1),
                new(new List<Direction> { Direction.Down }, 1),
                new(new List<Direction> { Direction.Left }, 1)
            };
            boundary = Boundary.Palace;
        }
    }
}