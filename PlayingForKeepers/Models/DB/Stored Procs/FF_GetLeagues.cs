using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.DB.Stored_Procs
{
    public class FF_GetLeagues
    {
        public int LeagueID { get; set; }
        public string LeagueName { get; set; }
        public int LeagueTeamsUsed { get; set; }
        public int LeagueTeamsPossible { get; set; }
        public string LeagueOwnerID { get; set; }
        public LeagueStatusListSP LeagueStatus { get; set; }
    }

    public enum LeagueStatusListSP
    {
        Submitted,
        Approved,
        Rejected
    }
}
