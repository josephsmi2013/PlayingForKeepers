using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PlayingForKeepers.Models.DB.Stored_Procs;
using PlayingForKeepers.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using PlayingForKeepers.Authorization;
using PlayingForKeepers.Pages.Shared;

namespace PlayingForKeepers.Pages.Leagues
{
    [Authorize(Roles = "User")]
    [BindProperties]
    public class LeaguesIndexModel : DI_BasePageModel
    {
        #region Public Properties   
        public List<FF_GetLeagues> GetLeagues { get; set; }
        #endregion



        #region Constructor method
        public LeaguesIndexModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        //Get Leagues into a list and returns to the page
        public async Task<IActionResult> OnGetAsync()
        {
            GetLeagues = await this.Context.GetLeaguesAsync();

            if (GetLeagues == null)
            {
                ViewData["ErrorMessage"] = $"No leagues have been found";
            }

            return Page();
        }
        #endregion
    }
}