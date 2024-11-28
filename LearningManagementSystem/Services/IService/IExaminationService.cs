using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils.Pagination;

namespace LearningManagementSystem.Services.IService
{
    public interface IExaminationService
    {
        Task<PagedResult<ExaminationResponseDto>> GetExamination(PaginationParams paginationParams, string? DepartmentId,
            string? SubjectId, string? ExamName);
        Task<bool> AddExam(ExaminationRequestDto exam);
        Task<bool> DeleteExam(int id);
        Task<List<QAExamResponseDto>> GetInformationExamination(int id);
        Task<bool> AddFromQuestionBank(ExQuestionBankRequestDto exam);
        Task<PagedResult<QuestionBankResponseDto>> GetQuestionByFilter(FilterQExaminationDto filter, PaginationParams paginationParams);
        Task<bool> AddQuestionBank(QuestionBankRequestDto questionAxamRequestDto);
        Task<bool> DeleteQuestionBank(int questionId);
        Task<bool> UpdateQuestionBank(QuestionBankRequestDto questionAxamRequestDto, int questionId);
    }
}
