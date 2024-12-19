
using LearningManagementSystem.Models;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Utils.Pagination;
using LearningManagementSystem.Dtos.Response;

namespace LearningManagementSystem.Services.IService
{
    public interface IUserService
    {
        Task<bool> AddUser(SignUpDto? user);
        Task<bool> DeleteUser(string userId);
        Task<List<ApplicationUser>> GetAllUser();
        Task<ApplicationUser> GetUserById(string userId);
        Task<PagedResult<UserResponseDto>> GetUsers(FilterUserDto filter, PaginationParams paginationParams);
    }
}
