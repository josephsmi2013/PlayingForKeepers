using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Pages.Shared;

namespace PlayingForKeepers.Pages.Administration
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "User")]
    public class DeleteRoleModel : DI_BasePageModel
    {
        #region contructor method
        public DeleteRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion


        #region OnPost method
        public async Task<IActionResult> OnGetAsync(string id)
        {
            string returnPath = "./RolesIndex";
            var role = await RoleManager.FindByIdAsync(id);

            if (ModelState.IsValid)
            {
                var result = await RoleManager.DeleteAsync(role);

                if(result.Succeeded)
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