using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.DB.Tables
{
    public class FF_LeagueTeams
    {
        [ForeignKey("LeagueID")]
        public int LeagueID { get; set; }

        [ForeignKey("TeamID")]
        public int TeamID { get; set; }

    }
}
