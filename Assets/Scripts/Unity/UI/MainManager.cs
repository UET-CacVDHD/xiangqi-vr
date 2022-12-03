using TMPro;
using UnityEngine;
using Xiangqi.Enum;

public class MainManager : MonoBehaviour
{
    public TextMeshProUGUI sideTurnText;
    public TextMeshProUGUI gameStateText;

    private string _prevGameState;
    private string _prevSideTurn;

    private void Update()
    {
        var currentSideTurn = Unity3DGameManager.Instance.gameSnapshot.SideTurn;
        if (_prevSideTurn != currentSideTurn)
        {
            UpdateTurnText(currentSideTurn);
            _prevSideTurn = currentSideTurn;
        }

        var currentGameState = Unity3DGameManager.Instance.gameSnapshot.State;
        if (_prevGameState != currentGameState)
        {
            UpdateGameStateText(currentGameState, currentSideTurn);
            _prevGameState = currentGameState;
        }
    }

    private void UpdateTurnText(string sideTurn)
    {
        sideTurnText.text = "Turn: " + sideTurn;
        sideTurnText.color = sideTurn == Side.Red ? Color.red : Color.black;
    }

    private void UpdateGameStateText(string gameState, string sideTurn)
    {
        if (gameState != GameState.Playing)
        {
            gameStateText.gameObject.SetActive(true);
            gameStateText.text = gameState switch
            {
                GameState.Checkmate => "Check Mate",
                GameState.GameOver => sideTurn == Side.Red ? "Black Wins" : "Red Wins",
                _ => gameStateText.text
            };
            gameStateText.color = sideTurn == Side.Red ? Color.black : Color.red;
        }
        else
        {
            gameStateText.gameObject.SetActive(false);
        }
    }

    public void HandleSaveBtnClick()
    {
        // Unity3DGameManager.Instance.SaveGame();
        // TEST
        Unity3DGameManager.Instance.LoadGame();
    }
}