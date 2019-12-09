using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using System.Net.Http;

namespace PlayingForKeepers.Pages.Shared
{
    public class DI_BasePageModel : PageModel
    {
        protected PlayingForKeepersDbContext Context { get; }

        protected IAuthorizationService AuthorizationService { get; }

        protected UserManager<ApplicationUser> UserManager { get; }

        protected RoleManager<IdentityRole> RoleManager { get; }

        protected readonly IHttpClientFactory ClientFactory;

        protected const string HTTPURI = "apis/v3/games/ffl/seasons/";

        public DI_BasePageModel(
               PlayingForKeepersDbContext context, 
               IAuthorizationService authorizationService, 
               UserManager<ApplicationUser> userManager, 
               RoleManager<IdentityRole> roleManager,
               IHttpClientFactory clientFactory
            ) : base()
        {
            Context = context;
            AuthorizationService = authorizationService;
            UserManager = userManager;
            RoleManager = roleManager;
            ClientFactory = clientFactory;
        }
    }
}
