using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetQuestionsBySubject(int LessionId);
        Task<List<Question>> GetQuestionsByTitleName(string title);
    }
}
