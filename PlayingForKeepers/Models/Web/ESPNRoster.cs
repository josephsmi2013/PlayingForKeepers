using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.Web
{
    public class ESPNRoster
    {
        public int gameId { get; set; }

        public int seasonId { get; set; }

        public List<TeamsSubType> teams { get; set; }
      
    }
    public class TeamsSubType
    {
        public int id { get; set; }

        public RosterTeamsSubType roster { get; set; }
    }

    public class RosterTeamsSubType
    {
        public float appliedStatTotal { get; set; }

        public List<EntriesSubType> entries { get; set; }
    }

    public class EntriesSubType
    {
        public int playerId { get; set; }

        public PlayerPoolEntrySubType playerPoolEntry { get; set; }
    }


    public class PlayerPoolEntrySubType
    {
        public int onTeamId { get; set; }

        public PlayerSubType player { get; set; }
    }

    public class PlayerSubType
    {
        public string injuryStatus { get; set; }

        public string fullName { get; set; }

        public string seasonOutlook { get; set; }
    }
}
