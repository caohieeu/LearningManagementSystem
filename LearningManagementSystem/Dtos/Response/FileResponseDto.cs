using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos.Response
{
    public class FileResponseDto
    {
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public FileType FileType { get; set; }
        public string Type { get; set; }
    }
}
