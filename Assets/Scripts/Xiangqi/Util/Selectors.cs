using System.Collections.Generic;
using System.Linq;
using Xiangqi.ChessPieceLogic;
using Xiangqi.Enum;
using Xiangqi.Enum.Command;

namespace Xiangqi.Util
{
    public static class Selectors
    {
        public static List<ChessPiece> SelectRowFromChessPieceRows(
            List<List<ChessPiece>> chessPieceRows,
            string verticalPosition,
            string chessSide = null)
        {
            var res = new List<ChessPiece>();

            // Flatten list of rows to list of chess pieces.
            if (verticalPosition == null) return chessPieceRows.SelectMany(row => row).ToList();

            if (chessPieceRows == null || chessPieceRows.Count == 0)
                return res;

            switch (chessPieceRows.Count)
            {
                case 2:
                    switch (verticalPosition)
                    {
                        case RelativePosition.Back:
                            res.AddRange(chessPieceRows[0]);
                            break;
                        case RelativePosition.Front:
                            res.AddRange(chessPieceRows[1]);
                            break;
                    }

                    break;
                case 3:
                    switch (verticalPosition)
                    {
                        case RelativePosition.Back:
                            res.AddRange(chessPieceRows[0]);
                            break;
                        case RelativePosition.Mid:
                            res.AddRange(chessPieceRows[1]);
                            break;
                        case RelativePosition.Front:
                            res.AddRange(chessPieceRows[2]);
                            break;
                    }

                    break;
                case 4:
                    switch (verticalPosition)
                    {
                        case RelativePosition.Back:
                            res.AddRange(chessPieceRows[0]);
                            break;
                        case RelativePosition.BackMid:
                            res.AddRange(chessPieceRows[1]);
                            break;
                        case RelativePosition.Mid:
                            return chessPieceRows[1].Concat(chessPieceRows[2]).ToList();
                        case RelativePosition.FrontMid:
                            res.AddRange(chessPieceRows[2]);
                            break;
                        case RelativePosition.Front:
                            res.AddRange(chessPieceRows[3]);
                            break;
                    }

                    break;
                case 5:
                    switch (verticalPosition)
                    {
                        case RelativePosition.Back:
                            res.AddRange(chessPieceRows[0]);
                            break;
                        case RelativePosition.BackMid:
                            res.AddRange(chessPieceRows[1]);
                            break;
                        case RelativePosition.Mid:
                            res.AddRange(chessPieceRows[2]);
                            break;
                        case RelativePosition.FrontMid:
                            res.AddRange(chessPieceRows[3]);
                            break;
                        case RelativePosition.Front:
                            res.AddRange(chessPieceRows[4]);
                            break;
                    }

                    break;
            }

            if (chessSide != null && chessSide == Side.Black)
                res.Reverse();

            return res;
        }

        public static List<ChessPiece> SelectColumnFromChessPieceColumns(
            List<List<ChessPiece>> chessPieceColumns,
            string horizontalPosition,
            string chessSide = null)
        {
            var res = new List<ChessPiece>();

            // Flatten list of columns to list of chess pieces.
            if (horizontalPosition == null) return chessPieceColumns.SelectMany(col => col).ToList();

            if (chessPieceColumns == null || chessPieceColumns.Count == 0)
                return res;

            switch (chessPieceColumns.Count)
            {
                case 2:
                    switch (horizontalPosition)
                    {
                        case RelativePosition.Left:
                            res.AddRange(chessPieceColumns[0]);
                            break;
                        case RelativePosition.Right:
                            res.AddRange(chessPieceColumns[1]);
                            break;
                    }

                    break;
                case 3:
                    switch (horizontalPosition)
                    {
                        case RelativePosition.Left:
                            res.AddRange(chessPieceColumns[0]);
                            break;
                        case RelativePosition.Mid:
                            res.AddRange(chessPieceColumns[1]);
                            break;
                        case RelativePosition.Right:
                            res.AddRange(chessPieceColumns[2]);
                            break;
                    }

                    break;
                case 4:
                    switch (horizontalPosition)
                    {
                        case RelativePosition.Left:
                            res.AddRange(chessPieceColumns[0]);
                            break;
                        case RelativePosition.LeftMid:
                            res.AddRange(chessPieceColumns[1]);
                            break;
                        case RelativePosition.Mid:
                            return chessPieceColumns[1].Concat(chessPieceColumns[2]).ToList();
                        case RelativePosition.RightMid:
                            res.AddRange(chessPieceColumns[2]);
                            break;
                        case RelativePosition.Right:
                            res.AddRange(chessPieceColumns[3]);
                            break;
                    }

                    break;
                case 5:
                    switch (horizontalPosition)
                    {
                        case RelativePosition.Left:
                            res.AddRange(chessPieceColumns[0]);
                            break;
                        case RelativePosition.LeftMid:
                            res.AddRange(chessPieceColumns[1]);
                            break;
                        case RelativePosition.Mid:
                            res.AddRange(chessPieceColumns[2]);
                            break;
                        case RelativePosition.RightMid:
                            res.AddRange(chessPieceColumns[3]);
                            break;
                        case RelativePosition.Right:
                            res.AddRange(chessPieceColumns[4]);
                            break;
                    }

                    break;
            }

            if (chessSide != null && chessSide == Side.Black)
                res.Reverse();

            return res;
        }
    }
}