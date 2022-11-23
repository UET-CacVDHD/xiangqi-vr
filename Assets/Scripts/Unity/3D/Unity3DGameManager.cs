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
    public ChessPieceSideTypePrefab[] chessPieceSideTypePrefabs;

    private GameSnapshot _gameSnapshot;
    private Dictionary<string, GameObject> _sideTypePrefabMap;

    public void SaveGame()
    {
        Debug.Log("Saving game");
        _gameSnapshot.SaveToFile();
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");

        var json = File.ReadAllText(Constants.SaveFilePath);
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
        var chessPieceContainer = GameObject.Find("ChessPieces").transform;
        foreach (var data in _gameSnapshot.chessPieceStoredDataList)
        {
            var chessPiece = Instantiate(_sideTypePrefabMap[data.side + data.type], chessPieceContainer);
            var chessPieceBehavior = chessPiece.GetComponent<ChessPiece>();
            chessPieceBehavior.aCell = new AbsoluteCell(data.absoluteCell);
            chessPieceBehavior.side = data.side;
            chessPieceBehavior.type = data.type;
            _gameSnapshot.chessboard[data.absoluteCell.row, data.absoluteCell.col] = chessPieceBehavior;
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