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

            // subscribe to state changing of each unit to recalculate all paths
            // TODO: not optimal - fix it
            foreach (var unitModel in GameManager.Instance.UnitsStateMonitoringService.UnitModels)
            {
                unitModel.SquareViewChanged += squareView =>
                {
                    RecalculatePaths();
                };
                unitModel.HealthChanged += health =>
                {
                    if (unitModel.IsDead)
                    {
                        // remove paths from dead units
                        var pathFromDeadUnitModel = Paths.FirstOrDefault(x=>x.FromUnitModel == unitModel);
                        if (pathFromDeadUnitModel == null)
                        {
                            //
                        }
                        Paths.Remove(pathFromDeadUnitModel);

                        // remove destination point to dead units
                        var pathsToDeadUnitModel = Paths.Where(x => x.ToUnitModel == unitModel).ToList();
                        foreach (var pathToDeadUnitModel in pathsToDeadUnitModel)
                        {
                            pathToDeadUnitModel.ToUnitModel = null;
                        }
                    }
                    // TODO: add reaction for not dead unit - resurrected

                    RecalculatePaths();
                };
            }
        }

        private void RecalculatePaths()
        {
            var unitModels = GameManager.Instance.UnitsStateMonitoringService.AliveUnitModels;
            foreach (var unitModel in unitModels)
            {
                RecalculatePath(unitModel);
            }
        }

        private void RecalculatePath(UnitModel unitModel)
        {
            var enemyUnitModels =
                GameManager.Instance.UnitsStateMonitoringService.GetAliveEnemyUnitModelsForUnitModel(unitModel);

            // no enemies
            if (enemyUnitModels.Count == 0)
            {
                return;
            }

            var pathToClosestEnemy = FindPathToClosestEnemy(unitModel, enemyUnitModels);

            var oldPathForThisUnitModel = Paths.FirstOrDefault(x => x.FromUnitModel == unitModel);

            //TODO: add path changed logging here

            if (oldPathForThisUnitModel!= null) // contains path for this pair of units - update path
            {
                oldPathForThisUnitModel.ToUnitModel = pathToClosestEnemy.ToUnitModel;
                oldPathForThisUnitModel.SquareViews = pathToClosestEnemy.SquareViews;
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
            enemyUnitModels.ForEach(x => enemySquareViews.Add(x.SquareView));

            var closestPath = GameManager.Instance.PathFindingService.FindClosestPath(
                unitModel.SquareView, enemySquareViews);

            var squareUnderUnit = closestPath[0];
            var squareUnderEnemyUnit = closestPath[closestPath.Count - 1];
            closestPath = closestPath.Where(x => x != squareUnderUnit).ToList();
            closestPath = closestPath.Where(x => x != squareUnderEnemyUnit).ToList();
            var unit = squareUnderUnit.UnitView.UnitModel;
            var enemyUnit = squareUnderEnemyUnit.UnitView.UnitModel;

            var pathToClosestEnemy = new Path(unit, enemyUnit, closestPath);

            return pathToClosestEnemy;
        }

        public class Path
        {
            public UnitModel FromUnitModel { get; private set; }
            public UnitModel ToUnitModel { get; set; }
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
