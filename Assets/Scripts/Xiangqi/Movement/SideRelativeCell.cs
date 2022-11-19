using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    public class SideRelativeCell
    {
        public SideRelativeCell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }

        public override string ToString()
        {
            return $"Relative cell: ({Row}, {Col})";
        }

        public Cell GetCell(string side)
        {
            if (side == Side.Red)
                return new Cell(Row, Col);
            return new Cell(Constant.BoardRows - Row + 1, Constant.BoardCols - Col + 1);
        }
    }
}