using LearningManagementSystem.Models;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.GetRoles()
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string? roleName)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.CreateRole(roleName)
            });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string? roleName)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.DeleteRole(roleName)
            });
        }
        [Authorize(Policy = "AddAuthorizationPermission")]
        [HttpPost("AssignPermissionToRole")]
        public async Task<IActionResult> AssignPermissionToRole(string? roleName, int permissionId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.AssignPermissionToRole(roleName, permissionId)
            });
        }
        [Authorize(Policy = "ViewAuthorizationPermission")]
        [HttpGet("GetPermissionOfRole")]
        public async Task<IActionResult> GetPermissionOfRole(string? roleName)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.GetPermissionOfRole(roleName)
            });
        }
        [HttpDelete("RemovePermissionToRole")]
        public async Task<IActionResult> RemovePermissionToRole(string? roleName, int permissionId)
        {
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.RemovePermissionToRole(roleName, permissionId)
            });
        }
        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(string? roleName, string? email)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("yêu cầu nhập tên vai trò");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("yêu cầu nhập email");
            }
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.AssignRoleToUser(roleName, email)
            });
        }
        [HttpDelete("RemoveRoleToUser")]
        public async Task<IActionResult> RemoveRoleToUser(string? roleName, string? email)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("yêu cầu nhập tên vai trò");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("yêu cầu nhập email");
            }
            return Ok(new ResponseEntity
            {
                code = 200,
                message = "Thực hiện thành công",
                data = await _roleService.RemoveRoleToUser(roleName, email)
            });
        }
    }
}
