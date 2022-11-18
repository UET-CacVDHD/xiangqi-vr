using UnityEngine;

public class Unity3dGameManager : MonoBehaviour
{
    public GameObject selected;


    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Select(GameObject obj)
    {
        selected = obj;
    }
}