using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Xiangqi.ChessPieceLogic;
using Xiangqi.Enum;
using Xiangqi.Util;

namespace Xiangqi.Game
{
    [Serializable]
    public class GameSnapshot
    {
        public List<ChessPiece> chessPieces;

        [SerializeField] private string sideTurn;
        [SerializeField] private string state;

        public ChessPiece[,] chessboard;

        public GameSnapshot(ChessPiece[,] prevChessboard, string sideTurn)
        {
            chessPieces = new List<ChessPiece>();
            this.sideTurn = sideTurn;

            chessboard = new ChessPiece[Constants.BoardRows + 1, Constants.BoardCols + 1];
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
            {
                if (prevChessboard[i, j] == null) continue;
                chessboard[i, j] = prevChessboard[i, j].Clone(this);
            }
        }

        public string SideTurn
        {
            get => sideTurn;
            set => sideTurn = value;
        }

        public string State
        {
            get => state;
            set => state = value;
        }

        public void SwitchTurn()
        {
            sideTurn = Side.GetOppositeSide(sideTurn);
        }

        // TODO: The serialize process is quite couple to Unity, it should be refactored later by using C# feature.
        public void SaveToFile()
        {
            AddChessPieceIn2DArrayToList();
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(Constants.SavedFilePath, json);
        }

        // The 2D array is not serializable so we must use an List<T> instead.
        private void AddChessPieceIn2DArrayToList()
        {
            chessPieces.Clear();

            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null)
                    chessPieces.Add(chessboard[i, j]);
        }

        public static GameSnapshot LoadFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);

            var gameSnapshot = JsonUtility.FromJson<GameSnapshot>(json);
            gameSnapshot.chessboard = new ChessPiece[Constants.BoardRows + 1, Constants.BoardCols + 1];

            foreach (var piece in gameSnapshot.chessPieces)
                gameSnapshot.chessboard[piece.aCell.row, piece.aCell.col] = piece.type switch
                {
                    ChessType.Rook => new Rook(piece.aCell, piece.isDead, piece.side, piece.type, gameSnapshot),
                    ChessType.Horse => new Horse(piece.aCell, piece.isDead, piece.side, piece.type,
                        gameSnapshot),
                    ChessType.Elephant => new Elephant(piece.aCell, piece.isDead, piece.side, piece.type,
                        gameSnapshot),
                    ChessType.Advisor => new Advisor(piece.aCell, piece.isDead, piece.side, piece.type,
                        gameSnapshot),
                    ChessType.General => new General(piece.aCell, piece.isDead, piece.side, piece.type,
                        gameSnapshot),
                    ChessType.Cannon => new Cannon(piece.aCell, piece.isDead, piece.side, piece.type,
                        gameSnapshot),
                    _ => new Soldier(piece.aCell, piece.isDead, piece.side, piece.type, gameSnapshot)
                };

            return gameSnapshot;
        }

        public ChessPiece FindChessPiece(string cpSide, string cpType)
        {
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null && chessboard[i, j].side == cpSide && chessboard[i, j].type == cpType)
                    return chessboard[i, j];
            return null;
        }

        public void UpdateGameState()
        {
            if (StateIsCheckMate())
                state = StateIsGameOver() ? GameState.GameOver : GameState.Checkmate;
            else
                state = GameState.Playing;

            Debug.Log(state);
        }

        private bool StateIsCheckMate()
        {
            var general = (General)FindChessPiece(sideTurn, ChessType.General);
            return general.CanBeKilled();
        }

        private bool StateIsGameOver()
        {
            for (var i = 1; i <= Constants.BoardRows; i++)
            for (var j = 1; j <= Constants.BoardCols; j++)
                if (chessboard[i, j] != null && chessboard[i, j].side == sideTurn)
                    if (chessboard[i, j].GetMovableAndNotLeadToGameOverCells().Count > 0)
                        return false;
            return true;
        }
    }
}