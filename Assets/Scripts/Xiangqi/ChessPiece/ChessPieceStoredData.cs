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
        public bool isDead;
        public string side;
        public string type;

        public ChessPieceStoredData(AbsoluteCell absoluteCell, bool isDead, string side, string type)
        {
            this.absoluteCell = absoluteCell;
            this.isDead = isDead;
            this.side = side;
            this.type = type;
        }

        public override string ToString()
        {
            return $"Cell: {absoluteCell}, isDead: {isDead}, side: {side}, type: {type}";
        }
    }
}