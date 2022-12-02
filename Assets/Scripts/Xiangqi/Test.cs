using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button testBtn;

    private void Start()
    {
        // var btn = gameObject.GetComponent<Button>();
        // btn.onClick.AddListener(TestButton);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TestButton()
    {
        Debug.Log("testing button");
    }
}