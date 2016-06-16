using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ContactManager.Authorization
{
    public class ContactRoleAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {
        protected override void Handle(AuthorizationContext context,
                    OperationAuthorizationRequirement requirement, Contact resource)
        {
            if (context.User == null)
            {
                return;
            }

            if (context.User.IsInRole(requirement.Name))
            {
                context.Succeed(requirement);
            }
        }
    }
}
