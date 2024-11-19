using LearningManagementSystem.DAL;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Services.IService;
using Microsoft.EntityFrameworkCore;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly LMSContext _context;
        private readonly IUserContext _userContext;
        private readonly INotificationService _notificationService;
        public FavoriteService(
            LMSContext context, 
            IUserContext userContext,
            INotificationService notificationService)
        {
            _context = context;
            _userContext = userContext;
            _notificationService = notificationService;
        }

        public async Task<int> GetFavoriteByItem(int idItem, string type)
        {
            var list = await _context.Favorites
                .Where(x => x.ItemId == idItem && x.ItemType == type)
                .ToListAsync();

            return list.Count;
        }

        public async Task<bool> PostFavorite(int id, string type)
        {
            try
            {
                await _context.Favorites.AddAsync(new Favorite
                {
                    ItemId = id,
                    UserId = await _userContext.GetId(),
                    CreatedAt = DateTime.Now,
                    ItemType = type
                });

                await _context.SaveChangesAsync();

                var userId = "";
                if(type == TypeQA.question)
                {
                    userId = _context.Questions
                        .Where(q => q.Id == id)
                        .Select(q => q.UserId)
                        .FirstOrDefault();
                }
                else
                {
                    userId = _context.Answers
                        .Where(q => q.Id == id)
                        .Select(q => q.UserId)
                        .FirstOrDefault();
                }

                //add notification when favorite QA
                await _notificationService.AddNotification(new NotificationRequestDto
                {
                    Title = "Thích bình luận",
                    Detail = "Có người đã thích bình luận của bạn",
                    Name = "đã thích bình luận của bạn",
                    UsersId = new List<string>(new string[]
                    {
                        userId ?? ""
                    })
                });

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoveFavorite(int id, string type)
        {
            try
            {
                var favorite = _context.Favorites
                    .Where(x => x.ItemId == id && x.ItemType == type)
                    .FirstOrDefault();

                if(favorite == null)
                {
                    throw new NotFoundException("Không tìm thấy nội dung");
                }
               
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
