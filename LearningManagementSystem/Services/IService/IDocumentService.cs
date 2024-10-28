using LearningManagementSystem.Dtos;
using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Services.IService
{
    public interface IDocumentService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType, string type);
        public Task PostMultiFileAsync(List<FileUploadDto> fileData);
        public Task DownloadFileById(int fileName);
        Task<IEnumerable<Document>> GetAllDocument();
    }
}
