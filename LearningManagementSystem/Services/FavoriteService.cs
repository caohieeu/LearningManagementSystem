using LearningManagementSystem.DAL;
using LearningManagementSystem.Exceptions;
using LearningManagementSystem.Models;
using LearningManagementSystem.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly LMSContext _context;
        private readonly IUserContext _userContext;
        public FavoriteService(LMSContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
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
