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
        protected List<Cell> GetMovableCells()
        {
            var res = new List<Cell>();
            foreach (var path in paths)
            {
                var crossBoundary = false;
                var pathIsBlocked = false;
                var obstacle = 0;

                for (var i = 1; i <= path.maxSteps; ++i)
                {
                    var relativeCell = cell.GetSideRelativeCell(side);
                    relativeCell.MoveAlongPath(path, i - 1);

                    for (var j = 0; j < path.directions.Count; ++j)
                    {
                        var dir = path.directions[j];
                        if (!boundary.IsWithinBoundary(relativeCell.row + dir.DeltaRow,
                                relativeCell.col + dir.DeltaCol))
                        {
                            crossBoundary = true;
                            break;
                        }

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

                    if (crossBoundary || pathIsBlocked) break;
                    res.Add(relativeCell.GetCell(side));

                    if (obstacle < 1) continue;
                    if (type == ChessType.Cannon && obstacle == 1)
                        continue;
                    break;
                }
            }

            return res;
        }

        public void MoveTo(Cell cell)
        {
            Debug.Log("moveto" + cell);
            this.cell = cell;
        }

        public ChessPieceStoredData GetChessPieceStoredData()
        {
            return new ChessPieceStoredData(cell, isDeath, side, type);
        }
    }
}