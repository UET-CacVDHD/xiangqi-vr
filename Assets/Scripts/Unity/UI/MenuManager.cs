using UnityEngine;

namespace Unity.UI
{
    public class MenuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        public void HandleLoadBtnClick()
        {
            Unity3DGameManager.instance.LoadGame();
        }

        public void HandleNewGameBtnClick()
        {
            Unity3DGameManager.instance.RestartGame();
        }
    }
}