using System;
using System.Collections;
using System.Collections.Generic;
using Unity._3D;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xiangqi.Enum;
using Xiangqi.Game;
using Xiangqi.Util;

public class Unity3DGameManager : MonoBehaviour
{
    public static Unity3DGameManager Instance;
    public ChessPieceSideTypePrefab[] chessPieceSideTypePrefabs;
    private GameSnapshot _gameSnapshot;
    private Dictionary<string, GameObject> _sideTypePrefabMap;

    private void Start()
    {
        // var res = TestParser.Expression.Parse("std:T3+2");
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        Debug.Log("Saving game");
        _gameSnapshot.SaveToFile();
        StartCoroutine(LoadScene(SceneIdx.Menu));
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");
        _gameSnapshot = GameSnapshot.LoadFromFile(Constant.StoredGamePath);
        StartCoroutine(LoadScene(SceneIdx.Main));
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game");
        _gameSnapshot = GameSnapshot.LoadFromFile(Constant.NewGamePath);
        StartCoroutine(LoadScene(SceneIdx.Main));
    }

    private IEnumerator LoadScene(int sceneIdx)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneIdx);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) yield return null;

        if (sceneIdx != SceneIdx.Main) yield break;
        InitTypeSidePrefabMap();
        InstantiateChessPiece();
    }

    private void InitTypeSidePrefabMap()
    {
        if (_sideTypePrefabMap != null) return;

        _sideTypePrefabMap = new Dictionary<string, GameObject>();
        foreach (var item in chessPieceSideTypePrefabs)
            _sideTypePrefabMap.Add(item.sideType, item.prefab);
    }

    private void InstantiateChessPiece()
    {
        var chessPieceContainer = GameObject.Find("ChessPieces").transform;

        for (var i = 1; i <= Constant.BoardRows; i++)
        for (var j = 1; j <= Constant.BoardCols; j++)
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