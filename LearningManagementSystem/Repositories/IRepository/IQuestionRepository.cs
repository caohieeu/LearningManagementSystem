using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetQuestionsByLession(int LessionId);
        Task<List<Question>> GetQuestionsByTitleName(string title);
        Task<PagedResult<Question>> GetQuestionByFilter(FilterQuestionDto filter, PaginationParams paginationParams);
    }
}
