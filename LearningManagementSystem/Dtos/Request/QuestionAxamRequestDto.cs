using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos.Request
{
    public class QuestionAxamRequestDto
    {
        public string QuestionText { get; set; } = string.Empty;
        public QType Type { get; set; }
        public int Order { get; set; }
        public LevelType Level { get; set; }
        public List<AnswerExamRequestDto> Answers { get; set; } = 
            new List<AnswerExamRequestDto>();
    }
}
