using Xiangqi.Util;
using Xiangqi.Enum;

namespace Xiangqi.ChessPiece
{
    public class Rook: ChessPiece
    {
        public Rook(Cell cell, Boundary boundary, Side side, bool isDeath) : base(cell, boundary, side, isDeath)
        {
        }

        protected override void ShowMovableCell()
        {
            
        }
    }
}