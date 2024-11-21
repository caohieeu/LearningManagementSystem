using LearningManagementSystem.Utils;

namespace LearningManagementSystem.Models
{
    public class QuestionExam
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public QEType Type { get; set; }
        public string SubjectId { get; set; }
        public int Order { get; set; }
    }
}
