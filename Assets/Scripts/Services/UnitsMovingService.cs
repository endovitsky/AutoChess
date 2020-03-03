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
                    var path = GameManager.Instance.UnitsPathProvidingService.Paths.FirstOrDefault(x =>
                        x.FromUnitModel == unitModel);

                    // no path from this unit
                    if (path == null)
                    {
                        continue;
                    }

                    // no next square to move
                    if (path.SquareViews.Count <= 0)
                    {
                        continue;
                    }

                    var squareView = path.SquareViews[0];

                    unitModel.Move(squareView.Position);
                }
            }
        }
    }
}
