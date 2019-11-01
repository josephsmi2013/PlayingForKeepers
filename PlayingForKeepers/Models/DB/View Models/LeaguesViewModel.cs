using System.Collections.Generic;
using PlayingForKeepers.Models.DB.Stored_Procs;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlayingForKeepers.Models.DB.View_Models
{
    public class LeaguesViewModel
    {
        #region Properties  

        //Properties for creating a new league
        public string LeagueName { get; set; }
        public int LeagueTeamsPossible { get; set; }


        //Property for getting a list of leagues 
        public List<FF_GetLeagues> LeagueList { get; set; }

        #endregion
    }
}
