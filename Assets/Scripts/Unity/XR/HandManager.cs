using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.XR
{
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

        private void ToggleActive(bool isActive)
        {
            GetComponent<LineRenderer>().enabled = isActive;
            GetComponent<XRRayInteractor>().enabled = isActive;
            GetComponent<XRInteractorLineVisual>().enabled = isActive;
        }

        public bool IsActive()
        {
            return _handSelected == gameObject;
        }

        public GameObject GetRayCastHit()
        {
            if (!GetComponent<XRRayInteractor>().enabled)
                return null;

            GetComponent<XRRayInteractor>().TryGetCurrent3DRaycastHit(out var hit);

            return hit.transform.gameObject.layer != LayerMask.NameToLayer("Hint") ? null : hit.transform.gameObject;
        }
    }
}