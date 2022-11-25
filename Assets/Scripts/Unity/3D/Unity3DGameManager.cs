using System;
using System.Collections.Generic;
using System.IO;
using Unity._3D.ChessPieceBehavior;
using UnityEngine;
using Xiangqi.Enum;
using Xiangqi.Game;
using Xiangqi.Util;
using Advisor = Xiangqi.ChessPieceLogic.Advisor;
using Cannon = Xiangqi.ChessPieceLogic.Cannon;
using Elephant = Xiangqi.ChessPieceLogic.Elephant;
using General = Xiangqi.ChessPieceLogic.General;
using Horse = Xiangqi.ChessPieceLogic.Horse;
using Rook = Xiangqi.ChessPieceLogic.Rook;
using Soldier = Xiangqi.ChessPieceLogic.Soldier;

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
        foreach (var piece in _gameSnapshot.chessPieces)
        {
            var chessPiece = Instantiate(_sideTypePrefabMap[piece.side + piece.type], chessPieceContainer);
            var chessPieceBehavior = chessPiece.GetComponent<ChessPieceBehavior>();

            chessPieceBehavior.Cp = piece.type switch
            {
                ChessType.Rook => new Rook(piece.aCell, piece.isDead, piece.side, piece.type),
                ChessType.Horse => new Horse(piece.aCell, piece.isDead, piece.side, piece.type),
                ChessType.Elephant => new Elephant(piece.aCell, piece.isDead, piece.side, piece.type),
                ChessType.Advisor => new Advisor(piece.aCell, piece.isDead, piece.side, piece.type),
                ChessType.General => new General(piece.aCell, piece.isDead, piece.side, piece.type),
                ChessType.Cannon => new Cannon(piece.aCell, piece.isDead, piece.side, piece.type),
                _ => new Soldier(piece.aCell, piece.isDead, piece.side, piece.type)
            };

            _gameSnapshot.chessboard[piece.aCell.row, piece.aCell.col] = piece;
        }

        Xiangqi.ChessPieceLogic.ChessPiece.chessboard = _gameSnapshot.chessboard;
    }

    [Serializable] // use serializable to make the class visible in Unity editor
    public class ChessPieceSideTypePrefab
    {
        public GameObject prefab;
        public string sideType;
    }
}