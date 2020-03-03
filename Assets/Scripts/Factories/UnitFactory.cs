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

        public void TryInstantiateUnit(SquareView squareView)
        {
            var teamName = "";

            var rightBorderPositionX = GameManager.Instance.ChessBoardConfigurationService.Width;
            var leftBorderPositionX = 0;

            if ((squareView.gameObject.transform.localPosition.x - leftBorderPositionX > GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) &&
                (rightBorderPositionX - squareView.gameObject.transform.localPosition.x >= GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight))
            {
                return;
            }

            if (squareView.gameObject.transform.localPosition.x - leftBorderPositionX <=
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromLeft) // near the left border
            {
                teamName = "Blue";
            }
            if (rightBorderPositionX - squareView.gameObject.transform.localPosition.x <
                GameManager.Instance.UnitsSpawningConfigurationService.StepFromRight) // near the right border
            {
                teamName = "Red";
            }

            if (!GameManager.Instance.UnitsCountService.IsCanSpawnUnitForTeam(teamName))
            {
                return;
            }

            InstantiateUnit(squareView, teamName);
        }

        public void InstantiateUnit(SquareView squareView, string teamName)
        {
            UnitView instance = null;

            instance = Instantiate(_unitViewPrefab, squareView.gameObject.transform);

            instance.Initialize(new UnitModel(
                GameManager.Instance.UnitConfigurationService.AttackDamage,
                GameManager.Instance.UnitConfigurationService.InitialHealth,
                teamName,
                squareView,
                instance));

            squareView.UnitView = instance;

            _teamUnitViewInstances[teamName].Add(instance);
            GameManager.Instance.UnitsCountService.IncreaseUnitsCountForTeam(teamName);
            GameManager.Instance.UnitsStateMonitoringService.RegisterUnitForStateMonitoring(instance.UnitModel);

            instance.gameObject.GetComponent<SpriteRenderer>().sprite =
                GameManager.Instance.TexturesResourcesManager.Get("Units", teamName);
        }
    }
}
