using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Services.IService;
using System.Security.Authentication;

namespace LearningManagementSystem.DAL
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAccountService _accountService;
        public UserContext(
            IHttpContextAccessor contextAccessor,
            IAccountService accountService)
        {
            _contextAccessor = contextAccessor;
            _accountService = accountService;
        }
        public async Task<UserResponseDto> GetCurrentUserInfo()
        {
            var httpContext = _contextAccessor.HttpContext;

            var token = httpContext?.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ").Last();

            if(token == null)
            {
                throw new AuthorizationException("Người dùng chưa được xác thực");
            }

            var currentUser = await _accountService.GetInfoUser(token);

            if(currentUser == null)
            {
                throw new AuthorizationException("Người dùng chưa được xác thực");
            }

            return currentUser.User;
        }

        public async Task<string> GetFullName()
        {
            var user = await GetCurrentUserInfo();

            return user.FullName;
        }
        public async Task<IntrospectResponseDto> GetCurrentInforUser()
        {
            var user = await GetCurrentUserInfo();

            IntrospectResponseDto response = new IntrospectResponseDto
            {
                Valid = false,
            };

            if (user == null)
            {
                throw new AuthorizationException("Người dùng chưa được xác thực");
            }

            response.User = user;

            return response;
        }

        public async Task<string> GetId()
        {
            var user = await GetCurrentUserInfo();

            return user.Id;
        }
    }
}
