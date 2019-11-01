using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PlayingForKeepers.Models.DB.Stored_Procs;
using PlayingForKeepers.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using PlayingForKeepers.Authorization;



namespace PlayingForKeepers.Pages.Leagues
{
    public class IndexModel : DI_BasePageModel
    {

        #region Public Properties   
        [BindProperty]
        public List<FF_GetLeagues> GetLeagues { get; set; }
        #endregion



        #region Constructor method
        public IndexModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }
        #endregion



        #region OnGet method
        //Get Leagues into a list and returns to the page
        public async Task OnGetAsync()
        {
            GetLeagues = await this.Context.GetLeaguesAsync();
        }
        #endregion
    }
}