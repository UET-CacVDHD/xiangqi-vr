using UnityEngine;
using Xiangqi.Movement;

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


        for (var i = 0; i < 10; i++)
        for (var j = 0; j < 9; j++)
        {
            var hintIndicator = Instantiate(HintIndicator);
            _hintIndicators[i * 9 + j] = hintIndicator;
            var position = new Cell(j + 1, i + 1);
            hintIndicator.transform.position = GetCoordinateFromChessboardCell(position);
            hintIndicator.GetComponent<HintBehavior>().SetPosition(position);
            // disable collider
            // _hintIndicators[i * 9 + j].GetComponent<HintBehavior>().ToggleHint(false);
        }
    }

    private void FixedUpdate()
    {
        // var HBaseVec = HBase.transform.position;
        // var HxVec = Hx.transform.position;
        // var HyVec = Hy.transform.position;
        //
        // _offsetColPerUnit = (HBaseVec - HxVec).magnitude * (HxVec - HBaseVec).normalized;
        // _offsetRowPerUnit = (HBaseVec - HyVec).magnitude * (HyVec - HBaseVec).normalized;
        //
        // for (var i = 0; i < 10; i++)
        // for (var j = 0; j < 9; j++)
        //     testArr[i * 9 + j].transform.position = GetCoordinateFromChessboardCell(new Cell(j + 1, i + 1));
    }

    public Vector3 GetCoordinateFromChessboardCell(Cell cell)
    {
        var HBaseVec = HBase.transform.position;
        return HBaseVec + _offsetColPerUnit * (cell.Col - 1) + _offsetRowPerUnit * (cell.Row - 1);
    }

    // public void ToggleHintIndicator(Cell cell, bool isEnabled)
    // {
    //     _hintIndicators[(cell.Row - 1) * 9 + cell.Col - 1].GetComponent<MeshRenderer>().enabled = isEnabled;
    //     _hintIndicators[(cell.Row - 1) * 9 + cell.Col - 1].GetComponent<Collider>().enabled = isEnabled;
    // }
}