using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PlayingForKeepers.Models.DB.Tables;
using System.Threading.Tasks;

namespace PlayingForKeepers.Authorization
{
    public class LeaguesAdministratorAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, FF_Leagues>
    {

        #region method to handle authorization requirement
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, FF_Leagues resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.LeaguesAdministratorRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
