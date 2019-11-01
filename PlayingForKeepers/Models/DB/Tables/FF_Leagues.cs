using System;
using System.Collections.Generic;

namespace PlayingForKeepers.Models.DB.Tables
{
    public partial class FF_Leagues
    {
        public int LeagueID { get; set; }
        public string LeagueName { get; set; }
        public int LeagueTeamsUsed { get; set; }
        public int LeagueTeamsPossible { get; set; }
        public string LeagueOwnerID { get; set; }
        public LeagueStatusList LeagueStatus {get; set; }
    }

    public enum LeagueStatusList
    {
        Submitted,
        Approved,
        Rejected
    }
}
