using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class EditRoleModel : DI_BasePageModel
    {
        #region public properties        
        public string RoleId { get; set; }
        public string RoleName { get; set; }    
        
        public List<string> Users { get; set; }
        #endregion



        #region Constructor method
        public EditRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
            Users = new List<string>();
        }
        #endregion



        #region OnGet method
        public async Task<IActionResult> OnGetAsync(string roleId)
        {            
            var role = await RoleManager.FindByIdAsync(roleId);

            RoleId = role.Id;
            RoleName = role.Name;

            foreach(var user in UserManager.Users)
            {
                if(await UserManager.IsInRoleAsync(user, role.Name))
                {
                    Users.Add(user.UserName);
                }
            }

            return Page();
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostAsync(string roleId)
        {
            string returnPath = "./RolesIndex";
            var role = await RoleManager.FindByIdAsync(roleId);

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

            return Page();
        }
        #endregion
    }
}