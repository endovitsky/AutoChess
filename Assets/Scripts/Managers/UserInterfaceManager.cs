using UnityEngine;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _startBattleWindowPrefab;
        [SerializeField]
        private RectTransform _gameOverWindowPrefab;

        private RectTransform _startBattleWindowInstance;
        private RectTransform _gameOverWindowInstance;

        public void Initialize()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                Debug.LogError("No canvas vas found in scene");

                return;
            }

            _startBattleWindowInstance = Instantiate(_startBattleWindowPrefab, canvas.gameObject.transform);
            _gameOverWindowInstance = Instantiate(_gameOverWindowPrefab, canvas.gameObject.transform);
        }
    }
}
