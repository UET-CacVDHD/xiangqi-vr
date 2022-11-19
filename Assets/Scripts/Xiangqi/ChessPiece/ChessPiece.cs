using System;
using System.Collections;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Movement;
using Xiangqi.Util;

namespace Xiangqi.ChessPiece
{
    [Serializable]
    public class ChessPiece : MonoBehaviour
    {
        protected Boundary boundary;
        protected Cell cell;
        protected bool isDeath;
        protected ArrayList paths;
        protected SideRelativeCell relativeCell;
        protected string side;

        public Boundary Boundary
        {
            get => boundary;
            set => boundary = value;
        }

        public Cell Cell
        {
            get => cell;
            set => cell = value;
        }

        public bool IsDeath
        {
            get => isDeath;
            set => isDeath = value;
        }

        public ArrayList Paths
        {
            get => paths;
            set => paths = value;
        }

        public string Side
        {
            get => side;
            set => side = value;
        }

        private void Start()
        {
            paths = new ArrayList
            {
                new Path(new ArrayList { Direction.Up }, Constant.BoardRows),
                new Path(new ArrayList { Direction.Right }, Constant.BoardCols),
                new Path(new ArrayList { Direction.Down }, Constant.BoardRows),
                new Path(new ArrayList { Direction.Left }, Constant.BoardCols)
            };
            ShowMovableCells();
        }

        // Update coordinate based on Cell per frame
        private void Update()
        {
            transform.position = CoordinateManager.Instance.GetCoordinateFromChessboardCell(cell);
        }

        public void OnMouseDown()
        {
            Debug.Log(gameObject + "is clicked");
        }

        protected void ShowMovableCells()
        {
            var sideRelativeCell = cell.GetSideRelativeCell(side);
            foreach (Path path in paths)
            {
                var crossBoundary = false;

                for (var i = 1; i <= path.MaxSteps; ++i)
                {
                    if (crossBoundary) break;
                    foreach (Direction dir in path.Directions)
                        if (!boundary.IsWithinBoundary(sideRelativeCell.Row + dir.DeltaRow * i,
                                sideRelativeCell.Col + dir.DeltaCol * i))
                            crossBoundary = true;
                        else
                            Debug.Log(new SideRelativeCell(sideRelativeCell.Row + dir.DeltaRow * i,
                                sideRelativeCell.Col + dir.DeltaCol * i).GetCell(side));
                }
            }
        }

        public void MoveTo(Cell cell)
        {
            this.cell = cell;
        }
    }
}