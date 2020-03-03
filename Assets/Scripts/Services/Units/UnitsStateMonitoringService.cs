using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Models;
using Debug = UnityEngine.Debug;

namespace Services.Units
{
    public class UnitsStateMonitoringService
    {
        public Action<int> AliveUnitModelsCountChanged = delegate { };

        public List<UnitModel> UnitModels { get; } = new List<UnitModel>();
        public List<UnitModel> AliveUnitModels
        {
            get { return UnitModels.Where(x => !x.IsDead).ToList(); }
        }

        private Dictionary<string, List<UnitModel>> _teamAliveUnitModels = new Dictionary<string, List<UnitModel>>();

        public void Initialize()
        {
            // by default the count of alive units for all teams is 0
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                _teamAliveUnitModels.Add(teamName, new List<UnitModel>());
            }
        }

        public void RegisterUnitForStateMonitoring(UnitModel unitModel)
        {
            UnitModels.Add(unitModel);

            if (!unitModel.IsDead)
            {
                var previousCount = _teamAliveUnitModels[unitModel.TeamName].Count;
                _teamAliveUnitModels[unitModel.TeamName].Add(unitModel);
                var currentCount = _teamAliveUnitModels[unitModel.TeamName].Count;

                Debug.Log($"Alive unit models count of {unitModel.TeamName} team " +
                          $"changed from {previousCount} to {currentCount}");

                AliveUnitModelsCountChanged.Invoke(_teamAliveUnitModels.Count);
            }

            unitModel.HealthChanged += health =>
            {
                if (unitModel.IsDead)
                {
                    var previousCount = _teamAliveUnitModels[unitModel.TeamName].Count;
                    _teamAliveUnitModels[unitModel.TeamName].Remove(unitModel);
                    var currentCount = _teamAliveUnitModels[unitModel.TeamName].Count;

                    Debug.Log($"Alive unit models count of {unitModel.TeamName} team " +
                              $"changed from {previousCount} to {currentCount}");

                    AliveUnitModelsCountChanged.Invoke(_teamAliveUnitModels.Count);
                }
            };
        }

        // TODO: move to more correct place
        public List<UnitModel> GetAliveUnitModelsForTeam(string teamName)
        {
            return _teamAliveUnitModels[teamName];
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
