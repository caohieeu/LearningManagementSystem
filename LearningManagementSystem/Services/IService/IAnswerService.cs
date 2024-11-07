using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;

namespace LearningManagementSystem.Services.IService
{
    public interface IAnswerService
    {
        Task<bool> DeleteAnswer(int id);
        Task<List<AnswerResponseDto>> GetAnswerByQuestion(int questionId);
        Task<bool> InsertAnswer(AnswerRequestDto answer);
        Task<bool> UpdateAnswer(AnswerRequestDto answer, int id);
    }
}
