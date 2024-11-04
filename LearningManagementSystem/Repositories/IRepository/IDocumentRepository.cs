using LearningManagementSystem.Models;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<List<Document>> GetDocumentBySubject(string subjectId);
        Task<List<Document>> FindDocumentByName(string name);
    }
}
