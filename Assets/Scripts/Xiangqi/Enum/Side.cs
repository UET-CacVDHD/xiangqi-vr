using System;

namespace Xiangqi.Enum
{
    public class Side
    {
        public const string Red = "Red";
        public const string Black = "Black";

        public static string GetOppositeSide(string side)
        {
            if (side == Red)
                return Black;
            if (side == Black)
                return Red;
            throw new Exception("Invalid side");
        }
    }
}