using System;

namespace Xiangqi.Enum
{
    public class Side
    {
        public const string Red = "Red";
        public const string Black = "Black";

        public static string GetOppositeSide(string side)
        {
            return side switch
            {
                Red => Black,
                Black => Red,
                _ => throw new Exception("Invalid side")
            };
        }
    }
}