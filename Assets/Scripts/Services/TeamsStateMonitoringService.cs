using Managers;

namespace Services
{
    public class TeamsStateMonitoringService
    {
        public int AliveTeamsCount
        {
            get
            {
                var aliveTeamsCount = 0;

                foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
                {
                    var aliveUnitModelsForTeam =
                        GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam(teamName);

                    // not all members of team is dead
                    if (aliveUnitModelsForTeam.Count != 0)
                    {
                        aliveTeamsCount++;
                    }
                }

                return aliveTeamsCount;
            }
        }

        public void Initialize()
        {
            OnAliveUnitModelsCountChanged(GameManager.Instance.UnitsStateMonitoringService.AliveUnitModels.Count);
            GameManager.Instance.UnitsStateMonitoringService.AliveUnitModelsCountChanged += OnAliveUnitModelsCountChanged;
        }

        private void OnAliveUnitModelsCountChanged(int aliveUnitModelsCount)
        {
            if (GameManager.Instance.GameStateService.SelectedGameState != GameStateService.GameState.Started)
            {
                return;
            }

            // only 1 team left - game is over
            if (AliveTeamsCount == 1)
            {
                GameManager.Instance.GameStateService.SelectedGameState = GameStateService.GameState.Finished;
            }
        }
    }
}
