using System;
using System.Collections.Generic;
using Managers;
using Models;
using Debug = UnityEngine.Debug;

namespace Services
{
    public class UnitsStateMonitoringService
    {
        public Action<int> AliveUnitModelsCountChanged = delegate { };

        private List<UnitModel> _unitModels = new List<UnitModel>();
        private Dictionary<string, List<UnitModel>> _aliveUnitModels = new Dictionary<string, List<UnitModel>>();

        public void Initialize()
        {
            // by default the count of alive units for all teams is 0
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                _aliveUnitModels.Add(teamName, new List<UnitModel>());
            }
        }

        public void RegisterUnitForStateMonitoring(UnitModel unitModel)
        {
            _unitModels.Add(unitModel);

            if (!unitModel.IsDead)
            {
                var previousCount = _aliveUnitModels[unitModel.TeamName].Count;
                _aliveUnitModels[unitModel.TeamName].Add(unitModel);
                var currentCount = _aliveUnitModels[unitModel.TeamName].Count;

                Debug.Log($"Alive unit models count of {unitModel.TeamName} team " +
                          $"changed from {previousCount} to {currentCount}");

                AliveUnitModelsCountChanged.Invoke(_aliveUnitModels.Count);
            }

            unitModel.HealthChanged += health =>
            {
                if (unitModel.IsDead)
                {
                    var previousCount = _aliveUnitModels[unitModel.TeamName].Count;
                    _aliveUnitModels[unitModel.TeamName].Remove(unitModel);
                    var currentCount = _aliveUnitModels[unitModel.TeamName].Count;

                    Debug.Log($"Alive unit models count of {unitModel.TeamName} team " +
                              $"changed from {previousCount} to {currentCount}");

                    AliveUnitModelsCountChanged.Invoke(_aliveUnitModels.Count);
                }
            };
        }

        // TODO: move to more correct place
        public List<UnitModel> GetAliveUnitModelsForTeam(string teamName)
        {
            return _aliveUnitModels[teamName];
        }

        public List<UnitModel> GetAliveEnemyUnitModelsForUnitModel(UnitModel unitModel)
        {
            return GetAliveEnemyUnitModelsForTeam(unitModel.TeamName);
        }

        public List<UnitModel> GetAliveEnemyUnitModelsForTeam(string teamName)
        {
            var enemyTeamName = GameManager.Instance.TeamsConfigurationService.GetEnemyTeamName(teamName);
            var enemyUnitModels = GetAliveUnitModelsForTeam(enemyTeamName);
            return enemyUnitModels;
        }
    }
}
