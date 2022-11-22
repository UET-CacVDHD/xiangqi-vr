using System;
using System.Collections.Generic;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Movement.Cell;

namespace Xiangqi.ChessPiece
{
    [Serializable]
    public class ChessPiece : MonoBehaviour
    {
        public static ChessPiece[,] chessboard;
        public AbsoluteCell aCell;
        public bool isDeath;
        public string side;
        public string type;
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
            transform.position = CoordinateManager.Instance.GetCoordinateFromChessboardCell(aCell);
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
        protected List<AbsoluteCell> GetMovableCells()
        {
            var res = new List<AbsoluteCell>();
            foreach (var path in paths)
            {
                var pathIsBlocked = false;
                var obstacle = 0;

                for (var i = 1; i <= path.maxSteps; ++i)
                {
                    if (IsDestinationOutOfBoundary(path, i)) break;

                    var rCell = aCell.GetSideRelativeCell(side);
                    rCell.MoveAlongPath(path, i - 1);

                    for (var j = 0; j < path.directions.Count; ++j)
                    {
                        var dir = path.directions[j];

                        rCell.MoveAlongDirection(dir, 1);

                        var currentACell = rCell.GetCell(side);
                        if (chessboard[currentACell.row, currentACell.col] == null) continue;

                        if (j <= path.directions.Count - 2)
                        {
                            pathIsBlocked = true;
                            break;
                        }

                        obstacle++;
                    }

                    if (pathIsBlocked) break;
                    res.Add(rCell.GetCell(side));

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
            var desCell = new AbsoluteCell(aCell);
            desCell.MoveAlongPath(path, step);

            return !boundary.IsWithinBoundary(desCell);
        }

        public void MoveTo(AbsoluteCell destination)
        {
            Debug.Log("moveto" + destination);

            var destinationObj = chessboard[destination.row, destination.col];
            if (destinationObj != null) destinationObj.isDeath = true;

            chessboard[destination.row, destination.col] = this;
            chessboard[aCell.row, aCell.col] = null;

            aCell = destination;
        }

        public ChessPieceStoredData GetChessPieceStoredData()
        {
            return new ChessPieceStoredData(aCell, isDeath, side, type);
        }
    }
}