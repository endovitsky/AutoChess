using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Factories
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _unitPrefab;

        private Dictionary<string, List<Transform>> _teamUnitInstances = new Dictionary<string, List<Transform>>();

        public void Initialize()
        {
            // by default there is no instances of units of any team
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                _teamUnitInstances.Add(teamName, new List<Transform>());
            }
        }

        public void TrySpawnUnit(Transform square)
        {
            var teamName = "";
            Transform instance = null;

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;

            if (square.localPosition.x - leftBorderPositionX <=
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) // near the left border
            {
                teamName = "Blue";

                if (GameManager.Instance.UnitsCountService.IsCanSpawnUnitForTeam(teamName))
                {
                    instance = Instantiate(_unitPrefab, square.gameObject.transform);
                }
            }
            if (rightBorderPositionX - square.localPosition.x <
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                teamName = "Red";

                if (GameManager.Instance.UnitsCountService.IsCanSpawnUnitForTeam(teamName))
                {
                    instance = Instantiate(_unitPrefab, square.gameObject.transform);
                }
            }

            if (instance == null)
            {
                return;
            }

            _teamUnitInstances[teamName].Add(instance);

            GameManager.Instance.UnitsCountService.TeamNamesUnitsCounts[teamName]++;

            instance.gameObject.GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);
        }
    }
}
