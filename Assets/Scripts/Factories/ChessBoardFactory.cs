using Managers;
using UnityEngine;
using Views;

namespace Factories
{
    public class ChessBoardFactory : MonoBehaviour
    {
        [SerializeField]
        private ChessBoardView _chessBoardViewPrefab;

        private ChessBoardView _chessBoardViewInstance;

        public void InstantiateChessBoard(Transform parent)
        {
            _chessBoardViewInstance = Instantiate(_chessBoardViewPrefab, parent);

            // set board position to center of parent container
            var xCorrection = GameManager.Instance.ChessBoardConfigurationService.Width / 2 +
                              GameManager.Instance.SquareFactory.SquareSize.x / 2;
            var yCorrection = GameManager.Instance.ChessBoardConfigurationService.Height / 2 +
                              GameManager.Instance.SquareFactory.SquareSize.y / 2;
            _chessBoardViewInstance.gameObject.transform.localPosition = new Vector3(
                _chessBoardViewInstance.gameObject.transform.localPosition.x - xCorrection,
                _chessBoardViewInstance.gameObject.transform.localPosition.y - yCorrection);

            _chessBoardViewInstance.Initialize();
        }
    }
}
