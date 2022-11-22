using System;
using UnityEngine.Serialization;
using Xiangqi.Movement.Cell;

namespace Xiangqi.ChessPiece
{
    // This class is used to serialize, deserialize information of a chess piece
    // Unable to use the ChessPiece class directly for 2 reasons:
    // It is not allowed to serialize List<ChessPiece> - Dhs
    // ChessPiece inherit MonoBehaviour, it can't have a constructor to create new instance when deserializing
    [Serializable]
    public class ChessPieceStoredData
    {
        [FormerlySerializedAs("cell")] public AbsoluteCell absoluteCell;
        public bool isDeath;
        public string side;
        public string type;

        public ChessPieceStoredData(AbsoluteCell absoluteCell, bool isDeath, string side, string type)
        {
            this.absoluteCell = absoluteCell;
            this.isDeath = isDeath;
            this.side = side;
            this.type = type;
        }

        public override string ToString()
        {
            return "Cell: " + absoluteCell + " isDeath: " + isDeath + " side: " + side + " type: " + type;
        }
    }
}