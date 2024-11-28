using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Dtos.Response
{
    public class QAExamResponseDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public QEType Type { get; set; }
        public LevelType Level { get; set; }
        public int Order { get; set; }
        public List<AnswerExamResponseDto> answerExams { get; set; }
    }
}
