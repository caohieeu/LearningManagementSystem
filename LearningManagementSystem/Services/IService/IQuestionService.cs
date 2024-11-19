using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Services.IService
{
    public interface IQuestionService
    {
        Task<PagedResult<QuestionResponseDto>> GetQuestionByFilter(FilterQuestionDto filter, 
            PaginationParams paginationParams);
        Task<List<QuestionResponseDto>> GetQuestionsByLession(int LessionId);
        Task<List<QuestionResponseDto>> GetQuestionsByTitleName(string title);
        Task<bool> InsertQuestion(QuestionRequestDto question);
    }
}
