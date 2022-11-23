using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement.Cell
{
    // Cell is calculated base on chess piece side
    public class RelativeCell : BaseCell
    {
        public RelativeCell(int row, int col) : base(row, col)
        {
        }

        public RelativeCell(BaseCell c) : base(c)
        {
        }

        public AbsoluteCell GetAbsoluteCell(string side)
        {
            return side == Side.Red
                ? new AbsoluteCell(row, col)
                : new AbsoluteCell(Constants.BoardRows - row + 1, Constants.BoardCols - col + 1);
        }
    }
}