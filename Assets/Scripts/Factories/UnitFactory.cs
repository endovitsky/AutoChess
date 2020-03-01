using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Factories
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _unitPrefab;

        private List<Transform> _unitInstances = new List<Transform>();

        public void TrySpawnUnit(Transform square)
        {
            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;
            if (square.localPosition.x - leftBorderPositionX <= GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft || // near the left border
                rightBorderPositionX - square.localPosition.x < GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                var instance = Instantiate(_unitPrefab, square.gameObject.transform);

                _unitInstances.Add(instance);
            }
        }
    }
}
