using Managers;
using UnityEngine;

namespace Components
{
    public class UnitSpawningComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _unitPrefab;

        private Transform _unitInstance;

        private int _stepFromLeft = 3;
        private int _stepFromRight = 3;

        private void Start()
        {
            var unit = this.gameObject.transform;

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;
            if (unit.localPosition.x - leftBorderPositionX <= _stepFromLeft || // near the left border
                rightBorderPositionX - unit.localPosition.x < _stepFromRight) // near the right border
            {
                _unitInstance = Instantiate(_unitPrefab, this.gameObject.transform);
            }
        }
    }
}
