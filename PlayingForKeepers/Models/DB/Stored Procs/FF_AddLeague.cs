using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayingForKeepers.Models.DB.Stored_Procs
{
    public class FF_AddLeague
    {

        public string LeagueName { get; set; }

        public int LeagueTeamsPossible { get; set; }

    }
}
