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
            var size = _chessBoardInstance._squarePrefab.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            var xCorrection = GameManager.Instance.ChessBoardConfigurationService.Width / 2 + size.x / 2;
            var yCorrection = GameManager.Instance.ChessBoardConfigurationService.Height / 2 + size.y / 2;
            _chessBoardInstance.gameObject.transform.localPosition = new Vector3(
                _chessBoardInstance.gameObject.transform.localPosition.x - xCorrection,
                _chessBoardInstance.gameObject.transform.localPosition.y - yCorrection);

            _chessBoardInstance.Initialize();
        }
    }
}
