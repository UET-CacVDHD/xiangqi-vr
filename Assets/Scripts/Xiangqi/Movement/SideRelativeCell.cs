using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    // Cell is calculated base on chess piece side
    public class SideRelativeCell
    {
        public SideRelativeCell(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int row;
        public int col;

        public override string ToString()
        {
            return $"Relative cell: ({row}, {col})";
        }

        public void MoveAlongDirection(Direction dir, int step)
        {
            row += dir.DeltaRow * step;
            col += dir.DeltaCol * step;
        }

        public void MoveAlongPath(Path path, int step)
        {
            foreach (var dir in path.directions) MoveAlongDirection(dir, step);
        }

        public Cell GetCell(string side)
        {
            return side == Side.Red
                ? new Cell(row, col)
                : new Cell(Constant.BoardRows - row + 1, Constant.BoardCols - col + 1);
        }
    }
}