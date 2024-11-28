using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IExaminationRepository : IRepository<Examination>
    {
        Task<bool> AddExamination(ExaminationRequestDto erd);
        Task<PagedResult<Examination>> GetExamination(PaginationParams paginationParams, string? DepartmentId,
            string? SubjectId, string? ExamName);
        Task<List<QuestionExam>> GetQuestionExamination(int examId);
        Task<List<AnswerExam>> GetAnswerExamination(int questionId);
        Task<bool> AddFromQuestionBank(ExQuestionBankRequestDto exam);
        Task<PagedResult<QuestionExam>> GetQuestionByFilter(FilterQExaminationDto filter, PaginationParams paginationParams);
        Task<bool> AddQuestionBank(QuestionBankRequestDto questionAxamRequestDto);
        Task<bool> DeleteQuestionBank(int questionId);
        Task<bool> UpdateQuestionBank(QuestionBankRequestDto questionAxamRequestDto, int questionId);
    }
}
