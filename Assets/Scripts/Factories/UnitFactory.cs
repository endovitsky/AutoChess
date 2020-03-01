using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Factories
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _unitPrefab;

        private List<Transform> _redTeamUnitInstances = new List<Transform>();
        private List<Transform> _blueTeamUnitInstances = new List<Transform>();

        public void TrySpawnUnit(Transform square)
        {
            var teamName = "";

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;

            if (square.localPosition.x - leftBorderPositionX <=
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) // near the left border
            {
                var instance = Instantiate(_unitPrefab, square.gameObject.transform);

                teamName = "Blue";

                instance.gameObject.GetComponent<SpriteRenderer>().sprite = 
                    GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);

                _blueTeamUnitInstances.Add(instance);
            }
            if (rightBorderPositionX - square.localPosition.x <
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                var instance = Instantiate(_unitPrefab, square.gameObject.transform);

                teamName = "Red";

                instance.gameObject.GetComponent<SpriteRenderer>().sprite =
                    GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);

                _redTeamUnitInstances.Add(instance);
            }
        }
    }
}
