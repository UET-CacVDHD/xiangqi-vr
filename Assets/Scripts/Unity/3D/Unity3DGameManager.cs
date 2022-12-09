using System;
using System.Collections;
using System.Collections.Generic;
using Unity._3D;
using Unity.Enum;
using Unity.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Xiangqi.Game;
using Xiangqi.Util;

public class Unity3DGameManager : MonoBehaviour
{
    public static Unity3DGameManager instance;
    public ChessPieceSideTypePrefab[] chessPieceSideTypePrefabs;

    [FormerlySerializedAs("_gameSnapshot")]
    public GameSnapshot gameSnapshot;

    public string currentSideTurn;

    private Dictionary<string, GameObject> _sideTypePrefabMap;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        currentSideTurn = gameSnapshot.SideTurn;
    }

    private void Update()
    {
        if (currentSideTurn != gameSnapshot.SideTurn) currentSideTurn = gameSnapshot.SideTurn;
    }

    public void SaveGame()
    {
        Debug.Log("Saving game");
        gameSnapshot.SaveToFile();
        StartCoroutine(LoadScene(SceneIdx.Menu));
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");
        gameSnapshot = GameSnapshot.LoadFromFile(Constant.StoredGamePath);
        StartCoroutine(LoadScene(SceneIdx.Main));
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game");
        gameSnapshot = GameSnapshot.LoadFromFile(Constant.NewGamePath);
        StartCoroutine(LoadScene(SceneIdx.Main));
    }

    public void UpdateTitleAlert(string content)
    {
        var comp = GameObject.Find("MainManager").GetComponent<MainManager>();
        comp.UpdateAlertTitleText(content);
    }

    public void UpdateSubTitleAlert(string content)
    {
        var comp = GameObject.Find("MainManager").GetComponent<MainManager>();
        comp.UpdateAlertSubTitleText(content);
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
            if (gameSnapshot.chessboard[i, j] == null) continue;

            var chessPiece = gameSnapshot.chessboard[i, j];
            var chessPieceBehavior =
                Instantiate(_sideTypePrefabMap[chessPiece.side + chessPiece.type], chessPieceContainer)
                    .GetComponent<ChessPieceBehavior>();

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