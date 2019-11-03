using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Pages.Shared;


namespace PlayingForKeepers.Pages.Administration
{
    [Authorize(Roles ="Admin")]
    [Authorize(Roles ="User")]
    [BindProperties]
    public class AddRoleModel : DI_BasePageModel
    {
        #region public properties
        public string RoleName { get; set; }
        #endregion



        #region Constructor method
        public AddRoleModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        public IActionResult OnGet()
        {

            return Page();
        }
        #endregion



        #region OnPost method
        public async Task<IActionResult> OnPostAsync()
        {
            string returnPath = "./RolesIndex";


            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = RoleName
                };

                IdentityResult result = await RoleManager.CreateAsync(identityRole);  
                
                if(result.Succeeded)
                {
                    return RedirectToPage(returnPath);
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }            

            return Page();
        }
        #endregion

    }
}