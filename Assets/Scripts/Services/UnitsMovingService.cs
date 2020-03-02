using System.Linq;
using Managers;

namespace Services
{
    public class UnitsMovingService
    {
        public void Initialize()
        {
            GameManager.Instance.TimerService.SecondsPassedCountChanged += OnSecondsPassedCountChanged;
        }

        private void OnSecondsPassedCountChanged(int secondsPassedCount)
        {
            if (GameManager.Instance.GameStateService.SelectedGameState != GameStateService.GameState.Started)
            {
                return;
            }

            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                foreach (var unitModel in GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(teamName))
                {
                    var path = GameManager.Instance.UnitsPathProvidingService.Paths.
                        First(x => x.Key == unitModel).Value.ToList();

                    // no next square to move
                    if (path.Count < 2)
                    {
                        continue;
                    }

                    var squareView = path[1];

                    var pathLength = GameManager.Instance.PathFindingService.GetDistance(path);
                    // this unit is in attack range with his target - do not need to move anymore
                    if (pathLength <= GameManager.Instance.UnitConfigurationService.AttackRange)
                    {
                        continue;
                    }

                    unitModel.Move(squareView);
                }
            }
        }
    }
}
