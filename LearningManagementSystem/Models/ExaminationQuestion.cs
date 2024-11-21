namespace LearningManagementSystem.Models
{
    public class ExaminationQuestion
    {
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
        public int QuestionExamId { get; set; }
        public QuestionExam QuestionExam { get; set; }
    }
}
