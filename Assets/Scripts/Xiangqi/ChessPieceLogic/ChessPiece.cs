using System;
using System.Collections.Generic;
using System.Linq;
using Xiangqi.Enum;
using Xiangqi.Game;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;

namespace Xiangqi.ChessPieceLogic
{
    [Serializable]
    public class ChessPiece
    {
        public AbsoluteCell aCell;
        public bool isDead;
        public string side;
        public string type;

        protected Boundary boundary;
        [NonSerialized] public GameSnapshot gss;

        protected List<AbsoluteCell> movableCells;
        protected List<Path> paths;

        public ChessPiece(AbsoluteCell aCell, bool isDead, string side, string type, GameSnapshot gss)
        {
            this.aCell = aCell;
            this.isDead = isDead;
            this.side = side;
            this.type = type;
            this.gss = gss;
            movableCells = new List<AbsoluteCell>();
        }

        protected virtual List<Path> GetAvailablePaths()
        {
            return paths;
        }

        public List<AbsoluteCell> GetMovableCells()
        {
            UpdateMovableCells();
            return movableCells;
        }

        public List<AbsoluteCell> GetMovableAndNotLeadToGameOverCells()
        {
            UpdateMovableCells();
            return movableCells.Where(cell => !LeadToGameOver(cell)).ToList();
        }

        private bool LeadToGameOver(AbsoluteCell nextMove)
        {
            var nextMoveGss = new GameSnapshot(gss.chessboard, side);
            var chessPiece = nextMoveGss.chessboard[aCell.row, aCell.col];
            chessPiece.MoveTo(nextMove);

            var general = (General)nextMoveGss.FindChessPiece(side, ChessType.General);
            return general.CanBeKilled() || general.FaceWithOpponentGeneral();
        }

        public virtual void UpdateMovableCells()
        {
            movableCells.Clear();
            var originalCell = aCell.GetRelativeCell(side);
            foreach (var path in GetAvailablePaths())
            {
                var obstacle = 0;

                var rCell = aCell.GetRelativeCell(side);

                for (var step = 1; step <= path.maxSteps; step++)
                {
                    if (IsDestinationOutOfBoundary(originalCell, path, step)) break;

                    var hasOpponentPieceAtEnd = false;

                    for (var indexOfDirection = 0; indexOfDirection < path.directions.Count; indexOfDirection++)
                    {
                        var currentDirection = path.directions[indexOfDirection];

                        rCell.MoveAlongDirection(currentDirection, 1);

                        var currentACell = rCell.GetAbsoluteCell(side);

                        var pieceAtCurrentCell = gss.chessboard[currentACell.row, currentACell.col];
                        if (pieceAtCurrentCell == null) continue;

                        obstacle++;
                        if (indexOfDirection == path.directions.Count - 1 && pieceAtCurrentCell.side != side)
                            hasOpponentPieceAtEnd = true;
                    }

                    if (obstacle == 0 ||
                        (obstacle == 1 && hasOpponentPieceAtEnd && type != ChessType.Cannon) ||
                        (obstacle == 2 && hasOpponentPieceAtEnd && type == ChessType.Cannon))
                        movableCells.Add(rCell.GetAbsoluteCell(side));
                    else if (obstacle >= 2) break;
                }
            }
        }

        private bool IsDestinationOutOfBoundary(RelativeCell cell, Path path, int step)
        {
            var clonedCell = new BaseCell(cell);
            clonedCell.MoveAlongPath(path, step);

            return !boundary.IsWithinBoundary(clonedCell);
        }

        public void MoveTo(AbsoluteCell destination)
        {
            UpdateChessboard(destination);
            gss.SwitchTurn();
            gss.VerifyCheckMate();
        }

        private void UpdateChessboard(AbsoluteCell destination)
        {
            var destinationObj = gss.chessboard[destination.row, destination.col];
            destinationObj?.GetKilled();

            gss.chessboard[destination.row, destination.col] = this;
            gss.chessboard[aCell.row, aCell.col] = null;

            aCell = destination;
        }

        public void GetKilled()
        {
            isDead = true;
        }

        public virtual ChessPiece Clone(GameSnapshot newGss)
        {
            return new ChessPiece(new AbsoluteCell(aCell), isDead, side, type, newGss);
        }
    }
}