﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB;
using PlayingForKeepers.Models.DB.Tables;
using PlayingForKeepers.Pages.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayingForKeepers.Pages.Leagues
{
    public class JoinedLeaguesModel : DI_BasePageModel
    {
        #region Public Properties   
        public List<FF_Leagues> GetJoinedLeagues { get; set; }
        #endregion



        #region Constructor method
        public JoinedLeaguesModel(PlayingForKeepersDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            : base(context, authorizationService, userManager, roleManager)
        {
        }
        #endregion



        #region OnGet method
        //Get Leagues into a list and returns to the page
        public async Task<IActionResult> OnGetAsync()
        {
            string userId = UserManager.GetUserId(User);
            GetJoinedLeagues = await this.Context.GetJoinedLeaguesAsync(userId);

            if (GetJoinedLeagues == null)
            {
                ViewData["ErrorMessage"] = $"No leagues have been found";
            }

            return Page();
        }
        #endregion
    }

}