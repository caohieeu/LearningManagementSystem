using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Services.IService
{
    public interface INotificationService
    {
        Task<bool> AddNotification(NotificationRequestDto notificationRequestDto);
        Task<bool> CreateNotificationToStudent(NotificationSubRequestDto notificationRequestDto);
        Task<bool> DeleteNotification(int id);
        Task<PagedResult<NotificationResponseDto>> GetPagedNotificationAsync(PaginationParams paginationParams, bool? isRead);
        Task<bool> ReadNotification(int notificationId);
    }
}
