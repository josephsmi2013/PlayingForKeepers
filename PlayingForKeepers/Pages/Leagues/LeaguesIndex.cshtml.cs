using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Models.DB.Tables;
using PlayingForKeepers.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Pages.Leagues
{
    [Authorize]
    [BindProperties]
    public class LeaguesIndexModel : DI_BasePageModel
    {
        #region Public Properties   
        public List<FF_Leagues> GetLeagues { get; set; }
        public List<FF_Leagues> GetJoinedLeagues { get; set; }
        public List<FF_Leagues> GetUnJoinedLeagues { get; set; }
        #endregion



        #region Constructor method
        public LeaguesIndexModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        //Get Leagues into a list and returns to the page
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = UserManager.GetUserId(User);

            GetLeagues = await this.Context.GetLeaguesAsync();
            GetJoinedLeagues = await this.Context.GetJoinedLeaguesAsync(userId);
            GetUnJoinedLeagues = GetLeagues.Except(GetJoinedLeagues).ToList();

            if (GetLeagues == null)
            {
                ViewData["ErrorMessage"] = $"No leagues have been found";
            }

            return Page();
        }
        #endregion



        #region OnPost method
        //Submits the JoinLeague form data    
        public async Task<IActionResult> OnPostJoinLeagueAsync(int leagueId)
        {



            // Settings
            string returnPath = "./LeaguesIndex";
            string userId = UserManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                bool success = await Context.JoinLeague(leagueId, userId);

                if (success)
                {
                    return RedirectToPage(returnPath);
                }

                ModelState.AddModelError("", "League already joined");
            }

            return Page();
        }
        #endregion



        #region OnPost method
        //Submits the DeleteLeague form data    
        public async Task<IActionResult> OnPostDeleteLeagueAsync(int leagueId)
        {


            // Settings
            string returnPath = "./LeaguesIndex";
            string userId = UserManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                bool success = await Context.DeleteLeague(leagueId);

                if (success)
                {
                    return RedirectToPage(returnPath);
                }

                ModelState.AddModelError("", "League not found");
            }

            return Page();
        }
        #endregion
    }
}