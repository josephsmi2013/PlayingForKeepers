using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.DB.Tables
{
    public class FF_Teams
    {

        [Key]
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamAbbr { get; set; }
        public string TeamOwnerID { get; set; }

        [ForeignKey("LeagueID")]
        public int LeagueID { get; set; }
    }
}
