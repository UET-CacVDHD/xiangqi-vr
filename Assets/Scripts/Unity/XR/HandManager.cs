using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandManager : MonoBehaviour
{
    private static GameObject _handSelected;

    private void Start()
    {
        if (_handSelected == null) _handSelected = GameObject.FindWithTag("VRHand");

        var handSelectedIsThisObject = _handSelected == gameObject;

        if (!handSelectedIsThisObject) ToggleActive(false);
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        SelectHand();
    }

    public void SelectHand()
    {
        if (gameObject == _handSelected) return;

        ToggleActive(true);
        _handSelected.GetComponent<HandManager>().ToggleActive(false);
        _handSelected = transform.gameObject;
    }

    public void ToggleActive(bool isActive)
    {
        GetComponent<LineRenderer>().enabled = isActive;
        GetComponent<XRRayInteractor>().enabled = isActive;
        GetComponent<XRInteractorLineVisual>().enabled = isActive;
    }
}