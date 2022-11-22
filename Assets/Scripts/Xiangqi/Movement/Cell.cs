using System;
using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    // Cell row index range: 1 (the lower left conner of the chessboard) -> 9.
    // Cell col index range: 1 (the lower left conner of the chessboard) -> 10.
    [Serializable]
    public class Cell
    {
        public int col;
        public int row;

        public Cell(int row, int col)
        {
            this.col = col;
            this.row = row;
        }

        public Cell(Cell c)
        {
            col = c.col;
            row = c.row;
        }

        public override string ToString()
        {
            return $"Cell: ({row}, {col})";
        }

        public SideRelativeCell GetSideRelativeCell(string side)
        {
            return side == Side.Red
                ? new SideRelativeCell(row, col)
                : new SideRelativeCell(Constant.BoardRows - row + 1, Constant.BoardCols - col + 1);
        }
    }
}