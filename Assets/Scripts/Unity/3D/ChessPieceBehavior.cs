using System;
using UnityEngine;

namespace Unity._3D.ChessPieceBehavior
{
    [Serializable]
    public class ChessPieceBehavior : MonoBehaviour
    {
        private CoordinateManager _coordinateManager;
        protected Xiangqi.ChessPieceLogic.ChessPiece cp;

        public Xiangqi.ChessPieceLogic.ChessPiece Cp
        {
            get => cp;
            set => cp = value;
        }

        protected virtual void Start()
        {
            _coordinateManager = GameObject.Find("CoordinateManager").GetComponent<CoordinateManager>();
        }

        private void Update()
        {
            transform.position = _coordinateManager.GetCoordinateFromChessboardCell(cp.aCell);
        }

        public void OnMouseUpAsButton()
        {
            _coordinateManager.SetChosenChessPiece(this);
            cp.UpdateMovableCells();
            _coordinateManager.ShowHintIndicatorsAtCells(cp.GetMovableCells());
        }
    }
}