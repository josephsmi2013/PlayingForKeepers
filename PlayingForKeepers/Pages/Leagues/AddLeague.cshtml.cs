using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Models.DB.Tables;
using PlayingForKeepers.Pages.Shared;
using System.Threading.Tasks;

namespace PlayingForKeepers.Pages.Leagues
{
    [Authorize]
    [BindProperties]
    public class AddLeagueModel : DI_BasePageModel
    {
        #region Public Properties   
        public FF_Leagues AddLeague { get; set; }
        #endregion



        #region Constructor method
        public AddLeagueModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        //Returns to the page after submit
        public IActionResult OnGet()
        {

            return Page();
        }
        #endregion



        #region OnPost method
        //Submits the CreateLeague form data    
        public async Task<IActionResult> OnPostAddLeagueAsync()
        {

            // Settings
            string returnPath = "./LeaguesIndex";

            string leagueName = AddLeague.LeagueName;
            int legueTeamsPossible = AddLeague.LeagueTeamsPossible;

            int charCounter = UserManager.GetUserName(User).IndexOf("@");
            string leagueOwner = UserManager.GetUserName(User).Substring(0, charCounter);

            int leagueStatus = (int)LeagueStatus.Approved;



            if (ModelState.IsValid)
            {
                bool success = await Context.ExecuteSP("FF_AddLeague", leagueName, legueTeamsPossible, leagueOwner, leagueStatus);

                if (success)
                {
                    return RedirectToPage(returnPath);
                }

                ModelState.AddModelError("", "League name already exists");
            }

            return Page();
        }
        #endregion
    }
}