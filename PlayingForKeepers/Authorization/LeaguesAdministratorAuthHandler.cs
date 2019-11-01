using System.Threading.Tasks;
using PlayingForKeepers.Models.DB.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PlayingForKeepers.Authorization
{
    public class LeaguesAdministratorAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, FF_Leagues>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     FF_Leagues resource)
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
    }
}
