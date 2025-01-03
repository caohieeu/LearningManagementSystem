﻿using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IAccountRepository : IRepository<ApplicationUser>
    {
        Task<IdentityResult> SignUpAsync(SignUpDto model, IFormFile? avatar);
        Task<AuthResponseDto> SignInAsync(SignInDto model);
        Dictionary<string, string> GetTokenInfo(string token);
        Task<UserResponseDto> GetByUsername(string username);
    }
}
