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
    }
}
