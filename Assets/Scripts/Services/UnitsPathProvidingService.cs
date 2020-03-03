using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Managers;
using Models;
using UnityEngine;
using Views;

namespace Services
{
    public class UnitsPathProvidingService
    {
        public Action<List<Path>> PathsChanged = delegate { };

        public List<Path> Paths { get; private set; } = new List<Path>();

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

                    unitModel.PositionChanged += squareView =>
                    {
                        // recalculate my paths to enemies
                        RecalculatePath(unitModel, enemyUnitModels);

                        // TODO: but enemies have their own paths to me - recalculate them too

                    };
                }
            }
        }

        private void RecalculatePath(UnitModel unitModel, List<UnitModel> enemyUnitModels)
        {
            var pathToClosestEnemy = FindPathToClosestEnemy(unitModel, enemyUnitModels);

            var path = Paths.FirstOrDefault(x => x.FromUnitModel == pathToClosestEnemy.ToUnitModel && 
                                                 x.ToUnitModel == pathToClosestEnemy.ToUnitModel);

            //TODO: add path changed logging here

            if (path!= null) // contains path for this pair of units - update path
            {
                path.SquareViews = path.SquareViews;
            }
            else // do not contains path for this pair of units - add path
            {
                Paths.Add(pathToClosestEnemy);
            }

            PathsChanged.Invoke(Paths);
        }

        private Path FindPathToClosestEnemy(UnitModel unitModel, List<UnitModel> enemyUnitModels)
        {
            var enemySquareViews = new List<SquareView>();
            foreach (var enemyUnitModel in enemyUnitModels)
            {
                var squareViewForEnemyUnitModel = enemyUnitModel.GetSquareViewForUnitModel();
                enemySquareViews.Add(squareViewForEnemyUnitModel);
            }

            var squareViewForUnitModel = unitModel.GetSquareViewForUnitModel();
            var closestPath = GameManager.Instance.PathFindingService.FindClosestPath(
                squareViewForUnitModel, enemySquareViews);

            var squareUnderUnit = closestPath[0];
            var squareUnderEnemyUnit = closestPath[closestPath.Count - 1];
            closestPath = closestPath.Where(x => x != squareUnderUnit).ToList();
            closestPath = closestPath.Where(x => x != squareUnderEnemyUnit).ToList();

            var unit = squareUnderUnit.GetUnitModelForSquareView();
            var enemyUnit = squareUnderEnemyUnit.GetUnitModelForSquareView();

            if (unit == null ||
                enemyUnit == null)
            {
                int a = 0;
                a++;
            }

            var pathToClosestEnemy = new Path(unit, enemyUnit, closestPath);

            return pathToClosestEnemy;
        }

        public class Path
        {
            public UnitModel FromUnitModel { get; private set; }
            public UnitModel ToUnitModel { get; private set; }
            public List<SquareView> SquareViews { get; set; }

            public Path(UnitModel fromUnitModel, 
                UnitModel toUnitModel, 
                List<SquareView> squareViews)
            {
                FromUnitModel = fromUnitModel;
                ToUnitModel = toUnitModel;
                SquareViews = squareViews;
            }
        }
    }
}
