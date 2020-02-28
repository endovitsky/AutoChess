using UnityEngine;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _startBattleWindowPrefab;

        private RectTransform _startBattleWindowInstance;

        public void Initialize()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No canvas vas found in scene");

                return;
            }

            _startBattleWindowInstance = Instantiate(_startBattleWindowPrefab, canvas.gameObject.transform);
        }
    }
}
