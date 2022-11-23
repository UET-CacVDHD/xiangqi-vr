using System;
using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement.Cell
{
    // Cell row index range: 1 (the lower left conner of the chessboard) -> 9.
    // Cell col index range: 1 (the lower left conner of the chessboard) -> 10.
    [Serializable]
    public class AbsoluteCell : BaseCell
    {
        public AbsoluteCell(int row, int col) : base(row, col)
        {
        }

        public AbsoluteCell(AbsoluteCell cell) : base(cell)
        {
        }

        public RelativeCell GetRelativeCell(string side)
        {
            return side == Side.Red
                ? new RelativeCell(row, col)
                : new RelativeCell(Constant.BoardRows - row + 1, Constant.BoardCols - col + 1);
        }
    }
}