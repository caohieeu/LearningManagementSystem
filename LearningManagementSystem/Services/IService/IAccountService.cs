using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Services.IService
{
    public interface IAccountService
    {
        Task<IdentityResult> SignUpAsync(SignUpDto model, IFormFile? avatar);
        Task<AuthResponseDto> SignInAsync(SignInDto model);
        Task<List<UserResponseDto>> GetAllUser();
        Task<IntrospectResponseDto> GetInfoUser(string token);
        Task<UserResponseDto> GetUserById(string id);
        Task<bool> SendMailForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ConfirmChangePassword(ConfirmPasswordDto confirmPasswordDto);
    }
}
