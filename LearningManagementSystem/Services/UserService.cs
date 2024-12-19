using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, 
            IMapper mapper, LMSContext context)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
        }

        public Task<bool> AddUser(SignUpDto? user)
        {
            return _userRepository.AddUser(user);
        }

        public Task<bool> DeleteUser(string userId)
        {
            return _userRepository.DeleteUser(userId);
        }

        public async Task<List<ApplicationUser>> GetAllUser()
        {
            return (List<ApplicationUser>)await _userRepository.GetAll();
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<PagedResult<UserResponseDto>> GetUsers(FilterUserDto filter, PaginationParams paginationParams)
        {
            var users = await _userRepository.GetUsers(filter, paginationParams);

            var result = new PagedResult<UserResponseDto>(
                new List<UserResponseDto>(), users.TotalItems,
                users.PageNumber, users.PageSize);

            foreach (var user in users.Items)
            {
                var item = _mapper.Map<UserResponseDto>(user);
                item.Role = (from ur in _context.UserRoles
                             join r in _context.Roles on ur.RoleId equals r.Id
                             where ur.UserId == user.Id
                             select r.Name).FirstOrDefault() ?? "";

                result.Items.Add(item);
            }

            return result;
        }
    }
}
