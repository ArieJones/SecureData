using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ContactManager
{
    public static class ContactOperationsRequirements
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = "canCreate" };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = "canRead" };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = "canUpdate" };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = "canDelete" };
    }
}
