using System;
using System.Linq;
using Unity.XR;
using UnityEngine;
using Xiangqi.ChessPieceLogic;

namespace Unity._3D
{
    [Serializable]
    public class ChessPieceBehavior : MonoBehaviour
    {
        public ParticleSystem explosionParticle;
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
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                var pieceDistanceToBase = Vector3.Distance(transform.position,
                    _coordinateManager.GetCoordinateFromChessboardCell(cp.aCell));
                if (pieceDistanceToBase > 0.05)
                    transform.position = _coordinateManager.GetCoordinateFromChessboardCell(cp.aCell);
            }
        }

        public void OnMouseUpAsButton()
        {
            Select();
        }

        public void Select()
        {
            Debug.Log("Selected");
            if (cp.side != cp.gss.SideTurn)
                return;
            _coordinateManager.SetChosenChessPiece(this);
            _coordinateManager.ShowHintIndicatorsAtCells(cp.GetMovableAndNotLeadToGameOverCells());
        }

        public void Deselect()
        {
            _coordinateManager.SetChosenChessPiece(null);
        }

        public void XROnDeselect()
        {
            var positionSelected = GameObject
                .FindGameObjectsWithTag("VRHand")
                .First(obj => obj.GetComponent<HandManager>().IsActive())
                .GetComponent<HandManager>()
                .GetRayCastHit();

            if (positionSelected != null)
                positionSelected.GetComponent<HintBehavior>().Select();

            Deselect();
        }
    }
}