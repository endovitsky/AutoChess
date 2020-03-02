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
        }

        public enum GameState
        {
            NotStarted,
            Started,
            Finished
        }
    }
}
