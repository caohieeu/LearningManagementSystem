using LearningManagementSystem.Dtos;
using LearningManagementSystem.Dtos.Request;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Services.IService
{
    public interface IDocumentService
    {
        public Task<Document> PostFileAsync(IFormFile fileData, FileType fileType, string type);
        public Task PostMultiFileAsync(List<FileUploadDto> fileData);
        public Task DownloadFileById(int fileName);
        Task<IEnumerable<Document>> GetAllDocument();
        Task<Document> UpdateName(int id, string name);
        Task<bool> RemoveDocument(int id);
        Task<bool> ApproveDocument(int id);
        Task<bool> CancelApproveDocument(int documentId);
        Task<List<Document>> GetDocumentBySubject(string subjectId);
        Task<bool> InsertNewResource(DocumentRequestDto document);
        Task<List<Document>> FindDocumentByName(string documentName);
    }
}
