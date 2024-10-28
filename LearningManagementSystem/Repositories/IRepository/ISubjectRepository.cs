using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<List<Subject>> GetAllInclude();
        Task<Subject?> GetSubjectById(string id);
    }
}
