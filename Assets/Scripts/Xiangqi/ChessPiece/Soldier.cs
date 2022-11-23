using System.Collections.Generic;
using System.Linq;
using Xiangqi.Enum;
using Xiangqi.Movement.Cell;
using Xiangqi.Util;

namespace Xiangqi.ChessPiece
{
    public class Soldier : ChessPiece
    {
        private void Start()
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, 1),
                new(new List<Direction> { Direction.Left }, 1),
                new(new List<Direction> { Direction.Right }, 1)
            };
            boundary = Boundary.Full;
        }


        private bool IsOverRiver()
        {
            var sideRelativeCell = aCell.GetRelativeCell(side);
            return sideRelativeCell.row > Constant.BoardRiver;
        }

        protected override List<Path> GetAvailablePaths()
        {
            return base.GetAvailablePaths().Where(path => path.directions[0] == Direction.Up || IsOverRiver()).ToList();
        }
    }
}