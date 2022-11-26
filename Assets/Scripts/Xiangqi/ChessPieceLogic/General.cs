using System;
using System.Collections.Generic;
using System.Linq;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

namespace Xiangqi.ChessPieceLogic
{
    public class General : ChessPiece
    {
        public General(AbsoluteCell aCell, bool isDead, string side, string type, ChessPiece[,] chessboard) : base(
            aCell, isDead, side, type, chessboard)
        {
            paths = new List<Path>
            {
                new(new List<Direction> { Direction.Up }, 1),
                new(new List<Direction> { Direction.Right }, 1),
                new(new List<Direction> { Direction.Down }, 1),
                new(new List<Direction> { Direction.Left }, 1)
            };
            boundary = Boundary.Palace;
        }

        public bool CanBeKilled()
        {
            for (var i = 1; i <= Constants.BoardRows; ++i)
            for (var j = 1; j <= Constants.BoardCols; ++j)
            {
                if (chessboard[i, j] == null || chessboard[i, j].side == side) continue;
                if (chessboard[i, j].GetMovableCells().Any(cell => cell.Equals(aCell))) return true;
            }

            return false;
        }

        public bool FaceWithOpponentGeneral()
        {
            var opponentGeneral = Helper.FindChessPiece(chessboard, Side.GetOppositeSide(side), ChessType.General);

            if (opponentGeneral.aCell.col != aCell.col)
                return false;

            for (var i = Math.Min(aCell.row, opponentGeneral.aCell.row) + 1;
                 i <= Math.Max(aCell.row, opponentGeneral.aCell.row) - 1;
                 ++i)
                if (chessboard[i, aCell.col] != null)
                    return false;

            return true;
        }

        public override ChessPiece Clone(ChessPiece[,] newChessboard)
        {
            return new General(new AbsoluteCell(aCell), isDead, side, type, newChessboard);
        }
    }
}