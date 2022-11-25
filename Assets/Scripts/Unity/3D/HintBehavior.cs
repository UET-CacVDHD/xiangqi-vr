using UnityEngine;
using Xiangqi.Motion.Cell;

public class HintBehavior : MonoBehaviour
{
    private CoordinateManager _coordinateManager;
    private bool _isEnabled;
    private AbsoluteCell _position;

    private void Start()
    {
        _coordinateManager = GameObject.Find("CoordinateManager").GetComponent<CoordinateManager>();
    }

    private void OnMouseOver()
    {
        if (!_isEnabled) return;
    }

    private void OnMouseUpAsButton()
    {
        if (!_isEnabled) return;

        _coordinateManager.MoveTo(_position);
    }


    public void SetPosition(AbsoluteCell position)
    {
        _position = position;
    }

    public void ToggleHint(bool isEnabled)
    {
        _isEnabled = isEnabled;
        GetComponent<MeshRenderer>().enabled = isEnabled;
        GetComponent<BoxCollider>().enabled = isEnabled;
    }
}