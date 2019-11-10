using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PlayingForKeepers.Models.DB.Tables;
using System.Threading.Tasks;

namespace PlayingForKeepers.Authorization
{
    public class LeaguesManagerAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, FF_Leagues>
    {

        #region method to handle authorization requirement
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, FF_Leagues resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if (requirement.Name != Constants.ApproveOperationName &&
                requirement.Name != Constants.RejectOperationName)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.LeaguesManagerRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
