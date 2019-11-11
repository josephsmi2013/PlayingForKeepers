using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayingForKeepers.Models.DB.Tables
{
    public partial class FF_Leagues
    {
        [Key]
        public int LeagueID { get; set; }
        public string LeagueName { get; set; }
        public int LeagueTeamsUsed { get; set; }
        public int LeagueTeamsPossible { get; set; }
        public string LeagueOwnerID { get; set; }

        [NotMapped]
        public LeagueStatus LeagueStatus { get; set; }
    }

    public enum LeagueStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
