using Microsoft.AspNetCore.Mvc;
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
    public class AddLeagueModel : DI_BasePageModel
    {
        #region Public Properties   
        public FF_AddLeague AddLeague { get; set; }
        #endregion



        #region Constructor method
        public AddLeagueModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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
        public async Task<IActionResult> OnPostAsync()
        {

            // Verification
            if (!ModelState.IsValid)
            {
                return Page();
            }


            // Settings
            string returnPath = "./LeaguesIndex";

            string leagueName = AddLeague.LeagueName;
            int legueTeamsPossible = AddLeague.LeagueTeamsPossible;

            int charCounter = UserManager.GetUserName(User).IndexOf("@");
            string leagueOwner = UserManager.GetUserName(User).Substring(0, charCounter);

            LeagueStatusListSP leagueStatus = LeagueStatusListSP.Approved;

            if(ModelState.IsValid)
            {
                bool success = await Context.AddLeague(leagueName, legueTeamsPossible, leagueOwner, leagueStatus);

                if(success)
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