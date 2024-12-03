using LearningManagementSystem.DAL;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArgumentException = LearningManagementSystem.Exceptions.ArgumentException;

namespace LearningManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LMSContext _contex;
        public RoleService(RoleManager<IdentityRole> roleManager, 
            LMSContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _contex = context;
        }

        public async Task<bool> AssignPermissionToRole(string? roleName, int? permissionId)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if(role == null)
            {
                throw new NotFoundException("Không tìm thấy vai trò");
            }

            if(permissionId == null)
            {
                throw new ArgumentException("quyền không được để trống");
            }

            var permission = await _contex.Permissions.FindAsync(permissionId);
            if(permission == null)
            {
                throw new NotFoundException("Không tìm thấy quyền");
            }

            var rolePermission = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            };

            _contex.RolePermissions.Add(rolePermission);
            await _contex.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignRoleToUser(string? roleName, string? email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                throw new NotFoundException("Không tìm thấy user");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if(role == null)
            {
                throw new NotFoundException("Không tìm thấy vai trò");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if(result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CreateRole(string? roleName)
        {
            if(string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("yêu cầu nhập tên vai trò");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if(roleExists)
            {
                throw new NotFoundException("Không tìm thấy vai trò");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if(result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRole(string? roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("yêu cầu nhập tên vai trò");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if(role == null)
            {
                throw new NotFoundException("Không tìm thấy vai trò");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Permission>> GetPermissionOfRole(string? roleName)
        {
            return await (from r in _contex.Roles
                    join rp in _contex.RolePermissions on r.Id equals rp.RoleId
                    join p in _contex.Permissions on rp.PermissionId equals p.Id
                    where r.Name == roleName
                    select p).ToListAsync();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> RemovePermissionToRole(string? roleName, int? permissionId)
        {
            if(string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("yêu cầu nhập tên vai trò");
            }

            if (permissionId == null)
            {
                throw new ArgumentException("quyền không được để trống");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new NotFoundException("Không tìm thấy vai trò");
            }
           
            var permission = _contex.RolePermissions.
                Where(pr => pr.PermissionId == permissionId && pr.RoleId == role.Id)
                .FirstOrDefault();
            if (permission == null)
            {
                throw new NotFoundException($"Không tìm thấy quyền {role.Name} trong vai trò {role.Name}");
            }

            _contex.RolePermissions.Remove(permission);
            _contex.SaveChanges();

            return true;
        }

        public async Task<bool> RemoveRoleToUser(string? roleName, string? email)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("yêu cầu nhập tên vai trò");
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("email không được để trống");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new NotFoundException("Không tìm thấy vai trò");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException($"Không tìm thấy người dùng có email là: {email}");
            }

            var userDelete =  _contex.UserRoles
                .Where(ur => ur.UserId == user.Id && ur.RoleId == role.Id)
                .FirstOrDefault();
            if (userDelete == null)
            {
                throw new NotFoundException($"Không tìm thấy người dùng trong vai trò: {role.Name}");
            }

            _contex.UserRoles.Remove(userDelete);
            _contex.SaveChanges();

            return true;
        }
    }
}
