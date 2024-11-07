using LearningManagementSystem.Dtos.Response;

namespace LearningManagementSystem.DAL
{
    public interface IUserContext
    {
        Task<UserResponseDto> GetCurrentUserInfo();
        Task<string> GetFullName();
        Task<string> GetId();
        Task<IntrospectResponseDto> GetCurrentInforUser();
    }
}
