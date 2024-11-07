namespace LearningManagementSystem.Services.IService
{
    public interface IFavoriteService
    {
        Task<bool> PostFavorite(int id, string type);
        Task<bool> RemoveFavorite(int id, string type);
        Task<int> GetFavoriteByItem(int idItem, string type);
    }
}
