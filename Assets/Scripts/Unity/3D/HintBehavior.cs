using UnityEngine;
using Xiangqi.Util;

public class HintBehavior : MonoBehaviour
{
    public CoordinateManager manager;
    private bool _isEnabled;
    private Cell _position;

    private void OnMouseDown()
    {
        if (!_isEnabled) return;

        // send to CoordinateManager its position

    }

    private void OnMouseOver()
    {
        if (!_isEnabled) return;
    }


    public void SetPosition(Cell position)
    {
        _position = position;
    }

    public void ToggleHint(bool isEnabled)
    {
        _isEnabled = isEnabled;
        GetComponent<MeshRenderer>().enabled = isEnabled;
        GetComponent<Collider>().enabled = isEnabled;
    }
}