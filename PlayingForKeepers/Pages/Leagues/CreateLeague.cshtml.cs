using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models.DB.Stored_Procs;
using PlayingForKeepers.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using PlayingForKeepers.Authorization;

namespace PlayingForKeepers.Pages.Leagues
{
    public class CreateLeagueModel : DI_BasePageModel
    {

        #region Public Properties   
        [BindProperty]
        public FF_AddLeague AddLeague { get; set; }
        #endregion



        #region Constructor method
        public CreateLeagueModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
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



        #region OnPost method.
        //Submits the CreateLeague form data    
        public async Task<IActionResult> OnPostAsync()
        {

            // Verification
            if (!ModelState.IsValid)
            {
                return Page();
            }


            // Settings
            string returnPath = "./Index";
            string leagueName = AddLeague.LeagueName;
            int legueTeamsPossible = AddLeague.LeagueTeamsPossible;
            int charCounter = UserManager.GetUserName(User).IndexOf("@");
            string leagueOwner = UserManager.GetUserName(User).Substring(0, charCounter);
            LeagueStatusListSP leagueStatus = LeagueStatusListSP.Approved;


            // requires using PlayingForKeepers.Authorization;
            //var isAuthorized = await AuthorizationService.AuthorizeAsync(User, AddLeague, LeaguesOperations.Create);

            //if (!isAuthorized.Succeeded)
            //{
            //    return new ChallengeResult();
            //}

            await Context.AddLeague(leagueName, legueTeamsPossible, leagueOwner, leagueStatus);

            return RedirectToPage(returnPath);

        }
        #endregion
    }
}