using LearningManagementSystem.Dtos;
using LearningManagementSystem.Services;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize(Policy = "ViewUserPermission")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _userService.GetAllUser()
            });
        }
        [HttpGet("GetUsers")]
        [Authorize(Policy = "ViewUserPermission")]
        public async Task<IActionResult> GetUsers([FromQuery] FilterUserDto filter,
            [FromQuery] PaginationParams paginationParams)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _userService.GetUsers(filter, paginationParams)
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(SignUpDto? user)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _userService.AddUser(user)
            });
        }
        [HttpDelete]
        [Authorize(Policy = "EditUserPermission")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _userService.DeleteUser(userId)
            });
        }
    }
}
