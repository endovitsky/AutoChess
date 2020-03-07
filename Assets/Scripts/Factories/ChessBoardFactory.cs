using Managers;
using Services;
using UnityEngine;
using Views;

namespace Factories
{
    public class ChessBoardFactory : MonoBehaviour
    {
        [SerializeField]
        private ChessBoardView _chessBoardViewPrefab;

        private ChessBoardView _chessBoardViewInstance;

        public void Initialize()
        {
            //SelectedGameStateChanged(GameManager.Instance.GameStateService.SelectedGameState);
            GameManager.Instance.GameStateService.SelectedGameStateChanged += SelectedGameStateChanged;
        }

        private void SelectedGameStateChanged(GameStateService.GameState gameState)
        {
            if (gameState != GameStateService.GameState.NotStarted)
            {
                return;
            }

            // clear
            if (_chessBoardViewInstance != null)
            {
                Destroy(_chessBoardViewInstance.gameObject);

                _chessBoardViewInstance = null;
            }

            // instantiate
            _chessBoardViewInstance = Instantiate(_chessBoardViewPrefab, GameManager.Instance.transform);
        }
    }
}
