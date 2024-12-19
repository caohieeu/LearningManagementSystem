using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Dtos.Response;
using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<List<Subject>> GetAllInclude();
        Task<Subject> GetSubjectById(string id);
        Task<bool> UpdateSubject(SubjectRequestDto subject, string subjectId);
        Task<IEnumerable<Subject>> GetSubjectByStudent(string userId);
        Task<IEnumerable<Subject>> GetSubjectByTeacher(string userId);
    }
}
