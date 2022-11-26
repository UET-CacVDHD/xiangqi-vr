using System.Collections.Generic;
using Xiangqi.Enum;
using Xiangqi.Game;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

namespace Xiangqi.ChessPieceLogic
{
    public class Rook : ChessPiece
    {
        public Rook(AbsoluteCell aCell, bool isDead, string side, string type, GameSnapshot gss) : base(aCell,
            isDead, side, type, gss)
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, Constants.BoardRows),
                new(new List<Direction> { Direction.Right }, Constants.BoardCols),
                new(new List<Direction> { Direction.Down }, Constants.BoardRows),
                new(new List<Direction> { Direction.Left }, Constants.BoardCols)
            };
            boundary = Boundary.Full;
        }

        public override ChessPiece Clone(GameSnapshot newGss)
        {
            return new Rook(new AbsoluteCell(aCell), isDead, side, type, newGss);
        }
    }
}