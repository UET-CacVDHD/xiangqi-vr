namespace Xiangqi.Util
{
    public class Direction
    {
        private readonly int _deltaRow;
        private readonly int _deltaCol;

        public Direction(int deltaRow, int deltaCol)
        {
            _deltaRow = deltaRow;
            _deltaCol = deltaCol;
        }

        public int DeltaRow
        {
            get => _deltaRow;
        }

        public int DeltaCol
        {
            get => _deltaCol;
        }
        
        public static Direction Up = new Direction(0, 1);
        public static Direction Right = new Direction(1, 0);
        public static Direction Down = new Direction(0, -1);
        public static Direction Left = new Direction(-1, 0);
    }
}