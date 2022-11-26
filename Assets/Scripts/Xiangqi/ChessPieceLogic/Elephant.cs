using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;

namespace Xiangqi.ChessPieceLogic
{
    public class Elephant : ChessPiece
    {
        public Elephant(AbsoluteCell aCell, bool isDead, string side, string type) : base(aCell, isDead, side, type)
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

        public override ChessPiece Clone()
        {
            return new Elephant(new AbsoluteCell(aCell), isDead, side, type);
        }
    }
}