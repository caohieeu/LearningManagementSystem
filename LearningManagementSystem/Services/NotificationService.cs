using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ISubjectService _subjectService;
        public NotificationService(
            INotificationRepository notificationRepository, 
            ISubjectService subjectService)
        {
            _notificationRepository = notificationRepository;
            _subjectService = subjectService;
        }
        public async Task<bool> AddNotification(NotificationRequestDto notificationRequestDto)
        {
            return await _notificationRepository.AddNotification(notificationRequestDto);
        }

        public async Task<bool> CreateNotificationToStudent(NotificationSubRequestDto notification)
        {
            var subject = await _subjectService.GetSubject(notification.SubjectId);

            return await _notificationRepository.AddNotification(new NotificationRequestDto
            {
                Title = notification.Title,
                Detail = notification.Detail,
                UsersId = notification.UsersId,
                Name = $"đã cập nhật thông báo trong môn {subject.Name}",   
            });
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notification = await _notificationRepository.GetById(id);

            if(notification == null)
            {
                throw new NotFoundException("Không tìm thấy thông báo");
            }

            return await _notificationRepository.Remove(notification);
        }

        public async Task<PagedResult<NotificationResponseDto>> GetPagedNotificationAsync(PaginationParams paginationParams, bool? isRead)
        {
            var notification = await _notificationRepository.GetPagedNotificationAsync(paginationParams);

            if(isRead != null)
            {
                if (isRead == true)
                {
                    notification.Items = notification.Items.Where(n => n.IsReaded).ToList();
                }
                else
                {
                    notification.Items = notification.Items.Where(n => !n.IsReaded).ToList();

                }
            }

            return notification;
        }

        public async Task<PagedResult<NotificationResponseDto>> GetPagedNotificationBySubjectAsync(PaginationParams paginationParams, string subjectId)
        {
            return await 
                _notificationRepository.GetPagedNotificationBySubjectAsync(paginationParams, subjectId);
        }

        public async Task<bool> ReadNotification(int notificationId)
        {
            var notification = await _notificationRepository.GetById(notificationId);

            if(notification == null)
            {
                throw new NotFoundException("Không tìm thấy thông báo");
            }
            notification.IsReaded = true;

            return await _notificationRepository.Update(notification);
        }
    }
}
