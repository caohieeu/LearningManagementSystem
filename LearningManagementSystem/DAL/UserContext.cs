using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Services.IService;

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
                throw new AuthorizationException("Token không không hợp lệ");
            }

            var currentUser = await _accountService.GetInfoUser(token);

            return currentUser.User;
        }

        public async Task<string> GetFullName()
        {
            var user = await GetCurrentUserInfo();

            return user.FullName;
        }
        public async Task<IntrospectResponseDto> GetCurrentInforUser()
        {
            IntrospectResponseDto response = new IntrospectResponseDto
            {
                Valid = true,
                User = await GetCurrentUserInfo()
            };

            return response;
        }

        public async Task<string> GetId()
        {
            var user = await GetCurrentUserInfo();

            return user.Id;
        }
    }
}
