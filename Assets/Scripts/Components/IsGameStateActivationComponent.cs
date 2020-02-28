using Managers;
using Services;
using UnityEngine;

namespace Components
{
    public class IsGameStateActivationComponent : MonoBehaviour
    {
        [SerializeField] 
        private GameStateService.GameState _gameState;

        private void Start()
        {
            GameManager.Instance.GameStateService.SelectedGameStateChanged += SelectedGameStateChanged;
        }
        private void OnDestroy()
        {
            if (GameManager.Instance.GameStateService.SelectedGameStateChanged != null)
            {
                GameManager.Instance.GameStateService.SelectedGameStateChanged -= SelectedGameStateChanged;
            }
        }

        private void SelectedGameStateChanged(GameStateService.GameState gameState)
        {
            this.gameObject.SetActive(gameState == _gameState);
        }
    }
}
