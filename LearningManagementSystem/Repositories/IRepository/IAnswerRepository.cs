using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<List<Answer>> GetAnswerByQuestion(int questionId);
    }   
}
