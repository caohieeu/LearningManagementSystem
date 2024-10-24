using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IAccountRepository : IRepository<ApplicationUser>
    {
        Task<IdentityResult> SignUpAsync(SignUpDto model);
        Task<ResponseEntity> SignInAsync(SignInDto model);
    }
}
