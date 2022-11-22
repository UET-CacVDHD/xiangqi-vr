using System;
using System.Collections.Generic;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Movement;

namespace Xiangqi.ChessPiece
{
    [Serializable]
    public class ChessPiece : MonoBehaviour
    {
        public static ChessPiece[,] chessboard;
        [SerializeField] public Cell cell;
        [SerializeField] public bool isDeath;
        [SerializeField] public string side;
        [SerializeField] public string type;
        protected Boundary boundary;
        protected List<Path> paths;

        public Boundary Boundary
        {
            get => boundary;
            set => boundary = value;
        }

        // Update coordinate based on Cell per frame
        private void Update()
        {
            transform.position = CoordinateManager.Instance.GetCoordinateFromChessboardCell(cell);
        }

        public void OnMouseDown()
        {
            Debug.Log(gameObject + "is clicked");
            CoordinateManager.Instance.chosenChessPiece = this;
            var movableCells = GetMovableCells();
            CoordinateManager.Instance.ShowHintIndicatorForAChessPiece(movableCells);
        }

        // TODO: split into smaller functions
        // TODO: handle cannon, general case
        protected List<Cell> GetMovableCells()
        {
            var res = new List<Cell>();
            foreach (var path in paths)
            {
                var pathIsBlocked = false;
                var obstacle = 0;

                for (var i = 1; i <= path.maxSteps; ++i)
                {
                    if (IsDestinationOutOfBoundary(path, i)) break;

                    var relativeCell = cell.GetSideRelativeCell(side);
                    relativeCell.MoveAlongPath(path, i - 1);

                    for (var j = 0; j < path.directions.Count; ++j)
                    {
                        var dir = path.directions[j];

                        relativeCell.MoveAlongDirection(dir, 1);

                        var currentCell = relativeCell.GetCell(side);
                        if (chessboard[currentCell.row, currentCell.col] == null) continue;

                        if (j <= path.directions.Count - 2)
                        {
                            pathIsBlocked = true;
                            break;
                        }

                        obstacle++;
                    }

                    if (pathIsBlocked) break;
                    res.Add(relativeCell.GetCell(side));

                    if (obstacle < 1) continue;
                    if (type == ChessType.Cannon && obstacle == 1)
                        continue;
                    break;
                }
            }

            return res;
        }

        private bool IsDestinationOutOfBoundary(Path path, int step)
        {
            var desCell = new Cell(cell);
            desCell.MoveAlongPath(path, step);

            return !boundary.IsWithinBoundary(desCell);
        }

        public void MoveTo(Cell destination)
        {
            Debug.Log("moveto" + destination);

            var destinationObj = chessboard[destination.row, destination.col];
            if (destinationObj != null)
            {
                destinationObj.isDeath = true;
            }

            chessboard[destination.row, destination.col] = this;
            chessboard[cell.row, cell.col] = null;
            
            cell = destination;
        }

        public ChessPieceStoredData GetChessPieceStoredData()
        {
            return new ChessPieceStoredData(cell, isDeath, side, type);
        }
    }
}