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
                    var path = GameManager.Instance.UnitsPathProvidingService.Paths.First(x => x.Key == unitModel);

                    var squareView = path.Value[1];
                    unitModel.Move(squareView);
                }
            }
        }
    }
}
