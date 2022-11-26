using System;
using System.Collections.Generic;
using System.IO;
using Unity._3D.ChessPieceBehavior;
using UnityEngine;
using Xiangqi.ChessPieceLogic;
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

        var json = File.ReadAllText(Constants.SaveFilePath);
        _gameSnapshot = JsonUtility.FromJson<GameSnapshot>(json);
        // TODO: search - provide default value of class field vs define in constructor
        _gameSnapshot.chessboard = new ChessPiece[Constants.BoardRows + 1, Constants.BoardCols + 1];

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
        foreach (var piece in _gameSnapshot.chessPieces)
        {
            var chessPieceBehavior = Instantiate(_sideTypePrefabMap[piece.side + piece.type], chessPieceContainer)
                .GetComponent<ChessPieceBehavior>();

            chessPieceBehavior.Cp = piece.type switch
            {
                ChessType.Rook => new Rook(piece.aCell, piece.isDead, piece.side, piece.type, _gameSnapshot),
                ChessType.Horse => new Horse(piece.aCell, piece.isDead, piece.side, piece.type,
                    _gameSnapshot),
                ChessType.Elephant => new Elephant(piece.aCell, piece.isDead, piece.side, piece.type,
                    _gameSnapshot),
                ChessType.Advisor => new Advisor(piece.aCell, piece.isDead, piece.side, piece.type,
                    _gameSnapshot),
                ChessType.General => new General(piece.aCell, piece.isDead, piece.side, piece.type,
                    _gameSnapshot),
                ChessType.Cannon => new Cannon(piece.aCell, piece.isDead, piece.side, piece.type,
                    _gameSnapshot),
                _ => new Soldier(piece.aCell, piece.isDead, piece.side, piece.type, _gameSnapshot)
            };
            chessPieceBehavior.Cp.gss = _gameSnapshot;
            _gameSnapshot.chessboard[piece.aCell.row, piece.aCell.col] = chessPieceBehavior.Cp;
        }
    }

    [Serializable] // use serializable to make the class visible in Unity editor
    public class ChessPieceSideTypePrefab
    {
        public GameObject prefab;
        public string sideType;
    }
}