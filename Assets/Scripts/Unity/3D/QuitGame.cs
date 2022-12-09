using UnityEditor;
using UnityEngine;

namespace Unity._3D
{
    public class QuitGame : MonoBehaviour
    {
        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif

            Application.Quit();
        }
    }
}