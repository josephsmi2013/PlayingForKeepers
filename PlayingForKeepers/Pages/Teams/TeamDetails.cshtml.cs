using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Models.DB.Tables;
using PlayingForKeepers.Models.Web;
using PlayingForKeepers.Pages.Shared;

namespace PlayingForKeepers.Pages.Teams
{
    public class TeamDetailsModel : DI_BasePageModel
    {
        #region Public Properties  
        public List<FF_Leagues> GetLeague { get; set; }
        public List<FF_Teams> GetTeam { get; set; }
        public object GetESPNRoster { get; set; }
        #endregion



        #region Constructor method
        public TeamDetailsModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpClientFactory clientFactory)
            : base(context, authorizationService, userManager, roleManager, clientFactory)
        {
        }
        #endregion



        #region OnGet method
        //Get LeagueTeams, TeamUsers, and LeagueActivity into a list and returns to the page
        public async Task<IActionResult> OnGetAsync(int leagueId)
        {
            GetLeague = await Context.GetLeaguesAsync(leagueId);
            GetTeam = await Context.GetTeamsAsync(leagueId, UserManager.GetUserId(User));
            GetESPNRoster = new object();

            string baseURL = HTTPURI + "2019/segments/0/leagues/584314?forTeamId=2&scoringPeriodId=13&view=mRoster";
            var request = new HttpRequestMessage(HttpMethod.Get, baseURL);
            var webClient = ClientFactory.CreateClient("ESPN");
            var response = await webClient.SendAsync(request);

            if (GetLeague == null)
            {
                ViewData["ErrorMessage"] = $"League not found";
            }

            if (GetLeague == null)
            {
                ViewData["ErrorMessage"] = $"Team not found";
            }

            if (response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();
                var JsonObject = JsonConvert.DeserializeObject<ESPNRoster>(JsonString);

                GetESPNRoster = JsonObject;
            }



            return Page();
        }
        #endregion


        #region OnPost method
        //Submits the CreateLeague form data    
        public async Task<IActionResult> OnPostUpdateTeamAsync(int teamId)
        {

            // Settings
            string returnPath = "./TeamDetails";

            string teamName = GetTeam[0].TeamName;
            string teamAbbr = GetTeam[0].TeamAbbr;
            string userId = UserManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                bool success = await Context.ExecuteSP("FF_UpdateTeam", teamId, teamName, teamAbbr, userId);

                if (success)
                {
                    return RedirectToPage(returnPath);
                }

                ModelState.AddModelError("", "Unable to update team");
            }

            return Page();
        }
        #endregion

    }
}