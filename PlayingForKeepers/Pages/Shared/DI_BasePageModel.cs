using PlayingForKeepers.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlayingForKeepers.Pages.Shared
{
    public class DI_BasePageModel : PageModel
    {
        protected PlayingForKeepersDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<IdentityUser> UserManager { get; }

        protected RoleManager<IdentityRole> RoleManager { get; }

        public DI_BasePageModel(PlayingForKeepersDbContext context,IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
            : base()
            {
                Context = context;
                AuthorizationService = authorizationService;
                UserManager = userManager;
                RoleManager = roleManager;
            }
    }
}
