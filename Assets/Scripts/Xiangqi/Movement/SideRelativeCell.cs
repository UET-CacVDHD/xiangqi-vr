using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    // Cell is calculated base on chess piece side
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

        public void MoveAlongDirection(Direction dir, int step)
        {
            Row += dir.DeltaRow * step;
            Col += dir.DeltaCol * step;
        }

        public void MoveAlongPath(Path path, int step)
        {
            foreach (var dir in path.directions) MoveAlongDirection(dir, step);
        }

        public Cell GetCell(string side)
        {
            return side == Side.Red
                ? new Cell(Row, Col)
                : new Cell(Constant.BoardRows - Row + 1, Constant.BoardCols - Col + 1);
        }
    }
}