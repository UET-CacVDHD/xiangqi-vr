using System;
using System.Collections.Generic;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Movement;
using Xiangqi.Movement.Cell;

namespace Xiangqi.ChessPiece
{
    [Serializable]
    public class ChessPiece : MonoBehaviour
    {
        public static ChessPiece[,] chessboard;
        public AbsoluteCell aCell;
        public bool isDead;
        public string side;
        public string type;

        private CoordinateManager _coordinateManager;
        protected Boundary boundary;
        protected List<Path> paths;

        public Boundary Boundary
        {
            get => boundary;
            set => boundary = value;
        }

        protected virtual void Start()
        {
            _coordinateManager = GameObject.Find("CoordinateManager").GetComponent<CoordinateManager>();
        }

        // Update coordinate based on Cell per frame
        private void Update()
        {
            transform.position = _coordinateManager.GetCoordinateFromChessboardCell(aCell);
        }

        public void OnMouseUpAsButton()
        {
            _coordinateManager.SetChosenChessPiece(this);
            var movableCells = GetMovableCells();
            _coordinateManager.ShowHintIndicatorsAtCells(movableCells);
        }

        protected virtual List<Path> GetAvailablePaths()
        {
            return paths;
        }


        // TODO: split into smaller functions
        // TODO: handle cannon, general case
        protected List<AbsoluteCell> GetMovableCells()
        {
            var res = new List<AbsoluteCell>();
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
                        if (pieceAtCurrentCell != null)
                        {
                            obstacle++;
                            if (indexOfDirection == path.directions.Count - 1 && pieceAtCurrentCell.side != side)
                                hasOpponentPieceAtEnd = true;
                        }
                    }

                    if (obstacle == 0 ||
                        (obstacle == 1 && hasOpponentPieceAtEnd && type != ChessType.Cannon) ||
                        (obstacle == 2 && hasOpponentPieceAtEnd && type == ChessType.Cannon))
                        res.Add(rCell.GetAbsoluteCell(side));
                }
            }

            return res;
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
            if (destinationObj != null) destinationObj.isDead = true;

            chessboard[destination.row, destination.col] = this;
            chessboard[aCell.row, aCell.col] = null;

            aCell = destination;
        }

        public ChessPieceStoredData GetChessPieceStoredData()
        {
            return new ChessPieceStoredData(aCell, isDead, side, type);
        }
    }
}