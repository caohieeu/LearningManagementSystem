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
    }
}
