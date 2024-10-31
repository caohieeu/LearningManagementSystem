using LearningManagementSystem.Models;
using LearningManagementSystem.Utils;
using System.Diagnostics.Contracts;

namespace LearningManagementSystem.Dtos.Request
{
    public class LessionRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status {  get; set; }
        public string type { get; set; }
        public int TitleId { get; set; }
        public List<string> ListClassId { get; set; }
        public List<int> ListDocumentId { get; set; }
    }
}
