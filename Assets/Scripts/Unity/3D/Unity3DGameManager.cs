using UnityEngine;
using Xiangqi.ChessPiece;
using Xiangqi.Enum;
using Xiangqi.Movement;

public class Unity3DGameManager : MonoBehaviour
{
    public GameObject selected;
    public GameObject[] chessPiecePrefabs;

    private void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Select(GameObject obj)
    {
        selected = obj;
    }

    public void LoadGame()
    {
        // TODO: load data from file, remove mock later
        MockGameState();
    }

    private void MockGameState()
    {
        var rook = Instantiate(chessPiecePrefabs[0], GameObject.Find("ChineseChess").transform);
        var comp = rook.GetComponent<ChessPiece>();
        comp.Cell = new Cell(1, 1);
        comp.Boundary = Boundary.Full;
        comp.Side = Side.Red;
    }
}