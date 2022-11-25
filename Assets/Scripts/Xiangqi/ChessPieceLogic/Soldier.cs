using System.Collections.Generic;
using System.Linq;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

namespace Xiangqi.ChessPieceLogic
{
    public class Soldier : ChessPiece
    {
        public Soldier(AbsoluteCell aCell, bool isDead, string side, string type) : base(aCell, isDead, side, type)
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
            return sideRelativeCell.row > Constants.BoardRiverRows;
        }

        protected override List<Path> GetAvailablePaths()
        {
            return base.GetAvailablePaths().Where(path => path.directions[0] == Direction.Up || IsOverRiver()).ToList();
        }
    }
}