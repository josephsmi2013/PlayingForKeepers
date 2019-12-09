using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Models.DB.Tables;
using PlayingForKeepers.Pages.Shared;

namespace PlayingForKeepers.Pages.Leagues
{
    public class LeagueRulesModel : DI_BasePageModel
    {
        #region Public Properties  
        public List<FF_Leagues> GetLeague { get; set; }
        public List<FF_LeagueRules> GetLeagueRules { get; set; }
        #endregion



        #region Constructor method
        public LeagueRulesModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpClientFactory clientFactory)
            : base(context, authorizationService, userManager, roleManager, clientFactory)
        {
        }
        #endregion



        #region OnGet method
        //Get LeagueTeams, TeamUsers, and LeagueActivity into a list and returns to the page
        public async Task<IActionResult> OnGetAsync(int leagueId)
        {
            GetLeague = await Context.GetLeaguesAsync(leagueId);
            GetLeagueRules = await Context.GetLeagueRulesAsync(leagueId);


            if (GetLeague == null)
            {
                ViewData["ErrorMessage"] = $"League not found";
            }

            if (GetLeagueRules == null)
            {
                ViewData["ErrorMessage"] = $"League rules not found";
            }


            return Page();
        }
        #endregion


    }
}