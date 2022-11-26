using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Game;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;

namespace Xiangqi.ChessPieceLogic
{
    public class General : ChessPiece
    {
        public General(AbsoluteCell aCell, bool isDead, string side, string type) : base(aCell, isDead, side, type)
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

        public override void UpdateMovableCells()
        {
            base.UpdateMovableCells();
            movableCells = movableCells.Where(NextMoveIsValid).ToList();
        }

        private bool NextMoveIsValid(AbsoluteCell nextMove)
        {
            var nextMoveGss = new GameSnapshot(chessboard);

            var general = (General)nextMoveGss.FindChessPiece(side, ChessType.General);
            general.MoveTo(nextMove);

            Debug.Log(nextMove);
            Debug.Log(general.CanBeKilled());
            return !general.CanBeKilled();
        }

        private bool GeneralFaceToFace()
        {
            return false;
        }

        public override ChessPiece Clone()
        {
            return new General(new AbsoluteCell(aCell), isDead, side, type);
        }
    }
}