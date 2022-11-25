using System;
using Xiangqi.Enum;

namespace Xiangqi.Motion.Cell
{
    [Serializable]
    public class BaseCell
    {
        public int col;
        public int row;

        public BaseCell(int row, int col)
        {
            this.col = col;
            this.row = row;
        }

        public BaseCell(BaseCell c)
        {
            col = c.col;
            row = c.row;
        }

        public override string ToString()
        {
            return $"Cell: ({row}, {col})";
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
    }
}