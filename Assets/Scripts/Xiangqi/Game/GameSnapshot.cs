using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Xiangqi.ChessPieceLogic;
using Xiangqi.Command;
using Xiangqi.Enum;
using Xiangqi.Enum.Command;
using Xiangqi.Motion.Cell;
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

            chessboard = new ChessPiece[Constant.BoardRows + 1, Constant.BoardCols + 1];
            for (var i = 1; i <= Constant.BoardRows; i++)
            for (var j = 1; j <= Constant.BoardCols; j++)
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
            File.WriteAllText(Constant.StoredGamePath, json);
        }

        // The 2D array is not serializable so we must use an List<T> instead.
        private void AddChessPieceIn2DArrayToList()
        {
            chessPieces.Clear();

            for (var i = 1; i <= Constant.BoardRows; i++)
            for (var j = 1; j <= Constant.BoardCols; j++)
                if (chessboard[i, j] != null)
                    chessPieces.Add(chessboard[i, j]);
        }

        public static GameSnapshot LoadFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);

            var gameSnapshot = JsonUtility.FromJson<GameSnapshot>(json);
            gameSnapshot.chessboard = new ChessPiece[Constant.BoardRows + 1, Constant.BoardCols + 1];

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
            for (var i = 1; i <= Constant.BoardRows; i++)
            for (var j = 1; j <= Constant.BoardCols; j++)
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
            for (var i = 1; i <= Constant.BoardRows; i++)
            for (var j = 1; j <= Constant.BoardCols; j++)
                if (chessboard[i, j] != null && chessboard[i, j].side == sideTurn)
                    if (chessboard[i, j].GetMovableAndNotLeadToGameOverCells().Count > 0)
                        return false;
            return true;
        }

        public List<ChessPieceMovableCells> ProcessStandardCommand(StandardCommand command)
        {
            return Selectors.SelectRowFromChessPieceRows(
                    GetChessPiecesByRow(command.StartChessType, piece => piece.side == sideTurn),
                    command.StartVerticalRelativePosition,
                    sideTurn) // quân và vị trí tương đối trước/sau/giữa
                .Where(cp => // số thứ tự cột bắt đầu
                {
                    if (Utilities.IsUndefined(command.StartColumn)) return true;

                    return cp.aCell.GetRelativeCell(sideTurn).col == command.StartColumn;
                })
                .Select(GetChessPieceMovableCells)
                .Select(cp => new ChessPieceMovableCells
                {
                    ChessPiece = cp.ChessPiece,
                    MovableCells = cp.MovableCells.Where(movableCell =>
                    {
                        var cpRCell = cp.ChessPiece.aCell.GetRelativeCell(sideTurn);
                        var movableRCell = movableCell.GetRelativeCell(sideTurn);
                        switch (command.Direction)
                        {
                            // bình
                            case DirectionCode.Sideways when command.EndColumn <= 0:
                                return false;
                            case DirectionCode.Sideways:
                                return movableRCell.row == cpRCell.row &&
                                       movableRCell.col == command.EndColumn;
                            // tiến thoái: tượng, mã, sĩ
                            case DirectionCode.Forward or DirectionCode.Backward when command.EndColumn > 0:
                            {
                                if (command.Direction == DirectionCode.Forward)
                                    return movableRCell.col == command.EndColumn && movableRCell.row > cpRCell.row;
                                if (command.Direction == DirectionCode.Backward)
                                    return movableRCell.col == command.EndColumn && movableRCell.row < cpRCell.row;
                                break;
                            }
                            //   tiến thoái: tướng, xe, tốt, pháo
                            case DirectionCode.Forward or DirectionCode.Backward:
                            {
                                var numOfStep = command.NumberOfSteps > 0 ? command.NumberOfSteps : 1;
                                if (command.Direction == DirectionCode.Forward)
                                    return movableRCell.col == cpRCell.col &&
                                           movableRCell.row == cpRCell.row + numOfStep;
                                if (command.Direction == DirectionCode.Backward)
                                    return movableRCell.col == cpRCell.col &&
                                           movableRCell.row == cpRCell.row - numOfStep;
                                break;
                            }
                        }

                        return false;
                    }).ToList()
                }).Where(cp => cp.MovableCells.Count > 0).ToList();
        }

        public List<List<ChessPiece>> GetChessPiecesByRow(string chessType,
            Func<ChessPiece, bool> filterCondition = null)
        {
            var rows = new List<List<ChessPiece>>();

            for (var i = 1; i <= Constant.BoardRows; i++)
            {
                var row = new List<ChessPiece>();
                for (var j = 1; j <= Constant.BoardCols; j++)
                    if (chessboard[i, j] != null
                        && chessboard[i, j].type == chessType
                        && (filterCondition == null || filterCondition(chessboard[i, j])))
                        row.Add(chessboard[i, j]);

                if (row.Count > 0)
                    rows.Add(row);
            }

            return rows;
        }

        public List<List<ChessPiece>> GetChessPiecesByColumn(string chessType,
            Func<ChessPiece, bool> filterCondition = null)
        {
            var cols = new List<List<ChessPiece>>();

            for (var j = 1; j <= Constant.BoardCols; j++)
            {
                var col = new List<ChessPiece>();
                for (var i = 1; i <= Constant.BoardRows; i++)
                    if (chessboard[i, j] != null
                        && chessboard[i, j].type == chessType
                        && (filterCondition == null || filterCondition(chessboard[i, j])))
                        col.Add(chessboard[i, j]);

                if (col.Count > 0)
                    cols.Add(col);
            }

            return cols;
        }

        public ChessPieceMovableCells GetChessPieceMovableCells(ChessPiece chessPiece)
        {
            return new ChessPieceMovableCells
            {
                ChessPiece = chessPiece,
                MovableCells = chessPiece.GetMovableAndNotLeadToGameOverCells()
            };
        }

        public class ChessPieceMovableCells
        {
            public ChessPiece ChessPiece { get; set; }
            public List<AbsoluteCell> MovableCells { get; set; } = new();
        }
    }
}