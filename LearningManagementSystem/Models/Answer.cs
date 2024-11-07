namespace LearningManagementSystem.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsShow { get; set; } 
        public DateTime DateTime { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
