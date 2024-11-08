﻿using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public Task<IdentityResult> SignUpAsync([FromBody] SignUpDto signUpDto)
        {
            return _accountService.SignUpAsync(signUpDto);
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
        [HttpGet("GetAll")]
        public async Task<ResponseEntity> GetAll()
        {
            return new ResponseEntity()
            {
                code = ErrorCode.NoError.GetErrorInfo().code,
                message = ErrorCode.NoError.GetErrorInfo().message,
                data = await _accountService.GetAllUser()
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
