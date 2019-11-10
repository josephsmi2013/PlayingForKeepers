using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PlayingForKeepers.Models;
using PlayingForKeepers.Models.DB.Tables;
using System.Threading.Tasks;

namespace PlayingForKeepers.Authorization
{
    public class LeaguesIsOwnerAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, FF_Leagues>
    {
        #region public properties
        public UserManager<ApplicationUser> _userManager { get; set; }
        #endregion



        #region  constructor method
        public LeaguesIsOwnerAuthHandler(UserManager<ApplicationUser>
            userManager)
        {
            _userManager = userManager;
        }
        #endregion



        #region method to handle authorization requirement
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   FF_Leagues resource)
        {
            if (context.User == null || resource == null)
            {
                // Return Task.FromResult(0) if targeting a version of
                // .NET Framework older than 4.6:
                return Task.CompletedTask;
            }

            // If we're not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.LeagueOwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
