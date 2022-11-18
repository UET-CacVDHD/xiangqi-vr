namespace Xiangqi.Util
{
    public class Boundary
    {
        private readonly int _rowLowerBound;
        private readonly int _rowUpperBound;
        private readonly int _colLowerBound;
        private readonly int _colUpperBound;

        public Boundary(int rowLowerBound, int rowUpperBound, int colLowerBound, int colUpperBound)
        {
            _rowLowerBound = rowLowerBound;
            _rowUpperBound = rowUpperBound;
            _colLowerBound = colLowerBound;
            _colUpperBound = colUpperBound;
        }

        public int RowLowerBound => _rowLowerBound;

        public int RowUpperBound => _rowUpperBound;

        public int ColLowerBound => _colLowerBound;

        public int ColUpperBound => _colUpperBound;

        public static Boundary Full = new Boundary(1, 9, 1, 9);
        public static Boundary River = new Boundary(1, 9, 1, 5);
        public static Boundary Palace = new Boundary(4, 6, 1, 3);
    }
}