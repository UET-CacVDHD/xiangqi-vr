using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Xiangqi.ChessPiece;
using Xiangqi.Game;
using Xiangqi.Movement.Cell;
using Xiangqi.Util;

public class Unity3DGameManager : MonoBehaviour
{
    public GameObject selected;
    public ChessPieceSideTypePrefab[] chessPieceSideTypePrefabs;

    private GameSnapshot _gameSnapshot;
    private Dictionary<string, GameObject> _sideTypePrefabMap;

    public void Select(GameObject obj)
    {
        selected = obj;
    }

    public void SaveGame()
    {
        Debug.Log("Saving game");
        _gameSnapshot.SaveToFile();
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");

        var json = File.ReadAllText(Constant.SaveFilePath);
        _gameSnapshot = JsonUtility.FromJson<GameSnapshot>(json);

        InitTypeSidePrefabMap();
        InstantiateChessPiece();
    }

    private void InitTypeSidePrefabMap()
    {
        _sideTypePrefabMap = new Dictionary<string, GameObject>();
        foreach (var item in chessPieceSideTypePrefabs)
            _sideTypePrefabMap.Add(item.sideType, item.prefab);
    }

    private void InstantiateChessPiece()
    {
        foreach (var data in _gameSnapshot.chessPieceStoredDataList)
        {
            Debug.Log(data);

            var chessPiece = Instantiate(_sideTypePrefabMap[data.side + data.type],
                GameObject.Find("ChineseChess").transform);
            var behavior = chessPiece.GetComponent<ChessPiece>();
            behavior.aCell = new AbsoluteCell(data.absoluteCell);
            behavior.side = data.side;
            behavior.type = data.type;
            _gameSnapshot.chessboard[data.absoluteCell.row, data.absoluteCell.col] = behavior;
        }

        ChessPiece.chessboard = _gameSnapshot.chessboard;
    }

    [Serializable]
    public class ChessPieceSideTypePrefab
    {
        public GameObject prefab;
        public string sideType;
    }
}