using Xiangqi.ChessPieceLogic;

namespace Xiangqi.Util
{
    public class Helper
    {
        // TODO: update to dictionary instead of looping through 2D array
        public static ChessPiece FindChessPiece(ChessPiece[,] chessboard, string cpSide, string cpType)
        {
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null && chessboard[i, j].side == cpSide && chessboard[i, j].type == cpType)
                    return chessboard[i, j];
            return null;
        }
    }
}