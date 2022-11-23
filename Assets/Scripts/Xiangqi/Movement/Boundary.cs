using Xiangqi.Movement.Cell;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    public class Boundary
    {
        public static readonly Boundary Full = new(1, Constants.BoardRows, 1, Constants.BoardCols);
        public static readonly Boundary River = new(1, Constants.BoardRiverRows, 1, Constants.BoardCols);
        public static readonly Boundary Palace = new(1, 3, 4, 6);

        public Boundary(int rowLowerBound, int rowUpperBound, int colLowerBound, int colUpperBound)
        {
            RowLowerBound = rowLowerBound;
            RowUpperBound = rowUpperBound;
            ColLowerBound = colLowerBound;
            ColUpperBound = colUpperBound;
        }

        public int RowLowerBound { get; }

        public int RowUpperBound { get; }

        public int ColLowerBound { get; }

        public int ColUpperBound { get; }

        public bool IsWithinBoundary(BaseCell cell)
        {
            return cell.row >= RowLowerBound &&
                   cell.row <= RowUpperBound &&
                   cell.col >= ColLowerBound &&
                   cell.col <= ColUpperBound;
        }
    }
}