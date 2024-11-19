using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<bool> AddNotification(NotificationRequestDto notificationRequestDto);
        Task<bool> RemoveNotification(int id);
        Task<PagedResult<NotificationResponseDto>> GetPagedNotificationAsync(PaginationParams paginationParams);
    }
}
