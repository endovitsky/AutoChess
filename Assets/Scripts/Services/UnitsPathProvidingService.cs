using System.Linq;
using Managers;

namespace Services
{
    public class UnitsPathProvidingService
    {
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

            var redUnitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam("Red").First();
            var blueUnitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam("Blue").First();

            var path = GameManager.Instance.PathFindingService.FindPath(
                redUnitModels.SquareView,
                blueUnitModels.SquareView);
        }
    }
}
