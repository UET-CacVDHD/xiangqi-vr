using System;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    // Cell row index range: 1 (the lower left conner of the chessboard) -> 9.
    // Cell col index range: 1 (the lower left conner of the chessboard) -> 10.
    [Serializable]
    public class Cell
    {
        [SerializeField] private int col;
        [SerializeField] private int row;

        public Cell(int row, int col)
        {
            this.col = col;
            this.row = row;
        }

        public Cell(Cell c)
        {
            col = c.col;
            row = c.row;
        }

        public int Col
        {
            get => col;
            set => col = value;
        }

        public int Row
        {
            get => row;
            set => row = value;
        }

        public override string ToString()
        {
            return $"Cell: ({Row}, {Col})";
        }

        public SideRelativeCell GetSideRelativeCell(string side)
        {
            return side == Side.Red
                ? new SideRelativeCell(Row, Col)
                : new SideRelativeCell(Constant.BoardRows - Row + 1, Constant.BoardCols - Col + 1);
        }
    }
}