using GameObjects;
using Managers;
using UnityEngine;

namespace Factories
{
    public class ChessBoardFactory : MonoBehaviour
    {
        [SerializeField]
        private ChessBoard _chessBoardPrefab;

        private ChessBoard _chessBoardInstance;

        public void InstantiateChessBoard(Transform parent)
        {
            _chessBoardInstance = Instantiate(_chessBoardPrefab, parent);

            // set board position to center of parent container
            var xCorrection = GameManager.Instance.ChessBoardConfigurationService.Width / 2 +
                              GameManager.Instance.SquareFactory.SquareSize.x / 2;
            var yCorrection = GameManager.Instance.ChessBoardConfigurationService.Height / 2 +
                              GameManager.Instance.SquareFactory.SquareSize.y / 2;
            _chessBoardInstance.gameObject.transform.localPosition = new Vector3(
                _chessBoardInstance.gameObject.transform.localPosition.x - xCorrection,
                _chessBoardInstance.gameObject.transform.localPosition.y - yCorrection);

            _chessBoardInstance.Initialize();
        }
    }
}
