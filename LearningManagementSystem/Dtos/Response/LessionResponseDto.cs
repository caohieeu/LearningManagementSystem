using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos.Response
{
    public class LessionResponseDto
    {
        public string Title { get; set; }
        public List<FileResponseDto> FileResponses { get; set; }
    }
}
