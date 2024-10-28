using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos
{
    public class FileUploadDto
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}
