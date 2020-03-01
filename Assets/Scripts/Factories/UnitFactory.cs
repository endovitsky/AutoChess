using System.Collections.Generic;
using Managers;
using Models;
using UnityEngine;
using Views;

namespace Factories
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField]
        private UnitView _unitViewPrefab;

        private Dictionary<string, List<UnitView>> _teamUnitViewInstances = new Dictionary<string, List<UnitView>>();

        public void Initialize()
        {
            // by default there is no instances of units of any team
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                _teamUnitViewInstances.Add(teamName, new List<UnitView>());
            }
        }

        public void TrySpawnUnit(Transform square)
        {
            var teamName = "";
            UnitView instance = null;

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;

            if (square.localPosition.x - leftBorderPositionX <=
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) // near the left border
            {
                teamName = "Blue";

                if (GameManager.Instance.UnitsCountService.IsCanSpawnUnitForTeam(teamName))
                {
                    instance = Instantiate(_unitViewPrefab, square.gameObject.transform);
                }
            }
            if (rightBorderPositionX - square.localPosition.x <
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                teamName = "Red";

                if (GameManager.Instance.UnitsCountService.IsCanSpawnUnitForTeam(teamName))
                {
                    instance = Instantiate(_unitViewPrefab, square.gameObject.transform);
                }
            }

            if (instance == null)
            {
                return;
            }

            instance.Initialize(new UnitModel(GameManager.Instance.UnitConfigurationService.InitialHealth));

            _teamUnitViewInstances[teamName].Add(instance);

            GameManager.Instance.UnitsCountService.TeamNamesUnitsCounts[teamName]++;

            instance.gameObject.GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);
        }
    }
}
