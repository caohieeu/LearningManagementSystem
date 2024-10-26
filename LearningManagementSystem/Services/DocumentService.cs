using LearningManagementSystem.Models;
using LearningManagementSystem.Repositories.IRepository;
using LearningManagementSystem.Services.IService;

namespace LearningManagementSystem.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public async Task<IEnumerable<Document>> GetAllDocument()
        {
            return await _documentRepository.GetAll();
        }
        public async Task<bool> InsertDocument()
        {
            return true;
        }
    }
}
