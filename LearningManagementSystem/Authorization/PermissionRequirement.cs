using Microsoft.AspNetCore.Authorization;

namespace LearningManagementSystem.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; set; }
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
