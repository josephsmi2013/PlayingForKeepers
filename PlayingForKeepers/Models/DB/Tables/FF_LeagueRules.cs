using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Models.DB.Tables
{
    public class FF_LeagueRules
    {
        [Key]
        public int LeagueRuleID { get; set; }

        [ForeignKey("LeagueID")]
        public int LeagueID { get; set; }

        public string LeagueRuleDescription { get; set; }

        public DateTime LeagueRuleCreatedDate { get; set; }

        public DateTime LeagueRuleModifiedDate { get; set; }


    }
}
