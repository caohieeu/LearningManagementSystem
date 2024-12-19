using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArgumentException = LearningManagementSystem.Exceptions.ArgumentException;

namespace LearningManagementSystem.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly LMSContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(LMSContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddUser(SignUpDto? user)
        {
            var userExist = _context.Users
                .Where(u => u.Email == user.Email || u.UserName == user.UserName)
                .FirstOrDefault();

            if (userExist != null)
            {
                throw new AlreadyExistException("Đã tồn tại người dùng");
            }

            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender,
                DepartmentId = user.DepartmentId,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                if(!string.IsNullOrEmpty(user.Role))
                {
                    if (!await _roleManager.RoleExistsAsync(user.Role))
                    {
                        throw new NotFoundException($"Không tồn tại vai trò {user.Role}");
                    }
                    await _userManager.AddToRoleAsync(newUser, user.Role);
                }

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("yêu cầu nhập user id");
            }
            
            var user = _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                throw new NotFoundException("Không tìm thấy người dùng");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<ApplicationUser>> GetUsers(FilterUserDto filter, PaginationParams paginationParams)
        {
            List<ApplicationUser> questions = new List<ApplicationUser>();
            var query = _context.Set<ApplicationUser>().AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                query = query.Where(q => q.UserName.Contains(filter.SearchString) || 
                q.Email.Contains(filter.SearchString) || q.Id.Contains(filter.SearchString));
            }
            if(!string.IsNullOrEmpty(filter.RoleId))
            {
                var userIds = _context.UserRoles
                    .Where(ur => ur.RoleId == filter.RoleId)
                    .Select(ur => ur.UserId)
                    .ToList();

                query = query.Where(u => userIds.Contains(u.Id));
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<ApplicationUser>(items.ToList(), totalItems,
                paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
