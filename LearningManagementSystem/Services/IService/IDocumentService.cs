using LearningManagementSystem.Models;

namespace LearningManagementSystem.Services.IService
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAllDocument();
    }
}
