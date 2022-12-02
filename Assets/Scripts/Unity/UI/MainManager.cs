using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    public void HandleSaveBtnClick()
    {
        Unity3DGameManager.Instance.SaveGame();
    }
}