using System.Collections.Generic;
using System.Linq;
using Managers;
using Views;

namespace Services
{
    public class UnitsPathProvidingService
    {
        public List<List<SquareView>> Paths { get; } = new List<List<SquareView>>();

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

            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                var enemyTeamName = GameManager.Instance.TeamsConfigurationService.TeamNames.First(x => x != teamName);
                var enemyUnitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(enemyTeamName);

                var unitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(teamName);
                foreach (var unitModel in unitModels)
                {
                    var enemySquareViews = new List<SquareView>();
                    enemyUnitModels.ForEach(x => enemySquareViews.Add(x.SquareView));

                    var pathToClosestEnemy =
                        GameManager.Instance.PathFindingService.FindClosestPath(unitModel.SquareView, enemySquareViews);

                    Paths.Add(pathToClosestEnemy);
                }
            }
        }

        public List<SquareView> GetPath(SquareView start, SquareView end)
        {
            var path = new List<SquareView>();

            path = Paths.FirstOrDefault(x => x[0] == start && x[x.Count - 1] == end);

            return path;
        }
    }
}
