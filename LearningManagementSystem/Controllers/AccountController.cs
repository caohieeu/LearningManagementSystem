using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LearningManagementSystem.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserContext _userContext;
        public AccountController(IAccountService accountService, IUserContext userContext)
        {
            _accountService = accountService;
            _userContext = userContext;
        }
        [HttpPost("SignUp")]
        public Task<IdentityResult> SignUpAsync([FromForm] SignUpDto signUpDto, IFormFile? avatar)
        {
            return _accountService.SignUpAsync(signUpDto, avatar);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInDto signInDto)
        {
            var response = await _accountService.SignInAsync(signInDto);
            if(!response.result)
            {
                return Unauthorized(new ResponseEntity
                {
                    code = ErrorCode.Unauthorized.GetErrorInfo().code,
                    message = ErrorCode.Unauthorized.GetErrorInfo().message,
                    data = response
                });
            }
            return Ok(new ResponseEntity
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = response
            });
        }
        [HttpPost("Auth/ForgotPassword")]
        public async Task<IActionResult> SendMailForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var response = await _accountService.SendMailForgotPassword(forgotPasswordDto);
            if(response)
            {
                return Ok("Gửi mail thành công, vui lòng kiểm tra email của bạn");
            }
            else
            {
                return BadRequest("Lỗi xảy ra, vui lòng thử lại sau");
            }
        }
        [HttpPost("Auth/ConfirmChangePassword")]
        public async Task<IActionResult> ConfirmChangePassword(ConfirmPasswordDto confirmPasswordDto)
        {
            var response = await _accountService.ConfirmChangePassword(confirmPasswordDto);
            if (response)
            {
                return Ok(new ResponseEntity
                {
                    code = 200,
                    message = "Đổi mật khẩu thành công",
                });
            }
            else
            {
                return BadRequest(new ResponseEntity
                {
                    code = (int)HttpStatusCode.BadRequest,
                    message = "Có lỗi xảy ra vui lòng thử lại sau",
                });
            }
        }
        [HttpPut]
        public async Task<ResponseEntity> UpdateAccount()
        {
            return new ResponseEntity()
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                //data = await _accountService.UpdateAccount()
            };
        }
        [HttpGet("GetInfo")]
        public async Task<IActionResult> GetInfo()
        {
            var result = await _userContext.GetCurrentInforUser();
            var response = new ResponseEntity()
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _userContext.GetCurrentInforUser()
            };

            if (!result.Valid)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

    }
}
