using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement.Cell;

namespace Xiangqi.ChessPiece
{
    public class Advisor : ChessPiece
    {
        private void Start()
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
    }
}