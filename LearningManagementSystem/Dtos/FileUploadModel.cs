using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos
{
    public class FileUploadModel
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}
