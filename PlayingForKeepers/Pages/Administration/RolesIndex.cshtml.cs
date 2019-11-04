using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayingForKeepers.Pages.Administration
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "User")]
    [BindProperties]
    public class RolesIndexModel : DI_BasePageModel
    {
        #region Public Properties   
        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        #endregion


        #region contructor method
        public RolesIndexModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        public void OnGet()
        {
            Roles = RoleManager.Roles.ToList();
        }
        #endregion



        #region DeleteRoleAsync method
        public async Task<IActionResult> OnPostDeleteRoleAsync(string roleId)
        {
            string returnPath = "./RolesIndex";
            var role = await RoleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"Role with Id =  {roleId} cannot be found";
                return Page();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = await RoleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToPage(returnPath);
                    }


                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return Page();
        }
        #endregion

    }
}