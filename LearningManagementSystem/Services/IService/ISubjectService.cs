using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services.IService
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectResponseDto>> GetAllSubject();
        Task<bool> AddSubject(SubjectRequestDto subjectDto);
        Task<bool> UpdateSubject(Subject subject);
        Task<bool> DeleteSubject(Subject subject);
    }
}
