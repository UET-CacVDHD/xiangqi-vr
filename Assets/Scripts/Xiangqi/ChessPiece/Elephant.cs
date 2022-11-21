using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement;

namespace Xiangqi.ChessPiece
{
    public class Elephant : ChessPiece
    {
        private void Start()
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.UpRight, Direction.UpRight }, 1),
                new(new List<Direction> { Direction.DownRight, Direction.DownRight }, 1),
                new(new List<Direction> { Direction.DownLeft, Direction.DownLeft }, 1),
                new(new List<Direction> { Direction.UpLeft, Direction.UpLeft }, 1)
            };
            boundary = Boundary.River;
        }
    }
}