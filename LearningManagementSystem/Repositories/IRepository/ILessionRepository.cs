using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface ILessionRepository : IRepository<Lession>
    {
        Task<LessionResponseDto> GetByTitleAndClass(int titleId, string classId);
    }
}
