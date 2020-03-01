using System;
using System.Collections.Generic;
using Managers;

namespace Services
{
    public class UnitsCountService
    {
        private static System.Random _random = new Random();

        public Dictionary<string, int> AllowedTeamNamesUnitsCounts = new Dictionary<string, int>();

        public Dictionary<string, int> TeamNamesUnitsCounts = new Dictionary<string, int>();

        public void Initialize()
        {
            // by default the count of units for all teams is 0
            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                TeamNamesUnitsCounts.Add(teamName, 0);
            }

            foreach (var teamName in GameManager.Instance.TeamsConfigurationService.TeamNames)
            {
                var unitsCount = GetRandomUnitsCount();

                AllowedTeamNamesUnitsCounts.Add(teamName, unitsCount);
            }
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
            var result = TeamNamesUnitsCounts[teamName] < AllowedTeamNamesUnitsCounts[teamName];

            return result;
        }
    }
}
