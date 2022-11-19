using System;
using Xiangqi.Util;

namespace Xiangqi.Movement
{
    // Cell index starts from 1
    // 
    public class Cell
    {
        public Cell(int col, int row)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public Tuple<int, int> RedAsRed()
        {
            return new Tuple<int, int>(Row, Col);
        }

        public Tuple<int, int> RedAsBlack()
        {
            return new Tuple<int, int>(Constant.BoardHeight - Row, Constant.BoardWidth - Col);
        }
    }
}