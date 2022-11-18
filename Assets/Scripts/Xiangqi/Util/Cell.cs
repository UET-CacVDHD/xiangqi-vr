using UnityEngine;

namespace Xiangqi.Util
{
    public class Cell
    {
       private int _row;
       private int _col;

       public Cell(int row, int col)
       {
           _row = row;
           _col = col;
       }

       public int Row
       {
           get => _row;
           set => _row = value;
       }

       public int Col
       {
           get => _col;
           set => _col = value;
       }

    }
}