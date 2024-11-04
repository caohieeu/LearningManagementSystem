using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Request
{
    public class QuestionRequestDto
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public int LessionId { get; set; }
    }
}
