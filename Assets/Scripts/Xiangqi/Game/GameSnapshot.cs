using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity._3D;
using UnityEngine;
using Xiangqi.ChessPieceLogic;
using Xiangqi.Command;
using Xiangqi.Enum;
using Xiangqi.Enum.Command;
using Xiangqi.Motion.Cell;
using Xiangqi.Parser;
using Xiangqi.Util;

namespace Xiangqi.Game
{
    [Serializable]
    public class GameSnapshot
    {
        public List<ChessPiece> chessPieces;

        [SerializeField] private string sideTurn;
        [SerializeField] private string state;

        private AudioManager _audioManager;

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
                    GetChessPiecesByRow(command.StartChessType,
                        piece => piece.side == sideTurn &&
                                 // xét nếu có số thứ tự cột bắt đầu
                                 (Utilities.IsUndefined(command.StartColumn)
                                  || piece.aCell.GetRelativeCell(sideTurn).col == command.StartColumn)),
                    command.StartVerticalRelativePosition,
                    sideTurn) // quân và vị trí tương đối trước/sau/giữa
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
                            case DirectionCode.Sideways when Utilities.IsUndefined(command.EndColumn):
                                return false;
                            case DirectionCode.Sideways:
                                return movableRCell.row == cpRCell.row &&
                                       movableRCell.col == command.EndColumn;
                            // tiến thoái: tượng, mã, sĩ
                            case DirectionCode.Forward or DirectionCode.Backward
                                when !Utilities.IsUndefined(command.EndColumn):
                            {
                                switch (command.Direction)
                                {
                                    case DirectionCode.Forward:
                                        return movableRCell.col == command.EndColumn && movableRCell.row > cpRCell.row;
                                    case DirectionCode.Backward:
                                        return movableRCell.col == command.EndColumn && movableRCell.row < cpRCell.row;
                                }

                                break;
                            }
                            //   tiến thoái: tướng, xe, tốt, pháo
                            case DirectionCode.Forward or DirectionCode.Backward:
                            {
                                var numOfStep = command.NumberOfSteps > 0 ? command.NumberOfSteps : 1;
                                switch (command.Direction)
                                {
                                    case DirectionCode.Forward:
                                        return movableRCell.col == cpRCell.col &&
                                               movableRCell.row == cpRCell.row + numOfStep;
                                    case DirectionCode.Backward:
                                        return movableRCell.col == cpRCell.col &&
                                               movableRCell.row == cpRCell.row - numOfStep;
                                }

                                break;
                            }
                        }

                        return false;
                    }).ToList()
                }).Where(cp => cp.MovableCells.Count > 0).ToList();
        }

        public List<ChessPieceMovableCells> ProcessExtendedMovementCommand(ExtendedMovementCommand command)
        {
            var selectionExtendedRelativePos = new List<ChessPiece>();

            if (command.StartExtendedRelativePosition is RelativePosition.Front or RelativePosition.FrontMid
                or RelativePosition.Mid or RelativePosition.BackMid or RelativePosition.Back)
            {
                // quân và vị trí tương đối trước/sau/giữa
                var rows = GetChessPiecesByRow(command.StartChessType,
                    piece => piece.side == sideTurn &&
                             // xét nếu có số thứ tự cột bắt đầu
                             (Utilities.IsUndefined(command.StartColumn)
                              || piece.aCell.GetRelativeCell(sideTurn).col == command.StartColumn));
                var selectedRow = Selectors.SelectRowFromChessPieceRows(rows, command.StartExtendedRelativePosition,
                    sideTurn);

                selectionExtendedRelativePos.AddRange(selectedRow);
            }

            if (command.StartExtendedRelativePosition is RelativePosition.Left or RelativePosition.LeftMid
                or RelativePosition.Mid or RelativePosition.RightMid or RelativePosition.Right or null)
            {
                // quân và vị trí tương đối trái/phải/giữa
                var cols = GetChessPiecesByColumn(command.StartChessType,
                    piece => piece.side == sideTurn &&
                             // xét nếu có số thứ tự cột bắt đầu
                             (Utilities.IsUndefined(command.StartColumn)
                              || piece.aCell.GetRelativeCell(sideTurn).col == command.StartColumn));
                var selectedCol = Selectors.SelectColumnFromChessPieceColumns(cols,
                    command.StartExtendedRelativePosition,
                    sideTurn);

                selectionExtendedRelativePos.AddRange(selectedCol);
            }


            return selectionExtendedRelativePos
                .Select(GetChessPieceMovableCells)
                .Select(cp => new ChessPieceMovableCells
                {
                    ChessPiece = cp.ChessPiece,
                    MovableCells = cp.MovableCells.Where(movableCell =>
                    {
                        var cpRCell = cp.ChessPiece.aCell.GetRelativeCell(sideTurn);
                        var movableRCell = movableCell.GetRelativeCell(sideTurn);
                        switch (command.ExtendedDirection)
                        {
                            // bình
                            case DirectionCode.Sideways when Utilities.IsUndefined(command.EndColumn):
                                return false;
                            case DirectionCode.Sideways:
                                return movableRCell.row == cpRCell.row &&
                                       movableRCell.col == command.EndColumn;
                            // tiến thoái: tượng, mã, sĩ
                            case DirectionCode.Forward or DirectionCode.Backward
                                when !Utilities.IsUndefined(command.EndColumn):
                            {
                                switch (command.ExtendedDirection)
                                {
                                    case DirectionCode.Forward:
                                        return movableRCell.col == command.EndColumn && movableRCell.row > cpRCell.row;
                                    case DirectionCode.Backward:
                                        return movableRCell.col == command.EndColumn && movableRCell.row < cpRCell.row;
                                }

                                break;
                            }
                            //   tiến thoái: tướng, xe, tốt, pháo
                            case DirectionCode.Forward or DirectionCode.Backward:
                            {
                                var numOfStep = command.NumberOfSteps > 0 ? command.NumberOfSteps : 1;
                                switch (command.ExtendedDirection)
                                {
                                    case DirectionCode.Forward:
                                        return movableRCell.col == cpRCell.col &&
                                               movableRCell.row == cpRCell.row + numOfStep;
                                    case DirectionCode.Backward:
                                        return movableRCell.col == cpRCell.col &&
                                               movableRCell.row == cpRCell.row - numOfStep;
                                }

                                break;
                            }
                            // tiến thoái chéo: tượng, mã, sĩ
                            case DirectionCode.ForwardLeft or DirectionCode.ForwardRight or DirectionCode.BackwardLeft
                                or DirectionCode.BackwardRight:
                            {
                                long defaultStep = 0;
                                switch (command.StartChessType)
                                {
                                    case ChessType.Elephant:
                                        defaultStep = 2;
                                        break;
                                    case ChessType.Horse:
                                    case ChessType.Advisor:
                                        defaultStep = 1;
                                        break;
                                }


                                var numOfStep = !Utilities.IsUndefined(command.NumberOfSteps)
                                    ? command.NumberOfSteps
                                    : defaultStep;


                                switch (command.ExtendedDirection)
                                {
                                    case DirectionCode.ForwardLeft:
                                        return movableRCell.col == cpRCell.col - numOfStep &&
                                               movableRCell.row > cpRCell.row;
                                    case DirectionCode.ForwardRight:
                                        return movableRCell.col == cpRCell.col + numOfStep &&
                                               movableRCell.row > cpRCell.row;
                                    case DirectionCode.BackwardLeft:
                                        return movableRCell.col == cpRCell.col - numOfStep &&
                                               movableRCell.row < cpRCell.row;
                                    case DirectionCode.BackwardRight:
                                        return movableRCell.col == cpRCell.col + numOfStep &&
                                               movableRCell.row < cpRCell.row;
                                }

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

        public void ParseAndExecuteCommand(string command)
        {
            Debug.Log($"Parsing: '{command}'");
            // TODO
            Unity3DGameManager.instance.UpdateSubTitleAlert(command);
            var chessPiecesPossibleMoves = new List<ChessPieceMovableCells>();

            var parser = CommandParser.Parser;
            var parseSuccess = parser.TryParse(command, out var polyCommand);
            if (!parseSuccess)
            {
                Debug.Log("parse fail");
                return;
            }

            var extMoveCommand = polyCommand.GetExtendedMovementCommand();
            if (extMoveCommand != null)
            {
                Debug.Log(extMoveCommand);
                chessPiecesPossibleMoves = ProcessExtendedMovementCommand(extMoveCommand);
                goto handlePossibleMoves;
            }

            var stdCommand = polyCommand.GetStandardCommand();
            if (stdCommand != null) chessPiecesPossibleMoves = ProcessStandardCommand(stdCommand);


            handlePossibleMoves:
            if (
                chessPiecesPossibleMoves.Count is <= 0 or > 1 ||
                chessPiecesPossibleMoves[0].MovableCells.Count is <= 0 or > 1)
            {
                Debug.Log($"No/too many valid moves: Count: {chessPiecesPossibleMoves.Count}");
                return;
            }


            var chessPieceToMove = chessPiecesPossibleMoves[0].ChessPiece;
            var cellToMoveTo = chessPiecesPossibleMoves[0].MovableCells[0];

            Debug.Log($"{chessPieceToMove.aCell} -> {cellToMoveTo}");

            chessboard[chessPieceToMove.aCell.row,
                chessPieceToMove.aCell.col].MoveTo(cellToMoveTo);
        }

        public class ChessPieceMovableCells
        {
            public ChessPiece ChessPiece { get; set; }
            public List<AbsoluteCell> MovableCells { get; set; } = new();
        }
    }
}