using System.Collections.Generic;
using Unity._3D.ChessPieceBehavior;
using UnityEngine;
using Xiangqi.Motion.Cell;
using Xiangqi.Util;

public class CoordinateManager : MonoBehaviour
{
    public GameObject anchorBase;
    public GameObject anchorX;
    public GameObject anchorY;
    public GameObject hintIndicatorPrefab;

    private readonly GameObject[] _hintIndicators = new GameObject[90];

    private ChessPieceBehavior _chosenChessPiece;

    private Vector3 _offsetColPerUnit;
    private Vector3 _offsetRowPerUnit;

    private void Start()
    {
        CalculateUnitVectors();
        InstantiateHintIndicators();
    }

    private void CalculateUnitVectors()
    {
        var baseVec = anchorBase.transform.position;
        var xVec = anchorX.transform.position;
        var yVec = anchorY.transform.position;

        _offsetColPerUnit = (baseVec - xVec).magnitude * (xVec - baseVec).normalized;
        _offsetRowPerUnit = (baseVec - yVec).magnitude * (yVec - baseVec).normalized;
    }

    private void InstantiateHintIndicators()
    {
        var hintIndicatorContainer = GameObject.Find("HintIndicators").transform;

        for (var i = 0; i < Constants.BoardRows; i++)
        for (var j = 0; j < Constants.BoardCols; j++)
        {
            var hintIndicator = Instantiate(hintIndicatorPrefab, hintIndicatorContainer);
            _hintIndicators[i * Constants.BoardCols + j] = hintIndicator;
            var position = new AbsoluteCell(i + 1, j + 1);
            hintIndicator.transform.position = GetCoordinateFromChessboardCell(position);
            hintIndicator.GetComponent<HintBehavior>().SetPosition(position);
        }

        DisableAllHintIndicators();
    }

    public Vector3 GetCoordinateFromChessboardCell(AbsoluteCell absoluteCell)
    {
        var hBaseVec = anchorBase.transform.position;
        return hBaseVec + _offsetRowPerUnit * (absoluteCell.row - 1) + _offsetColPerUnit * (absoluteCell.col - 1);
    }

    public void ShowHintIndicatorsAtCells(List<AbsoluteCell> cells)
    {
        DisableAllHintIndicators();
        foreach (var cell in cells) ToggleHintIndicator(cell, true);
    }

    private void DisableAllHintIndicators()
    {
        for (var i = 0; i < Constants.BoardRows; i++)
        for (var j = 0; j < Constants.BoardCols; j++)
            ToggleHintIndicator(new AbsoluteCell(i + 1, j + 1), false);
    }

    private void ToggleHintIndicator(AbsoluteCell absoluteCell, bool isEnabled)
    {
        _hintIndicators[(absoluteCell.row - 1) * Constants.BoardCols + absoluteCell.col - 1]
            .GetComponent<HintBehavior>()
            .ToggleHint(isEnabled);
    }

    public void MoveTo(AbsoluteCell absoluteCell)
    {
        if (_chosenChessPiece == null) return;

        _chosenChessPiece.Cp.MoveTo(absoluteCell);
        _chosenChessPiece = null;
        DisableAllHintIndicators();
    }

    public void SetChosenChessPiece(ChessPieceBehavior chessPiece)
    {
        _chosenChessPiece = chessPiece;
    }
}