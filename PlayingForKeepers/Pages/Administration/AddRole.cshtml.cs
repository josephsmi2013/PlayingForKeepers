using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Pages.Shared;
using System.Net.Http;
using System.Threading.Tasks;


namespace PlayingForKeepers.Pages.Administration
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class AddRoleModel : DI_BasePageModel
    {
        #region public properties
        public string RoleName { get; set; }
        #endregion



        #region Constructor method
        public AddRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpClientFactory clientFactory)
            : base(context, authorizationService, userManager, roleManager, clientFactory)
        {
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostAddRoleAsync()
        {
            string returnPath = "./RolesIndex";


            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = RoleName
                };

                IdentityResult result = await RoleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToPage(returnPath);
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
        #endregion

    }
}