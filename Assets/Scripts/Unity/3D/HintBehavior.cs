using UnityEngine;
using Xiangqi.Movement.Cell;

public class HintBehavior : MonoBehaviour
{
    public CoordinateManager manager;
    private bool _isEnabled;
    private AbsoluteCell _position;

    // TODO: debug hint indicator doesn't receive event.
    private void OnMouseDown()
    {
        if (!_isEnabled) return;
        Debug.Log(_position);
        CoordinateManager.Instance.chosenChessPiece.MoveTo(_position);
        // send to CoordinateManager its position
    }

    private void OnMouseOver()
    {
        if (!_isEnabled) return;
    }


    public void SetPosition(AbsoluteCell position)
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