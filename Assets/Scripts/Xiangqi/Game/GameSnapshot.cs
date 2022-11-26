using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Xiangqi.ChessPieceLogic;
using Xiangqi.Util;

namespace Xiangqi.Game
{
    [Serializable]
    public class GameSnapshot
    {
        public string turn;

        public List<ChessPiece> chessPieces;

        public ChessPiece[,] chessboard;


        public GameSnapshot(ChessPiece[,] prevChessboard)
        {
            chessPieces = new List<ChessPiece>();

            chessboard = new ChessPiece[Constants.BoardRows + 1, Constants.BoardCols + 1];
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
            {
                if (prevChessboard[i, j] == null) continue;
                chessboard[i, j] = prevChessboard[i, j].Clone();
                chessboard[i, j].chessboard = chessboard;
            }
        }

        // TODO: The serialize process is quite couple to Unity, it should be refactored later by using C# feature.
        public void SaveToFile()
        {
            chessPieces.Clear();

            AddChessPieceIn2DArrayToList();
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(Constants.SaveFilePath, json);
        }

        // The 2D array is not serializable so we must use an List<T> instead.
        private void AddChessPieceIn2DArrayToList()
        {
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null)
                    chessPieces.Add(chessboard[i, j]);
        }

        public ChessPiece FindChessPiece(string side, string type)
        {
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null && chessboard[i, j].side == side && chessboard[i, j].type == type)
                    return chessboard[i, j];
            return null;
        }
    }
}