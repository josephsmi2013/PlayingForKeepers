using System.ComponentModel.DataAnnotations.Schema;

namespace PlayingForKeepers.Models.DB.Tables
{
    public class FF_LeagueUsers
    {
        [ForeignKey("LeagueID")]
        public int LeagueID { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

    }
}
