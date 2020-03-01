using UnityEngine;

namespace Components.SetterComponents
{
    public class CanvasMainCameraSetterComponent : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}
