namespace LearningManagementSystem.Models
{
    public class QuestionExam
    {
        public int Id { get; set; }
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
        public string QuestionText { get; set; }
        public int Order { get; set; }
    }
}
