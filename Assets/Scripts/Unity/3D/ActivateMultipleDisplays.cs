using UnityEngine;

namespace Unity._3D
{
    public class ActivateMultipleDisplays : MonoBehaviour
    {
        private void Start()
        {
            // VR display
            Display.displays[1].Activate();
        }
    }
}