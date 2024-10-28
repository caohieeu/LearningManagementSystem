using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;
using System.Diagnostics.Contracts;

namespace LearningManagementSystem.Dtos.Request
{
    public class LessionRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
        public int DocumentId { get; set; }
        public string ClassId { get; set; }
    }
}
