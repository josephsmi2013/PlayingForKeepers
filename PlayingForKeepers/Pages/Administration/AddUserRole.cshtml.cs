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
    [BindProperties]
    public class AddUserRoleModel : DI_BasePageModel
    {
        #region Public Properties   
        public List<IdentityUser> GetUsers { get; set; }
        public List<bool> IsSelected { get; set; }
        public string RoleId { get; set; }
        #endregion



        #region contructor method
        public AddUserRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
            GetUsers = new List<IdentityUser>();
            IsSelected = new List<bool>();

        }
        #endregion


        #region OnGet method
        //Gets users not already assigned to the role
        public async Task OnGetAsync(string roleId)
        {
            RoleId = roleId;
            IdentityRole role = await RoleManager.FindByIdAsync(RoleId);
 

            foreach(IdentityUser user in UserManager.Users)
            {
                if(!await UserManager.IsInRoleAsync(user, role.Name))
                {
                    GetUsers.Add(user);
                    IsSelected.Add(false);
                }

            }
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostAsync(string roleId)
        {
            string returnPath = "./EditRole";
            IdentityRole role = await RoleManager.FindByIdAsync(roleId);

            if (ModelState.IsValid)
            {

                for(int i = 0; i < IsSelected.Count; i++)
                {
                    if(IsSelected[i])
                    {
                        IdentityUser user = await UserManager.FindByIdAsync(GetUsers[i].Id);
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

            return Page();
        }
        #endregion
    }
}