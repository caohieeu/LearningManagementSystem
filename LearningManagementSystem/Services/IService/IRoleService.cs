using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Services.IService
{
    public interface IRoleService
    {
        Task<bool> AssignPermissionToRole(string? roleName, int? permissionId);
        Task<bool> AssignRoleToUser(string? roleName, string? email);
        Task<bool> CreateRole(string? roleName);
        Task<bool> DeleteRole(string? roleName);
        Task<List<Permission>> GetPermissionOfRole(string? roleName);
        Task<List<IdentityRole>> GetRoles();
        Task<bool> RemovePermissionToRole(string? roleName, int? permissionId);
        Task<bool> RemoveRoleToUser(string roleName, string email);
    }
}
