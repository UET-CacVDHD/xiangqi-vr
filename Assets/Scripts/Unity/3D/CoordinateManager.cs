using System.Collections.Generic;
using UnityEngine;
using Xiangqi.ChessPiece;
using Xiangqi.Movement;
using Xiangqi.Util;

public class CoordinateManager : MonoBehaviour
{
    public static CoordinateManager Instance;
    public GameObject HBase;
    public GameObject Hx;
    public GameObject Hy;
    public GameObject HintIndicator;

    public ChessPiece chosenChessPiece;
    private readonly GameObject[] _hintIndicators = new GameObject[90];

    private Vector3 _offsetColPerUnit;
    private Vector3 _offsetRowPerUnit;

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
            var position = new Cell(i + 1, j + 1);
            hintIndicator.transform.position = GetCoordinateFromChessboardCell(position);
            hintIndicator.GetComponent<HintBehavior>().SetPosition(position);
        }

        DisableAllHintIndicators();
    }

    public Vector3 GetCoordinateFromChessboardCell(Cell cell)
    {
        var hBaseVec = HBase.transform.position;
        return hBaseVec + _offsetRowPerUnit * (cell.row - 1) + _offsetColPerUnit * (cell.col - 1);
    }

    public void ShowHintIndicatorForAChessPiece(List<Cell> cells)
    {
        DisableAllHintIndicators();
        foreach (var cell in cells) ToggleHintIndicator(cell, true);
    }

    private void DisableAllHintIndicators()
    {
        for (var i = 0; i < Constant.BoardRows; i++)
        for (var j = 0; j < Constant.BoardCols; j++)
            _hintIndicators[i * 9 + j].GetComponent<HintBehavior>().ToggleHint(false);
    }

    private void ToggleHintIndicator(Cell cell, bool isEnabled)
    {
        Debug.Log(cell);
        _hintIndicators[(cell.row - 1) * Constant.BoardCols + cell.col - 1].GetComponent<MeshRenderer>().enabled =
            isEnabled;
        _hintIndicators[(cell.row - 1) * Constant.BoardCols + cell.col - 1].GetComponent<Collider>().enabled =
            isEnabled;
    }
}