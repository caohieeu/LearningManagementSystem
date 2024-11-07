using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Services.IService
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(SignUpDto model);
        Task<AuthResponseDto> SignInAsync(SignInDto model);
        Task<List<UserResponseDto>> GetAllUser();
        bool CheckToken(string token);
        Task<IntrospectResponseDto> GetInfoUser(string token);
        Task<UserResponseDto> GetUserById(string id);
    }
}
