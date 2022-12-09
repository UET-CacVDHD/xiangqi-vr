using TMPro;
using Unity._3D;
using UnityEngine;
using Xiangqi.Enum;

namespace Unity.UI
{
    public class MainManager : MonoBehaviour
    {
        public TextMeshProUGUI sideTurnText;
        public TextMeshProUGUI gameStateText;
        public TextMeshProUGUI titleAlertText;
        public TextMeshProUGUI subtitleAlertText;

        private AudioManager _audioManager;

        private string _prevGameState;
        private string _prevSideTurn;

        private void Start()
        {
            _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        private void Update()
        {
            var currentSideTurn = Unity3DGameManager.instance.gameSnapshot.SideTurn;
            if (_prevSideTurn != currentSideTurn)
            {
                UpdateTurnText(currentSideTurn);
                _prevSideTurn = currentSideTurn;

                _audioManager.PlayChessPieceSmashSound();
                Invoke(nameof(PlayTurnSound), 0.1f);
            }

            var currentGameState = Unity3DGameManager.instance.gameSnapshot.State;
            if (_prevGameState != currentGameState)
            {
                if (currentGameState == GameState.Checkmate)
                    _audioManager.PlayCheckSound();
                else if (currentGameState == GameState.GameOver)
                    _audioManager.PlayCongratSound(currentSideTurn == Side.Red ? Side.Black : Side.Red);
                UpdateGameStateText(currentGameState, currentSideTurn);
                _prevGameState = currentGameState;
            }
        }

        private void PlayTurnSound()
        {
            _audioManager.PlayTurnSound(Unity3DGameManager.instance.gameSnapshot.SideTurn);
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

        public void UpdateAlertTitleText(string content)
        {
            titleAlertText.text = content;
        }

        public void UpdateAlertSubTitleText(string content)
        {
            subtitleAlertText.text = content;
        }

        public void HandleSaveBtnClick()
        {
            Unity3DGameManager.instance.SaveGame();
        }
    }
}