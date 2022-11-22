using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Movement;
using Xiangqi.Util;

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

        private void LateUpdate()
        {
            var sideRelativeCell = cell.GetSideRelativeCell(side);
            if (sideRelativeCell.row > Constant.BoardRiver)
            {
                paths.Add(new Path(new List<Direction> { Direction.Left }, 1));
                paths.Add(new Path(new List<Direction> { Direction.Right }, 1));
            }
        }
    }
}