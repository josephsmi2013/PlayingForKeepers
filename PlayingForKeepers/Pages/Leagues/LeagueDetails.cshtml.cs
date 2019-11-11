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
        public List<FF_LeagueActivity> GetLeagueActivity { get; set; }
        #endregion



        #region Constructor method
        public LeagueDetailsModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        //Get Leagues into a list and returns to the page
        public async Task<IActionResult> OnGetAsync(int leagueId)
        {
            string userId = UserManager.GetUserId(User);

            GetLeagueActivity = await this.Context.GetLeagueActivityAsync(leagueId);

            if (GetLeagueActivity == null)
            {
                ViewData["ErrorMessage"] = $"No league activity has been found";
            }

            return Page();
        }
        #endregion
    }
}