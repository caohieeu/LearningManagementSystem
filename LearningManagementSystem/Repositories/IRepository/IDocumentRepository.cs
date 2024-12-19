using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<List<Document>> GetDocumentBySubject(string subjectId);
        Task<List<Document>> GetResoucesByTitleAndClass(int titleId, string classId);
        Task<List<Document>> FindDocumentByName(string name);
    }
}
