using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement;

namespace Xiangqi.ChessPiece
{
    public class General : ChessPiece
    {
        protected override void Start()
        {
            base.Start();
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