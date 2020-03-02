using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Models;
using UnityEngine;
using Views;

namespace Services
{
    public class UnitsPathProvidingService
    {
        public Action<Dictionary<UnitModel, List<SquareView>>> PathsChanged = delegate { };

        public Dictionary<UnitModel, List<SquareView>> Paths { get; } = new Dictionary<UnitModel, List<SquareView>>();

        public void Initialize()
        {
            GameManager.Instance.GameStateService.SelectedGameStateChanged += OnSelectedGameStateChanged;
        }

        private void OnSelectedGameStateChanged(GameStateService.GameState gameState)
        {
            if (gameState != GameStateService.GameState.Started)
            {
                return;
            }

            RecalculatePaths();
        }

        private void RecalculatePaths()
        {
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                var enemyTeamName = GameManager.Instance.TeamsConfigurationService.TeamNames.First(x => x != teamName);
                var enemyUnitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(enemyTeamName);

                var unitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(teamName);
                foreach (var unitModel in unitModels)
                {
                    RecalculatePath(unitModel, enemyUnitModels);

                    unitModel.SquareViewChanged += squareView =>
                    {
                        RecalculatePath(unitModel, enemyUnitModels);
                    };
                }
            }
        }

        private void RecalculatePath(UnitModel unitModel, List<UnitModel> enemyUnitModels)
        {
            var enemySquareViews = new List<SquareView>();
            enemyUnitModels.ForEach(x => enemySquareViews.Add(x.SquareView));

            var pathToClosestEnemy =
                GameManager.Instance.PathFindingService.FindClosestPath(unitModel.SquareView, enemySquareViews);

            Paths[unitModel] = pathToClosestEnemy;

            Debug.Log($"Path changed for unit in square " +
                      $"{pathToClosestEnemy[0].Position.x}:{pathToClosestEnemy[0].Position.y}");

            PathsChanged.Invoke(Paths);
        }
    }
}
