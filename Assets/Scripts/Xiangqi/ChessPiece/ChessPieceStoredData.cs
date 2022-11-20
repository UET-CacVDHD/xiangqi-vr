using System;
using Xiangqi.Movement;

namespace Xiangqi.ChessPiece
{
    // This class is used to serialize, deserialize information of a chess piece
    // Unable to use the ChessPiece class directly for 2 reasons:
    // It is not allowed to serialize List<ChessPiece> - Dhs
    // ChessPiece inherit MonoBehaviour, it can't have a constructor to create new instance when deserializing
    [Serializable]
    public class ChessPieceStoredData
    {
        public Cell cell;
        public bool isDeath;
        public string side;
        public string type;

        public ChessPieceStoredData(Cell cell, bool isDeath, string side, string type)
        {
            this.cell = cell;
            this.isDeath = isDeath;
            this.side = side;
            this.type = type;
        }

        public override string ToString()
        {
            return "Cell: " + cell + " isDeath: " + isDeath + " side: " + side + " type: " + type;
        }
    }
}