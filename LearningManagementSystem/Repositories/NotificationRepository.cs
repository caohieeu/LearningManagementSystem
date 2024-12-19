using AutoMapper;
using LearningManagementSystem.DAL;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;
using LearningManagementSystem.Utils.Pagination;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly LMSContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserContext _userContext;
        public NotificationRepository(
            LMSContext context, 
            IMapper mapper,
            IAccountService accountService,
            IUserContext userContext) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
            _userContext = userContext;
        }
        public async Task<PagedResult<NotificationResponseDto>> GetPagedNotificationAsync(PaginationParams paginationParams)
        {
            var query = _context.Notifications.AsQueryable();
            var userId = await _userContext.GetId();

            int totalItems = await query.CountAsync();

            var items = await query
                .Where(n => _context.UserNotifications
                    .Any(un => un.NotificationId == n.Id &&
                        un.UserId == userId))
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            var listNoification = new List<NotificationResponseDto>();
            foreach (var item in items)
            {
                var user = _context.UserNotifications
                    .Where(u => u.NotificationId == item.Id)
                    .First();
                var notification = _mapper.Map<NotificationResponseDto>(item);
                notification.User = await _accountService.GetUserById(user.UserActive);

                listNoification.Add(notification);
            }

            return new PagedResult<NotificationResponseDto>(listNoification, totalItems, 
                paginationParams.PageNumber, paginationParams.PageSize);
        }
        public async Task<bool> AddNotification(NotificationRequestDto notificationRequestDto)
        {
            var notification = new Notification
            {
                Title = notificationRequestDto.Title,
                Detail = notificationRequestDto.Detail,
                DateCreated = DateTime.Now,
                Name = notificationRequestDto.Name ?? "thông báo",
                IsReaded = false
            };

            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            foreach(var id in notificationRequestDto.UsersId)
            {
                _context.UserNotifications
                    .Add(new UserNotification
                    {
                        UserId = id,
                        NotificationId = notification.Id,
                        UserActive = await _userContext.GetId()
                    });
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> RemoveNotification(int id)
        {
            var user = await _userContext.GetCurrentUserInfo();

            var notification = _context.UserNotifications
                .Where(x => x.UserId == user.Id && x.NotificationId == id)
                .FirstOrDefault();

            if(notification == null)
            {
                throw new NotFoundException("Không tìm thấy thông báo");
            }

            _context.UserNotifications.Remove(notification);
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PagedResult<NotificationResponseDto>> GetPagedNotificationBySubjectAsync(PaginationParams paginationParams, string subjectId)
        {
            var query = _context.Notifications.AsQueryable();
            var userId = await _userContext.GetId();

            int totalItems = await query.CountAsync();

            var items = await query
                .Where(n => _context.UserNotifications
                    .Any(un => un.NotificationId == n.Id &&
                        un.UserId == userId))
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            var listNoification = new List<NotificationResponseDto>();
            foreach (var item in items)
            {
                var user = _context.UserNotifications
                    .Where(u => u.NotificationId == item.Id)
                    .First();
                var notification = _mapper.Map<NotificationResponseDto>(item);
                notification.User = await _accountService.GetUserById(user.UserActive);

                listNoification.Add(notification);
            }

            return new PagedResult<NotificationResponseDto>(listNoification, totalItems,
                paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
