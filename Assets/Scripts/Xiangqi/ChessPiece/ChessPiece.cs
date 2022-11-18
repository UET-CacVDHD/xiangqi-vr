using Xiangqi.Util;
using UnityEngine;
using Xiangqi.Enum;

namespace Xiangqi.ChessPiece
{
    public abstract class ChessPiece : MonoBehaviour
    {
        protected Cell cell;
        protected Boundary boundary;
        protected Side side;
        protected bool isDeath;

        protected ChessPiece(Cell cell, Boundary boundary, Side side, bool isDeath)
        {
            this.cell = cell;
            this.boundary = boundary;
            this.side = side;
            this.isDeath = isDeath;
        }

        // Update is called once per frame
        void Update()
        {
            // transform.position = CoordinateManager.GetCoordinateFromChessboardCell(cell);
        }

        private void OnMouseDown()
        {
            Debug.Log(gameObject + "is clicked");
            ShowMovableCell();
        }

        protected abstract void ShowMovableCell();
    }
}

