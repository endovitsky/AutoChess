using System.Linq;
using Managers;

namespace Services
{
    public class UnitsFightingService
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

                    // no target for this unit
                    if (path.ToUnitModel == null)
                    {
                        continue;
                    }

                    // do not eat reached the target
                    if (path.SquareViews.Count > GameManager.Instance.UnitConfigurationService.AttackRange)
                    {
                        continue;
                    }

                    var target = path.ToUnitModel;
                    unitModel.Attack(target);
                }
            }
        }
    }
}
