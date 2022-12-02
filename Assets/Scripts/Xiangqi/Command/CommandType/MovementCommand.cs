using System;
using System.Collections.Generic;
using Xiangqi.ChessPieceLogic;
using Xiangqi.Enum;
using Xiangqi.Enum.Command;
using Xiangqi.Game;
using Xiangqi.Util;

namespace Xiangqi.Command.CommandType
{
    public class MovementCommand : BaseCommand
    {
        private MovementCode code;

        public MovementCommand(string text, GameSnapshot gss) : base(text, gss)
        {
        }

        // TODO: AI - team
        protected override void ConvertTextToCode()
        {
            var rand = new Random();
            var randNum = rand.Next(1, 6);

            code = randNum switch
            {
                1 => new MovementCode("M", RowPos.Front, 2, ".", 3),
                2 => new MovementCode("P", null, 2, "=", 5),
                3 => new MovementCode("G", null, 3, "+", 4),
                4 => new MovementCode("X", null, 1, "+", 7),
                _ => throw new Exception("Command cannot be parsed")
            };
        }

        protected override void ExecuteCommand()
        {
            var chessPieces = FindPossibleTargetChessPieces();

            // foreach
        }

        private List<ChessPiece> FindPossibleTargetChessPieces()
        {
            var possibleCps = new List<ChessPiece>();

            for (var i = 1; i <= Constant.BoardRows; ++i)
                if (gss.chessboard[i, code.to] != null)
                {
                    var chessPiece = gss.chessboard[i, code.to];
                    if (chessPiece.type == code.chessType && chessPiece.side == gss.SideTurn)
                        possibleCps.Add(chessPiece);
                }


            if (possibleCps.Count == 0) throw new Exception("No chess piece found");
            if (possibleCps.Count == 1) return possibleCps;

            // TODO: handle g, tg, sg cases
            if (code.rowPos == null)
                return possibleCps;

            if (code.rowPos == RowPos.Front)
                return gss.SideTurn == Side.Red
                    ? new List<ChessPiece> { possibleCps[1] }
                    : new List<ChessPiece> { possibleCps[0] };

            return gss.SideTurn == Side.Red
                ? new List<ChessPiece> { possibleCps[0] }
                : new List<ChessPiece> { possibleCps[1] };
        }

        public class MovementCode
        {
            public string chessType;
            public string dir;
            public int from;
            public string rowPos;
            public int to;

            public MovementCode(string chessType, string rowPos, int from, string dir, int to)
            {
                // TODO: validate input
                this.chessType = chessType;
                this.from = from;
                this.dir = dir;
                this.to = to;
            }
        }
    }
}