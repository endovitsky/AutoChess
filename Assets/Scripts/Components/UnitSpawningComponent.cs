using Managers;
using UnityEngine;

namespace Components
{
    public class UnitSpawningComponent : MonoBehaviour
    {
        [SerializeField]
        private Transform _unitPrefab;

        private Transform _unitInstance;

        private void Start()
        {
            var unit = this.gameObject.transform;

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;
            if (unit.localPosition.x - leftBorderPositionX <= GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft || // near the left border
                rightBorderPositionX - unit.localPosition.x < GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                _unitInstance = Instantiate(_unitPrefab, this.gameObject.transform);
            }
        }
    }
}
