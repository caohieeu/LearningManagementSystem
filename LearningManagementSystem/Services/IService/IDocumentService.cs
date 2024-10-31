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
        Task<bool> AddDocument(DocumentRequestDto document);
    }
}
