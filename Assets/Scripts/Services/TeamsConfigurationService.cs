using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class TeamsConfigurationService
    {
        public List<string> TeamNames { get; private set; }

        public void Initialize()
        {
            // TODO: add receiving this data from json config file
            TeamNames = new List<string> {"Red", "Blue"};
        }

        public string GetEnemyTeamName(string teamName)
        {
            var enemyTeamName = TeamNames.First(x => x != teamName);

            return enemyTeamName;
        }
    }
}
