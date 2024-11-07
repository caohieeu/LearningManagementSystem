using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;

namespace LearningManagementSystem.Services.IService
{
    public interface IQuestionService
    {
        Task<List<QuestionResponseDto>> GetQuestionsBySubject(int LessionId);
        Task<List<QuestionResponseDto>> GetQuestionsByTitleName(string title);
        Task<bool> InsertQuestion(QuestionRequestDto question);
    }
}
