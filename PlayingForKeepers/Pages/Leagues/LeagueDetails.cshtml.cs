using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize]
    [BindProperties]
    public class LeagueDetailsModel : DI_BasePageModel
    {
        #region Public Properties  
        public int LeagueID { get; set; }
        public List<FF_LeagueActivity> GetLeagueActivity { get; set; }
        public List<FF_LeagueTeams> GetLeagueTeams { get; set; }        
        public List<FF_Teams> GetTeams { get; set; }
        public List<FF_Teams> GetTeamsFiltered { get; set; }
        #endregion



        #region Constructor method
        public LeagueDetailsModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        //Get LeagueTeams, TeamUsers, and LeagueActivity into a list and returns to the page
        public async Task<IActionResult> OnGetAsync(int leagueId)
        {
            LeagueID = leagueId;
            GetLeagueActivity = await Context.GetLeagueActivityAsync(LeagueID);
            GetLeagueTeams = await Context.GetLeagueTeamsAsync(LeagueID);            
            GetTeams = await Context.GetTeamsAsync();

            GetTeamsFiltered = (from t in GetTeams
                                join lt in GetLeagueTeams
                                on t.TeamID equals lt.TeamID
                                select t).ToList();

            if (GetLeagueActivity == null)
            {
                ViewData["ErrorMessage"] = $"No league activity has been found";
            }

            if (GetLeagueTeams == null)
            {
                ViewData["ErrorMessage"] = $"No league teams have been found";
            }

            if (GetTeams == null)
            {
                ViewData["ErrorMessage"] = $"No team definitions have been found";
            }





            return Page();
        }
        #endregion



        #region OnPost method
        //Submits the JoinTeam form data    
        public async Task<IActionResult> OnPostJoinTeamAsync(int teamId, int leagueId)
        {



            // Settings
            string returnPath = "./LeagueDetails";
            string userId = UserManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                bool success = await Context.JoinTeam(teamId, userId);

                if (success)
                {
                    return RedirectToPage(returnPath, new { LeagueID = leagueId });
                }

                ModelState.AddModelError("", "One or more teams in this league is already assigned to you");
            }

            return Page();
        }
        #endregion



        #region OnPost method
        //Submits the LeaveTeam form data    
        public async Task<IActionResult> OnPostLeaveTeamAsync(int teamId, int leagueId)
        {



            // Settings
            string returnPath = "./LeagueDetails";
            string userId = UserManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                bool success = await Context.LeaveTeam(teamId, userId);

                if (success)
                {
                    return RedirectToPage(returnPath, new { LeagueID = leagueId });
                }

                ModelState.AddModelError("", "You are not the owner of this team");
            }

            return Page();
        }
        #endregion
    }
}