    using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface ITitleRepository : IRepository<Title>
    {
        Task<bool> AddTitle(TitleRequestDto title);
        Task<List<Title>> GetBySubject(string id);
        Task<bool> DeleteById(int id);
    }
}
