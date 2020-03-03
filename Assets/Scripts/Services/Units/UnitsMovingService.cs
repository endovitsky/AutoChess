using System.Linq;
using Managers;

namespace Services.Units
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
                    var path = GameManager.Instance.UnitsPathProvidingService.Paths.FirstOrDefault(x =>
                        x.FromUnitModel == unitModel);

                    // no path for this unit
                    if (path == null)
                    {
                        continue;
                    }

                    // no next square to move
                    if (path.SquareViews.Count <= 0)
                    {
                        continue;
                    }

                    // reached stack range - no need to move anymore
                    if (path.SquareViews.Count <= GameManager.Instance.UnitConfigurationService.AttackRange)
                    {
                        continue;
                    }

                    var squareView = path.SquareViews[0];

                    unitModel.Move(squareView);
                }
            }
        }
    }
}
