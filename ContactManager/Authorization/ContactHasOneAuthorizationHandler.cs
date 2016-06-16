//#define HasOne
#if HasOne

using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ContactManager.Authorization
{
    public class ContactHasOneAuthorizationHandler 
                : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {
        protected override void Handle(AuthorizationContext context, 
                OperationAuthorizationRequirement requirement, Contact resource)
        {

            if (resource == null)
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

#endif
// Details will be shown if you are the owner or if the address contains 1