using System.Collections.Generic;
using System.Linq;
using Xiangqi.Enum;
using Xiangqi.Game;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

namespace Xiangqi.ChessPieceLogic
{
    public class Soldier : ChessPiece
    {
        public Soldier(AbsoluteCell aCell, bool isDead, string side, string type, GameSnapshot gss) : base(
            aCell, isDead, side, type, gss)
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
            return sideRelativeCell.row > Constant.BoardRiverRows;
        }

        protected override List<Path> GetAvailablePaths()
        {
            return base.GetAvailablePaths().Where(path => path.directions[0] == Direction.Up || IsOverRiver()).ToList();
        }

        public override ChessPiece Clone(GameSnapshot newGss)
        {
            return new Soldier(new AbsoluteCell(aCell), isDead, side, type, newGss);
        }
    }
}