using UnityEngine;


namespace Xiangqi.Game
{
    using ChessPiece;
    public class GameManager : MonoBehaviour
    {
        // save entire game state that's not unity related
        public ChessPiece[,] chessPieces = new ChessPiece[9, 10];
    }
}
