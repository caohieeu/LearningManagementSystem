using LearningManagementSystem.Dtos.Request;

namespace LearningManagementSystem.Services.IService
{
    public interface IQuestionService
    {
        Task<bool> InsertQuestion(QuestionRequestDto question);
    }
}
