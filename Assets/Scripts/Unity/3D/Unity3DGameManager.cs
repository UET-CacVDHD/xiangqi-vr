using UnityEngine;
using UnityEngine.UI;
using Xiangqi.ChessPiece;
using Xiangqi.Enum;
using Xiangqi.Movement;

public class Unity3DGameManager : MonoBehaviour
{
    public GameObject selected;
    public GameObject[] chessPiecePrefabs;
    public Button saveButton;
    public Button loadButton;

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

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {
        // TODO: remove mock func by loading data from file
        MockGameState();
    }

    private void MockGameState()
    {
        var rRook = Instantiate(chessPiecePrefabs[0], GameObject.Find("ChineseChess").transform);
        var comp = rRook.GetComponent<ChessPiece>();
        comp.Cell = new Cell(1, 1);
        comp.Boundary = Boundary.Full;
        comp.Side = Side.Red;

        // var bRook = Instantiate(chessPiecePrefabs[1], GameObject.Find("ChineseChess").transform);
        // var comp = bRook.GetComponent<ChessPiece>();
        // comp.Cell = new Cell(10, 9);
        // comp.Boundary = Boundary.Full;
        // comp.Side = Side.Black;

        // var rHorse = Instantiate(chessPiecePrefabs[2], GameObject.Find("ChineseChess").transform);
        // comp = rHorse.GetComponent<ChessPiece>();
        // comp.Cell = new Cell(1, 2);
        // comp.Boundary = Boundary.Full;
        // comp.Side = Side.Red;
    }
}