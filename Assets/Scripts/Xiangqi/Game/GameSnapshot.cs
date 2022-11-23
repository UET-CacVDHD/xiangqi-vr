using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Xiangqi.ChessPiece;
using Xiangqi.Util;

namespace Xiangqi.Game
{
    [Serializable]
    public class GameSnapshot
    {
        public string turn;

        public List<ChessPieceStoredData> chessPieceStoredDataList = new();

        public ChessPiece.ChessPiece[,] chessboard =
            new ChessPiece.ChessPiece[Constants.BoardRows + 1, Constants.BoardCols + 1];


        // TODO: The serialize process is quite couple to Unity, it should be refactored later by using C# feature.
        public void SaveToFile()
        {
            AddChessPieceIn2DArrayToList();
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(Constants.SaveFilePath, json);
        }

        // The 2D array is not serializable so we must use an List<T> instead.
        private void AddChessPieceIn2DArrayToList()
        {
            chessPieceStoredDataList.Clear();
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null)
                {
                    var chessPiece = chessboard[i, j].GetChessPieceStoredData();
                    chessPieceStoredDataList.Add(chessPiece);
                    Debug.Log(chessPiece);
                }
        }
    }
}