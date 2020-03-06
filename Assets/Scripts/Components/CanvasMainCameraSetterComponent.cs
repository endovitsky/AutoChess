using UnityEngine;

namespace Components
{
    public class CanvasMainCameraSetterComponent : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}
