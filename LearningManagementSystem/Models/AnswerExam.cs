namespace LearningManagementSystem.Models
{
    public class AnswerExam
    {
        public int Id { get; set; }
        public string QuestionExamId { get; set; }
        public QuestionExam QuestionExam { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect {  get; set; }
        public int Order {  get; set; }
    }
}
