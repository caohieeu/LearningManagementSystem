using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Request
{
    public class AnswerRequestDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsShow { get; set; }
        public int QuestionId { get; set; }
    }
}
