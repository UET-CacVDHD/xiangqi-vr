namespace Xiangqi.Movement
{
    public class Boundary
    {
        public static Boundary Full = new(1, 9, 1, 9);
        public static Boundary River = new(1, 9, 1, 5);
        public static Boundary Palace = new(4, 6, 1, 3);

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

        public bool IsWithinBoundary(int row, int col)
        {
            return row >= RowLowerBound && row <= RowUpperBound && col >= ColLowerBound && col <= ColUpperBound;
        }
    }
}