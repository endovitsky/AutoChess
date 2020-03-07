using System.Linq;
using Managers;

namespace Services.Units
{
    public class UnitsFightingService
    {
        public void Initialize()
        {
            // uninit
            GameManager.Instance.TimerService.SecondsPassedCountChanged -= OnSecondsPassedCountChanged;
            // init
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
                var aliveUnitModelsForTeam = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(teamName);
                foreach (var unitModel in aliveUnitModelsForTeam)
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
