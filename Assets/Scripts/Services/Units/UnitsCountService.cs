using System;
using System.Collections.Generic;
using Managers;
using Models;

namespace Services.Units
{
    public class UnitsCountService
    {
        private static System.Random _random = new Random();

        private Dictionary<string, int> _allowedTeamNamesUnitsCounts = new Dictionary<string, int>();
        private Dictionary<string, int> _teamNamesUnitsCounts = new Dictionary<string, int>();

        public void Initialize()
        {
            OnSelectedGameStateChanged(GameManager.Instance.GameStateService.SelectedGameState);
            GameManager.Instance.GameStateService.SelectedGameStateChanged += OnSelectedGameStateChanged;

            GameManager.Instance.UnitFactory.UnitInstantiated += OnUnitInstantiated;
            GameManager.Instance.UnitFactory.UnitDestroyed += OnUnitDestroyed;
        }

        private void OnSelectedGameStateChanged(GameStateService.GameState gameState)
        {
            // by default the count of units for all teams is 0
            _teamNamesUnitsCounts.Clear();
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                _teamNamesUnitsCounts.Add(teamName, 0);
            }

            _allowedTeamNamesUnitsCounts.Clear();
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                var unitsCount = GetRandomUnitsCount();

                _allowedTeamNamesUnitsCounts.Add(teamName, unitsCount);
            }
        }

        private void OnUnitInstantiated(UnitModel unitModel)
        {
            _teamNamesUnitsCounts[unitModel.TeamName]++;
        }
        private void OnUnitDestroyed(UnitModel unitModel)
        {
            _teamNamesUnitsCounts[unitModel.TeamName]--;
        }

        private int GetRandomUnitsCount()
        {
            var result = _random.Next(
                GameManager.Instance.UnitsSpawningConfigurationService.MinUnitsCount,
                GameManager.Instance.UnitsSpawningConfigurationService.MaxUnitsCount + 1);

            return result;
        }

        public bool IsCanSpawnUnitForTeam(string teamName)
        {
            var result = _teamNamesUnitsCounts[teamName] < _allowedTeamNamesUnitsCounts[teamName];

            return result;
        }
    }
}
