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
    public class RolesIndexModel : DI_BasePageModel
    {

        #region Public Properties   
        [BindProperty]
        public List<IdentityRole> GetRoles { get; set; }
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
            GetRoles = RoleManager.Roles.ToList();
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostAsync(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);

            if (ModelState.IsValid)
            {
                var result = await RoleManager.DeleteAsync(role);


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