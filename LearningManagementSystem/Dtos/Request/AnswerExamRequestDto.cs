using LearningManagementSystem.Models;

namespace LearningManagementSystem.Dtos.Request
{
    public class AnswerExamRequestDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
    }
}
