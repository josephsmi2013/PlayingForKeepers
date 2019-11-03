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
        public List<IdentityRole> Roles { get; set; }
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

    }
}