using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Services.IService
{
    public interface IAccountService
    {
       Task<IdentityResult> SignUpAsync(SignUpDto model);
        Task<ResponseEntity> SignInAsync(SignInDto model);
        Task<List<UserResponseDto>> GetAllUser();
    }
}
