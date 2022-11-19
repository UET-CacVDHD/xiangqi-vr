namespace Xiangqi.Enum
{
    public class Direction
    {
        public static readonly Direction Up = new(1, 0);
        public static readonly Direction Right = new(0, 1);
        public static readonly Direction Down = new(-1, 0);
        public static readonly Direction Left = new(0, -1);

        public static readonly Direction UpRight = new(1, 1);
        public static readonly Direction DownRight = new(-1, 1);
        public static readonly Direction DownLeft = new(-1, -1);
        public static readonly Direction UpLeft = new(1, -1);

        public Direction(int deltaRow, int deltaCol)
        {
            DeltaRow = deltaRow;
            DeltaCol = deltaCol;
        }

        public int DeltaCol { get; }
        public int DeltaRow { get; }
    }
}