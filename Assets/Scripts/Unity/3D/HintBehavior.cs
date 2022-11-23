using UnityEngine;
using Xiangqi.Movement.Cell;

public class HintBehavior : MonoBehaviour
{
    private CoordinateManager _coordManager;
    private bool _isEnabled;
    private AbsoluteCell _position;

    private void Start()
    {
        _coordManager = GameObject.Find("CoordinateManager").GetComponent<CoordinateManager>();
    }

    private void OnMouseOver()
    {
        if (!_isEnabled) return;
    }

    private void OnMouseUpAsButton()
    {
        if (!_isEnabled) return;

        _coordManager.MoveTo(_position);
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