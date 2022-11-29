using System;
using System.Collections.Generic;
using Unity._3D;
using UnityEngine;
using Xiangqi.Command;
using Xiangqi.Enum;
using Xiangqi.Game;
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
        _gameSnapshot = GameSnapshot.LoadFromFile(Constants.StoredGamePath);

        InitTypeSidePrefabMap();
        InstantiateChessPiece();
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game");
        _gameSnapshot = GameSnapshot.LoadFromFile(Constants.NewGamePath);

        InitTypeSidePrefabMap();
        InstantiateChessPiece();

        try
        {
            var cmd = CommandParser.CreateCommand("pháo 2 bình 5", _gameSnapshot);
            cmd?.HandleCommand();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
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


        for (var i = 1; i <= Constants.BoardRows; i++)
        for (var j = 1; j <= Constants.BoardCols; j++)
        {
            if (_gameSnapshot.chessboard[i, j] == null) continue;

            var chessPiece = _gameSnapshot.chessboard[i, j];
            var chessPieceBehavior =
                Instantiate(_sideTypePrefabMap[chessPiece.side + chessPiece.type], chessPieceContainer)
                    .GetComponent<ChessPieceBehavior>();

            if (chessPiece.side == Side.Black)
                chessPieceBehavior.transform.Rotate(180, 0, 0);

            chessPieceBehavior.Cp = chessPiece;
        }
    }

    [Serializable] // use serializable to make the class visible in Unity editor
    public class ChessPieceSideTypePrefab
    {
        public GameObject prefab;
        public string sideType;
    }
}