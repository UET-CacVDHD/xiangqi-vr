using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Motion;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

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
        public ChessPiece[,] chessboard;
        protected List<AbsoluteCell> movableCells;
        protected List<Path> paths;

        public ChessPiece(AbsoluteCell aCell, bool isDead, string side, string type)
        {
            this.aCell = aCell;
            this.isDead = isDead;
            this.side = side;
            this.type = type;
            movableCells = new List<AbsoluteCell>();
            chessboard = new ChessPiece[Constants.BoardRows + 1, Constants.BoardCols + 1];
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

                        var pieceAtCurrentCell = chessboard[currentACell.row, currentACell.col];
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
            Debug.Log("moveto" + destination);

            var destinationObj = chessboard[destination.row, destination.col];
            destinationObj?.GetKilled();

            chessboard[destination.row, destination.col] = this;
            chessboard[aCell.row, aCell.col] = null;

            aCell = destination;
        }

        protected bool CanBeKilled()
        {
            for (var i = 1; i <= Constants.BoardRows; ++i)
            for (var j = 1; j <= Constants.BoardCols; ++j)
            {
                if (chessboard[i, j] == null || chessboard[i, j].side == side ||
                    chessboard[i, j].type == ChessType.General) continue;

                if (chessboard[i, j].GetMovableCells().Any(cell => cell.Equals(aCell))) return true;
            }

            return false;
        }

        public void GetKilled()
        {
            isDead = true;
        }

        public virtual ChessPiece Clone()
        {
            return new ChessPiece(new AbsoluteCell(aCell), isDead, side, type);
        }
    }
}