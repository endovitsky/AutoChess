using System;
using System.Linq;
using Managers;
using UnityEngine;

namespace Services
{
    public class GameStateService
    {
        public Action<GameState> SelectedGameStateChanged = delegate { };

        public GameState SelectedGameState
        {
            get => _selectedGameState;
            set
            {
                if (value == _selectedGameState)
                {
                    return;
                }

                Debug.Log($"Selected game state changed from {_selectedGameState} to {value}");

                _selectedGameState = value;

                SelectedGameStateChanged.Invoke(_selectedGameState);
            }
        }

        private GameState _selectedGameState;

        public void Initialize()
        {
            SelectedGameState = GameState.NotStarted;

            SelectedGameStateChanged += OnSelectedGameStateChanged;
        }

        private void OnSelectedGameStateChanged(GameState gameState)
        {
            if (gameState != GameState.Started)
            {
                return;
            }

            var redUnitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam("Red").First();
            var blueUnitModels = GameManager.Instance.UnitsStateMonitoringService.GetAliveUnitModelsForTeam("Blue").First();

            GameManager.Instance.PathFindingService.GetClosestUnitOfTeam(redUnitModels, blueUnitModels.TeamName);
        }

        public enum GameState
        {
            NotStarted,
            Started,
            Finished
        }
    }
}
