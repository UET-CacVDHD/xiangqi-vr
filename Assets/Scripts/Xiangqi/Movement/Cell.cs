using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    // Cell row index range: 1 (the lower left conner of the chessboard) -> 9.
    // Cell col index range: 1 (the lower left conner of the chessboard) -> 10.
    public class Cell
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public override string ToString()
        {
            return $"Cell: ({Row}, {Col})";
        }

        public SideRelativeCell GetSideRelativeCell(string side)
        {
            if (side == Side.Red)
                return new SideRelativeCell(Row, Col);
            return new SideRelativeCell(Constant.BoardRows - Row + 1, Constant.BoardCols - Col + 1);
        }
    }
}