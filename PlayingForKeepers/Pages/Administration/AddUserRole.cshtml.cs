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
    public class AddUserRoleModel : DI_BasePageModel
    {
        #region Public Properties   
        public List<ApplicationUser> GetUsers { get; set; } = new List<ApplicationUser>();
        public List<bool> IsSelected { get; set; } = new List<bool>();
        public string RoleId { get; set; }
        #endregion



        #region contructor method
        public AddUserRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion


        #region OnGet method
        //Gets users not already assigned to the role
        public async Task<IActionResult> OnGetAsync(string roleId)
        {
            RoleId = roleId;
            IdentityRole role = await RoleManager.FindByIdAsync(RoleId);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"Role with Id =  {roleId} cannot be found";
                return Page();
            }
            else
            {
                foreach (ApplicationUser user in UserManager.Users)
                {
                    if (!await UserManager.IsInRoleAsync(user, role.Name))
                    {
                        GetUsers.Add(user);
                        IsSelected.Add(false);
                    }

                }
            }

            return Page();
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostAddUserRoleAsync(string roleId)
        {
            string returnPath = "./EditRole";
            IdentityRole role = await RoleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewData["ErrorMessage"] = $"Role with Id =  {roleId} cannot be found";
                return Page();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    for (int i = 0; i < IsSelected.Count; i++)
                    {
                        if (IsSelected[i])
                        {
                            ApplicationUser user = await UserManager.FindByIdAsync(GetUsers[i].Id);
                            IdentityResult result = await UserManager.AddToRoleAsync(user, role.Name);

                            foreach (IdentityError error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }

                    if (ModelState.ErrorCount == 0)
                    {
                        return RedirectToPage(returnPath, new { roleId = roleId });
                    }

                }
            }

            return Page();
        }
        #endregion
    }
}