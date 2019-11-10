using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayingForKeepers.Pages.Administration
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class EditRoleModel : DI_BasePageModel
    {
        #region public properties        
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<string> Users { get; set; } = new List<string>();
        #endregion



        #region Constructor method
        public EditRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        public async Task<IActionResult> OnGetAsync(string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"Role with Id =  {roleId} cannot be found";
                return Page();
            }
            else
            {
                RoleId = role.Id;
                RoleName = role.Name;

                foreach (var user in UserManager.Users)
                {
                    if (await UserManager.IsInRoleAsync(user, role.Name))
                    {
                        Users.Add(user.UserName);
                    }
                }
            }

            return Page();
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostEditRoleAsync(string roleId)
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
                    role.Name = RoleName;
                    var result = await RoleManager.UpdateAsync(role);

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