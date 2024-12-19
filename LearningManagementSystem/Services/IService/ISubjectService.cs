using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services.IService
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectResponseDto>> GetAllSubject();
        Task<Subject> GetSubject(string id);
        Task<bool> AddSubject(SubjectRequestDto subjectDto);
        Task<bool> UpdateSubject(SubjectRequestDto subject, string subjectId);
        Task<bool> DeleteSubject(Subject subject);
        Task<Subject> GetSubjectById(string subjectId);
        Task<List<SubjectResponseDto>> GetSubjectsByUser();
        Task<bool> AssignSubjectToStudent(AssignSubjectDto subject);
        Task<SubjectDetailDto> GetSubjectDetail(string subjectId, string classId);
    }
}
