using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    public void HandleLoadBtnClick()
    {
        Unity3DGameManager.Instance.LoadGame();
    }

    public void HandleNewGameBtnClick()
    {
        Unity3DGameManager.Instance.RestartGame();
    }
}