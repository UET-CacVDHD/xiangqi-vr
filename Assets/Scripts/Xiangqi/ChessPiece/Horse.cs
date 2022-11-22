using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement.Cell;

namespace Xiangqi.ChessPiece
{
    public class Horse : ChessPiece
    {
        private void Start()
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
    }
}