namespace LearningManagementSystem.Dtos.Response
{
    public class AnswerExamResponseDto
    {
        public int Id { get; set; }
        public string answerText { get; set; }
        public int isCorrect {  get; set; }
        public int order { get; set; }
    }
}
