using System;
using System.Collections.Generic;
using UnityEngine;
using Xiangqi.ChessPiece;
using Xiangqi.Movement.Cell;
using Xiangqi.Util;

public class CoordinateManager : MonoBehaviour
{
    public static CoordinateManager Instance;
    public GameObject HBase;
    public GameObject Hx;
    public GameObject Hy;
    public GameObject HintIndicator;

    private readonly GameObject[] _hintIndicators = new GameObject[90];

    private Vector3 _offsetColPerUnit;
    private Vector3 _offsetRowPerUnit;

    [NonSerialized] public ChessPiece chosenChessPiece;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CalculateUnitVectors();
        InstantiateHintIndicators();
    }

    private void CalculateUnitVectors()
    {
        var HBaseVec = HBase.transform.position;
        var HxVec = Hx.transform.position;
        var HyVec = Hy.transform.position;

        _offsetColPerUnit = (HBaseVec - HxVec).magnitude * (HxVec - HBaseVec).normalized;
        _offsetRowPerUnit = (HBaseVec - HyVec).magnitude * (HyVec - HBaseVec).normalized;
    }

    private void InstantiateHintIndicators()
    {
        for (var i = 0; i < Constant.BoardRows; i++)
        for (var j = 0; j < Constant.BoardCols; j++)
        {
            var hintIndicator = Instantiate(HintIndicator);
            _hintIndicators[i * Constant.BoardCols + j] = hintIndicator;
            var position = new AbsoluteCell(i + 1, j + 1);
            hintIndicator.transform.position = GetCoordinateFromChessboardCell(position);
            hintIndicator.GetComponent<HintBehavior>().SetPosition(position);
        }

        DisableAllHintIndicators();
    }

    public Vector3 GetCoordinateFromChessboardCell(AbsoluteCell absoluteCell)
    {
        var hBaseVec = HBase.transform.position;
        return hBaseVec + _offsetRowPerUnit * (absoluteCell.row - 1) + _offsetColPerUnit * (absoluteCell.col - 1);
    }

    public void ShowHintIndicatorForAChessPiece(List<AbsoluteCell> cells)
    {
        DisableAllHintIndicators();
        foreach (var cell in cells) ToggleHintIndicator(cell, true);
    }

    private void DisableAllHintIndicators()
    {
        for (var i = 0; i < Constant.BoardRows; i++)
        for (var j = 0; j < Constant.BoardCols; j++)
            ToggleHintIndicator(new AbsoluteCell(i + 1, j + 1), false);
    }

    private void ToggleHintIndicator(AbsoluteCell absoluteCell, bool isEnabled)
    {
        _hintIndicators[(absoluteCell.row - 1) * Constant.BoardCols + absoluteCell.col - 1].GetComponent<HintBehavior>()
            .ToggleHint(isEnabled);
    }

    public void MoveTo(AbsoluteCell absoluteCell)
    {
        if (chosenChessPiece == null) return;

        chosenChessPiece.MoveTo(absoluteCell);
        chosenChessPiece = null;
        DisableAllHintIndicators();
    }
}