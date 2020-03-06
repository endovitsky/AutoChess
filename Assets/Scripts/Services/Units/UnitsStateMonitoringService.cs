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

        public void RegisterUnitForStateMonitoring(UnitModel unitModel)
        {
            UnitModels.Add(unitModel);

            if (!unitModel.IsDead)
            {
                OnIsDeadChanged(unitModel);
            }

            unitModel.HealthChanged += health =>
            {
                if (unitModel.IsDead)
                {
                    OnIsDeadChanged(unitModel);
                }
            };
        }

        private void OnIsDeadChanged(UnitModel unitModel)
        {
            var previousCount = GetAliveUnitModelsForTeam(unitModel.TeamName).Count;
            var currentCount = GetAliveUnitModelsForTeam(unitModel.TeamName).Count;

            Debug.Log($"Alive unit models count of {unitModel.TeamName} team " +
                      $"changed from {previousCount} to {currentCount}");

            AliveUnitModelsCountChanged.Invoke(AliveUnitModels.Count);
        }

        // TODO: move to more correct place
        public List<UnitModel> GetAliveUnitModelsForTeam(string teamName)
        {
            var unitModels = UnitModels.Where(x=>x.IsDead == false && 
                                                 x.TeamName == teamName).ToList();

            return unitModels;
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
