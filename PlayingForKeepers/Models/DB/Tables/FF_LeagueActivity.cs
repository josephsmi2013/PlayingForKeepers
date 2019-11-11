using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.DB.Tables
{
    public class FF_LeagueActivity
    {
        [Key]
        public int LeagueActivityID { get; set; }
        [NotMapped]
        public int LeagueID { get; set; }
        public DateTime LeagueActivityDate { get; set; }
        public string LeagueActivityDescription { get; set; }
        public LeagueActivityType LeagueActivityType { get; set; }
    }

    public enum LeagueActivityType
    {
        Created,
        Read,
        Updated,
        Deleted
    }
}
