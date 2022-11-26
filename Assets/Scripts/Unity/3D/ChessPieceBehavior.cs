using System;
using UnityEngine;
using Xiangqi.ChessPieceLogic;

namespace Unity._3D.ChessPieceBehavior
{
    [Serializable]
    public class ChessPieceBehavior : MonoBehaviour
    {
        private CoordinateManager _coordinateManager;
        protected ChessPiece cp;

        public ChessPiece Cp
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
            if (cp.isDead)
                Destroy(gameObject);
            else transform.position = _coordinateManager.GetCoordinateFromChessboardCell(cp.aCell);
        }

        public void OnMouseUpAsButton()
        {
            if (cp.side != cp.gss.SideTurn)
                return;
            _coordinateManager.SetChosenChessPiece(this);
            _coordinateManager.ShowHintIndicatorsAtCells(cp.GetMovableAndNotLeadToGameOverCells());
        }
    }
}