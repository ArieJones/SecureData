using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ContactManager.Authorization
{
    public class ContactHasOneAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {
        protected override void Handle(AuthorizationContext context, OperationAuthorizationRequirement requirement, Contact resource)
        {

            if (resource == null)
            {
                return;
            }

            if (string.CompareOrdinal(requirement.Name, "ContainsOne") != 0)
            {
                return;
            }

            if (resource.Address.Contains("1"))
            {
                context.Succeed(requirement);
            }
        }
    }
}
