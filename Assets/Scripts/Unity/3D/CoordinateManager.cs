using UnityEngine;
using Xiangqi.Movement;
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

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        var HBaseVec = HBase.transform.position;
        var HxVec = Hx.transform.position;
        var HyVec = Hy.transform.position;

        _offsetColPerUnit = (HBaseVec - HxVec).magnitude * (HxVec - HBaseVec).normalized;
        _offsetRowPerUnit = (HBaseVec - HyVec).magnitude * (HyVec - HBaseVec).normalized;


        for (var i = 0; i < Constant.BoardRows; i++)
        for (var j = 0; j < Constant.BoardCols; j++)
        {
            var hintIndicator = Instantiate(HintIndicator);
            _hintIndicators[i * Constant.BoardCols + j] = hintIndicator;
            var position = new Cell(i + 1, j + 1);
            hintIndicator.transform.position = GetCoordinateFromChessboardCell(position);
            hintIndicator.GetComponent<HintBehavior>().SetPosition(position);
            // disable collider
            // _hintIndicators[i * 9 + j].GetComponent<HintBehavior>().ToggleHint(false);
        }
    }

    public Vector3 GetCoordinateFromChessboardCell(Cell cell)
    {
        var HBaseVec = HBase.transform.position;
        return HBaseVec + _offsetRowPerUnit * (cell.Row - 1) + _offsetColPerUnit * (cell.Col - 1);
    }

    // public void ToggleHintIndicator(Cell cell, bool isEnabled)
    // {
    //     _hintIndicators[(cell.Row - 1) * Constant.BoardCols + cell.Col - 1].GetComponent<MeshRenderer>().enabled = isEnabled;
    //     _hintIndicators[(cell.Row - 1) * Constant.BoardCols + cell.Col - 1].GetComponent<Collider>().enabled = isEnabled;
    // }
}