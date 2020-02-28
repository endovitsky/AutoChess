using GameObjects;
using UnityEngine;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour
    {
        [SerializeField]
        private ChessBoard _chessBoardPrefab;

        private ChessBoard _chessBoardInstance;

        public void Initialize()
        {
            _chessBoardInstance = Instantiate(_chessBoardPrefab, this.gameObject.transform);

            var size = _chessBoardInstance._squarePrefab.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            _chessBoardInstance.gameObject.transform.localPosition = new Vector3(
                _chessBoardInstance.gameObject.transform.localPosition.x - _chessBoardInstance._width / 2 + size.x / 2,
                _chessBoardInstance.gameObject.transform.localPosition.y - _chessBoardInstance._height / 2 + size.y / 2);

            _chessBoardInstance.Initialize();
        }
    }
}
