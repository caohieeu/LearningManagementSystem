using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<bool> AddUser(SignUpDto? user);
        Task<bool> DeleteUser(string userId);
        Task<ApplicationUser> GetUserById(string userId);
        Task<PagedResult<ApplicationUser>> GetUsers(FilterUserDto filter, PaginationParams paginationParams);
    }
}
