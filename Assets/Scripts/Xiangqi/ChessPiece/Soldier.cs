using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement;

namespace Xiangqi.ChessPiece
{
    public class Soldier : ChessPiece
    {
        private void Start()
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, 1)
            };
            boundary = Boundary.Full;
        }
    }
}